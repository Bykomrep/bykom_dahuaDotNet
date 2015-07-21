/*
 * ************************************************************************
 *                            SDK
 *                      大华网络SDK(C#版)示例程序
 * 
 * (c) Copyright 2007, ZheJiang Dahua Technology Stock Co.Ltd.
 *                      All Rights Reserved
 * 版 本 号:0.01
 * 文件名称:frm_PlayBackByFileSet.cs
 * 功能说明:按时间回放设置画面
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

namespace DaHuaNetSDKSample
{
    public partial class frm_PlayBackByTimeSet : Form
    {
        public bool blnOKEnter = false;
        public frm_PlayBackByTimeSet()
        {
            InitializeComponent();
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            blnOKEnter = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            blnOKEnter = false;
            this.Close();
        }

        private void frm_PlayBackByTimeSet_Load(object sender, EventArgs e)
        {
            //for(int i =1;i<=8;i++)
            //{
            //    cmbChannelSelect.Items.Add(i.ToString());
            //}
            cmbChannelSelect.SelectedIndex = 0;
            dtpStart.Value = DateTime.Now.AddDays(-7);

            Utility.StringUtil.InitControlText(this);
        }

        private void cmbChannelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtChannelID.Text = cmbChannelSelect.SelectedIndex.ToString();
        }
    }
}