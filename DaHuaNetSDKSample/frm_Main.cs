/*
 * ************************************************************************
 *                            Demo程序
 *                      大华网络SDK(C#版)样例程序
 * 
 * (c) Copyright 2007, ZheJiang Dahua Technology Stock Co.Ltd.
 *                      All Rights Reserved
 * 版 本 号:1.00
 * 文件名称:frm_Main.cs
 * 功能说明:针对C#版SDK的应用开发示例[主窗口程序]
 * 作    者:李德明
 * 作成日期:2007/11/21
 * 修改日志:    日期        版本号      作者        变更事由
 *              2007/11/21  1.0         李德明      新建作成
 * 
 * ************************************************************************
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DHNetSDK.CS;                                          //大华网络SDK(C#版)开发包引用
using DHNetSDK;
using System.Runtime.InteropServices;                                  

namespace DaHuaNetSDKSample
{
    public partial class frm_Main : Form
    {
        #region << 变量定义 >>
        
        /// <summary>
        /// 设备用户对象
        /// </summary>
        private Client clientInstance = null;

        /// <summary>
        /// 设备用户信息
        /// </summary>
        private CLIENT_INFO clientInfo;

        /// <summary>
        /// 上次点击的PictureBox控件
        /// </summary>
        private PictureBox oldPicRealPlay;

        /// <summary>
        /// 当前点击的PictureBox控件
        /// </summary>
        private PictureBox picRealPlay;

        private const string MsgTitle = "大华网络SDK Demo程序";

        #endregion

        #region << 系统实现 >>

        /// <summary>
        /// 画面构造函数
        /// </summary>
        public frm_Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Main_Load(object sender, EventArgs e)
        {

            clientInstance = new Client();//初期化数字录相机
            
        }

        /// <summary>
        /// 关闭按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            switch (btnExit.Text)
            {
                case "关闭(全)":
                    if (picRealPlay != null)
                    {
                        picRealPlay.BackColor = SystemColors.Control;
                    }
                    clientInstance.StopRealPlay(0);//结束实时监视0
                    clientInstance.StopRealPlay(1);//结束实时监视1
                    clientInstance.StopRealPlay(2);//结束实时监视2
                    clientInstance.StopRealPlay(3);//结束实时监视3
                    clientInstance.StopRealPlay(4);//结束实时监视4
                    clientInstance.StopRealPlay(5);//结束实时监视5
                    clientInstance.StopRealPlay(6);//结束实时监视6
                    clientInstance.StopRealPlay(7);//结束实时监视7
                    picRealPlay0.Refresh();
                    picRealPlay1.Refresh();
                    picRealPlay2.Refresh();
                    picRealPlay3.Refresh();
                    picRealPlay4.Refresh();
                    picRealPlay5.Refresh();
                    picRealPlay6.Refresh();
                    picRealPlay7.Refresh();
                    btnExit.Text = "打开(全)";
                    break;
                case "打开(全)":
                    btnExit.Text = "关闭(全)";
                    clientInstance.RealPlay(0, this.picRealPlay0.Handle);//实时监视绑定容器0
                    clientInstance.RealPlay(1, this.picRealPlay1.Handle);//实时监视绑定容器1
                    clientInstance.RealPlay(2, this.picRealPlay2.Handle);//实时监视绑定容器2
                    clientInstance.RealPlay(3, this.picRealPlay3.Handle);//实时监视绑定容器3
                    clientInstance.RealPlay(4, this.picRealPlay4.Handle);//实时监视绑定容器4
                    clientInstance.RealPlay(5, this.picRealPlay5.Handle);//实时监视绑定容器5
                    clientInstance.RealPlay(6, this.picRealPlay6.Handle);//实时监视绑定容器6
                    clientInstance.RealPlay(7, this.picRealPlay7.Handle);//实时监视绑定容器7
                    break;
            }
            //this.Close();
        }

        /// <summary>
        /// 画面关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnDelDevice_Click(null, null);
            clientInstance.Cleanup();//释放资源
        }

        /// <summary>
        /// 添加设备按钮按下
        /// </summary>
        /// <param name="sender">系统参数</param>
        /// <param name="e">系统参数</param>
        private void btnAddDevice_Click(object sender, EventArgs e)
        {
            bool intLogin = false;
            frm_AddDevice fAddDev = new frm_AddDevice();
            fAddDev.ShowDialog();
            try
            {
                if (fAddDev.blnOKEnter == true)
                {
                    //设备用户信息获得
                    clientInfo.Name = fAddDev.txtName.Text.ToString();
                    clientInfo.DeviceIP = fAddDev.cmbDevIP.Text.ToString();
                    clientInfo.DevicePort = int.Parse(fAddDev.txtDevProt.Text.ToString());
                    clientInfo.Password = fAddDev.txtPassword.Text.ToString();
                    clientInfo.DeviceName = fAddDev.txtDevName.Text.ToString();
                    //将用户信息提交给类对象
                    clientInstance.ClientInfo = clientInfo;
                    intLogin = clientInstance.Login();//DVR设备用户登陆
                    if (intLogin ==true)
                    {
                        
                        clientInstance.RealPlay(0, this.picRealPlay0.Handle);//实时监视绑定容器0
                        clientInstance.RealPlay(1, this.picRealPlay1.Handle);//实时监视绑定容器1
                        clientInstance.RealPlay(2, this.picRealPlay2.Handle);//实时监视绑定容器2
                        clientInstance.RealPlay(3, this.picRealPlay3.Handle);//实时监视绑定容器3
                        clientInstance.RealPlay(4, this.picRealPlay4.Handle);//实时监视绑定容器4
                        clientInstance.RealPlay(5, this.picRealPlay5.Handle);//实时监视绑定容器5
                        clientInstance.RealPlay(6, this.picRealPlay6.Handle);//实时监视绑定容器6
                        clientInstance.RealPlay(7, this.picRealPlay7.Handle);//实时监视绑定容器7
                        //画面按钮有效性控制
                        btnDelDevice.Enabled = true;
                        btnAddDevice.Enabled = false;
                        btn_PlayBackByTime.Enabled = true;
                        btnExit.Enabled = true;
                        tclControl.Enabled = false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("设备信息输入有误！", MsgTitle);
                btnAddDevice_Click(null, null);
            }
        }

        /// <summary>
        /// 删除设备按钮按下
        /// </summary>
        /// <param name="sender">系统参数</param>
        /// <param name="e">系统参数</param>
        private void btnDelDevice_Click(object sender, EventArgs e)
        {
            clientInstance.Logout();//DVR设备用户注销
            //画面按钮有效性控制
            btnDelDevice.Enabled = false;
            btnAddDevice.Enabled = true;
            btn_PlayBackByTime.Enabled = false;
            btnExit.Enabled = false;
            tclControl.Enabled = false;
            picRealPlay0.Refresh();
            picRealPlay1.Refresh();
            picRealPlay2.Refresh();
            picRealPlay3.Refresh();
            picRealPlay4.Refresh();
            picRealPlay5.Refresh();
            picRealPlay6.Refresh();
            picRealPlay7.Refresh();
            if (picRealPlay != null)
            {
                picRealPlay.Refresh();
                picRealPlay.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// 按时间回放按钮按下
        /// </summary>
        /// <param name="sender">系统参数</param>
        /// <param name="e">系统参数</param>
        private void btn_PlayBackByTime_Click(object sender, EventArgs e)
        {
            if(clientInstance.Logined==true)
            {
                frm_PlayBackByTimeSet fPBSet = new frm_PlayBackByTimeSet();
                 NET_RECORDFILE_INFO fileInfo = new NET_RECORDFILE_INFO();
                int fileCount=0;
                bool blnQueryRecordFile = false;

                fPBSet.ShowDialog();
                if (fPBSet.blnOKEnter == true)
                {
                    DateTime startTime = fPBSet.dtpStart.Value;
                    DateTime endTime = fPBSet.dtpEnd.Value;
                    if (startTime.Date>= endTime.Date)
                    {
                        MessageBox.Show("开始日期不在结束日期设置前，请重新设置！", MsgTitle);
                    }
                    else
                    {
                        blnQueryRecordFile = clientInstance.QueryRecordFile(int.Parse(fPBSet.txtChannelID.Text.ToString()), 0, startTime, endTime, null, out fileInfo, Marshal.SizeOf(typeof(NET_RECORDFILE_INFO)), out fileCount, 5000, false);
                        if (blnQueryRecordFile == true)
                        {
                            if (picRealPlay == null)
                            {
                                picRealPlay = picRealPlay15;
                            }
                            if (clientInstance.PlayBackByTime(int.Parse(fPBSet.txtChannelID.Text.ToString()), startTime, endTime, picRealPlay.Handle, null, IntPtr.Zero) == false)
                            {
                                MessageBox.Show("按时间回放失败！", MsgTitle);
                            }
                            else
                            {
                                btnPlay.Text = "||";
                                //画面按钮有效性控制
                                btn_PlayBackByTime.Enabled = false;
                                tclControl.Enabled = true;
                                btnPlay.Enabled = true;
                                btnSlow.Enabled = true;
                                btnStop.Enabled = true;
                                btnFast.Enabled = true;
                                btnNext.Enabled = true;
                                hsbPlayBack.Enabled = true;
                            }
                        }
                    }
                    //MessageBox.Show(blnQueryRecordFile.ToString(),MsgTitle);
                }
            }
        }

        /// <summary>
        /// 播放和暂停按钮按下
        /// </summary>
        /// <param name="sender">系统定义</param>
        /// <param name="e">系统定义</param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            switch (btnPlay.Text.ToString())
            { 
                case ">"://播放控制
                    if (clientInstance.PlayBackControl(0, PlayCon.Play))
                    {
                        btnPlay.Text = "||";
                    }
                    break;
                case "||"://暂停控制
                    if (clientInstance.PlayBackControl(0, PlayCon.Pause) == true)
                    {
                        btnPlay.Text = ">";
                    }
                    break;
            }
        }

        /// <summary>
        /// 单步播放按钮按下
        /// </summary>
        /// <param name="sender">系统定义</param>
        /// <param name="e">系统定义</param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (btnNext.Text.ToString())
            {
                case ">|"://单步播放开始
                    if (clientInstance.PlayBackControl(0, PlayCon.StepPlay) == true)
                    {
                        btnNext.Text = ">||";
                    }
                    else
                    {
                        MessageBox.Show("单步播放错误！", MsgTitle);
                    }
                    break;
                case ">||"://单步播放停止
                    if (clientInstance.PlayBackControl(0, PlayCon.StepStop))
                    {
                        btnNext.Text = ">|";
                    }
                    else
                    {
                        MessageBox.Show("单步播放停止错误！", MsgTitle);
                    }
                    break;
            }
        }

        /// <summary>
        /// 停止播放按钮按下
        /// </summary>
        /// <param name="sender">系统定义</param>
        /// <param name="e">系统定义</param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (clientInstance.PlayBackControl(0, PlayCon.Stop) ==false)
            {
                MessageBox.Show("停止回放失败！", MsgTitle);
            }
            //画面按钮有效性控制
            btnPlay.Enabled = false;
            btnSlow.Enabled = false;
            btnStop.Enabled = false;
            btnFast.Enabled = false;
            btnNext.Enabled = false;
            hsbPlayBack.Enabled = false;
            btn_PlayBackByTime.Enabled = true;
            if (picRealPlay != null)
            {
                picRealPlay.Refresh();
                picRealPlay.BackColor = SystemColors.Control;
            }
            else
            {
                picRealPlay15.Refresh();
                picRealPlay.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// 慢放按钮按下
        /// </summary>
        /// <param name="sender">系统定义</param>
        /// <param name="e">系统定义</param>
        private void btnSlow_Click(object sender, EventArgs e)
        {
            clientInstance.PlayBackControl(0, PlayCon.Slow);
        }

        /// <summary>
        /// 快放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFast_Click(object sender, EventArgs e)
        {
            if (clientInstance.PlayBackControl(0, PlayCon.Fast) == false)
            { 
              //出错处理代码

            }
            
        }
         
        /// <summary>
        /// 播放窗口图片控制单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picRealPlay_Click(object sender, EventArgs e)
        {
            if (oldPicRealPlay != null)
            {
                oldPicRealPlay.BackColor = SystemColors.Control;
            }
            picRealPlay = (PictureBox)sender;
            picRealPlay.BackColor = SystemColors.ActiveBorder;
            oldPicRealPlay = picRealPlay;

        }
        #endregion

        
    }
}