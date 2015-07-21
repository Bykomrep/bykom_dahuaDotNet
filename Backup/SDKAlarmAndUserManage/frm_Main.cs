/*
 * ************************************************************************
 *                            SDK
 *                      大华网络SDK(C#版)示例程序
 * 
 * (c) Copyright 2007, ZheJiang Dahua Technology Stock Co.Ltd.
 *                      All Rights Reserved
 * 版 本 号:0.01
 * 文件名称:frm_MainC.cs
 * 功能说明:原始封装应用示例程序主画面
 * 作    者:李德明
 * 作成日期:2007/11/10
 * 修改日志:    日期        版本号      作者        变更事由
 *              2007/11/10  0.01        李德明      新建作成
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
using DaHuaNetSDKSample;
using DHNetSDK;                         //大华网络SDK（C#版）
using System.Runtime.InteropServices;
using Utility;

namespace SDKAlarmAndUserManage
{
    public partial class frm_Main : Form
    {
        /// <summary>
        /// 设备用户登录句柄
        /// </summary>
        private int pLoginID;

        /// <summary>
        /// 程序消息提示Title
        /// </summary>
        private const string pMsgTitle = "大华网络SDK Demo程序";

        /// <summary>
        /// 最后操作信息显示格式
        /// </summary>
        private const string pErrInfoFormatStyle = "代码:errcode;\n描述:errmSG.";

        /// <summary>
        /// 用户信息
        /// </summary>
        private USER_MANAGE_INFO userManageInfo;

        /// <summary>
        /// 常规报警信息
        /// </summary>
        private NET_CLIENT_STATE clientState;

        /// <summary>
        /// 遮挡报警信息
        /// </summary>
        private byte[] AlarmShelter;

        /// <summary>
        /// 音频报警信息
        /// </summary>
        private byte[] AlarmAudio;

        /// <summary>
        /// 外部报警信息
        /// </summary>
        private byte[] AlarmExternal;

        /// <summary>
        /// 硬盘损坏报警信息
        /// </summary>
        private byte[] AlarmDiskErr;

        /// <summary>
        /// 硬盘空间不足报警信息
        /// </summary>
        private bool AlarmDiskFull;

        /// <summary>
        /// 回调信息类型
        /// </summary>
        private CALLBACK_TYPE cbkType;

        /// <summary>
        /// 获取配置信息的回调
        /// </summary>
        private fMessCallBack msgCallBack;

        /// <summary>
        /// 设备断开连接的回调
        /// </summary>
        private fDisConnect disConnect;

        /// <summary>
        /// 当前用户组ID
        /// </summary>
        private uint ActiveGroupID;

        /// <summary>
        /// 当前用户组序号Index
        /// </summary>
        private uint ActiveGroupIndex;

        /// <summary>
        /// 当前用户ID
        /// </summary>
        private uint ActiveUserID;

        /// <summary>
        /// 当前用户序号Index
        /// </summary>
        private uint ActiveUserIndex;

        /// <summary>
        /// 权限列表项
        /// </summary>
        private struct LISTITEM
        {
            public uint index;
            public uint keyID;
        }
        /// <summary>
        /// 权限列表的索引和Key值
        /// </summary>
        private LISTITEM[] rightListArray;

        /// <summary>
        /// 用户ID列表
        /// </summary>
        private uint[] userIDListArray;

        public frm_Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设备用户登录/注销处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUserLogin_Click(object sender, EventArgs e)
        {
            string strLogin  = StringUtil.ConvertString("设备用户登录");
            string strLogout = StringUtil.ConvertString("设备用户注销");

            if (btnUserLogin.Text.Equals(strLogin))
            {
                frm_AddDevice fLogin = new frm_AddDevice();
                fLogin.ShowDialog();
                if (fLogin.blnOKEnter == true)
                {
                    //设备用户信息获得
                    NET_DEVICEINFO deviceInfo = new NET_DEVICEINFO();

                    int error = 0;
                    //设备用户登录
                    pLoginID = DHClient.DHLogin(fLogin.cmbDevIP.Text.ToString(), ushort.Parse(fLogin.txtDevProt.Text.ToString()), fLogin.txtName.Text.ToString(), fLogin.txtPassword.Text.ToString(), out deviceInfo, out error);
                    if (pLoginID != 0)
                    {
                        btnUserLogin.BackColor = Color.Yellow;
                        btnUserLogin.Text = strLogout;
                        grpMain.Enabled = true;
                        //读取用户信息
                        GetUserInfo(pLoginID, ref userManageInfo, 3000);
                        //设置消息回调函数
                        DHClient.DHSetDVRMessCallBack(msgCallBack, IntPtr.Zero);
                        IntPtr pVer = new IntPtr();
                        pVer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
                        int nRet = 0;
                        int nVer = 0;
                        bool bRet = DHClient.DHQueryDevState((int)pLoginID, (int)DHClient.DH_DEVSTATE_PROTOCAL_VER, pVer, 4, ref nRet, 2000);
                        if (bRet)
                        {
                             nVer = (int)Marshal.PtrToStructure((IntPtr)((UInt32)pVer), typeof(int));

                             //开始侦听
                             if (nVer >= 5)
                             {
                                 if (DHClient.DHStartListenEx(pLoginID) == true)
                                 {
                                     timeDisplayAlarmInfo.Enabled = true;
                                 }
                                 else
                                 {
                                     timeDisplayAlarmInfo.Enabled = false;
                                 }
                             }
                             else
                             {
                                 if (DHClient.DHStartListen(pLoginID) == true)
                                 {
                                     timeDisplayAlarmInfo.Enabled = true;
                                 }
                                 else
                                 {
                                     timeDisplayAlarmInfo.Enabled = false;
                                 }
                             }
                        }
                        
                    }
                }
            }
            else
            {
                bool result = DHClient.DHLogout(pLoginID);
                if (result == false)
                {
                    //报最后一次操作的错误信息
                    MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                }


                //画面初期化
                timeDisplayAlarmInfo.Enabled = false;
                this.Controls.Clear();
                InitializeComponent();
                StringUtil.InitControlText(this);
                pLoginID = 0;
                userManageInfo = new USER_MANAGE_INFO();
                clientState = new NET_CLIENT_STATE();
                AlarmAudio = new byte[16];
                AlarmDiskErr = new byte[32];//ＤＶＲ最大可挂３２块硬盘，每一位表示一个硬盘的状态，0:硬盘状态正常;1:硬盘出错
                AlarmDiskFull = false;
                AlarmShelter = new byte[16];
                AlarmExternal = new byte[16];
                ActiveUserID = 0;
                ActiveGroupID = 0;
                ActiveGroupIndex = 0;
                ActiveUserIndex = 0;
                btnUserLogin.BackColor = Color.Transparent;
                btnUserLogin.Text = strLogin;
                grpMain.Enabled = false;
                this.WindowState = FormWindowState.Normal;
            }

        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="usrInfo">用户信息</param>
        /// <param name="waitTime">超时限制</param>
        private void GetUserInfo(int loginId,ref USER_MANAGE_INFO usrInfo,int waitTime)
        {
             bool blnGetUserInfo = false;
             ActiveUserID = 0;
             ActiveGroupID = 0;
             ActiveGroupIndex = 0;
             ActiveUserIndex = 0;
            //读取用户信息
            blnGetUserInfo = DHClient.DHQueryUserInfo(loginId, ref  usrInfo, waitTime);
            if (blnGetUserInfo == true)
            {
                trvUserManageList.Nodes.Clear();
                chkRightList.Items.Clear();
                //用户组添加
                foreach (USER_GROUP_INFO gp in userManageInfo.groupList)
                {
                    if (gp.dwID != 0)
                    {
                        trvUserManageList.Nodes.Add(gp.dwID.ToString(), DHClient.DHByteArrayToString(gp.name));
                    }

                }
                //将用户添加到用户组的Node中
                userIDListArray = new uint[200];//最大200用户
                uint userIndex = 0;
                foreach (USER_INFO user in userManageInfo.userList)
                {
                    if (user.dwGroupID != 0 && user.dwID != 0)
                    {
                        userIDListArray[userIndex] = user.dwID;
                        trvUserManageList.Nodes[user.dwGroupID.ToString()].Nodes.Add(userIndex.ToString(), DHClient.DHByteArrayToString(user.name));
                        userIndex += 1;
                    }
                }
                //权限列表[没有选择组或用户时，显示所有可用的权限]
                chkRightList.Items.Clear();//清空所有权限列表
                rightListArray = new LISTITEM[userManageInfo.dwRightNum];
                for (uint i = 0; i < userManageInfo.dwRightNum; i++)
                {
                    rightListArray[i].index = i;//保存权限列表项的索引
                    rightListArray[i].keyID = userManageInfo.rightList[i].dwID;//保存权限列表项的Key值
                    chkRightList.Items.Add(DHClient.DHByteArrayToString(userManageInfo.rightList[i].name), CheckState.Checked);
                }
            }
            else
            {
                MessageBox.Show("获取用户信息失败！", pMsgTitle);
            }
        }
        /// <summary>
        /// 画面加载初始化处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Main_Load(object sender, EventArgs e)
        {
            msgCallBack = new fMessCallBack(GetAlarmMessage);
            disConnect = new fDisConnect(DisConnectEvent);
            AlarmAudio = new byte[16];
            AlarmDiskErr = new byte[32];//ＤＶＲ最大可挂３２块硬盘，每一位表示一个硬盘的状态，0:硬盘状态正常;1:硬盘出错
            AlarmDiskFull = false;
            AlarmShelter = new byte[16];
            AlarmExternal = new byte[16];
            DHClient.DHInit(disConnect, IntPtr.Zero);
            DHClient.DHSetEncoding(LANGUAGE_ENCODING.gb2312);//字符编码格式设置，默认为gb2312字符编码，如果为其他字符编码请设置

            StringUtil.InitControlText(this);
        }
        /// <summary>
        /// 画面处理，[全部]按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFull_Click(object sender, EventArgs e)
        {
            setCheckList(true);            
        }
        /// <summary>
        /// 选择项处理
        /// </summary>
        /// <param name="checkValue"></param>
        private void setCheckList(bool checkValue)
        {
            for (int i = 0; i < chkRightList.Items.Count;i++ )
            {
                chkRightList.SetItemChecked(i, checkValue);
            }
        }

        /// <summary>
        /// 画面处理，[清除]按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            setCheckList(false);
        }

        /// <summary>
        /// 用户选择处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvUserManageList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)//用户组被选中
            {
                setCheckList(false);
                ShowGroupInfo(userManageInfo.groupList[e.Node.Index],true);
                txtUserAddDemo.Text = "";
                txtUserAddName.Text = "";
                txtUserAddPassword.Text = "";
                chkUserReusable.Checked = false;
                //grpUserAdd.Enabled = false;
                ActiveUserID = 0;
                ActiveGroupIndex = (uint)e.Node.Index;
                ActiveUserIndex = 0;
            }
            else//用户被选中
            {
                ShowGroupInfo(userManageInfo.groupList[e.Node.Parent.Index],false);
                setCheckList(false);                
                ShowUserInfo(userManageInfo.userList[int.Parse(e.Node.Name)],true);
                ActiveGroupIndex = (uint)e.Node.Parent.Index;
                ActiveUserIndex = uint.Parse(e.Node.Name);
                grpUserAdd.Enabled = true;
                
            }
        }

        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="blnShowRights">是否显示权限信息</param>
        private void ShowUserInfo(USER_INFO userInfo,bool blnShowRights)
        {
            txtUserAddName.Text = DHClient.DHByteArrayToString(userInfo.name);
            txtUserAddName.ReadOnly = true;
            txtUserAddPassword.Text = DHClient.DHByteArrayToString(userInfo.passWord);
            txtUserAddPassword.ReadOnly = true;
            txtUserAddDemo.Text = DHClient.DHByteArrayToString(userInfo.memo);
            txtUserAddDemo.ReadOnly = true;
            chkUserReusable.Checked=(userInfo.dwReusable ==1?true:false);
            chkUserReusable.AutoCheck =  false;
            ActiveUserID = userInfo.dwID;
            ActiveGroupID = userInfo.dwGroupID;
            if (blnShowRights == true)
            {
                ShowRights(userInfo.rights, userInfo.dwRightNum);
            }
        }
        /// <summary>
        /// 显示组信息
        /// </summary>
        /// <param name="groupInfo">组信息</param>
        /// <param name="blnShowRights">是否显示权限信息</param>
        private void ShowGroupInfo(USER_GROUP_INFO groupInfo, bool blnShowRights)
        {
            ActiveGroupID = groupInfo.dwID;
            txtGroupdemo.Text = DHClient.DHByteArrayToString(groupInfo.memo);
            txtGroupdemo.ReadOnly = true;
            txtGroupName.ReadOnly = true;
            txtGroupName.Text = DHClient.DHByteArrayToString(groupInfo.name);
            if (blnShowRights == true)
            {
                ShowRights(groupInfo.rights, groupInfo.dwRightNum);
            }
        }
        /// <summary>
        /// 显示权限信息
        /// </summary>
        /// <param name="rights">权限信息列表</param>
        /// <param name="rightsNum">权限数</param>
        private void ShowRights(uint[] rights,uint rightsNum)
        {
            
            for (uint i = 0; i < rightsNum; i++)
            {
                foreach (LISTITEM rt in rightListArray)
                {
                    if (rt.keyID == rights[i])
                    {
                        chkRightList.SetItemChecked((int)rt.index, true);
                    }
                }
            }

        }
        /// <summary>
        /// 添加保存用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (ActiveGroupID != 0)
            {
                switch (btnAddUser.Text)
                {
                    case "增加用户":
                        ActiveUserID = 0;
                        grpUserChangePassword.Visible = false;//更改用户密码的控件不显示
                        txtUserAddDemo.Text = "";
                        txtUserAddName.Text = "";
                        txtUserAddPassword.Text = "";
                        chkUserReusable.AutoCheck = true;
                        chkUserReusable.Checked = false;
                        txtUserAddDemo.ReadOnly = false;
                        txtUserAddName.ReadOnly = false;
                        txtUserAddPassword.ReadOnly = false;
                        btnAddUser.BackColor = Color.Yellow;
                        btnAddUser.Text = "保存添加";
                        break;
                    case "保存添加":
                        bool blnAddUser = false;
                        #region << 添加用户信息代码 >>
                        //保存用户信息
                        USER_INFO usrInfo = new USER_INFO("无效参数");//此代码必需，分配指定大小的数组，如passWord,name,meno,rights
                        usrInfo.dwGroupID = userManageInfo.groupList[ActiveGroupIndex].dwID;
                        usrInfo.dwReusable = (uint)(chkUserReusable.Checked == true ? 1 : 0);
                        DHClient.DHStringToByteArry(txtUserAddPassword.Text, ref usrInfo.passWord);
                        DHClient.DHStringToByteArry(txtUserAddName.Text, ref usrInfo.name);
                        DHClient.DHStringToByteArry(txtUserAddDemo.Text, ref  usrInfo.memo);
                        //usrInfo.rights = new uint[100];
                        uint rightsNum = 0;
                        for (int i = 0; i < chkRightList.Items.Count; i++)
                        {
                            if (chkRightList.GetItemChecked(i) == true)
                            {
                                usrInfo.rights[rightsNum] = rightListArray[i].keyID;
                                rightsNum += 1;
                            }
                        }
                        usrInfo.dwRightNum = rightsNum;
                        blnAddUser = DHClient.DHOperateUserInfo(pLoginID, USER_OPERATE.DH_USER_ADD, usrInfo, 3000);
                        #endregion
                        if (blnAddUser == false)//添加用户信息成功                        
                        {
                            MessageBox.Show("保存用户信息失败！\n"  +DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                        }
                        GetUserInfo(pLoginID, ref userManageInfo, 3000);
                        btnAddUser.BackColor = Color.Transparent;
                        txtUserAddDemo.Text = "";
                        txtUserAddName.Text = "";
                        txtUserAddPassword.Text = "";
                        chkUserReusable.Checked = false;
                        btnAddUser.Text = "增加用户";
                        break;
                }
            }
            else
            {
                MessageBox.Show("请选择一个组", pMsgTitle);
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (ActiveUserID != 0)
            {
                switch (btnEditUser.Text)
                {
                    case "修改用户":
                        grpUserChangePassword.Visible = false;//更改用户密码的控件不显示
                        chkUserReusable.AutoCheck = true;
                        txtUserAddDemo.ReadOnly = false;
                        txtUserAddName.ReadOnly = false;
                        txtUserAddPassword.ReadOnly = true;
                        btnEditUser.BackColor = Color.Yellow;
                        btnEditUser.Text = "保存修改";
                        break;
                    case "保存修改":
                        bool blnEditUser = false;
                        #region << 保存修改用户信息代码 >>
                        //保存修改用户信息
                        USER_INFO usrInfo = new USER_INFO("无效参数");
                        usrInfo.dwGroupID = userManageInfo.userList[ActiveUserIndex].dwGroupID;
                        usrInfo.dwID = userManageInfo.userList[ActiveUserIndex].dwID;
                        usrInfo.dwReusable = (uint)(chkUserReusable.Checked == true ? 1 : 0);
                        usrInfo.passWord = userManageInfo.userList[ActiveUserIndex].passWord;
                        DHClient.DHStringToByteArry(txtUserAddName.Text, ref usrInfo.name);
                        DHClient.DHStringToByteArry(txtUserAddDemo.Text, ref  usrInfo.memo);
                        //usrInfo.rights = new uint[100];
                        uint rightsNum = 0;
                        for (int i = 0; i < chkRightList.Items.Count; i++)
                        {
                            if (chkRightList.GetItemChecked(i) == true)
                            {
                                usrInfo.rights[rightsNum] = rightListArray[i].keyID;
                                rightsNum += 1;
                            }
                        }
                        usrInfo.dwRightNum = rightsNum;
                        blnEditUser = DHClient.DHOperateUserInfo(pLoginID, USER_OPERATE.DH_USER_EDIT, usrInfo, userManageInfo.userList[ActiveUserIndex], 3000);
                        #endregion
                        if (blnEditUser == true)//添加用户信息成功
                        {                            
                            GetUserInfo(pLoginID, ref userManageInfo, 3000);
                        }
                        else
                        {
                            MessageBox.Show("修改用户信息失败！\n" + DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                        }
                        btnEditUser.BackColor = Color.Transparent;
                        txtUserAddDemo.Text = "";
                        txtUserAddName.Text = "";
                        txtUserAddPassword.Text = "";
                        chkUserReusable.Checked = false;
                        btnEditUser.Text = "修改用户";
                        ActiveUserID = 0;
                        break;
                }
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelUser_Click(object sender, EventArgs e)
        {
            bool blnDelUser = false;
            if (ActiveUserID == 0)
            {
                MessageBox.Show("请选择用户名",pMsgTitle);
                return;
            }
            if (MessageBox.Show("确认删除该用户？", pMsgTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            { 
                //删除用户代码
                blnDelUser= DHClient.DHOperateUserInfo(pLoginID, USER_OPERATE.DH_USER_DELETE,userManageInfo.userList[ActiveUserIndex], 3000);
                if (blnDelUser == false)
                {
                    MessageBox.Show("删除用户失败！\n" + DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                }
                ActiveUserID = 0;
                GetUserInfo(pLoginID, ref userManageInfo, 3000);
            }
        }
         /// <summary>
         /// 更改密码按钮按下
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (ActiveUserID != 0)
            {
                switch (btnChangePassword.Text)
                {
                    case "修改密码":
                        grpUserChangePassword.Visible = true;
                        txtUserCNewPassword.Text = "";
                        txtUserNewPassword.Text = "";
                        txtUserOldPassword.Text = "";
                        btnChangePassword.Text = "保存修改";
                        btnChangePassword.BackColor = Color.Yellow;
                        break;
                    case "保存修改":
                        bool blnChangePassword = false;
                        if (txtUserCNewPassword.Text.Equals(txtUserNewPassword.Text) == false)
                        {
                            MessageBox.Show("新密码和确认输入的密码不一致！请重新输入新密码", pMsgTitle);
                            txtUserNewPassword.Text = "";
                            txtUserCNewPassword.Text = "";
                            return;
                        }
                        //保存修改用户信息
                        USER_INFO userNewInfo = new USER_INFO("无用参数");
                        DHClient.DHStringToByteArry(txtUserNewPassword.Text, ref userNewInfo.passWord);
                        USER_INFO userOldInfo = new USER_INFO("无用参数");
                        userOldInfo.name = userManageInfo.userList[ActiveUserIndex].name;
                        DHClient.DHStringToByteArry(txtUserOldPassword.Text,ref  userOldInfo.passWord);
                        blnChangePassword = DHClient.DHOperateUserInfo(pLoginID, USER_OPERATE.DH_USER_CHANGEPASSWORD, userNewInfo,userOldInfo, 3000);
                        if (blnChangePassword == false)
                        {
                            MessageBox.Show("密码修改失败！\n"+DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                        }
                        btnChangePassword.Text = "修改密码";
                        btnChangePassword.BackColor = Color.Transparent;
                        break;

                }
            }
            else
            {
                MessageBox.Show("请选择用户!", pMsgTitle);
            }

        }
        /// <summary>
        /// 增加组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            switch (btnAddGroup.Text)
            { 
                case "增加组":
                    ActiveGroupID = 0;
                    ActiveGroupIndex = 0;
                    txtGroupName.Text = "";
                    txtGroupName.ReadOnly = false;
                    txtGroupdemo.Text = "";
                    txtGroupdemo.ReadOnly = false;
                    btnAddGroup.Text = "保存组";
                    btnAddGroup.BackColor = Color.Yellow;
                    break;
                case "保存组":
                    bool blnAddGroup = false;

                    USER_GROUP_INFO groupInfo = new USER_GROUP_INFO("无用参数");
                    DHClient.DHStringToByteArry(txtGroupName.Text,ref  groupInfo.name);
                    DHClient.DHStringToByteArry(txtGroupdemo.Text, ref groupInfo.memo);
                    uint rightsNum = 0;
                    for (int i = 0; i < chkRightList.Items.Count; i++)
                    {
                        if (chkRightList.GetItemChecked(i) == true)
                        {
                            groupInfo.rights[rightsNum] = rightListArray[i].keyID;
                            rightsNum += 1;
                        }
                    }
                    groupInfo.dwRightNum = rightsNum;
                    blnAddGroup = DHClient.DHOperateUserInfo(pLoginID, USER_OPERATE.DH_GROUP_ADD, groupInfo, 3000);
                    if (blnAddGroup == false)
                    {
                        MessageBox.Show("增加组失败！\n" + DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                    }
                    btnAddGroup.Text = "增加组";
                    btnAddGroup.BackColor = Color.Transparent;
                    ActiveUserID = 0;
                    GetUserInfo(pLoginID, ref userManageInfo, 3000);
                    break;
            }
        }
        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelGroup_Click(object sender, EventArgs e)
        {
            bool blnDelGroup = false;
            if (ActiveGroupID == 0)
            {
                MessageBox.Show("请选择用户组！", pMsgTitle);
                return;
            }
            if (MessageBox.Show("确认删除该用户组？", pMsgTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                //删除用户代码
                blnDelGroup=DHClient.DHOperateUserInfo(pLoginID, USER_OPERATE.DH_GROUP_DELETE, userManageInfo.groupList[ActiveGroupIndex], 3000);
                if (blnDelGroup == false)
                {
                    MessageBox.Show("删除组失败！\n" + DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                }
                ActiveGroupID = 0;
                GetUserInfo(pLoginID, ref userManageInfo, 3000);
            }
        }
        /// <summary>
        /// 修改用户组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            if (ActiveGroupID == 0)
            {
                MessageBox.Show("请选择用户组！", pMsgTitle);
                return;
            }
            switch (btnEditGroup.Text)
            { 
                case "修改组":
                    txtGroupName.ReadOnly = false;
                    txtGroupdemo.ReadOnly = false;
                    btnEditGroup.Text = "保存组";
                    btnEditGroup.BackColor = Color.Yellow;
                    break;
                case "保存组":
                    bool blnAddGroup = false;
                    USER_GROUP_INFO groupInfo = new USER_GROUP_INFO("无用参数");
                    groupInfo.dwID = userManageInfo.groupList[ActiveGroupIndex].dwID;                    
                    DHClient.DHStringToByteArry(txtGroupName.Text, ref  groupInfo.name);
                    DHClient.DHStringToByteArry(txtGroupdemo.Text, ref groupInfo.memo);
                    uint rightsNum = 0;
                    for (int i = 0; i < chkRightList.Items.Count; i++)
                    {
                        if (chkRightList.GetItemChecked(i) == true)
                        {
                            groupInfo.rights[rightsNum] = rightListArray[i].keyID;
                            rightsNum += 1;
                        }
                    }
                    groupInfo.dwRightNum = rightsNum;
                    blnAddGroup = DHClient.DHOperateUserInfo(pLoginID, USER_OPERATE.DH_GROUP_EDIT, groupInfo, userManageInfo.groupList[ActiveGroupIndex], 3000);
                    if (blnAddGroup == false)
                    {
                        MessageBox.Show("修改组失败！\n" + DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                       
                    }
                    btnEditGroup.Text = "修改组";
                    btnEditGroup.BackColor = Color.Transparent;
                    ActiveUserID = 0;
                    GetUserInfo(pLoginID, ref userManageInfo, 3000);
                    break;
            }
        }
        /// <summary>
        ///  报警信息处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeDisplayAlarmInfo_Tick(object sender, EventArgs e)
        {
            //显示报警消息
            switch (cbkType)
            {
                case CALLBACK_TYPE.DH_COMM_ALARM://常规报警信息显示
                    SetAlarmControlEnable(clientState.channelcount);
                    #region << 输入报警 >>
                    chkAlarmIn00.Checked = (clientState.alarm[0] == 1 && chkAlarmIn00.Enabled == true ? true : false);
                    chkAlarmIn01.Checked = (clientState.alarm[1] == 1 && chkAlarmIn01.Enabled == true ? true : false);
                    chkAlarmIn02.Checked = (clientState.alarm[2] == 1 && chkAlarmIn02.Enabled == true ? true : false);
                    chkAlarmIn03.Checked = (clientState.alarm[3] == 1 && chkAlarmIn03.Enabled == true ? true : false);
                    chkAlarmIn04.Checked = (clientState.alarm[4] == 1 && chkAlarmIn04.Enabled == true ? true : false);
                    chkAlarmIn05.Checked = (clientState.alarm[5] == 1 && chkAlarmIn05.Enabled == true ? true : false);
                    chkAlarmIn06.Checked = (clientState.alarm[6] == 1 && chkAlarmIn06.Enabled == true ? true : false);
                    chkAlarmIn07.Checked = (clientState.alarm[7] == 1 && chkAlarmIn07.Enabled == true ? true : false);
                    chkAlarmIn08.Checked = (clientState.alarm[8] == 1 && chkAlarmIn08.Enabled == true ? true : false);
                    chkAlarmIn09.Checked = (clientState.alarm[9] == 1 && chkAlarmIn09.Enabled == true ? true : false);
                    chkAlarmIn10.Checked = (clientState.alarm[10] == 1 && chkAlarmIn10.Enabled == true ? true : false);
                    chkAlarmIn11.Checked = (clientState.alarm[11] == 1 && chkAlarmIn11.Enabled == true ? true : false);
                    chkAlarmIn12.Checked = (clientState.alarm[12] == 1 && chkAlarmIn12.Enabled == true ? true : false);
                    chkAlarmIn13.Checked = (clientState.alarm[13] == 1 && chkAlarmIn13.Enabled == true ? true : false);
                    chkAlarmIn14.Checked = (clientState.alarm[14] == 1 && chkAlarmIn14.Enabled == true ? true : false);
                    chkAlarmIn15.Checked = (clientState.alarm[15] == 1 && chkAlarmIn15.Enabled == true ? true : false);
                    #endregion
                    #region << 动态检测 >>
                    chkMotionDetect00.Checked = (clientState.motiondection[0] == 1 && chkMotionDetect00.Enabled == true ? true : false);
                    chkMotionDetect01.Checked = (clientState.motiondection[1] == 1 && chkMotionDetect01.Enabled == true ? true : false);
                    chkMotionDetect02.Checked = (clientState.motiondection[2] == 1 && chkMotionDetect02.Enabled == true ? true : false);
                    chkMotionDetect03.Checked = (clientState.motiondection[3] == 1 && chkMotionDetect03.Enabled == true ? true : false);
                    chkMotionDetect04.Checked = (clientState.motiondection[4] == 1 && chkMotionDetect04.Enabled == true ? true : false);
                    chkMotionDetect05.Checked = (clientState.motiondection[5] == 1 && chkMotionDetect05.Enabled == true ? true : false);
                    chkMotionDetect06.Checked = (clientState.motiondection[6] == 1 && chkMotionDetect06.Enabled == true ? true : false);
                    chkMotionDetect07.Checked = (clientState.motiondection[7] == 1 && chkMotionDetect07.Enabled == true ? true : false);
                    chkMotionDetect08.Checked = (clientState.motiondection[8] == 1 && chkMotionDetect08.Enabled == true ? true : false);
                    chkMotionDetect09.Checked = (clientState.motiondection[9] == 1 && chkMotionDetect09.Enabled == true ? true : false);
                    chkMotionDetect10.Checked = (clientState.motiondection[10] == 1 && chkMotionDetect10.Enabled == true ? true : false);
                    chkMotionDetect11.Checked = (clientState.motiondection[11] == 1 && chkMotionDetect11.Enabled == true ? true : false);
                    chkMotionDetect12.Checked = (clientState.motiondection[12] == 1 && chkMotionDetect12.Enabled == true ? true : false);
                    chkMotionDetect13.Checked = (clientState.motiondection[13] == 1 && chkMotionDetect13.Enabled == true ? true : false);
                    chkMotionDetect14.Checked = (clientState.motiondection[14] == 1 && chkMotionDetect14.Enabled == true ? true : false);
                    chkMotionDetect15.Checked = (clientState.motiondection[15] == 1 && chkMotionDetect15.Enabled == true ? true : false);
                    #endregion
                    #region << 视频丢失 >>
                    chkVideoLost00.Checked = (clientState.videolost[0] == 1 && chkVideoLost00.Enabled == true ? true : false);
                    chkVideoLost01.Checked = (clientState.videolost[1] == 1 && chkVideoLost01.Enabled == true ? true : false);
                    chkVideoLost02.Checked = (clientState.videolost[2] == 1 && chkVideoLost02.Enabled == true ? true : false);
                    chkVideoLost03.Checked = (clientState.videolost[3] == 1 && chkVideoLost03.Enabled == true ? true : false);
                    chkVideoLost04.Checked = (clientState.videolost[4] == 1 && chkVideoLost04.Enabled == true ? true : false);
                    chkVideoLost05.Checked = (clientState.videolost[5] == 1 && chkVideoLost05.Enabled == true ? true : false);
                    chkVideoLost06.Checked = (clientState.videolost[6] == 1 && chkVideoLost06.Enabled == true ? true : false);
                    chkVideoLost07.Checked = (clientState.videolost[7] == 1 && chkVideoLost07.Enabled == true ? true : false);
                    chkVideoLost08.Checked = (clientState.videolost[8] == 1 && chkVideoLost08.Enabled == true ? true : false);
                    chkVideoLost09.Checked = (clientState.videolost[9] == 1 && chkVideoLost09.Enabled == true ? true : false);
                    chkVideoLost10.Checked = (clientState.videolost[10] == 1 && chkVideoLost10.Enabled == true ? true : false);
                    chkVideoLost11.Checked = (clientState.videolost[11] == 1 && chkVideoLost11.Enabled == true ? true : false);
                    chkVideoLost12.Checked = (clientState.videolost[12] == 1 && chkVideoLost12.Enabled == true ? true : false);
                    chkVideoLost13.Checked = (clientState.videolost[13] == 1 && chkVideoLost13.Enabled == true ? true : false);
                    chkVideoLost14.Checked = (clientState.videolost[14] == 1 && chkVideoLost14.Enabled == true ? true : false);
                    chkVideoLost15.Checked = (clientState.videolost[15] == 1 && chkVideoLost15.Enabled == true ? true : false);
                    #endregion
                    break;
                case CALLBACK_TYPE.DH_SHELTER_ALARM://视频遮档报警
                    #region << 视频遮档报警 >>
                    chkShelter00.Checked = (AlarmShelter[0] == 1 && chkShelter00.Enabled == true ? true : false);
                    chkShelter01.Checked = (AlarmShelter[1] == 1 && chkShelter01.Enabled == true ? true : false);
                    chkShelter02.Checked = (AlarmShelter[2] == 1 && chkShelter02.Enabled == true ? true : false);
                    chkShelter03.Checked = (AlarmShelter[3] == 1 && chkShelter03.Enabled == true ? true : false);
                    chkShelter04.Checked = (AlarmShelter[4] == 1 && chkShelter04.Enabled == true ? true : false);
                    chkShelter05.Checked = (AlarmShelter[5] == 1 && chkShelter05.Enabled == true ? true : false);
                    chkShelter06.Checked = (AlarmShelter[6] == 1 && chkShelter06.Enabled == true ? true : false);
                    chkShelter07.Checked = (AlarmShelter[7] == 1 && chkShelter07.Enabled == true ? true : false);
                    chkShelter08.Checked = (AlarmShelter[8] == 1 && chkShelter08.Enabled == true ? true : false);
                    chkShelter09.Checked = (AlarmShelter[9] == 1 && chkShelter09.Enabled == true ? true : false);
                    chkShelter10.Checked = (AlarmShelter[10] == 1 && chkShelter10.Enabled == true ? true : false);
                    chkShelter11.Checked = (AlarmShelter[11] == 1 && chkShelter11.Enabled == true ? true : false);
                    chkShelter12.Checked = (AlarmShelter[12] == 1 && chkShelter12.Enabled == true ? true : false);
                    chkShelter13.Checked = (AlarmShelter[13] == 1 && chkShelter13.Enabled == true ? true : false);
                    chkShelter14.Checked = (AlarmShelter[14] == 1 && chkShelter14.Enabled == true ? true : false);
                    chkShelter15.Checked = (AlarmShelter[15] == 1 && chkShelter15.Enabled == true ? true : false);
                    #endregion
                    break;
                case CALLBACK_TYPE.DH_DISK_FULL_ALARM://硬盘空间不足
                    chkDiskFull.Checked = AlarmDiskFull;
                    break;
                case CALLBACK_TYPE.DH_DISK_ERROR_ALARM://硬盘损坏
                    #region << 硬盘损坏 >>
                    string strTemp="";
                    foreach(byte bt in AlarmDiskErr)
                    {
                        strTemp+=bt.ToString();
                    }
                    labDiskBroken.Text = strTemp;
                    #endregion
                    break;
                case CALLBACK_TYPE.DH_SOUND_DETECT_ALARM://音频报警
                    #region << 音频报警 >>
                    chkAudio00.Checked = (AlarmAudio[0] == 1 && chkAudio00.Enabled == true ? true : false);
                    chkAudio01.Checked = (AlarmAudio[1] == 1 && chkAudio01.Enabled == true ? true : false);
                    chkAudio02.Checked = (AlarmAudio[2] == 1 && chkAudio02.Enabled == true ? true : false);
                    chkAudio03.Checked = (AlarmAudio[3] == 1 && chkAudio03.Enabled == true ? true : false);
                    chkAudio04.Checked = (AlarmAudio[4] == 1 && chkAudio04.Enabled == true ? true : false);
                    chkAudio05.Checked = (AlarmAudio[5] == 1 && chkAudio05.Enabled == true ? true : false);
                    chkAudio06.Checked = (AlarmAudio[6] == 1 && chkAudio06.Enabled == true ? true : false);
                    chkAudio07.Checked = (AlarmAudio[7] == 1 && chkAudio07.Enabled == true ? true : false);
                    chkAudio08.Checked = (AlarmAudio[8] == 1 && chkAudio08.Enabled == true ? true : false);
                    chkAudio09.Checked = (AlarmAudio[9] == 1 && chkAudio09.Enabled == true ? true : false);
                    chkAudio10.Checked = (AlarmAudio[10] == 1 && chkAudio10.Enabled == true ? true : false);
                    chkAudio11.Checked = (AlarmAudio[11] == 1 && chkAudio11.Enabled == true ? true : false);
                    chkAudio12.Checked = (AlarmAudio[12] == 1 && chkAudio12.Enabled == true ? true : false);
                    chkAudio13.Checked = (AlarmAudio[13] == 1 && chkAudio13.Enabled == true ? true : false);
                    chkAudio14.Checked = (AlarmAudio[14] == 1 && chkAudio14.Enabled == true ? true : false);
                    chkAudio15.Checked = (AlarmAudio[15] == 1 && chkAudio15.Enabled == true ? true : false);
                    #endregion                    
                    break;
                case CALLBACK_TYPE.DH_ALARM_ALARM_EX:
                    #region << 外部报警 >>
                    chkAlarmIn00.Checked = (AlarmExternal[0] == 1 && chkAlarmIn00.Enabled == true ? true : false);
                    chkAlarmIn01.Checked = (AlarmExternal[1] == 1 && chkAlarmIn01.Enabled == true ? true : false);
                    chkAlarmIn02.Checked = (AlarmExternal[2] == 1 && chkAlarmIn02.Enabled == true ? true : false);
                    chkAlarmIn03.Checked = (AlarmExternal[3] == 1 && chkAlarmIn03.Enabled == true ? true : false);
                    chkAlarmIn04.Checked = (AlarmExternal[4] == 1 && chkAlarmIn04.Enabled == true ? true : false);
                    chkAlarmIn05.Checked = (AlarmExternal[5] == 1 && chkAlarmIn05.Enabled == true ? true : false);
                    chkAlarmIn06.Checked = (AlarmExternal[6] == 1 && chkAlarmIn06.Enabled == true ? true : false);
                    chkAlarmIn07.Checked = (AlarmExternal[7] == 1 && chkAlarmIn07.Enabled == true ? true : false);
                    chkAlarmIn08.Checked = (AlarmExternal[8] == 1 && chkAlarmIn08.Enabled == true ? true : false);
                    chkAlarmIn09.Checked = (AlarmExternal[9] == 1 && chkAlarmIn09.Enabled == true ? true : false);
                    chkAlarmIn10.Checked = (AlarmExternal[10] == 1 && chkAlarmIn10.Enabled == true ? true : false);
                    chkAlarmIn11.Checked = (AlarmExternal[11] == 1 && chkAlarmIn11.Enabled == true ? true : false);
                    chkAlarmIn12.Checked = (AlarmExternal[12] == 1 && chkAlarmIn12.Enabled == true ? true : false);
                    chkAlarmIn13.Checked = (AlarmExternal[13] == 1 && chkAlarmIn13.Enabled == true ? true : false);
                    chkAlarmIn14.Checked = (AlarmExternal[14] == 1 && chkAlarmIn14.Enabled == true ? true : false);
                    chkAlarmIn15.Checked = (AlarmExternal[15] == 1 && chkAlarmIn15.Enabled == true ? true : false);
                    #endregion   
                    break;
            }
        }

        /// <summary>
        /// 获取报警信息处理
        /// </summary>
        /// <param name="lCommand">命令</param>
        /// <param name="lLoginID">登录ID</param>
        /// <param name="pBuf">缓存</param>
        /// <param name="dwBufLen">缓存数据大小</param>
        /// <param name="pchDVRIP">DVR设备IP地址</param>
        /// <param name="nDVRPort">DVR设备端口</param>
        /// <param name="dwUser">用户数据</param>
        /// <returns></returns>
        private bool GetAlarmMessage(int lCommand, int lLoginID, IntPtr pBuf, UInt32 dwBufLen, IntPtr pchDVRIP, int nDVRPort, UInt32 dwUser)
        {
            bool returnValue = false;
            //MessageBox.Show("HI1");
            
            switch (lCommand)
            { 
                case (int)CALLBACK_TYPE.DH_COMM_ALARM://常规报警信息
                    cbkType = CALLBACK_TYPE.DH_COMM_ALARM;
                    clientState = (NET_CLIENT_STATE)Marshal.PtrToStructure(pBuf, typeof(NET_CLIENT_STATE));
                    #region << 测试代码 >>
                    
                    #endregion
                    break;
                case (int)CALLBACK_TYPE.DH_SHELTER_ALARM://视频遮档报警
                    cbkType = CALLBACK_TYPE.DH_SHELTER_ALARM;
                    for (int i = 0; i < dwBufLen; i++)
                    {
                        AlarmShelter[i] = (byte)Marshal.PtrToStructure((IntPtr)((UInt32)pBuf + i), typeof(byte));
                    }
                    break;
                case (int)CALLBACK_TYPE.DH_DISK_FULL_ALARM://硬盘空间不足
                    cbkType = CALLBACK_TYPE.DH_DISK_FULL_ALARM;
                    AlarmDiskFull = (bool)Marshal.PtrToStructure(pBuf, typeof(bool));
                    break;
                case (int)CALLBACK_TYPE.DH_DISK_ERROR_ALARM://硬盘损坏
                    cbkType = CALLBACK_TYPE.DH_DISK_ERROR_ALARM;
                    for (int i = 0; i < dwBufLen; i++)
                    {
                        AlarmDiskErr[i] = (byte)Marshal.PtrToStructure((IntPtr)((UInt32)pBuf + i), typeof(byte));
                    }
                    break;
                case (int)CALLBACK_TYPE.DH_SOUND_DETECT_ALARM://音频报警
                    cbkType = CALLBACK_TYPE.DH_SOUND_DETECT_ALARM;
                    for (int i = 0; i < dwBufLen; i++)
                    {
                        AlarmAudio[i] = (byte)Marshal.PtrToStructure((IntPtr)((UInt32)pBuf + i), typeof(byte));
                    }
                    break;
                case (int)CALLBACK_TYPE.DH_ALARM_ALARM_EX://外部报警
                    cbkType = CALLBACK_TYPE.DH_ALARM_ALARM_EX;
                    for (int i = 0; i < dwBufLen; i++)
                    {
                        AlarmExternal[i] = (byte)Marshal.PtrToStructure((IntPtr)((UInt32)pBuf + i), typeof(byte));
                    }
                    break;
            }
            return returnValue;
        }
        /// <summary>
        /// 设备断开连接处理
        /// </summary>
        /// <param name="lLoginID"></param>
        /// <param name="pchDVRIP"></param>
        /// <param name="nDVRPort"></param>
        /// <param name="dwUser"></param>
        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            //设备断开连接处理            
            MessageBox.Show("设备用户断开连接",pMsgTitle);
        }
        /// <summary>
        /// 设置界面报警显示控件有效
        /// </summary>
        /// <param name="channelNum">通道数</param>
        private void SetAlarmControlEnable(int channelNum)
        {
            bool blnEnable = false;
            if (channelNum > 0)
            {
                blnEnable = true;
            }
            chkAlarmIn00.Enabled = blnEnable;
            chkShelter00.Enabled = blnEnable;
            chkMotionDetect00.Enabled = blnEnable;
            chkVideoLost00.Enabled = blnEnable;
            chkAudio00.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 1)
            {
                blnEnable = true;
            }
            chkAlarmIn01.Enabled = blnEnable;
            chkShelter01.Enabled = blnEnable;
            chkMotionDetect01.Enabled = blnEnable;
            chkVideoLost01.Enabled = blnEnable;
            chkAudio01.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 2)
            {
                blnEnable = true;
            }
            chkAlarmIn02.Enabled = blnEnable;
            chkShelter02.Enabled = blnEnable;
            chkMotionDetect02.Enabled = blnEnable;
            chkVideoLost02.Enabled = blnEnable;
            chkAudio02.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 3)
            {
                blnEnable = true;
            }
            chkAlarmIn03.Enabled = blnEnable;
            chkShelter03.Enabled = blnEnable;
            chkMotionDetect03.Enabled = blnEnable;
            chkVideoLost03.Enabled = blnEnable;
            chkAudio03.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 4)
            {
                blnEnable = true;
            }
            chkAlarmIn04.Enabled = blnEnable;
            chkShelter04.Enabled = blnEnable;
            chkMotionDetect04.Enabled = blnEnable;
            chkVideoLost04.Enabled = blnEnable;
            chkAudio04.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 5)
            {
                blnEnable = true;
            }
            chkAlarmIn05.Enabled = blnEnable;
            chkShelter05.Enabled = blnEnable;
            chkMotionDetect05.Enabled = blnEnable;
            chkVideoLost05.Enabled = blnEnable;
            chkAudio05.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 6)
            {
                blnEnable = true;
            }
            chkAlarmIn06.Enabled = blnEnable;
            chkShelter06.Enabled = blnEnable;
            chkMotionDetect06.Enabled = blnEnable;
            chkVideoLost06.Enabled = blnEnable;
            chkAudio06.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 7)
            {
                blnEnable = true;
            }
            chkAlarmIn07.Enabled = blnEnable;
            chkShelter07.Enabled = blnEnable;
            chkMotionDetect07.Enabled = blnEnable;
            chkVideoLost07.Enabled = blnEnable;
            chkAudio07.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 8)
            {
                blnEnable = true;
            }
            chkAlarmIn08.Enabled = blnEnable;
            chkShelter08.Enabled = blnEnable;
            chkMotionDetect08.Enabled = blnEnable;
            chkVideoLost08.Enabled = blnEnable;
            chkAudio08.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 9)
            {
                blnEnable = true;
            }
            chkAlarmIn09.Enabled = blnEnable;
            chkShelter09.Enabled = blnEnable;
            chkMotionDetect09.Enabled = blnEnable;
            chkVideoLost09.Enabled = blnEnable;
            chkAudio09.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 10)
            {
                blnEnable = true;
            }
            chkAlarmIn10.Enabled = blnEnable;
            chkShelter10.Enabled = blnEnable;
            chkMotionDetect10.Enabled = blnEnable;
            chkVideoLost10.Enabled = blnEnable;
            chkAudio10.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 11)
            {
                blnEnable = true;
            }
            chkAlarmIn11.Enabled = blnEnable;
            chkShelter11.Enabled = blnEnable;
            chkMotionDetect11.Enabled = blnEnable;
            chkVideoLost11.Enabled = blnEnable;
            chkAudio11.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 12)
            {
                blnEnable = true;
            }
            chkAlarmIn12.Enabled = blnEnable;
            chkShelter12.Enabled = blnEnable;
            chkMotionDetect12.Enabled = blnEnable;
            chkVideoLost12.Enabled = blnEnable;
            chkAudio12.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 13)
            {
                blnEnable = true;
            }
            chkAlarmIn13.Enabled = blnEnable;
            chkShelter13.Enabled = blnEnable;
            chkMotionDetect13.Enabled = blnEnable;
            chkVideoLost13.Enabled = blnEnable;
            chkAudio13.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 14)
            {
                blnEnable = true;
            }
            chkAlarmIn14.Enabled = blnEnable;
            chkShelter14.Enabled = blnEnable;
            chkMotionDetect14.Enabled = blnEnable;
            chkVideoLost14.Enabled = blnEnable;
            chkAudio14.Enabled = blnEnable;
            blnEnable = false;
            if (channelNum > 15)
            {
                blnEnable = true;
            }
            chkAlarmIn15.Enabled = blnEnable;
            chkShelter15.Enabled = blnEnable;
            chkMotionDetect15.Enabled = blnEnable;
            chkVideoLost15.Enabled = blnEnable;
            chkAudio15.Enabled = blnEnable;
            blnEnable = false;            

        }
    }
}