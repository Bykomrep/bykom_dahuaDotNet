﻿namespace DaHuaNetSDKSample
{
    partial class frmMultiDVRSample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMultiDVRSample));
            this.btnAddDevice1 = new System.Windows.Forms.Button();
            this.btnAddDevice2 = new System.Windows.Forms.Button();
            this.btnRealPlay1 = new System.Windows.Forms.Button();
            this.btnRealPlay2 = new System.Windows.Forms.Button();
            this.btnStopReal2 = new System.Windows.Forms.Button();
            this.btnStopReal1 = new System.Windows.Forms.Button();
            this.picRealPlay2 = new System.Windows.Forms.PictureBox();
            this.picRealPlay1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picRealPlay2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRealPlay1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddDevice1
            // 
            this.btnAddDevice1.Location = new System.Drawing.Point(292, 21);
            this.btnAddDevice1.Name = "btnAddDevice1";
            this.btnAddDevice1.Size = new System.Drawing.Size(101, 41);
            this.btnAddDevice1.TabIndex = 0;
            this.btnAddDevice1.Text = "添加设备";
            this.btnAddDevice1.UseVisualStyleBackColor = true;
            this.btnAddDevice1.Click += new System.EventHandler(this.btnAddDevice1_Click);
            // 
            // btnAddDevice2
            // 
            this.btnAddDevice2.Location = new System.Drawing.Point(292, 20);
            this.btnAddDevice2.Name = "btnAddDevice2";
            this.btnAddDevice2.Size = new System.Drawing.Size(101, 41);
            this.btnAddDevice2.TabIndex = 1;
            this.btnAddDevice2.Text = "添加设备";
            this.btnAddDevice2.UseVisualStyleBackColor = true;
            this.btnAddDevice2.Click += new System.EventHandler(this.btnAddDevice2_Click);
            // 
            // btnRealPlay1
            // 
            this.btnRealPlay1.Location = new System.Drawing.Point(292, 65);
            this.btnRealPlay1.Name = "btnRealPlay1";
            this.btnRealPlay1.Size = new System.Drawing.Size(101, 41);
            this.btnRealPlay1.TabIndex = 4;
            this.btnRealPlay1.Text = "实时监控";
            this.btnRealPlay1.UseVisualStyleBackColor = true;
            this.btnRealPlay1.Click += new System.EventHandler(this.btnRealPlay1_Click);
            // 
            // btnRealPlay2
            // 
            this.btnRealPlay2.Location = new System.Drawing.Point(292, 67);
            this.btnRealPlay2.Name = "btnRealPlay2";
            this.btnRealPlay2.Size = new System.Drawing.Size(101, 41);
            this.btnRealPlay2.TabIndex = 5;
            this.btnRealPlay2.Text = "实时监控";
            this.btnRealPlay2.UseVisualStyleBackColor = true;
            this.btnRealPlay2.Click += new System.EventHandler(this.btnRealPlay2_Click);
            // 
            // btnStopReal2
            // 
            this.btnStopReal2.Location = new System.Drawing.Point(292, 114);
            this.btnStopReal2.Name = "btnStopReal2";
            this.btnStopReal2.Size = new System.Drawing.Size(101, 41);
            this.btnStopReal2.TabIndex = 7;
            this.btnStopReal2.Text = "停止监控";
            this.btnStopReal2.UseVisualStyleBackColor = true;
            this.btnStopReal2.Click += new System.EventHandler(this.btnStopReal2_Click);
            // 
            // btnStopReal1
            // 
            this.btnStopReal1.Location = new System.Drawing.Point(292, 112);
            this.btnStopReal1.Name = "btnStopReal1";
            this.btnStopReal1.Size = new System.Drawing.Size(101, 41);
            this.btnStopReal1.TabIndex = 6;
            this.btnStopReal1.Text = "停止监控";
            this.btnStopReal1.UseVisualStyleBackColor = true;
            this.btnStopReal1.Click += new System.EventHandler(this.btnStopReal1_Click);
            // 
            // picRealPlay2
            // 
            this.picRealPlay2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picRealPlay2.Location = new System.Drawing.Point(7, 20);
            this.picRealPlay2.Name = "picRealPlay2";
            this.picRealPlay2.Size = new System.Drawing.Size(281, 233);
            this.picRealPlay2.TabIndex = 3;
            this.picRealPlay2.TabStop = false;
            // 
            // picRealPlay1
            // 
            this.picRealPlay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picRealPlay1.Location = new System.Drawing.Point(7, 21);
            this.picRealPlay1.Name = "picRealPlay1";
            this.picRealPlay1.Size = new System.Drawing.Size(281, 233);
            this.picRealPlay1.TabIndex = 2;
            this.picRealPlay1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStopReal1);
            this.groupBox1.Controls.Add(this.btnAddDevice1);
            this.groupBox1.Controls.Add(this.picRealPlay1);
            this.groupBox1.Controls.Add(this.btnRealPlay1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 265);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备一";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picRealPlay2);
            this.groupBox2.Controls.Add(this.btnAddDevice2);
            this.groupBox2.Controls.Add(this.btnStopReal2);
            this.groupBox2.Controls.Add(this.btnRealPlay2);
            this.groupBox2.Location = new System.Drawing.Point(12, 281);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 265);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设备二";
            // 
            // frmMultiDVRSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 557);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMultiDVRSample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "多台DVR设备同时点播演示";
            this.Load += new System.EventHandler(this.frmMultiDVRSample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picRealPlay2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRealPlay1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddDevice1;
        private System.Windows.Forms.Button btnAddDevice2;
        private System.Windows.Forms.PictureBox picRealPlay1;
        private System.Windows.Forms.PictureBox picRealPlay2;
        private System.Windows.Forms.Button btnRealPlay1;
        private System.Windows.Forms.Button btnRealPlay2;
        private System.Windows.Forms.Button btnStopReal2;
        private System.Windows.Forms.Button btnStopReal1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}