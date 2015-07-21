namespace SDKPlayDemo
{
    partial class frmDataRecord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataRecord));
            this.btnShowOpenFileDlg = new System.Windows.Forms.Button();
            this.txtDirPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartRecord = new System.Windows.Forms.Button();
            this.btnStopRecord = new System.Windows.Forms.Button();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPlayChannelNO = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.fbdMain = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayChannelNO)).BeginInit();
            this.SuspendLayout();
            // 
            // btnShowOpenFileDlg
            // 
            this.btnShowOpenFileDlg.Location = new System.Drawing.Point(215, 50);
            this.btnShowOpenFileDlg.Name = "btnShowOpenFileDlg";
            this.btnShowOpenFileDlg.Size = new System.Drawing.Size(55, 31);
            this.btnShowOpenFileDlg.TabIndex = 0;
            this.btnShowOpenFileDlg.Text = "浏览";
            this.btnShowOpenFileDlg.UseVisualStyleBackColor = true;
            this.btnShowOpenFileDlg.Click += new System.EventHandler(this.btnShowOpenFileDlg_Click);
            // 
            // txtDirPath
            // 
            this.txtDirPath.Location = new System.Drawing.Point(14, 56);
            this.txtDirPath.Name = "txtDirPath";
            this.txtDirPath.Size = new System.Drawing.Size(195, 21);
            this.txtDirPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "数据流录像保存路径:";
            // 
            // btnStartRecord
            // 
            this.btnStartRecord.Location = new System.Drawing.Point(91, 140);
            this.btnStartRecord.Name = "btnStartRecord";
            this.btnStartRecord.Size = new System.Drawing.Size(78, 31);
            this.btnStartRecord.TabIndex = 3;
            this.btnStartRecord.Text = "开始保存";
            this.btnStartRecord.UseVisualStyleBackColor = true;
            this.btnStartRecord.Click += new System.EventHandler(this.btnStartRecord_Click);
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.Location = new System.Drawing.Point(221, 140);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(78, 31);
            this.btnStopRecord.TabIndex = 4;
            this.btnStopRecord.Text = "结束保存";
            this.btnStopRecord.UseVisualStyleBackColor = true;
            this.btnStopRecord.Click += new System.EventHandler(this.btnStopRecord_Click);
            // 
            // cmbDataType
            // 
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Items.AddRange(new object[] {
            "原码流格式",
            "AVI格式"});
            this.cmbDataType.Location = new System.Drawing.Point(101, 95);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(108, 20);
            this.cmbDataType.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "录像保存格式:";
            // 
            // nudPlayChannelNO
            // 
            this.nudPlayChannelNO.Location = new System.Drawing.Point(305, 96);
            this.nudPlayChannelNO.Name = "nudPlayChannelNO";
            this.nudPlayChannelNO.Size = new System.Drawing.Size(80, 21);
            this.nudPlayChannelNO.TabIndex = 7;
            this.nudPlayChannelNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(228, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "播放通道号:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(276, 56);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(109, 21);
            this.txtFileName.TabIndex = 9;
            // 
            // frmDataRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 195);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudPlayChannelNO);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDataType);
            this.Controls.Add(this.btnStopRecord);
            this.Controls.Add(this.btnStartRecord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDirPath);
            this.Controls.Add(this.btnShowOpenFileDlg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDataRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据流录像[录像格式转换]演示";
            this.Load += new System.EventHandler(this.frmDataRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudPlayChannelNO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShowOpenFileDlg;
        private System.Windows.Forms.TextBox txtDirPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartRecord;
        private System.Windows.Forms.Button btnStopRecord;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPlayChannelNO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.FolderBrowserDialog fbdMain;
    }
}