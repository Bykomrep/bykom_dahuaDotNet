using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DHPlaySDK;
using System.Runtime.InteropServices;
using Utility;

namespace SDKPlayDemo
{
    public partial class frmDisplayRegion : Form
    {
        /// <summary>
        /// 播放器通道号
        /// </summary>
        public int DisplayRegionPort = 0;
        /// <summary>
        /// 显示区域序号
        /// </summary>
        public UInt32 RegionSN = 1;

        
        public frmDisplayRegion()
        {
            InitializeComponent();
        }

        private void frmDisplayRegion_Load(object sender, EventArgs e)
        {
            StringUtil.InitControlText(this);
        }
        /// <summary>
        /// 显示按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            //此处需要判断文本框中是不是输入的纯整型数字

            Rectangle pRectangle = new Rectangle((int)nudLeft.Value, (int)nudTop.Value, (int)(nudRight.Value - nudLeft.Value), (int)(nudBottom.Value - nudTop.Value));
            IntPtr pBoxInfo = IntPtr.Zero;
            pBoxInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Rectangle)));//分配固定的指定大小的内存空间
            if (pBoxInfo != IntPtr.Zero)
            {
                Marshal.StructureToPtr(pRectangle, pBoxInfo, true); 
            }
            DHPlay.DHSetDisplayRegion(DisplayRegionPort, RegionSN, pBoxInfo, this.picDisplayMain.Handle, true);
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DHPlay.DHRefreshPlayEx(DisplayRegionPort, RegionSN);            
            
        }
        /// <summary>
        /// 关闭窗口的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDisplayRegion_FormClosing(object sender, FormClosingEventArgs e)
        {
            //此操作不做下次打开画面时会有问题,不做此操作不能正确显示
            DHPlay.DHSetDisplayRegion(DisplayRegionPort, RegionSN, IntPtr.Zero, this.picDisplayMain.Handle, false);
        }
    }
}