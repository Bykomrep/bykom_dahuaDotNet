
/*
 * ************************************************************************
 *                            SDK
 *                      网络SDK(C#版)示例程序
 * 
 * 版 本 号:0.01
 * 文件名称:frm_AddDevice.cs
 * 功能说明:设备用户登录信息输入画面
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
using System.IO;


namespace SDKDownLoadFileDemo
{
    public partial class frm_AddDevice : Form
    {
        [Serializable]
        private class DEVINFO
        {
            public string strDevName;
            public string strDevIP;
            public string strDevPort;
            public string strDevUser;
            public string strDevPwd;
        }

        /// <summary>
        /// 确认按钮是否按下
        /// </summary>
        public bool blnOKEnter = false;

        /// <summary>
        /// 已记录的设备数目
        /// </summary>
        private int devNum;

        /// <summary>
        /// 已记录的设备信息
        /// </summary>
        private DEVINFO[] devInfos;

        private const string fileName = @"DownLoadFile_devInfo.dat";

        public frm_AddDevice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载时进行初始化工作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_AddDevice_Load(object sender, EventArgs e)
        {
            ReadDevInfo();

            foreach (DEVINFO info in devInfos)
                cmbDevIP.Items.Add(info.strDevIP);

            cmbDevIP.SelectedIndex = 0;

            //语言设置
            string oldAppName = Utility.StringUtil.AppName;

            Utility.StringUtil.AppName = "Common";
            Utility.StringUtil.InitControlText(this);

            Utility.StringUtil.AppName = oldAppName;
        }

        /// <summary>
        /// 通过下拉框选择不同的设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDevIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = ((ComboBox)sender).SelectedIndex;
                DEVINFO info = devInfos[index];

                txtDevName.Text = info.strDevName;
                txtDevProt.Text = info.strDevPort;
                txtName.Text = info.strDevUser;
                txtPassword.Text = info.strDevPwd;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// 确认按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            blnOKEnter = true;

            SaveDevInfo();

            this.Close();
        }

        /// <summary>
        /// 取消按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            blnOKEnter = false;

            this.Close();
        }

        /// <summary>
        /// 没有设备信息文件时，用于初始化设备信息
        /// </summary>
        private void InitDevInfo()
        {
            devNum = 1;
            devInfos = new DEVINFO[1];

            devInfos[0] = new DEVINFO();
            devInfos[0].strDevName = "test";
            devInfos[0].strDevIP = "10.24.5.23";
            devInfos[0].strDevPort = "37777";
            devInfos[0].strDevUser = "admin";
            devInfos[0].strDevPwd = "admin";
        }

        /// <summary>
        /// 保存输入过的设备信息
        /// </summary>
        private void SaveDevInfo()
        {
            // 最多记录iMaxNum个设备信息
            const int iMaxNum = 10;

            try
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string strIP = cmbDevIP.Text;
                    bool bNewIP = true;

                    foreach (DEVINFO info in devInfos)
                    {
                        if (strIP.Equals(info.strDevIP))
                        {
                            bNewIP = false;
                            break;
                        }
                    }

                    int oldNum = devNum;
                    devNum += (bNewIP && devNum < iMaxNum) ? 1 : 0;
                    sw.WriteLine(devNum);

                    sw.WriteLine(txtDevName.Text);
                    sw.WriteLine(strIP);
                    sw.WriteLine(txtDevProt.Text);
                    sw.WriteLine(txtName.Text);
                    sw.WriteLine(txtPassword.Text);

                    for (int i = 0; i < oldNum && i < iMaxNum; i++)
                    {
                        DEVINFO info = devInfos[i];

                        if (!strIP.Equals(info.strDevIP))
                        {
                            sw.WriteLine(info.strDevName);
                            sw.WriteLine(info.strDevIP);
                            sw.WriteLine(info.strDevPort);
                            sw.WriteLine(info.strDevUser);
                            sw.WriteLine(info.strDevPwd);
                        }
                    }

                    sw.Close();
                }

            }
            catch (System.Exception ex)
            {

            }
        }

        /// <summary>
        /// 读取设备信息
        /// </summary>
        private void ReadDevInfo()
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string strLine;
                    StreamReader sr = File.OpenText(fileName);

                    strLine = sr.ReadLine();
                    devNum = int.Parse(strLine);
                    devInfos = new DEVINFO[devNum];

                    for (int i = 0; i < devNum; i++)
                    {
                        devInfos[i] = new DEVINFO();
                        devInfos[i].strDevName = sr.ReadLine();
                        devInfos[i].strDevIP = sr.ReadLine();
                        devInfos[i].strDevPort = sr.ReadLine();
                        devInfos[i].strDevUser = sr.ReadLine();
                        devInfos[i].strDevPwd = sr.ReadLine();
                    }

                    sr.Close();
                }
                else
                {
                    InitDevInfo();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);

                InitDevInfo();
            }
        }
    }
}
