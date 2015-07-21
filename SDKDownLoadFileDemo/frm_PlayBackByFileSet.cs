
/*
 * ************************************************************************
 *                            SDK
 *                      网络SDK(C#版)示例程序
 * 
 * 版 本 号:0.01
 * 文件名称:frm_PlayBackByFileSet.cs
 * 功能说明:按文件回放检索画面
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
using DHNetSDK;                     
using System.Runtime.InteropServices;
using Utility;


namespace SDKDownLoadFileDemo
{
    public partial class frm_PlayBackByFileSet : Form
    {
        /// <summary>
        /// 用户登录ID
        /// </summary>
        public int gLoginID;

        /// <summary>
        /// 选择的文件信息
        /// </summary>
        public NET_RECORDFILE_INFO gFileInfo;

        /// <summary>
        /// 文件信息查询结果
        /// </summary>
        private NET_RECORDFILE_INFO[] nriFileInfo;

        /// <summary>
        /// 最多查询文件数
        /// </summary>
        private const int intFilesMaxCount = 1024;

        /// <summary>
        /// 确认按钮是否按下
        /// </summary>
        public bool blnOKEnter = false;

        /// <summary>
        /// 提示消息标题
        /// </summary>
        private const string strMsgTitle = "网络SDK Demo程序";

        public frm_PlayBackByFileSet()
        {
            InitializeComponent();
        
        }

        private void frm_PlayBackByFileSet_Load(object sender, EventArgs e)
        {
            //通道选择列表
            //for (int i = 1; i <= 8; i++)
            //{
            //    cmbChannelSelect.Items.Add(i.ToString());
            //}
            //默认选择项
            cmbChannelSelect.SelectedIndex = 0;
            //记录文件类型默认选择
            cmbRecordFileTypeSelect.SelectedIndex = 0;
            //OK按钮没有被按下
            blnOKEnter = false;
            btnOK.Enabled = false;
            //开始日期为当前日期的前一周
            dtpStart.Value = DateTime.Now.AddDays(-7);
            ////开始日期为当前日期的前一月
            //dtpStart.Value = DateTime.Now.AddMonths(-1);

            StringUtil.InitControlText(this);
        }

        /// <summary>
        /// 通道选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbChannelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtChannelID.Text = cmbChannelSelect.SelectedIndex.ToString();
        }

        /// <summary>
        /// 查询按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            #region << 画面操作 >>
            int channelID = 0;
            RECORD_FILE_TYPE rfType = RECORD_FILE_TYPE.ALLRECORDFILE;
            if (txtChannelID.Text.Trim().Length == 0)
            {
                MessageUtil.ShowMsgBox(StringUtil.ConvertString("请输入通道号!"),
                                       StringUtil.ConvertString(strMsgTitle));
                return;
            }
            else
            {
                channelID = int.Parse(txtChannelID.Text);
            }
            if (txtDevName.Text.Trim().Length == 0)
            {
                MessageUtil.ShowMsgBox(StringUtil.ConvertString("请输入设备名!"),
                                       StringUtil.ConvertString(strMsgTitle));
                return;
            }
            if (cmbChannelSelect.SelectedIndex == -1)
            {
                MessageUtil.ShowMsgBox(StringUtil.ConvertString("请选择文件类型!"),
                                       StringUtil.ConvertString(strMsgTitle));
                return;
            }
            else
            {
                switch (cmbRecordFileTypeSelect.SelectedIndex)
                {
                    case 0:
                        rfType = RECORD_FILE_TYPE.ALLRECORDFILE;
                        break;
                    case 1:
                        rfType = RECORD_FILE_TYPE.OUTALARM;
                        break;
                    case 2:
                        rfType = RECORD_FILE_TYPE.DYNAMICSCANALARM;
                        break;
                    case 3:
                        rfType = RECORD_FILE_TYPE.ALLALARM;
                        break;
                    case 4:
                        rfType = RECORD_FILE_TYPE.CARDNOSEACH;
                        break;
                    case 5:
                        rfType = RECORD_FILE_TYPE.COMBINEDSEACH;
                        break;

                }
            }

            string strTmStart = dtpStart.Text + " " + txtTimeStart.Text;
            string strTmEnd = dtpEnd.Text + " " + txtTimeEnd.Text;

            DateTime tmStart;
            DateTime tmEnd;

            try
            {
                tmStart = DateTime.Parse(strTmStart);
                tmEnd = DateTime.Parse(strTmEnd);
            }
            catch (System.Exception ex)
            {
                MessageUtil.ShowMsgBox(StringUtil.ConvertString("请输入正确的时间格式!"),
                                       StringUtil.ConvertString(strMsgTitle));
                return;
            }

            if (tmStart >= tmEnd)
            {
                MessageUtil.ShowMsgBox(StringUtil.ConvertString("开始日期不在结束日期前!"), 
                                       StringUtil.ConvertString(strMsgTitle));
                return;
            }

            #endregion

            #region << 查询操作 >>

            nriFileInfo = new NET_RECORDFILE_INFO[intFilesMaxCount];
            string strTimeFormatStyle = "yyyy年mm月dd日 hh:MM:ss";//日期时间格式化字符，具体定义请参见NET_TIME结构的ToSting方法说明
            int intFileCount = 0;
            bool blnQueryRecordFile = false;
            blnQueryRecordFile = DHClient.DHQueryRecordFile(gLoginID, channelID, rfType, tmStart, tmEnd, null, ref nriFileInfo, intFilesMaxCount * Marshal.SizeOf(typeof(NET_RECORDFILE_INFO)), out intFileCount, 5000, false);            
            if (blnQueryRecordFile == true)
            {
                lsvFiles.Items.Clear();
                if(0==intFileCount)
                {
                    MessageUtil.ShowMsgBox(StringUtil.ConvertString("未查询到录像文件!"),
                                           StringUtil.ConvertString(strMsgTitle));
                    return;
                }

                ListViewItem lvi;
                for (int i = 0; i < intFileCount; i++)
                {
                    lvi = new ListViewItem();
                    lvi.SubItems[0].Text = txtDevName.Text + nriFileInfo[i].ch.ToString();
                    lvi.SubItems.Add(nriFileInfo[i].starttime.ToString(strTimeFormatStyle));
                    lvi.SubItems.Add(nriFileInfo[i].endtime.ToString(strTimeFormatStyle));
                    lvi.SubItems.Add(nriFileInfo[i].size.ToString());
                    lsvFiles.Items.Add(lvi);
                }
                return;
            }
            else
            {
                btnOK.Enabled = false;
            }
            return;
            #endregion

        }

        /// <summary>
        /// 文件信息列表选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsvFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.ItemIndex != -1)
            {
                btnOK.Enabled = true;
                gFileInfo = nriFileInfo[e.ItemIndex];
            }
            else
            {
                btnOK.Enabled = false;
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
            gFileInfo = nriFileInfo[lsvFiles.SelectedItems[0].Index];
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

        private void cmbRecordFileTypeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}