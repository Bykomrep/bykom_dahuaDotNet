namespace DaHuaNetSDKSample
{
    partial class frm_PlayBackByFileSet
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PlayBackByFileSet));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpTimeStart = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cmbRecordFileTypeSelect = new System.Windows.Forms.ComboBox();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbChannelSelect = new System.Windows.Forms.ComboBox();
            this.txtChannelID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDevName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lsvFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCancel = new System.Windows.Forms.Button();
            this.pictureBoxPlayback = new System.Windows.Forms.PictureBox();
            this.gpbPlayBackControl = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblInfoVideo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.lblCurrentFrame = new System.Windows.Forms.Label();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.trbPlayFrames = new System.Windows.Forms.TrackBar();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.btnSoundPlayBack = new System.Windows.Forms.Button();
            this.btnStepPlayE = new System.Windows.Forms.Button();
            this.btnSlow = new System.Windows.Forms.Button();
            this.btnFast = new System.Windows.Forms.Button();
            this.btnSetpPlayS = new System.Windows.Forms.Button();
            this.btnStop2 = new System.Windows.Forms.Button();
            this.btnPlay2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timerGetPlayInfo = new System.Windows.Forms.Timer(this.components);
            this.lblInfoSearch = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblfechaaux = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayback)).BeginInit();
            this.gpbPlayBackControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbPlayFrames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpTimeEnd);
            this.groupBox1.Controls.Add(this.dtpTimeStart);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.cmbRecordFileTypeSelect);
            this.groupBox1.Controls.Add(this.dtpEnd);
            this.groupBox1.Controls.Add(this.dtpStart);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbChannelSelect);
            this.groupBox1.Controls.Add(this.txtChannelID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDevName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(804, 90);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // dtpTimeEnd
            // 
            this.dtpTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeEnd.Location = new System.Drawing.Point(600, 52);
            this.dtpTimeEnd.Name = "dtpTimeEnd";
            this.dtpTimeEnd.Size = new System.Drawing.Size(80, 20);
            this.dtpTimeEnd.TabIndex = 27;
            this.dtpTimeEnd.Value = new System.DateTime(2015, 7, 4, 23, 59, 0, 0);
            // 
            // dtpTimeStart
            // 
            this.dtpTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeStart.Location = new System.Drawing.Point(362, 52);
            this.dtpTimeStart.Name = "dtpTimeStart";
            this.dtpTimeStart.Size = new System.Drawing.Size(80, 20);
            this.dtpTimeStart.TabIndex = 26;
            this.dtpTimeStart.Value = new System.DateTime(2015, 7, 6, 0, 0, 0, 0);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(690, 45);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(104, 28);
            this.btnQuery.TabIndex = 25;
            this.btnQuery.Text = "Buscar";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cmbRecordFileTypeSelect
            // 
            this.cmbRecordFileTypeSelect.FormattingEnabled = true;
            this.cmbRecordFileTypeSelect.Items.AddRange(new object[] {
            "Todos los videos",
            "Alarma externa",
            "Detección de movimiento",
            "Todas las alarmas",
            "Petición de tarjeta",
            "Combinación de condiciones"});
            this.cmbRecordFileTypeSelect.Location = new System.Drawing.Point(49, 51);
            this.cmbRecordFileTypeSelect.Name = "cmbRecordFileTypeSelect";
            this.cmbRecordFileTypeSelect.Size = new System.Drawing.Size(148, 21);
            this.cmbRecordFileTypeSelect.TabIndex = 23;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(490, 52);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(108, 20);
            this.dtpEnd.TabIndex = 20;
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(251, 52);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(108, 20);
            this.dtpStart.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(448, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "Hasta:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(210, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Desde:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbChannelSelect
            // 
            this.cmbChannelSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChannelSelect.FormattingEnabled = true;
            this.cmbChannelSelect.Location = new System.Drawing.Point(268, 22);
            this.cmbChannelSelect.Name = "cmbChannelSelect";
            this.cmbChannelSelect.Size = new System.Drawing.Size(101, 21);
            this.cmbChannelSelect.TabIndex = 16;
            this.cmbChannelSelect.SelectedIndexChanged += new System.EventHandler(this.cmbChannelSelect_SelectedIndexChanged);
            // 
            // txtChannelID
            // 
            this.txtChannelID.Location = new System.Drawing.Point(375, 22);
            this.txtChannelID.Name = "txtChannelID";
            this.txtChannelID.Size = new System.Drawing.Size(45, 20);
            this.txtChannelID.TabIndex = 5;
            this.txtChannelID.Text = "0";
            this.txtChannelID.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(203, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Canal:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDevName
            // 
            this.txtDevName.Enabled = false;
            this.txtDevName.Location = new System.Drawing.Point(49, 23);
            this.txtDevName.Name = "txtDevName";
            this.txtDevName.Size = new System.Drawing.Size(123, 20);
            this.txtDevName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DVR";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "Tipo";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lsvFiles
            // 
            this.lsvFiles.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lsvFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lsvFiles.FullRowSelect = true;
            this.lsvFiles.GridLines = true;
            this.lsvFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvFiles.HideSelection = false;
            this.lsvFiles.HotTracking = true;
            this.lsvFiles.HoverSelection = true;
            this.lsvFiles.Location = new System.Drawing.Point(16, 14);
            this.lsvFiles.MultiSelect = false;
            this.lsvFiles.Name = "lsvFiles";
            this.lsvFiles.Size = new System.Drawing.Size(370, 450);
            this.lsvFiles.TabIndex = 26;
            this.lsvFiles.UseCompatibleStateImageBehavior = false;
            this.lsvFiles.View = System.Windows.Forms.View.Details;
            this.lsvFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lsvFiles_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IPDispositivo/Canal";
            this.columnHeader1.Width = 139;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Desde";
            this.columnHeader2.Width = 191;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Hasta";
            this.columnHeader3.Width = 209;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Tamaño";
            this.columnHeader4.Width = 132;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(699, 583);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 28);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cerrar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pictureBoxPlayback
            // 
            this.pictureBoxPlayback.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBoxPlayback.Location = new System.Drawing.Point(2, 3);
            this.pictureBoxPlayback.Name = "pictureBoxPlayback";
            this.pictureBoxPlayback.Size = new System.Drawing.Size(370, 370);
            this.pictureBoxPlayback.TabIndex = 27;
            this.pictureBoxPlayback.TabStop = false;
            // 
            // gpbPlayBackControl
            // 
            this.gpbPlayBackControl.BackColor = System.Drawing.Color.Transparent;
            this.gpbPlayBackControl.Controls.Add(this.label7);
            this.gpbPlayBackControl.Controls.Add(this.lblInfoVideo);
            this.gpbPlayBackControl.Controls.Add(this.label6);
            this.gpbPlayBackControl.Controls.Add(this.lblTotalTime);
            this.gpbPlayBackControl.Controls.Add(this.lblCurrentFrame);
            this.gpbPlayBackControl.Controls.Add(this.lblCurrentTime);
            this.gpbPlayBackControl.Controls.Add(this.trbPlayFrames);
            this.gpbPlayBackControl.Controls.Add(this.trackBarVolume);
            this.gpbPlayBackControl.Controls.Add(this.btnSoundPlayBack);
            this.gpbPlayBackControl.Controls.Add(this.btnStepPlayE);
            this.gpbPlayBackControl.Controls.Add(this.btnSlow);
            this.gpbPlayBackControl.Controls.Add(this.btnFast);
            this.gpbPlayBackControl.Controls.Add(this.btnSetpPlayS);
            this.gpbPlayBackControl.Controls.Add(this.btnStop2);
            this.gpbPlayBackControl.Controls.Add(this.btnPlay2);
            this.gpbPlayBackControl.Enabled = false;
            this.gpbPlayBackControl.Location = new System.Drawing.Point(17, 387);
            this.gpbPlayBackControl.Name = "gpbPlayBackControl";
            this.gpbPlayBackControl.Size = new System.Drawing.Size(371, 80);
            this.gpbPlayBackControl.TabIndex = 31;
            this.gpbPlayBackControl.TabStop = false;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 15);
            this.label7.TabIndex = 29;
            this.label7.Text = "f/s:";
            // 
            // lblInfoVideo
            // 
            this.lblInfoVideo.AutoSize = true;
            this.lblInfoVideo.Location = new System.Drawing.Point(238, 63);
            this.lblInfoVideo.Name = "lblInfoVideo";
            this.lblInfoVideo.Size = new System.Drawing.Size(104, 13);
            this.lblInfoVideo.TabIndex = 29;
            this.lblInfoVideo.Text = "hh:mm:ss - hh:mm:ss";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(171, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 15);
            this.label6.TabIndex = 28;
            this.label6.Text = "/";
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblTotalTime.Location = new System.Drawing.Point(183, 63);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(49, 13);
            this.lblTotalTime.TabIndex = 22;
            this.lblTotalTime.Text = "00:00:00";
            // 
            // lblCurrentFrame
            // 
            this.lblCurrentFrame.AutoSize = true;
            this.lblCurrentFrame.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCurrentFrame.Location = new System.Drawing.Point(31, 62);
            this.lblCurrentFrame.Name = "lblCurrentFrame";
            this.lblCurrentFrame.Size = new System.Drawing.Size(85, 13);
            this.lblCurrentFrame.TabIndex = 19;
            this.lblCurrentFrame.Text = "0000000000000";
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCurrentTime.Location = new System.Drawing.Point(126, 63);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(49, 13);
            this.lblCurrentTime.TabIndex = 21;
            this.lblCurrentTime.Text = "00:00:00";
            // 
            // trbPlayFrames
            // 
            this.trbPlayFrames.AutoSize = false;
            this.trbPlayFrames.BackColor = System.Drawing.SystemColors.Control;
            this.trbPlayFrames.Location = new System.Drawing.Point(3, 9);
            this.trbPlayFrames.Name = "trbPlayFrames";
            this.trbPlayFrames.Size = new System.Drawing.Size(364, 20);
            this.trbPlayFrames.TabIndex = 18;
            this.trbPlayFrames.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trbPlayFrames_MouseDown);
            this.trbPlayFrames.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbPlayFrames_MouseUp);
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.AutoSize = false;
            this.trackBarVolume.Enabled = false;
            this.trackBarVolume.Location = new System.Drawing.Point(320, 41);
            this.trackBarVolume.Maximum = 65535;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(50, 18);
            this.trackBarVolume.TabIndex = 17;
            this.trackBarVolume.Value = 30000;
            // 
            // btnSoundPlayBack
            // 
            this.btnSoundPlayBack.Image = global::bykomDahua.Properties.Resources.audioOff16x16;
            this.btnSoundPlayBack.Location = new System.Drawing.Point(280, 34);
            this.btnSoundPlayBack.Name = "btnSoundPlayBack";
            this.btnSoundPlayBack.Size = new System.Drawing.Size(40, 25);
            this.btnSoundPlayBack.TabIndex = 16;
            this.btnSoundPlayBack.Tag = "Off";
            this.btnSoundPlayBack.UseVisualStyleBackColor = true;
            this.btnSoundPlayBack.Click += new System.EventHandler(this.btnSoundPlayBack_Click);
            // 
            // btnStepPlayE
            // 
            this.btnStepPlayE.BackColor = System.Drawing.Color.Transparent;
            this.btnStepPlayE.Enabled = false;
            this.btnStepPlayE.Location = new System.Drawing.Point(185, 34);
            this.btnStepPlayE.Name = "btnStepPlayE";
            this.btnStepPlayE.Size = new System.Drawing.Size(40, 25);
            this.btnStepPlayE.TabIndex = 15;
            this.btnStepPlayE.Text = ">||";
            this.btnStepPlayE.UseVisualStyleBackColor = false;
            this.btnStepPlayE.Click += new System.EventHandler(this.btnStepPlayE_Click_1);
            // 
            // btnSlow
            // 
            this.btnSlow.Image = global::bykomDahua.Properties.Resources.slow16x16;
            this.btnSlow.Location = new System.Drawing.Point(95, 34);
            this.btnSlow.Name = "btnSlow";
            this.btnSlow.Size = new System.Drawing.Size(40, 25);
            this.btnSlow.TabIndex = 11;
            this.btnSlow.Tag = "<<";
            this.btnSlow.UseVisualStyleBackColor = true;
            this.btnSlow.Click += new System.EventHandler(this.btnSlow_Click);
            // 
            // btnFast
            // 
            this.btnFast.Image = global::bykomDahua.Properties.Resources.fast16x16;
            this.btnFast.Location = new System.Drawing.Point(135, 34);
            this.btnFast.Name = "btnFast";
            this.btnFast.Size = new System.Drawing.Size(40, 25);
            this.btnFast.TabIndex = 10;
            this.btnFast.Tag = ">>";
            this.btnFast.UseVisualStyleBackColor = true;
            this.btnFast.Click += new System.EventHandler(this.btnFast_Click);
            // 
            // btnSetpPlayS
            // 
            this.btnSetpPlayS.BackColor = System.Drawing.Color.Transparent;
            this.btnSetpPlayS.Image = global::bykomDahua.Properties.Resources.step16x16;
            this.btnSetpPlayS.Location = new System.Drawing.Point(225, 34);
            this.btnSetpPlayS.Name = "btnSetpPlayS";
            this.btnSetpPlayS.Size = new System.Drawing.Size(40, 25);
            this.btnSetpPlayS.TabIndex = 9;
            this.btnSetpPlayS.Tag = ">|";
            this.btnSetpPlayS.UseVisualStyleBackColor = false;
            this.btnSetpPlayS.Click += new System.EventHandler(this.btnSetpPlayS_Click);
            // 
            // btnStop2
            // 
            this.btnStop2.Image = global::bykomDahua.Properties.Resources.Stop16x16;
            this.btnStop2.Location = new System.Drawing.Point(46, 34);
            this.btnStop2.Name = "btnStop2";
            this.btnStop2.Size = new System.Drawing.Size(40, 25);
            this.btnStop2.TabIndex = 8;
            this.btnStop2.UseVisualStyleBackColor = true;
            this.btnStop2.Click += new System.EventHandler(this.btnStop2_Click);
            // 
            // btnPlay2
            // 
            this.btnPlay2.Image = global::bykomDahua.Properties.Resources.play16x16;
            this.btnPlay2.Location = new System.Drawing.Point(6, 34);
            this.btnPlay2.Name = "btnPlay2";
            this.btnPlay2.Size = new System.Drawing.Size(40, 25);
            this.btnPlay2.TabIndex = 7;
            this.btnPlay2.Tag = ">";
            this.btnPlay2.UseVisualStyleBackColor = true;
            this.btnPlay2.Click += new System.EventHandler(this.btnPlay2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lsvFiles);
            this.groupBox2.Location = new System.Drawing.Point(12, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 474);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.gpbPlayBackControl);
            this.groupBox3.Location = new System.Drawing.Point(416, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 473);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel1.Controls.Add(this.pictureBoxPlayback);
            this.panel1.Location = new System.Drawing.Point(15, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(375, 375);
            this.panel1.TabIndex = 32;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(297, 580);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(115, 13);
            this.progressBar1.TabIndex = 27;
            this.progressBar1.Visible = false;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // timerGetPlayInfo
            // 
            this.timerGetPlayInfo.Interval = 1;
            this.timerGetPlayInfo.Tick += new System.EventHandler(this.timerGetPlayInfo_Tick);
            // 
            // lblInfoSearch
            // 
            this.lblInfoSearch.AutoSize = true;
            this.lblInfoSearch.Location = new System.Drawing.Point(229, 580);
            this.lblInfoSearch.Name = "lblInfoSearch";
            this.lblInfoSearch.Size = new System.Drawing.Size(64, 13);
            this.lblInfoSearch.TabIndex = 28;
            this.lblInfoSearch.Text = "Buscando...";
            this.lblInfoSearch.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "label8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "label9";
            // 
            // lblfechaaux
            // 
            this.lblfechaaux.AutoSize = true;
            this.lblfechaaux.Location = new System.Drawing.Point(559, 580);
            this.lblfechaaux.Name = "lblfechaaux";
            this.lblfechaaux.Size = new System.Drawing.Size(61, 13);
            this.lblfechaaux.TabIndex = 37;
            this.lblfechaaux.Text = "lblfechaaux";
            this.lblfechaaux.Visible = false;
            // 
            // frm_PlayBackByFileSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 623);
            this.Controls.Add(this.lblfechaaux);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblInfoSearch);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_PlayBackByFileSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Grabaciones";
            this.Load += new System.EventHandler(this.frm_PlayBackByFileSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayback)).EndInit();
            this.gpbPlayBackControl.ResumeLayout(false);
            this.gpbPlayBackControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbPlayFrames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.TextBox txtChannelID;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtDevName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dtpEnd;
        public System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbRecordFileTypeSelect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ListView lsvFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        public System.Windows.Forms.ComboBox cmbChannelSelect;
        private System.Windows.Forms.PictureBox pictureBoxPlayback;
        private System.Windows.Forms.GroupBox gpbPlayBackControl;
        private System.Windows.Forms.Button btnStepPlayE;
        private System.Windows.Forms.Button btnSlow;
        private System.Windows.Forms.Button btnFast;
        private System.Windows.Forms.Button btnSetpPlayS;
        private System.Windows.Forms.Button btnStop2;
        private System.Windows.Forms.Button btnPlay2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpTimeEnd;
        private System.Windows.Forms.DateTimePicker dtpTimeStart;
        private System.Windows.Forms.Button btnSoundPlayBack;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.TrackBar trbPlayFrames;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Label lblCurrentFrame;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timerGetPlayInfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblInfoSearch;
        private System.Windows.Forms.Label lblInfoVideo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblfechaaux;
    }
}