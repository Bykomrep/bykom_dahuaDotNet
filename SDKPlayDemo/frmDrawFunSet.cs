using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Utility;

namespace SDKPlayDemo
{
    public partial class frmDrawFunSet : Form
    {
        /// <summary>
        /// 字体
        /// </summary>
        public Font FontSet;
        /// <summary>
        /// 格式刷
        /// </summary>
        public Brush BrushSet;
        /// <summary>
        /// 显示文字位置
        /// </summary>
        public PointF TextPointSet;
        /// <summary>
        /// 显示时间位置
        /// </summary>
        public PointF TimePointSet;
        /// <summary>
        /// 显示文字内容
        /// </summary>
        public string DisplayText;
        /// <summary>
        /// 是否显示时间
        /// </summary>
        public bool ShowTime;
        /// <summary>
        /// 是否按下确定按钮
        /// </summary>
        public bool BlnOK=true;
        /// <summary>
        /// 是否显示自定义绘图
        /// </summary>
        public bool BlnDraw = false;
        /// <summary>
        /// 绘图格式[0:曲线;1:圆;2:扇形]
        /// </summary>
        public int DrawStyle = 0;
        /// <summary>
        /// 颜色
        /// </summary>
        private Color pColor;
        public frmDrawFunSet()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 画面加载处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDrawFunSet_Load(object sender, EventArgs e)
        {
            pColor = Color.Red;

            StringUtil.InitControlText(this);
        }
        /// <summary>
        /// 确认按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            FontSet = txtDisplayText.Font;
            BrushSet=new SolidBrush(pColor);
            TextPointSet = new PointF((float)numDisplayTextX.Value, (float)numDisplayTextY.Value);
            TimePointSet = new PointF((float)numDisplayTimeX.Value, (float)numDisplayTimeY.Value);
            DisplayText = txtDisplayText.Text.ToString().Trim();
            ShowTime = chkDisplayTime.Checked;
            BlnDraw = chkDraw.Checked;
            DrawStyle = cmbDrawStyle.SelectedIndex;
            this.Close();
        }
        /// <summary>
        /// 字体设置按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFontSet_Click(object sender, EventArgs e)
        {
            fdlMain.ShowColor = true;
            if (fdlMain.ShowDialog() == DialogResult.OK)
            {
                txtDisplayText.Font = fdlMain.Font;
                txtDisplayText.ForeColor = fdlMain.Color;
            }
        }
        /// <summary>
        /// 颜色设置按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColorSet_Click(object sender, EventArgs e)
        {
            cdlMain.FullOpen = true;
            if (cdlMain.ShowDialog() == DialogResult.OK)
            {
                pColor = cdlMain.Color;
                txtDisplayText.ForeColor = pColor;
            }
        }
        /// <summary>
        /// 取消按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            BlnOK = false;
        }

    }
}