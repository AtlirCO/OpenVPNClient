using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using AtlirVPNConnect.Util;

namespace AtlirVPNConnect
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Arguments = string.Join(" ", args);
            PreLoad();
            Application.Run(new Login());
        }

        public static string Arguments;

        private static void PreLoad()
        {
            Util.EnsureAdmin();
            Util.MutexCheck();
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));
            InternalData.LoadSettings();
            Util.VerifyInstall();
            if (InternalData.SettingsGrid.Startup && InternalData.SettingsGrid.SchTasks)
                Startup.CheckRestart();
        }

        internal class Util
        {

            private static Mutex MutexHandle;

            public static bool IsAdministrator()
            {
                return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            }

            /// <summary>
            /// ClickOnce doesn't allow UAC, so this is a work around that enforces Administrator without having to use the manifest.
            /// </summary>
            public static void EnsureAdmin()
            {
                if (IsAdministrator()) return;
                if (!new Process { StartInfo = new ProcessStartInfo { FileName = Application.ExecutablePath, UseShellExecute = true, Verb = "runas" } }.Start())
                    MessageBox.Show("Administrator privileges are required for this application to start.", "UAC Error");
                Environment.Exit(0);
            }

            /// <summary>
            /// Verfies the OpenVPN installation and ensures the tap driver was installed.
            /// If the registry keys do not exist but the process is still avaliable for use, then ignore and continue.
            /// </summary>
            public static void VerifyInstall()
            {
                try
                {
                    if (
                        new Process
                        {
                            StartInfo =
                            {
                                FileName = "openvpn",
                                UseShellExecute = true,
                                CreateNoWindow = true,
                                WindowStyle = ProcessWindowStyle.Hidden
                            }
                        }.Start())
                    {
                        InternalData.SaveSettings();
                        return;
                    }

                }
                catch (Exception)
                {
                    
                }
                MessageBox.Show(
                        "OpenVPN and the TAP driver are required for this application to work. Please follow the install guide on Atlir.org.",
                        "Installation Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            /// <summary>
            /// Ensure that we only have one instance of the application running
            /// </summary>
            public static void MutexCheck()
            {
                try
                {
                    Mutex.OpenExisting("AtlirVPN");
                }
                catch
                {
                    MutexHandle = new Mutex(true, "AtlirVPN");
                    return;
                }
                Environment.Exit(1);
            }
        }
    }
}