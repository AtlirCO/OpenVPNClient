using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtlirVPNConnect.Util;
using Newtonsoft.Json.Linq;

namespace AtlirVPNConnect.Atlir
{
    internal class Api
    {
        /// <summary>
        /// Next callback
        /// </summary>
        /// <param name="success">Whether or not the action was successful</param>
        /// <param name="value">A object returned by the function (requested data or error information)</param>
        public delegate void Next(bool success, object value);

        public static int AsyncThreads = 0;

        public static RequestBuilder Session = new RequestBuilder();

        /// <summary>
        /// Async login
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="next">Async callback</param>
        public static void Login(string username, string password, Next next)
        {
            Async(() =>
            {
                try
                {
                    var sender = new JObject();
                    sender["username"] = username;
                    sender["password"] = password;
                    var jO = JObject.Parse(Session.Post("login", sender.ToString()));
                    if (jO["msg"] != null)
                    {
                        next(true, jO["msg"].ToString());
                    }
                    else
                    {
                        next(false, jO["emsg"].ToString());
                    }
                }
                catch (Exception)
                {
                    next(false, "Could not connect, please try again.");
                }
            });
        }

        /// <summary>
        /// Get the active server list from the push api
        /// </summary>
        /// <param name="next">Callback for the async handler</param>
        public static void Servers(Next next)
        {
            Async(() =>
            {
                var eval = Session.Get("servers");
                if (eval == null) return;
                var jO = JArray.Parse(Session.Get("servers"));
                next(jO != null, jO);
            });
        }

        /// <summary>delPort
        /// Remove Ports using the push api
        /// </summary>
        /// <param name="next">Callback for the async handler</param>
        public static void delPort(int port, string serverIP, string InterIPAddress, Next next)
        {
            Async(() =>
            {
                //var eval = Session.Get("ports/del/" + InternalData.SettingsGrid.Username + "/" + InternalData.InformationGrid.ipAddress + "/" + port + "/" + serverIP);
                var eval = Session.Get("ports/del/" + InternalData.SettingsGrid.Username + "/" + InterIPAddress + "/" + port + "/" + serverIP);
                if (eval == null) return;
                var jO = JObject.Parse(eval); 
                if (jO["msg"] != null)
                {
                    next(true, jO["msg"].ToString());
                }
                else
                {
                    next(false, jO["emsg"].ToString());
                }
            });
        }

        /// <summary>
        /// Adds Ports using the push api
        /// </summary>
        /// <param name="next">Callback for the async handler</param>
        public static void addPort(int port, string serverIP, string InterIPAddress, Next next)
        {
            Async(() =>
            {
                //var eval = Session.Get("ports/add/" + InternalData.SettingsGrid.Username + "/" + InternalData.InformationGrid.ipAddress + "/" + port + "/" + serverIP);
                var eval = Session.Get("ports/add/" + InternalData.SettingsGrid.Username + "/" + InterIPAddress + "/" + port + "/" + serverIP);
                if (eval == null) return;
                var jO = JObject.Parse(eval);
                if (jO["msg"] != null)
                {
                    next(true, jO["msg"].ToString());
                }
                else
                {
                    next(false, jO["emsg"].ToString());
                }
            });
        }

        /// <summary>
        /// Updates Internal IP for portforwarding using the push api
        /// </summary>
        /// <param name="next">Callback for the async handler</param>
        public static void updatePort(int port, string serverIP, string InterIPAddress, Next next)
        {
            Async(() =>
            {
                //var eval = Session.Get("ports/add/" + InternalData.SettingsGrid.Username + "/" + InternalData.InformationGrid.ipAddress + "/" + port + "/" + serverIP);
                var eval = Session.Get("ports/update/" + InternalData.SettingsGrid.Username + "/" + InterIPAddress + "/" + port + "/" + serverIP);
                if (eval == null) return;
                var jO = JObject.Parse(eval);
                if (jO["msg"] != null)
                {
                    next(true, jO["msg"].ToString());
                }
                else
                {
                    next(false, jO["emsg"].ToString());
                }
            });
        }

        /// <summary>
        /// Get the ports open for the user
        /// </summary>
        /// <param name="next">Callback for the async handler</param>
        public static void getPorts(Next next)
        {
            Async(() =>
            {
                var eval = Session.Get("ports/get/" + InternalData.SettingsGrid.Username);
                if (eval == null) return;
                var jO = JArray.Parse(Session.Get("ports/get/" + InternalData.SettingsGrid.Username)); 
                next(jO != null, jO);
            });
        }

        /// <summary>
        /// Gets the non-default, config from the server containg only the certs for the server
        /// </summary>
        /// <param name="ip">Ip of the server</param>
        /// <param name="next">Callback for the async handler</param>
        public static void GetCerts(string ip, Next next)
        {
            Async(() =>
            {
                var config = Session.Get("config/" + ip);
                next(config != "404", config);
            });
        }


