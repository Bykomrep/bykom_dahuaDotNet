using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Utility
{
    public partial class MsgForm : Form
    {
        /// <summary>
        /// 按钮类型
        /// </summary>
        private MessageUtil.MBButtonType btnType;

        public MsgForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示回调，根据显示信息多少设置窗体的大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MsgForm_Shown(object sender, EventArgs e)
        {
            int iTextMargin = 50; // 文本与窗体边框的间距
            int iBottomMargin = 10; // 按钮与窗体底部的间距

            Point point = picShow.Location;
            if (picShow.Image == null)
                txtLable.Location = picShow.Location;

            Size txtSize = txtLable.Size;
            Size formSize = this.Size;

            // 根据文本框大小缩放窗体适应
            int nWidth = txtLable.Size.Width + iTextMargin * 2; //point.X * 2
            int nHeight = point.Y * 2 + txtLable.Height + btnOne.Size.Height + iBottomMargin;
            this.Size = new Size(nWidth, nHeight);

            // 文本框居中
            txtLable.Location = new Point((nWidth - txtSize.Width) / 2, txtLable.Location.Y);

            // 设置按钮位置
            int iBtnY = point.Y * 2 + txtLable.Height + btnOne.Size.Height;
            btnOne.Location = new Point(btnOne.Location.X, iBtnY);
            btnTwo.Location = new Point(btnTwo.Location.X, iBtnY);
            btnThree.Location = new Point(btnThree.Location.X, iBtnY);

            this.CenterButtons();
        }

        /// <summary>
        /// 消息框内容
        /// </summary>
        public string Content
        {
            set
            {
                txtLable.Text = value;
            }
            get
            {
                return txtLable.Text;
            }
        }

        /// <summary>
        /// 图标类型
        /// </summary>
        public MessageUtil.MBIconType IconType
        {
            set
            {
                switch (value)
                {
                    case MessageUtil.MBIconType.MBIcon_None:
                        picShow.Image = null;
                        picShow.Visible = false;
                        break;

                    case MessageUtil.MBIconType.MBIcon_OK:
                        break;

                    case MessageUtil.MBIconType.MBIcon_Warning:
                        picShow.Image = imgList.Images["Warning"];
                        break;

                    case MessageUtil.MBIconType.MBIcon_Error:
                        picShow.Image = imgList.Images["Error"];
                        break;

                    default:
                        picShow.Image = null;
                        picShow.Visible = false;
                        break;
                }
            }
        }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public MessageUtil.MBButtonType ButtonType
        {
            set
            {
                btnType = value;
            }
        }

        private void CenterButtons()
        {
            int nMarginButtons = 15;

            switch (btnType)
            {
                case MessageUtil.MBButtonType.MBBtn_OK:
                    {
                        btnTwo.Visible = false;
                        btnThree.Visible = false;
                        Point point = btnOne.Location;
                        int x = (this.Size.Width - btnOne.Size.Width) / 2;
                        point.X = x;
                        btnOne.Location = point;
                        btnOne.Text = StringUtil.ConvertString("确认", "Common");
                        btnOne.DialogResult = DialogResult.OK;
                    }
                    break;

                case MessageUtil.MBButtonType.MBBtn_OKCancel:
                    {
                        btnThree.Visible = false;
                        Point point = btnOne.Location;

                        int x = (this.Size.Width - btnOne.Size.Width * 2) / 2;

                        point.X = x - nMarginButtons;
                        btnOne.Location = point;
                        point.X = x + btnOne.Size.Width + nMarginButtons;
                        btnTwo.Location = point;

                        btnOne.Text = StringUtil.ConvertString("确认", "Common");
                        btnTwo.Text = StringUtil.ConvertString("取消", "Common");
                        btnOne.DialogResult = DialogResult.OK;
                        btnTwo.DialogResult = DialogResult.Cancel;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}