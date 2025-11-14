using System;
using System.Drawing;
using System.Drawing.Imaging;
using Basler.Pylon;

namespace IndustrialCamera
{
    public class IndustrialCamera
    {
        private Camera _camera;
        private readonly PixelDataConverter _converter = new PixelDataConverter();
        private volatile bool _grabbing;
        private readonly object _imgLock = new object();
        private Bitmap _lastFrame;

        public string Serial { get; private set; }
        public string PfsPath { get; set; }  // optional: .pfs to apply after Open

        public bool IsOpen => _camera != null && _camera.IsOpen;
        public bool IsGrabbing => _camera != null && _camera.StreamGrabber.IsGrabbing;

        /// <summary>Raised on UI/consumer thread via caller. Emits a cloned Bitmap you own & must Dispose when replaced.</summary>
        public event Action<Bitmap> FrameReady;

        public void OpenBySerial(string serial)
        {
            if (string.IsNullOrWhiteSpace(serial))
                throw new ArgumentException("Serial cannot be empty.", nameof(serial));

            Serial = serial;
            _camera = new Camera(serial);
            _camera.Open();

            // Optional: load .pfs configuration if provided
            if (!string.IsNullOrWhiteSpace(PfsPath))
            {
                TryApplyPfs(PfsPath);
            }

            // Optional: set pixel format for convenient display
            if (_camera.Parameters[PLCamera.PixelFormat].IsWritable)
            {
                if (!_camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerRG8))
                    _camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void OpenByEnumerating(string serial)
        {
            if (string.IsNullOrWhiteSpace(serial))
            {
                _camera = new Camera();
                _camera.Open();
                return;
            }

            var list = CameraFinder.Enumerate();
            var info = list.Find(ci =>
                ci.ContainsKey(CameraInfoKey.SerialNumber) &&
                ci[CameraInfoKey.SerialNumber] == serial);

            if (info == null) throw new Exception($"Camera with S/N {serial} not found.");

            Serial = serial;
            _camera = new Camera(info);
            _camera.Open();

            if (!string.IsNullOrWhiteSpace(PfsPath))
            {
                TryApplyPfs(PfsPath);
            }

            //if (_camera.Parameters[PLCamera.PixelFormat].IsWritable)
            //{
            //    if (!_camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerRG8))
            //        _camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8);
            //}
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Start()
        {
            if (_camera == null || !_camera.IsOpen)
                throw new InvalidOperationException("Open the camera before Start().");

            if (_grabbing) return;

            _camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
            _camera.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
            _grabbing = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Stop()
        {
            _grabbing = false;
            if (_camera != null)
            {
                _camera.StreamGrabber.ImageGrabbed -= OnImageGrabbed;
                if (_camera.StreamGrabber.IsGrabbing)
                    _camera.StreamGrabber.Stop();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Close()
        {
            Stop();
            if (_camera != null)
            {
                if (_camera.IsOpen) _camera.Close();
                _camera.Dispose();
                _camera = null;
            }

            lock (_imgLock)
            {
                _lastFrame?.Dispose();
                _lastFrame = null;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Dispose() => Close();

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            if (!_grabbing) return;

            try
            {
                using (IGrabResult result = e.GrabResult)
                {
                    if (!result.GrabSucceeded) return;

                    _converter.OutputPixelFormat = PixelType.BGR8packed;

                    using (var bmp = new Bitmap(result.Width, result.Height, PixelFormat.Format24bppRgb))
                    {
                        var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                        var data = bmp.LockBits(rect, ImageLockMode.WriteOnly, bmp.PixelFormat);
                        try
                        {
                            _converter.Convert(data.Scan0, data.Stride * data.Height, result);
                        }
                        finally
                        {
                            bmp.UnlockBits(data);
                        }

                        var frame = (Bitmap)bmp.Clone(); // emit a clone

                        // manage internal last frame to avoid leaks if consumer forgets
                        lock (_imgLock)
                        {
                            _lastFrame?.Dispose();
                            _lastFrame = frame;
                        }

                        // Caller (Form) should marshal to UI thread if needed
                        FrameReady?.Invoke(frame);
                    }
                }
            }
            catch
            {
                // swallow transient errors during stop/close
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void TryApplyPfs(string path)
        {
            try
            {
                PfsLoader.ApplyFromPfs(_camera, path);
            }
            catch
            {
                // ignore invalid/partial pfs; continue with defaults
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void ForceColorPixelFormat(Camera cam)
        {
            var pf = cam.Parameters[PLCamera.PixelFormat];
            if (!pf.IsWritable) return;

            // Bayer first (debayer to color), then try YCbCr422_8 as fallback
            string[] preferred = {
                PLCamera.PixelFormat.BayerBG8,     // your camera supports this
                PLCamera.PixelFormat.YCbCr422_8    // also color on many Baslers
            };

            foreach (var fmt in preferred)
            {
                if (pf.CanSetValue(fmt) && pf.TrySetValue(fmt))
                    return; // success
            }
            // If we get here, we stay Mono8 (grayscale)
        }
    }

}
