namespace SDKCapturePicture
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelIP = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Logout = new System.Windows.Forms.Button();
            this.Login = new System.Windows.Forms.Button();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxPsw = new System.Windows.Forms.TextBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.labelPsw = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBoxSnap = new System.Windows.Forms.GroupBox();
            this.buttonSet = new System.Windows.Forms.Button();
            this.buttonGet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSnapSpace = new System.Windows.Forms.Label();
            this.comboBoxQuality = new System.Windows.Forms.ComboBox();
            this.comboBoxSnapSpace = new System.Windows.Forms.ComboBox();
            this.comboBoxRe = new System.Windows.Forms.ComboBox();
            this.comboBoxSnapMode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSnapMode = new System.Windows.Forms.Label();
            this.textBoxSerial = new System.Windows.Forms.TextBox();
            this.comboBoxChannel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelChannel = new System.Windows.Forms.Label();
            this.radioButtonMore = new System.Windows.Forms.RadioButton();
            this.radioButtonOne = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxSnap.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(17, 26);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(29, 12);
            this.labelIP.TabIndex = 0;
            this.labelIP.Text = "IP：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Logout);
            this.groupBox1.Controls.Add(this.Login);
            this.groupBox1.Controls.Add(this.textBoxPort);
            this.groupBox1.Controls.Add(this.textBoxPsw);
            this.groupBox1.Controls.Add(this.textBoxUser);
            this.groupBox1.Controls.Add(this.textBoxIP);
            this.groupBox1.Controls.Add(this.labelPsw);
            this.groupBox1.Controls.Add(this.labelUser);
            this.groupBox1.Controls.Add(this.labelPort);
            this.groupBox1.Controls.Add(this.labelIP);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 97);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登陆";
            // 
            // Logout
            // 
            this.Logout.Location = new System.Drawing.Point(398, 56);
            this.Logout.Name = "Logout";
            this.Logout.Size = new System.Drawing.Size(75, 23);
            this.Logout.TabIndex = 2;
            this.Logout.Text = "登出";
            this.Logout.UseVisualStyleBackColor = true;
            this.Logout.Click += new System.EventHandler(this.Logout_Click);
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(398, 17);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(75, 23);
            this.Login.TabIndex = 2;
            this.Login.Text = "登入";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(258, 20);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 21);
            this.textBoxPort.TabIndex = 1;
            // 
            // textBoxPsw
            // 
            this.textBoxPsw.Location = new System.Drawing.Point(258, 58);
            this.textBoxPsw.Name = "textBoxPsw";
            this.textBoxPsw.PasswordChar = '*';
            this.textBoxPsw.Size = new System.Drawing.Size(100, 21);
            this.textBoxPsw.TabIndex = 1;
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(76, 58);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(115, 21);
            this.textBoxUser.TabIndex = 1;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(76, 20);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(115, 21);
            this.textBoxIP.TabIndex = 1;
            // 
            // labelPsw
            // 
            this.labelPsw.AutoSize = true;
            this.labelPsw.Location = new System.Drawing.Point(211, 61);
            this.labelPsw.Name = "labelPsw";
            this.labelPsw.Size = new System.Drawing.Size(41, 12);
            this.labelPsw.TabIndex = 0;
            this.labelPsw.Text = "密码：";
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(17, 61);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(53, 12);
            this.labelUser.TabIndex = 0;
            this.labelUser.Text = "用户名：";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(211, 26);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(41, 12);
            this.labelPort.TabIndex = 0;
            this.labelPort.Text = "端口：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBoxSnap);
            this.groupBox2.Controls.Add(this.textBoxSerial);
            this.groupBox2.Controls.Add(this.comboBoxChannel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.labelChannel);
            this.groupBox2.Location = new System.Drawing.Point(13, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 255);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "远程抓图";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonOne);
            this.groupBox3.Controls.Add(this.radioButtonMore);
            this.groupBox3.Controls.Add(this.buttonStop);
            this.groupBox3.Controls.Add(this.buttonStart);
            this.groupBox3.Location = new System.Drawing.Point(19, 181);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(465, 64);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "抓图";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(356, 24);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(64, 23);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(259, 24);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(64, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "开始";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // groupBoxSnap
            // 
            this.groupBoxSnap.Controls.Add(this.buttonSet);
            this.groupBoxSnap.Controls.Add(this.buttonGet);
            this.groupBoxSnap.Controls.Add(this.label3);
            this.groupBoxSnap.Controls.Add(this.labelSnapSpace);
            this.groupBoxSnap.Controls.Add(this.comboBoxQuality);
            this.groupBoxSnap.Controls.Add(this.comboBoxSnapSpace);
            this.groupBoxSnap.Controls.Add(this.comboBoxRe);
            this.groupBoxSnap.Controls.Add(this.comboBoxSnapMode);
            this.groupBoxSnap.Controls.Add(this.label2);
            this.groupBoxSnap.Controls.Add(this.labelSnapMode);
            this.groupBoxSnap.Location = new System.Drawing.Point(18, 58);
            this.groupBoxSnap.Name = "groupBoxSnap";
            this.groupBoxSnap.Size = new System.Drawing.Size(467, 110);
            this.groupBoxSnap.TabIndex = 3;
            this.groupBoxSnap.TabStop = false;
            this.groupBoxSnap.Text = "抓图配置";
            // 
            // buttonSet
            // 
            this.buttonSet.Location = new System.Drawing.Point(264, 81);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(75, 24);
            this.buttonSet.TabIndex = 4;
            this.buttonSet.Text = "设置";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // buttonGet
            // 
            this.buttonGet.Location = new System.Drawing.Point(102, 81);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(75, 24);
            this.buttonGet.TabIndex = 4;
            this.buttonGet.Text = "获取";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "画质：";
            // 
            // labelSnapSpace
            // 
            this.labelSnapSpace.AutoSize = true;
            this.labelSnapSpace.Location = new System.Drawing.Point(262, 18);
            this.labelSnapSpace.Name = "labelSnapSpace";
            this.labelSnapSpace.Size = new System.Drawing.Size(65, 12);
            this.labelSnapSpace.TabIndex = 3;
            this.labelSnapSpace.Text = "抓图间隔：";
            // 
            // comboBoxQuality
            // 
            this.comboBoxQuality.FormattingEnabled = true;
            this.comboBoxQuality.Location = new System.Drawing.Point(333, 49);
            this.comboBoxQuality.Name = "comboBoxQuality";
            this.comboBoxQuality.Size = new System.Drawing.Size(103, 20);
            this.comboBoxQuality.TabIndex = 1;
            this.comboBoxQuality.SelectedIndexChanged += new System.EventHandler(this.OnSelchangeComboSnapQuality);
            // 
            // comboBoxSnapSpace
            // 
            this.comboBoxSnapSpace.FormattingEnabled = true;
            this.comboBoxSnapSpace.Location = new System.Drawing.Point(333, 16);
            this.comboBoxSnapSpace.Name = "comboBoxSnapSpace";
            this.comboBoxSnapSpace.Size = new System.Drawing.Size(103, 20);
            this.comboBoxSnapSpace.TabIndex = 1;
            this.comboBoxSnapSpace.SelectedIndexChanged += new System.EventHandler(this.OnSelchangeComboSnapSpace);
            // 
            // comboBoxRe
            // 
            this.comboBoxRe.FormattingEnabled = true;
            this.comboBoxRe.Location = new System.Drawing.Point(93, 49);
            this.comboBoxRe.Name = "comboBoxRe";
            this.comboBoxRe.Size = new System.Drawing.Size(112, 20);
            this.comboBoxRe.TabIndex = 1;
            this.comboBoxRe.SelectedIndexChanged += new System.EventHandler(this.OnSelchangeComboSnapSize);
            // 
            // comboBoxSnapMode
            // 
            this.comboBoxSnapMode.FormattingEnabled = true;
            this.comboBoxSnapMode.Location = new System.Drawing.Point(93, 16);
            this.comboBoxSnapMode.Name = "comboBoxSnapMode";
            this.comboBoxSnapMode.Size = new System.Drawing.Size(112, 20);
            this.comboBoxSnapMode.TabIndex = 1;
            this.comboBoxSnapMode.SelectedIndexChanged += new System.EventHandler(this.OnSelchangeComboSnapMode);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "分辨率：";
            // 
            // labelSnapMode
            // 
            this.labelSnapMode.AutoSize = true;
            this.labelSnapMode.Location = new System.Drawing.Point(16, 20);
            this.labelSnapMode.Name = "labelSnapMode";
            this.labelSnapMode.Size = new System.Drawing.Size(65, 12);
            this.labelSnapMode.TabIndex = 0;
            this.labelSnapMode.Text = "抓图模式：";
            // 
            // textBoxSerial
            // 
            this.textBoxSerial.Location = new System.Drawing.Point(281, 17);
            this.textBoxSerial.Name = "textBoxSerial";
            this.textBoxSerial.Size = new System.Drawing.Size(112, 21);
            this.textBoxSerial.TabIndex = 2;
            // 
            // comboBoxChannel
            // 
            this.comboBoxChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChannel.FormattingEnabled = true;
            this.comboBoxChannel.Location = new System.Drawing.Point(74, 17);
            this.comboBoxChannel.Name = "comboBoxChannel";
            this.comboBoxChannel.Size = new System.Drawing.Size(121, 20);
            this.comboBoxChannel.TabIndex = 1;
            this.comboBoxChannel.SelectedIndexChanged += new System.EventHandler(this.OnSelchangeComboChannel);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请求序列号：";
            // 
            // labelChannel
            // 
            this.labelChannel.AutoSize = true;
            this.labelChannel.Location = new System.Drawing.Point(15, 23);
            this.labelChannel.Name = "labelChannel";
            this.labelChannel.Size = new System.Drawing.Size(41, 12);
            this.labelChannel.TabIndex = 0;
            this.labelChannel.Text = "通道：";
            // 
            // radioButtonMore
            // 
            this.radioButtonMore.AutoSize = true;
            this.radioButtonMore.Location = new System.Drawing.Point(141, 27);
            this.radioButtonMore.Name = "radioButtonMore";
            this.radioButtonMore.Size = new System.Drawing.Size(59, 16);
            this.radioButtonMore.TabIndex = 2;
            this.radioButtonMore.TabStop = true;
            this.radioButtonMore.Text = "定时抓";
            this.radioButtonMore.UseVisualStyleBackColor = true;
            // 
            // radioButtonOne
            // 
            this.radioButtonOne.AutoSize = true;
            this.radioButtonOne.Location = new System.Drawing.Point(35, 27);
            this.radioButtonOne.Name = "radioButtonOne";
            this.radioButtonOne.Size = new System.Drawing.Size(59, 16);
            this.radioButtonOne.TabIndex = 2;
            this.radioButtonOne.TabStop = true;
            this.radioButtonOne.Text = "抓一幅";
            this.radioButtonOne.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 382);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "远程抓图";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxSnap.ResumeLayout(false);
            this.groupBoxSnap.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelPsw;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxPsw;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Button Logout;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelChannel;
        private System.Windows.Forms.ComboBox comboBoxChannel;
        private System.Windows.Forms.TextBox textBoxSerial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxSnap;
        private System.Windows.Forms.ComboBox comboBoxSnapMode;
        private System.Windows.Forms.Label labelSnapMode;
        private System.Windows.Forms.Label labelSnapSpace;
        private System.Windows.Forms.ComboBox comboBoxSnapSpace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxQuality;
        private System.Windows.Forms.ComboBox comboBoxRe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.Button buttonGet;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.RadioButton radioButtonOne;
        private System.Windows.Forms.RadioButton radioButtonMore;
    }
}

