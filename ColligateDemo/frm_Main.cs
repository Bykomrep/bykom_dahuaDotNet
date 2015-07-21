using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DHPlaySDK;
using DHNetSDK;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;
using DaHuaNetSDKSample;

namespace ColligateDemo
{
    public partial class frm_Main : Form
    {
        private NET_DEVICEINFO deviceInfo;

        private int pLoginID;
        private const string pMsgTitle = "Error";
        private const string pErrInfoFormatStyle = "ErrInfoFormatStyle.";
        private int uRealHandle;
        private int[] pRealPlayHandle;
        private fRealDataCallBack cbRealData;
        private fDisConnect disConnect;
        private bool blnSetRealDataCallBack = false;
        
        
        public string ip = "";
        public ushort port = 0;
        public string user = "";
        public string pass = "";
        public int channel = 0;
        public string caption = "";
        public string cuenta_id = "";
        public string sys2015 = "";
        public string mysql_DB_name = "";
        public string  mysql_DB_host = "";
        public int mysql_DB_port = 3306;
        public string mysql_DB_user = "";
        public string mysql_DB_password = "";
        public int cantidadVentanas = 1;
        int cantidadCamaras = 0;
        private COLOR_STRUCT pColor = new COLOR_STRUCT();

        public frm_Main()
        {
            InitializeComponent();

        }

        private bool salvarFotoEnDB(byte[] rawData)
        {
            

            string connectionString = "SERVER=" + this.mysql_DB_host + ";" + "DATABASE=" +
            this.mysql_DB_name + ";" + "UID=" + this.mysql_DB_user + ";" + "PASSWORD=" + this.mysql_DB_password + ";";
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO evrlimagenes(order_rl, sys2015, imagen, extension, camara) VALUES (?order_rl, ?sys2015, ?imagen, 'JPG', ?camara)";
                MySqlParameter bykomCuentaId = new MySqlParameter("?order_rl", MySqlDbType.Int32, 11);
                MySqlParameter bykomSys2015 = new MySqlParameter("?sys2015", MySqlDbType.VarChar, 256);
                MySqlParameter bykomFileParameter = new MySqlParameter("?imagen", MySqlDbType.Blob, rawData.Length);
                MySqlParameter bykomCamara = new MySqlParameter("?camara", MySqlDbType.Int32, 11);

                bykomCuentaId.Value = Int32.Parse(this.cuenta_id);
                bykomSys2015.Value = this.sys2015;
                bykomFileParameter.Value = rawData;
                bykomCamara.Value = this.channel + 1;

                command.Parameters.Add(bykomCuentaId);
                command.Parameters.Add(bykomSys2015);
                command.Parameters.Add(bykomFileParameter);
                command.Parameters.Add(bykomCamara);

                connection.Open();
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar foto en DB: " + ex.Message, pMsgTitle);
                return false;
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Error grave: " + ex1.Message, pMsgTitle);
                return false;
            }
            finally
            {
                try
                {
                    connection.Close();
                }
                catch (MySqlException ex){}
            }

            return true;
        }

        private void play(int canal)
        {              
           bool canStop = DHClient.DHStopRealPlay(pRealPlayHandle[this.channel]);
            picPlayMain.Refresh();
            // pRealPlayHandle[canal] = DHClient.DHRealPlay(pLoginID, canal, picPlayMain.Handle);
            //Seteo de calidad (mainstream)
            REALPLAY_TYPE emStreamType = REALPLAY_TYPE.DH_RType_RealPlay;


            if (comboBoxCalidad.SelectedIndex == 0)
            {
                emStreamType = REALPLAY_TYPE.DH_RType_RealPlay_0;
            }
            else
            {
                emStreamType = REALPLAY_TYPE.DH_RType_RealPlay_1;
            }


            pRealPlayHandle[canal] = DHClient.DHRealPlayEx(pLoginID, canal, emStreamType, picPlayMain.Handle);

            
            this.channel = canal;

            getAjustesVideo();
           
        }

