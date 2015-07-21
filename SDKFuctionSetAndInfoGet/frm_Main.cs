using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DHNetSDK;                 //大华网络SDK
using DaHuaNetSDKSample;        //大华通用画面例程
using Utility;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace SDKFuctionSetAndInfoGet
{
    public partial class frm_Main : Form
    {
        /// <summary>
        /// 设备用户登录句柄
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
        /// 设备信息配置
        /// </summary>
        private DHDEV_SYSTEM_ATTR_CFG sysAttrConfig;

        /// <summary>
        /// 所有图象通道属性
        /// </summary>
        private DHDEV_CHANNEL_CFG[] channelConfig;

        /// <summary>
        /// 网络配置属性
        /// </summary>
        DHDEV_NET_CFG netConfig;

        /// <summary>
        /// 定时录像配置信息
        /// </summary>
        DHDEV_RECORD_CFG[] recConfig;

        /// <summary>
        /// 串口配置
        /// </summary>
        DHDEV_COMM_CFG commConfig;

        /// <summary>
        /// 报警设置
        /// </summary>
        DHDEV_ALARM_SCHEDULE alarmAllConfig;

        /// <summary>
        /// 断开回调
        /// </summary>
        private fDisConnect disConnect;

        /// <summary>
        /// 升级句柄
        /// </summary>
        private Int32 hUpgradeId;

        /// <summary>
        /// 升级回调
        /// </summary>
        private fUpgradeCallBack upgradeCallBack;


        /// <summary>
        /// 升级进度
        /// </summary>
        public delegate void UpdatePosDelegate(int pos);
        private UpdatePosDelegate updatePosDelegate;
        private void UpdatePos(int npos)
        {
            if (-1 == npos)
            {
                hUpgradeId = 0;
                progressBarUpdate.Value = 0;
                labelUpgradeMsg.Text = "升级进度";
                MessageBox.Show("升级完成");
            }
            else if (-2 == npos)
            {
                MessageBox.Show("发送升级数据失败!");
                if (hUpgradeId != 0)
                {
                    if (!DHClient.DHCLIENT_StopUpgrade(hUpgradeId))
                    {
                        MessageBox.Show("结束升级失败");
                    }
                    hUpgradeId = 0;
                    progressBarUpdate.Value = 0;
                    labelUpgradeMsg.Text = "升级进度";
                }
            }
            else
            {
                progressBarUpdate.Value = npos;
                if (npos >= 100)
                {
                    labelUpgradeMsg.Text = "文件已传送完毕，请等待..";
                }
            }
        }


        public frm_Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 取得版本号按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSDKVersion_Click(object sender, EventArgs e)
        {
            ////标准备SDK版本号格式:00.00.00.00
            //labSDKVersion.Text = DHClient.DHGetSDKVersion();
            //数字型SDK版本号格式:0.0.0.0
            labSDKVersion.Text = DHClient.DHGetSDKVersion("D3");
            //labSDKVersion.Text = DHClient.DHGetSDKVersion();//等同于:DHClient.DHGetSDKVersion("S3");
        }
        /// <summary>
        /// 画面加载处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Main_Load(object sender, EventArgs e)
        {
            disConnect = new fDisConnect(DisConnectEvent);
            DHClient.DHInit(disConnect, IntPtr.Zero);//SDK初始化
            DHClient.DHSetEncoding(LANGUAGE_ENCODING.gb2312);//字符编码格式设置，默认为gb2312字符编码，如果为其他字符编码请设置
            grbMain.Enabled = false;

            hUpgradeId = 0;
            upgradeCallBack = new fUpgradeCallBack(UpgradeCallBack);

            this.updatePosDelegate = this.UpdatePos;

            StringUtil.InitControlText(this);
        }
        /// <summary>
        /// 用户登录按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUserLogin_Click(object sender, EventArgs e)
        {
            string strLogin = StringUtil.ConvertString("设备用户登录");
            string strLogout = StringUtil.ConvertString("设备用户注销");

            if (btnUserLogin.Text.Equals(strLogin))
            {
                frm_AddDevice fadFrom = new frm_AddDevice();
                fadFrom.ShowDialog();
                if (fadFrom.blnOKEnter == true)
                {
                    //设备用户信息获得
                    NET_DEVICEINFO deviceInfo = new NET_DEVICEINFO();
                    int error = 0;
                    pLoginID = DHClient.DHLogin(fadFrom.cmbDevIP.Text.ToString(), ushort.Parse(fadFrom.txtDevProt.Text.ToString()), fadFrom.txtName.Text.ToString(), fadFrom.txtPassword.Text.ToString(), out deviceInfo, out error);
                    if (pLoginID != 0)
                    {
                        btnUserLogin.BackColor = Color.Yellow;
                        btnUserLogin.Text = strLogout;
                        grbMain.Enabled = true;
                        tbcMain.SelectedIndex = 0;
                    }
                    else
                    {
                        //报最后一次操作的错误信息
                        MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                        btnUserLogin_Click(null, null);
                    }
                }
            }
            else
            {
                bool result = DHClient.DHLogout(pLoginID);
                if (result == false)
                {
                    //报最后一次操作的错误信息
                    MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                }

                //画面初始化
                this.Controls.Clear();
                InitializeComponent();
                pLoginID = 0;
                sysAttrConfig = new DHDEV_SYSTEM_ATTR_CFG();
                channelConfig = new DHDEV_CHANNEL_CFG[16];
                netConfig = new DHDEV_NET_CFG();
                recConfig = new DHDEV_RECORD_CFG[16];
                commConfig = new DHDEV_COMM_CFG();
                alarmAllConfig = new DHDEV_ALARM_SCHEDULE();
                btnUserLogin.BackColor = Color.Transparent;
                btnUserLogin.Text = strLogin;
                grbMain.Enabled = false;
                tbcMain.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 读取按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadConfig_Click(object sender, EventArgs e)
        {
            bool returnValue = false;
            switch (tbcMain.SelectedIndex)
            {
                case 0://设备属性                    
                    returnValue = DHClient.DHGetDevConfig(pLoginID, ref sysAttrConfig);
                    if (returnValue == true)
                    {
                        setDataToControl(sysAttrConfig);
                    }
                    else
                    {
                        //报最后一次操作的错误信息
                        MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                    }
                    break;
                case 1://通道属性

                    # region << 获取指定通道的通道属性 >>
                    //DHDEV_CHANNEL_CFG channelConfig = new DHDEV_CHANNEL_CFG();
                    //returnValue = DHClient.DHGetDevConfig(pLoginID, 0, ref channelConfig);
                    //if (returnValue == true)
                    //{
                    //    setDataToControl(channelConfig);
                    //}
                    //else
                    //{
                    //    //报最后一次操作的错误信息
                    //    MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                    //}
                    #endregion

                    #region << 获取所有通道的通道属性  >>
                    if (sysAttrConfig.Equals(new DHDEV_SYSTEM_ATTR_CFG()) == false)
                    {
                        channelConfig = new DHDEV_CHANNEL_CFG[sysAttrConfig.byVideoCaptureNum];
                        #region << 通道号选择列表处理 >>
                        cmbChannelNum.Items.Clear();
                        for (int i = 0; i < sysAttrConfig.byVideoCaptureNum; i++)
                        {
                            cmbChannelNum.Items.Add(i.ToString());
                        }
                        #endregion
                        returnValue = DHClient.DHGetDevConfig(pLoginID, ref channelConfig);
                        if (returnValue == true)
                        {
                            //setDataToControl(channelConfig[0]);//显示0通道信息
                            cmbChannelNum.SelectedIndex = 0;//显示0通道信息
                        }
                        else
                        {
                            //报最后一次操作的错误信息
                            MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                        }
                    }
                    #endregion

                    #region << 多预览 >>
                    //DHDEV_PREVIEW_CFG previewConfig = new DHDEV_PREVIEW_CFG();
                    //returnValue = DHClient.DHGetDevConfig(pLoginID, ref previewConfig);
                    //if(returnValue==true)
                    //{

                    //}
                    //else
                    //{
                    //    //报最后一次操作的错误信息
                    //    MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                    //}
                    #endregion

                    break;
                case 2://串口属性
                    //获取串口属性
                    commConfig = new DHDEV_COMM_CFG();
                    returnValue = DHClient.DHGetDevConfig(pLoginID, ref commConfig);
                    if (returnValue == true)
                    {
                        cmbCOMFunction.Items.Clear();
                        foreach(DH_PROANDFUN_NAME pName in commConfig.s232FuncName)
                        {
                            cmbCOMFunction.Items.Add(DHClient.DHByteArrayToString(pName.ProName));
                        }
                        cmb458DecProName.Items.Clear();
                        foreach (DH_PROANDFUN_NAME pName in commConfig.DecProName)
                        {
                            cmb458DecProName.Items.Add(DHClient.DHByteArrayToString(pName.ProName));
                        }
                        cmbRS232.SelectedIndex = 0;
                        cmb485.SelectedIndex = 0;
                        //setDataToControl(commConfig.st232[0]);//显示232串口属性
                        //setDataToControl(commConfig.stDecoder[0]);//显示485串口属性
                    }
                    else
                    {
                        //报最后一次操作的错误信息
                        MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                    }

                    break;
                case 3://录像配置

                    #region << 获取指定通道录像配置 >>
                    //DHDEV_RECORD_CFG recConfig = new DHDEV_RECORD_CFG();
                    //returnValue = DHClient.DHGetDevConfig(pLoginID, 0, ref recConfig);
                    //if (returnValue == true)
                    //{
                    //    //setDataToControl(recConfig);//显示录像配置
                    //}
                    //else
                    //{
                    //    //报最后一次操作的错误信息
                    //    MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                    //}
                    #endregion

                    #region << 获取所有通道录像配置 >>
                    if (sysAttrConfig.Equals(new DHDEV_SYSTEM_ATTR_CFG()) == false)
                    {
                        cmbRecChannel.Items.Clear();
                        for (int i = 0; i < sysAttrConfig.byVideoCaptureNum; i++)
                        {
                            cmbRecChannel.Items.Add(i.ToString());
                        }
                            recConfig = new DHDEV_RECORD_CFG[sysAttrConfig.byVideoCaptureNum];
                        returnValue = DHClient.DHGetDevConfig(pLoginID, ref recConfig);

                        if (returnValue == true)
                        {
                            cmbRecChannel.SelectedIndex = 0;
                        }
                        else
                        {
                            //报最后一次操作的错误信息
                            MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                        }
                    }
                    #endregion

                    break;
                case 4://网络配置

                    returnValue = DHClient.DHGetDevConfig(pLoginID, ref  netConfig);
                    if (returnValue == true)
                    {
                        setDataToControl(netConfig);
                    }
                    else
                    {
                        //报最后一次操作的错误信息
                        MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                    }
                    break;
                case 5://报警属性
                    alarmAllConfig = new DHDEV_ALARM_SCHEDULE();
                    returnValue = DHClient.DHGetDevConfig(pLoginID, ref  alarmAllConfig);
                    if (returnValue == true)
                    {
                        cmbAlarm.SelectedIndex = 0;                        
                        //setDataToControl(alarmConfig);//显示报警属性
                    }
                    else
                    {
                        //报最后一次操作的错误信息
                        MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                    }
                    ////DVR时间信息的取得
                    //NET_TIME dvrTime = new NET_TIME();
                    //returnValue = DHClient.DHGetDevConfig(pLoginID, ref  dvrTime);
                    //if (returnValue == true)
                    //{
                    //    //setDataToControl(alarmConfig);//显示报警属性
                    //}
                    //else
                    //{
                    //    //报最后一次操作的错误信息
                    //    MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                    //}
                    //break;                    
                    break;
            }
        }

        #region << 获取数据显示到画面控件 >>
        /// <summary>
        /// 232串口配置显示
        /// </summary>
        /// <param name="c232Config">232串口配置</param>
        private void setDataToControl(DH_RS232_CFG c232Config)
        {
            try
            {
                cmbCOMFunction.SelectedIndex = (int)((uint)c232Config.byFunction);
                cmbCOMDataBit.SelectedIndex = (int)((uint)c232Config.struComm.byDataBit);
                cmbCOMStopBit.SelectedIndex = (int)((uint)c232Config.struComm.byStopBit);
                cmbCOMBaudRate.SelectedIndex = (int)((uint)c232Config.struComm.byBaudRate);
                cmbCOMParity.SelectedIndex = (int)((uint)c232Config.struComm.byParity);
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }
        }
        /// <summary>
        /// 458串口配置显示
        /// </summary>
        /// <param name="c232Config">458串口配置</param>
        private void setDataToControl(DH_485_CFG c458Config)
        {
            try
            {
                cmb458DecProName.SelectedIndex = (int)c458Config.wProtocol;
                cmb458DataBit.SelectedIndex = (int)((uint)c458Config.struComm.byDataBit);
                cmb458StopBit.SelectedIndex = (int)((uint)c458Config.struComm.byStopBit);
                cmb458BaudRate.SelectedIndex = (int)((uint)c458Config.struComm.byBaudRate);
                cmb458Parity.SelectedIndex = (int)((uint)c458Config.struComm.byParity);
                txt458Add.Text = c458Config.wDecoderAddress.ToString();
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }

        }
        /// <summary>
        /// 设备信息显示
        /// </summary>
        /// <param name="sysAttrConfig">设备属性</param>
        private void setDataToControl(DHDEV_SYSTEM_ATTR_CFG sysAttrConfig)
        {
            try
            {
                txtDevType.Text = sysAttrConfig.DevType();//设备类型                
                txtDevType2.Text="";
                for(int i =0 ; i<sysAttrConfig.szDevType.Length;i++)
                {
                    txtDevType2.Text += (char)sysAttrConfig.szDevType[i];
                }
                txtSN.Text = sysAttrConfig.DevSerialNo();//序列号
                txtVideoIn.Text = sysAttrConfig.byVideoCaptureNum.ToString("D");//视频输入数
                txtAudioIn.Text = sysAttrConfig.byAudioCaptureNum.ToString("D");//音频输入数
                txtAlarmIn.Text = sysAttrConfig.byAlarmInNum.ToString("D");//报警输入数
                txtAlarmOut.Text = sysAttrConfig.byAlarmOutNum.ToString("D");//报警输出数
                txtNetIO.Text = sysAttrConfig.byNetIONum.ToString("D");//网络口数
                txtUSBIO.Text = sysAttrConfig.byUsbIONum.ToString("D");//USB口数
                txtIDEIO.Text = sysAttrConfig.byIdeIONum.ToString("D");//IDE口数
                txt232IO.Text = sysAttrConfig.byComIONum.ToString("D");//232口数
                txtLPTIO.Text = sysAttrConfig.byLPTIONum.ToString("D");//并口数
                txtTalkIn.Text = sysAttrConfig.byTalkInChanNum.ToString("D");//对讲输入数
                txtTalkOut.Text = sysAttrConfig.byTalkOutChanNum.ToString("D");//对讲输出数
                txtDecodeChanNum.Text = sysAttrConfig.byDecodeChanNum.ToString();//解码通道数
                txtIdeControlNum.Text = sysAttrConfig.byIdeControlNum.ToString();//IDE控制器数
                txtIdeControlType.Text = sysAttrConfig.byIdeControlType.ToString();//IDE控制器类型
                txtVgaIONum.Text = sysAttrConfig.byVgaIONum.ToString();//VGA口数
                txtDevNo.Text = sysAttrConfig.wDevNo.ToString();//设备号
                cmbOverWrite.SelectedIndex = sysAttrConfig.byOverWrite;//硬盘满
                txtRecordLen.Text = sysAttrConfig.byRecordLen.ToString();//录像打包长度
                cmbVideoStandard.SelectedIndex = sysAttrConfig.byVideoStandard;//视频制式
                cmbTimeFmt.SelectedIndex = sysAttrConfig.byTimeFmt;//时间格式
                cmbDateFormat.SelectedIndex = sysAttrConfig.byDateFormat;//日期格式
                cmbDateSprtr.SelectedIndex = sysAttrConfig.byDateSprtr;//日期分割符
                txtStartChanNo.Text = sysAttrConfig.byStartChanNo.ToString();//开始通道号
                txtSoftWareVersion.Text = "软件版本:" + DHClient.DHUInt32ToString(sysAttrConfig.stVersion.dwSoftwareVersion, "V1.V2") + ";创建日期:" + DHClient.DHUInt32ToString(sysAttrConfig.stVersion.dwSoftwareBuildDate, "yyyy年m月d日");//软件版本
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }
        }
        /// <summary>
        /// 网络配置信息显示
        /// </summary>
        /// <param name="netConfig">网络配置</param>
        private void setDataToControl(DHDEV_NET_CFG netConfig)
        {
            try
            {
                txtDevName.Text = DHClient.DHByteArrayToString(netConfig.sDevName);
                txtTcpLinkNum.Text = netConfig.wTcpMaxConnectNum.ToString();
                txtTcpPort.Text = netConfig.wTcpPort.ToString();
                txtUDPPort.Text = netConfig.wUdpPort.ToString();
                txtHTTPPort.Text = netConfig.wHttpPort.ToString();
                txtHTTPSPort.Text = netConfig.wHttpsPort.ToString();
                txtSSLPort.Text = netConfig.wSslPort.ToString();                
                //邮件信息读取
                txtMailCCAdd.Text = netConfig.struMail.CcAddr();
                txtMailUserName.Text = DHClient.DHByteArrayToString(netConfig.struMail.sUserName);
                txtMailPassword.Text = DHClient.DHByteArrayToString(netConfig.struMail.sUserPsw);
                txtMailPort.Text = netConfig.struMail.wMailPort.ToString();
                txtMailIPAdd.Text = DHClient.DHByteArrayToString(netConfig.struMail.sMailIPAddr);
                txtMailSendAdd.Text = DHClient.DHByteArrayToString(netConfig.struMail.sSenderAddr);
                txtMailReceiveAdd.Text = DHClient.DHByteArrayToString(netConfig.struMail.sDestAddr);
                txtMailBCCAdd.Text = DHClient.DHByteArrayToString(netConfig.struMail.sBccAddr);
                txtMailSubject.Text = DHClient.DHByteArrayToString(netConfig.struMail.sSubject);
                //以太网信息
                cmbNetIONum.SelectedIndex = 0;
                //远程主机信息
                cmbRemohost.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }
        }

        /// <summary>
        /// 通道信息显示
        /// </summary>
        /// <param name="netConfig">通道信息</param>
        private void setDataToControl(DHDEV_CHANNEL_CFG channelConfig)
        {
            try
            {
                txtChannelName.Text = DHClient.DHByteArrayToString(channelConfig.szChannelName);
                txtBrightness0.Text = channelConfig.stColorCfg[0].byBrightness.ToString("D");//亮度
                txtContrast0.Text = channelConfig.stColorCfg[0].byContrast.ToString("D");//对比度
                txtSaturation0.Text = channelConfig.stColorCfg[0].bySaturation.ToString("D");//饱和度
                txtHue0.Text = channelConfig.stColorCfg[0].byHue.ToString("D");//色度
                chkGainEn0.Checked = (channelConfig.stColorCfg[0].byGainEn == 1 ? true : false);//增益使能
                txtGain0.Text = channelConfig.stColorCfg[0].byGain.ToString("D");//增益
                txtBrightness1.Text = channelConfig.stColorCfg[1].byBrightness.ToString("D");//亮度
                txtContrast1.Text = channelConfig.stColorCfg[1].byContrast.ToString("D");//对比度
                txtSaturation1.Text = channelConfig.stColorCfg[1].bySaturation.ToString("D");//饱和度
                txtHue1.Text = channelConfig.stColorCfg[1].byHue.ToString("D");//色度
                chkGainEn1.Checked = (channelConfig.stColorCfg[1].byGainEn == 1 ? true : false);//增益使能
                txtGain1.Text = channelConfig.stColorCfg[1].byGain.ToString("D");//增益
                cmbVideoEncOpt.SelectedIndex = 0;//码流选择
                cmbOSD.SelectedIndex = 0;//OSD类型选择

            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }
        }

        /// <summary>
        /// 录像配置信息显示
        /// </summary>
        /// <param name="netConfig">录像配置信息</param>
        private void setDataToControl(DHDEV_RECORD_CFG recConfig)
        {
            try
            {
                chkRedundancyEn.Checked = (recConfig.byRedundancyEn == 1 ? true : false);
                txtPreRecordLen.Text = recConfig.byPreRecordLen.ToString();
                cmbWeeks.SelectedIndex = -1;
                cmbWeeks.SelectedIndex = 0;

            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }
        }

        /// <summary>
        /// 录像配置信息时间段信息显示
        /// </summary>
        /// <param name="netConfig">时间段信息</param>
        private void setDataToControl(DH_REC_TSECT tsectConfig)
        {
            try
            {
                //时间段一
                chkT1Enable.Checked = tsectConfig.sTSECT[0].bEnable;
                txtT1StartH.Text = tsectConfig.sTSECT[0].iBeginHour.ToString();
                txtT1StartM.Text = tsectConfig.sTSECT[0].iBeginMin.ToString();
                txtT1StartS.Text = tsectConfig.sTSECT[0].iBeginSec.ToString();
                txtT1EndH.Text = tsectConfig.sTSECT[0].iEndHour.ToString();
                txtT1EndM.Text = tsectConfig.sTSECT[0].iEndMin.ToString();
                txtT1EndS.Text = tsectConfig.sTSECT[0].iEndSec.ToString();
                //时间段二
                chkT2Enable.Checked = tsectConfig.sTSECT[1].bEnable;
                txtT2StartH.Text = tsectConfig.sTSECT[1].iBeginHour.ToString();
                txtT2StartM.Text = tsectConfig.sTSECT[1].iBeginMin.ToString();
                txtT2StartS.Text = tsectConfig.sTSECT[1].iBeginSec.ToString();
                txtT2EndH.Text = tsectConfig.sTSECT[1].iEndHour.ToString();
                txtT2EndM.Text = tsectConfig.sTSECT[1].iEndMin.ToString();
                txtT2EndS.Text = tsectConfig.sTSECT[1].iEndSec.ToString();
                //时间段三
                chkT3Enable.Checked = tsectConfig.sTSECT[2].bEnable;
                txtT3StartH.Text = tsectConfig.sTSECT[2].iBeginHour.ToString();
                txtT3StartM.Text = tsectConfig.sTSECT[2].iBeginMin.ToString();
                txtT3StartS.Text = tsectConfig.sTSECT[2].iBeginSec.ToString();
                txtT3EndH.Text = tsectConfig.sTSECT[2].iEndHour.ToString();
                txtT3EndM.Text = tsectConfig.sTSECT[2].iEndMin.ToString();
                txtT3EndS.Text = tsectConfig.sTSECT[2].iEndSec.ToString();
                //时间段四
                chkT4Enable.Checked = tsectConfig.sTSECT[3].bEnable;
                txtT4StartH.Text = tsectConfig.sTSECT[3].iBeginHour.ToString();
                txtT4StartM.Text = tsectConfig.sTSECT[3].iBeginMin.ToString();
                txtT4StartS.Text = tsectConfig.sTSECT[3].iBeginSec.ToString();
                txtT4EndH.Text = tsectConfig.sTSECT[3].iEndHour.ToString();
                txtT4EndM.Text = tsectConfig.sTSECT[3].iEndMin.ToString();
                txtT4EndS.Text = tsectConfig.sTSECT[3].iEndSec.ToString();
                //时间段五
                chkT5Enable.Checked = tsectConfig.sTSECT[4].bEnable;
                txtT5StartH.Text = tsectConfig.sTSECT[4].iBeginHour.ToString();
                txtT5StartM.Text = tsectConfig.sTSECT[4].iBeginMin.ToString();
                txtT5StartS.Text = tsectConfig.sTSECT[4].iBeginSec.ToString();
                txtT5EndH.Text = tsectConfig.sTSECT[4].iEndHour.ToString();
                txtT5EndM.Text = tsectConfig.sTSECT[4].iEndMin.ToString();
                txtT5EndS.Text = tsectConfig.sTSECT[4].iEndSec.ToString();
                //时间段六
                chkT6Enable.Checked = tsectConfig.sTSECT[5].bEnable;
                txtT6StartH.Text = tsectConfig.sTSECT[5].iBeginHour.ToString();
                txtT6StartM.Text = tsectConfig.sTSECT[5].iBeginMin.ToString();
                txtT6StartS.Text = tsectConfig.sTSECT[5].iBeginSec.ToString();
                txtT6EndH.Text = tsectConfig.sTSECT[5].iEndHour.ToString();
                txtT6EndM.Text = tsectConfig.sTSECT[5].iEndMin.ToString();
                txtT6EndS.Text = tsectConfig.sTSECT[5].iEndSec.ToString();
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }
        }
         /// <summary>
        /// 录像配置信息时间段信息显示
        /// </summary>
        /// <param name="netConfig">时间段信息</param>
        private void setDataToControl(DH_REC_TSECT tsectConfig,string strType)
        {
            try
            {
                if (strType == "Alarm")
                {
                    //时间段一
                    chkAT1Enable.Checked = tsectConfig.sTSECT[0].bEnable;
                    txtAT1StartH.Text = tsectConfig.sTSECT[0].iBeginHour.ToString();
                    txtAT1StartM.Text = tsectConfig.sTSECT[0].iBeginMin.ToString();
                    txtAT1StartS.Text = tsectConfig.sTSECT[0].iBeginSec.ToString();
                    txtAT1EndH.Text = tsectConfig.sTSECT[0].iEndHour.ToString();
                    txtAT1EndM.Text = tsectConfig.sTSECT[0].iEndMin.ToString();
                    txtAT1EndS.Text = tsectConfig.sTSECT[0].iEndSec.ToString();
                    //时间段二
                    chkAT2Enable.Checked = tsectConfig.sTSECT[1].bEnable;
                    txtAT2StartH.Text = tsectConfig.sTSECT[1].iBeginHour.ToString();
                    txtAT2StartM.Text = tsectConfig.sTSECT[1].iBeginMin.ToString();
                    txtAT2StartS.Text = tsectConfig.sTSECT[1].iBeginSec.ToString();
                    txtAT2EndH.Text = tsectConfig.sTSECT[1].iEndHour.ToString();
                    txtAT2EndM.Text = tsectConfig.sTSECT[1].iEndMin.ToString();
                    txtAT2EndS.Text = tsectConfig.sTSECT[1].iEndSec.ToString();
                    //时间段三
                    chkAT3Enable.Checked = tsectConfig.sTSECT[2].bEnable;
                    txtAT3StartH.Text = tsectConfig.sTSECT[2].iBeginHour.ToString();
                    txtAT3StartM.Text = tsectConfig.sTSECT[2].iBeginMin.ToString();
                    txtAT3StartS.Text = tsectConfig.sTSECT[2].iBeginSec.ToString();
                    txtAT3EndH.Text = tsectConfig.sTSECT[2].iEndHour.ToString();
                    txtAT3EndM.Text = tsectConfig.sTSECT[2].iEndMin.ToString();
                    txtAT3EndS.Text = tsectConfig.sTSECT[2].iEndSec.ToString();
                    //时间段四
                    chkAT4Enable.Checked = tsectConfig.sTSECT[3].bEnable;
                    txtAT4StartH.Text = tsectConfig.sTSECT[3].iBeginHour.ToString();
                    txtAT4StartM.Text = tsectConfig.sTSECT[3].iBeginMin.ToString();
                    txtAT4StartS.Text = tsectConfig.sTSECT[3].iBeginSec.ToString();
                    txtAT4EndH.Text = tsectConfig.sTSECT[3].iEndHour.ToString();
                    txtAT4EndM.Text = tsectConfig.sTSECT[3].iEndMin.ToString();
                    txtAT4EndS.Text = tsectConfig.sTSECT[3].iEndSec.ToString();
                    //时间段五
                    chkAT5Enable.Checked = tsectConfig.sTSECT[4].bEnable;
                    txtAT5StartH.Text = tsectConfig.sTSECT[4].iBeginHour.ToString();
                    txtAT5StartM.Text = tsectConfig.sTSECT[4].iBeginMin.ToString();
                    txtAT5StartS.Text = tsectConfig.sTSECT[4].iBeginSec.ToString();
                    txtAT5EndH.Text = tsectConfig.sTSECT[4].iEndHour.ToString();
                    txtAT5EndM.Text = tsectConfig.sTSECT[4].iEndMin.ToString();
                    txtAT5EndS.Text = tsectConfig.sTSECT[4].iEndSec.ToString();
                    //时间段六
                    chkAT6Enable.Checked = tsectConfig.sTSECT[5].bEnable;
                    txtAT6StartH.Text = tsectConfig.sTSECT[5].iBeginHour.ToString();
                    txtAT6StartM.Text = tsectConfig.sTSECT[5].iBeginMin.ToString();
                    txtAT6StartS.Text = tsectConfig.sTSECT[5].iBeginSec.ToString();
                    txtAT6EndH.Text = tsectConfig.sTSECT[5].iEndHour.ToString();
                    txtAT6EndM.Text = tsectConfig.sTSECT[5].iEndMin.ToString();
                    txtAT6EndS.Text = tsectConfig.sTSECT[5].iEndSec.ToString();
                }
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }
        }

        /// <summary>
        /// 消息触发配置处理
        /// </summary>
        private void setActionMask(DH_MSG_HANDLE msgHandle)
        {
            //是否有效
            chkActionMask01.Enabled = ((int)(msgHandle.dwActionMask & 0x00000001) == 0x00000001 ? true : false);//上传服务器　0x00000001
            chkActionMask02.Enabled = ((int)(msgHandle.dwActionMask & 0x00000002) == 0x00000002 ? true : false);//联动录像　0x00000002
            chkActionMask08.Enabled = ((int)(msgHandle.dwActionMask & 0x00000008) == 0x00000008 ? true : false);//发送邮件　0x00000008
            chkActionMask10.Enabled = ((int)(msgHandle.dwActionMask & 0x00000010) == 0x00000010 ? true : false);//设备本地报警轮巡　0x00000010
            chkActionMask20.Enabled = ((int)(msgHandle.dwActionMask & 0x00000020) == 0x00000020 ? true : false);//设备提示使能　0x00000020
            chkActionMask40.Enabled = ((int)(msgHandle.dwActionMask & 0x00000040) == 0x00000040 ? true : false);//设备报警输出使能　0x00000040
            //是否选择
            chkActionMask01.Checked = ((int)(msgHandle.dwActionFlag & 0x00000001) == 0x00000001 ? true : false);//上传服务器　0x00000001
            chkActionMask02.Checked = ((int)(msgHandle.dwActionFlag & 0x00000002) == 0x00000002 ? true : false);//联动录像　0x00000002
            chkActionMask08.Checked = ((int)(msgHandle.dwActionFlag & 0x00000008) == 0x00000008 ? true : false);//发送邮件　0x00000008
            chkActionMask10.Checked = ((int)(msgHandle.dwActionFlag & 0x00000010) == 0x00000010 ? true : false);//设备本地报警轮巡　0x00000010
            chkActionMask20.Checked = ((int)(msgHandle.dwActionFlag & 0x00000020) == 0x00000020 ? true : false);//设备提示使能　0x00000020
            chkActionMask40.Checked = ((int)(msgHandle.dwActionFlag & 0x00000040) == 0x00000040 ? true : false);//设备报警输出使能　0x00000040

            chkRelAlarmOut01.Checked = ((int)msgHandle.byRelAlarmOut[0] == 1 ? true : false);
            chkRelAlarmOut02.Checked = ((int)msgHandle.byRelAlarmOut[1] == 1 ? true : false);
            chkRelAlarmOut03.Checked = ((int)msgHandle.byRelAlarmOut[2] == 1 ? true : false);
            chkRelAlarmOut04.Checked = ((int)msgHandle.byRelAlarmOut[3] == 1 ? true : false);
            chkRelAlarmOut05.Checked = ((int)msgHandle.byRelAlarmOut[4] == 1 ? true : false);
            chkRelAlarmOut06.Checked = ((int)msgHandle.byRelAlarmOut[5] == 1 ? true : false);
            chkRelAlarmOut07.Checked = ((int)msgHandle.byRelAlarmOut[6] == 1 ? true : false);
            chkRelAlarmOut08.Checked = ((int)msgHandle.byRelAlarmOut[7] == 1 ? true : false);
            chkRelAlarmOut09.Checked = ((int)msgHandle.byRelAlarmOut[8] == 1 ? true : false);
            chkRelAlarmOut10.Checked = ((int)msgHandle.byRelAlarmOut[9] == 1 ? true : false);
            chkRelAlarmOut11.Checked = ((int)msgHandle.byRelAlarmOut[10] == 1 ? true : false);
            chkRelAlarmOut12.Checked = ((int)msgHandle.byRelAlarmOut[11] == 1 ? true : false);
            chkRelAlarmOut13.Checked = ((int)msgHandle.byRelAlarmOut[12] == 1 ? true : false);
            chkRelAlarmOut14.Checked = ((int)msgHandle.byRelAlarmOut[13] == 1 ? true : false);
            chkRelAlarmOut15.Checked = ((int)msgHandle.byRelAlarmOut[14] == 1 ? true : false);
            chkRelAlarmOut16.Checked = ((int)msgHandle.byRelAlarmOut[15] == 1 ? true : false);
            chkRecordChannel01.Checked = ((int)msgHandle.byRecordChannel[0] == 1 ? true : false);
            chkRecordChannel02.Checked = ((int)msgHandle.byRecordChannel[1] == 1 ? true : false);
            chkRecordChannel03.Checked = ((int)msgHandle.byRecordChannel[2] == 1 ? true : false);
            chkRecordChannel04.Checked = ((int)msgHandle.byRecordChannel[3] == 1 ? true : false);
            chkRecordChannel05.Checked = ((int)msgHandle.byRecordChannel[4] == 1 ? true : false);
            chkRecordChannel06.Checked = ((int)msgHandle.byRecordChannel[5] == 1 ? true : false);
            chkRecordChannel07.Checked = ((int)msgHandle.byRecordChannel[6] == 1 ? true : false);
            chkRecordChannel08.Checked = ((int)msgHandle.byRecordChannel[7] == 1 ? true : false);
            chkRecordChannel09.Checked = ((int)msgHandle.byRecordChannel[8] == 1 ? true : false);
            chkRecordChannel10.Checked = ((int)msgHandle.byRecordChannel[9] == 1 ? true : false);
            chkRecordChannel11.Checked = ((int)msgHandle.byRecordChannel[10] == 1 ? true : false);
            chkRecordChannel12.Checked = ((int)msgHandle.byRecordChannel[11] == 1 ? true : false);
            chkRecordChannel13.Checked = ((int)msgHandle.byRecordChannel[12] == 1 ? true : false);
            chkRecordChannel14.Checked = ((int)msgHandle.byRecordChannel[13] == 1 ? true : false);
            chkRecordChannel15.Checked = ((int)msgHandle.byRecordChannel[14] == 1 ? true : false);
            chkRecordChannel16.Checked = ((int)msgHandle.byRecordChannel[15] == 1 ? true : false);
        }

        /// <summary>
        /// 报警配置信息显示
        /// </summary>
        /// <param name="alarmConfig"></param>
        private void setDataToControl(DH_ALARMIN_CFG alarmConfig)
        { 
            try
            {
                chkAlarmEn.Checked = (alarmConfig.byAlarmEn == 1 ? true : false);
                cmbAlarmType.SelectedIndex = alarmConfig.byAlarmType;
                setActionMask(alarmConfig.struHandle);
                cmbAWeeks.SelectedIndex = -1;
                cmbAWeeks.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }
        }

        /// <summary>
        /// 图像遮挡报警配置信息显示
        /// </summary>
        /// <param name="alarmConfig"></param>
        private void setDataToControl(DH_BLIND_CFG alarmConfig)
        {
            try
            {
                chkAlarmEn.Checked = (alarmConfig.byBlindEnable == 1 ? true : false);
                txtSenseLevel.Text = alarmConfig.byBlindLevel.ToString();
                setActionMask(alarmConfig.struHandle);
                cmbAWeeks.SelectedIndex = -1;
                cmbAWeeks.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }
        }


        /// <summary>
        /// 视频丢失报警配置信息显示
        /// </summary>
        /// <param name="alarmConfig"></param>
        private void setDataToControl(DH_VIDEO_LOST_CFG alarmConfig)
        {
            try
            {
                chkAlarmEn.Checked = (alarmConfig.byAlarmEn == 1 ? true : false);
                setActionMask(alarmConfig.struHandle);
                cmbAWeeks.SelectedIndex = -1;
                cmbAWeeks.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }
        }


        /// <summary>
        /// 动态检测报警配置信息显示
        /// </summary>
        /// <param name="alarmConfig"></param>
        private void setDataToControl(DH_MOTION_DETECT_CFG alarmConfig)
        {
            try
            {
                /*******************************************
                 * 此处需要根据动态检测区域结构体中的:动态检
                 * 测区域的行数和列数做相应的处理
                 * -------本例程中没有作相应的体现-------
                 *******************************************/
                string strTemp ;
                string strValue;
                chkAlarmEn.Checked = (alarmConfig.byMotionEn == 1 ? true : false);
                txtSenseLevel.Text = alarmConfig.wSenseLevel.ToString();
                foreach (Control bt in grpDetected.Controls)
                {
                    if (bt.GetType() == typeof(Button))
                    {
                        strTemp = ((Button)bt).Tag.ToString().Substring(0, 2);
                        strValue = alarmConfig.byDetected[int.Parse(((Button)bt).Tag.ToString().Substring(0, 1), System.Globalization.NumberStyles.AllowHexSpecifier)].Detected[int.Parse(((Button)bt).Tag.ToString().Substring(1, 1), System.Globalization.NumberStyles.AllowHexSpecifier)].ToString();
                        switch (strValue)
                        {
                            case "1"://有效
                                ((Button)bt).BackColor = Color.White;
                                break;
                            case "0"://无效
                                ((Button)bt).BackColor = Color.Gray;
                                break;
                        }
                        ((Button)bt).Tag = strTemp + strValue;
                    }
                }
                setActionMask(alarmConfig.struHandle);
                cmbAWeeks.SelectedIndex = -1;
                cmbAWeeks.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }
        }


        #endregion

        /// <summary>
        /// 通道号选择处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbChannelNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChannelNum.Items.Count > 0 && channelConfig.Equals(new DHDEV_CHANNEL_CFG()) == false && cmbChannelNum.SelectedIndex != -1)
            {
                setDataToControl(channelConfig[cmbChannelNum.SelectedIndex]);
            }

        }

        /// <summary>
        /// 增益使能时间段0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkGainEn_CheckedChanged(object sender, EventArgs e)
        {
            txtGain0.Enabled = chkGainEn0.Checked;
        }

        /// <summary>
        /// 增益使能时间段1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkGainEn1_CheckedChanged(object sender, EventArgs e)
        {
            txtGain1.Enabled = chkGainEn1.Checked;
        }
        /// <summary>
        /// 码流选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbVideoEncOpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChannelNum.Items.Count > 0 && cmbChannelNum.SelectedIndex != -1 && channelConfig.Equals(new DHDEV_CHANNEL_CFG()) == false && cmbVideoEncOpt.SelectedIndex != -1)
            {
                if (cmbVideoEncOpt.SelectedIndex < 3)
                {
                    VideoEncOptSelectChange(channelConfig[cmbChannelNum.SelectedIndex].stMainVideoEncOpt[cmbVideoEncOpt.SelectedIndex]);
                }
                else
                {
                    VideoEncOptSelectChange(channelConfig[cmbChannelNum.SelectedIndex].stAssiVideoEncOpt[cmbVideoEncOpt.SelectedIndex - 3]);
                }
            }


        }

        /// <summary>
        /// 码流选择处理
        /// </summary>
        /// <param name="videoOpt"></param>
        private void VideoEncOptSelectChange(DH_VIDEOENC_OPT videoOpt)
        {
            try
            {
                chkVideoEnable.Checked = (videoOpt.byVideoEnable == 1 ? true : false);//视频使能
                chkAudioEnable.Checked = (videoOpt.byAudioEnable == 1 ? true : false);//音频使能
                cmbBitRateControl.SelectedIndex = videoOpt.byBitRateControl;//码流控制
                cmbFramesPerSec.SelectedIndex = videoOpt.byFramesPerSec;//帧率
                cmbEncodeMode.SelectedIndex = videoOpt.byEncodeMode;//解码模式
                cmbImageSize.SelectedIndex = videoOpt.byImageSize;//分辨率
                cmbImageQlty.SelectedIndex = videoOpt.byImageQlty - 1;//画质[1-6转成SelectIndex时要减1]
                cmbFormatTag.SelectedIndex = videoOpt.wFormatTag;//音频编码
                txtChannels.Text = videoOpt.nChannels.ToString("D");//声道数
                txtSamplesPerSec.Text = videoOpt.nSamplesPerSec.ToString("D");//采样率
                txtBitsPerSampl.Text = videoOpt.wBitsPerSample.ToString("D");//采样深度
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }

        }

        /// <summary>
        /// OSD类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOSD_SelectedIndexChanged(object sender, EventArgs e)
        {
            labBlindEnable.Visible = false;
            cmbBlindEnable.Visible = false;
            if (cmbChannelNum.Items.Count > 0 && cmbChannelNum.SelectedIndex != -1 && channelConfig.Equals(new DHDEV_CHANNEL_CFG()) == false && cmbOSD.SelectedIndex != -1)
            {
                DH_ENCODE_WIDGET encodeWidGet;
                switch (cmbOSD.SelectedIndex)
                {
                    case 1://TimeOSD
                        encodeWidGet = channelConfig[cmbChannelNum.SelectedIndex].stTimeOSD;
                        break;
                    case 0://ChannelNameOSD
                        encodeWidGet = channelConfig[cmbChannelNum.SelectedIndex].stChannelOSD;
                        break;
                    case 2://BlindCover
                        labBlindEnable.Visible = true;
                        cmbBlindEnable.Visible = true;
                        encodeWidGet = channelConfig[cmbChannelNum.SelectedIndex].stBlindCover[0];
                        cmbBlindEnable.SelectedIndex = (int)channelConfig[cmbChannelNum.SelectedIndex].byBlindEnable;
                        break;
                    default:
                        encodeWidGet = channelConfig[cmbChannelNum.SelectedIndex].stChannelOSD;
                        break;
                }
                OSDSelectChange(encodeWidGet);
            }
        }
        /// <summary>
        /// OSD类型选择处理
        /// </summary>
        /// <param name="encodeWidGet"></param>
        private void OSDSelectChange(DH_ENCODE_WIDGET encodeWidGet)
        {
            try
            {
                chkShow.Checked = (encodeWidGet.bShow == 1 ? true : false);
                string rgbaValue = "";
                //前景色
                rgbaValue = encodeWidGet.rgbaFrontground.ToString("X");
                rgbaValue = "00000000".Remove(0, rgbaValue.Length) + rgbaValue;
                txtFrontgroundR.Text = int.Parse(rgbaValue.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
                txtFrontgroundG.Text = int.Parse(rgbaValue.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
                txtFrontgroundB.Text = int.Parse(rgbaValue.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
                txtFrontgroundA.Text = int.Parse(rgbaValue.Substring(6, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
                //背景色
                rgbaValue = encodeWidGet.rgbaBackground.ToString("X");
                rgbaValue = "00000000".Remove(0, rgbaValue.Length) + rgbaValue;
                txtBackgroundR.Text = int.Parse(rgbaValue.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
                txtBackgroundG.Text = int.Parse(rgbaValue.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
                txtBackgroundB.Text = int.Parse(rgbaValue.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
                txtBackgroundA.Text = int.Parse(rgbaValue.Substring(6, 2), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
                //边距
                txtLeft.Text = encodeWidGet.rcRect.left.ToString();
                txtRight.Text = encodeWidGet.rcRect.right.ToString();
                txtTop.Text = encodeWidGet.rcRect.top.ToString();
                txtBottom.Text = encodeWidGet.rcRect.bottom.ToString();
            }
            catch
            {
                MessageBox.Show("赋值错误！", pMsgTitle);
            }

        }
        /// <summary>
        /// 保存配置信息按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            bool returnValue = false;
            switch (tbcMain.SelectedIndex)
            {
                case 0://设备属性
                    #region << 保存可写属性设置值 >>
                    sysAttrConfig.wDevNo = ushort.Parse(txtDevNo.Text);//设备号
                    sysAttrConfig.byOverWrite = (byte)cmbOverWrite.SelectedIndex;//硬盘满处理方式
                    sysAttrConfig.byRecordLen = (byte)int.Parse(txtRecordLen.Text);//录像打包长度
                    sysAttrConfig.byDateFormat = (byte)cmbDateFormat.SelectedIndex;//日期格式
                    sysAttrConfig.byDateSprtr = (byte)cmbDateSprtr.SelectedIndex;//日期分割符
                    sysAttrConfig.byTimeFmt = (byte)cmbTimeFmt.SelectedIndex;//时间格式
                    #endregion
                    returnValue = DHClient.DHSetDevConfig(pLoginID, sysAttrConfig);
                    break;
                case 1://图像通道属性
                    cmbChannelNum_DropDown(null, null);//保存通道设置信息
                    returnValue = DHClient.DHSetDevConfig(pLoginID, channelConfig);
                    break;
                case 2://串口属性                   
                    cmbRS232_DropDown(null, null);//232串口信息保存
                    cmb485_DropDown(null, null);//485串口信息保存
                    returnValue = DHClient.DHSetDevConfig(pLoginID,commConfig);
                    break;
                case 3://录像属性
                    cmbRecChannel_DropDown(null, null);
                    cmbWeeks_DropDown(null, null);
                    returnValue = DHClient.DHSetDevConfig(pLoginID, recConfig);
                    break;
                case 4://网络配置
                    #region << 保存可写属性设置值 >>
                    DHClient.DHStringToByteArry(txtDevName.Text, ref  netConfig.sDevName);
                    netConfig.wTcpMaxConnectNum = (ushort)int.Parse(txtTcpLinkNum.Text);//TCP最大连接数
                    netConfig.wTcpPort = (ushort)int.Parse(txtTcpPort.Text);//TCP端口
                    netConfig.wUdpPort = (ushort)int.Parse(txtUDPPort.Text);//UDP端口
                    netConfig.wHttpPort = (ushort)int.Parse(txtHTTPPort.Text);//HTTP端口
                    netConfig.wHttpsPort = (ushort)int.Parse(txtHTTPSPort.Text);//HTTPS端口
                    netConfig.wSslPort = (ushort)int.Parse(txtSSLPort.Text);//SSL端口
                    //保存邮件信息
                    DHClient.DHStringToByteArry(txtMailBCCAdd.Text, ref netConfig.struMail.sBccAddr);
                    DHClient.DHStringToByteArry(txtMailCCAdd.Text, ref netConfig.struMail.sCcAddr);
                    DHClient.DHStringToByteArry(txtMailPassword.Text, ref netConfig.struMail.sUserPsw);
                    netConfig.struMail.wMailPort = ushort.Parse(txtMailPort.Text);
                    DHClient.DHStringToByteArry(txtMailReceiveAdd.Text, ref  netConfig.struMail.sDestAddr);
                    DHClient.DHStringToByteArry(txtMailSendAdd.Text, ref  netConfig.struMail.sSenderAddr);
                    DHClient.DHStringToByteArry(txtMailSubject.Text, ref  netConfig.struMail.sSubject);                    
                    DHClient.DHStringToByteArry(txtMailUserName.Text, ref netConfig.struMail.sUserName);
                    DHClient.DHStringToByteArry(txtMailIPAdd.Text, ref netConfig.struMail.sMailIPAddr);
                    //以太网口信息数据保存
                    cmbNetIONum_DropDown(null, null);
                    //远程主机信息数据保存
                    cmbRemohost_DropDown(null, null);
                    #endregion
                    returnValue = DHClient.DHSetDevConfig(pLoginID, netConfig);
                    break;
                case 5://报警属性
                    cmbAlarm_DropDown(null, null);
                    cmbAlarmInOrChannel_DropDown(null, null);
                    returnValue = DHClient.DHSetDevConfig(pLoginID, alarmAllConfig);
                    break;
            }
            if (returnValue == true)
            {
                //报设定成功消息
                MessageBox.Show("设置成功！", pMsgTitle);
            }
            else
            {
                if (DHClient.LastOperationInfo.errCode != "0")
                {
                    //报最后一次操作的错误信息
                    MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                }
            }
        }

        /// <summary>
        /// 只允许输入数字的控件按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] chrAccept = new char[1];
            chrAccept[0] = (char)Keys.Back;
            DHKeyPressSet('0', '9', chrAccept, e);

        }

        #region << 画面控件按键控制与业务无关 >>
        /// <summary>
        /// 控制画面控制只能按下数字键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DHKeyPressSet(char chrFrom, char chrTo, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= chrFrom && e.KeyChar <= chrTo) == false)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 控制画面控制只能按下数字键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DHKeyPressSet(char[] chrAccept, KeyPressEventArgs e)
        {
            foreach (char chr in chrAccept)
            {
                if (e.KeyChar == chr)
                {
                    return;
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// 控制画面控制只能按下数字键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DHKeyPressSet(char chrFrom, char chrTo, char[] chrAccept, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= chrFrom && e.KeyChar <= chrTo) == true)
            {
                return;
            }
            foreach (char chr in chrAccept)
            {
                if (e.KeyChar == chr)
                {
                    return;
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// 控制画面控制只能按下数字键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DHKeyPressSet(Keys[] keysAccept, KeyPressEventArgs e)
        {
            char[] chrAccept = new char[keysAccept.Length];
            for (int i = 0; i < keysAccept.Length; i++)
            {
                chrAccept[i] = (char)keysAccept[i];
            }
            DHKeyPressSet(chrAccept, e);
        }

        /// <summary>
        /// 控制画面控制只能按下数字键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DHKeyPressSet(char chrFrom, char chrTo, Keys[] keysAccept, KeyPressEventArgs e)
        {
            char[] chrAccept = new char[keysAccept.Length];
            for (int i = 0; i < keysAccept.Length; i++)
            {
                chrAccept[i] = (char)keysAccept[i];
            }
            DHKeyPressSet(chrFrom, chrTo, chrAccept, e);
        }

        #endregion

        /// <summary>
        /// 以太网口选择处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbNetIONum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (netConfig.Equals(new DHDEV_NET_CFG()) == false && cmbNetIONum.SelectedIndex != -1)
            {
                NetIONumSelect(netConfig.stEtherNet[cmbNetIONum.SelectedIndex]);
            }
        }
        /// <summary>
        /// 以太网口选择处理
        /// </summary>
        /// <param name="ethernetConfig"></param>
        private void NetIONumSelect(DH_ETHERNET ethernetConfig)
        {
            txtIPAdd.Text = DHClient.DHByteArrayToString(ethernetConfig.sDevIPAddr);
            txtMaskAdd.Text = DHClient.DHByteArrayToString(ethernetConfig.sDevIPMask);
            txtGatewayIP.Text = DHClient.DHByteArrayToString(ethernetConfig.sGatewayIP);
            cmbNetInterface.SelectedIndex = (int)ethernetConfig.dwNetInterface;
            txtMacIP.Text = DHClient.DHByteArrayToString(ethernetConfig.byMACAddr);
        }
        /// <summary>
        /// 远程主机选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRemohost_SelectedIndexChanged(object sender, EventArgs e)
        {
            labIPorHostNameTitle.Visible = false;
            txtIPorHostName.Visible = false;
            labIPorHostNameTitle.Text = ":";
            txtIPorHostName.Text = "";
            if (netConfig.Equals(new DHDEV_NET_CFG()) == false && cmbRemohost.SelectedIndex != -1)
            {
                DH_REMOTE_HOST remoteHost = new DH_REMOTE_HOST();
                int selectIndex=cmbRemohost.SelectedIndex;
                #region << 控件有效无效处理 >>
                switch (selectIndex)
                {
                    case 0://报警服务器
                    case 1://日志服务器
                    case 2://SMTP服务器
                    case 3://多播组
                        chkHostEnable.Enabled = false;
                        txtHostIPAddr.Enabled = true;
                        txtHostPort.Enabled = true;
                        txtHostUserName.Enabled = false;
                        txtHostPassword.Enabled = false;
                        break;
                    case 4:////NFS服务器
                        chkHostEnable.Enabled = true;
                        txtHostIPAddr.Enabled = true;
                        txtHostPort.Enabled = true;
                        txtHostUserName.Enabled = true;
                        txtHostPassword.Enabled = true;
                        break;
                    case 5://PPPoE服务器
                        chkHostEnable.Enabled = true;
                        txtHostIPAddr.Enabled = false;
                        txtHostPort.Enabled = false;
                        txtHostUserName.Enabled = true;
                        txtHostPassword.Enabled = true;
                        break;
                    case 6://DDNS服务器
                        chkHostEnable.Enabled = true;
                        txtHostIPAddr.Enabled = true;
                        txtHostPort.Enabled = true;
                        txtHostUserName.Enabled = false;
                        txtHostPassword.Enabled = false;
                        break;
                    case 7://DNS服务器
                        chkHostEnable.Enabled = false;
                        txtHostIPAddr.Enabled = true;
                        txtHostPort.Enabled = false;
                        txtHostUserName.Enabled = false;
                        txtHostPassword.Enabled = false;
                        break;
                }
                #endregion
                
                switch (selectIndex)
                {
                    case 0://报警服务器
                        remoteHost = netConfig.struAlarmHost;
                        break;
                    case 1://日志服务器
                        remoteHost = netConfig.struLogHost;
                        break;
                    case 2://SMTP服务器
                        remoteHost = netConfig.struSmtpHost;
                        break;
                    case 3://多播组
                        remoteHost = netConfig.struMultiCast;
                        break;
                    case 4://NFS服务器
                        remoteHost = netConfig.struNfs;
                        break;
                    case 5://PPPoE服务器
                        remoteHost = netConfig.struPppoe;
                        labIPorHostNameTitle.Visible = true;
                        txtIPorHostName.Visible = true;
                        labIPorHostNameTitle.Text = "注册IP:";
                        txtIPorHostName.Text = DHClient.DHByteArrayToString(netConfig.sPppoeIP);
                        break;
                    case 6://DDNS服务器
                        remoteHost = netConfig.struDdns;
                        labIPorHostNameTitle.Visible = true;
                        txtIPorHostName.Visible = true;
                        labIPorHostNameTitle.Text = "DDNS主机名:";
                        txtIPorHostName.Text = DHClient.DHByteArrayToString(netConfig.sDdnsHostName);
                        break;
                    case 7://DNS服务器
                        remoteHost = netConfig.struDns;
                        break;
                }
                if (remoteHost.Equals(new DH_REMOTE_HOST()) == false)
                {
                    RemohostSelect(remoteHost);
                }

            }
        }
        /// <summary>
        /// 远程主机选择处理
        /// </summary>
        /// <param name="remoteHost"></param>
        private void RemohostSelect(DH_REMOTE_HOST remoteHost)
        {
            txtHostIPAddr.Text = DHClient.DHByteArrayToString(remoteHost.sHostIPAddr);
            txtHostPassword.Text = DHClient.DHByteArrayToString(remoteHost.sHostPassword);
            txtHostPort.Text = remoteHost.wHostPort.ToString();
            txtHostUserName.Text = DHClient.DHByteArrayToString(remoteHost.sHostUser);
            chkHostEnable.Checked = (remoteHost.byEnable == 1 ? true : false);
        }

        /// <summary>
        /// OSD类型选择前的数据保存处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOSD_DropDown(object sender, EventArgs e)
        {
            if (cmbOSD.SelectedIndex != -1)
            {
                switch (cmbOSD.SelectedIndex)
                {
                    case 1://TimeOSD
                        OSDSaveData(ref channelConfig[cmbChannelNum.SelectedIndex].stTimeOSD);
                        break;
                    case 0://ChannelNameOSD
                        OSDSaveData(ref channelConfig[cmbChannelNum.SelectedIndex].stChannelOSD);
                        break;
                    case 2://BlindCover
                        OSDSaveData(ref channelConfig[cmbChannelNum.SelectedIndex].stBlindCover[0]);
                        channelConfig[cmbChannelNum.SelectedIndex].byBlindEnable = (byte)cmbBlindEnable.SelectedIndex;
                        break;
                }
            }
        }

        /// <summary>
        /// OSD类型选择前的数据保存处理
        /// </summary>
        /// <param name="encodeWidGet"></param>
        private void OSDSaveData(ref DH_ENCODE_WIDGET encodeWidGet)
        {
            encodeWidGet.bShow = (byte)(chkShow.Checked == true ? 1 : 0);
            encodeWidGet.rcRect.left = int.Parse(txtLeft.Text);
            encodeWidGet.rcRect.top = int.Parse(txtTop.Text);
            encodeWidGet.rcRect.right = int.Parse(txtRight.Text);
            encodeWidGet.rcRect.bottom = int.Parse(txtBottom.Text);
            encodeWidGet.rgbaBackground = uint.Parse(StringToHexString(txtBackgroundR.Text) + 
                                                     StringToHexString(txtBackgroundG.Text) + 
                                                     StringToHexString(txtBackgroundB.Text) + 
                                                     StringToHexString(txtBackgroundA.Text), 
                                                     System.Globalization.NumberStyles.AllowHexSpecifier);
            encodeWidGet.rgbaFrontground = uint.Parse(StringToHexString(txtFrontgroundR.Text) +
                                                      StringToHexString(txtFrontgroundG.Text) +
                                                      StringToHexString(txtFrontgroundB.Text) +
                                                      StringToHexString(txtFrontgroundA.Text),
                                                      System.Globalization.NumberStyles.AllowHexSpecifier);

        }

        /// <summary>
        /// 将一个整数转换为标准备的16进制表示[格式:00]
        /// </summary>
        /// <param name="strValue">0-255的整数</param>
        /// <returns></returns>
        private string StringToHexString(string strValue)
        {
            try
            {
                string result = "";
                int value = int.Parse(strValue);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("不合法的字符串!", pMsgTitle);
                    return ""; 
                }
                result = value.ToString("X");
                result = "00".Remove(0, result.Length) + result;
                return result;

            }
            catch
            {
                MessageBox.Show("不合法的字符串!", pMsgTitle);
                return "";
            }
        }

        /// <summary>
        /// 码流选择前的数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbVideoEncOpt_DropDown(object sender, EventArgs e)
        {
            if (cmbVideoEncOpt.SelectedIndex != -1)
            {
                if (cmbVideoEncOpt.SelectedIndex < 3)
                {
                    VideEncOptSaveData(ref channelConfig[cmbChannelNum.SelectedIndex].stMainVideoEncOpt[cmbVideoEncOpt.SelectedIndex]);
                }
                else
                {
                    VideEncOptSaveData(ref channelConfig[cmbChannelNum.SelectedIndex].stAssiVideoEncOpt[cmbVideoEncOpt.SelectedIndex - 3]);
                }
            }

        }
        
        /// <summary>
        /// 码流选择前的数据保存处理
        /// </summary>
        private void VideEncOptSaveData(ref DH_VIDEOENC_OPT videoOpt)
        {
            try
            {
                videoOpt.byVideoEnable=(byte)(chkVideoEnable.Checked ==true? 1 : 0);//视频使能
                videoOpt.byAudioEnable = (byte)(chkAudioEnable.Checked == true ? 1 : 0);//音频使能
                videoOpt.byBitRateControl=(byte)cmbBitRateControl.SelectedIndex ;//码流控制
                videoOpt.byFramesPerSec=(byte)cmbFramesPerSec.SelectedIndex ;//帧率
                videoOpt.byEncodeMode = (byte)cmbEncodeMode.SelectedIndex;//解码模式
                videoOpt.byImageSize=(byte)cmbImageSize.SelectedIndex;//分辨率
                videoOpt.byImageQlty=(byte)(cmbImageQlty.SelectedIndex +1);//画质[1-6转成SelectIndex时要减1]
                videoOpt.wFormatTag=(byte)cmbFormatTag.SelectedIndex;//音频编码
                videoOpt.nChannels=ushort.Parse(txtChannels.Text);//声道数
                videoOpt.nSamplesPerSec=ushort.Parse(txtSamplesPerSec.Text);//采样率
                videoOpt.wBitsPerSample=ushort.Parse(txtBitsPerSampl.Text);//采样深度
            }
            catch
            {
                MessageBox.Show("保存错误！", pMsgTitle);
            }
        }

        /// <summary>
        /// 通道号选择前保存处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbChannelNum_DropDown(object sender, EventArgs e)
        {
            if (cmbChannelNum.SelectedIndex != -1)
            {
                ChannelNumSaveData(ref channelConfig[cmbChannelNum.SelectedIndex]);
                cmbVideoEncOpt_DropDown(null, null);
                cmbOSD_DropDown(null, null);
            }
        }

        /// <summary>
        /// 通道号选择前保存处理
        /// </summary>
        /// <param name="channelConfig"></param>
        private void ChannelNumSaveData(ref DHDEV_CHANNEL_CFG channelConfig)
        {
            try
            {
                DHClient.DHStringToByteArry(txtChannelName.Text ,ref channelConfig.szChannelName);
                channelConfig.stColorCfg[0].byBrightness=(byte)int.Parse(txtBrightness0.Text);//亮度
                channelConfig.stColorCfg[0].byContrast=(byte)int.Parse(txtContrast0.Text );//对比度
                channelConfig.stColorCfg[0].bySaturation=(byte)int.Parse(txtSaturation0.Text);//饱和度
                channelConfig.stColorCfg[0].byHue=(byte)int.Parse(txtHue0.Text);//色度
                channelConfig.stColorCfg[0].byGainEn = (byte)(chkGainEn0.Checked ==true ?1 : 0);//增益使能
                channelConfig.stColorCfg[0].byGain=(byte)int.Parse(txtGain0.Text);//增益
                channelConfig.stColorCfg[1].byBrightness = (byte)int.Parse(txtBrightness1.Text);//亮度
                channelConfig.stColorCfg[1].byContrast = (byte)int.Parse(txtContrast1.Text);//对比度
                channelConfig.stColorCfg[1].bySaturation = (byte)int.Parse(txtSaturation1.Text);//饱和度
                channelConfig.stColorCfg[1].byHue = (byte)int.Parse(txtHue1.Text);//色度
                channelConfig.stColorCfg[1].byGainEn = (byte)(chkGainEn1.Checked == true ? 1 : 0);//增益使能
                channelConfig.stColorCfg[1].byGain = (byte)int.Parse(txtGain1.Text);//增益
            }
            catch
            {
                MessageBox.Show("保存错误！", pMsgTitle);
            }
        }
        /// <summary>
        /// 以太网口选择前保存处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbNetIONum_DropDown(object sender, EventArgs e)
        {
            if (cmbNetInterface.SelectedIndex != -1)
            {
                NetIONumSaveData(ref netConfig.stEtherNet[cmbNetIONum.SelectedIndex]);
            }
        }
        /// <summary>
        /// 以太网口选择前保存处理
        /// </summary>
        /// <param name="ethernetConfig"></param>
        private void NetIONumSaveData(ref DH_ETHERNET ethernetConfig)
        {
           DHClient.DHStringToByteArry(txtIPAdd.Text,ref ethernetConfig.sDevIPAddr);
           DHClient.DHStringToByteArry(txtMaskAdd.Text,ref  ethernetConfig.sDevIPMask);
           DHClient.DHStringToByteArry(txtGatewayIP.Text,ref  ethernetConfig.sGatewayIP);
            ethernetConfig.dwNetInterface=(uint)cmbNetInterface.SelectedIndex;
            DHClient.DHStringToByteArry(txtMacIP.Text,ref ethernetConfig.byMACAddr);
        }

        /// <summary>
        /// 远程主机选择处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRemohost_DropDown(object sender, EventArgs e)
        {
            if (cmbRemohost.SelectedIndex != -1)
            {
                switch (cmbRemohost.SelectedIndex)
                {
                    case 0://报警服务器
                        RemohostSaveData(ref netConfig.struAlarmHost);
                        break;
                    case 1://日志服务器
                        RemohostSaveData(ref netConfig.struLogHost);
                        break;
                    case 2://SMTP服务器
                        RemohostSaveData(ref netConfig.struSmtpHost);
                        break;
                    case 3://多播组
                        RemohostSaveData(ref netConfig.struMultiCast);
                        break;
                    case 4://NFS服务器
                        RemohostSaveData(ref netConfig.struNfs);
                        break;
                    case 5://PPPoE服务器
                        RemohostSaveData(ref netConfig.struPppoe);
                        DHClient.DHStringToByteArry(txtIPorHostName.Text,ref netConfig.sPppoeIP);
                        break;
                    case 6://DDNS服务器
                        RemohostSaveData(ref netConfig.struDdns);
                        DHClient.DHStringToByteArry(txtIPorHostName.Text,ref netConfig.sDdnsHostName);
                        break;
                    case 7://DNS服务器
                        RemohostSaveData(ref netConfig.struDns);
                        break;
                }
            }

        }
        /// <summary>
        /// 远程主机选择处理
        /// </summary>
        /// <param name="remoteHost"></param>
        private void RemohostSaveData(ref DH_REMOTE_HOST remoteHost)
        {
            DHClient.DHStringToByteArry(txtHostIPAddr.Text, ref remoteHost.sHostIPAddr);
            DHClient.DHStringToByteArry(txtHostPassword.Text, ref remoteHost.sHostPassword);
            remoteHost.wHostPort = ushort.Parse(txtHostPort.Text);
            DHClient.DHStringToByteArry(txtHostUserName.Text,ref  remoteHost.sHostUser);
            remoteHost.byEnable = (byte)(chkHostEnable.Checked == true ? 1 : 0);
        }

        /// <summary>
        /// IP地址输入控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IPAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] chrAccept = new char[2];
            chrAccept[0] = (char)Keys.Back;
            chrAccept[1] = '.';//IP地址分隔符
            DHKeyPressSet('0', '9', chrAccept, e);
        }
        /// <summary>
        /// 232串口选择处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRS232_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRS232.SelectedIndex != -1 && commConfig.Equals(new DHDEV_COMM_CFG())==false)
            {
                setDataToControl(commConfig.st232[cmbRS232.SelectedIndex]);
            }
        }

        /// <summary>
        /// 解码器选择处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb485_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb485.SelectedIndex != -1 && commConfig.Equals(new DHDEV_COMM_CFG()) == false)
            {
                setDataToControl(commConfig.stDecoder[cmb485.SelectedIndex]);
            }
        }

        /// <summary>
        /// 232串口选择前的数据保存处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRS232_DropDown(object sender, EventArgs e)
        {
            if (cmbRS232.SelectedIndex != -1)
            {
                COMSaveData(ref commConfig.st232[cmbRS232.SelectedIndex]);
            }
        }

        /// <summary>
        /// 485串口选择前的数据保存处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb485_DropDown(object sender, EventArgs e)
        {
            if (cmb485.SelectedIndex != -1)
            {
                COMSaveData(ref commConfig.stDecoder[cmb485.SelectedIndex]);
            }
        }

        /// <summary>
        /// 232串口信息保存
        /// </summary>
        /// <param name="rs232Config"></param>
        private void COMSaveData(ref DH_RS232_CFG rs232Config)
        {
            try
            {
                rs232Config.struComm.byBaudRate = (byte)cmbCOMBaudRate.SelectedIndex;
                rs232Config.struComm.byDataBit = (byte)cmbCOMDataBit.SelectedIndex;
                rs232Config.struComm.byParity = (byte)cmbCOMParity.SelectedIndex;
                rs232Config.struComm.byStopBit = (byte)cmbCOMStopBit.SelectedIndex;
                rs232Config.byFunction = (byte)cmbCOMFunction.SelectedIndex;
            }
            catch
            {
                MessageBox.Show("保存错误！", pMsgTitle);
            }
        }
        /// <summary>
        /// 485串口信息保存
        /// </summary>
        /// <param name="rs232Config"></param>
        private void COMSaveData(ref DH_485_CFG rs485Config)
        {
            try
            {
                rs485Config.wDecoderAddress = (ushort)int.Parse(txt458Add.Text);
                rs485Config.struComm.byBaudRate = (byte)cmb458BaudRate.SelectedIndex;
                rs485Config.struComm.byDataBit = (byte)cmb458DataBit.SelectedIndex;
                rs485Config.struComm.byParity = (byte)cmb458Parity.SelectedIndex;
                rs485Config.struComm.byStopBit = (byte)cmb458StopBit.SelectedIndex;
                rs485Config.wProtocol = (ushort)cmb458DecProName.SelectedIndex;
            }
            catch
            {
                MessageBox.Show("保存错误！", pMsgTitle);
            }
        }
        /// <summary>
        /// 密码是否'*'显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEmailPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEmailPassword.Checked == true)
            {
                txtMailPassword.PasswordChar = '*';
            }
            else
            {
                txtMailPassword.PasswordChar =new char();
            }
        }

        private void cmbRecChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sysAttrConfig.Equals(new DHDEV_SYSTEM_ATTR_CFG()) == false && cmbRecChannel.SelectedIndex != -1)
            {
                setDataToControl(recConfig[cmbRecChannel.SelectedIndex]);
            }
        }

        private void cmbWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sysAttrConfig.Equals(new DHDEV_SYSTEM_ATTR_CFG()) == false && cmbRecChannel.SelectedIndex != -1　&& cmbWeeks.SelectedIndex!=-1)
            {
                setDataToControl(recConfig[cmbRecChannel.SelectedIndex].stSect[cmbWeeks.SelectedIndex]);
            }
        }

        /// <summary>
        /// 录像信息星期选择前数据保存处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWeeks_DropDown(object sender, EventArgs e)
        {
            if ( cmbRecChannel.SelectedIndex!=-1 &&  cmbWeeks.SelectedIndex != -1)
            {
                TSECTSaveDate(ref recConfig[cmbRecChannel.SelectedIndex].stSect[cmbWeeks.SelectedIndex]);
            }
        }
        /// <summary>
        ///  录像信息星期选择前数据保存处理
        /// </summary>
        /// <param name="recTsect"></param>
        private void TSECTSaveDate(ref DH_REC_TSECT recTsect)
        {
            
            //时间段一
            int intTimeIndex = 0;
            recTsect.sTSECT[intTimeIndex].bEnable = chkT1Enable.Checked;
            recTsect.sTSECT[intTimeIndex].iBeginHour = int.Parse(txtT1StartH.Text);
            recTsect.sTSECT[intTimeIndex].iBeginMin = int.Parse(txtT1StartM.Text);
            recTsect.sTSECT[intTimeIndex].iBeginSec = int.Parse(txtT1StartS.Text);
            recTsect.sTSECT[intTimeIndex].iEndHour = int.Parse(txtT1EndH.Text);
            recTsect.sTSECT[intTimeIndex].iEndMin = int.Parse(txtT1EndM.Text);
            recTsect.sTSECT[intTimeIndex].iEndSec = int.Parse(txtT1EndS.Text);
            //时间段二
            intTimeIndex = 1;
            recTsect.sTSECT[intTimeIndex].bEnable = chkT2Enable.Checked;
            recTsect.sTSECT[intTimeIndex].iBeginHour = int.Parse(txtT2StartH.Text);
            recTsect.sTSECT[intTimeIndex].iBeginMin = int.Parse(txtT2StartM.Text);
            recTsect.sTSECT[intTimeIndex].iBeginSec = int.Parse(txtT2StartS.Text);
            recTsect.sTSECT[intTimeIndex].iEndHour = int.Parse(txtT2EndH.Text);
            recTsect.sTSECT[intTimeIndex].iEndMin = int.Parse(txtT2EndM.Text);
            recTsect.sTSECT[intTimeIndex].iEndSec = int.Parse(txtT2EndS.Text);
            //时间段三
            intTimeIndex = 2;
            recTsect.sTSECT[intTimeIndex].bEnable = chkT3Enable.Checked;
            recTsect.sTSECT[intTimeIndex].iBeginHour = int.Parse(txtT3StartH.Text);
            recTsect.sTSECT[intTimeIndex].iBeginMin = int.Parse(txtT3StartM.Text);
            recTsect.sTSECT[intTimeIndex].iBeginSec = int.Parse(txtT3StartS.Text);
            recTsect.sTSECT[intTimeIndex].iEndHour = int.Parse(txtT3EndH.Text);
            recTsect.sTSECT[intTimeIndex].iEndMin = int.Parse(txtT3EndM.Text);
            recTsect.sTSECT[intTimeIndex].iEndSec = int.Parse(txtT3EndS.Text);
            //时间段四
            intTimeIndex = 3;
            recTsect.sTSECT[intTimeIndex].bEnable = chkT4Enable.Checked;
            recTsect.sTSECT[intTimeIndex].iBeginHour = int.Parse(txtT4StartH.Text);
            recTsect.sTSECT[intTimeIndex].iBeginMin = int.Parse(txtT4StartM.Text);
            recTsect.sTSECT[intTimeIndex].iBeginSec = int.Parse(txtT4StartS.Text);
            recTsect.sTSECT[intTimeIndex].iEndHour = int.Parse(txtT4EndH.Text);
            recTsect.sTSECT[intTimeIndex].iEndMin = int.Parse(txtT4EndM.Text);
            recTsect.sTSECT[intTimeIndex].iEndSec = int.Parse(txtT4EndS.Text);
            //时间段五
            intTimeIndex = 4;
            recTsect.sTSECT[intTimeIndex].bEnable = chkT5Enable.Checked;
            recTsect.sTSECT[intTimeIndex].iBeginHour = int.Parse(txtT5StartH.Text);
            recTsect.sTSECT[intTimeIndex].iBeginMin = int.Parse(txtT5StartM.Text);
            recTsect.sTSECT[intTimeIndex].iBeginSec = int.Parse(txtT5StartS.Text);
            recTsect.sTSECT[intTimeIndex].iEndHour = int.Parse(txtT5EndH.Text);
            recTsect.sTSECT[intTimeIndex].iEndMin = int.Parse(txtT5EndM.Text);
            recTsect.sTSECT[intTimeIndex].iEndSec = int.Parse(txtT5EndS.Text);
            //时间段六
            intTimeIndex = 5;
            recTsect.sTSECT[intTimeIndex].bEnable = chkT6Enable.Checked;
            recTsect.sTSECT[intTimeIndex].iBeginHour = int.Parse(txtT6StartH.Text);
            recTsect.sTSECT[intTimeIndex].iBeginMin = int.Parse(txtT6StartM.Text);
            recTsect.sTSECT[intTimeIndex].iBeginSec = int.Parse(txtT6StartS.Text);
            recTsect.sTSECT[intTimeIndex].iEndHour = int.Parse(txtT6EndH.Text);
            recTsect.sTSECT[intTimeIndex].iEndMin = int.Parse(txtT6EndM.Text);
            recTsect.sTSECT[intTimeIndex].iEndSec = int.Parse(txtT6EndS.Text);
        }

        /// <summary>
        /// 录像信息通道选择前的数据保存处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRecChannel_DropDown(object sender, EventArgs e)
        {
            if (cmbRecChannel.SelectedIndex != -1)
            {
                RecConfigSaveData(ref recConfig[cmbRecChannel.SelectedIndex]);
            }
        }
        /// <summary>
        /// 录像信息通道选择前的数据保存处理
        /// </summary>
        /// <param name="recCfg">录像配置信息</param>
        private void RecConfigSaveData(ref DHDEV_RECORD_CFG recCfg)
        {
            recCfg.byRedundancyEn = (byte)(chkRedundancyEn.Checked == true ? 1 : 0);
            recCfg.byPreRecordLen = (byte)int.Parse(txtPreRecordLen.Text);
            cmbWeeks_DropDown(null, null);
        }

        private void chkActionMask40_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbAlarm_SelectedIndexChanged(object sender, EventArgs e)
        { 
            int sltIndex=cmbAlarm.SelectedIndex;

            if (sltIndex != -1 & sysAttrConfig.Equals(new DHDEV_SYSTEM_ATTR_CFG()) == false)
            {
                int channelCount;
                cmbAlarmInOrChannel.Items.Clear();
                if (sltIndex == 0)
                {

                    labChannelOrAlarmIn.Text = "报警输入:";
                    labAlarmTypeOrSenseLevel.Text = "报警类型:";
                    labAlarmTypeOrSenseLevel.Visible = true;
                    cmbAlarmType.Visible = true;
                    txtSenseLevel.Visible = false;

                    channelCount = sysAttrConfig.byAlarmInNum;//报警输入数
                    for (int i = 1; i <= channelCount; i++)
                    {
                        cmbAlarmInOrChannel.Items.Add("报警输入 " + i.ToString());
                    }
                }
                else
                {

                    labChannelOrAlarmIn.Text = "通道:";
                    labAlarmTypeOrSenseLevel.Text = "灵敏度:";
                    cmbAlarmType.Visible = false;
                    if (sltIndex != 3)
                    {
                        labAlarmTypeOrSenseLevel.Visible = true;
                        txtSenseLevel.Visible = true;
                    }
                    else
                    {
                        labAlarmTypeOrSenseLevel.Visible = false;
                        txtSenseLevel.Visible = false;
                    }

                    channelCount = sysAttrConfig.byVideoCaptureNum;//视频口数量
                    for (int i = 1; i <= channelCount; i++)
                    {
                        cmbAlarmInOrChannel.Items.Add("通道 " + i.ToString());
                    }
                }
                chkActionMask01.Enabled = (sltIndex == 2 || cmbAlarm.SelectedIndex == 3 ? false : true);
                chkActionMask20.Enabled = (sltIndex == 1 ? false : true);
                chkActionMask10.Enabled = (sltIndex == 0 ? true : false);
                btnDetected.Visible = (sltIndex == 1 ? true : false);
                //当选择项不为动态检测时动态检测区域设置画面不显示
                if (sltIndex != 1)
                {
                    grpDetected.Visible = false;
                }
                //显示数据到画面
                switch (sltIndex)
                {
                    case 0://报警输入
                        setDataToControl(alarmAllConfig.struLocalAlmIn[0]);
                        cmbAlarmInOrChannel.SelectedIndex = 0;
                        break;
                    case 1://动态检测
                        setDataToControl(alarmAllConfig.struMotion[0]);
                        cmbAlarmInOrChannel.SelectedIndex = 0;
                        break;
                    case 2://视频丢失
                        setDataToControl(alarmAllConfig.struVideoLost[0]);
                        cmbAlarmInOrChannel.SelectedIndex = 0;
                        break;
                    case 3://视频遮挡
                        setDataToControl(alarmAllConfig.struBlind[0]);
                        cmbAlarmInOrChannel.SelectedIndex = 0;
                        break;

                }
            }
        }

        private void cmbAWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlarm.SelectedIndex != -1 & cmbAlarmInOrChannel.SelectedIndex != -1 & cmbAWeeks.SelectedIndex != -1)
            {
                switch (cmbAlarm.SelectedIndex)
                { 
                    case 0://报警输入
                        setDataToControl(alarmAllConfig.struLocalAlmIn[cmbAlarmInOrChannel.SelectedIndex].stSect[cmbAWeeks.SelectedIndex], "Alarm");
                        break;
                    case 1://动态检测
                        setDataToControl(alarmAllConfig.struMotion[cmbAlarmInOrChannel.SelectedIndex].stSect[cmbAWeeks.SelectedIndex], "Alarm");
                        break;
                    case 2://视频丢失
                        setDataToControl(alarmAllConfig.struVideoLost[cmbAlarmInOrChannel.SelectedIndex].stSect[cmbAWeeks.SelectedIndex], "Alarm");
                        break;
                    case 3://视频遮挡
                        setDataToControl(alarmAllConfig.struBlind[cmbAlarmInOrChannel.SelectedIndex].stSect[cmbAWeeks.SelectedIndex], "Alarm");
                        break;
                }
            }
        }

        private void cmbAlarmInOrChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbAlarm.SelectedIndex)
            {
                case 0://报警输入
                    setDataToControl(alarmAllConfig.struLocalAlmIn[cmbAlarmInOrChannel.SelectedIndex]);
                    break;
                case 1://动态检测
                    setDataToControl(alarmAllConfig.struMotion[cmbAlarmInOrChannel.SelectedIndex]);
                    break;
                case 2://视频丢失
                    setDataToControl(alarmAllConfig.struVideoLost[cmbAlarmInOrChannel.SelectedIndex]);
                    break;
                case 3://视频遮挡
                    setDataToControl(alarmAllConfig.struBlind[cmbAlarmInOrChannel.SelectedIndex]);
                    break;
            }
        }

        private void cmbAlarm_DropDown(object sender, EventArgs e)
        {
            if (cmbAlarm.SelectedIndex != -1)
            {
                switch (cmbAlarm.SelectedIndex)
                {
                    case 0://报警输入
                        AlarmSaveData(ref alarmAllConfig.struLocalAlmIn[cmbAlarmInOrChannel.SelectedIndex]);
                        break;
                    case 1://动态检测
                        AlarmSaveData(ref alarmAllConfig.struMotion[cmbAlarmInOrChannel.SelectedIndex]);
                        break;
                    case 2://视频丢失
                        AlarmSaveData(ref alarmAllConfig.struVideoLost[cmbAlarmInOrChannel.SelectedIndex]);
                        break;
                    case 3://视频遮挡
                        AlarmSaveData(ref alarmAllConfig.struBlind[cmbAlarmInOrChannel.SelectedIndex]);
                        break;
                }
            }
        }
        /// <summary>
        /// 保存报警输入信息
        /// </summary>
        /// <param name="AlarmConfig"></param>
        private void AlarmSaveData(ref DH_ALARMIN_CFG AlarmConfig)
        {
            cmbAWeeks_DropDown(null, null);
            AlarmConfig.byAlarmEn = (byte)(chkAlarmEn.Checked==true?1:0);
            AlarmConfig.byAlarmType =(byte) cmbAlarmType.SelectedIndex;
            AlarmSaveData(ref AlarmConfig.struHandle);
            
        }
        /// <summary>
        /// 保存视频丢失信息
        /// </summary>
        /// <param name="AlarmConfig"></param>
        private void AlarmSaveData(ref DH_VIDEO_LOST_CFG AlarmConfig)
        {
            cmbAWeeks_DropDown(null, null);
            AlarmConfig.byAlarmEn =(byte) (chkAlarmEn.Checked == true ? 1 : 0);
            AlarmSaveData(ref AlarmConfig.struHandle);
            
        }
        /// <summary>
        /// 保存视频遮挡信息
        /// </summary>
        /// <param name="AlarmConfig"></param>
        private void AlarmSaveData(ref DH_BLIND_CFG AlarmConfig)
        {
            cmbAWeeks_DropDown(null, null);
            AlarmConfig.byBlindEnable =(byte) (chkAlarmEn.Checked == true ? 1 : 0);
            AlarmConfig.byBlindLevel = (byte)(int.Parse(txtSenseLevel.Text));            
            AlarmSaveData(ref AlarmConfig.struHandle);
            
        }
        /// <summary>
        /// 保存动态检测信息
        /// </summary>
        /// <param name="AlarmConfig"></param>
        private void AlarmSaveData(ref DH_MOTION_DETECT_CFG AlarmConfig)
        {
            cmbAWeeks_DropDown(null, null);
            AlarmConfig.byMotionEn = (byte)(chkAlarmEn.Checked == true ? 1 : 0);
            AlarmConfig.wSenseLevel = (ushort)(int.Parse(txtSenseLevel.Text));            
            AlarmSaveData(ref AlarmConfig.struHandle);
            
        }

        private void AlarmSaveData(ref DH_MSG_HANDLE msgHandle)
        {
            msgHandle.dwActionMask =(uint) (msgHandle.dwActionMask | (chkActionMask01.Checked == true ? 0x00000001 : 0x00000000));
            msgHandle.dwActionMask = (uint)(msgHandle.dwActionMask | (chkActionMask02.Checked == true ? 0x00000002 : 0x00000000));
            msgHandle.dwActionMask = (uint)(msgHandle.dwActionMask | (chkActionMask08.Checked == true ? 0x00000008 : 0x00000000));
            msgHandle.dwActionMask = (uint)(msgHandle.dwActionMask | (chkActionMask10.Checked == true ? 0x00000010 : 0x00000000));
            msgHandle.dwActionMask = (uint)(msgHandle.dwActionMask | (chkActionMask20.Checked == true ? 0x00000020 : 0x00000000));
            msgHandle.dwActionMask = (uint)(msgHandle.dwActionMask | (chkActionMask40.Checked == true ? 0x00000040 : 0x00000000));
            msgHandle.byRecordChannel[0] = (byte)(chkRecordChannel01.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[1] = (byte)(chkRecordChannel02.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[2] = (byte)(chkRecordChannel03.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[3] = (byte)(chkRecordChannel04.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[4] = (byte)(chkRecordChannel05.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[5] = (byte)(chkRecordChannel06.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[6] = (byte)(chkRecordChannel07.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[7] = (byte)(chkRecordChannel08.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[8] = (byte)(chkRecordChannel09.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[9] = (byte)(chkRecordChannel10.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[10] = (byte)(chkRecordChannel11.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[11] = (byte)(chkRecordChannel12.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[12] = (byte)(chkRecordChannel13.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[13] = (byte)(chkRecordChannel14.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[14] = (byte)(chkRecordChannel15.Checked == true ? 1 : 0);
            msgHandle.byRecordChannel[15] = (byte)(chkRecordChannel16.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[0] = (byte)(chkRelAlarmOut01.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[1] = (byte)(chkRelAlarmOut02.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[2] = (byte)(chkRelAlarmOut03.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[3] = (byte)(chkRelAlarmOut04.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[4] = (byte)(chkRelAlarmOut05.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[5] = (byte)(chkRelAlarmOut06.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[6] = (byte)(chkRelAlarmOut07.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[7] = (byte)(chkRelAlarmOut08.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[8] = (byte)(chkRelAlarmOut09.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[9] = (byte)(chkRelAlarmOut10.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[10] = (byte)(chkRelAlarmOut11.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[11] = (byte)(chkRelAlarmOut12.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[12] = (byte)(chkRelAlarmOut13.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[13] = (byte)(chkRelAlarmOut14.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[14] = (byte)(chkRelAlarmOut15.Checked == true ? 1 : 0);
            msgHandle.byRelAlarmOut[15] = (byte)(chkRelAlarmOut16.Checked == true ? 1 : 0);
        }

        private void cmbAWeeks_DropDown(object sender, EventArgs e)
        {
            if (cmbAlarm.SelectedIndex != -1 & cmbAlarmInOrChannel.SelectedIndex != -1 & cmbAWeeks.SelectedIndex != -1)
            {
                switch (cmbAlarm.SelectedIndex)
                {
                    case 0://报警输入
                        AlarmSaveData(ref alarmAllConfig.struLocalAlmIn[cmbAlarmInOrChannel.SelectedIndex].stSect[cmbAWeeks.SelectedIndex]);
                        break;
                    case 1://动态检测
                        AlarmSaveData(ref alarmAllConfig.struMotion[cmbAlarmInOrChannel.SelectedIndex].stSect[cmbAWeeks.SelectedIndex]);
                        break;
                    case 2://视频丢失
                        AlarmSaveData(ref alarmAllConfig.struVideoLost[cmbAlarmInOrChannel.SelectedIndex].stSect[cmbAWeeks.SelectedIndex]);
                        break;
                    case 3://视频遮挡
                        AlarmSaveData(ref alarmAllConfig.struBlind[cmbAlarmInOrChannel.SelectedIndex].stSect[cmbAWeeks.SelectedIndex]);
                        break;
                }
            }
        }

        private void AlarmSaveData(ref DH_REC_TSECT recTsect)
        {
            //时间段一
            recTsect.sTSECT[0].bEnable = chkAT1Enable.Checked;
            recTsect.sTSECT[0].iBeginHour = int.Parse(txtAT1StartH.Text);
            recTsect.sTSECT[0].iBeginMin = int.Parse(txtAT1StartM.Text);
            recTsect.sTSECT[0].iBeginSec = int.Parse(txtAT1StartS.Text);
            recTsect.sTSECT[0].iEndHour = int.Parse(txtAT1EndH.Text);
            recTsect.sTSECT[0].iEndMin = int.Parse(txtAT1EndM.Text);
            recTsect.sTSECT[0].iEndSec = int.Parse(txtAT1EndS.Text);
            //时间段二
            recTsect.sTSECT[1].bEnable = chkAT2Enable.Checked;
            recTsect.sTSECT[1].iBeginHour = int.Parse(txtAT2StartH.Text);
            recTsect.sTSECT[1].iBeginMin = int.Parse(txtAT2StartM.Text);
            recTsect.sTSECT[1].iBeginSec = int.Parse(txtAT2StartS.Text);
            recTsect.sTSECT[1].iEndHour = int.Parse(txtAT2EndH.Text);
            recTsect.sTSECT[1].iEndMin = int.Parse(txtAT2EndM.Text);
            recTsect.sTSECT[1].iEndSec = int.Parse(txtAT2EndS.Text);
            //时间段三
            recTsect.sTSECT[2].bEnable = chkAT3Enable.Checked;
            recTsect.sTSECT[2].iBeginHour = int.Parse(txtAT3StartH.Text);
            recTsect.sTSECT[2].iBeginMin = int.Parse(txtAT3StartM.Text);
            recTsect.sTSECT[2].iBeginSec = int.Parse(txtAT3StartS.Text);
            recTsect.sTSECT[2].iEndHour = int.Parse(txtAT3EndH.Text);
            recTsect.sTSECT[2].iEndMin = int.Parse(txtAT3EndM.Text);
            recTsect.sTSECT[2].iEndSec = int.Parse(txtAT3EndS.Text);
            //时间段四
            recTsect.sTSECT[3].bEnable = chkAT4Enable.Checked;
            recTsect.sTSECT[3].iBeginHour = int.Parse(txtAT4StartH.Text);
            recTsect.sTSECT[3].iBeginMin = int.Parse(txtAT4StartM.Text);
            recTsect.sTSECT[3].iBeginSec = int.Parse(txtAT4StartS.Text);
            recTsect.sTSECT[3].iEndHour = int.Parse(txtAT4EndH.Text);
            recTsect.sTSECT[3].iEndMin = int.Parse(txtAT4EndM.Text);
            recTsect.sTSECT[3].iEndSec = int.Parse(txtAT4EndS.Text);
            //时间段五
            recTsect.sTSECT[4].bEnable = chkAT5Enable.Checked;
            recTsect.sTSECT[4].iBeginHour = int.Parse(txtAT5StartH.Text);
            recTsect.sTSECT[4].iBeginMin = int.Parse(txtAT5StartM.Text);
            recTsect.sTSECT[4].iBeginSec = int.Parse(txtAT5StartS.Text);
            recTsect.sTSECT[4].iEndHour = int.Parse(txtAT5EndH.Text);
            recTsect.sTSECT[4].iEndMin = int.Parse(txtAT5EndM.Text);
            recTsect.sTSECT[4].iEndSec = int.Parse(txtAT5EndS.Text);
            //时间段六
            recTsect.sTSECT[5].bEnable = chkAT6Enable.Checked;
            recTsect.sTSECT[5].iBeginHour = int.Parse(txtAT6StartH.Text);
            recTsect.sTSECT[5].iBeginMin = int.Parse(txtAT6StartM.Text);
            recTsect.sTSECT[5].iBeginSec = int.Parse(txtAT6StartS.Text);
            recTsect.sTSECT[5].iEndHour = int.Parse(txtAT6EndH.Text);
            recTsect.sTSECT[5].iEndMin = int.Parse(txtAT6EndM.Text);
            recTsect.sTSECT[5].iEndSec = int.Parse(txtAT6EndS.Text);
        }

        private void cmbAlarmInOrChannel_DropDown(object sender, EventArgs e)
        {
            if (cmbAlarmInOrChannel.SelectedIndex != -1)
            {
                switch (cmbAlarm.SelectedIndex)
                {
                    case 0://报警输入
                        AlarmSaveData(ref alarmAllConfig.struLocalAlmIn[cmbAlarmInOrChannel.SelectedIndex]);
                        break;
                    case 1://动态检测
                        AlarmSaveData(ref alarmAllConfig.struMotion[cmbAlarmInOrChannel.SelectedIndex]);
                        break;
                    case 2://视频丢失
                        AlarmSaveData(ref alarmAllConfig.struVideoLost[cmbAlarmInOrChannel.SelectedIndex]);
                        break;
                    case 3://视频遮挡
                        AlarmSaveData(ref alarmAllConfig.struBlind[cmbAlarmInOrChannel.SelectedIndex]);
                        break;
                }
            }
        }

        private void btnDetected_Click(object sender, EventArgs e)
        {
            grpDetected.Visible = !grpDetected.Visible;
        }

        private void btnDetectedSelect_Click(object sender, EventArgs e)
        {
            string strTemp = ((Button)sender).Tag.ToString().Substring(0,2);
            string strValue="";
            if (((Button)sender).Tag.ToString().Length >= 3)
            {
                strValue = ((Button)sender).Tag.ToString().Substring(2, 1);
            }
            else
            {
                strValue = "0";
            }
            switch (strValue)
            { 
                case "0"://无效
                    strValue = "1";//有效
                    ((Button)sender).BackColor = Color.White;
                    break;
                case "1"://有效
                    strValue = "0";
                    ((Button)sender).BackColor = Color.Gray ;
                    break;
            }
            alarmAllConfig.struMotion[cmbAlarmInOrChannel.SelectedIndex].byDetected[int.Parse(strTemp.Substring(0, 1), System.Globalization.NumberStyles.AllowHexSpecifier)].Detected[int.Parse(strTemp.Substring(1, 1), System.Globalization.NumberStyles.AllowHexSpecifier)] = (byte)int.Parse(strValue);
            ((Button)sender).Tag = strTemp + strValue;
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

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fbdMain = new OpenFileDialog();
            
            if (fbdMain.ShowDialog() == DialogResult.OK)
            {
                textBoxFilePath.Text = fbdMain.FileName;
            }
        }

        private void buttonStartUpgrate_Click(object sender, EventArgs e)
        {
            if (0 != pLoginID)
            {
                if (textBoxFilePath.Text.Length == 0)
                {
                    MessageBox.Show("选择升级文件");
                    return;
                }
                if (hUpgradeId != 0)
                {
                    DHClient.DHCLIENT_StopUpgrade(hUpgradeId);
                    hUpgradeId = 0;
                }

                hUpgradeId = DHClient.DHCLIENT_StartUpgradeEx(pLoginID, EM_UPGRADE_TYPE.DH_UPGRADE_BIOS_TYPE, textBoxFilePath.Text, upgradeCallBack, 0);
                if (hUpgradeId != 0)
                {
                    if (DHClient.DHCLIENT_SendUpgrade(hUpgradeId))
                    {
                        MessageBox.Show("开始升级");
                    }
                }
            }
            else
            {
                MessageBox.Show("请先登录!");
            }
             
        }

        private void buttonStopUpgrate_Click(object sender, EventArgs e)
        {
            if (hUpgradeId != 0)
            {
                if (DHClient.DHCLIENT_StopUpgrade(hUpgradeId))
                {
                    MessageBox.Show("停止成功!");
                }
                hUpgradeId = 0;
            }
        }

        private void UpgradeCallBack(Int32 lLoginID, UInt32 lUpgradechannel, Int32 nTotalSize, Int32 nSendSize, UInt32 dwUser)
        {
            if (-1 == nSendSize)
            {
                this.progressBarUpdate.Invoke(this.updatePosDelegate, -1);
            }
            else if (-2 == nSendSize)
            {
                this.progressBarUpdate.Invoke(this.updatePosDelegate, -2);
            }
            else if (-1 == nTotalSize)
            {
                if(0 <= nSendSize && nSendSize <= 100)
                {
                    this.progressBarUpdate.Invoke(this.updatePosDelegate, nSendSize);
                }
            }
            else
            {
                int nPos = (int)((nSendSize * 100.0) / (nTotalSize * 1.0) );
                this.progressBarUpdate.Invoke(this.updatePosDelegate, nPos);
            }
        }

    }
}