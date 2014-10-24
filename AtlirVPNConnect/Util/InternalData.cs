using System;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

namespace AtlirVPNConnect.Util
{
    public class InternalData
    {
        public static InfoGrid InformationGrid = new InfoGrid();
        public static SetGrid SettingsGrid = new SetGrid();
        private const string StorageName = "AtlirVPN";
        private static readonly string BaseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + StorageName;

        public static void SaveSettings()
        {
            if (string.IsNullOrEmpty(SettingsGrid.Config))
                SettingsGrid.Config = Properties.Settings.Default.Config;
            if (SettingsGrid.Startup)
            {
                if (!SettingsGrid.SchTasks)
                    Startup.CreateStartup(SettingsGrid.Hidden);
                else
                    Startup.AdminStartup(SettingsGrid.Hidden);
            }
            else
                Startup.DeleteStartup();
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);
            File.WriteAllText(BaseDir + "\\settings.json", JsonConvert.SerializeObject(SettingsGrid));
        }

        public static void LoadSettings()
        {
            if (!Directory.Exists(BaseDir) && !File.Exists(BaseDir + "\\settings.json")) return;
            SettingsGrid = JsonConvert.DeserializeObject<SetGrid>(File.ReadAllText(BaseDir + "\\settings.json"));
            if (string.IsNullOrEmpty(SettingsGrid.Config))
                SettingsGrid.Config = Properties.Settings.Default.Config;
        }

        public class InfoGrid
        {
            [Category("Network Transfer")]
            [DisplayName("Upload")]
            [Description("Total upload on the current interface.")]
            [ReadOnly(true)]
            public string UploadCount { get; set; }

            [Category("Network Transfer")]
            [DisplayName("Download")]
            [Description("Total download on the current interface.")]
            [ReadOnly(true)]
            public string DownloadCount { get; set; }

            [Category("Network Info")]
            [DisplayName("MAC")]
            [Description("MAC address for the network interface.")]
            [ReadOnly(true)]
            public string MacAddr { get; set; }

            [Category("Network Info")]
            [DisplayName("Name")]
            [Description("Interface Name.")]
            [ReadOnly(true)]
            public string Name { get; set; }

            [Category("Network Info")]
            [DisplayName("GUID")]
            [Description("Interface GUID.")]
            [ReadOnly(true)]
            public string Guid { get; set; }

            [Category("Network Info")]
            [DisplayName("DNS Servers")]
            [Description("DNS Servers for this network.")]
            [ReadOnly(true)]
            public string DNS { get; set; }
        }

        public class SetGrid
        {
            [Category("Startup")]
            [DisplayName("Enable Startup")]
            [Description("Startup with windows.")]
            [DefaultValue(false)]
            public bool Startup { get; set; }

            [Category("Startup")]
            [DisplayName("Startup Minimized")]
            [Description("Startup with windows in hidden mode.")]
            [DefaultValue(false)]
            public bool Hidden { get; set; }

            [Category("Startup")]
            [DisplayName("UAC Startup")]
            [Description("Uses schtasks to create a administrator task to startup without requesting UAC.")]
            [DefaultValue(false)]
            public bool SchTasks { get; set; }

            [Category("OpenVPN Config")]
            [DisplayName("Block Sites")]
            [Description("Block (route) sites from the VPN. These sites will be routed through the default network instead.")]
            [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public string Ignore { get; set; }

            [Category("OpenVPN Config")]
            [DisplayName("Config Settings")]
            [Description("Edit the config settings for the .ovpn files.")]
            [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public string Config { get; set; }

            [DisplayName("Save Window Size")]
            [Description("Restore the window size, that the application was left, on next start.")]
            [DefaultValue(true)]
            public bool ResizeForm { get; set; }

            [DisplayName("Auto Connect")]
            [Description("Auto connect to the server with least ping on start.")]
            [DefaultValue(false)]
            public bool AutoConnect { get; set; }

            [Browsable(false)]
            public int Height { get; set; }
            [Browsable(false)]
            public int Width { get; set; }

            [Browsable(false)]
            public string Username;
            [Browsable(false)]
            public string Password;
        }
    }
}