        Dictionary<int, PictureBox> pantallas = new Dictionary<int, PictureBox>();
        private void play2(int canal)
        {
            PictureBox p = pantallas[canal];
            pRealPlayHandle[canal] = DHClient.DHRealPlayEx(pLoginID, canal, REALPLAY_TYPE.DH_RType_RealPlay_1, p.Handle);
            //pRealPlayHandle[canal] = DHClient.DHRealPlay(pLoginID, canal, p.Handle);
            /*int xxx = 0;
            if (pRealPlayHandle[canal] == 0 && xxx < 5)
            {
                pRealPlayHandle[canal] = DHClient.DHRealPlayEx(pLoginID, canal, REALPLAY_TYPE.DH_RType_RealPlay_1, p.Handle);
                xxx++;
                Thread.Sleep(200);
            }*/
            //pantallas.Add(canal, p);
        }
        private void initPantallas(int cantidadVentanas)
        {
            
            pantallas.Clear();
            if (cantidadVentanas == 1)
            {
                pantallas.Add(0, picPlayMain);
            }
            else if (cantidadVentanas == 4)
            {
                pantallas.Add(0, pictureBox4_1);
                pantallas.Add(1, pictureBox4_2);
                pantallas.Add(2, pictureBox4_3);
                pantallas.Add(3, pictureBox4_4);
            }
            else if (cantidadVentanas == 9)
            {
                pantallas.Add(0, pictureBox9_1);
                pantallas.Add(1, pictureBox9_2);
                pantallas.Add(2, pictureBox9_3);
                pantallas.Add(3, pictureBox9_4);
                pantallas.Add(4, pictureBox9_5);
                pantallas.Add(5, pictureBox9_6);
                pantallas.Add(6, pictureBox9_7);
                pantallas.Add(7, pictureBox9_8);
                pantallas.Add(8, pictureBox9_9);
                

            }
            else if (cantidadVentanas == 16)
            {
                pantallas.Add(0, pictureBox1);
                pantallas.Add(1, pictureBox2);
                pantallas.Add(2, pictureBox3);
                pantallas.Add(3, pictureBox4);
                pantallas.Add(4, pictureBox5);
                pantallas.Add(5, pictureBox6);
                pantallas.Add(6, pictureBox7);
                pantallas.Add(7, pictureBox8);
                pantallas.Add(8, pictureBox9);
                pantallas.Add(9, pictureBox10);
                pantallas.Add(10, pictureBox11);
                pantallas.Add(11, pictureBox12);
                pantallas.Add(12, pictureBox13);
                pantallas.Add(13, pictureBox14);
                pantallas.Add(14, pictureBox15);
                pantallas.Add(15, pictureBox16);
            }
        }
        private void login(int cantidadVentanas)
        {
            //this.initPantallas(cantidadVentanas);
            //this.inicializarVentanas(cantidadVentanas);
          
            int error = 0;
            if (pLoginID == 0)
            {
                deviceInfo = new NET_DEVICEINFO();
                pLoginID = DHClient.DHLogin(this.ip, this.port, this.user, this.pass, out deviceInfo, out error);
            }
            if (pLoginID != 0)
            {
                this.startButtons(false);

                pRealPlayHandle = new int[deviceInfo.byChanNum];
                cantidadCamaras = deviceInfo.byChanNum;
                comboBoxCamaras.Items.Clear();
                for (int i = 0; i < cantidadCamaras; i++)
                {
                    comboBoxCamaras.Items.Add((i + 1).ToString());
                }
                
                //this.play(this.channel);

                comboBoxCamaras.SelectedIndex = this.channel;
                comboBoxVentanas.SelectedIndex = this.getIndexCantidadVentanas(cantidadVentanas);
                realPlay();

               
            }
            else
            {
                /*btnStartRealPlay.Enabled = false;
                btnLogin.Enabled = true;
                btnLogout.Enabled = false;*/
                this.startButtons(true);
                if (DHClient.LastOperationInfo.errCode == "-2147483548")
                {
                    MessageBox.Show("Error de conexion a la camara: Password incorrecto!", pMsgTitle);
                }
                else if (DHClient.LastOperationInfo.errCode == "-2147483547")
                {
                    MessageBox.Show("Error de conexion a la camara: Usuario incorrecto!", pMsgTitle);
                }
                else if (DHClient.LastOperationInfo.errCode == "-2147483541")
                {
                    MessageBox.Show("Error de conexion a la camara! No fue posible conectar a la IP o puerto especificado", pMsgTitle);
                }
                else
                {
                    MessageBox.Show("Error de conexion a la camara:" + DHClient.LastOperationInfo.errCode, pMsgTitle);
                }


                
                Application.Exit();
            }
        }

