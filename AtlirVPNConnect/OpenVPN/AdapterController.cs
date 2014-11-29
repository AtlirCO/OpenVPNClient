using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using AtlirVPNConnect.Util;

namespace AtlirVPNConnect.OpenVPN
{
    class AdapterController
    {
        private static Thread MonThread;
        private static NetworkInterface ActiveInterface;

        public static NetworkInterface InterfaceAvaliable()
        {
            if (string.IsNullOrEmpty(ActiveController.InterfaceGuid)) return null;
            return NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(ni => ni.Id.Substring(1, 32) == ActiveController.InterfaceGuid.Substring(1, 32));
        }

        public static void StartMonitor()
        {
            if (MonThread != null && MonThread.IsAlive) return;
            MonThread = new Thread(() =>
            {
                ActiveInterface = InterfaceAvaliable();
                if (ActiveInterface == null) return;
                while (ActiveController.IsRunning)
                {
                    var stats = ActiveInterface.GetIPStatistics();
                    InternalData.InformationGrid.UploadCount = ((stats.BytesSent / 1024) / 1024) + "MB";
                    InternalData.InformationGrid.DownloadCount = ((stats.BytesReceived / 1024) / 1024) + "MB";
                    InternalData.InformationGrid.MacAddr = ActiveInterface.GetPhysicalAddress().ToString();
                    InternalData.InformationGrid.Name = ActiveInterface.Name;
                    InternalData.InformationGrid.Guid = ActiveInterface.Id;
                    InternalData.InformationGrid.DNS = String.Join(", ", ActiveInterface.GetIPProperties().DnsAddresses);
                    InternalData.InformationGrid.ipAddress = getIP();
                    EventRegistrar.Interface.OnUpdateInterfaceStats();
                    Thread.Sleep(10000);
                }
            })
            {
                IsBackground = true
            };
            MonThread.Start();
        }

        public static String getIP()
        {
            var ipAddress = "";
            foreach (UnicastIPAddressInformation ip in ActiveInterface.GetIPProperties().UnicastAddresses)
            {
                if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddress = (ip.Address.ToString());
                }
            }
            return ipAddress;
        }
    }
}
