
/*
 * ************************************************************************
 *                            SDK
 *                      大华网络SDK(C#版)示例程序
 * 
 * (c) Copyright 2007, ZheJiang Dahua Technology Stock Co.Ltd.
 *                      All Rights Reserved
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

namespace ColligateDemo
{
    public partial class frm_AddDevice : Form
    {
        /// <summary>
        /// 确认按钮是否按下
        /// </summary>
        public bool blnOKEnter = false;

        public frm_AddDevice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确认按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            blnOKEnter = true;
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
    }
}