        private void realPlay()
        {
            if (cantidadVentanas == 1)
            {
                play(this.channel);               
            }
            else
            {
                for (int i = 0; i < cantidadCamaras; i++)
                {
                    if (i < cantidadVentanas)
                    {
                        this.play2(i);
                        //Thread.Sleep(2000);
                    }
                }
            }
        }

        private int getIndexCantidadVentanas(int cantidadVentanas)
        {
            if (cantidadVentanas == 1) return 0;
            if (cantidadVentanas == 4) return 1;
            if (cantidadVentanas == 9) return 2;
            if (cantidadVentanas == 16) return 3;
            return 0;
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            /*frm_AddDevice fAddDev = new frm_AddDevice();
            fAddDev.ShowDialog();
            //DHClient.DHSetShowException(true);
            try
            {
                if (fAddDev.blnOKEnter == true)
                {
                    //设备用户信息获得
                    deviceInfo = new NET_DEVICEINFO();
                    int error = 0;
                    //设备用户登录
                    pLoginID = DHClient.DHLogin(fAddDev.cmbDevIP.Text.ToString(), ushort.Parse(fAddDev.txtDevProt.Text.ToString()), fAddDev.txtName.Text.ToString(), fAddDev.txtPassword.Text.ToString(), out deviceInfo, out error);
                    if (pLoginID != 0)
                    {

                        btnStartRealPlay.Enabled = true;
                        btnLogin.Enabled = false;
                        btnLogout.Enabled = true;
                    }
                    else
                    {
                        btnStartRealPlay.Enabled = false;
                        btnLogin.Enabled = true;
                        btnLogout.Enabled = false;
                        MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                    }
                }
            }
            catch
            {
                //报最后一次操作的错误信息
                MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
            }*/
        }

        /// <summary>
        /// 实时监视数据回调数据处理
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwDataType"></param>
        /// <param name="pBuffer"></param>
        /// <param name="dwBufSize"></param>
        /// <param name="dwUser"></param>
        private void cbRealDataFun(int lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr dwUser)
        {
            DHPlay.DHPlayControl(PLAY_COMMAND.InputData, 0,pBuffer,dwBufSize);//此处第二个参数nPort参数要与网络实时监控数据的取得的RealHandle对应
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            cargarToolTipes();
            groupBoxVentanas1.Location = new Point(4, 4);
            groupBoxVentanas4.Location = groupBoxVentanas1.Location;
            groupBoxVentanas9.Location = groupBoxVentanas1.Location;
            groupBoxVentanas16.Location = groupBoxVentanas1.Location;
            groupBoxRecord.Location = groupBoxVentanas1.Location;

            //se cargan las calidades disponibles
            this.comboBoxCalidad.Items.Insert(0, "Alta");
            this.comboBoxCalidad.Items.Insert(1, "Baja");

           
            this.comboBoxCalidad.SelectedIndex = 0;//eliminar esta línea cuando este el parámetro

            //setea calidad segun parámetro
            // comboBoxCalidad.SelectedIndex = qualityCam;

            //estado boton records según parámetro
            /*
            if (bykomCanViewRecords == 1)
            {
                btnGrabaciones.Enabled = true;
            }
            else
            {
                btnGrabaciones.Enabled = false;
            }
           */
           

           disConnect = new fDisConnect(DisConnectEvent);
           bool blnInit=  DHClient.DHInit(disConnect, IntPtr.Zero);
           login(this.cantidadVentanas);
        }

