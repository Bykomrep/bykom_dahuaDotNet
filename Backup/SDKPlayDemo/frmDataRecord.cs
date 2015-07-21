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
    public partial class frmDataRecord : Form
    {
        public frmDataRecord()
        {
            InitializeComponent();
        }

        private void frmDataRecord_Load(object sender, EventArgs e)
        {
            StringUtil.InitControlText(this);
        }
        /// <summary>
        /// 找开录像保存路径对话框按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowOpenFileDlg_Click(object sender, EventArgs e)
        {
            if (fbdMain.ShowDialog() == DialogResult.OK)
            {
                txtDirPath.Text = fbdMain.SelectedPath;
            }
        }
        /// <summary>
        /// 开始录像按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartRecord_Click(object sender, EventArgs e)
        {
            string strtemp = txtDirPath.Text.ToString() + @"\" + txtFileName.Text.ToString();
            strtemp=strtemp.Replace(@"\\", @"\");
            if (strtemp.Length > 0)
            {
                //开始录像
                if (DHPlay.DHStartDataRecord((int)nudPlayChannelNO.Value, strtemp, cmbDataType.SelectedIndex))
                {
                    //MessageBox.Show("开始数据流保存成功!", "提示:");
                    MessageBox.Show(strtemp, "提示:");
                }
                else
                {
                    MessageBox.Show("开始数据流保存失败!", "提示:");
                };
            }
            else
            {
                MessageBox.Show("请选择存放目录和设备保存文件名!", "提示:");
            }
        }
        /// <summary>
        /// 停止录像按钮按下处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopRecord_Click(object sender, EventArgs e)
        {

            if (DHPlay.DHStopDataRecord((int)nudPlayChannelNO.Value)==false)
            {
                MessageBox.Show("停止数据流保存失败!", "提示:");
            }
        }
    }
}