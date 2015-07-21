namespace DaHuaNetSDKSample
{
    partial class frm_PTZControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PTZControl));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelPoint = new System.Windows.Forms.Button();
            this.btnGotoPoint = new System.Windows.Forms.Button();
            this.btnSetPoint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPointNO = new System.Windows.Forms.NumericUpDown();
            this.nudChannel = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLamControl = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannel)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDelPoint);
            this.groupBox2.Controls.Add(this.btnGotoPoint);
            this.groupBox2.Controls.Add(this.btnSetPoint);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nudPointNO);
            this.groupBox2.Location = new System.Drawing.Point(12, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(167, 125);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预置点配置";
            // 
            // btnDelPoint
            // 
            this.btnDelPoint.Location = new System.Drawing.Point(9, 92);
            this.btnDelPoint.Name = "btnDelPoint";
            this.btnDelPoint.Size = new System.Drawing.Size(150, 24);
            this.btnDelPoint.TabIndex = 6;
            this.btnDelPoint.Text = "删除预置点";
            this.btnDelPoint.UseVisualStyleBackColor = true;
            this.btnDelPoint.Click += new System.EventHandler(this.btnDelPoint_Click);
            // 
            // btnGotoPoint
            // 
            this.btnGotoPoint.Location = new System.Drawing.Point(9, 69);
            this.btnGotoPoint.Name = "btnGotoPoint";
            this.btnGotoPoint.Size = new System.Drawing.Size(150, 24);
            this.btnGotoPoint.TabIndex = 5;
            this.btnGotoPoint.Text = "转向预置点";
            this.btnGotoPoint.UseVisualStyleBackColor = true;
            this.btnGotoPoint.Click += new System.EventHandler(this.btnGotoPoint_Click);
            // 
            // btnSetPoint
            // 
            this.btnSetPoint.Location = new System.Drawing.Point(9, 46);
            this.btnSetPoint.Name = "btnSetPoint";
            this.btnSetPoint.Size = new System.Drawing.Size(150, 24);
            this.btnSetPoint.TabIndex = 4;
            this.btnSetPoint.Text = "设置预置点";
            this.btnSetPoint.UseVisualStyleBackColor = true;
            this.btnSetPoint.Click += new System.EventHandler(this.btnSetPoint_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "预置点编号:";
            // 
            // nudPointNO
            // 
            this.nudPointNO.Location = new System.Drawing.Point(84, 20);
            this.nudPointNO.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudPointNO.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPointNO.Name = "nudPointNO";
            this.nudPointNO.Size = new System.Drawing.Size(75, 21);
            this.nudPointNO.TabIndex = 2;
            this.nudPointNO.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudChannel
            // 
            this.nudChannel.Location = new System.Drawing.Point(63, 28);
            this.nudChannel.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudChannel.Name = "nudChannel";
            this.nudChannel.Size = new System.Drawing.Size(39, 21);
            this.nudChannel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "通道号:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLamControl);
            this.groupBox1.Location = new System.Drawing.Point(115, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(69, 46);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "灯光雨刷";
            // 
            // btnLamControl
            // 
            this.btnLamControl.Location = new System.Drawing.Point(6, 16);
            this.btnLamControl.Name = "btnLamControl";
            this.btnLamControl.Size = new System.Drawing.Size(50, 24);
            this.btnLamControl.TabIndex = 7;
            this.btnLamControl.Text = "开";
            this.btnLamControl.UseVisualStyleBackColor = true;
            this.btnLamControl.Click += new System.EventHandler(this.btnLamControl_Click);
            // 
            // frm_PTZControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 203);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudChannel);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_PTZControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "云台控制";
            this.Load += new System.EventHandler(this.frm_PTZControl_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannel)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDelPoint;
        private System.Windows.Forms.Button btnGotoPoint;
        private System.Windows.Forms.Button btnSetPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPointNO;
        private System.Windows.Forms.NumericUpDown nudChannel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLamControl;
    }
}