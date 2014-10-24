using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using AtlirVPNConnect.Util;

namespace AtlirVPNConnect.OpenVPN
{
    internal class ActiveController
    {
        private static Process OpenVpnContainerProcess;

        public static string InterfaceGuid = "";

        public static bool IsRunning
        {
            get { return OpenVpnContainerProcess != null && !OpenVpnContainerProcess.HasExited; }
        }

        /// <summary>
        /// Start the OpenVPN container process
        /// </summary>
        /// <param name="configFile">Config file path</param>
        /// <param name="username">Login Username</param>
        /// <param name="password">Login Password</param>
        public static void Run(string configFile, string username, string password)
        {
            if (IsRunning) return;
            ExitEvent.Reset();
            OpenVpnContainerProcess = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "openvpn.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = "--config " + configFile + " --service " + ExitEvent.Event + " 0",
                },
            };
            var task = EventRegistrar.Status.AddTask("Starting OpenVPN service");
            OpenVpnContainerProcess.OutputDataReceived += OpenVpnContainerProcessOnOutputDataReceived;
            OpenVpnContainerProcess.ErrorDataReceived += OpenVpnContainerProcessOnErrorDataReceived;
            OpenVpnContainerProcess.Exited += OpenVpnContainerProcess_Exited;
            OpenVpnContainerProcess.Start();
            OpenVpnContainerProcess.BeginErrorReadLine();
            OpenVpnContainerProcess.BeginOutputReadLine();
            //Send username(dont have to wait)
            Write(username);
            //Wait for password prompt
            Thread.Sleep(1000);
            //Send password
            Write(password);
            task.Lock();
        }

        private static void Write(string value)
        {
            if (IsRunning)
            {
                OpenVpnContainerProcess.StandardInput.Write(value);
            }
        }

        /// <summary>
        /// Gracefully stops the OpenVPN process, notifying the server of the disconnect
        /// </summary>
        /// <param name="wait">Wait for the process to exit</param>
        public static void SoftKill(bool wait = false)
        {
            EventRegistrar.Status.OnStatusUpdate("Stopping OpenVPN container...");
            if (!IsRunning) return;
            ExitEvent.Set();
            if (wait)
                OpenVpnContainerProcess.WaitForExit(10000);
        }

        /// <summary>
        /// Forces the OpenVPN container process to close
        /// Using this means that OpenVPN will not stop gracefully, which will skip notifying the server of the disconnect
        /// The server will pick up the disconnect typically within 5 minutes of disconnects, via this method
        /// </summary>
        public static void HardKill()
        {
            if (OpenVpnContainerProcess != null && !OpenVpnContainerProcess.HasExited)
            {
                OpenVpnContainerProcess.Kill();
            }
        }

        private static void OpenVpnContainerProcessOnErrorDataReceived(object sender,
            DataReceivedEventArgs dataReceivedEventArgs)
        {
            OpenVpnInterpreter.Message(dataReceivedEventArgs.Data);
            EventRegistrar.OpenVpn.OnErrorRecieved(dataReceivedEventArgs.Data);
        }

        private static void OpenVpnContainerProcessOnOutputDataReceived(object sender,
            DataReceivedEventArgs dataReceivedEventArgs)
        {
            OpenVpnInterpreter.Message(dataReceivedEventArgs.Data);
            EventRegistrar.OpenVpn.OnDataRecieved(dataReceivedEventArgs.Data);
        }

        private static void OpenVpnContainerProcess_Exited(object sender, EventArgs e)
        {
            EventRegistrar.OpenVpn.OnDisconnect();
            EventRegistrar.Status.OnStatusUpdate("OpenVPN process has exited");
        }

        /// <summary>
        /// Controls the threading exit event that we will tell the openvpn process to look for
        /// </summary>
        internal class ExitEvent
        {
            public const string Event = "ov-exit";

            public static void Reset()
            {
                EventWaitHandle handle;
                EventWaitHandle.TryOpenExisting(Event, out handle);
                if (handle != null)
                    handle.Reset();
            }

            public static void Set()
            {
                EventWaitHandle handle;
                EventWaitHandle.TryOpenExisting(Event, out handle);
                if (handle != null)
                    handle.Set();
            }
        }

        internal class OpenVpnInterpreter
        {
            private delegate void MsgPass(string msg);

            public static void Message(string msg)
            {
                foreach (
                    var action in OVpnActions.Where(action => msg != null).Where(action => msg.Contains(action.Key)))
                    action.Value(msg);
            }

            /// <summary>
            /// When messages from the OpenVPN process come in, they are run through this to process the data for anything that actionable
            /// </summary>
            private static readonly Dictionary<string, MsgPass> OVpnActions = new Dictionary<string, MsgPass>()
            {
                {" OpenVPN", msg => EventRegistrar.Status.OnStatusUpdate("Connecting")},
                {
                    "AUTH: Received control message: AUTH_FAILED",
                    msg =>
                        EventRegistrar.Status.OnStatusUpdate(
                            "Failed to login to OpenVPN server. Do you have too many open connections?")
                },
                {
                    "Initialization Sequence Completed", msg =>
                    {
                        EventRegistrar.Status.OnStatusUpdate("Connected to VPN");
                        EventRegistrar.OpenVpn.OnConnect();
                        AdapterController.StartMonitor();
                    }
                },
                {
                    "Notified TAP-Windows driver", msg =>
                    {
                        InterfaceGuid = msg.Substring(msg.IndexOf("{", StringComparison.Ordinal), 39);
                    }
                }
            };
        }
    }
}