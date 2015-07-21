namespace SDKDownLoadFileDemo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsvFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cmbRecordFileTypeSelect = new System.Windows.Forms.ComboBox();
            this.txtTimeEnd = new System.Windows.Forms.TextBox();
            this.txtTimeStart = new System.Windows.Forms.TextBox();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbChannelSelect = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtChannelID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDevName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsvFiles);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.cmbRecordFileTypeSelect);
            this.groupBox1.Controls.Add(this.txtTimeEnd);
            this.groupBox1.Controls.Add(this.txtTimeStart);
            this.groupBox1.Controls.Add(this.dtpEnd);
            this.groupBox1.Controls.Add(this.dtpStart);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbChannelSelect);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.txtChannelID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDevName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(684, 548);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
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
            this.lsvFiles.Location = new System.Drawing.Point(24, 84);
            this.lsvFiles.MultiSelect = false;
            this.lsvFiles.Name = "lsvFiles";
            this.lsvFiles.Size = new System.Drawing.Size(633, 412);
            this.lsvFiles.TabIndex = 26;
            this.lsvFiles.UseCompatibleStateImageBehavior = false;
            this.lsvFiles.View = System.Windows.Forms.View.Details;
            this.lsvFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lsvFiles_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "设备/通道";
            this.columnHeader1.Width = 163;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "开始时间";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "结束时间";
            this.columnHeader3.Width = 167;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "大小";
            this.columnHeader4.Width = 145;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(543, 52);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(114, 26);
            this.btnQuery.TabIndex = 25;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cmbRecordFileTypeSelect
            // 
            this.cmbRecordFileTypeSelect.FormattingEnabled = true;
            this.cmbRecordFileTypeSelect.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.cmbRecordFileTypeSelect.Items.AddRange(new object[] {
            "0:所有录像文件  ",
            "1:外部报警    ",
            "2:动态检测报警    ",
            "3:所有报警    ",
            "4:卡号查询    ",
            "5:组合条件查询"});
            this.cmbRecordFileTypeSelect.Location = new System.Drawing.Point(543, 21);
            this.cmbRecordFileTypeSelect.Name = "cmbRecordFileTypeSelect";
            this.cmbRecordFileTypeSelect.Size = new System.Drawing.Size(114, 20);
            this.cmbRecordFileTypeSelect.TabIndex = 23;
            // 
            // txtTimeEnd
            // 
            this.txtTimeEnd.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtTimeEnd.Location = new System.Drawing.Point(402, 48);
            this.txtTimeEnd.Name = "txtTimeEnd";
            this.txtTimeEnd.Size = new System.Drawing.Size(56, 21);
            this.txtTimeEnd.TabIndex = 22;
            this.txtTimeEnd.Text = "12:01:01";
            // 
            // txtTimeStart
            // 
            this.txtTimeStart.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtTimeStart.Location = new System.Drawing.Point(402, 22);
            this.txtTimeStart.Name = "txtTimeStart";
            this.txtTimeStart.Size = new System.Drawing.Size(56, 21);
            this.txtTimeStart.TabIndex = 21;
            this.txtTimeStart.Text = "12:01:01";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(292, 48);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(108, 21);
            this.dtpEnd.TabIndex = 20;
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(292, 21);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(108, 21);
            this.dtpStart.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(227, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "结束时间:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(216, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "开始时间:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbChannelSelect
            // 
            this.cmbChannelSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChannelSelect.FormattingEnabled = true;
            this.cmbChannelSelect.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.cmbChannelSelect.Location = new System.Drawing.Point(87, 48);
            this.cmbChannelSelect.Name = "cmbChannelSelect";
            this.cmbChannelSelect.Size = new System.Drawing.Size(101, 20);
            this.cmbChannelSelect.TabIndex = 16;
            this.cmbChannelSelect.SelectedIndexChanged += new System.EventHandler(this.cmbChannelSelect_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(587, 502);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 26);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(511, 502);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 26);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtChannelID
            // 
            this.txtChannelID.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtChannelID.Location = new System.Drawing.Point(194, 48);
            this.txtChannelID.Name = "txtChannelID";
            this.txtChannelID.Size = new System.Drawing.Size(45, 21);
            this.txtChannelID.TabIndex = 5;
            this.txtChannelID.Text = "0";
            this.txtChannelID.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "通道ID:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDevName
            // 
            this.txtDevName.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtDevName.Location = new System.Drawing.Point(87, 21);
            this.txtDevName.Name = "txtDevName";
            this.txtDevName.Size = new System.Drawing.Size(123, 21);
            this.txtDevName.TabIndex = 1;
            this.txtDevName.Text = "test";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备名:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(472, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "文件类型:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frm_PlayBackByFileSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 575);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_PlayBackByFileSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "按文件回放下载";
            this.Load += new System.EventHandler(this.frm_PlayBackByFileSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.TextBox txtChannelID;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtDevName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtTimeEnd;
        public System.Windows.Forms.TextBox txtTimeStart;
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
    }
}