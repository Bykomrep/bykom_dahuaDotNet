using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DHNetSDK;
using System.Runtime.InteropServices;
using Utility;      

namespace SDKDownLoadFileDemo
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
        private const string pMsgTitle = "网络SDK Demo程序";
        /// <summary>
        /// 最后操作信息显示格式
        /// </summary>
        private const string pErrInfoFormatStyle = "代码:errcode;\n描述:errmSG.";
        /// <summary>
        /// 用户信息
        /// </summary>
        private USER_MANAGE_INFO userManageInfo;
        /// <summary>
        /// 常规报警信息
        /// </summary>
        private NET_CLIENT_STATE clientState;
        /// <summary>
        /// 按文件下载句柄
        /// </summary>
        private int pDownloadHandleByFile;
        /// <summary>
        /// 按时间下载句柄
        /// </summary>
        private int pDownloadHandleByTime;
        /// <summary>
        /// 下载句柄
        /// </summary>
        private int pDownloadHandle;
        /// <summary>
        /// 断开回调
        /// </summary>
        private fDisConnect disConnect;
        /// <summary>
        /// 下载回调
        /// </summary>
        private fDownLoadPosCallBack downLoadFun;

        //private delegate void fTimeDownLoadPosCallBack(int lPlayHandle, UInt32 dwTotalSize, UInt32 dwDownLoadSize, int index, NET_RECORDFILE_INFO recordfileinfo, IntPtr dwUser);

        private fTimeDownLoadPosCallBack timeDownLoadFun;
        /// <summary>
        /// 下载进度百分比
        /// </summary>
        private double dblDownLoadPos;

        private delegate void fSetProgressPos(int lPlayHandle, UInt32 dwTotalSize, UInt32 dwDownLoadSize);

        private fSetProgressPos setProgressPos;

        private string strUserName = "test";

        private NET_DEVICEINFO deviceInfo;

        public frm_Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 画面加载初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            disConnect = new fDisConnect(DisConnectEvent);
            DHClient.DHInit(disConnect, IntPtr.Zero);
            downLoadFun = new fDownLoadPosCallBack(DownLoadPosFun);
            timeDownLoadFun = new fTimeDownLoadPosCallBack(TimeDownLoadPosFun);
            //timeDownLoadFun = new fDownLoadPosCallBack(TimeDownLoadPosFun);
            setProgressPos  = new fSetProgressPos(DownloadProgress);
            grbMain.Enabled = false;
            btnDownLoad1.Tag = "";
            btnDownLoad2.Tag = "";

            StringUtil.InitControlText(this);
        }
        /// <summary>
        /// 设备断开连接处理
        /// </summary>
        /// <param name="lLoginID">登录ID</param>
        /// <param name="pchDVRIP">DVR设备IP</param>
        /// <param name="nDVRPort">DVR设备端口</param>
        /// <param name="dwUser">用户数据</param>
        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            //设备断开连接处理          
            MessageUtil.ShowMsgBox(StringUtil.ConvertString("设备用户断开连接"),
                                   StringUtil.ConvertString(pMsgTitle));
        }
        /// <summary>
        /// 用户登录按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUserLogin_Click(object sender, EventArgs e)
        {
            string strBtn = btnUserLogin.Text;
            if (strBtn == Utility.StringUtil.ConvertString("设备用户登录"))
            {
                frm_AddDevice fLogin = new frm_AddDevice();
                fLogin.ShowDialog();
                if (fLogin.blnOKEnter == true)
                {
                    //设备用户信息获得
                    deviceInfo = new NET_DEVICEINFO();
                    int error = 0;
                    //设备用户登录
                    pLoginID = DHClient.DHLogin(fLogin.cmbDevIP.Text.ToString(), ushort.Parse(fLogin.txtDevProt.Text.ToString()), fLogin.txtName.Text.ToString(), fLogin.txtPassword.Text.ToString(), out deviceInfo, out error);
                    if (pLoginID != 0)
                    {
                        strUserName = fLogin.txtDevName.Text;
                        btnUserLogin.BackColor = Color.Yellow;
                        btnUserLogin.Text = StringUtil.ConvertString("设备用户注销");
                        grbMain.Enabled = true;
                    }
                    else
                    {
                        //报最后一次操作的错误信息
                        MessageUtil.ShowMsgBox(StringUtil.ConvertString(DHClient.LastOperationInfo.errMessage, "ErrorMessage"),
                                               StringUtil.ConvertString(pMsgTitle));
                    }
                }
            }
            else if (strBtn == Utility.StringUtil.ConvertString("设备用户注销"))
            {
                bool result = DHClient.DHLogout(pLoginID);
                if (result == false)
                {
                    //报最后一次操作的错误信息
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString(DHClient.LastOperationInfo.errMessage, "ErrorMessage"),
                                           StringUtil.ConvertString(pMsgTitle));
                }
                btnUserLogin.BackColor = Color.Transparent;
                btnUserLogin.Text = StringUtil.ConvertString("设备用户登录");
                grbMain.Enabled = false;

                Utility.StringUtil.InitControlText(this);
            }
        }

        /// <summary>
        /// 按文件下载按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownLoad1_Click(object sender, EventArgs e)
        {
            frm_PlayBackByFileSet fpf = new frm_PlayBackByFileSet();
            int pPlayBackChannelID = 0;
            NET_RECORDFILE_INFO fileInfo;
            fpf.gLoginID = pLoginID;
            fpf.txtDevName.Text = strUserName;
            fpf.cmbChannelSelect.Items.Clear();
            for (int i = 1; i <= deviceInfo.byChanNum; i++)
                fpf.cmbChannelSelect.Items.Add(i);
            fpf.ShowDialog(this);
            if (fpf.blnOKEnter == true)
            {
                pPlayBackChannelID = int.Parse(fpf.txtChannelID.Text.ToString());
                fileInfo = fpf.gFileInfo;
                //**********按文件下载**********
                if (txtDirPath1.Text.Trim().Length>0 && txtFileName1.Text.Trim().Length > 0)
                {
                    string strFileName = txtFileName1.Text;
                    strFileName = strFileName.ToLower();
                    if (!strFileName.EndsWith(".dav"))
                        strFileName += ".dav";

                    pDownloadHandleByFile = DHClient.DHDownloadByRecordFile(pLoginID, fileInfo, txtDirPath1.Text + @"\" + strFileName, downLoadFun, IntPtr.Zero);
                    if (pDownloadHandleByFile != 0)
                    {
                        btnDownLoad1.Tag = "下载中";
                        pDownloadHandle = pDownloadHandleByFile;
                        btnDownLoad2.Enabled = false;
                        btnDownLoad1.Enabled = false;
                        btnStopDownLoad1.Enabled = true;
                        MessageUtil.ShowMsgBox(StringUtil.ConvertString("开始下载!"),
                                               StringUtil.ConvertString(pMsgTitle));
                    }
                    else
                    {
                        MessageUtil.ShowMsgBox(StringUtil.ConvertString(DHClient.LastOperationInfo.errMessage, "ErrorMessage"),
                                               StringUtil.ConvertString(pMsgTitle));
                    }
                }
                else
                {
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString("请输入有效的录像保存目录和文件名!"),
                                           StringUtil.ConvertString(pMsgTitle));
                }
                
                //******************************                
            }
        }

        private void DownloadProgress(int lPlayHandle, UInt32 dwTotalSize, UInt32 dwDownLoadSize)
        {

            if (dwDownLoadSize != 0xFFFFFFFF &&
                dwDownLoadSize != 0xFFFFFFFE &&
                dwDownLoadSize <= dwTotalSize)
            {
                int iPos = (int)((dwDownLoadSize * 100) / dwTotalSize);
                Console.WriteLine(iPos.ToString() + " " + dwDownLoadSize.ToString() + "/" + dwTotalSize.ToString());
                psbMain.Value = iPos;
            }
            else
            {
                if (0xFFFFFFFF == dwDownLoadSize)
                {
                    btnDownLoad2.Tag = "";
                    psbMain.Value = 0;
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString("下载结束！"));
                    btnDownLoad1.Enabled = true;
                    btnDownLoad2.Enabled = true;

                    psbMain.Value = 0;

                    //DHClient.DHStopDownload(lPlayHandle);
                }
                else if (0xFFFFFFFE == dwDownLoadSize)
                {
                    btnDownLoad2.Tag = "";
                    psbMain.Value = 0;
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString("磁盘空间不足！"));
                    btnDownLoad1.Enabled = true;
                    btnDownLoad2.Enabled = true;

                    psbMain.Value = 0;

                    //DHClient.DHStopDownload(lPlayHandle);
                }
            }
        }

        /// <summary>
        /// 下载回调
        /// </summary>
        /// <param name="lPlayHandle">播放句柄</param>
        /// <param name="dwTotalSize">累计大小</param>
        /// <param name="dwDownLoadSize">下载大小</param>
        /// <param name="dwUser">用户数据</param>
        private void DownLoadPosFun(int lPlayHandle, UInt32 dwTotalSize, UInt32 dwDownLoadSize, IntPtr dwUser)
        {
            this.Invoke(setProgressPos, new object[]{lPlayHandle, dwTotalSize, dwDownLoadSize});

            if (0xFFFFFFFF == dwDownLoadSize || 0xFFFFFFFE == dwDownLoadSize)
            {
                DHClient.DHStopDownload(lPlayHandle);
            }
        }

        //private void TimeDownLoadPosFun(int lPlayHandle, UInt32 dwTotalSize, UInt32 dwDownLoadSize, int index, NET_RECORDFILE_INFO recordfileinfo, IntPtr dwUser)
        //private void TimeDownLoadPosFun(int lPlayHandle, UInt32 dwTotalSize, UInt32 dwDownLoadSize, IntPtr dwUser)
        public void TimeDownLoadPosFun(int lPlayHandle, int dwTotalSize, int dwDownLoadSize, int index
            , NET_RECORDFILE_INFO recordfileinfo, IntPtr dwUser)
        {
            this.Invoke(setProgressPos, new object[] { lPlayHandle, (UInt32)dwTotalSize, (UInt32)dwDownLoadSize });

            if (0xFFFFFFFF == dwDownLoadSize || 0xFFFFFFFE == dwDownLoadSize)
            {
                DHClient.DHStopDownload(lPlayHandle);
            }
        }

        /// <summary>
        /// 按文件下载目录选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDirSelect1_Click(object sender, EventArgs e)
        {
            if(fbdMain.ShowDialog()==DialogResult.OK)
            {
                txtDirPath1.Text = fbdMain.SelectedPath;
            }
        }
        /// <summary>
        /// 停止按文件下载按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopDownLoad1_Click(object sender, EventArgs e)
        {
            if (btnDownLoad1.Tag.ToString().Equals("下载中"))
            {
                bool blnStopSucced = DHClient.DHStopDownload(pDownloadHandleByFile);
                if (blnStopSucced)
                {
                    btnDownLoad1.Enabled = true;
                    btnDownLoad2.Enabled = true;
                    btnStopDownLoad1.Enabled = false;

                    MessageUtil.ShowMsgBox(StringUtil.ConvertString("停止下载成功！"),
                                           StringUtil.ConvertString(pMsgTitle));
                }
                else
                {
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString(DHClient.LastOperationInfo.errMessage, "ErrorMessage"),
                                           StringUtil.ConvertString(pMsgTitle));
                }
                btnDownLoad1.Tag = "";
                psbMain.Maximum = 100;
                psbMain.Value = 0;
            }
            else
            {
                MessageUtil.ShowMsgBox(StringUtil.ConvertString("当前没有下载任务!"),
                                       StringUtil.ConvertString(pMsgTitle));
            }
            
        }
        /// <summary>
        /// 获取按文件下载的下载进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetDownLoadPos1_Click(object sender, EventArgs e)
        {
            if (btnDownLoad1.Tag.ToString().Equals("下载中"))
            {
                int dwTotal=0;
                int dwSize=0;
                bool blnGetPosSucced = DHClient.DHGetDownloadPos(pDownloadHandleByFile, out dwTotal, out dwSize);
                if (blnGetPosSucced)
                {
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString("下载统计:") + "\n\t" +
                                           StringUtil.ConvertString("总长度:") +
                                           dwTotal.ToString() + "\n\t" + StringUtil.ConvertString("己下载:") + dwSize.ToString(),
                                           StringUtil.ConvertString(pMsgTitle));
                }
                else
                {
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString(DHClient.LastOperationInfo.errMessage, "ErrorMessage"),
                                           StringUtil.ConvertString(pMsgTitle));
                }
            }
            else
            {
                MessageUtil.ShowMsgBox(StringUtil.ConvertString("当前没有下载任务!"),
                                       StringUtil.ConvertString(pMsgTitle));
            }
        }
        /// <summary>
        /// 按时间下载按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownLoad2_Click(object sender, EventArgs e)
        {
            frm_PlayBackByTimeSet fPBSet = new frm_PlayBackByTimeSet();
            NET_RECORDFILE_INFO            fileInfo = new NET_RECORDFILE_INFO();
            int fileCount = 0;
            bool blnQueryRecordFile = false;
            int pPlayBackChannelID=0;
            fPBSet.txtDevName.Text = strUserName;
            fPBSet.cmbChannelSelect.Items.Clear();
            for (int i = 1; i <= deviceInfo.byChanNum; i++ )
            {
                fPBSet.cmbChannelSelect.Items.Add(i);
            }
            fPBSet.ShowDialog();
            if (fPBSet.blnOKEnter == true)
            {
                DateTime startTime = fPBSet.StartTime;
                DateTime endTime = fPBSet.EndTime;

                blnQueryRecordFile = DHClient.DHQueryRecordFile(pLoginID, int.Parse(fPBSet.txtChannelID.Text.ToString()), RECORD_FILE_TYPE.ALLRECORDFILE,
                                                                startTime, endTime, null, ref fileInfo, Marshal.SizeOf(typeof(NET_RECORDFILE_INFO)), out  fileCount, 5000, false);//按时间回放
                if (blnQueryRecordFile == true)
                {
                    //**********按文件下载**********
                    pPlayBackChannelID = int.Parse(fPBSet.txtChannelID.Text.ToString());
                    if (txtDirPath2.Text.Trim().Length > 0 && txtFileName2.Text.Trim().Length > 0)
                    {
                        string strFileName = txtFileName2.Text;
                        strFileName = strFileName.ToLower();
                        if (!strFileName.EndsWith(".dav"))
                            strFileName += ".dav";

                        // 关闭上次上次下载有关资源
                        if (pDownloadHandleByTime != 0)
                        {
                            DHClient.DHStopDownload(pDownloadHandleByTime);
                            pDownloadHandle = 0;
                        }

                        pDownloadHandleByTime = DHClient.DHDownloadByTime(pLoginID, pPlayBackChannelID, 0, startTime
                            , endTime, txtDirPath2.Text + @"\" + strFileName, timeDownLoadFun, IntPtr.Zero);
                        if (pDownloadHandleByTime != 0)
                        {
                            btnDownLoad2.Tag = "下载中";
                            pDownloadHandle = pDownloadHandleByTime;
                            btnDownLoad2.Enabled = false;
                            btnDownLoad1.Enabled = false;
                            btnStopDownLoad2.Enabled = true;
                            MessageUtil.ShowMsgBox(StringUtil.ConvertString("开始下载！"),
                                                   StringUtil.ConvertString(pMsgTitle));
                        }
                        else
                        {
                            MessageUtil.ShowMsgBox(StringUtil.ConvertString(DHClient.LastOperationInfo.errMessage, "ErrorMessage"),
                                                   StringUtil.ConvertString(pMsgTitle));
                        }
                    }
                    else
                    {
                        MessageUtil.ShowMsgBox(StringUtil.ConvertString("请输入有效的录像保存目录和文件名!"),
                                               StringUtil.ConvertString(pMsgTitle));
                    }
                    //*******************************
                }
                else
                {
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString("这个时间段里没有录像文件供下载!"),
                                           StringUtil.ConvertString(pMsgTitle));
                }
            }
        }
        /// <summary>
        /// 停止按时间下载按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopDownLoad2_Click(object sender, EventArgs e)
        {
            if (btnDownLoad2.Tag.ToString().Equals("下载中"))
            {
                bool blnStopSucced = DHClient.DHStopDownload(pDownloadHandleByTime);
                if (blnStopSucced)
                {
                    btnDownLoad1.Enabled = true;
                    btnDownLoad2.Enabled = true;
                    btnStopDownLoad2.Enabled = false;

                    MessageUtil.ShowMsgBox(StringUtil.ConvertString("停止下载成功！"),
                       StringUtil.ConvertString(pMsgTitle));
                }
                else
                {
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString(DHClient.LastOperationInfo.errMessage, "ErrorMessage"),
                                           StringUtil.ConvertString(pMsgTitle));
                }
                btnDownLoad2.Tag = "";
                psbMain.Maximum = 100;
                psbMain.Value = 0;
            }
            else
            {
                MessageUtil.ShowMsgBox(StringUtil.ConvertString("当前没有下载任务！"),
                                       StringUtil.ConvertString(pMsgTitle));
            }
        }
        /// <summary>
        /// 获取按时间下载的下载进度按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetDownLoadPos2_Click(object sender, EventArgs e)
        {
            if (btnDownLoad2.Tag.ToString().Equals("下载中"))
            {
                int dwTotal = 0;
                int dwSize = 0;
                bool blnGetPosSucced = DHClient.DHGetDownloadPos(pDownloadHandleByTime, out dwTotal, out dwSize);
                if (blnGetPosSucced)
                {
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString("下载统计:") + "\n\t" +
                                           StringUtil.ConvertString("总长度:") +
                                           dwTotal.ToString() + "\n\t" + StringUtil.ConvertString("己下载:") + dwSize.ToString(),
                                           StringUtil.ConvertString(pMsgTitle));
                }
                else
                {
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString(DHClient.LastOperationInfo.errMessage, "ErrorMessage"),
                                           StringUtil.ConvertString(pMsgTitle));
                }
            }
            else
            {
                MessageUtil.ShowMsgBox(StringUtil.ConvertString("当前没有下载任务！"), 
                                       StringUtil.ConvertString(pMsgTitle));
            }
        }
        /// <summary>
        /// 保存目录选择按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDirSelect_Click(object sender, EventArgs e)
        {
            if (fbdMain.ShowDialog() == DialogResult.OK)
            {
                txtDirPath2.Text = fbdMain.SelectedPath;
            }

        }
    }
}