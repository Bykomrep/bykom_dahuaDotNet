namespace SDKPlayDemo
{
    partial class frmDrawFunSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDrawFunSet));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDisplayText = new System.Windows.Forms.TextBox();
            this.btnFontSet = new System.Windows.Forms.Button();
            this.chkDisplayTime = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numDisplayTextX = new System.Windows.Forms.NumericUpDown();
            this.numDisplayTextY = new System.Windows.Forms.NumericUpDown();
            this.numDisplayTimeY = new System.Windows.Forms.NumericUpDown();
            this.numDisplayTimeX = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fdlMain = new System.Windows.Forms.FontDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnColorSet = new System.Windows.Forms.Button();
            this.cdlMain = new System.Windows.Forms.ColorDialog();
            this.chkDraw = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbDrawStyle = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numDisplayTextX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDisplayTextY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDisplayTimeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDisplayTimeX)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(49, 349);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 33);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "设置";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(149, 349);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 33);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "内容:";
            // 
            // txtDisplayText
            // 
            this.txtDisplayText.Location = new System.Drawing.Point(55, 20);
            this.txtDisplayText.Name = "txtDisplayText";
            this.txtDisplayText.Size = new System.Drawing.Size(203, 21);
            this.txtDisplayText.TabIndex = 5;
            this.txtDisplayText.Text = "自定义文字内容";
            // 
            // btnFontSet
            // 
            this.btnFontSet.Location = new System.Drawing.Point(55, 20);
            this.btnFontSet.Name = "btnFontSet";
            this.btnFontSet.Size = new System.Drawing.Size(73, 24);
            this.btnFontSet.TabIndex = 6;
            this.btnFontSet.Text = "字体";
            this.btnFontSet.UseVisualStyleBackColor = true;
            this.btnFontSet.Click += new System.EventHandler(this.btnFontSet_Click);
            // 
            // chkDisplayTime
            // 
            this.chkDisplayTime.AutoSize = true;
            this.chkDisplayTime.Checked = true;
            this.chkDisplayTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayTime.Location = new System.Drawing.Point(6, 0);
            this.chkDisplayTime.Name = "chkDisplayTime";
            this.chkDisplayTime.Size = new System.Drawing.Size(72, 16);
            this.chkDisplayTime.TabIndex = 7;
            this.chkDisplayTime.Text = "显示时间";
            this.chkDisplayTime.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "上:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "左:";
            // 
            // numDisplayTextX
            // 
            this.numDisplayTextX.Location = new System.Drawing.Point(150, 90);
            this.numDisplayTextX.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numDisplayTextX.Name = "numDisplayTextX";
            this.numDisplayTextX.Size = new System.Drawing.Size(49, 21);
            this.numDisplayTextX.TabIndex = 10;
            this.numDisplayTextX.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numDisplayTextY
            // 
            this.numDisplayTextY.Location = new System.Drawing.Point(55, 92);
            this.numDisplayTextY.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numDisplayTextY.Name = "numDisplayTextY";
            this.numDisplayTextY.Size = new System.Drawing.Size(49, 21);
            this.numDisplayTextY.TabIndex = 11;
            this.numDisplayTextY.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numDisplayTimeY
            // 
            this.numDisplayTimeY.Location = new System.Drawing.Point(56, 30);
            this.numDisplayTimeY.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numDisplayTimeY.Name = "numDisplayTimeY";
            this.numDisplayTimeY.Size = new System.Drawing.Size(49, 21);
            this.numDisplayTimeY.TabIndex = 15;
            this.numDisplayTimeY.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numDisplayTimeX
            // 
            this.numDisplayTimeX.Location = new System.Drawing.Point(151, 30);
            this.numDisplayTimeX.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numDisplayTimeX.Name = "numDisplayTimeX";
            this.numDisplayTimeX.Size = new System.Drawing.Size(49, 21);
            this.numDisplayTimeX.TabIndex = 14;
            this.numDisplayTimeX.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "左:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "上:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDisplayText);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numDisplayTextX);
            this.groupBox1.Controls.Add(this.numDisplayTextY);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 117);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "自定义文字";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkDisplayTime);
            this.groupBox2.Controls.Add(this.numDisplayTimeX);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numDisplayTimeY);
            this.groupBox2.Location = new System.Drawing.Point(11, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 70);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnColorSet);
            this.groupBox3.Controls.Add(this.btnFontSet);
            this.groupBox3.Location = new System.Drawing.Point(12, 225);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(274, 57);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "文字格式设置";
            // 
            // btnColorSet
            // 
            this.btnColorSet.Location = new System.Drawing.Point(137, 20);
            this.btnColorSet.Name = "btnColorSet";
            this.btnColorSet.Size = new System.Drawing.Size(73, 24);
            this.btnColorSet.TabIndex = 7;
            this.btnColorSet.Text = "格式刷颜色";
            this.btnColorSet.UseVisualStyleBackColor = true;
            this.btnColorSet.Click += new System.EventHandler(this.btnColorSet_Click);
            // 
            // chkDraw
            // 
            this.chkDraw.AutoSize = true;
            this.chkDraw.Location = new System.Drawing.Point(29, 20);
            this.chkDraw.Name = "chkDraw";
            this.chkDraw.Size = new System.Drawing.Size(48, 16);
            this.chkDraw.TabIndex = 19;
            this.chkDraw.Text = "显示";
            this.chkDraw.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbDrawStyle);
            this.groupBox4.Controls.Add(this.chkDraw);
            this.groupBox4.Location = new System.Drawing.Point(12, 288);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(274, 49);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "自定义绘图";
            // 
            // cmbDrawStyle
            // 
            this.cmbDrawStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrawStyle.FormattingEnabled = true;
            this.cmbDrawStyle.Items.AddRange(new object[] {
            "曲线",
            "圆",
            "扇形"});
            this.cmbDrawStyle.Location = new System.Drawing.Point(96, 16);
            this.cmbDrawStyle.Name = "cmbDrawStyle";
            this.cmbDrawStyle.Size = new System.Drawing.Size(162, 20);
            this.cmbDrawStyle.TabIndex = 20;
            // 
            // frmDrawFunSet
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(307, 394);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDrawFunSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字符叠加设置";
            this.Load += new System.EventHandler(this.frmDrawFunSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numDisplayTextX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDisplayTextY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDisplayTimeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDisplayTimeX)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDisplayText;
        private System.Windows.Forms.Button btnFontSet;
        private System.Windows.Forms.CheckBox chkDisplayTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numDisplayTextX;
        private System.Windows.Forms.NumericUpDown numDisplayTextY;
        private System.Windows.Forms.NumericUpDown numDisplayTimeY;
        private System.Windows.Forms.NumericUpDown numDisplayTimeX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FontDialog fdlMain;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnColorSet;
        private System.Windows.Forms.ColorDialog cdlMain;
        private System.Windows.Forms.CheckBox chkDraw;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbDrawStyle;
    }
}