namespace AtlirVPNConnect
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.statusBar = new StatusBar();
            this.button1 = new Button();
            this.password = new TextArea();
            this.username = new TextArea();
            this.header1 = new Header();
            this.labeler2 = new Labeler();
            this.labeler1 = new Labeler();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar.Font = new System.Drawing.Font("Source Sans Pro", 9F);
            this.statusBar.Location = new System.Drawing.Point(0, 195);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(284, 24);
            this.statusBar.TabIndex = 6;
            this.statusBar.Tasks = null;
            this.statusBar.Text = "Please Login";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(176, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 25);
            this.button1.TabIndex = 5;
            this.button1.Text = "Login";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(24, 127);
            this.password.Multiline = false;
            this.password.Name = "password";
            this.password.Password = true;
            this.password.ReadOnly = false;
            this.password.Size = new System.Drawing.Size(232, 23);
            this.password.TabIndex = 2;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(24, 76);
            this.username.Multiline = false;
            this.username.Name = "username";
            this.username.Password = false;
            this.username.ReadOnly = false;
            this.username.Size = new System.Drawing.Size(232, 23);
            this.username.TabIndex = 1;
            // 
            // header1
            // 
            this.header1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Font = new System.Drawing.Font("Source Sans Pro", 12F);
            this.header1.Location = new System.Drawing.Point(0, 0);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(284, 40);
            this.header1.TabIndex = 0;
            this.header1.Text = "AtlirVPN - Login";
            // 
            // labeler2
            // 
            this.labeler2.Font = new System.Drawing.Font("Source Sans Pro", 9F);
            this.labeler2.Location = new System.Drawing.Point(24, 105);
            this.labeler2.Name = "labeler2";
            this.labeler2.Size = new System.Drawing.Size(60, 22);
            this.labeler2.TabIndex = 4;
            this.labeler2.Text = "Password";
            // 
            // labeler1
            // 
            this.labeler1.Font = new System.Drawing.Font("Source Sans Pro", 9F);
            this.labeler1.Location = new System.Drawing.Point(24, 54);
            this.labeler1.Name = "labeler1";
            this.labeler1.Size = new System.Drawing.Size(63, 22);
            this.labeler1.TabIndex = 3;
            this.labeler1.Text = "Username";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 219);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labeler2);
            this.Controls.Add(this.labeler1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.header1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Shown += new System.EventHandler(this.Login_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Login_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Header header1;
        private TextArea username;
        private TextArea password;
        private Labeler labeler1;
        private Labeler labeler2;
        private Button button1;
        private StatusBar statusBar;
    }
}