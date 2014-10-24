using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AtlirVPNConnect.OpenVPN;
using AtlirVPNConnect.Util;
using AtlirVPNConnect.Atlir;


namespace AtlirVPNConnect
{
    public partial class Controller : Form
    {
        public Controller()
        {
            InitializeComponent();
            ActiveController.ExitEvent.Set();
            if (InternalData.SettingsGrid.ResizeForm)
            {
                Width = InternalData.SettingsGrid.Width;
                Height = InternalData.SettingsGrid.Height;
            }
            EventRegistrar.OpenVpn.DataRecieved += OpenVpnEventsOnDataRecieved;
            EventRegistrar.Status.StatusUpdate += data => Invoke((MethodInvoker) (() => statusBar.Text = data));
            EventRegistrar.Status.RunningTasks += data => Invoke((MethodInvoker) (() => statusBar.Tasks = data));
            EventRegistrar.OpenVpn.Connect += OpenVpnOnConnect;
            EventRegistrar.OpenVpn.Disconnect += OpenVpnOnDisconnect;
            EventRegistrar.Interface.UpdateInterfaceStats += InterfaceOnUpdateInterfaceStats;
            infoGrid.SelectedObject = InternalData.InformationGrid;
            settingsGrid.SelectedObject = InternalData.SettingsGrid;
        }

        #region Declerations

        //Cache serverlist for searching
        private readonly List<ListViewItem> ListStorage = new List<ListViewItem>();

        #endregion

        #region EventRegistrar

        private void OpenVpnEventsOnDataRecieved(string data)
        {
            if (!IsDisposed && !consoleOutput.IsDisposed)
            {
                Invoke((MethodInvoker) (() => consoleOutput.Inner.AppendText(data + "\r\n")));
            }
        }

        private void OpenVpnOnConnect()
        {
            Invoke((MethodInvoker) (() =>
            {
                if (Disposing || IsDisposed) return;
                functionButton.Text = "Disconnect";
                disconnectToolStripMenuItem.Visible = true;
                notify.ShowBalloonTip(5000, "AtlirVPN", "Connected to VPN", ToolTipIcon.Info);
            }));
        }

        private void OpenVpnOnDisconnect()
        {
            Invoke((MethodInvoker) (() =>
            {
                if (Disposing || IsDisposed) return;
                functionButton.Text = "Connect";
                disconnectToolStripMenuItem.Visible = false;
                notify.ShowBalloonTip(5000, "AtlirVPN", "Disconnected from VPN", ToolTipIcon.Info);

            }));
        }

        private void InterfaceOnUpdateInterfaceStats()
        {
            if (!IsDisposed && !infoGrid.IsDisposed)
            {
                Invoke((MethodInvoker) (() => infoGrid.Refresh()));
            }
        }

        #endregion

        #region Functions

        private void UpdateServerList(Api.Next done = null)
        {
            serverView.Items.Clear();
            ListStorage.Clear();
            Api.Sorted.ServerList.ServersToItems(
                (success, value) => Invoke((MethodInvoker) (() =>
                {
                    var item = (ListViewItem) (value);
                    if (serverView.Groups[item.SubItems[1].Text] == null)
                    {
                        item.Group = new ListViewGroup(item.SubItems[1].Text, item.SubItems[1].Text);
                        serverView.Groups.Add(item.Group);
                    }
                    else
                        item.Group = serverView.Groups[item.SubItems[1].Text];
                    serverView.Items.Add(item);
                    ListStorage.Add(item);
                })), done);
        }

