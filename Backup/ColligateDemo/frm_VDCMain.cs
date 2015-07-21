using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DHVDCSDK;
using DHNetSDK;

namespace ColligateDemo
{
    public partial class frm_VDCDemo : Form
    {

        private NET_DEVICEINFO deviceInfo;
        /// <summary>
        /// 通道数
        /// </summary>
        private int ChannelCount=0;

        /// <summary>
        /// 设备用户登录ＩＤ
        /// </summary>
        private int pLoginID;

        /// <summary>
        /// 程序消息提示Title
        /// </summary>
        private const string pMsgTitle = "大华解码卡SDK Demo程序";

        /// <summary>
        /// 最后操作信息显示格式
        /// </summary>
        private const string pErrInfoFormatStyle = "代码:errcode;\n描述:errmSG.";

        /// <summary>
        /// 实时监视句柄
        /// </summary>
        private int uRealHandle;
        /// <summary>
        /// 初始化设备状态值
        /// </summary>
        private bool blnInitDevDevice = false;
        /// <summary>
        /// 初始化DirectDraw
        /// </summary>
        private bool blnInitDraw = false;
        /// <summary>
        /// 播放通道句柄
        /// </summary>
        private UInt32 hChannel = 0;

        private fDisConnect disConnect;

        private fRealDataCallBack cbRealData;

        public frm_VDCDemo()
        {
            InitializeComponent();
        }

        private void frm_VDCMain_Load(object sender, EventArgs e)
        {

            //初始化网络SDK
            //disConnect = new fDisConnect(DisConnectEvent);
            //if (DHClient.DHInit(disConnect, IntPtr.Zero)==true)
            //{

            //}
            //else
            //{
            //    MessageBox.Show("网络SDK初始化失败!", pMsgTitle);
            //    return;
            //}


            #region << 画面处理 >>
            //开始播放按钮
            btnStartPlay.Enabled = false;
            //暂停播放按钮
            btnPause.Enabled = false;
            btnPause.Tag = "1";
            btnPause.Text = "暂停播放";
            //停止播放按钮
            btnStop.Enabled = false;
            #endregion

            #region << 初始化解码卡设备 >>
            if (DHVDC.DHInitDecDevice(ref ChannelCount) == 0)
            {
                //MessageBox.Show("初始化解码卡成功！", "提示");
                blnInitDevDevice = true;
            }
            else
            {
                //MessageBox.Show("初始化解码卡失败！", "提示");
                blnInitDevDevice = false;
                return;
            }
            #endregion

            #region << 初始化DirectDraw >>
            if(DHVDC.DHInitDirectDraw(picMain.Handle,(uint)Color.Blue.ToArgb())==0)
            {
                //MessageBox.Show("初始化DirectDraw成功！", "提示");
                blnInitDraw = true;
            }
            else
            {
                //MessageBox.Show("初始化DirectDraw失败！", "提示");
                blnInitDraw = false;
                return;
            }
            #endregion
        }

        private void frm_VDCMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            # region << 释放DirectDraw >>
            if (blnInitDraw == true)
            {
                if (DHVDC.DHReleaseDirectDraw() == 0)
                {
                    //MessageBox.Show("释放DirectDraw成功！", pMsgTitle);
                }
                else
                {
                    //MessageBox.Show("释放DirectDraw失败！", pMsgTitle);
                    return;
                }
            }
            #endregion
            #region << 关闭设备 >>
            if (blnInitDevDevice == true)
            {
                if (DHVDC.DHReleaseDecDevice() == 0)
                {
                    //MessageBox.Show("关闭解码卡成功！", pMsgTitle);
                }
                else
                {
                    //MessageBox.Show("关闭解码卡失败！", pMsgTitle);
                    return;
                }
            }
            #endregion
        }

        private void btnStartPlay_Click(object sender, EventArgs e)
        {
            //打开通道
            if (DHVDC.DHChannelOpen(0, ref hChannel) == 0)
            {
                MessageBox.Show("打开通道成功！", pMsgTitle);
            }
            else
            {
                MessageBox.Show("打开通道失败！", pMsgTitle);
                return;
            }
            //打开流播放方式
            if(DHVDC.DHOpenStream(hChannel,IntPtr.Zero,(uint)921600/*900*1024*/)==0)
            {
                MessageBox.Show("打开流播放方式成功！", pMsgTitle);
            }
            else
            {
                MessageBox.Show("打开流播放方式失败！", pMsgTitle);
                return;
            }
            //通过网络SDK取数据
            cbRealData = new fRealDataCallBack(cbRealDataFun);
            uRealHandle = DHClient.DHRealPlay(pLoginID, 0, IntPtr.Zero);//只取数据不播放
            DHVDC.DHPlay(hChannel, picMain.Handle);
            if (DHClient.DHSetRealDataCallBack(uRealHandle, cbRealData, IntPtr.Zero)==true)//设置数据回调处理函数
            {
                // MessageBox.Show("设置数据回调处理函数成功！", pMsgTitle);
            }
            else
            {
                MessageBox.Show("设置数据回调处理函数失败！", pMsgTitle);
                return;
            }
            btnPause.Enabled = true;
            btnStop.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            switch (int.Parse(btnPause.Tag.ToString()))
            { 
                case 0://继续播放
                    btnPause.Tag = "1";
                    btnPause.Text = "暂停播放";
                    if (DHVDC.DHPause(hChannel, false)==0)
                    {
                        MessageBox.Show("继续播放成功!", pMsgTitle);
                    }
                    break;
                case 1://暂停播放
                    btnPause.Tag = "0";
                    btnPause.Text = "继续播放";
                    if (DHVDC.DHPause(hChannel,true)==0)
                    {
                        MessageBox.Show("暂停播放成功!", pMsgTitle);
                    }
                    break;
            }
        }

        private void btnAddDev_Click(object sender, EventArgs e)
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
                    //设备用户登录
                    pLoginID = DHClient.DHLogin(fAddDev.cmbDevIP.Text.ToString(), ushort.Parse(fAddDev.txtDevProt.Text.ToString()), fAddDev.txtName.Text.ToString(), fAddDev.txtPassword.Text.ToString(), out deviceInfo, out error);
                    if (pLoginID != 0)
                    {                        
                        //btnStartRealPlay.Enabled = true;
                        btnStartPlay.Enabled = true;
                    }
                    else
                    {
                        //btnStartRealPlay.Enabled = false;
                        btnStartPlay.Enabled = false;
                        MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                    }
                }
            }
            catch
            {
                //报最后一次操作的错误信息
                MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
            }
        }

        /// <summary>
        /// 实时监视数据回调数据处理
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwDataType"></param>
        /// <param name="pBuffer"></param>
        /// <param name="dwBufSize"></param>
        /// <param name="dwUser"></param>
        private void cbRealDataFun(int lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr dwUser)
        {
            DHVDC.DHInputData(hChannel, pBuffer, dwBufSize);
        }

        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            MessageBox.Show("设备断开！", pMsgTitle);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }
    }
}