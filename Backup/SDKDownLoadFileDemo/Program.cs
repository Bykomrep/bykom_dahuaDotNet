using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Utility;
using System.Configuration;

namespace SDKDownLoadFileDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            StringUtil.AppName = @"DownLoadFile";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_Main());
        }
    }
}