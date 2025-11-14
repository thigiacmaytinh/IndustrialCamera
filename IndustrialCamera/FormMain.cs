using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TGMTcs;

namespace IndustrialCamera
{
    public partial class FormMain : Form
    {
        static FormMain m_instance;

        IndustrialCamera _camera;

        public FormMain()
        {
            InitializeComponent();

            picCamera.SizeMode = PictureBoxSizeMode.Zoom;

            _camera = new IndustrialCamera();
            _camera.FrameReady += OnFrameReady;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static FormMain GetInstance()
        {
            if (m_instance == null)
                m_instance = new FormMain();
            return m_instance;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void FormMain_Load(object sender, EventArgs e)
        {
            txt_serial.Text = TGMTregistry.GetInstance().ReadString("CameraSerial");
            txt_configFile.Text = TGMTregistry.GetInstance().ReadString("CameraConfigFile");

            if (!Directory.Exists("pictures"))
                Directory.CreateDirectory("pictures");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _camera.Dispose();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try 
            { 
                _camera?.Dispose(); 
            } 
            catch { }

            base.OnFormClosing(e);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void txt_serial_TextChanged(object sender, EventArgs e)
        {
            TGMTregistry.GetInstance().SaveValue("CameraSerial", txt_serial.Text.Trim());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void txt_configFile_TextChanged(object sender, EventArgs e)
        {
            TGMTregistry.GetInstance().SaveValue("CameraConfigFile", txt_configFile.Text.Trim());
        }       

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_start_Click(object sender, EventArgs e)
        {
            try
            {
                string serial = txt_serial.Text.Trim();
                string pfsPath = txt_configFile.Text.Trim();

                if (!string.IsNullOrWhiteSpace(pfsPath))
                    _camera.PfsPath = pfsPath;

                if (!_camera.IsOpen)
                    _camera.OpenByEnumerating(serial);

                _camera.Start();

                btn_start.Enabled = false;
                btn_stop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Start failed: " + ex.Message);
                try 
                { 
                    _camera?.Close(); 
                } 
                catch 
                { 
                }

                btn_start.Enabled = true; 
                btn_stop.Enabled = false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_stop_Click(object sender, EventArgs e)
        {
            try 
            { 
                _camera?.Stop(); 
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message); 
            }
            finally
            {
                btn_start.Enabled = true; 
                btn_stop.Enabled = false;
                var old = picCamera.Image as Bitmap; 
                picCamera.Image = null; 
                old?.Dispose();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_takePicture_Click(object sender, EventArgs e)
        {
            Bitmap bmp = picCamera.Image.Clone() as Bitmap;
            if (bmp == null) return;
            string fileName = Path.Combine("pictures", DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg");
            try
            {
                bmp.Save(fileName, ImageFormat.Jpeg);
                MessageBox.Show("Picture saved: " + fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save picture: " + ex.Message);
            }
            finally
            {
                bmp.Dispose();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnFrameReady(Bitmap frame)
        {
            // Marshal to UI thread if needed
            if (picCamera.InvokeRequired)
            {
                picCamera.BeginInvoke(new Action<Bitmap>(OnFrameReady), frame);
                return;
            }

            // Swap image and dispose the old one
            var old = picCamera.Image as Bitmap;
            picCamera.Image = frame;
            old?.Dispose();
        }
    }
}
