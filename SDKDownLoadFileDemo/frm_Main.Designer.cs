﻿namespace SDKDownLoadFileDemo
{
    partial class frm_Main
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
            this.btnUserLogin = new System.Windows.Forms.Button();
            this.grbMain = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnGetDownLoadPos1 = new System.Windows.Forms.Button();
            this.btnStopDownLoad1 = new System.Windows.Forms.Button();
            this.txtFileName1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDirSelect1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDirPath1 = new System.Windows.Forms.TextBox();
            this.btnDownLoad1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnGetDownLoadPos2 = new System.Windows.Forms.Button();
            this.btnStopDownLoad2 = new System.Windows.Forms.Button();
            this.txtFileName2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDirSelect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDirPath2 = new System.Windows.Forms.TextBox();
            this.btnDownLoad2 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.psbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.fbdMain = new System.Windows.Forms.FolderBrowserDialog();
            this.grbMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUserLogin
            // 
            this.btnUserLogin.Location = new System.Drawing.Point(12, 12);
            this.btnUserLogin.Name = "btnUserLogin";
            this.btnUserLogin.Size = new System.Drawing.Size(93, 28);
            this.btnUserLogin.TabIndex = 6;
            this.btnUserLogin.Text = "设备用户登录";
            this.btnUserLogin.UseVisualStyleBackColor = true;
            this.btnUserLogin.Click += new System.EventHandler(this.btnUserLogin_Click);
            // 
            // grbMain
            // 
            this.grbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbMain.Controls.Add(this.tabControl1);
            this.grbMain.Location = new System.Drawing.Point(12, 46);
            this.grbMain.Name = "grbMain";
            this.grbMain.Size = new System.Drawing.Size(790, 429);
            this.grbMain.TabIndex = 7;
            this.grbMain.TabStop = false;
            this.grbMain.Text = "录像下载";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(778, 403);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnGetDownLoadPos1);
            this.tabPage1.Controls.Add(this.btnStopDownLoad1);
            this.tabPage1.Controls.Add(this.txtFileName1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnDirSelect1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtDirPath1);
            this.tabPage1.Controls.Add(this.btnDownLoad1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(770, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "按文件方式";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnGetDownLoadPos1
            // 
            this.btnGetDownLoadPos1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGetDownLoadPos1.Location = new System.Drawing.Point(456, 226);
            this.btnGetDownLoadPos1.Name = "btnGetDownLoadPos1";
            this.btnGetDownLoadPos1.Size = new System.Drawing.Size(113, 28);
            this.btnGetDownLoadPos1.TabIndex = 28;
            this.btnGetDownLoadPos1.Text = "取当前下载进度";
            this.btnGetDownLoadPos1.UseVisualStyleBackColor = true;
            this.btnGetDownLoadPos1.Click += new System.EventHandler(this.btnGetDownLoadPos1_Click);
            // 
            // btnStopDownLoad1
            // 
            this.btnStopDownLoad1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStopDownLoad1.Location = new System.Drawing.Point(332, 226);
            this.btnStopDownLoad1.Name = "btnStopDownLoad1";
            this.btnStopDownLoad1.Size = new System.Drawing.Size(113, 28);
            this.btnStopDownLoad1.TabIndex = 27;
            this.btnStopDownLoad1.Text = "停止下载";
            this.btnStopDownLoad1.UseVisualStyleBackColor = true;
            this.btnStopDownLoad1.Click += new System.EventHandler(this.btnStopDownLoad1_Click);
            // 
            // txtFileName1
            // 
            this.txtFileName1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtFileName1.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtFileName1.Location = new System.Drawing.Point(191, 158);
            this.txtFileName1.Name = "txtFileName1";
            this.txtFileName1.Size = new System.Drawing.Size(409, 21);
            this.txtFileName1.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Location = new System.Drawing.Point(75, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "录像保存文件名：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDirSelect1
            // 
            this.btnDirSelect1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDirSelect1.Location = new System.Drawing.Point(606, 124);
            this.btnDirSelect1.Name = "btnDirSelect1";
            this.btnDirSelect1.Size = new System.Drawing.Size(90, 21);
            this.btnDirSelect1.TabIndex = 24;
            this.btnDirSelect1.Text = "目录选择";
            this.btnDirSelect1.UseVisualStyleBackColor = true;
            this.btnDirSelect1.Click += new System.EventHandler(this.btnDirSelect1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Location = new System.Drawing.Point(75, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "录像保存目录：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDirPath1
            // 
            this.txtDirPath1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDirPath1.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtDirPath1.Location = new System.Drawing.Point(191, 124);
            this.txtDirPath1.Name = "txtDirPath1";
            this.txtDirPath1.Size = new System.Drawing.Size(409, 21);
            this.txtDirPath1.TabIndex = 22;
            // 
            // btnDownLoad1
            // 
            this.btnDownLoad1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDownLoad1.Location = new System.Drawing.Point(208, 226);
            this.btnDownLoad1.Name = "btnDownLoad1";
            this.btnDownLoad1.Size = new System.Drawing.Size(113, 28);
            this.btnDownLoad1.TabIndex = 21;
            this.btnDownLoad1.Text = "录像文件下载";
            this.btnDownLoad1.UseVisualStyleBackColor = true;
            this.btnDownLoad1.Click += new System.EventHandler(this.btnDownLoad1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnGetDownLoadPos2);
            this.tabPage2.Controls.Add(this.btnStopDownLoad2);
            this.tabPage2.Controls.Add(this.txtFileName2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.btnDirSelect);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtDirPath2);
            this.tabPage2.Controls.Add(this.btnDownLoad2);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(770, 378);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "按时间方式";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnGetDownLoadPos2
            // 
            this.btnGetDownLoadPos2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGetDownLoadPos2.Location = new System.Drawing.Point(456, 226);
            this.btnGetDownLoadPos2.Name = "btnGetDownLoadPos2";
            this.btnGetDownLoadPos2.Size = new System.Drawing.Size(113, 28);
            this.btnGetDownLoadPos2.TabIndex = 20;
            this.btnGetDownLoadPos2.Text = "取当前下载进度";
            this.btnGetDownLoadPos2.UseVisualStyleBackColor = true;
            this.btnGetDownLoadPos2.Click += new System.EventHandler(this.btnGetDownLoadPos2_Click);
            // 
            // btnStopDownLoad2
            // 
            this.btnStopDownLoad2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStopDownLoad2.Location = new System.Drawing.Point(332, 226);
            this.btnStopDownLoad2.Name = "btnStopDownLoad2";
            this.btnStopDownLoad2.Size = new System.Drawing.Size(113, 28);
            this.btnStopDownLoad2.TabIndex = 19;
            this.btnStopDownLoad2.Text = "停止下载";
            this.btnStopDownLoad2.UseVisualStyleBackColor = true;
            this.btnStopDownLoad2.Click += new System.EventHandler(this.btnStopDownLoad2_Click);
            // 
            // txtFileName2
            // 
            this.txtFileName2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtFileName2.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtFileName2.Location = new System.Drawing.Point(191, 158);
            this.txtFileName2.Name = "txtFileName2";
            this.txtFileName2.Size = new System.Drawing.Size(409, 21);
            this.txtFileName2.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.Location = new System.Drawing.Point(75, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "录像保存文件名：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDirSelect
            // 
            this.btnDirSelect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDirSelect.Location = new System.Drawing.Point(606, 124);
            this.btnDirSelect.Name = "btnDirSelect";
            this.btnDirSelect.Size = new System.Drawing.Size(90, 21);
            this.btnDirSelect.TabIndex = 16;
            this.btnDirSelect.Text = "目录选择";
            this.btnDirSelect.UseVisualStyleBackColor = true;
            this.btnDirSelect.Click += new System.EventHandler(this.btnDirSelect_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.Location = new System.Drawing.Point(75, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "录像保存目录：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDirPath2
            // 
            this.txtDirPath2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDirPath2.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtDirPath2.Location = new System.Drawing.Point(191, 124);
            this.txtDirPath2.Name = "txtDirPath2";
            this.txtDirPath2.Size = new System.Drawing.Size(409, 21);
            this.txtDirPath2.TabIndex = 14;
            // 
            // btnDownLoad2
            // 
            this.btnDownLoad2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDownLoad2.Location = new System.Drawing.Point(208, 226);
            this.btnDownLoad2.Name = "btnDownLoad2";
            this.btnDownLoad2.Size = new System.Drawing.Size(113, 28);
            this.btnDownLoad2.TabIndex = 13;
            this.btnDownLoad2.Text = "录像文件下载";
            this.btnDownLoad2.UseVisualStyleBackColor = true;
            this.btnDownLoad2.Click += new System.EventHandler(this.btnDownLoad2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.psbMain});
            this.statusStrip1.Location = new System.Drawing.Point(0, 478);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(826, 24);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 19);
            this.toolStripStatusLabel1.Text = "下载进度";
            // 
            // psbMain
            // 
            this.psbMain.Name = "psbMain";
            this.psbMain.Size = new System.Drawing.Size(600, 18);
            this.psbMain.Step = 1;
            this.psbMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 502);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grbMain);
            this.Controls.Add(this.btnUserLogin);
            this.Name = "frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "录像查询下载[网络SDK(C#版原始封装)演示程序]";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grbMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUserLogin;
        private System.Windows.Forms.GroupBox grbMain;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar psbMain;
        private System.Windows.Forms.FolderBrowserDialog fbdMain;
        private System.Windows.Forms.TextBox txtFileName2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDirSelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDirPath2;
        private System.Windows.Forms.Button btnDownLoad2;
        private System.Windows.Forms.Button btnStopDownLoad2;
        private System.Windows.Forms.Button btnGetDownLoadPos1;
        private System.Windows.Forms.Button btnStopDownLoad1;
        private System.Windows.Forms.TextBox txtFileName1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDirSelect1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDirPath1;
        private System.Windows.Forms.Button btnDownLoad1;
        private System.Windows.Forms.Button btnGetDownLoadPos2;
    }
}