        private void OpenVpnSwitch(string config, bool ask = true)
        {
            if (ask && ActiveController.IsRunning &&
                (!ActiveController.IsRunning ||
                 MessageBox.Show("Do you want to disconnect from the current server and connect to this one?",
                     "Reconnect", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)) return;
            if (ActiveController.IsRunning)
                ActiveController.SoftKill(true);
            ActiveController.Run(config, InternalData.SettingsGrid.Username, InternalData.SettingsGrid.Password);
        }

        private void CloseApplication()
        {
            Hide();
            notify.Visible = false;
            ActiveController.SoftKill(true);
            Environment.Exit(0);
        }

        private void OpenWindow()
        {
            Show();
            TopMost = true;
            TopMost = false;
        }

        private void HandleConnect(string ip)
        {
            Api.Sorted.CreateConfig(ip, (success, value) =>
            {
                if (!success) return;
                OpenVpnSwitch((string)value);
            });
        }

        private void ConnectToSelected()
        {
            if (serverView.SelectedItems.Count > 0)
            {
                HandleConnect(serverView.SelectedItems[0].Name);
            }
        }

        private void GetConfigForSelected()
        {
            Api.Sorted.CreateConfig(serverView.SelectedItems[0].Name, (success, value) =>
            {
                if (!success ||
                    MessageBox.Show("Show the downloaded config in explorer?", "Show Config", MessageBoxButtons.YesNo) !=
                    DialogResult.Yes) return;
                Process.Start("explorer",
                    "/select,\"" + Path.GetDirectoryName(Application.ExecutablePath) + "\\" + (string) value + "\"");
            });
        }

        /// <summary>
        /// Sort the serverView ListView
        /// </summary>
        /// <param name="type">0 = by location, 1 = by search text, 2 = by ping</param>
        private void Sort(int type)
        {
            var items = new List<ListViewItem>();
            switch (type)
            {
                case 0:
                    serverView.ShowGroups = true;
                    serverView.Items.Clear();
                    serverView.Items.AddRange(ListStorage.ToArray());
                    foreach (var c in ListStorage)
                    {
                        serverView.Groups[c.SubItems[1].Text].Items.Add(c);
                    }
                    return;
                case 1:
                    serverView.ShowGroups = false;
                    items = ListStorage.Where(c => c.Tag.ToString().Contains(searchBox.Text.ToLower())).ToList();
                    break;
                case 2:
                    serverView.ShowGroups = false;
                    items = ListStorage.ToList();
                    break;
            }
            serverView.Items.Clear();
            serverView.Items.AddRange(items.OrderBy(c => Convert.ToInt32(c.SubItems[4].Text)).ToArray());
        }

        private void AutoConnect()
        {
            var lowestPing = ListStorage.OrderBy(c => Convert.ToInt32(c.SubItems[4].Text)).FirstOrDefault();
            Console.WriteLine(lowestPing);
            if(lowestPing != null)
                HandleConnect(lowestPing.Name);
        }

        #endregion

        #region Form Events

        //Handle silent startup
        private void Controller_Load(object sender, EventArgs e)
        {
            if (Program.Arguments.Contains("-silent"))
            {
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
            }
        }

        //Handle silent startup
        private void Controller_Shown(object sender, EventArgs e)
        {
            Api.Next done = null;
            if (InternalData.SettingsGrid.AutoConnect)
                done = (success, value) => AutoConnect();
            UpdateServerList(done);
            if (Program.Arguments.Contains("-silent"))
            {
                Visible = false;
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
            }
        }

        //Tray menu
        private void notify_DoubleClick(object sender, EventArgs e)
        {
            OpenWindow();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenWindow();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveController.SoftKill();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseApplication();
        }

        //Connecting
        private void serverView_DoubleClick(object sender, EventArgs e)
        {
            ConnectToSelected();
        }

        //Searching/sorting
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            Sort(!string.IsNullOrEmpty(searchBox.Text) ? 1 : 0);
        }

        //ServerViewMenu
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectToSelected();
        }

        private void downloadConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetConfigForSelected();
        }

        //ServerViewMenu: Sorting
        private void locationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sort(0);
        }

        private void pingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Sort(2);
        }

        //ServerViewMenu
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateServerList();
        }


        //Misc
        private void Controller_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void connectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutoConnect();
        }

        private void functionButton_Click(object sender, EventArgs e)
        {
            if (ActiveController.IsRunning)
                ActiveController.SoftKill();
            else
            {
                if (serverView.SelectedItems.Count > 0)
                    ConnectToSelected();
                else
                    AutoConnect();
            }
        }

        private void Controller_ResizeEnd(object sender, EventArgs e)
        {
            InternalData.SettingsGrid.Height = Height;
            InternalData.SettingsGrid.Width = Width;
            InternalData.SaveSettings();
        }

        private void settingsGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            InternalData.SaveSettings();
        }

        #endregion
    }
}