        /// <summary>
        /// Get the user statistics
        /// </summary>
        /// <param name="next">Callback for the async handler</param>
        public static void GetStats(Next next)
        {
            Async(() =>
            {
                string ret = Session.Get("stats");
                next(ret != "404", ret);
            });
        }

        private static void Async(ThreadStart method)
        {
            new Thread(() =>
            {
                AsyncThreads++;
                method();
                AsyncThreads--;
            })
            {
                IsBackground = true
            }.Start();
        }

        /// <summary>
        /// Organizes the raw API into a form used for this application
        /// </summary>
        public class Sorted
        {
            public static void Stats(Next next)
            {
                Async(() => GetStats((success, value) =>
                {
                    if (success)
                    {
                        var jO = (JObject) value;
                        var data = new Dictionary<string, string>();
                        data["username"] = jO["user"]["username"].ToString();
                        data["activeConnections"] = jO["user"]["activeConnections"].ToString();
                        data["subscription"] = jO["user"]["subscription"].ToString();
                        data["serversAvailable"] = jO["server"]["serversAvailable"].ToString();
                        next(true, data);
                    }
                }));
            }

            public static void CreateConfig(string ip, Next next)
            {
                Async(() => GetCerts(ip, (success, value) =>
                {
                    var task = EventRegistrar.Status.AddTask("Creating config for " + ip);
                    if (!Directory.Exists("ovpn"))
                        Directory.CreateDirectory("ovpn");
                    if (success)
                    {
                        var blockedSites = "";
                        if (!string.IsNullOrEmpty(InternalData.SettingsGrid.Ignore))
                        {
                            blockedSites = InternalData.SettingsGrid.Ignore.Trim().Replace("\r\n", ",").Split(',')
                                .Aggregate("",
                                    (current, site) =>
                                        current +
                                        ("route " + site.Replace(" ", "") + " 255.255.255.255 net_gateway\r\n"));
                        }
                        File.WriteAllText("ovpn\\" + ip + ".ovpn",
                            Properties.Settings.Default.Config.Replace("$ip", ip) + "\r\n" + blockedSites + "\r\n" +
                            (string) value);

                    }
                    next(success, "ovpn\\" + ip + ".ovpn");
                    task.Lock();
                }));
            }

            public class ServerList
            {
                public static void ServersToItems(Next next, Next done = null)
                {
                    Servers((success, value) =>
                    {
                        var s = EventRegistrar.Status.AddTask("Fetching servers, and ping tests");
                        var items = ((JArray)value).Select(jO =>
                        {
                            var name = new[]
                            {
                                jO["name"].ToString(),
                                jO["locationCountry"].ToString(),
                                jO["locationCity"].ToString(),
                                jO["ip"].ToString()
                            };
                            return new ListViewItem(name)
                            {
                                Name = jO["ip"].ToString(),
                                Tag = string.Join(" ", name).ToLower()
                            };
                        })
                            .ToList();
                        Parallel.ForEach(items, new ParallelOptions { MaxDegreeOfParallelism = 10 },
                            item =>
                            {
                                var pingSender = new Ping();
                                var reply = pingSender.Send(IPAddress.Parse(item.Name), 5000);
                                if (reply != null && reply.Status != IPStatus.Success) return;
                                item.SubItems.Add(reply.RoundtripTime + "");
                                next(true, item);
                            });
                        s.Lock();
                        if (done != null)
                            done(true, null);
                    });
                }

                public static void loadPorts(Next next, Next done = null)
                {
                    getPorts((success, value) =>
                    {
                        var s = EventRegistrar.Status.AddTask("Fetching ports");
                        var items = ((JArray)value).Select(jO =>
                        {
                            var name = new[]
                            {
                                jO["serverip"].ToString(),
                                jO["port"].ToString(),
                                jO["internalip"].ToString(),
                                jO["createdAt"].ToString(),
                                jO["updatedAt"].ToString()
                            };
                            return new ListViewItem(name)
                            {
                                Name = jO["id"].ToString(),
                                Tag = string.Join(" ", name).ToLower()
                            };
                        })
                            .ToList(); 
                        Parallel.ForEach(items, new ParallelOptions { MaxDegreeOfParallelism = 10 },
                            item =>
                            {
                                next(true, item);
                            });
                        s.Lock();
                        if (done != null)
                            done(true, null);
                    });
                }

                public static void ServersToDropDown(Next next, Next done = null)
                {
                    Servers((success, value) =>
                    {
                        var s = EventRegistrar.Status.AddTask("Fetching Servers for Portforwarding");
                        var items = ((JArray)value).Select(jO =>
                        {
                            var name = new[]
                            {
                                jO["name"].ToString(),
                                jO["ip"].ToString()
                            };
                            return new ListViewItem(name)
                            {
                                Name = jO["ip"].ToString(),
                                Tag = string.Join(" ", name).ToLower()
                            };
                        })
                            .ToList();
                        s.Lock();
                        if (done != null)
                            done(true, null);
                    });
                }
            }
        }
    }
}