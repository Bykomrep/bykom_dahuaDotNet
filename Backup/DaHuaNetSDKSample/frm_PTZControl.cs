using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DHNetSDK;

namespace DaHuaNetSDKSample
{
    public partial class frm_PTZControl : Form
    {
        /// <summary>
        /// 设备用户登录ＩＤ
        /// </summary>
        public int LoginID=0;

        private bool blnLam = true;
        public frm_PTZControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 设置预置点处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetPoint_Click(object sender, EventArgs e)
        {
            if (DHClient.DHPTZControl(LoginID, (int)nudChannel.Value, PTZ_CONTROL.PTZ_POINT_SET_CONTROL, (ushort)nudPointNO.Value/*预置点值*/, false))
            { 

            }
            else
            {
                MessageBox.Show("云台控制:设置预置点失败!", "提示");
            }
        }
        /// <summary>
        /// 画面加载处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PTZControl_Load(object sender, EventArgs e)
        {
            Utility.StringUtil.InitControlText(this);

            if (LoginID == 0)
            {
                MessageBox.Show("请登录设备","提示");
                this.Close();
            }
        }
        /// <summary>
        /// 转向预置点处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGotoPoint_Click(object sender, EventArgs e)
        {
            if (DHClient.DHPTZControl(LoginID, (int)nudChannel.Value, PTZ_CONTROL.PTZ_POINT_MOVE_CONTROL, (ushort)nudPointNO.Value/*预置点值*/, false))
            {

            }
            else
            {
                MessageBox.Show("云台控制:转向预置点失败!", "提示");
            }
        }
        /// <summary>
        /// 删除预置点处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelPoint_Click(object sender, EventArgs e)
        {
            if (DHClient.DHPTZControl(LoginID, (int)nudChannel.Value, PTZ_CONTROL.PTZ_POINT_DEL_CONTROL, (ushort)nudPointNO.Value/*预置点值*/, false))
            {

            }
            else
            {
                MessageBox.Show("云台控制:删除预定点失败!", "提示");
            }
        }
        /// <summary>
        /// 雨刷开关处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLamControl_Click(object sender, EventArgs e)
        {
            if (DHClient.DHPTZControl(LoginID, (int)nudChannel.Value, PTZ_CONTROL.PTZ_LAMP_CONTROL, (ushort)(blnLam == true ? 1 : 0), false))
            {
                blnLam = !blnLam;
            }
            else
            {
                MessageBox.Show("云台控制:雨刷功能失败!", "提示");
            }
        }
    }
}