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
        /// ��ť����
        /// </summary>
        private MessageUtil.MBButtonType btnType;

        public MsgForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ʾ�ص���������ʾ��Ϣ�������ô���Ĵ�С
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MsgForm_Shown(object sender, EventArgs e)
        {
            int iTextMargin = 50; // �ı��봰��߿�ļ��
            int iBottomMargin = 10; // ��ť�봰��ײ��ļ��

            Point point = picShow.Location;
            if (picShow.Image == null)
                txtLable.Location = picShow.Location;

            Size txtSize = txtLable.Size;
            Size formSize = this.Size;

            // �����ı����С���Ŵ�����Ӧ
            int nWidth = txtLable.Size.Width + iTextMargin * 2; //point.X * 2
            int nHeight = point.Y * 2 + txtLable.Height + btnOne.Size.Height + iBottomMargin;
            this.Size = new Size(nWidth, nHeight);

            // �ı������
            txtLable.Location = new Point((nWidth - txtSize.Width) / 2, txtLable.Location.Y);

            // ���ð�ťλ��
            int iBtnY = point.Y * 2 + txtLable.Height + btnOne.Size.Height;
            btnOne.Location = new Point(btnOne.Location.X, iBtnY);
            btnTwo.Location = new Point(btnTwo.Location.X, iBtnY);
            btnThree.Location = new Point(btnThree.Location.X, iBtnY);

            this.CenterButtons();
        }

        /// <summary>
        /// ��Ϣ������
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
        /// ͼ������
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
        /// ��ť����
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
                        btnOne.Text = StringUtil.ConvertString("ȷ��", "Common");
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

                        btnOne.Text = StringUtil.ConvertString("ȷ��", "Common");
                        btnTwo.Text = StringUtil.ConvertString("ȡ��", "Common");
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