using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DHPlaySDK;
using Utility;


namespace SDKPlayDemo
{
    public partial class frm_Main : Form
    {
        /// <summary>
        /// 程序消息提示Title
        /// </summary>
        private const string pMsgTitle = "大华PlaySDK Demo程序";
        /// <summary>
        /// 解码回调类型
        /// </summary>
        private STREAM_TYPE stmType = STREAM_TYPE.STREAM_WITHOUT;//无
        /// <summary>
        /// 播放数据回调
        /// </summary>
        private DecCBFun playCBFun;
        /// <summary>
        /// 保存图片路径
        /// </summary>
        private string strSavePicPath="";
        /// <summary>
        /// 视频大小模式
        /// </summary>
        private int pPlayVideoSizeMode = 0;
        /// <summary>
        /// 是否循环播放
        /// </summary>
        private bool blnLoop = false;

        #region << 字符叠加处理相关变量 >>
        /// <summary>
        /// 显示字符的正文
        /// </summary>
        private string pDisplayText;
        /// <summary>
        /// 字体格式，没有分开时间字体和显示字符字体，两者用同一字体
        /// </summary>
        private Font pFontSet;
        /// <summary>
        /// 字体绘制的格式刷
        /// </summary>
        private Brush pBrushSet;
        /// <summary>
        /// 显示字符的位置点
        /// </summary>
        private PointF pTextPointSet;
        /// <summary>
        /// 显示时间的位置点
        /// </summary>
        private PointF pTimePointSet;
        /// <summary>
        /// 是否显示时间
        /// </summary>
        private bool pShowTime;
        /// <summary>
        /// 是否显示自绘图
        /// </summary>
        private bool pShowDraw=false;
        /// <summary>
        /// 绘图格式
        /// </summary>
        private int pDrawStyle;
        #endregion
        /// <summary>
        /// 录像颜色信息保存
        /// </summary>
        private COLOR_STRUCT pColor = new COLOR_STRUCT();
        /// <summary>
        /// 自定义绘图回调(用于字符叠加)
        /// </summary>
        private DrawFun dFun;

        public frm_Main()
        {
            InitializeComponent();
        }
        private void frm_Main_Load(object sender, EventArgs e)
        {
            chkEnableSound.Checked = true;//判断是不是允许声音
            //字符叠加设置初始值
            pShowTime =true;
            pTextPointSet = new PointF(100,10);
            pTimePointSet = new PointF(200,30);
            pFontSet = this.Font;
            pBrushSet = new SolidBrush(Color.Red);
            pDisplayText = "";
            //设置画面控件可用属性
            SetOpenCloseFileControl(0);
            //播放数据回调设置
            playCBFun = new DecCBFun(pDecCBFun);
            SetCallBackTypeCheck(stmType);
            mnuSizeTrue_Click(null, null);
            //字符叠加设置
            dFun = new DrawFun(DrawFun);
            DHPlay.DHRigisterDrawFun(0, dFun, 0);//字符叠加功能演示

            StringUtil.InitControlText(this);
        }

