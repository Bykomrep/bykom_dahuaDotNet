using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DHNetSDK;
using System.Runtime.InteropServices;

namespace SDKCapturePicture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.textBoxIP.Text = "10.64.3.114";
            this.textBoxPort.Text = "37777";
            this.textBoxUser.Text = "admin";
            this.textBoxPsw.Text = "admin";
            this.textBoxSerial.Text = "1";
            m_bEnable = false;

            m_nLoginID = 0;
            m_bInit = false;
            this.Login.Enabled = true;
            this.Logout.Enabled = false;
            this.radioButtonOne.Checked = true;

            m_stuSnapCfg = new DHDEV_SNAP_CFG[32];
            for (int i = 0; i < 32; ++i )
            {
                m_stuSnapCfg[i].struSnapEnc = new DH_VIDEOENC_OPT[32];
            }

            m_stuSnapAttr = new DH_SNAP_ATTR_EN();
            m_stuSnapAttr.stuSnap = new DH_QUERY_SNAP_INFO[16];

            m_stuSnapAttrEx = new DH_SNAP_ATTR_EN_EX();
            m_stuSnapAttrEx.m_bQueried = new Int32[32];
            m_stuSnapAttrEx.m_stuSnapAttrEx = new CFG_SNAPCAPINFO_INFO[32];

            //分辨率
            m_dicPix = new Dictionary<string, int>();
            m_dicPix["D1"] = 0;
            m_dicPix["HD1"] = 1;
            m_dicPix["BCIF"] = 2;
            m_dicPix["CIF"] = 3;
            m_dicPix["QCIF"] = 4;
            m_dicPix["VGA"] = 5;
            m_dicPix["QVGA"] = 6;
            m_dicPix["SVCD"] = 7;
            m_dicPix["QQVGA"] = 8;
            m_dicPix["SVGA"] = 9;
            m_dicPix["XVGA"] = 10;
            m_dicPix["WXGA"] = 11;
            m_dicPix["SXGA"] = 12;
            m_dicPix["WSXGA"] = 13;
            m_dicPix["UXGA"] = 14;
            m_dicPix["WUXGA"] = 15;
            m_dicPix["LTF"] = 16;
            m_dicPix["720p"] = 17;
            m_dicPix["1080p"] = 18;
            m_dicPix["1.3M"] = 19;
            m_dicPix["NR"] = 20;

            m_dicQuality = new Dictionary<string, int>();
            m_dicSnapSpace = new Dictionary<string, int>();
            m_dicSnapMode = new Dictionary<string, int>();


            InitSDK();
        }

        private void InitSDK()
        {
            m_disConnect = new fDisConnect(DisConnectEvent);
            m_SnapRecv = new fSnapRev(SnapRev);
            m_bInit = DHClient.DHInit(m_disConnect, IntPtr.Zero);
            if (!m_bInit)
            {
                MessageBox.Show("初始化失败");
            }
            else
            {
                DHClient.DHSetSnapRevCallBack(m_SnapRecv, 0);
            }
                
        }

        private void Login_Click(object sender, EventArgs e)
        {
            if (!m_bInit)
            {
                MessageBox.Show("库初始化失败");
                return;
            }

            //设备用户信息获得
            NET_DEVICEINFO deviceInfo = new NET_DEVICEINFO();
            int error = 0;
            m_nLoginID = DHClient.DHLogin(this.textBoxIP.Text.ToString(), ushort.Parse(this.textBoxPort.Text.ToString()),
                        this.textBoxUser.Text.ToString(), this.textBoxPsw.Text.ToString(), out deviceInfo, out error);

            if (m_nLoginID > 0)
            {
                this.Login.Enabled = false;
                this.Logout.Enabled = true;
                m_nChannelNum = deviceInfo.byChanNum;
                for (int i = 0; i < m_nChannelNum; ++i )
                {
                    this.comboBoxChannel.Items.Add(i.ToString());
                }
                
                //query json ability.
                Int32 dwRetLen = 0;
                IntPtr pDevEnable = new IntPtr();
                pDevEnable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DH_DEV_ENABLE_INFO)));
                bool bRet = DHClient.DHQuerySystemInfo(m_nLoginID, DH_SYS_ABILITY.ABILITY_DEVALL_INFO, pDevEnable,
                                                Marshal.SizeOf(typeof(DH_DEV_ENABLE_INFO)), ref dwRetLen, 1000);
                if (bRet == false)
                {
                    //MessageBox.Show(ConvertString("Query device ability failed."));
                    MessageBox.Show("查询设备能力失败");
                    return;
                }


                DH_DEV_ENABLE_INFO devEnable = new DH_DEV_ENABLE_INFO();
                devEnable = (DH_DEV_ENABLE_INFO)Marshal.PtrToStructure(pDevEnable, typeof(DH_DEV_ENABLE_INFO));
                m_bJSON = devEnable.IsFucEnable[(Int32)DH_FUN_SUPPORT.EN_JSON_CONFIG] > 0 ? true : false;

                 if (m_bJSON == false)
                 {
                     int nRetLen = 0;
                     IntPtr pStuSnapAttr = new IntPtr();
                     pStuSnapAttr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DH_SNAP_ATTR_EN)));
                     bool nRet = DHClient.DHQueryDevState(m_nLoginID, (int)DHClient.DH_DEVSTATE_SNAP
                                                        , pStuSnapAttr, Marshal.SizeOf(typeof(DH_SNAP_ATTR_EN)) , ref nRetLen, 1000);
                     if (nRet == false || nRetLen != Marshal.SizeOf(typeof(DH_SNAP_ATTR_EN)))
                     {
                         MessageBox.Show("获取抓图能力集失败!");
                         return;
                     }
                     else
                     {
                         m_stuSnapAttr = (DH_SNAP_ATTR_EN)Marshal.PtrToStructure(pStuSnapAttr, typeof(DH_SNAP_ATTR_EN));
                     }
                 }
                 else//json
                 {
                     InitSnapConfigExUI(0);
                 }

                 IntPtr pSnapCfg = new IntPtr();
                 pSnapCfg = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DHDEV_SNAP_CFG)) * 32);
                 UInt32 dwRetConfig = 0;
                 bRet = DHClient.DHGetDevConfig(m_nLoginID, CONFIG_COMMAND.DH_DEV_SNAP_CFG, -1, pSnapCfg, (UInt32)Marshal.SizeOf(typeof(DHDEV_SNAP_CFG)) * 32, ref dwRetConfig, 1000);
                 if (!bRet)
                 {
                     MessageBox.Show("获取抓图配置失败!");
                     return;
                 }
                 else
                 {
                     for (int i = 0; i < 32; ++i )
                     {
                         m_stuSnapCfg[i] = (DHDEV_SNAP_CFG)Marshal.PtrToStructure((IntPtr)((UInt32)pSnapCfg+i*Marshal.SizeOf(typeof(DHDEV_SNAP_CFG)))
                                                                    , typeof(DHDEV_SNAP_CFG));
                     }
                 }

                 if (this.comboBoxChannel.Items.Count > 0)
                 {
                     this.comboBoxChannel.SelectedIndex = 0;
                 }

                 this.buttonGet.Enabled = true;
                 this.buttonSet.Enabled = true;
                 this.buttonStart.Enabled = true;
                 this.buttonStop.Enabled = true;
            }
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            if (!m_bInit)
            {
                return;
            }
            if (m_nLoginID > 0 )
            {
                DHClient.DHLogout(m_nLoginID);
                m_nLoginID = 0;
            }

            this.Login.Enabled = true;
            this.Logout.Enabled = false;

            this.comboBoxChannel.Text = "";
            this.comboBoxChannel.Items.Clear();
            this.comboBoxQuality.Text = "";
            this.comboBoxQuality.Items.Clear();
            this.comboBoxRe.Text = "";
            this.comboBoxRe.Items.Clear();
            this.comboBoxSnapMode.Text = "";
            this.comboBoxSnapMode.Items.Clear();
            this.comboBoxSnapSpace.Text = "";
            this.comboBoxSnapSpace.Items.Clear();

            this.buttonGet.Enabled = false;
            this.buttonSet.Enabled = false;
            this.buttonStart.Enabled = false;
            this.buttonStop.Enabled = false;

        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            UInt32 dwRetLen = 0;
            IntPtr pSnapCfg = new IntPtr();
            pSnapCfg = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DHDEV_SNAP_CFG)) * 32);
            bool bRet = DHClient.DHGetDevConfig(m_nLoginID, CONFIG_COMMAND.DH_DEV_SNAP_CFG, 0, pSnapCfg, (UInt32)Marshal.SizeOf(typeof(DHDEV_SNAP_CFG)) * 32, ref dwRetLen, 1000);
            if (!bRet)
            {
                //AfxMessageBox(ConvertString("Get snap configure failed."));
                MessageBox.Show("获取抓图配置失败!");
                return;
            }
            else
            {
                for (int i = 0; i < 32; ++i)
                {
                    m_stuSnapCfg[i] = (DHDEV_SNAP_CFG)Marshal.PtrToStructure((IntPtr)((UInt32)pSnapCfg + i * Marshal.SizeOf(typeof(DHDEV_SNAP_CFG)))
                                                               , typeof(DHDEV_SNAP_CFG));
                }
            }

            OnSelchangeComboChannel(null, null);
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            int nIndex = this.comboBoxChannel.SelectedIndex;// m_ctlChannel.GetCurSel();
            if (nIndex < 0)
            {
                return;
            }
            int nChn = nIndex;// m_ctlChannel.GetItemData(nIndex);

            IntPtr pSnapCfg = new IntPtr();
            pSnapCfg = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DHDEV_SNAP_CFG)) * 32);
            for (int i = 0; i < 32; ++i )
            {
                Marshal.StructureToPtr(m_stuSnapCfg[i]
                    , (IntPtr)((UInt32)pSnapCfg + i * Marshal.SizeOf(typeof(DHDEV_SNAP_CFG))), false);
            }

            bool bRet = DHClient.DHSetDevConfig(m_nLoginID, CONFIG_COMMAND.DH_DEV_SNAP_CFG, nChn, pSnapCfg, (UInt32)Marshal.SizeOf(typeof(DHDEV_SNAP_CFG)) * 32, 1000);
            if (!bRet)
            {
                //AfxMessageBox(ConvertString("Set snap configure failed!"));
                MessageBox.Show("保存抓图配置失败!");
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (0 != m_nLoginID)
            {
                if (m_snapmode == -1)
                {
                    //MessageBox(ConvertString("please select snap mode!"), ConvertString("prompt"));
                    return;
                }

                //Fill in request structure 
                SNAP_PARAMS snapparams = new SNAP_PARAMS();
                snapparams.Channel = (UInt32)this.comboBoxChannel.SelectedIndex;
                snapparams.mode = this.radioButtonOne.Checked?0:1;//m_snapmode>0?0:1;
                snapparams.CmdSerial = UInt32.Parse(this.textBoxSerial.Text.ToString());

                if (snapparams.mode == 1)
                {
                    //Time interval for scheduled snapshot. Use snapshot setup to configure.
                    snapparams.InterSnap = (UInt32)m_dicSnapSpace[this.comboBoxSnapSpace.Text.ToString()];
                }

                bool bRet = DHClient.DHSnapPicture(m_nLoginID, snapparams);
                if (!bRet)
                {
                    MessageBox.Show("抓图启动失败");
                }
                else
                {
                    MessageBox.Show("抓图启动成功");
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (0 == m_nLoginID)
            {
                return;
            }

            //Fill in stop request.
            SNAP_PARAMS snapparams = new SNAP_PARAMS();
            snapparams.mode = -1;
            snapparams.Channel = (UInt32)this.comboBoxChannel.SelectedIndex;
            snapparams.CmdSerial = UInt32.Parse(this.textBoxSerial.Text.ToString());

            bool b = DHClient.DHSnapPicture(m_nLoginID, snapparams);
            if (!b)
            {
                MessageBox.Show("停止抓图失败");
            }
            else
            {
                MessageBox.Show("停止抓图成功");
            }
        }

        private void OnSelchangeComboChannel(object sender, EventArgs e) 
        {
            // TODO: Add your control notification handler code here
            int nIndex = this.comboBoxChannel.SelectedIndex;
            
            if(nIndex < 0)
            {
                return;
            }
            int nChn = nIndex;

            int nSnapMode = 0;
            if (this.comboBoxSnapMode.SelectedIndex>=0)
            {
                nSnapMode = m_dicSnapMode[this.comboBoxSnapMode.Text.ToString()];// m_stuSnapCfg[nChn].bTimingEnable > 0 ? 0 : 1;
            }
            
        	
            if(m_bJSON == false)
            {
                InitSnapConfigUI(nChn, e==null?false:true);
                UpdateSnapConfig(nChn, nSnapMode,  e);
            }
            else
            {
                InitSnapConfigExUI(nChn);
                UpdateSnapConfig(nChn, nSnapMode,  e);
            }
        }

        private void OnSelchangeComboSnapMode(object sender, EventArgs e)
        {
            int nSnapMode = 0;
            int nIndex = this.comboBoxSnapMode.SelectedIndex;// m_ccbSnapMode.GetCurSel();
            if (nIndex >= 0)
            {
                m_snapmode = m_dicSnapMode[this.comboBoxSnapMode.Text.ToString()];
                if (m_snapmode == 1)
                {
                    m_stuSnapCfg[ushort.Parse(this.comboBoxChannel.Text.ToString())].bTimingEnable = 1;
                }
                else
                {
                    m_stuSnapCfg[ushort.Parse(this.comboBoxChannel.Text.ToString())].bTimingEnable = 0;
                }

                OnSelchangeComboChannel(null,null);
            }
        }

        private void OnSelchangeComboSnapSize(object sender, EventArgs e)
        {
            int nSnapMode = 0;
            int nIndex = this.comboBoxSnapMode.SelectedIndex;
            if (nIndex >= 0)
            {
                nSnapMode = m_dicSnapMode[this.comboBoxSnapMode.Text.ToString()];

                nIndex = this.comboBoxRe.SelectedIndex;
                if (nIndex >= 0)
                {
                    int nIndexChl = this.comboBoxChannel.SelectedIndex;
                    if (nIndexChl < 0)
                    {
                        return;
                    }
                    int nChl = nIndexChl;
                    m_stuSnapCfg[nChl].struSnapEnc[nSnapMode].byImageSize = (byte)m_dicPix[this.comboBoxRe.Text.ToString()];//m_ccbSize.GetItemData(nIndex);
                }
            }
        }

        private void OnSelchangeComboSnapSpace(object sender, EventArgs e)
        {
            int nSnapMode = 0;
            int nIndex = this.comboBoxSnapMode.SelectedIndex;
            if (nIndex >= 0)
            {
                nSnapMode = m_dicSnapMode[this.comboBoxSnapMode.Text.ToString()];

                nIndex = this.comboBoxSnapSpace.SelectedIndex;
                if (nIndex >= 0)
                {
                    int nIndexChl = this.comboBoxChannel.SelectedIndex;
                    if (nIndexChl < 0)
                    {
                        return;
                    }
                    int nChl = nIndexChl;
                    m_stuSnapCfg[nChl].struSnapEnc[nSnapMode].byFramesPerSec = (byte)m_dicSnapSpace[this.comboBoxSnapSpace.Text.ToString()];//m_ccbFrameRate.GetItemData(nIndex);
                }
            }
        }

        private void OnSelchangeComboSnapQuality(object sender, EventArgs e)
        {
            int nSnapMode = 0;
            int nIndex = this.comboBoxSnapMode.SelectedIndex;
            if (nIndex >= 0)
            {
                nSnapMode = m_dicSnapMode[this.comboBoxSnapMode.Text.ToString()];

                nIndex = this.comboBoxQuality.SelectedIndex;
                if (nIndex >= 0)
                {
                    int nIndexChl = this.comboBoxChannel.SelectedIndex;
                    if (nIndex < 0)
                    {
                        return;
                    }
                    int nChl = nIndexChl;
                    m_stuSnapCfg[nChl].struSnapEnc[nSnapMode].byImageQlty = (byte)m_dicQuality[this.comboBoxQuality.Text.ToString()];//m_ccbQuality.GetItemData(nIndex);
                }
            }	
        }

        private void OnCheckTimeEnable(object sender, EventArgs e)
        {
            m_bEnable = !m_bEnable;
            int nSnapMode = 0;
            int nIndex = this.comboBoxSnapMode.SelectedIndex;
            if (nIndex >= 0)
            {
                nSnapMode = m_dicSnapMode[this.comboBoxSnapMode.Text.ToString()];
                if (nSnapMode != 0)
                {
                    return;
                }

                nIndex = this.comboBoxChannel.SelectedIndex;
                if (nIndex < 0)
                {
                    return;
                }
                int nChl = nIndex;

                m_stuSnapCfg[nChl].bTimingEnable = (byte)(m_bEnable==true?1:0);
                m_stuSnapCfg[nChl].struSnapEnc[0].byAudioEnable = (byte)(m_bEnable == true ? 1 : 0);
                m_stuSnapCfg[nChl].struSnapEnc[0].byVideoEnable = (byte)(m_bEnable == true ? 1 : 0);
                m_stuSnapCfg[nChl].struSnapEnc[1].byAudioEnable = (byte)(m_bEnable == true ? 1 : 0);
                m_stuSnapCfg[nChl].struSnapEnc[1].byVideoEnable = (byte)(m_bEnable == true ? 1 : 0);
            }
        }

        private void InitSnapConfigUI(int nChn, bool bInitSnapmode)
        {
            //const DH_QUERY_SNAP_INFO& stuSnapInfo = m_stuSnapAttr.stuSnap[nChn];
            

            if (bInitSnapmode)
            {
                this.comboBoxSnapMode.Items.Clear();
                for (int i = 0; i < m_stuSnapAttr.stuSnap[nChn].nSnapModeCount; i++)
                {
                    if (m_stuSnapAttr.stuSnap[nChn].SnapMode[i] == 0)
                    {
                        this.comboBoxSnapMode.Items.Add("定时触发抓图");
                        m_dicSnapMode["定时触发抓图"] = 0;
                    }
                    else if (m_stuSnapAttr.stuSnap[nChn].SnapMode[i] == 1)
                    {
                        this.comboBoxSnapMode.Items.Add("手动触发抓图");
                        m_dicSnapMode["手动触发抓图"] = 1;
                    }
                }
            }
            

            this.comboBoxSnapSpace.Items.Clear();
            string csFrame = "";
            for(int i = 0; i < m_stuSnapAttr.stuSnap[nChn].nFramesCount; i++)
            {
                if(m_stuSnapAttr.stuSnap[nChn].Frames[i] > 0)
                {
                    csFrame = string.Format("{0:n0} 秒1帧", m_stuSnapAttr.stuSnap[nChn].Frames[i]);
                }
                else if(m_stuSnapAttr.stuSnap[nChn].Frames[i] < 0)
                {
                    csFrame = string.Format("{0:n0} 秒1帧", Math.Abs(m_stuSnapAttr.stuSnap[nChn].Frames[i]));
                }
                this.comboBoxSnapSpace.Items.Add(csFrame);
                m_dicSnapSpace[csFrame] = m_stuSnapAttr.stuSnap[nChn].Frames[i];
            }


            UInt32 dwMask = 0x0001;
            this.comboBoxRe.Items.Clear();
            for (int i = 0; i < 32; i++)
            {
                if ((m_stuSnapAttr.stuSnap[nChn].dwVideoStandardMask & dwMask) > 0)
                {
                    foreach (KeyValuePair<string,int> temp in m_dicPix)
                    {
                        if (temp.Value == i)
                        {
                            this.comboBoxRe.Items.Add(temp.Key);
                        }
                    }
                }
                dwMask <<= 1;
            }
        	
	        string csQuality;
	        //m_ccbQuality.ResetContent();
            this.comboBoxQuality.Items.Clear();
	        for(int i = 0; i < m_stuSnapAttr.stuSnap[nChn].nPicQualityCount; i++)
	        {
		        csQuality = string.Format("{0:n0}%", m_stuSnapAttr.stuSnap[nChn].PictureQuality[i]);
                this.comboBoxQuality.Items.Add(csQuality);
                m_dicQuality[csQuality] = m_stuSnapAttr.stuSnap[nChn].PictureQuality[i];
	        }
        }

        private void UpdateSnapConfig(int nChn, int nSnapMode, EventArgs e)
        {
	        if(nChn < 0 || nChn > 32 || nSnapMode < 0 || nSnapMode > 1)
	        {
		        return;
	        }
        	
	        
            int nCount = 0;
            if (e!=null)
            {
                 nCount = this.comboBoxSnapMode.Items.Count;// m_ccbSnapMode.GetCount();
                if (nCount > 0 && nCount >= nSnapMode)
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        if (i == nSnapMode)
                        {
                            this.comboBoxSnapMode.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            
            

            nCount = this.comboBoxSnapSpace.Items.Count;// m_ccbFrameRate.GetCount();
            foreach (KeyValuePair<string,int> temp in m_dicSnapSpace)
            {
                if (temp.Value == (sbyte)m_stuSnapCfg[nChn].struSnapEnc[nSnapMode].byFramesPerSec)
                {
                    this.comboBoxSnapSpace.SelectedIndex = this.comboBoxSnapSpace.FindString(temp.Key);
                    break;
                }
                if (Math.Abs(m_stuSnapCfg[nChn].struSnapEnc[nSnapMode].byFramesPerSec) == Math.Abs(temp.Value))
                {
                    this.comboBoxSnapSpace.SelectedIndex = this.comboBoxSnapSpace.FindString(temp.Key);
                    break;
                }
            }

            foreach (KeyValuePair<string, int> temp in m_dicPix)
            {
                if (temp.Value == m_stuSnapCfg[nChn].struSnapEnc[nSnapMode].byImageSize)
                {
                    this.comboBoxRe.SelectedIndex = this.comboBoxRe.FindString(temp.Key);
                    break;
                }
            }

            foreach (KeyValuePair<string, int> temp in m_dicQuality)
            {
                if (temp.Value == m_stuSnapCfg[nChn].struSnapEnc[nSnapMode].byImageQlty)
                {
                    this.comboBoxQuality.SelectedIndex = this.comboBoxQuality.FindString(temp.Key);
                    break;
                }
            }
        }
            
        private void  InitSnapConfigExUI(int nChn)
        {
             if(m_stuSnapAttrEx.m_bQueried[nChn] == 0)
             {
                 object result = new object();
                 UInt32 dwRetLen = 0;
                 bool bRet = bRet = DHClient.DHGetNewDevConfig(m_nLoginID, 0,
                     DHClient.CFG_CMD_SNAPCAPINFO, ref result, typeof(CFG_SNAPCAPINFO_INFO), 0, 3000);
                 if(bRet == false)
                 {
                     MessageBox.Show("获取设备能力集失败");
                     return;
                 }

                 m_stuSnapAttrEx.m_stuSnapAttrEx[nChn] = (CFG_SNAPCAPINFO_INFO)result;
                 m_stuSnapAttrEx.m_bQueried[nChn] = 1;
             }

             if(m_stuSnapAttrEx.m_bQueried[nChn] == 0)
             {
                 return;
             }
             
              CFG_SNAPCAPINFO_INFO  stuSnapInfo = m_stuSnapAttrEx.m_stuSnapAttrEx[nChn];
              int i = 0;

              this.comboBoxSnapMode.Items.Clear();
              if((stuSnapInfo.dwMode&0x00000001) > 0)
              {	
                  this.comboBoxSnapMode.Items.Add("定时触发抓图");
                  m_dicPix["定时触发抓图"] = 0;
              }
              if((stuSnapInfo.dwMode&0x00000002) > 0)
              {
                  this.comboBoxSnapMode.Items.Add("手动触发抓图");
                  m_dicPix["手动触发抓图"] = 1;
              }

             //
              string csFrame = "";
              this.comboBoxSnapSpace.Items.Clear();
              for(i = 0; i < stuSnapInfo.dwFramesPerSecNum; i++)
              {
                  if(stuSnapInfo.nFramesPerSecList[i] > 0)
                  {
                      csFrame = string.Format("{0:n0} 秒1帧", stuSnapInfo.nFramesPerSecList[i]);
                  }
                  else if(stuSnapInfo.nFramesPerSecList[i] < 0)
                  {
                      csFrame = string.Format("{0:n0} 秒1帧", Math.Abs(stuSnapInfo.nFramesPerSecList[i]));
                  }
                  m_dicSnapSpace[csFrame] = stuSnapInfo.nFramesPerSecList[i];
              }
          // 
          // 		IMAGE_SIZE_D1,								// 704*576(PAL)  704*480(NTSC)
          // 		IMAGE_SIZE_HD1,								// 352*576(PAL)  352*480(NTSC)
          // 		IMAGE_SIZE_BCIF,							// 704*288(PAL)  704*240(NTSC)
          // 		IMAGE_SIZE_CIF,								// 352*288(PAL)  352*240(NTSC)
          // 		IMAGE_SIZE_QCIF,							// 176*144(PAL)  176*120(NTSC)
          // 		IMAGE_SIZE_VGA,								// 640*480
          // 		IMAGE_SIZE_QVGA,							// 320*240
          // 		IMAGE_SIZE_SVCD,							// 480*480
          // 		IMAGE_SIZE_QQVGA,							// 160*128
          // 		IMAGE_SIZE_SVGA,							// 800*592
          // 		IMAGE_SIZE_XVGA,							// 1024*768
          // 		IMAGE_SIZE_WXGA,							// 1280*800
          // 		IMAGE_SIZE_SXGA,							// 1280*1024  
          // 		IMAGE_SIZE_WSXGA,							// 1600*1024  
          // 		IMAGE_SIZE_UXGA,							// 1600*1200
          // 		IMAGE_SIZE_WUXGA,							// 1920*1200
          // 		IMAGE_SIZE_LTF,								// 240*192
          // 		IMAGE_SIZE_720,								// 1280*720
          // 		IMAGE_SIZE_1080,							// 1920*1080
          // 		IMAGE_SIZE_1_3M,							// 1280*960
          // 		IMAGE_SIZE_NR  

              //
              UInt32 dwMask = 0x0001;
              this.comboBoxRe.Items.Clear();
              for (i = 0; i < 32; i++)
              {
                  if ((m_stuSnapAttr.stuSnap[nChn].dwVideoStandardMask & dwMask) > 0)
                  {
                      foreach (KeyValuePair<string, int> temp in m_dicPix)
                      {
                          if (temp.Value == i)
                          {
                              this.comboBoxRe.Items.Add(temp.Key);
                          }
                      }
                  }
                  dwMask <<= 1;
              }
             
              //
              //IMAGE_QUALITY_Q10 = 1,							// 图像质量10%
              //IMAGE_QUALITY_Q30,								// 图像质量30%
              //IMAGE_QUALITY_Q50,								// 图像质量50%
              //IMAGE_QUALITY_Q60,								// 图像质量60%
              //IMAGE_QUALITY_Q80,								// 图像质量80%
              //IMAGE_QUALITY_Q100,								// 图像质量100%
               int[] nMapQuality = {0, 10, 30, 50, 60, 80, 100};
              string csQuality = "";
              this.comboBoxQuality.Items.Clear();
              for(i = 0; i < stuSnapInfo.dwQualityMun; i++)
              {
                  if (stuSnapInfo.emQualityList[i] > 0 && (int)(stuSnapInfo.emQualityList[i]) < 7)
                  {
                      csQuality = string.Format("{0:n0}%", nMapQuality[(int)(stuSnapInfo.emQualityList[i])]);
                  }
                  
                  this.comboBoxQuality.Items.Add(csQuality);
                  m_dicQuality[csQuality] = m_stuSnapAttr.stuSnap[nChn].PictureQuality[i];
              }
        }

        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            MessageBox.Show("设备断开！");
        }

        private int m_nCountPic;//用户图片计数
        private void SnapRev(Int32 lLoginID, IntPtr pBuf, UInt32 RevLen, UInt32 EncodeType, UInt32 CmdSerial, UInt32 dwUser)
        {
            byte[] buf = new byte[RevLen];
            Marshal.Copy(pBuf, buf, 0, (int)RevLen);

            string strName = string.Format("test{0:n0}.jpg", m_nCountPic++);
            // Create the file.
            using (System.IO.FileStream fs = System.IO.File.Create(strName))
            {
                //Marshal.read
                fs.Write(buf, 0, (int)RevLen);
                fs.Close();

                MessageBox.Show("获取的图片在本工作目录下，图片名为"+strName);
            }

            return ;
        }


        private int m_nLoginID;     //登陆设备的ID
        private bool m_bInit;       //库是否初始化成功
        private int m_nChannelNum;  //设备的通道个数
        private bool m_bJSON;       //
        private bool m_bEnable;

        private int m_snapmode;

        private Dictionary<string, int> m_dicPix;//分辨率
        private Dictionary<string, int> m_dicQuality;//图像质量
        private Dictionary<string, int> m_dicSnapSpace;//抓图间隔
        private Dictionary<string, int> m_dicSnapMode;//抓图模式

        private DH_SNAP_ATTR_EN m_stuSnapAttr;
        private DH_SNAP_ATTR_EN_EX m_stuSnapAttrEx;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        private DHDEV_SNAP_CFG[] m_stuSnapCfg;

        // 委托
        private fDisConnect m_disConnect;
        private fSnapRev m_SnapRecv;

        public struct DH_SNAP_ATTR_EN_EX
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
	        public Int32[] m_bQueried;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
	        public CFG_SNAPCAPINFO_INFO[]	m_stuSnapAttrEx;
        };
    }
}