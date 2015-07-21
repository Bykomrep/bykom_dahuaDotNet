/*
 * ************************************************************************
 *                            SDK
 *                      大华网络SDK(C#版)示例程序
 * 
 * (c) Copyright 2007, ZheJiang Dahua Technology Stock Co.Ltd.
 *                      All Rights Reserved
 * 版 本 号:0.01
 * 文件名称:frm_MainC.cs
 * 功能说明:原始封装应用示例程序主画面
 * 作    者:李德明
 * 作成日期:2007/11/26
 * 修改日志:    日期        版本号      作者        变更事由
 *              2007/11/26  0.01        李德明      新建作成
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
using DHNetSDK;                         //大华网络SDK(C#)
using System.Runtime.InteropServices;
using DHPlaySDK;
using Utility;

namespace DaHuaNetSDKSample
{
    public partial class frm_MainC : Form
    {
        #region << 变量定义 >>

        /// <summary>
        /// 设备用户登录ＩＤ
        /// </summary>
        private int pLoginID;

        /// <summary>
        /// 程序消息提示Title
        /// </summary>
        private const string pMsgTitle = "大华网络SDK Demo程序";

        /// <summary>
        /// 最后操作信息显示格式
        /// </summary>
        private const string pErrInfoFormatStyle = "代码:errcode;\n描述:errmSG.";

        /// <summary>
        /// 当前回放的文件信息
        /// </summary>
        NET_RECORDFILE_INFO fileInfo;

        /// <summary>
        /// 播放方式
        /// </summary>
        private int playBy = 0;

        /// <summary>
        /// 实时播放句柄保存
        /// </summary>
        private int[] pRealPlayHandle;

        /// <summary>
        /// 回放句柄保存
        /// </summary>
        private int[] pPlayBackHandle;

        /// <summary>
        /// 回放通道号
        /// </summary>
        private int pPlayBackChannelID;

        /// <summary>
        /// 上次点击的PictureBox控件
        /// </summary>
        private PictureBox oldPicRealPlay;

        /// <summary>
        /// 当前点击的PictureBox控件
        /// </summary>
        private PictureBox picRealPlay;

        private fDisConnect disConnect;

        private NET_DEVICEINFO deviceInfo;

 
        #endregion

        #region << 窗口类构造函数 >>

        public frm_MainC()
        {
            InitializeComponent();

        }

        #endregion 

        #region << 应用功能代码 >>

        /// <summary>
        /// 画面初期化处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_MainC_Load(object sender, EventArgs e)
        {
            disConnect = new fDisConnect(DisConnectEvent);
            DHClient.DHInit(disConnect, IntPtr.Zero);
            DHClient.DHSetEncoding(LANGUAGE_ENCODING.gb2312);//字符编码格式设置，默认为gb2312字符编码，如果为其他字符编码请设置            
            pRealPlayHandle = null;
            btnRealPlay.Text = StringUtil.ConvertString("RealPlay");
            btnRealPlay.Enabled = false;
            btnPlayBackByTime.Enabled = false;
            gpbPlayBackControl.Enabled = false;
            btnUserLogout.Enabled = false;
            gpbPTZControl.Enabled = false;
            btnPlayByRecordFile.Enabled = false;

            StringUtil.InitControlText(this);
        }

        /// <summary>
        /// [设备用户登录]按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUserLogin_Click(object sender, EventArgs e)
        {
            frm_AddDevice fAddDev = new frm_AddDevice();
            fAddDev.ShowDialog();
            //DHClient.DHSetShowException(true);
            try
            {
                if (fAddDev.blnOKEnter == true)
                {
                    //设备用户信息获得
                    deviceInfo = new NET_DEVICEINFO();
                    int error = 0;
                    pLoginID = DHClient.DHLogin(fAddDev.cmbDevIP.Text.ToString(), ushort.Parse(fAddDev.txtDevProt.Text.ToString()),fAddDev.txtName.Text.ToString(), fAddDev.txtPassword.Text.ToString(), out deviceInfo, out error);
                    if (pLoginID != 0)
                    {
                        pPlayBackHandle = new int[deviceInfo.byChanNum];
                        //画面按钮有效性控制
                        pRealPlayHandle = null;
                        btnRealPlay.Text = StringUtil.ConvertString("RealPlay");
                        btnUserLogin.Enabled = false;
                        btnRealPlay.Enabled = true;
                        btnPlayBackByTime.Enabled = true;
                        gpbPlayBackControl.Enabled = false;
                        btnUserLogout.Enabled = true;
                        gpbPTZControl.Enabled = false;
                        btnPlayByRecordFile.Enabled = true;
                        cmbChannelSelect.Items.Clear();
                        for (int i = 0; i < deviceInfo.byChanNum; i++)
                        {
                            cmbChannelSelect.Items.Add(i.ToString());
                        }
                    }
                    else
                    { 
                        MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle),pMsgTitle);
                        btnUserLogin_Click(null, null);                        
                    }
                }
            }
            catch
            {
                //报最后一次操作的错误信息
                MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                btnUserLogin_Click(null, null);
            }
        }

        /// <summary>
        /// [设备用户注销]按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUserLogout_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = DHClient.DHLogout(pLoginID);
                if (result == false)
                {
                    //报最后一次操作的错误信息
                    MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                }
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
                }
                //画面初期化
                this.Controls.Clear();
                InitializeComponent();
                Utility.StringUtil.InitControlText(this);
                pLoginID = 0;
                fileInfo = new NET_RECORDFILE_INFO();
                playBy = 0;
                pRealPlayHandle = new int[16];
                pPlayBackHandle = new int[16];
                pPlayBackChannelID = 0;
                deviceInfo = new NET_DEVICEINFO();
                this.WindowState = FormWindowState.Normal;
                btnRealPlay.Enabled = false;
                btnPlayBackByTime.Enabled = false;
                gpbPlayBackControl.Enabled = false;
                btnUserLogout.Enabled = false;
                btnRealPlay.Text = StringUtil.ConvertString("RealPlay");
                btnPlayByRecordFile.Enabled = false;
                gpbPlayBackControl.Enabled = false;
                gpbPTZControl.Enabled = false;
            }
            catch
            {
                MessageBox.Show("设备用户注销失败！", pMsgTitle);                
            }
        }

        /// <summary>
        /// 窗口关闭处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_MainC_FormClosing(object sender, FormClosingEventArgs e)
        {
            DHClient.DHCleanup();
        }

        /// <summary>
        /// 实时播放按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        Dictionary<int, PictureBox> pantallas = new Dictionary<int, PictureBox>();
        private void btnRealPlay_Click(object sender, EventArgs e)
        {
            string strStart = StringUtil.ConvertString("RealPlay");
            string strStop = StringUtil.ConvertString("StopPlay");

            if (btnRealPlay.Text.Equals(strStart))
            {
                btnRealPlay.Text = strStop;
                pRealPlayHandle = new int[deviceInfo.byChanNum];
                cmbChannelSelect.Items.Clear();
                for (int i = 0; i < deviceInfo.byChanNum; i++)
                {
                    cmbChannelSelect.Items.Add(i.ToString());
                }
                cmbChannelSelect.SelectedIndex = 0;
                pantallas.Clear();
                for (int i = 0; i < deviceInfo.byChanNum; i++)
                {
                    switch (i)
                    {
                        case 0:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay0.Handle);
                            pantallas.Add(i, picRealPlay0);
                            break;
                        case 1:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay1.Handle);
                            pantallas.Add(i, picRealPlay1);
                            break;
                        case 2:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay2.Handle);
                            pantallas.Add(i, picRealPlay2);
                            break;
                        case 3:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay3.Handle);
                            pantallas.Add(i, picRealPlay3);
                            break;
                        case 4:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay4.Handle);
                            pantallas.Add(i, picRealPlay4);
                            break;
                        case 5:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay5.Handle);
                            pantallas.Add(i, picRealPlay5);
                            break;
                        case 6:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay6.Handle);
                            pantallas.Add(i, picRealPlay6);
                            break;
                        case 7:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay7.Handle);
                            pantallas.Add(i, picRealPlay7);
                            break;
                        case 8:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay8.Handle);
                            pantallas.Add(i, picRealPlay8);
                            break;
                        case 9:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay9.Handle);
                            pantallas.Add(i, picRealPlay9);
                            break;
                        case 10:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay10.Handle);
                            pantallas.Add(i, picRealPlay10);
                            break;
                        case 11:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay11.Handle);
                            pantallas.Add(i, picRealPlay11);
                            break;
                        case 12:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay12.Handle);
                            pantallas.Add(i, picRealPlay12);
                            break;
                        case 13:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay13.Handle);
                            pantallas.Add(i, picRealPlay13);
                            break;
                        case 14:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay14.Handle);
                            pantallas.Add(i, picRealPlay14);
                            break;
                        case 15:
                            pRealPlayHandle[i] = DHClient.DHRealPlay(pLoginID, i, picRealPlay15.Handle);
                            pantallas.Add(i, picRealPlay15);
                            break;
                    }

                }
                gpbPTZControl.Enabled = true;
            }
            else
            {
                btnRealPlay.Text = strStart;
                cmbChannelSelect.Items.Clear();
                for (int i = 0; i < deviceInfo.byChanNum; i++)
                {
                    DHClient.DHStopRealPlay(pRealPlayHandle[i]);

                }
                picRealPlay0.Refresh();
                picRealPlay1.Refresh();
                picRealPlay2.Refresh();
                picRealPlay3.Refresh();
                picRealPlay4.Refresh();
                picRealPlay5.Refresh();
                picRealPlay6.Refresh();
                picRealPlay7.Refresh();
                picRealPlay8.Refresh();
                picRealPlay9.Refresh();
                picRealPlay10.Refresh();
                picRealPlay11.Refresh();
                picRealPlay12.Refresh();
                picRealPlay13.Refresh();
                picRealPlay14.Refresh();
                picRealPlay15.Refresh();
                gpbPTZControl.Enabled = false;
            }
        }


        /// <summary>
        /// 按时间回放按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayBackByTime_Click(object sender, EventArgs e)
        {
            playBy = 1;
            frm_PlayBackByTimeSet fPBSet = new frm_PlayBackByTimeSet();
            fPBSet.cmbChannelSelect.Items.Clear();
            for (int i = 0; i < deviceInfo.byChanNum; i++)
            {
                fPBSet.cmbChannelSelect.Items.Add(i.ToString());
            }
            fileInfo = new NET_RECORDFILE_INFO();
            int fileCount = 0;
            bool blnQueryRecordFile = false;

            fPBSet.ShowDialog();
            if (fPBSet.blnOKEnter == true)
            {
                DateTime startTime = fPBSet.dtpStart.Value;
                DateTime endTime = fPBSet.dtpEnd.Value;
                if (startTime.Date >= endTime.Date)
                {
                    MessageBox.Show("开始日期不在结束日期设置前，请重新设置！", pMsgTitle);
                }
                else
                {
                    blnQueryRecordFile = DHClient.DHQueryRecordFile(pLoginID, int.Parse(fPBSet.txtChannelID.Text.ToString()), RECORD_FILE_TYPE.ALLRECORDFILE, 
                                                                    startTime, endTime, null, ref fileInfo, Marshal.SizeOf(typeof(NET_RECORDFILE_INFO)), out  fileCount, 5000, false);//按时间回放
                    if (blnQueryRecordFile == true)
                    {
                        if (picRealPlay == null)
                        {
                            picRealPlay = picRealPlay15;
                        }
                        pPlayBackChannelID = int.Parse(fPBSet.txtChannelID.Text.ToString());
                        pPlayBackHandle[pPlayBackChannelID] = DHClient.DHPlayBackByTime(pLoginID, pPlayBackChannelID, startTime, endTime, picRealPlay.Handle, null, IntPtr.Zero);
                        if (pPlayBackHandle[pPlayBackChannelID] == 0)
                        {
                            MessageBox.Show("按时间回放失败！", pMsgTitle);
                        }
                        else
                        {
                            btnPlay.Text = "||";
                            //画面按钮有效性控制
                            btnPlayBackByTime.Enabled = false;
                            gpbPlayBackControl.Enabled = true;
                            btnPlay.Enabled = true;
                            btnSlow.Enabled = true;
                            btnStop.Enabled = true;
                            btnFast.Enabled = true;
                            btnSetpPlayS.Enabled = true;
                            hsbPlayBack.Enabled = true;
                            btnPlayByRecordFile.Enabled = false;
                        }
                    }
                }
                //MessageBox.Show(blnQueryRecordFile.ToString(),MsgTitle);
            }
        }

        /// <summary>
        /// 播放暂停按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            //停止步进播放
            if (btnStepPlayE.Enabled == true)
            {
                btnStepPlayE_Click(null, null);
            }
            switch (btnPlay.Text.ToString())
            {
                case ">"://播放控制
                    if (DHClient.DHPlayBackControl(pPlayBackHandle[pPlayBackChannelID], PLAY_CONTROL.Play) == true)//播放
                    {
                        btnPlay.Text = "||";
                    }
                    break;
                case "||"://暂停控制
                    if (DHClient.DHPlayBackControl(pPlayBackHandle[pPlayBackChannelID], PLAY_CONTROL.Pause) == true)//暂停
                    {
                        btnPlay.Text = ">";
                    }
                    break;
            }
        }

        /// <summary>
        /// 停止按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (DHClient.DHPlayBackControl(pPlayBackHandle[pPlayBackChannelID], PLAY_CONTROL.Stop) == false)//停止回放
            {
                MessageBox.Show("停止回放失败！", pMsgTitle);
            }
            //画面按钮有效性控制
            gpbPlayBackControl.Enabled = false;
            btnPlay.Enabled = false;
            btnSlow.Enabled = false;
            btnStop.Enabled = false;
            btnFast.Enabled = false;
            btnSetpPlayS.Enabled = false;
            hsbPlayBack.Enabled = false;
            btnPlayBackByTime.Enabled = true;
            btnPlayByRecordFile.Enabled = true;
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
        /// 单步播放按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (DHClient.DHPlayBackControl(pPlayBackHandle[pPlayBackChannelID], PLAY_CONTROL.StepPlay) == true)//单步播放开始
                {
                    btnStepPlayE.Enabled = true;
                }
                else
                {
                    MessageBox.Show("单步播放错误！", pMsgTitle);
                }
            }
            catch
            {
                MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
            }

        }

        /// <summary>
        /// 步进播放停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStepPlayE_Click(object sender, EventArgs e)
        {
            try
            {
                if (DHClient.DHPlayBackControl(pPlayBackHandle[pPlayBackChannelID], PLAY_CONTROL.StepStop) == false)//单步播放停止
                {
                    MessageBox.Show("单步播放停止错误！", pMsgTitle);
                }
                
            }
            catch
            {
                MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
            }
            btnStepPlayE.Enabled = false;
        }

        /// <summary>
        /// 慢放按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSlow_Click(object sender, EventArgs e)
        {
            //停止步进播放
            if (btnStepPlayE.Enabled == true)
            {
                btnStepPlayE_Click(null, null);
            }

            DHClient.DHPlayBackControl(pPlayBackHandle[pPlayBackChannelID], PLAY_CONTROL.Slow);//慢放控制
        }

        /// <summary>
        /// 快放按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFast_Click(object sender, EventArgs e)
        {
            //停止步进播放
            if (btnStepPlayE.Enabled==true)
            {
                btnStepPlayE_Click(null, null);
            }
            DHClient.DHPlayBackControl(pPlayBackHandle[pPlayBackChannelID], PLAY_CONTROL.Fast);//快放控制
        }

        /// <summary>
        /// 拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hsbPlayBack_ValueChanged(object sender, EventArgs e)
        {
            //停止步进播放
            if (btnStepPlayE.Enabled == true)
            {
                btnStepPlayE_Click(null, null);
            }
            
            #region << 拖拽功能 >>
            
            switch (playBy)//播放方式0:按文件播放;1:按时间播放
            {
                case 0://按文件播放:按字节偏移播放回放
                    DHClient.DHPlayBackControl(pPlayBackHandle[pPlayBackChannelID], PLAY_CONTROL.SeekByBit, (uint)(hsbPlayBack.Value * (fileInfo.size/100)));
                    break;
                case 1://按时间播放:按时间编移播放回放
                    DHClient.DHPlayBackControl(pPlayBackHandle[pPlayBackChannelID], PLAY_CONTROL.SeekByTime, 
                                                (uint)(hsbPlayBack.Value * ((fileInfo.endtime.ToDateTime().TimeOfDay.TotalSeconds-fileInfo.starttime.ToDateTime().TimeOfDay.TotalSeconds)/100)));
                    break;                
            }

            #endregion
        }
        /// <summary>
        /// 云台控制[按钮鼠标按下]处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPTZControl_MouseDown(object sender, MouseEventArgs e)
        {
            int channelId = 0;
            if (cmbChannelSelect.SelectedIndex == -1)
            {
                MessageBox.Show("请选择通道！", pMsgTitle);
                return;
            }
            else
            {
                channelId = cmbChannelSelect.SelectedIndex;
            }
            ushort stepValue = 1;
            if (txtStep.Text.Length > 0)
            {
                stepValue = ushort.Parse(txtStep.Text.ToString());
            }
            else
            {
                MessageBox.Show("请输入步进(速度)值！", pMsgTitle);
                return;
            }

            # region <<**********云台控制代码**********>>
            switch (((Button)sender).Name)
            {
                case "btnLEFTTOP"://左上
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.EXTPTZ_LEFTTOP, stepValue, stepValue, false);
                    break;
                case "btnTOP"://上
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_UP_CONTROL, stepValue, false);
                    break;
                case "btnRIGHTDOWN"://右下
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.EXTPTZ_RIGHTDOWN, stepValue, stepValue, false);
                    break;
                case "btnLEFT"://左
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_LEFT_CONTROL, stepValue, false);
                    break;
                case "btnDOWN"://下
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_DOWN_CONTROL, stepValue, false);
                    break;
                case "btnRIGHT"://右
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_RIGHT_CONTROL, stepValue, false);
                    break;
                case "btnRIGHTTOP"://右上
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.EXTPTZ_RIGHTTOP, stepValue, stepValue, false);
                    break;
                case "btnLEFTDOWN"://右上
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.EXTPTZ_LEFTDOWN, stepValue, stepValue, false);
                    break;
                //case "btnZoomP"://放大
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_ZOOM_ADD_CONTROL, stepValue, false);
                //    break;
                //case "btnZoomD"://缩小
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_ZOOM_DEC_CONTROL, stepValue, false);
                //    break;
                //case "btnFocusP"://焦距近
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_FOCUS_ADD_CONTROL, stepValue, false);
                //    break;
                //case "btnFocusD"://焦距远
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_FOCUS_DEC_CONTROL, stepValue, false);
                //    break;
                //case "btnIrisOpen"://光圈开
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_APERTURE_ADD_CONTROL, stepValue, false);
                //    break;
                //case "btnIrisClose"://光圈关
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_APERTURE_DEC_CONTROL, stepValue, false);
                //    break;
            }
            #endregion 
        }
        /// <summary>
        /// 云台控制[按钮鼠标抬起]处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPTZControl_MouseUp(object sender, MouseEventArgs e)
        {
            int channelId = 0;
            if (cmbChannelSelect.SelectedIndex == -1)
            {
                MessageBox.Show("请选择通道！", pMsgTitle);
                return;
            }
            else
            {
                channelId = cmbChannelSelect.SelectedIndex;
            }
            ushort stepValue = 1;
            if (txtStep.Text.Length > 0)
            {
                stepValue = ushort.Parse(txtStep.Text.ToString());
            }
            else
            {
                MessageBox.Show("请输入步进(速度)值！", pMsgTitle);
                return;
            }

            # region <<**********云台控制代码**********>>
            switch (((Button)sender).Name)
            {
                case "btnLEFTTOP"://左上
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.EXTPTZ_LEFTTOP, stepValue, stepValue, true);
                    break;
                case "btnTOP"://上
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_UP_CONTROL, stepValue, true);
                    break;
                case "btnRIGHTDOWN"://右下
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.EXTPTZ_RIGHTDOWN, stepValue, stepValue, true);
                    break;
                case "btnLEFT"://左
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_LEFT_CONTROL, stepValue, true);
                    break;
                case "btnDOWN"://下
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_DOWN_CONTROL, stepValue, true);
                    break;
                case "btnRIGHT"://右
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_RIGHT_CONTROL, stepValue, true);
                    break;
                case "btnRIGHTTOP"://右上
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.EXTPTZ_RIGHTTOP, stepValue, stepValue, true);
                    break;
                case "btnLEFTDOWN"://右上
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.EXTPTZ_LEFTDOWN, stepValue, stepValue, true);
                    break;
                //case "btnZoomP"://放大
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_ZOOM_ADD_CONTROL, stepValue, true);
                //    break;
                //case "btnZoomD"://缩小
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_ZOOM_DEC_CONTROL, stepValue, true);
                //    break;
                //case "btnFocusP"://焦距近
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_FOCUS_ADD_CONTROL, stepValue, true);
                //    break;
                //case "btnFocusD"://焦距远
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_FOCUS_DEC_CONTROL, stepValue, true);
                //    break;
                //case "btnIrisOpen"://光圈开
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_APERTURE_ADD_CONTROL, stepValue, true);
                //    break;
                //case "btnIrisClose"://光圈关
                //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_APERTURE_DEC_CONTROL, stepValue, true);
                //    break;
            }
            #endregion 

        }
        /// <summary>
        /// 云台控制按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPTZControl_Click(object sender, EventArgs e)
        {
            int channelId = 0;
            if (cmbChannelSelect.SelectedIndex == -1)
            {
                MessageBox.Show("请选择通道！", pMsgTitle);
                return;
            }
            else
            {
                channelId = cmbChannelSelect.SelectedIndex;
            }
            ushort stepValue = 1;
            if (txtStep.Text.Length > 0)
            {
                stepValue = ushort.Parse(txtStep.Text.ToString());
            }
            else
            {
                MessageBox.Show("请输入步进(速度)值！", pMsgTitle);
                return;
            }

            # region <<**********云台控制代码**********>>
            switch (((Button)sender).Name)
            {
                case "btnZoomP"://放大
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_ZOOM_ADD_CONTROL, stepValue, false);
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_ZOOM_ADD_CONTROL, stepValue, true);
                    break;
                case "btnZoomD"://缩小
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_ZOOM_DEC_CONTROL, stepValue, false);
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_ZOOM_DEC_CONTROL, stepValue, true);
                    break;
                case "btnFocusP"://焦距近
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_FOCUS_ADD_CONTROL, stepValue, false);
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_FOCUS_ADD_CONTROL, stepValue, true);
                    break;
                case "btnFocusD"://焦距远
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_FOCUS_DEC_CONTROL, stepValue, false);
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_FOCUS_DEC_CONTROL, stepValue, true);
                    break;
                case "btnIrisOpen"://光圈开
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_APERTURE_ADD_CONTROL, stepValue, false);
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_APERTURE_ADD_CONTROL, stepValue, true);
                    break;
                case "btnIrisClose"://光圈关
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_APERTURE_DEC_CONTROL, stepValue, false);
                    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_APERTURE_DEC_CONTROL, stepValue, true);
                    break;
            }
            #endregion 
        }

        /// <summary>
        /// 按文件回放按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayByRecordFile_Click(object sender, EventArgs e)
        {
            playBy = 0;
            frm_PlayBackByFileSet fpf = new frm_PlayBackByFileSet();
            fpf.gLoginID = pLoginID;
            fpf.cmbChannelSelect.Items.Clear();
            for (int i = 0; i < deviceInfo.byChanNum; i++)
            {
                fpf.cmbChannelSelect.Items.Add(i.ToString());
            }
                fpf.ShowDialog(this);
            if (fpf.blnOKEnter == true)
            {

                if (picRealPlay == null)
                {
                    picRealPlay = picRealPlay15;
                }
                pPlayBackChannelID = int.Parse(fpf.txtChannelID.Text.ToString());
                fileInfo = fpf.gFileInfo;
                //**********按文件回放**********
                pPlayBackHandle[pPlayBackChannelID] = DHClient.DHPlayBackByRecordFile(pLoginID, ref fileInfo, picRealPlay.Handle, null, IntPtr.Zero);
                //******************************
                if (pPlayBackHandle[pPlayBackChannelID] == 0)
                {
                    MessageBox.Show("按时间回放失败！", pMsgTitle);
                }
                else
                {
                    //**********画面控制代码**********
                    btnPlay.Text = "||";
                    //画面按钮有效性控制
                    btnPlayBackByTime.Enabled = false;
                    gpbPlayBackControl.Enabled = true;
                    btnPlay.Enabled = true;
                    btnSlow.Enabled = true;
                    btnStop.Enabled = true;
                    btnFast.Enabled = true;
                    btnSetpPlayS.Enabled = true;
                    hsbPlayBack.Enabled = true;
                    btnPlayByRecordFile.Enabled = false;
                    //*********************************
                }
            }
        }

        /// <summary>
        /// 设备断开连接处理
        /// </summary>
        /// <param name="lLoginID"></param>
        /// <param name="pchDVRIP"></param>
        /// <param name="nDVRPort"></param>
        /// <param name="dwUser"></param>
        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            //设备断开连接处理            
            MessageBox.Show("设备用户断开连接", pMsgTitle);
        }
        /// <summary>
        /// 抓图处理代码
        /// </summary>
        /// <param name="hPlayHandle">播放句柄</param>
        /// <param name="bmpPath">图像促存路径</param>
        private void CapturePicture(int hPlayHandle,string bmpPath)
        {
            if (DHClient.DHCapturePicture(hPlayHandle, bmpPath))
            {
                //抓图成功处理
                MessageBox.Show("抓图成功!", pMsgTitle);
            }
            else
            {
                //抓图失败处理
                MessageBox.Show("抓图失败!", pMsgTitle);
            }
        }
        /// <summary>
        /// 抓图按钮按下
        /// </summary>
        /// <param name="hPlayHandle"></param>
        private void CapturePicture(int hPlayHandle)
        {
            string bmpPath = Application.StartupPath +  @"\DH_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp";
            //抓图处理
            CapturePicture(hPlayHandle, bmpPath);
        }
        #endregion

        #region << 窗口控制　与功能无关 >>

        private void txtStep_KeyPress(object sender, KeyPressEventArgs e)
        {

            //步进/速度,范围1~8, 8控制效果最明显btnPTZControl_Click事件使用该值
            if ((e.KeyChar >= '1' && e.KeyChar <= '8'))
            {
                txtStep.Text = e.KeyChar.ToString();
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtStep_KeyDown(object sender, KeyEventArgs e)
        {
            //步进(速度)值输入控制，与业务无关
            ushort i = 0;
            if (txtStep.Text.Length > 0)
            {

                i = ushort.Parse(txtStep.Text.ToString());
                if (e.KeyCode == Keys.Up)
                {
                    if (i < 8)
                    {
                        i += 1;
                        txtStep.Text = i.ToString();
                    }
                    e.Handled = true;

                }
                if (e.KeyCode == Keys.Down)
                {

                    if (i > 1)
                    {
                        i -= 1;
                        txtStep.Text = i.ToString();
                    }
                    e.Handled = true;
                }
                if (e.KeyCode == Keys.Back)
                {
                    txtStep.Text = "1";
                    e.Handled = true;
                }
            }
            else
            {
                txtStep.Text = "1";
            }
        }

        /// <summary>
        /// 图像控件单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picRealPlay_Click(object sender, EventArgs e)
        {
            if (btnPlayBackByTime.Enabled == true || btnPlayByRecordFile.Enabled == true)
            {
                if (oldPicRealPlay != null)
                {
                    oldPicRealPlay.BackColor = SystemColors.Control;
                }

                picRealPlay = (PictureBox)sender;
                picRealPlay.BackColor = SystemColors.ActiveBorder;
                oldPicRealPlay = picRealPlay;
            }
        }
        #endregion
        /// <summary>
        /// 抓图按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCapturePicture2_Click(object sender, EventArgs e)
        {
            CapturePicture(pPlayBackHandle[pPlayBackChannelID]);
        }
        /// <summary>
        /// 抓图按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCapturePicture_Click(object sender, EventArgs e)
        {
            int channelId = 0;
            if (cmbChannelSelect.SelectedIndex == -1)
            {
                MessageBox.Show("请选择通道！", pMsgTitle);
                return;
            }
            else
            {
                channelId = cmbChannelSelect.SelectedIndex;
            }
            CapturePicture(pRealPlayHandle[channelId]);
        }

        //private void btnSetControlPoint_Click(object sender, EventArgs e)
        //{
        //    int channelId = 0;
        //    if (cmbChannelSelect.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("请选择通道！", pMsgTitle);
        //        return;
        //    }
        //    else
        //    {
        //        channelId = cmbChannelSelect.SelectedIndex;
        //    }
        //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_POINT_SET_CONTROL, 1/*预置点值*/, true);
        //}

        //private void btnGotoControlPoint_Click(object sender, EventArgs e)
        //{
        //    int channelId = 0;
        //    if (cmbChannelSelect.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("请选择通道！", pMsgTitle);
        //        return;
        //    }
        //    else
        //    {
        //        channelId = cmbChannelSelect.SelectedIndex;
        //    }
        //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_POINT_MOVE_CONTROL, 1, true);
        //}

        //private void btnLampControlOn_Click(object sender, EventArgs e)
        //{
        //    int channelId = 0;
        //    if (cmbChannelSelect.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("请选择通道！", pMsgTitle);
        //        return;
        //    }
        //    else
        //    {
        //        channelId = cmbChannelSelect.SelectedIndex;
        //    }
        //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_LAMP_CONTROL, 1/*开启*/, true);
        //}

        //private void btnLampControlOff_Click(object sender, EventArgs e)
        //{
        //    int channelId = 0;
        //    if (cmbChannelSelect.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("请选择通道！", pMsgTitle);
        //        return;
        //    }
        //    else
        //    {
        //        channelId = cmbChannelSelect.SelectedIndex;
        //    }
        //    DHClient.DHPTZControl(pLoginID, channelId, PTZ_CONTROL.PTZ_LAMP_CONTROL, 0/*关闭*/, true);
        //}

        /// <summary>
        /// 云台扩展控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPTZControl_Click_1(object sender, EventArgs e)
        {
            frm_PTZControl fPTZ = new frm_PTZControl();
            fPTZ.LoginID = pLoginID;
            fPTZ.ShowDialog();
        }

        /// <summary>
        /// 多设备演示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMultiDrv_Click(object sender, EventArgs e)
        {
            frmMultiDVRSample fmds = new frmMultiDVRSample();
            fmds.ShowDialog();
        }
        private void visibleCuadrados(bool visible)
        {
            //picRealPlay0.Visible = visible;
            picRealPlay1.Visible = visible;
            picRealPlay2.Visible = visible;
            picRealPlay3.Visible = visible;
            picRealPlay4.Visible = visible;
            picRealPlay5.Visible = visible;
            picRealPlay6.Visible = visible;
            picRealPlay7.Visible = visible;
            picRealPlay8.Visible = visible;
            picRealPlay9.Visible = visible;
            picRealPlay10.Visible = visible;
            picRealPlay11.Visible = visible;
            picRealPlay12.Visible = visible;
            picRealPlay13.Visible = visible;
            picRealPlay14.Visible = visible;
            picRealPlay15.Visible = visible;


        }
        private void btnExpandir_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            
        }

        bool expandido = false;
        int oldWith = 0;
        int oldHeight = 0;
        Point oldLocation;
        private void expandir(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            
            //groupBox1.Visible = false;
            visibleCuadrados(false);
            int i = 0;
            foreach (PictureBox p  in pantallas.Values)
            {
                if (p.Name == pic.Name)
                {
                    break;
                }
                i++;
            }

            //DHClient.DHRealPlay(pLoginID, i, pictureBoxGrande.Handle);
            
            
            /*if (!expandido)
            {
                expandido = true;
                oldWith = pic.Width;
                oldHeight = pic.Height;
                oldLocation = pic.Location;
                pic.Size = new Size(900, 500);
                pic.Location = new Point(0, 0);
            }
            else
            {
                expandido = false;
                pic.Width = oldWith;
                pic.Height = oldHeight;
                pic.Location = oldLocation;
            }*/
        }
       
    }
}