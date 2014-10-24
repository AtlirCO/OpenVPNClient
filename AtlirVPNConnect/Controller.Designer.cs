namespace AtlirVPNConnect
{
    partial class Controller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Controller));
            this.serverViewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new StatusBar();
            this.header1 = new Header();
            this.tabs1 = new Tabs();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.searchBox = new TextArea();
            this.functionButton = new Button();
            this.serverView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.infoGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.consoleOutput = new TextArea();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.settingsGrid = new System.Windows.Forms.PropertyGrid();
            this.serverViewMenu.SuspendLayout();
            this.trayMenu.SuspendLayout();
            this.tabs1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverViewMenu
            // 
            this.serverViewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.downloadConfigToolStripMenuItem,
            this.sortToolStripMenuItem,
            this.toolStripSeparator2,
            this.refreshToolStripMenuItem});
            this.serverViewMenu.Name = "serverViewMenu";
            this.serverViewMenu.Size = new System.Drawing.Size(153, 98);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // downloadConfigToolStripMenuItem
            // 
            this.downloadConfigToolStripMenuItem.Name = "downloadConfigToolStripMenuItem";
            this.downloadConfigToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.downloadConfigToolStripMenuItem.Text = "Get Config File";
            this.downloadConfigToolStripMenuItem.Click += new System.EventHandler(this.downloadConfigToolStripMenuItem_Click);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locationToolStripMenuItem,
            this.pingToolStripMenuItem1});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sortToolStripMenuItem.Text = "Sort";
            // 
            // locationToolStripMenuItem
            // 
            this.locationToolStripMenuItem.Name = "locationToolStripMenuItem";
            this.locationToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.locationToolStripMenuItem.Text = "Location";
            this.locationToolStripMenuItem.Click += new System.EventHandler(this.locationToolStripMenuItem_Click);
            // 
            // pingToolStripMenuItem1
            // 
            this.pingToolStripMenuItem1.Name = "pingToolStripMenuItem1";
            this.pingToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.pingToolStripMenuItem1.Text = "Ping";
            this.pingToolStripMenuItem1.Click += new System.EventHandler(this.pingToolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // notify
            // 
            this.notify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notify.ContextMenuStrip = this.trayMenu;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Text = "AtlirVPN";
            this.notify.Visible = true;
            this.notify.DoubleClick += new System.EventHandler(this.notify_DoubleClick);
            // 
            // trayMenu
            // 
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.connectToolStripMenuItem1,
            this.disconnectToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.trayMenu.Size = new System.Drawing.Size(134, 98);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem1
            // 
            this.connectToolStripMenuItem1.Name = "connectToolStripMenuItem1";
            this.connectToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.connectToolStripMenuItem1.Text = "Connect";
            this.connectToolStripMenuItem1.Click += new System.EventHandler(this.connectToolStripMenuItem1_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Visible = false;
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusBar
            // 
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar.Font = new System.Drawing.Font("Source Sans Pro", 9F);
            this.statusBar.Location = new System.Drawing.Point(0, 277);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(603, 24);
            this.statusBar.TabIndex = 8;
            this.statusBar.Tasks = null;
            this.statusBar.Text = "Ready";
            // 
            // header1
            // 
            this.header1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Font = new System.Drawing.Font("Source Sans Pro", 12F);
            this.header1.Location = new System.Drawing.Point(0, 0);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(603, 40);
            this.header1.TabIndex = 8;
            this.header1.Text = "AtlirVPN";
            // 
            // tabs1
            // 
            this.tabs1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs1.Controls.Add(this.tabPage1);
            this.tabs1.Controls.Add(this.tabPage2);
            this.tabs1.Controls.Add(this.tabPage3);
            this.tabs1.Controls.Add(this.tabPage4);
            this.tabs1.Location = new System.Drawing.Point(12, 46);
            this.tabs1.Name = "tabs1";
            this.tabs1.SelectedIndex = 0;
            this.tabs1.Size = new System.Drawing.Size(579, 225);
            this.tabs1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPage1.Controls.Add(this.searchBox);
            this.tabPage1.Controls.Add(this.functionButton);
            this.tabPage1.Controls.Add(this.serverView);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(571, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Servers";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(3, 4);
            this.searchBox.Multiline = false;
            this.searchBox.Name = "searchBox";
            this.searchBox.Password = false;
            this.searchBox.ReadOnly = false;
            this.searchBox.Size = new System.Drawing.Size(234, 23);
            this.searchBox.TabIndex = 2;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // functionButton
            // 
            this.functionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.functionButton.Location = new System.Drawing.Point(488, 3);
            this.functionButton.Name = "functionButton";
            this.functionButton.Size = new System.Drawing.Size(80, 25);
            this.functionButton.TabIndex = 0;
            this.functionButton.Text = "Connect";
            this.functionButton.Click += new System.EventHandler(this.functionButton_Click);
            // 
            // serverView
            // 
            this.serverView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serverView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader3,
            this.columnHeader4});
            this.serverView.ContextMenuStrip = this.serverViewMenu;
            this.serverView.FullRowSelect = true;
            this.serverView.Location = new System.Drawing.Point(3, 30);
            this.serverView.MultiSelect = false;
            this.serverView.Name = "serverView";
            this.serverView.Size = new System.Drawing.Size(565, 163);
            this.serverView.TabIndex = 1;
            this.serverView.UseCompatibleStateImageBehavior = false;
            this.serverView.View = System.Windows.Forms.View.Details;
            this.serverView.DoubleClick += new System.EventHandler(this.serverView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Server Label";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Location";
            this.columnHeader2.Width = 90;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "City";
            this.columnHeader5.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Address";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Ping";
            this.columnHeader4.Width = 80;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPage2.Controls.Add(this.infoGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(571, 196);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Info";
            // 
            // infoGrid
            // 
            this.infoGrid.BackColor = System.Drawing.Color.White;
            this.infoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoGrid.HelpBackColor = System.Drawing.Color.White;
            this.infoGrid.Location = new System.Drawing.Point(3, 3);
            this.infoGrid.Name = "infoGrid";
            this.infoGrid.Size = new System.Drawing.Size(565, 190);
            this.infoGrid.TabIndex = 0;
            this.infoGrid.ToolbarVisible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPage3.Controls.Add(this.consoleOutput);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(571, 196);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Advanced";
            // 
            // consoleOutput
            // 
            this.consoleOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleOutput.Location = new System.Drawing.Point(6, 6);
            this.consoleOutput.Multiline = true;
            this.consoleOutput.Name = "consoleOutput";
            this.consoleOutput.Password = false;
            this.consoleOutput.ReadOnly = true;
            this.consoleOutput.Size = new System.Drawing.Size(559, 184);
            this.consoleOutput.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPage4.Controls.Add(this.settingsGrid);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(571, 196);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Settings";
            // 
            // settingsGrid
            // 
            this.settingsGrid.BackColor = System.Drawing.Color.White;
            this.settingsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsGrid.HelpBackColor = System.Drawing.Color.White;
            this.settingsGrid.Location = new System.Drawing.Point(3, 3);
            this.settingsGrid.Name = "settingsGrid";
            this.settingsGrid.Size = new System.Drawing.Size(565, 190);
            this.settingsGrid.TabIndex = 1;
            this.settingsGrid.ToolbarVisible = false;
            this.settingsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.settingsGrid_PropertyValueChanged);
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(603, 301);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.header1);
            this.Controls.Add(this.tabs1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(619, 340);
            this.Name = "Controller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AtlirVPN - Connect Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Controller_FormClosing);
            this.Load += new System.EventHandler(this.Controller_Load);
            this.Shown += new System.EventHandler(this.Controller_Shown);
            this.ResizeEnd += new System.EventHandler(this.Controller_ResizeEnd);
            this.serverViewMenu.ResumeLayout(false);
            this.trayMenu.ResumeLayout(false);
            this.tabs1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Tabs tabs1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Header header1;
        private StatusBar statusBar;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView serverView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip serverViewMenu;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private TextArea consoleOutput;
        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid infoGrid;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Button functionButton;
        private System.Windows.Forms.PropertyGrid settingsGrid;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem downloadConfigToolStripMenuItem;
        private TextArea searchBox;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem1;

    }
}

