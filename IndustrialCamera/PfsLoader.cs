using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Basler.Pylon;

public static class PfsLoader
{
    /// <summary>
    /// Apply parameters from a pylon .pfs file to an OPENED camera.
    /// Supports both XML (.pfs saved as XML) and GenApi text (.pfs as key-value lines).
    /// </summary>
    public static void ApplyFromPfs(Camera camera, string pfsPath)
    {
        if (camera == null) throw new ArgumentNullException(nameof(camera));
        if (!camera.IsOpen) throw new InvalidOperationException("Open the camera before loading PFS.");
        if (string.IsNullOrWhiteSpace(pfsPath)) throw new ArgumentNullException(nameof(pfsPath));
        if (!File.Exists(pfsPath)) throw new FileNotFoundException("PFS file not found", pfsPath);

        // Peek the first non-empty, non-comment char
        using (var sr = new StreamReader(pfsPath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed)) continue;
                if (trimmed.StartsWith("#")) continue; // GenApi text comment
                // XML files start with '<' (<?xml... or <Persistence>)
                if (trimmed.StartsWith("<"))
                {
                    ApplyXml(camera, pfsPath);
                    return;
                }
                // Otherwise assume GenApi text K/V
                ApplyGenApiText(camera, pfsPath);
                return;
            }
        }
    }

    // ------- GenApi text (.pfs) loader -------
    private static void ApplyGenApiText(Camera camera, string pfsPath)
    {
        using (var sr = new StreamReader(pfsPath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length == 0 || line.StartsWith("#")) continue;

                // Expected: FeatureName[TAB]Value
                // Some files may have spaces; split once on whitespace.
                string name, value;
                int tabIdx = line.IndexOf('\t');
                if (tabIdx >= 0)
                {
                    name = line.Substring(0, tabIdx).Trim();
                    value = line.Substring(tabIdx + 1).Trim();
                }
                else
                {
                    // Fallback: split on first whitespace
                    var parts = line.Split((char[])null, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2) continue;
                    name = parts[0].Trim();
                    value = parts[1].Trim();
                }

                TrySet(camera, name, value);
            }
        }
    }

    // ------- XML (.pfs) loader (old format) -------
    private static void ApplyXml(Camera camera, string pfsPath)
    {
        var xml = new XmlDocument();
        xml.Load(pfsPath);
        var nodes = xml.SelectNodes("//Feature");
        if (nodes == null) return;

        foreach (XmlNode node in nodes)
        {
            var name = node.Attributes?["Name"]?.Value;
            if (string.IsNullOrEmpty(name)) continue;

            var rawValue = (node.InnerText ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(rawValue))
                rawValue = node.Attributes?["Value"]?.Value?.Trim();

            if (string.IsNullOrEmpty(rawValue)) continue;

            TrySet(camera, name, rawValue);
        }
    }

    // ------- Typed setter that safely handles enums/float/int/bool/string -------
    private static void TrySet(Camera camera, string name, string rawValue)
    {
        try
        {
            var p = camera.Parameters[name];
            if (p == null || !p.IsWritable) return;

            // Enum
            if (p is IEnumParameter eparam)
            {
                if (eparam.CanSetValue(rawValue))
                {
                    eparam.SetValue(rawValue);
                    return;
                }
                // Sometimes enums are given as int; try map by string anyway
            }

            // Float
            if (p is IFloatParameter fparam &&
                double.TryParse(rawValue, NumberStyles.Float, CultureInfo.InvariantCulture, out double dv))
            {
                dv = Math.Max(fparam.GetMinimum(), Math.Min(fparam.GetMaximum(), dv));
                fparam.SetValue(dv);
                return;
            }

            // Integer
            if (p is IIntegerParameter iparam &&
                long.TryParse(rawValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out long lv))
            {
                lv = Math.Max(iparam.GetMinimum(), Math.Min(iparam.GetMaximum(), lv));
                iparam.SetValue(lv);
                return;
            }

            // Boolean (GenApi text often uses 0/1 or On/Off)
            if (p is IBooleanParameter bparam)
            {
                bool bv = ParseBoolFlexible(rawValue);
                bparam.SetValue(bv);
                return;
            }

            // String
            if (p is IStringParameter sparam)
            {
                sparam.SetValue(rawValue);
                return;
            }
        }
        catch
        {
            // ignore individual feature failures to keep going
        }
    }

    private static bool ParseBoolFlexible(string s)
    {
        if (string.IsNullOrEmpty(s)) return false;
        s = s.Trim().ToLowerInvariant();
        if (s == "1" || s == "true" || s == "on" || s == "yes") return true;
        if (s == "0" || s == "false" || s == "off" || s == "no") return false;
        bool.TryParse(s, out bool b);
        return b;
    }
}