        private void cargarToolTipes()
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
        }

        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            MessageBox.Show("Se perdio la conexion con la camara!", pMsgTitle);
            //this.stop();
            btnCancelar_Click(null, null);
            stop();            
            cantidadVentanas = Int16.Parse(comboBoxVentanas.SelectedItem + "");
            login(cantidadVentanas);
        }

        private void stop()
        {
            this.startButtons(true);
            for (int i = 0; i < pRealPlayHandle.Length; i++)
            {
                
                if (pRealPlayHandle[i] != 0)
                {
                    bool pudo = DHClient.DHStopRealPlay(pRealPlayHandle[i]);
                    /*int xxx = 0;
                    if (!pudo && xxx < 5)
                    {
                        pudo = DHClient.DHStopRealPlayEx(pRealPlayHandle[i]);
                        xxx++;
                    }*/
                }
                

                pRealPlayHandle[i] = 0;
                
            }
            Thread.Sleep(200);
            for (int i = 0; i < cantidadVentanas; i++)
            {
                if (i < pantallas.Count) pantallas[i].Refresh();
                
            }
            //groupBoxVentanas16.Visible = false;
            //groupBoxVentanas16.Visible = true;
            /*for (int i = 0; i < cantidadVentanas; i++)
            {
                if (i < pantallas.Count) pantallas[i].Refresh();

            }
            for (int i = 0; i < cantidadVentanas; i++)
            {
                if (i < pantallas.Count) pantallas[i].Refresh();

            }
            for (int i = 0; i < cantidadVentanas; i++)
            {
                if (i < pantallas.Count) pantallas[i].Refresh();

            }*/

            DHClient.DHLogout(pLoginID);
            
            DHClient.DHCleanup();
            DHClient.DHStopService(pLoginID);
            pLoginID = 0;

            
        }

        private void startButtons(bool init)
        {
            if (init)
            {
                btnPlay.Enabled = true;
                btnGrabaciones.Enabled = false;
                btnStop.Enabled = false;
                comboBoxVentanas.Enabled = true;
                this.btnTomarFoto.Enabled = false;
            }
            else
            {
                comboBoxVentanas.Enabled = false;
                btnGrabaciones.Enabled = true;
                btnPlay.Enabled = false;
                btnStop.Enabled = true;
                if (this.sys2015 != "")
                {
                    this.btnTomarFoto.Enabled = true;
                }
                
            }
        }
        private void btnStartRealPlay_Click(object sender, EventArgs e)
        {
            /*
            if (pLoginID != 0)
            {

                uRealHandle = DHClient.DHRealPlay(pLoginID, 0, IntPtr.Zero);//只取数据不播放
                cbRealData = new fRealDataCallBack(cbRealDataFun);
                if (DHPlay.DHPlayControl(PLAY_COMMAND.OpenStream, 0, IntPtr.Zero, 0, (UInt32)(900 * 1024)))
                {
                    // MessageBox.Show("打开流播放成功！", pMsgTitle);
                }
                else
                {
                    MessageBox.Show("打开流播放失败！", pMsgTitle);
                    btnStopRealPlay.Enabled = false;
                    return;
                }

                if (DHPlay.DHSetStreamOpenMode(0, PLAY_MODE.STREAME_REALTIME))//设置流播放模式
                {
                    //MessageBox.Show("设置流播放模式成功！", pMsgTitle);
                }
                else
                {
                    MessageBox.Show("设置流播放模式失败！", pMsgTitle); 
                    btnStopRealPlay.Enabled = false;
                    return;
                }
                if (DHPlay.DHPlayControl(PLAY_COMMAND.Start, 0, picPlayMain.Handle))
                {
                    // MessageBox.Show("流播放开始成功！", pMsgTitle);
                }
                else
                {
                    MessageBox.Show("流播放开始失败！", pMsgTitle);
                    btnStopRealPlay.Enabled = false;
                    return;
                }
                if (blnSetRealDataCallBack == true)
                {
                    btnStopRealPlay.Enabled = true;
                    btnStartRealPlay.Enabled = false;
                    return;
                }
                if (DHClient.DHSetRealDataCallBack(uRealHandle, cbRealData, IntPtr.Zero))//设置数据回调处理函数
                {
                    // MessageBox.Show("设置数据回调处理函数成功！", pMsgTitle);
                    blnSetRealDataCallBack = true;
                }
                else
                {
                    MessageBox.Show("设置数据回调处理函数失败！", pMsgTitle);
                    btnStopRealPlay.Enabled = false;
                    blnSetRealDataCallBack = false;
                    return;
                }
                btnStopRealPlay.Enabled = true;
                btnStartRealPlay.Enabled = false;
                //picPlayMain.Refresh();
            }    
             */
        }

        private void btnVDC_Click(object sender, EventArgs e)
        {

            frm_VDCDemo fVDC = new frm_VDCDemo();
            fVDC.ShowDialog();
        }

        private void btnTomarFoto_Click(object sender, EventArgs e)
        {
            string bmpPath = Application.StartupPath + @"\DH_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp";
            
            if (pRealPlayHandle[this.channel] == 0)
            {
                MessageBox.Show("Manejador no encontrado!", pMsgTitle);
                return;
            }
            if (DHClient.DHCapturePicture(pRealPlayHandle[this.channel], bmpPath))
            {
                //MessageBox.Show("Foto tomada con exito: " + bmpPath, pMsgTitle);
                byte[] rawData = File.ReadAllBytes(bmpPath);
                if (this.salvarFotoEnDB(rawData))
                {
                    MessageBox.Show("Foto tomada con éxito!", "Info");
                }
                File.Delete(bmpPath);
            }
            else
            {
                MessageBox.Show("Error al tomar foto en DHCapturePicture", pMsgTitle);
            }
        }

        private void comboBoxCamaras_SelectedValueChanged(object sender, EventArgs e)
        {
            int canal = Int16.Parse(comboBoxCamaras.SelectedItem+"")-1;
            this.play(canal);
        }

        private void inicializarVentanas(int cantidadVentanas)
        {
            this.initPantallas(cantidadVentanas);
            if (cantidadVentanas == 1)
            {
                groupBoxVentanas1.Visible = true;
                groupBoxVentanas4.Visible = false;
                groupBoxVentanas9.Visible = false;
                groupBoxVentanas16.Visible = false;
                
                comboBoxCamaras.Enabled = true;
            } 
            else 
            {
                if (cantidadVentanas == 4)
                {
                    groupBoxVentanas1.Visible = false;
                    groupBoxVentanas4.Visible = true;
                    groupBoxVentanas9.Visible = false;
                    groupBoxVentanas16.Visible = false;
                    
                    comboBoxCamaras.Enabled = false;
                }
                else if (cantidadVentanas == 9)
                {
                    groupBoxVentanas1.Visible = false;
                    groupBoxVentanas4.Visible = false;
                    groupBoxVentanas9.Visible = true;
                    groupBoxVentanas16.Visible = false;

                    comboBoxCamaras.Enabled = false;
                }
                else if (cantidadVentanas == 16)
                {
                    groupBoxVentanas1.Visible = false;
                    groupBoxVentanas4.Visible = false;
                    groupBoxVentanas9.Visible = false;
                    groupBoxVentanas16.Visible = true;

                    comboBoxCamaras.Enabled = false;
                }
                PictureBox p = pantallas[this.channel];
                seleccionCamara(p);
            }
        }
        private void comboBoxVentanas_SelectedValueChanged(object sender, EventArgs e)
        {
            //this.stop();
            
            cantidadVentanas = Int16.Parse(comboBoxVentanas.SelectedItem + "");
            this.inicializarVentanas(cantidadVentanas);

            //login(cantidadVentanas);
        }

        private void refresh()
        {
            for (int i = 0; i < pantallas.Values.Count; i++)
            {
                pantallas[i].Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.stop();
            this.stopState();
        }

        private void stopState()
        {
            comboBoxCamaras.Enabled=true;
            comboBoxVentanas.Enabled=true;
            btnPlay.Enabled=true;
            btnStop.Enabled=false;
            btnTomarFoto.Enabled=false;
            btnGrabaciones.Enabled=false;
            btnOpenSound.Enabled=false;
            btnMic.Enabled=false;
            btnPTZUp.Enabled=false;
            btnPTZDown.Enabled=false;
            btnPTZRight.Enabled=false;
            btnPTZLeft.Enabled=false;
            btnPTZZoomIn.Enabled=false;
            btnPTZZoomOut.Enabled=false;
            groupBoxAjustes.Enabled = false;
        }

        private void playState()
        {
            comboBoxCamaras.Enabled = true;
            comboBoxVentanas.Enabled = false;
            btnPlay.Enabled = false;
            btnStop.Enabled = true;
            btnTomarFoto.Enabled = true;
            btnGrabaciones.Enabled = true;
            btnOpenSound.Enabled = true;
            btnMic.Enabled = false;
            btnPTZUp.Enabled = true;
            btnPTZDown.Enabled = true;
            btnPTZRight.Enabled = true;
            btnPTZLeft.Enabled = true;
            btnPTZZoomIn.Enabled = true;
            btnPTZZoomOut.Enabled = true;
            groupBoxAjustes.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cantidadVentanas = Int16.Parse(comboBoxVentanas.SelectedItem + "");
            //this.inicializarVentanas(cantidadVentanas);

            login(cantidadVentanas);
            this.playState();
        }

        private void seleccionCamara(PictureBox p) 
        {
            if (cantidadVentanas == 1)
            {
                return;
            }
            Panel panelSelector = null;
            if (cantidadVentanas == 4)
            {
                panelSelector = panelSelector4;
            } else if (cantidadVentanas == 9)
            {
                panelSelector = panelSelector9;

            } else if (cantidadVentanas == 16) 
            {
                panelSelector = panelSelector16;
            }

            for (int i = 0; i < pantallas.Count; i++)
            {
                PictureBox pActual = pantallas[i];
                if (pActual.Name == p.Name)
                {
                    panelSelector.Location = pActual.Location;
                    if (comboBoxCamaras.Items.Count > i) comboBoxCamaras.SelectedIndex = i;

                }
                else
                {
                    //pActual.Dock = DockStyle.None;

                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox4_1_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox4_2_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox4_3_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox4_4_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox9_1_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox9_2_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox9_3_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox9_4_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox9_5_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox9_6_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox9_7_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox9_8_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);
        }

        private void pictureBox9_9_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            seleccionCamara(p);

        }

        private void frm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            DHClient.DHCleanup();
        }

        int pPlayBackChannelID, playRecordFile;
        NET_RECORDFILE_INFO fileInfo;

        private void btnGrabaciones_Click(object sender, EventArgs e)
        {
            DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.Stop);
            frm_PlayBackByFileSet fpf = new frm_PlayBackByFileSet();
            fpf.gLoginID = pLoginID;
            fpf.ipDispositivo = this.ip;//pasa el ip actual
            
            //carga canales en combochanel de grabaciones
            fpf.cmbChannelSelect.Items.Clear();
            for (int i = 0; i < deviceInfo.byChanNum; i++)
            {
                fpf.cmbChannelSelect.Items.Add((i + 1).ToString());
            }
            //setea el combochanel de grabaciones en el canal actual
            fpf.cmbChannelSelect.SelectedIndex = comboBoxCamaras.SelectedIndex;
            //setea el textbox chanel en el canal actual
            fpf.channelActual = comboBoxCamaras.SelectedIndex.ToString();

            fpf.ShowDialog(this);
            /*
            if (fpf.blnOKEnter == true)
            {
                pPlayBackChannelID = int.Parse(fpf.txtChannelID.Text.ToString());
                fileInfo = fpf.gFileInfo;
                playRecordFile = DHClient.DHPlayBackByRecordFile(pLoginID, ref fileInfo, picRecord.Handle, null, IntPtr.Zero);
                if (playRecordFile == 0)
                {
                    MessageBox.Show("Ocurrió un error con al cargar la grabación", pMsgTitle);
                    btnCancelar_Click(null, null);
                }
                else
                {
                    btnPlay2.Text = "||";
                    groupBoxVentanas1.Visible = false;
                    groupBoxVentanas4.Visible = false;
                    groupBoxVentanas9.Visible = false;
                    groupBoxVentanas16.Visible = false;
                    groupBoxRecord.Visible = true;
                    gpbPlayBackControl.Enabled = true;
                    btnTomarFoto.Enabled = false;
                    groupBox5.Enabled = false;
                    groupBox4.Enabled = false;
                    btnStop.Enabled = false;
                    btnPlay.Enabled = false;
                }
            }
             * */
        }

       

       

        private void btnStop2_Click(object sender, EventArgs e)
        {
            if (DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.Stop) == false)//停止回放
            {
                MessageBox.Show("Ocurrió un error al detener la grabación", pMsgTitle);
            }
            btnCancelar_Click(null, null);
        }

       



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            picRecord.Refresh();
            picRecord.BackColor = SystemColors.Control;

            DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.Stop);

            groupBoxVentanas1.Visible = false;
            groupBoxVentanas4.Visible = false;
            groupBoxVentanas9.Visible = false;
            groupBoxVentanas16.Visible = false;
            groupBoxRecord.Visible = false;
            
            btnTomarFoto.Enabled = true;
            groupBox5.Enabled = true;
            groupBox4.Enabled = true;
            btnStop.Enabled = true;
            btnPlay.Enabled = false;

            cantidadVentanas = Int16.Parse(comboBoxVentanas.SelectedItem + "");
            this.inicializarVentanas(cantidadVentanas);
        }

        private void comboBoxVentanas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void hsbPlayBack_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult x = MessageBox.Show("¿Salir de la Aplicación?", " : Atención!", MessageBoxButtons.YesNo);
            if (x == DialogResult.Yes)
            {
                stop();
                Application.Exit();
            }
        }

        private void hsbBrightness_ValueChanged(object sender, EventArgs e)
        {
            setAjustesVideo();
        }
        
        private void hsbContrast_ValueChanged(object sender, EventArgs e)
        {
            setAjustesVideo();           
        }

        private void hsbSaturation_ValueChanged(object sender, EventArgs e)
        {
            setAjustesVideo();                    
        }

        private void hsbHue_ValueChanged(object sender, EventArgs e)
        {
            setAjustesVideo();
        }

        private void setAjustesVideo()
        {
            hsbBrightness.Value=64;
            hsbContrast.Value = 64;
            hsbSaturation.Value = 64;
            hsbHue.Value = 64;
           
        }

        private void getAjustesVideo()
        {
            bool blnReturn = DHPlay.DHPlayControl(PLAY_COMMAND.GetColor, 0, (uint)picPlayMain.Handle, ref pColor);
            if (blnReturn)
            {
                hsbBrightness.Value = pColor.pBrightness;
                hsbContrast.Value = pColor.pContrast;
                hsbHue.Value = pColor.pHue;
                hsbSaturation.Value = pColor.pSaturation;
            }
        }

        private void btnValPorDefecto_Click(object sender, EventArgs e)
        {
            pColor.pBrightness = 64;
            pColor.pContrast = 64;
            pColor.pSaturation = 64;
            pColor.pHue = 64;

            DHPlay.DHPlayControl(PLAY_COMMAND.SetColor, 0, 0, ref pColor);
        }

        private void btnOpenSound_Click(object sender, EventArgs e)
        {
            if (btnOpenSound.Tag.ToString() == "Off")
            {
                btnOpenSound.Image = global::bykomDahua.Properties.Resources.audio16x16;
                btnOpenSound.Tag = "On";                
                DHPlay.DHPlayControl(PLAY_COMMAND.PlaySound, 0);
            }
            else
            {
                btnOpenSound.Image = global::bykomDahua.Properties.Resources.audioOff16x16;
                btnOpenSound.Tag = "Off";
                DHPlay.DHPlayControl(PLAY_COMMAND.StopSound);
            }
           
        }

        private void btnPTZUp_Click(object sender, EventArgs e)
        {
            if (cantidadVentanas == 1 && pLoginID > 0)
            {
                if (!DHClient.DHPTZControl(pLoginID, this.channel, DHNetSDK.PTZ_CONTROL.PTZ_UP_CONTROL, 1, false))
                {
                    MessageBox.Show("La función no está habilitada para este dispositivo", "Error");
                }
            }
          
        }

        private void btnPTZDown_Click(object sender, EventArgs e)
        {
            if (cantidadVentanas == 1 && pLoginID > 0)
            {
                if (!DHClient.DHPTZControl(pLoginID, this.channel, DHNetSDK.PTZ_CONTROL.PTZ_DOWN_CONTROL, 1, false))
                {
                    MessageBox.Show("La función no está habilitada para este dispositivo", "Error");
                }
            }
        }

        private void btnPTZLeft_Click(object sender, EventArgs e)
        {
            if (cantidadVentanas == 1 && pLoginID > 0)
            {
                if (!DHClient.DHPTZControl(pLoginID, this.channel, DHNetSDK.PTZ_CONTROL.PTZ_LEFT_CONTROL, 1, false))
                {
                    MessageBox.Show("La función no está habilitada para este dispositivo", "Error");
                }
            }

        }

        private void btnPTZRight_Click(object sender, EventArgs e)
        {
            if (cantidadVentanas == 1 && pLoginID > 0)
            {
                if (!DHClient.DHPTZControl(pLoginID, this.channel, DHNetSDK.PTZ_CONTROL.PTZ_RIGHT_CONTROL, 1, false))
                {
                    MessageBox.Show("La función no está habilitada para este dispositivo", "Error");
                }
            }
        }

        private void btnPTZZoomIn_Click(object sender, EventArgs e)
        {
            if (cantidadVentanas == 1 && pLoginID > 0)
            {
                if (!DHClient.DHPTZControl(pLoginID, this.channel, DHNetSDK.PTZ_CONTROL.PTZ_ZOOM_ADD_CONTROL, 1, false))
                {
                    MessageBox.Show("La función no está habilitada para este dispositivo", "Error");
                }
            }
        }

        private void btnPTZZoomOut_Click(object sender, EventArgs e)
        {
            if (cantidadVentanas == 1 && pLoginID > 0)
            {
                
                if (!DHClient.DHPTZControl(pLoginID, this.channel, DHNetSDK.PTZ_CONTROL.PTZ_ZOOM_DEC_CONTROL, 1, false))
                {
                    MessageBox.Show("La función no está habilitada para este dispositivo", "Error");
                }
            }
        }
        
    }
}