using System;
using System.Windows.Forms;
using AtlirVPNConnect.Util;

namespace AtlirVPNConnect
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        private void DoLogin()
        {
            statusBar.Text = "Working...";
            button1.Enabled = !button1.Enabled;
            Atlir.Api.Login(username.Text, password.Text, (success, value) => Invoke((MethodInvoker)delegate
            {
                statusBar.Text = (string)value;
                if (success)
                {
                    InternalData.SettingsGrid.Username = username.Text;
                    InternalData.SettingsGrid.Password = password.Text;
                    InternalData.SaveSettings();
                    new Controller().Show();
                    Visible = false;
                }
                else
                {
                    if (Program.Arguments.Contains("-silent"))
                        Close();
                    button1.Enabled = !button1.Enabled;
                }
            }));
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                DoLogin();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (!Program.Arguments.Contains("-silent")) return;
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(InternalData.SettingsGrid.Username))
            {
                username.Text = InternalData.SettingsGrid.Username;
                password.Text = InternalData.SettingsGrid.Password;
                //DoLogin();
            }
            else
            {
                if (Program.Arguments.Contains("-silent"))
                    Close();
            }
        }
    }
}
