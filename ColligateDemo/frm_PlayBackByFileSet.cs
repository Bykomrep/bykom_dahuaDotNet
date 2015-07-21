
/*
 * ************************************************************************
 *                            SDK
 *                      大华网络SDK(C#版)示例程序
 * 
 * (c) Copyright 2007, ZheJiang Dahua Technology Stock Co.Ltd.
 *                      All Rights Reserved
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
using DHPlaySDK;                   
using System.Runtime.InteropServices;

namespace DaHuaNetSDKSample
{
    public partial class frm_PlayBackByFileSet : Form
    {

        
        public int gLoginID;

       
        public NET_RECORDFILE_INFO gFileInfo;

      
        private NET_RECORDFILE_INFO[] nriFileInfo;

       
        private const int intFilesMaxCount = 50;

        
        public bool blnOKEnter = false;

        public string ipDispositivo;
        public string channelActual;
        public int nroframeActual;
        private int segundosTotales;
        private int positionValue = 0;

        private const string strMsgTitle = "Reproductor Dahua & Bykom";

        private int pPlayBackChannelID, playRecordFile;
        private NET_RECORDFILE_INFO fileInfo;


        public frm_PlayBackByFileSet()
        {
            InitializeComponent();

            dtpStart.Format = DateTimePickerFormat.Custom; dtpStart.CustomFormat = "dd/MM/yyyy";
            dtpEnd.Format = DateTimePickerFormat.Custom; dtpEnd.CustomFormat = "dd/MM/yyyy";
            dtpTimeStart.Format = DateTimePickerFormat.Custom; dtpTimeStart.CustomFormat = "HH:mm: ss";
            dtpTimeStart.ShowUpDown = true;
            dtpTimeEnd.Format = DateTimePickerFormat.Custom; dtpTimeEnd.CustomFormat = "HH:mm: ss";
            dtpTimeEnd.ShowUpDown = true;
        
        }

        private void frm_PlayBackByFileSet_Load(object sender, EventArgs e)
        {
            cargarToolTipes();
            cmbRecordFileTypeSelect.SelectedIndex = 0;//Tipo de archivo
            
            //controles originales
            blnOKEnter = false;
           // btnOK.Enabled = false;

           //dtpStart.Value = DateTime.Now.AddDays(-7);//Una semana antes de la fecha de inicio de la fecha actual
            
            //La fecha de inicio para el primer mes de la fecha actual
            //dtpStart.Value = DateTime.Now.AddMonths(-1);

            txtDevName.Text = ipDispositivo;
            txtChannelID.Text = channelActual;
            Utility.StringUtil.InitControlText(this);

            //Se agrega los handler
            lsvFiles.DoubleClick += new System.EventHandler(this.lsvFiles_DoubleClick);
        }

        private void cargarToolTipes()
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
        }

      
        private void cmbChannelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {                      
            txtChannelID.Text = Convert.ToString(cmbChannelSelect.SelectedIndex + 1);
        }

       
        private void btnQuery_Click(object sender, EventArgs e)
        {
           

            #region << 画面操作 >>

           
            int channelID = 0;
            RECORD_FILE_TYPE rfType = RECORD_FILE_TYPE.ALLRECORDFILE;

            if (txtChannelID.Text.Trim().Length == 0)
            {
                MessageBox.Show("Por favor ingrese el número de canal", strMsgTitle);
                return;
            }
            else
            {
                channelID = int.Parse(txtChannelID.Text);
            }
            if (txtDevName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Por favor ingrese el nombre del dispositivo", strMsgTitle);
                return;
            }
            if (cmbChannelSelect.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione el tipo de archivo", strMsgTitle);
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
            string timeFormating = "";
            timeFormating = dtpStart.Value.Day.ToString() +"/"+ dtpStart.Value.Month.ToString() +"/"+ dtpStart.Value.Year.ToString() +" "+ dtpTimeStart.Value.Hour.ToString() + ":" + dtpTimeStart.Value.Minute.ToString() + ":" + dtpTimeStart.Value.Second.ToString();
            dtpStart.Value = DateTime.Parse(timeFormating);
            timeFormating = dtpTimeEnd.Value.Day.ToString() + "/" + dtpTimeEnd.Value.Month.ToString() + "/" + dtpTimeEnd.Value.Year.ToString() + " " + dtpTimeEnd.Value.Hour.ToString() + ":" + dtpTimeEnd.Value.Minute.ToString() + ":" + dtpTimeEnd.Value.Second.ToString();
            dtpEnd.Value = DateTime.Parse(timeFormating);

            DateTime tmStart = dtpStart.Value;
            DateTime tmEnd = dtpEnd.Value;

            if (tmStart.Date > tmEnd.Date)
            {
                MessageBox.Show("La fecha desde no puede ser posterior a la fecha hasta", ": Atención !");
                return;
            }
            else
            {
                if (tmStart.Date == tmEnd.Date && dtpTimeStart.Value.TimeOfDay >= dtpTimeEnd.Value.TimeOfDay)
                {
                    MessageBox.Show("La hora hasta debe ser mayor que la hora desde cuando la busqueda es en el mísmo día", ": Atención !");
                    return;

                }
            }
 

            #endregion

           

            #region << 查询操作 >>


          

            nriFileInfo = new NET_RECORDFILE_INFO[intFilesMaxCount];
            string strTimeFormatStyle = "hh:MM:ss dd/mm/yyyy";
            int intFileCount = 0;
            bool blnQueryRecordFile = false;

            this.progressBar1.Visible = true;
            this.progressBar1.Value = 50;
            this.lblInfoSearch.Visible = true;
            this.Refresh();

            blnQueryRecordFile = DHClient.DHQueryRecordFile(gLoginID, channelID, rfType, tmStart, tmEnd, null, ref nriFileInfo, intFilesMaxCount * Marshal.SizeOf(typeof(NET_RECORDFILE_INFO)), out intFileCount, 5000, false);            
            if (blnQueryRecordFile == true)
            {
                
                lsvFiles.Items.Clear();

                if (intFileCount > 0)
                {                  
                    this.progressBar1.Maximum = intFileCount;
                    ListViewItem lvi;
                    for (int i = 0; i < intFileCount; i++)
                    {
                        lvi = new ListViewItem();
                        lvi.SubItems[0].Text = txtDevName.Text + " / " + nriFileInfo[i].ch.ToString();
                        lvi.SubItems.Add(nriFileInfo[i].starttime.ToString(strTimeFormatStyle));
                        lvi.SubItems.Add(nriFileInfo[i].endtime.ToString(strTimeFormatStyle));
                        lvi.SubItems.Add(nriFileInfo[i].size.ToString());
                        lsvFiles.Items.Add(lvi);
                        
                        this.progressBar1.Increment(1);
                        this.lblInfoSearch.Text = "" + i.ToString() + "/" + intFileCount.ToString();
                    }
                    this.progressBar1.Visible = false;
                    this.lblInfoSearch.Visible = false;
                    return;
                }
                else
                {
                    MessageBox.Show("No se encontraron registros", ": Atención!");
                }
                
            }
            else
            {
                MessageBox.Show("No se pudo acceder al disco del dispositivo","Error");
            }

            this.progressBar1.Visible = false;
            this.lblInfoSearch.Visible = false;
            return;
            #endregion
          
           
        }

       
        private void lsvFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.ItemIndex != -1)
            {
                //btnOK.Enabled = true;
                gFileInfo = nriFileInfo[e.ItemIndex];
            }
            else
            {
                //btnOK.Enabled = false;
            }
        }
        
     
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            blnOKEnter = true;
            gFileInfo = nriFileInfo[lsvFiles.SelectedItems[0].Index];
            this.Close();
        }
         

     
        private void btnCancel_Click(object sender, EventArgs e)
        {
            procesoStopPlayBack();
            blnOKEnter = false;
            this.Close();
        }

        private void procesoPlay2()
        {
            gFileInfo = nriFileInfo[lsvFiles.SelectedItems[0].Index];

            pPlayBackChannelID = int.Parse(txtChannelID.Text.ToString());
            fileInfo = gFileInfo;

            

            playRecordFile = DHClient.DHPlayBackByRecordFile(gLoginID, ref fileInfo, pictureBoxPlayback.Handle, null, IntPtr.Zero);
            if (playRecordFile == 0)
            {
               // MessageBox.Show("Ocurrió un error con al cargar la grabación", "Error");
                btnCancelar_Click(null, null);
                playRecordFile = DHClient.DHPlayBackByRecordFile(gLoginID, ref fileInfo, pictureBoxPlayback.Handle, null, IntPtr.Zero);

            }
            if (playRecordFile > 0)
            {


                uint sizeFilecharge = fileInfo.size / 100;

                uint currentFrames = DHPlay.DHPlayControl(PLAY_COMMAND.GetCurrentFrameNum, 0, true);

                lblCurrentFrame.Text = currentFrames.ToString();
                lblCurrentTime.Text = fileInfo.starttime.dwHour.ToString() + ":" + fileInfo.starttime.dwMinute.ToString() + ":" + fileInfo.starttime.dwSecond.ToString();
                lblInfoVideo.Text = fileInfo.starttime.dwHour.ToString() + ":" + fileInfo.starttime.dwMinute.ToString() + ":" + fileInfo.starttime.dwSecond.ToString() + " - " + fileInfo.endtime.dwHour.ToString() + ":" + fileInfo.endtime.dwMinute.ToString() + ":" + fileInfo.endtime.dwSecond.ToString();

                DateTime startTime = new DateTime(fileInfo.starttime.dwYear, fileInfo.starttime.dwMonth, fileInfo.starttime.dwDay, fileInfo.starttime.dwHour, fileInfo.starttime.dwMinute, fileInfo.starttime.dwSecond);
                DateTime endTime = new DateTime(fileInfo.endtime.dwYear, fileInfo.endtime.dwMonth, fileInfo.endtime.dwDay, fileInfo.endtime.dwHour, fileInfo.endtime.dwMinute, fileInfo.endtime.dwSecond);
                TimeSpan span = endTime.Subtract(startTime);

                segundosTotales = (int)span.TotalSeconds;
                lblTotalTime.Text = span.Hours.ToString() + ":" + span.Minutes.ToString() + ":" + span.Seconds.ToString();


                trbPlayFrames.Maximum = 100;//(int) sizeFilecharge;

                timerGetPlayInfo.Enabled = true;

                btnPlay2.Tag = "||";
                play2State();
                gpbPlayBackControl.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ocurrió un error con al cargar la grabación", "Error");
                btnCancelar_Click(null, null);
            }
        }


        private int trbValueToSeconds(int trbValue, int segundosTotales)
        {
            //calcula la cantidad de segundos que equivale el value del trbFrames
            int segundos = Convert.ToInt16((trbValue * segundosTotales) / 100);
            return segundos;           
        }


        private int secondsToTrbValue(int timePlayed, int segundosTotales)
        {
            if (segundosTotales > 0)
            {

                int porcentajeTime = Convert.ToInt16((timePlayed * 100) / segundosTotales);

                return porcentajeTime;
            }
            else
            {
                return 0;
            }
           
        }

        private void lsvFiles_DoubleClick(object sender, EventArgs e)
        {
            procesoStopPlayBack();
            procesoPlay2();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
                     
           
            DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.Stop);
            pictureBoxPlayback.Refresh();
            stop2State();
            timerGetPlayInfo.Enabled = false;
          
           
        }

        
        private void btnStepPlayE_Click(object sender, EventArgs e)
        {
            try
            {
                if (DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.StepStop) == false)
                {
                   // MessageBox.Show("Ocurrió un error al reproducir la grabación", "Error");
                }
            }
            catch
            {                
                MessageBox.Show("Error interno nro: " + DHClient.LastOperationInfo.errCode, "Error");
            }
            btnStepPlayE.Enabled = false;
        }

        private void btnPlay2_Click(object sender, EventArgs e)
        {
            
            if (btnStepPlayE.Enabled == true)
            {
                btnStepPlayE_Click(null, null);
            }
            switch (btnPlay2.Tag.ToString())
            {
                case ">":
                    if (DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.Play) == true)
                    {
                        btnPlay2.Tag = "||";                      
                        play2State();
                        timerGetPlayInfo.Enabled = true;
                    }
                    break;

                case "||":
                    if (DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.Pause) == true)
                    {
                        btnPlay2.Tag = ">";
                        btnPlay2.Image = global::bykomDahua.Properties.Resources.play16x16;
                        timerGetPlayInfo.Enabled = false;
                        pause2State();
                    }
                    break;

                case "x"://se presionó stop
                     procesoStopPlayBack();
                     procesoPlay2();
                    break;
            }
        }

        private void btnStop2_Click(object sender, EventArgs e)
        {
            procesoStopPlayBack();
            
            btnPlay2.Tag = "x";
            btnPlay2.Image = global::bykomDahua.Properties.Resources.play16x16;

            timerGetPlayInfo.Enabled = false;

            pictureBoxPlayback.Refresh();
            pictureBoxPlayback.BackColor = SystemColors.ActiveCaption;
            
            stop2State();

        }

        private void stop2State()
        {
            btnStop2.Enabled = false;
            btnSlow.Enabled = false;
            btnFast.Enabled = false;           
            btnSetpPlayS.Enabled = false;
            btnStepPlayE.Enabled = false;
            btnSoundPlayBack.Enabled = false;
            trackBarVolume.Enabled = false;
            resetInfoTimers();
            btnPlay2.Image = global::bykomDahua.Properties.Resources.play16x16;

        }

        private void resetInfoTimers()
        {
            lblTotalTime.Text = "00:00:00";
            lblCurrentTime.Text = "00:00:00";
            lblCurrentFrame.Text = "0000000000000";
            lblInfoVideo.Text = "hh:mm:ss - hh:mm:ss";
        }

        private void play2State()
        {
            btnStop2.Enabled = true;
            btnSlow.Enabled = true;
            btnFast.Enabled = true;

            btnSetpPlayS.Enabled = true;
            btnStepPlayE.Enabled = false;
            btnSoundPlayBack.Enabled = true;
            trackBarVolume.Enabled = true;

            btnPlay2.Image = global::bykomDahua.Properties.Resources.pause_circle16x16;

        }

        private void pause2State()
        {
            btnStop2.Enabled = true;
            btnSlow.Enabled = false;
            btnFast.Enabled = false;

            btnSetpPlayS.Enabled = false;
            btnStepPlayE.Enabled = false;
            btnSoundPlayBack.Enabled = false;
            trackBarVolume.Enabled = false;

            btnPlay2.Image = global::bykomDahua.Properties.Resources.play16x16;

        }
        
        private void fastSlowState()
        {
            btnStop2.Enabled = true;
            btnSlow.Enabled = false;
            btnFast.Enabled = false;

            btnSetpPlayS.Enabled = false;
            btnStepPlayE.Enabled = true;
            btnSoundPlayBack.Enabled = false;
            trackBarVolume.Enabled = false;           
        }

        

        private void procesoStopPlayBack()
        { 
                if (DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.Stop) == false)
                {
                    //MessageBox.Show("Ocurrió un error al detener la reproducción", "error");
                    
                }            
        }

        private void btnSetpPlayS_Click(object sender, EventArgs e)
        {
            try
            {
                if (DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.StepPlay) == true)
                {
                    btnStepPlayE.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al ejecutar operación", "Error");
                }
            }
            catch
            {
                MessageBox.Show(DHClient.LastOperationInfo.ToString(), "Error");
            }
        }

        private void btnStepPlayE_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.StepStop) == false)//单步播放停止
                {
                   // MessageBox.Show("Ocurrió un error al reproducir la grabación", "Error");
                }

            }
            catch
            {
                MessageBox.Show(DHClient.LastOperationInfo.ToString(), "Error");
            }
            btnStepPlayE.Enabled = false;
        }

       

        private void btnSoundPlayBack_Click(object sender, EventArgs e)
        {
            if (btnSoundPlayBack.Tag.ToString() == "Off")
            {
               
                btnSoundPlayBack.Tag = "On";
                DHPlay.DHPlayControl(PLAY_COMMAND.PlaySound, 0);

                int VolumenRep = (int)DHPlay.DHPlayControl(PLAY_COMMAND.GetVolume, 0, true);

                if (trackBarVolume.Minimum <= VolumenRep && VolumenRep <= trackBarVolume.Maximum)
                {
                    trackBarVolume.Enabled = true;
                    trackBarVolume.Value = VolumenRep;
                }
                else
                {
                    trackBarVolume.Enabled = false;
                    trackBarVolume.Value = VolumenRep;
                }
               
                btnSoundPlayBack.Image = global::bykomDahua.Properties.Resources.audio16x16;
            }
            else
            {               
                btnSoundPlayBack.Tag = "Off";
                DHPlay.DHPlayControl(PLAY_COMMAND.StopSound);
                trackBarVolume.Enabled = false;
                btnSoundPlayBack.Image = global::bykomDahua.Properties.Resources.audioOff16x16;
            }
        }

        private void trackBarVolume_ValueChanged(object sender, EventArgs e)
        {
            if (trackBarVolume.Minimum <= trackBarVolume.Value && trackBarVolume.Value <= trackBarVolume.Maximum)
            {
                DHPlay.DHPlayControl(PLAY_COMMAND.SetVolume, 0, (uint)trackBarVolume.Value);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            ProgressBar pbar = new ProgressBar();
        }

        private void btnSlow_Click(object sender, EventArgs e)
        {
            if (playRecordFile > 0)
            {
                if (DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.Slow) == false)
                {
                    MessageBox.Show("No se pudo aplicar la operación", "error");
                }
               
            }
        }

        private void btnFast_Click(object sender, EventArgs e)
        {
            if (playRecordFile > 0)
            {
                if (DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.Fast) == false)
                {
                    MessageBox.Show("No se pudo aplicar la operación", "error");
                }
              
            }
        }       

        private void trbPlayFrames_MouseUp(object sender, MouseEventArgs e)
        {  
            
            if (DHPlay.DHPlayControl(PLAY_COMMAND.ReSume, 0))
            {
                btnPlay2.Tag = "||";
                play2State();
               
            }
            else
            {
                MessageBox.Show("Problema al ejecutar operación", "Error");
            }
            
                    
            if (!DHClient.DHPlayBackControl(playRecordFile, PLAY_CONTROL.SeekByBit, (uint)(trbPlayFrames.Value * (fileInfo.size / 100))))
            {
                MessageBox.Show("Problema al ejecutar operación", "Error");
            }
            positionValue = trbPlayFrames.Value;

            timerGetPlayInfo.Start();
            timerGetPlayInfo.Enabled = true;
          
        }
        
        private void trbPlayFrames_MouseDown(object sender, MouseEventArgs e)
        {   
            if (DHPlay.DHPlayControl(PLAY_COMMAND.Pause, 0))
            {
                btnPlay2.Tag = ">";                
                timerGetPlayInfo.Stop();
                timerGetPlayInfo.Enabled = false;  
            }
            else
            {
                MessageBox.Show("Problema al ejecutar operación", "Error");
            } 
             
        }

        private void timerGetPlayInfo_Tick(object sender, EventArgs e)
        {
            
            uint playedFrameNum = DHPlay.DHPlayControl(PLAY_COMMAND.GetCurrentFrameNum, 0, true);
            uint playedTime = DHPlay.DHPlayControl(PLAY_COMMAND.GetPlayedTime, 1, true);
            
            //f/s
            lblCurrentFrame.Text = Convert.ToString(playedFrameNum);

           //valor de trackbar + valor en segundos de los segundos transcurridos
            trbPlayFrames.Value = positionValue + secondsToTrbValue((int)playedTime, segundosTotales);
                      
           // (*) El valor del trakbar se pasa a segundos segun la duracion del video
            int segundos = trbValueToSeconds(trbPlayFrames.Value, segundosTotales);           

            //tiempo transcurrido orig
            lblCurrentTime.Text = DHPlay.DHConvertToTime(playedTime, 1, "HH:MM:SS");

            //Se agrega el cálculo (*) al tiempo transcurrido
            DateTime  timeAux = Convert.ToDateTime(lblCurrentTime.Text);
            timeAux = timeAux.AddSeconds(segundos);
            lblfechaaux.Text = timeAux.ToString("HH:mm:ss");

            lblCurrentTime.Text = lblfechaaux.Text;
           
          


        }

        private void btnTobegin_Click(object sender, EventArgs e)
        {
            if (playRecordFile > 0)
            {
                uint ppio = DHPlay.DHPlayControl(PLAY_COMMAND.ToBegin, 0, true);
                
            }
        }

        private void lblInfoVideos_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DHPlay.DHPlayControl(PLAY_COMMAND.ReSume, 0))
            {
                btnPlay2.Tag = "||";
                play2State();

            }
            else
            {
                MessageBox.Show("Problema al ejecutar operación", "Error");
            }
        }


       

      

       

    }
}