        /// <summary>
        /// 音频允许设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableSound_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableSound.Checked)
            {
                chkEnableSound.Image=imlMain.Images["AUDIO_ENABLE"];                
                DHPlay.DHPlayControl(PLAY_COMMAND.PlaySound, 0);                
            }
            else
            {
                chkEnableSound.Image = imlMain.Images["AUDIO_DISABLE"];
                DHPlay.DHPlayControl(PLAY_COMMAND.StopSound);
            }
            trbSound.Enabled = chkEnableSound.Checked;
            mnuSoundUp.Enabled = trbSound.Enabled;
            mnuSoundDown.Enabled = trbSound.Enabled;
            mnuSoundSwitch.Checked = chkEnableSound.Checked;
            mnuSoundSwitch.Image = chkEnableSound.Image;
        }

        
        /// <summary>
        /// 打开录像文件菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuOpenFile_Click(object sender, EventArgs e)
        {
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                if (DHPlay.DHPlayControl(PLAY_COMMAND.OpenFile, 0, ofdMain.FileName))
                {
                        SetOpenCloseFileControl(1);                                                
                        stlTotalTime.Text = DHPlay.DHConvertToTime(DHPlay.DHPlayControl(PLAY_COMMAND.GetFileTotalTime, 0, true),1,"HH:mm:ss");
                        bool blnReturn=DHPlay.DHPlayControl(PLAY_COMMAND.GetColor,0,(uint)picPlayMain.Handle,ref pColor);
                        if (blnReturn)
                        {
                            hsbBrightness.Value = pColor.pBrightness;
                            hsbContrast.Value = pColor.pContrast;
                            hsbHue.Value = pColor.pHue;
                            hsbSaturation.Value = pColor.pSaturation;
                        }
                        trbSound.Value =(int) DHPlay.DHPlayControl(PLAY_COMMAND.GetVolume, 0,true);
                }
                else
                {
                    MessageBox.Show("打开文件失败！", pMsgTitle);
                }
            }
        }

        /// <summary>
        /// 播放录像菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuStart_Click(object sender, EventArgs e)
        {
            if (!DHPlay.DHPlayControl(PLAY_COMMAND.Start, 0, this.picPlayMain.Handle))            
            {
                MessageBox.Show("播放文件失败！", pMsgTitle);
            }
            else
            {
                SetPlayStopControl(1);
                chkEnableSound_CheckedChanged(sender, e);//处理声音开关6
            }
         
        }
        /// <summary>
        /// 关闭录像文件菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCloseFile_Click(object sender, EventArgs e)
        {
            if (!DHPlay.DHPlayControl(PLAY_COMMAND.CloseFile, 0))
            {
                MessageBox.Show("关闭文件失败！", pMsgTitle);
            }
            else
            {
                this.picPlayMain.Refresh();//刷新画面
                if (mnuStop.Enabled)
                {
                    mnuStop_Click(sender, e);
                }
                SetOpenCloseFileControl(0);
            }            
        }
        /// <summary>
        /// 退出菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 窗口关闭时的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //添加窗口关闭时的清理工作
            if (mnuStop.Enabled)
            {
                mnuStop_Click(sender, e);
            }
        }
        /// <summary>
        /// 播放暂停菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPause_Click(object sender, EventArgs e)
        {
            if (DHPlay.DHPlayControl(PLAY_COMMAND.Pause, 0))
            {
                SetPauseResumControl(1);
            }
            else
            {
                MessageBox.Show("暂停播放失败！", pMsgTitle);
            }            
        }
        /// <summary>
        /// 停止播放菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuStop_Click(object sender, EventArgs e)
        {
            if (!DHPlay.DHPlayControl(PLAY_COMMAND.Stop, 0))
            {
                MessageBox.Show("停止播放失败！", pMsgTitle);
            }
            else
            {
                SetPlayStopControl(0);
                trbPlayFrames.Value = 0;
                stlCurrentFrame.Text = "0";
                stlCurrentTime.Text = "00:00:00";
                this.picPlayMain.Refresh();//刷新画面
            }            
        }
        

        /// <summary>
        /// 播放/停止按钮处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartPlay_Click(object sender, EventArgs e)
        {
            if (btnStartPlay.Tag.ToString() == "Start")
            {
                mnuStart_Click(null, null);
            }
            else
            {
                mnuStop_Click(null, null);
            }
        }

        /// <summary>
        /// 播放回调函数
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="pBuf"></param>
        /// <param name="nSize"></param>
        /// <param name="pFrameInfo"></param>
        /// <param name="nReserved1"></param>
        /// <param name="nReserved2"></param>
        private void pDecCBFun(int nPort, ref String pBuf,int nSize, ref  FRAME_INFO pFrameInfo, int nReserved1, int nReserved2)
        {
            this.picPlayMain.Height = pFrameInfo.nHeight;
            this.picPlayMain.Width = pFrameInfo.nWidth;
            this.stlCurrentTime.Text = pFrameInfo.nStamp.ToString();

        }

        /// <summary>
        /// 继续播放菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuResum_Click(object sender, EventArgs e)
        {
            if (DHPlay.DHPlayControl(PLAY_COMMAND.ReSume, 0))
            {                
                SetPauseResumControl(0);
            }
            else
            {
                MessageBox.Show("继续播放失败！", pMsgTitle);
            }            
        }

        /// <summary>
        /// 暂停播放菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPauseResume_Click(object sender, EventArgs e)
        {
            if (btnPauseResume.Tag.ToString() == "Pause")
            {
                mnuPause_Click(null, null);
            }
            else
            {
                mnuResum_Click(null, null);
            }
        }

        /// <summary>
        /// 设置暂停/继续相关控件
        /// </summary>
        /// <param name="intPause">0:暂停有效;1:继续有效;2:全无效</param>
        private void SetPauseResumControl(int intPause)
        {
            switch (intPause)
            { 
                case 0:
                    mnuPause.Enabled = true;
                    btnPauseResume.Enabled = true;
                    mnuResum.Enabled = false;
                    btnPauseResume.Image = imlMain.Images["PAUSE_ENABLE"];
                    btnPauseResume.Tag = "Pause";
                    timerGetPlayInfo.Enabled = true;
                    break;
                case 1:
                    mnuPause.Enabled = false;
                    mnuResum.Enabled = true;
                    btnPauseResume.Image = imlMain.Images["RESUM_ENABLE"];
                    btnPauseResume.Tag = "Resum";
                    timerGetPlayInfo.Enabled = false;
                    break;
                case 2:
                    mnuPause.Enabled = false;
                    mnuResum.Enabled = false;
                    btnPauseResume.Image = imlMain.Images["PAUSE_ENABLE"];
                    btnPauseResume.Tag = "Pause";
                    btnPauseResume.Enabled = false;
                    timerGetPlayInfo.Enabled = false;
                    break;
            }
        }
        /// <summary>
        /// 设置播放/停止相关控件
        /// </summary>
        /// <param name="intPlay">0:播放有效;1:停止有效;2:全无效;3:播放有效(单帧播放不控制)</param>
        private void SetPlayStopControl(int intPlay)
        {
            switch (intPlay)
            { 
                case 0:
                    mnuStart.Enabled = true;
                    mnuStop.Enabled = false;
                    btnStartPlay.Enabled = true;
                    btnStartPlay.Tag = "Start";
                    btnStartPlay.Image = imlMain.Images["PLAY_ENABLE"];
                    SetPauseResumControl(2);
                    SetOtherControl(1);
                    SetStepControl(1);
                    mnuSetDisplayRegion.Enabled = false;
                    timerGetPlayInfo.Enabled = false;
                    trbPlayFrames.Enabled = false;                    
                    break;
                case 1:
                    mnuStart.Enabled = false;
                    mnuStop.Enabled = true;
                    btnStartPlay.Tag = "Stop";
                    btnStartPlay.Image = imlMain.Images["STOP_ENABLE"];
                    mnuSetDisplayRegion.Enabled = true;
                    trbPlayFrames.Enabled = true;
                    SetPauseResumControl(0);
                    SetOtherControl(0);
                    SetStepControl(0);
                    timerGetPlayInfo.Enabled = true;                    
                    break;
                case 2:
                    mnuStart.Enabled = false;
                    mnuStop.Enabled = false;
                    btnStartPlay.Tag = "Start";
                    btnStartPlay.Image = imlMain.Images["PLAY_ENABLE"];
                    btnStartPlay.Enabled = false;
                    SetPauseResumControl(2);
                    SetOtherControl(1);
                    SetStepControl(1);
                    trbPlayFrames.Enabled = false;
                    timerGetPlayInfo.Enabled = false;
                    break;
                case 3:
                    mnuStart.Enabled = true;
                    mnuStop.Enabled = false;
                    btnStartPlay.Enabled = true;
                    btnStartPlay.Tag = "Start";
                    btnStartPlay.Image = imlMain.Images["PLAY_ENABLE"];
                    SetPauseResumControl(2);
                    SetOtherControl(1);
                    timerGetPlayInfo.Enabled = true;                    
                    break;
            }
        }
        
        /// <summary>
        /// 设置打开/关闭文件相关控件
        /// </summary>
        /// <param name="intOpen">0:打开文件有效;1:关闭文件有效;2:剪切文件有效;3:全无效</param>
        private void SetOpenCloseFileControl(int intOpen)
        {
            switch (intOpen)
            { 
                case 0:
                    mnuOpenFile.Enabled = true;
                    mnuCloseFile.Enabled = false;
                    mnuCutFile.Enabled = false;
                    SetPlayStopControl(2);
                    break;
                case 1:
                    mnuCloseFile.Enabled = true;
                    mnuCutFile.Enabled = true;
                    mnuOpenFile.Enabled = false;
                    SetPlayStopControl(0);
                    break;
                case 2:
                    mnuCutFile.Enabled = true;                    
                    break;
                case 3:
                    mnuOpenFile.Enabled = false;
                    mnuCloseFile.Enabled = false;
                    mnuCutFile.Enabled = false;
                    break;
            }
        }
        /// <summary>
        /// 设置其他与播放有关的控件
        /// </summary>
        /// <param name="intStepNext">0:控件有效;1:控件无效</param>
        private void SetOtherControl(int intStepNext)
        {
            bool blnEnable = false;
            switch (intStepNext)
            { 
                case 0:
                    blnEnable = true;
                    break;
                case 1:
                    blnEnable = false;
                    break;
            }
            btnPlayFast.Enabled = blnEnable;
            btnPlaySlow.Enabled = blnEnable;
            btnFullScreen.Enabled = blnEnable;
            mnuLoop.Enabled = blnEnable;
            mnuSlow.Enabled = blnEnable;
            mnuFast.Enabled = blnEnable;            
            btnPlayFast.Image = imlMain.Images["FAST_ENABLE"];
            btnPlaySlow.Image = imlMain.Images["SLOW_ENABLE"];
        }
        /// <summary>
        /// 单帧播放控件相关
        /// </summary>
        /// <param name="intStep">0:控件有效;1:控件无效</param>
        private void SetStepControl(int intStep)
        {
            bool blnEnable = false;
            switch (intStep)
            {
                case 0:
                    blnEnable = true;
                    break;
                case 1:
                    blnEnable = false;
                    break;
            }
            btnStepBack.Enabled = blnEnable;
            btnStepOnByOne.Enabled = blnEnable;
            btnToBegin.Enabled = blnEnable;
            btnToEnd.Enabled = blnEnable;
            mnuStepBack.Enabled = blnEnable;
            mnuStepOneByOne.Enabled = blnEnable;
            mnuToBegin.Enabled = blnEnable;
            mnuToEnd.Enabled = blnEnable;
            mnuFullScreen.Enabled = blnEnable;
            mnuCatchPic.Enabled = blnEnable;
            btnCatchPic.Enabled = blnEnable;
            //声音相关控件
            chkEnableSound.Enabled = blnEnable;
            trbSound.Enabled = blnEnable;
            mnuSoundControl.Enabled = blnEnable;
            //颜色相关控件
            hsbBrightness.Enabled = blnEnable;
            hsbContrast.Enabled = blnEnable;
            hsbHue.Enabled = blnEnable;
            hsbSaturation.Enabled = blnEnable;
            mnuColorControl.Enabled = blnEnable;
            btnStepBack.Image = imlMain.Images["STEPBACK_ENABLE"];
            btnStepOnByOne.Image = imlMain.Images["STEP_ENABLE"];
            btnToBegin.Image = imlMain.Images["TOBEGIN_ENABLE"];
            btnToEnd.Image = imlMain.Images["TOEND_ENABLE"];
            btnCatchPic.Image = imlMain.Images["CATCHPIC_ENABLE"];
            btnFullScreen.Image = imlMain.Images["FULLSCREEN_ENABLE"];

        }

        /// <summary>
        /// 快放菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFast_Click(object sender, EventArgs e)
        {
            if (!DHPlay.DHPlayControl(PLAY_COMMAND.Fast,0))
            {
                MessageBox.Show("快放失败！", pMsgTitle);
            }
        }

        /// <summary>
        /// 慢放菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSlow_Click(object sender, EventArgs e)
        {
            if (!DHPlay.DHPlayControl(PLAY_COMMAND.Slow,0))
            {
                MessageBox.Show("慢放失败！", pMsgTitle);
            }
        }

        /// <summary>
        /// 单帧播放菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuStepOneByOne_Click(object sender, EventArgs e)
        {
            if (!DHPlay.DHPlayControl(PLAY_COMMAND.OneByOne, 0))
            {
                MessageBox.Show("单帧播放失败！", pMsgTitle);
            }
            else
            { 
                //播放/停止按钮为播放状态
                SetPlayStopControl(3);
                
            }
        }
        /// <summary>
        /// 单帧播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStepOnByOne_Click(object sender, EventArgs e)
        {
            mnuStepOneByOne_Click(sender, e);
        }
        /// <summary>
        /// 播放回调类型:无
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCallBack_WithOut_Click(object sender, EventArgs e)
        {
            stmType = STREAM_TYPE.STREAM_WITHOUT;
            SetCallBackTypeCheck(stmType);
        }

        /// <summary>
        /// 设置播放回调类型选择菜单
        /// </summary>
        /// <param name="stmTp"></param>
        private void SetCallBackTypeCheck(STREAM_TYPE stmTp)
        {
            bool blnChecked = false;
            mnuCallBack_Audio.Checked = blnChecked;
            mnuCallBack_Audio.Image = imlMain.Images["AUDIO_ENABLE"];
            mnuCallBack_Mix.Checked = blnChecked;
            mnuCallBack_Mix.Image = imlMain.Images["MIX_ENABLE"];
            mnuCallBack_Video.Checked = blnChecked;
            mnuCallBack_Video.Image = imlMain.Images["VIDEO_ENABLE"];
            mnuCallBack_WithOut.Checked = blnChecked;
            mnuCallBack_WithOut.Image = imlMain.Images["WITHOUT_ENABLE"];            
            blnChecked=true;
            switch (stmTp)
            { 
                case STREAM_TYPE.STREAM_WITHOUT:
                    mnuCallBack_WithOut.Checked = blnChecked;
                    mnuCallBack_WithOut.Image = imlMain.Images["CHECKED_ENABLE"];
                    break;
                case STREAM_TYPE.STREAM_AUDIO:
                    mnuCallBack_Audio.Checked = blnChecked;
                    mnuCallBack_Audio.Image = imlMain.Images["CHECKED_ENABLE"];
                    break;
                case STREAM_TYPE.STREAM_VIDEO:
                    mnuCallBack_Video.Checked = blnChecked;
                    mnuCallBack_Video.Image = imlMain.Images["CHECKED_ENABLE"];
                    break;
                case STREAM_TYPE.STREAM_MIX:
                    mnuCallBack_Mix.Checked = blnChecked;
                    mnuCallBack_Mix.Image = imlMain.Images["CHECKED_ENABLE"];
                    break;
            }
        }
        /// <summary>
        /// 解码回调流-音频菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCallBack_Audio_Click(object sender, EventArgs e)
        {
            stmType = STREAM_TYPE.STREAM_AUDIO;
            SetCallBackTypeCheck(stmType);
        }
        /// <summary>
        /// 解码回调流-视频菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCallBack_Video_Click(object sender, EventArgs e)
        {
            stmType = STREAM_TYPE.STREAM_VIDEO;
            SetCallBackTypeCheck(stmType);
        }
        /// <summary>
        /// 解码回调流-混合菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCallBack_Mix_Click(object sender, EventArgs e)
        {
            stmType = STREAM_TYPE.STREAM_MIX;
            SetCallBackTypeCheck(stmType);
        }

        /// <summary>
        /// 循环播放菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuLoop_Click(object sender, EventArgs e)
        {
            if (mnuLoop.Checked)
            {
                mnuLoop.Image = imlMain.Images["CHECKED_ENABLE"];                
            }
            else
            {
                mnuLoop.Image = imlMain.Images["LOOP_ENABLE"];
            }
            blnLoop = mnuLoop.Checked;
        }
        /// <summary>
        /// 全屏菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFullScreen_Click(object sender, EventArgs e)
        {
            stsMain.Visible = !mnuFullScreen.Checked;
            palMain.Visible = !mnuFullScreen.Checked;
            trbPlayFrames.Visible = !mnuFullScreen.Checked;
            groupBox1.Visible = !mnuFullScreen.Checked;
            
            musMain.Visible = !mnuFullScreen.Checked;
            if (mnuFullScreen.Checked)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                mnuFullScreen.Image = imlMain.Images["CHECKED_ENABLE"];
                picPlayMain.Parent = this;
                
                
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                mnuFullScreen.Image = imlMain.Images["FULLSCREEN_ENABLE"];
                picPlayMain.Parent = palMain;
                
            }
            
        }
        /// <summary>
        /// 抓图按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCatchPic_Click(object sender, EventArgs e)
        {
            mnuCatchPic_Click(sender, e);
        }
        /// <summary>
        /// 全屏按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            mnuFullScreen.Checked = !mnuFullScreen.Checked;
            mnuFullScreen_Click(sender,e);
        }

        /// <summary>
        /// 抓图菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCatchPic_Click(object sender, EventArgs e)
        {
            //保存图片目录未选择则须要选择一下保存路径
            if (strSavePicPath.Length == 0)
            {
                if (fbdMain.ShowDialog() == DialogResult.OK)
                {
                    strSavePicPath = fbdMain.SelectedPath;
                }
                else
                {
                    return;
                }
            }
            //抓图处理代码
            bool blnSavePic = false;
            string saveFilePath=strSavePicPath+@"\"+(DateTime.Now.ToString("yyyyMMdd_HHmmss"))+".bmp";            
            blnSavePic = DHPlay.DHPlayControl(PLAY_COMMAND.CatchPic, 0,saveFilePath);
            if (!blnSavePic)            
            {
                MessageBox.Show("保存图片失败!\n" + saveFilePath, pMsgTitle); 
            }

        }
        /// <summary>
        /// 双击播放画面处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPlayMain_DoubleClick(object sender, EventArgs e)
        {
            if (btnFullScreen.Enabled)
            {
                btnFullScreen_Click(sender, e);
            }
        }

        /// <summary>
        /// 音量控制滑块处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trbSound_ValueChanged(object sender, EventArgs e)
        {
            mnuSoundUp.Enabled = (trbSound.Value == trbSound.Maximum ? false : true);
            mnuSoundDown.Enabled = (trbSound.Value == 0 ? false : true);
            DHPlay.DHPlayControl(PLAY_COMMAND.SetVolume, 0,(uint) trbSound.Value);
        }
        /// <summary>
        /// 视频亮度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hsbBrightness_ValueChanged(object sender, EventArgs e)
        {
            mnuBrightnessUp.Enabled = (hsbBrightness.Value == hsbBrightness.Maximum ? false : true);            
            mnuBrightnessDown.Enabled = (hsbBrightness.Value == 0 ? false : true);
            pColor.pBrightness = hsbBrightness.Value;            
            DHPlay.DHPlayControl(PLAY_COMMAND.SetColor,0,0, ref pColor);
        }
        /// <summary>
        /// 视频对比度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hsbContrast_ValueChanged(object sender, EventArgs e)
        {
            mnuContrastUp.Enabled = (hsbContrast.Value == hsbContrast.Maximum ? false : true);
            mnuContrastDown.Enabled = (hsbContrast.Value == 0 ? false : true);
            pColor.pContrast = hsbContrast.Value;
            DHPlay.DHPlayControl(PLAY_COMMAND.SetColor, 0, 0, ref pColor);
        }
        /// <summary>
        /// 视频饱和度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hsbSaturation_ValueChanged(object sender, EventArgs e)
        {
            mnuSaturationDown.Enabled = (hsbSaturation.Value == 0 ? false : true);
            mnuSaturationUp.Enabled = (hsbSaturation.Value == hsbSaturation.Maximum ? false : true);
            pColor.pSaturation = hsbSaturation.Value;
            DHPlay.DHPlayControl(PLAY_COMMAND.SetColor, 0, 0, ref pColor);
        }
        /// <summary>
        /// 视频色调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hsbHue_ValueChanged(object sender, EventArgs e)
        {
            mnuHueUp.Enabled = (hsbHue.Value == hsbHue.Maximum ? false : true);
            mnuHueDown.Enabled = (hsbHue.Value == 0 ? false : true);
            pColor.pHue = hsbHue.Value;
            DHPlay.DHPlayControl(PLAY_COMMAND.SetColor, 0, 0, ref pColor);
        }
        /// <summary>
        /// 设置当前的播放属性
        /// </summary>
        /// <param name="playedFrameNum"></param>
        /// <param name="playedTime"></param>
        private void SetPlayCurrentInfo(UInt32 playedFrameNum, UInt32 playedTime)
        {            
            stlCurrentFrame.Text = Convert.ToString(playedFrameNum+1);
            stlCurrentTime.Text  =DHPlay.DHConvertToTime(playedTime,1,"HH:MM:SS");
            trbPlayFrames.Value = (int)(playedFrameNum < trbPlayFrames.Maximum ? playedFrameNum : 0);
            if (trbPlayFrames.Maximum <= playedFrameNum+1)
            {
                mnuStop_Click(null, null);
                if (blnLoop)
                {
                    mnuStart_Click(null, null);
                }
            }
        }
        /// <summary>
        /// 画面时钟处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerGetPlayInfo_Tick(object sender, EventArgs e)
        {
            
            if (pPlayVideoSizeMode != 9)
            {
                FRAME_INFO pFrameInfo = new FRAME_INFO();
                uint totalFrames = DHPlay.DHPlayControl(PLAY_COMMAND.GetFileTotalFrames, 0, true);
                DHPlay.DHPlayControl(PLAY_COMMAND.GetPictureSize, 0, ref pFrameInfo);
                stlTotalFrames.Text = Convert.ToString(totalFrames);
                trbPlayFrames.Maximum =(int) (totalFrames>0?totalFrames:0);
                int pParentHeight;
                int pParentWidth;
                if (palMain.Visible)
                {
                    pParentWidth = palMain.Width;
                    pParentHeight = palMain.Height;
                }
                else
                {
                    pParentWidth = this.Width;
                    pParentHeight = this.Height;
                }
                switch (pPlayVideoSizeMode )
                { 
                    case 0://原始大小
                        picPlayMain.Width = pFrameInfo.nWidth;
                        picPlayMain.Height = pFrameInfo.nHeight;
                        picPlayMain.Top = (pParentHeight - picPlayMain.Height) / 2;
                        picPlayMain.Left = (pParentWidth - picPlayMain.Width) / 2;
                        break;
                    case 1://缩放
                        picPlayMain.Top = 0;
                        picPlayMain.Height = pParentHeight;
                        picPlayMain.Width = Convert.ToInt32( pFrameInfo.nWidth * ((float)pParentHeight / (float)pFrameInfo.nHeight));
                        picPlayMain.Left = (pParentWidth - picPlayMain.Width) / 2;
                        break;
                }
            }
            SetPlayCurrentInfo(DHPlay.DHPlayControl(PLAY_COMMAND.GetCurrentFrameNum, 0, true), DHPlay.DHPlayControl(PLAY_COMMAND.GetPlayedTime, 0, true));
        }
        /// <summary>
        /// 播放进度条处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trbPlayFrames_MouseUp(object sender, MouseEventArgs e)
        {
            mnuResum_Click(sender, e);
            //float playPos = ((float)trbPlayFrames.Value / (float)trbPlayFrames.Maximum);
            DHPlay.DHPlayControl(PLAY_COMMAND.SetCurrentFrameNum, 0,(uint)trbPlayFrames.Value);
            
        }
        /// <summary>
        /// 播放进度条处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trbPlayFrames_MouseDown(object sender, MouseEventArgs e)
        {
            mnuPause_Click(sender, e);
        }

        /// <summary>
        /// 原始大小菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSizeTrue_Click(object sender, EventArgs e)
        {
            pPlayVideoSizeMode = 0;
            picPlayMain.Dock = DockStyle.None;
            picPlayMain.Anchor = AnchorStyles.None;
        }

        /// <summary>
        /// 自动适应菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSizeRoom_Click(object sender, EventArgs e)
        {
            pPlayVideoSizeMode = 1;
            picPlayMain.Dock = DockStyle.None;
            picPlayMain.Anchor = AnchorStyles.None;
        }
        /// <summary>
        /// 跟随大小菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuVideoResizeByForm_Click(object sender, EventArgs e)
        {
            pPlayVideoSizeMode = 9;
            picPlayMain.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// 单帧播放菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuStepBack_Click(object sender, EventArgs e)
        {

            if (!DHPlay.DHPlayControl(PLAY_COMMAND.OneByOneBack, 0))
            {
                MessageBox.Show("单帧播放失败！", pMsgTitle);
            }
            else
            {
                //播放/停止按钮为播放状态
                SetPlayStopControl(3);
            }
        }
        /// <summary>
        /// 单帧播放按钮处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStepBack_Click(object sender, EventArgs e)
        {
            mnuStepBack_Click(sender, e);
        }
        /// <summary>
        /// 到最前帧播放菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuToBegin_Click(object sender, EventArgs e)
        {
            if (!DHPlay.DHPlayControl(PLAY_COMMAND.ToBegin, 0))
            {
                MessageBox.Show("定位到起始帧失败！", pMsgTitle);
            }
            else
            {
                if (mnuPause.Enabled)
                {
                    mnuPause_Click(null, null);
                }
                trbPlayFrames.Value = 0;
                stlCurrentFrame.Text = "1";
            }

        }
        /// <summary>
        /// 到最后帧播放菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuToEnd_Click(object sender, EventArgs e)
        {
            if (!DHPlay.DHPlayControl(PLAY_COMMAND.ToEnd,0))
            {
                MessageBox.Show("定位最后一帧失败！", pMsgTitle);
            }
            else
            {
                if (mnuPause.Enabled)
                {
                    mnuPause_Click(null, null);
                }
                trbPlayFrames.Value = (trbPlayFrames.Maximum - 1>0?trbPlayFrames.Maximum - 1:0);
                stlCurrentFrame.Text = Convert.ToString((uint.Parse(stlTotalFrames.Text) - 1));
            }
        }
        /// <summary>
        /// 到最前帧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToBegin_Click(object sender, EventArgs e)
        {
            mnuToBegin_Click(sender, e);
        }
        /// <summary>
        /// 到最后帧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToEnd_Click(object sender, EventArgs e)
        {
            mnuToEnd_Click(sender, e);
        }
        /// <summary>
        /// 声音控制处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSoundSwitch_Click(object sender, EventArgs e)
        {
            chkEnableSound.Checked = !chkEnableSound.Checked;
            chkEnableSound_CheckedChanged(null, null);
        }
        /// <summary>
        /// 加音量处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSoundUp_Click(object sender, EventArgs e)
        {
            if (trbSound.Value + 500 > trbSound.Maximum)
            {
                trbSound.Value = trbSound.Maximum;
            }
            else
            {
                trbSound.Value += 500;
            }
            trbSound_ValueChanged(null,null);
        }
        /// <summary>
        /// 降音量处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSoundDown_Click(object sender, EventArgs e)
        {
            if (trbSound.Value - 500 <0)
            {
                trbSound.Value = 0;
            }
            else
            {
                trbSound.Value -= 500;
            }
            trbSound_ValueChanged(null, null);
        }

        #region << 播放视频颜色配置 >>
        private void mnuBrightnessUp_Click(object sender, EventArgs e)
        {
            if (hsbBrightness.Value + 1 > hsbBrightness.Maximum)
            {
                hsbBrightness.Value = hsbBrightness.Maximum;
            }
            else
            {
                hsbBrightness.Value += 1;
            }
            hsbBrightness_ValueChanged(null, null);
        }

        private void mnuBrightnessDown_Click(object sender, EventArgs e)
        {
            if (hsbBrightness.Value -1 > hsbBrightness.Maximum)
            {
                hsbBrightness.Value =0;
            }
            else
            {
                hsbBrightness.Value -= 1;
            }
            hsbBrightness_ValueChanged(null, null);
        }

        private void mnuContrastUp_Click(object sender, EventArgs e)
        {
            if (hsbContrast.Value + 1 > hsbContrast.Maximum)
            {
                hsbContrast.Value = hsbContrast.Maximum;
            }
            else
            {
                hsbContrast.Value += 1;
            }
            hsbContrast_ValueChanged(null, null);
        }

        private void mnuContrastDown_Click(object sender, EventArgs e)
        {
            if (hsbContrast.Value -1 > hsbContrast.Maximum)
            {
                hsbContrast.Value = hsbContrast.Maximum;
            }
            else
            {
                hsbContrast.Value -= 1;
            }
            hsbContrast_ValueChanged(null, null);
        }

        private void mnuSaturationUp_Click(object sender, EventArgs e)
        {
            if (hsbSaturation.Value + 1 > hsbSaturation.Maximum)
            {
                hsbSaturation.Value = hsbSaturation.Maximum;
            }
            else
            {
                hsbSaturation.Value += 1;
            }
            hsbSaturation_ValueChanged(null, null);
        }

        private void mnuSaturationDown_Click(object sender, EventArgs e)
        {
            if (hsbSaturation.Value - 1 > hsbSaturation.Maximum)
            {
                hsbSaturation.Value = hsbSaturation.Maximum;
            }
            else
            {
                hsbSaturation.Value -= 1;
            }
            hsbSaturation_ValueChanged(null, null);
        }

        private void mnuHueUp_Click(object sender, EventArgs e)
        {
            if (hsbHue.Value + 1 > hsbHue.Maximum)
            {
                hsbHue.Value = hsbHue.Maximum;
            }
            else
            {
                hsbHue.Value += 1;
            }
            hsbHue_ValueChanged(null, null);
        }

        private void mnuHueDown_Click(object sender, EventArgs e)
        {
            if (hsbHue.Value + 1 > hsbHue.Maximum)
            {
                hsbHue.Value = hsbHue.Maximum;
            }
            else
            {
                hsbHue.Value -= 1;
            }
            hsbHue_ValueChanged(null, null);
        }

        #endregion

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            frmAbout fa = new frmAbout();
            fa.ShowDialog();
        }
        /// <summary>
        /// 局部放大演示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSetDisplayRegion_Click(object sender, EventArgs e)
        {
            frmDisplayRegion fdr = new frmDisplayRegion();//局部放大演示窗口            
            fdr.DisplayRegionPort = 0;
            fdr.ShowDialog();
        }
        /// <summary>
        /// 录像转换演示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDataRecord_Click(object sender, EventArgs e)
        {
            frmDataRecord fdr = new frmDataRecord();    //数据流录像演示窗口            
            fdr.ShowDialog();
        }

        #region << 字符叠加处理 >>

        /// <summary>
        /// 字符叠加处理函数
        /// </summary>
        /// <param name="PlayPort"></param>
        /// <param name="Hdc"></param>
        /// <param name="UserData"></param>
        private void DrawFun(int PlayPort, IntPtr Hdc, int UserData)
        {
            Graphics gps = Graphics.FromHdc(Hdc);
            if (pDisplayText.Length > 0)
            {
                gps.DrawString(pDisplayText, pFontSet/*文字的字体*/, pBrushSet/*格式刷*/, pTextPointSet);
            }
            if (pShowTime == true)
            {
                gps.DrawString(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), pFontSet/*文字的字体*/, pBrushSet/*格式刷*/, pTimePointSet);
            }
            if (pShowDraw == true)
            {
                switch (pDrawStyle)
                {
                    case 0://曲线
                        gps.DrawBezier(Pens.Yellow, pTextPointSet, new PointF(pTextPointSet.X + 100, pTextPointSet.Y + 10), pTimePointSet, new PointF(pTimePointSet.X + 200, pTimePointSet.Y));
                        break;
                    case 1://圆
                        gps.DrawEllipse(Pens.Yellow, pTextPointSet.X, pTextPointSet.Y, pTimePointSet.X, pTimePointSet.Y);
                        break;
                    case 2://扇形
                        //gps.DrawPie(Pens.BlueViolet , pTextPointSet.X, pTextPointSet.Y, pTimePointSet.X, pTimePointSet.Y, 30, -150);
                        gps.DrawPie(new Pen(pBrushSet, 4), 100, 150, 200, 250, 0, -90);
                        break;
                }

            }
            //其他需要绘制的内容需要软件设计中添加

        }

        #endregion

        /// <summary>
        /// 字符叠加菜单处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDrawFunSet_Click(object sender, EventArgs e)
        {
            frmDrawFunSet fDFS=new frmDrawFunSet();
            fDFS.ShowDialog();
            if (fDFS.BlnOK)
            {
                pShowTime = fDFS.ShowTime;
                pTextPointSet = fDFS.TextPointSet;
                pTimePointSet = fDFS.TimePointSet;
                pFontSet = fDFS.FontSet;
                pBrushSet = fDFS.BrushSet;
                pDisplayText = fDFS.DisplayText;
                pShowDraw = fDFS.BlnDraw;
                pDrawStyle = fDFS.DrawStyle;
            }
        }

    }
}