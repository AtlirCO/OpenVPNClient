using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AtlirVPNConnect.Util
{
    class Startup
    {
        private const string StartupName = "AtlirVPN";

        public static void CreateStartup(bool silent)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key != null) key.SetValue(StartupName, "\"" + Application.ExecutablePath + "\"" + ((silent) ? " -silent" : ""));
        }

        public static void AdminStartup(bool silent)
        {
            DeleteStartup();
            CreateTask(silent);
        }

        public static void DeleteStartup(bool schtasks = true)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key != null && key.GetValue(StartupName) != null)
                key.DeleteValue(StartupName);
            if (FindTask(StartupName))
                DeleteTask();
        }

        private static void CreateTask(bool silent)
        {
            var start = new ProcessStartInfo
            {
                FileName = "schtasks",
                Arguments = "/create /sc onlogon /tn " + StartupName + " /rl highest /tr \"" + Application.ExecutablePath + " " + ((silent) ? " -silent" : "") + "\"",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            Process.Start(start);
        }

        /// <summary>
        /// Schtasks will end the task if it runs too long, so we have to restart the program so that it wont be killed
        /// </summary>
        public static void CheckRestart()
        {
            if (!TaskInfo(StartupName).Contains("Running")) return;
            Process.Start(Application.ExecutablePath, Program.Arguments);
            Environment.Exit(0);
        }

        private static void DeleteTask()
        {
            var start = new ProcessStartInfo
            {
                FileName = "schtasks",
                Arguments = "/delete /f /tn " + StartupName,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(start);
        }

        /// <summary>
        /// Query Schtasks for information about a task
        /// </summary>
        /// <param name="task">Task name</param>
        /// <returns></returns>
        private static string TaskInfo(string task)
        {
            var start = new ProcessStartInfo
            {
                FileName = "schtasks.exe",
                Arguments = "/query /TN " + task,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Check if a task exists
        /// </summary>
        /// <param name="name">The name of the task</param>
        /// <returns></returns>
        private static bool FindTask(string name)
        {
            return TaskInfo(name).Contains(name);
        }
    }
}
