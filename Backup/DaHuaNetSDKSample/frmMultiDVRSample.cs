using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DHNetSDK;
using DHPlaySDK;

namespace DaHuaNetSDKSample
{

    public partial class frmMultiDVRSample : Form
    {
        /// <summary>
        /// 登录句柄1
        /// </summary>
        private int pLoginID1;
        /// <summary>
        /// 登录句柄2
        /// </summary>
        private int pLoginID2;
        /// <summary>
        /// 设备信息
        /// </summary>
        private NET_DEVICEINFO deviceInfo;
        /// <summary>
        /// 断开回调
        /// </summary>
        private fDisConnect disConnect;
        /// <summary>
        /// 实时播放句柄1
        /// </summary>
        private int realPlayHandle1 = 0;
        /// <summary>
        /// 实时播放句柄2
        /// </summary>
        private int realPlayHandle2 = 0;


        public frmMultiDVRSample()
        {            
            InitializeComponent();
        }

        /// <summary>
        /// 设备连接断开处理
        /// </summary>
        /// <param name="lLoginID">登录ID</param>
        /// <param name="pchDVRIP">DVR设备IP</param>
        /// <param name="nDVRPort">端口</param>
        /// <param name="dwUser">用户数据</param>
        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            //设备断开连接处理            
            MessageBox.Show("设备用户断开连接", "提示");
        }
        /// <summary>
        /// 添加设备1处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDevice1_Click(object sender, EventArgs e)
        {
            frm_AddDevice fAddDev = new frm_AddDevice();
            fAddDev.ShowDialog();
            try
            {
                if (fAddDev.blnOKEnter == true)
                {
                    //设备用户信息获得
                    deviceInfo = new NET_DEVICEINFO();
                    int error = 0;
                    //设备1登录:注意登录ID
                    pLoginID1 = DHClient.DHLogin(fAddDev.cmbDevIP.Text.ToString(), ushort.Parse(fAddDev.txtDevProt.Text.ToString()), fAddDev.txtName.Text.ToString(), fAddDev.txtPassword.Text.ToString(), out deviceInfo, out error);
                    //是否登录成功判断
                    if (pLoginID1 != 0)
                    {
                        MessageBox.Show("设备1登录成功！", "提示");
                    }
                    else
                    {
                        MessageBox.Show("设备1登录失败！", "提示");
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 添加设备2处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDevice2_Click(object sender, EventArgs e)
        {

            frm_AddDevice fAddDev = new frm_AddDevice();
            fAddDev.ShowDialog();
            try
            {
                if (fAddDev.blnOKEnter == true)
                {
                    //设备用户信息获得
                    deviceInfo = new NET_DEVICEINFO();
                    int error = 0;
                    //设备2登录:注意登录ID
                    pLoginID2 = DHClient.DHLogin(fAddDev.cmbDevIP.Text.ToString(), ushort.Parse(fAddDev.txtDevProt.Text.ToString()), fAddDev.txtName.Text.ToString(), fAddDev.txtPassword.Text.ToString(), out deviceInfo, out error);
                    //是否登录成功判断
                    if (pLoginID2 != 0)
                    {
                        MessageBox.Show("设备2登录成功！", "提示");
                    }
                    else
                    {
                        MessageBox.Show("设备2登录失败！", "提示");
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 设备1实时播放处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRealPlay1_Click(object sender, EventArgs e)
        {
            //实时监控开始：注意realPlayHandle1和pLoginID1
            realPlayHandle1 = DHClient.DHRealPlay(pLoginID1, 0, picRealPlay1.Handle);
        }
        /// <summary>
        /// 多设备演示程序加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMultiDVRSample_Load(object sender, EventArgs e)
        {
            disConnect = new fDisConnect(DisConnectEvent);
            DHClient.DHInit(disConnect, IntPtr.Zero);
            DHClient.DHSetEncoding(LANGUAGE_ENCODING.gb2312);//字符编码格式设置，默认为gb2312字符编码，如果为其他字符编码请设置

            Utility.StringUtil.InitControlText(this);
        }
        /// <summary>
        /// 设备2实时监视处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRealPlay2_Click(object sender, EventArgs e)
        {
            //实时监控开始：注意realPlayHandle2和pLoginID1
            realPlayHandle2 = DHClient.DHRealPlay(pLoginID2, 1, picRealPlay2.Handle);
        }
        /// <summary>
        /// 设备1停止实时监视处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopReal1_Click(object sender, EventArgs e)
        {
            //停止监控：注意realPlayHandle1
            DHClient.DHStopRealPlay(realPlayHandle1);
            picRealPlay1.Refresh();
        }
        /// <summary>
        /// 设备2停止实时监视处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopReal2_Click(object sender, EventArgs e)
        {
            //停止监控：注意realPlayHandle2
            DHClient.DHStopRealPlay(realPlayHandle2);            
            picRealPlay2.Refresh();

        }

    }
}