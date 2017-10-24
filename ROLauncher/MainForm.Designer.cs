namespace ROLauncher
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.promptLabel = new System.Windows.Forms.Label();
            this.userLoginTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.forgotPasswordLinkLabel = new System.Windows.Forms.LinkLabel();
            this.logInButton = new System.Windows.Forms.Button();
            this.rememberMeCheckBox = new System.Windows.Forms.CheckBox();
            this.myComLogoPictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusBarLabel = new System.Windows.Forms.Label();
            this.updateAvailableLinkLabel = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.playButton = new System.Windows.Forms.Button();
            this.autoStartCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.myComLogoPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // promptLabel
            // 
            this.promptLabel.AutoSize = true;
            this.promptLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.promptLabel.Location = new System.Drawing.Point(33, 12);
            this.promptLabel.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.promptLabel.Name = "promptLabel";
            this.promptLabel.Size = new System.Drawing.Size(261, 13);
            this.promptLabel.TabIndex = 0;
            this.promptLabel.Text = "Please enter your                    login and password.";
            this.promptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // userLoginTextBox
            // 
            this.userLoginTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userLoginTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLoginTextBox.Location = new System.Drawing.Point(30, 40);
            this.userLoginTextBox.Name = "userLoginTextBox";
            this.userLoginTextBox.Size = new System.Drawing.Size(324, 29);
            this.userLoginTextBox.TabIndex = 1;
            this.userLoginTextBox.TextChanged += new System.EventHandler(this.loginTextBox_TextChanged);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordTextBox.Location = new System.Drawing.Point(30, 75);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '●';
            this.passwordTextBox.Size = new System.Drawing.Size(324, 29);
            this.passwordTextBox.TabIndex = 2;
            this.passwordTextBox.TextChanged += new System.EventHandler(this.passwordTextBox_TextChanged);
            // 
            // forgotPasswordLinkLabel
            // 
            this.forgotPasswordLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.forgotPasswordLinkLabel.AutoSize = true;
            this.forgotPasswordLinkLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.forgotPasswordLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.forgotPasswordLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.forgotPasswordLinkLabel.Location = new System.Drawing.Point(294, 12);
            this.forgotPasswordLinkLabel.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.forgotPasswordLinkLabel.Name = "forgotPasswordLinkLabel";
            this.forgotPasswordLinkLabel.Size = new System.Drawing.Size(57, 13);
            this.forgotPasswordLinkLabel.TabIndex = 6;
            this.forgotPasswordLinkLabel.TabStop = true;
            this.forgotPasswordLinkLabel.Text = "Forgot it?";
            this.forgotPasswordLinkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.forgotPasswordLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forgotPasswordLinkLabel_LinkClicked);
            // 
            // logInButton
            // 
            this.logInButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logInButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.logInButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(163)))), ((int)(((byte)(240)))));
            this.logInButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.logInButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(186)))), ((int)(((byte)(244)))));
            this.logInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logInButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.logInButton.ForeColor = System.Drawing.Color.White;
            this.logInButton.Location = new System.Drawing.Point(176, 110);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(86, 34);
            this.logInButton.TabIndex = 4;
            this.logInButton.Text = "LOG IN";
            this.logInButton.UseVisualStyleBackColor = false;
            this.logInButton.Click += new System.EventHandler(this.logInButton_ClickAsync);
            // 
            // rememberMeCheckBox
            // 
            this.rememberMeCheckBox.AutoSize = true;
            this.rememberMeCheckBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.rememberMeCheckBox.Location = new System.Drawing.Point(30, 110);
            this.rememberMeCheckBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.rememberMeCheckBox.Name = "rememberMeCheckBox";
            this.rememberMeCheckBox.Size = new System.Drawing.Size(122, 17);
            this.rememberMeCheckBox.TabIndex = 2;
            this.rememberMeCheckBox.Text = "Keep me logged in";
            this.rememberMeCheckBox.UseVisualStyleBackColor = true;
            this.rememberMeCheckBox.CheckedChanged += new System.EventHandler(this.rememberMeCheckBox_CheckedChanged);
            // 
            // myComLogoPictureBox
            // 
            this.myComLogoPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.myComLogoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("myComLogoPictureBox.Image")));
            this.myComLogoPictureBox.Location = new System.Drawing.Point(128, 14);
            this.myComLogoPictureBox.Name = "myComLogoPictureBox";
            this.myComLogoPictureBox.Size = new System.Drawing.Size(55, 20);
            this.myComLogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.myComLogoPictureBox.TabIndex = 9;
            this.myComLogoPictureBox.TabStop = false;
            this.myComLogoPictureBox.Click += new System.EventHandler(this.myComLogoPictureBox_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.statusBarLabel);
            this.panel1.Controls.Add(this.updateAvailableLinkLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 151);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 26);
            this.panel1.TabIndex = 6;
            // 
            // statusBarLabel
            // 
            this.statusBarLabel.AutoSize = true;
            this.statusBarLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.statusBarLabel.Location = new System.Drawing.Point(3, 5);
            this.statusBarLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.statusBarLabel.Name = "statusBarLabel";
            this.statusBarLabel.Size = new System.Drawing.Size(0, 13);
            this.statusBarLabel.TabIndex = 0;
            // 
            // updateAvailableLinkLabel
            // 
            this.updateAvailableLinkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.updateAvailableLinkLabel.AutoSize = true;
            this.updateAvailableLinkLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.updateAvailableLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.updateAvailableLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.updateAvailableLinkLabel.Location = new System.Drawing.Point(3, 5);
            this.updateAvailableLinkLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.updateAvailableLinkLabel.Name = "updateAvailableLinkLabel";
            this.updateAvailableLinkLabel.Size = new System.Drawing.Size(309, 13);
            this.updateAvailableLinkLabel.TabIndex = 0;
            this.updateAvailableLinkLabel.TabStop = true;
            this.updateAvailableLinkLabel.Text = "Update available! Click here to open My.com Game Center";
            this.updateAvailableLinkLabel.Visible = false;
            this.updateAvailableLinkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.updateAvailableLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked_1);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 150);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(384, 1);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 149);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(384, 1);
            this.panel3.TabIndex = 13;
            // 
            // playButton
            // 
            this.playButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.playButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(165)))), ((int)(((byte)(90)))));
            this.playButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(120)))), ((int)(((byte)(54)))));
            this.playButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(165)))), ((int)(((byte)(90)))));
            this.playButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(183)))), ((int)(((byte)(123)))));
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.playButton.ForeColor = System.Drawing.Color.White;
            this.playButton.Location = new System.Drawing.Point(268, 110);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(86, 34);
            this.playButton.TabIndex = 5;
            this.playButton.Text = "PLAY";
            this.playButton.UseVisualStyleBackColor = false;
            this.playButton.Click += new System.EventHandler(this.playButton_ClickAsync);
            // 
            // autoStartCheckBox
            // 
            this.autoStartCheckBox.AutoSize = true;
            this.autoStartCheckBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.autoStartCheckBox.Location = new System.Drawing.Point(30, 127);
            this.autoStartCheckBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.autoStartCheckBox.Name = "autoStartCheckBox";
            this.autoStartCheckBox.Size = new System.Drawing.Size(151, 17);
            this.autoStartCheckBox.TabIndex = 3;
            this.autoStartCheckBox.Text = "Auto-start ({0} sec. delay)";
            this.autoStartCheckBox.UseVisualStyleBackColor = true;
            this.autoStartCheckBox.CheckedChanged += new System.EventHandler(this.autoStartCheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AcceptButton = this.logInButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 177);
            this.Controls.Add(this.logInButton);
            this.Controls.Add(this.autoStartCheckBox);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.forgotPasswordLinkLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rememberMeCheckBox);
            this.Controls.Add(this.userLoginTextBox);
            this.Controls.Add(this.myComLogoPictureBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.promptLabel);
            this.Controls.Add(this.playButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Revelation Online Launcher";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_ShownAsync);
            ((System.ComponentModel.ISupportInitialize)(this.myComLogoPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label promptLabel;
        private System.Windows.Forms.TextBox userLoginTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.LinkLabel forgotPasswordLinkLabel;
        private System.Windows.Forms.Button logInButton;
        private System.Windows.Forms.CheckBox rememberMeCheckBox;
        private System.Windows.Forms.PictureBox myComLogoPictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label statusBarLabel;
        private System.Windows.Forms.LinkLabel updateAvailableLinkLabel;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.CheckBox autoStartCheckBox;
    }
}

