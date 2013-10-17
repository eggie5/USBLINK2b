using System;
using System.Collections.Generic;
using Bin;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using USBLiNK;
using ZedGraph;
using EngMATLib;
using Exocortex.DSP;
using System.IO;
using com.BearBrand;
using System.Collections;
using System.Threading;

namespace com.AComm
{
    public partial class Form1 : Form
    {
        //internal EngMATAccess ema;
        internal CSQuickUsb usb;
        MATLABFileIO fio;
   
        LineItem aux_line;
        GraphPane pane1;
        GraphPane pane2;
        GraphPane pane3;
        GraphPane pane4;
        GraphPane aux_pane;
        MasterPane myMaster;
        PictureBox[] leds;
        Bitmap on = Properties.Resources.onled;
        Bitmap off = Properties.Resources.off;
        LineItem[] curves = new LineItem[4];
        GraphPane[] panes = new GraphPane[4];
        Label[] data_labels = new Label[4];

        CPanel control_panel;


        MLApp.MLApp matlab;

        public Form1()
        {
            control_panel=new CPanel(this);

            InitializeComponent();

            usb = new CSQuickUsb();
            usb.USBConnectionChanged += new CSQuickUsb.usbEventHandler(OnUSBConnectionChanged);
            usbStatusStrip.Text = "USB Status: Disconnected";
            
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
             //init matlab
               matlab = new MLApp.MLApp();

               matlab.Execute("fs=1400");
               matlab.Execute("cd 'C:\\Program Files (x86)\\MATLAB\\R2009a\\work'");

            }
            catch (DllNotFoundException ee)
            {
                Console.WriteLine(ee.Message);
                Log("MATLAB not loaded");
            }

            fio = new MATLABFileIO(this);

            if (!usb.Open(usb.FindDevices()))
            {
                MessageBox.Show(usb.LastError, "USB Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //SetUSBControlsState(false);


            }

            if (usb.IsOpen)
            {
                //usbStatusStrip.Text = "USB Statue: Disconnect";
                WriteUSBSettings();
            }

            #region setup the graph

            myMaster = Graph.MasterPane;

            // Remove the default pane that comes with the ZedGraphControl.MasterPane
            myMaster.PaneList.Clear();
            myMaster.IsShowTitle = false;
            //Fill the pane background with a color gradient
            myMaster.PaneFill = new Fill(Color.FromArgb(((System.Byte)(159)), ((System.Byte)(191)), ((System.Byte)(245))), Color.FromArgb(((System.Byte)(196)), System.Drawing.Color.FromArgb(((System.Byte)(223)), ((System.Byte)(234)), ((System.Byte)(251)))));
            //Set the margins and the space between panes to 10 points
            myMaster.MarginAll = 10;
            myMaster.InnerPaneGap = 10;
            myMaster.Legend.IsVisible = false;

            pane1 = new GraphPane();
            pane1.XAxis.Max = 512;
            pane1.XAxis.IsShowGrid = true;
            pane1.XAxis.ScaleFontSpec.FontColor = Color.Black;
            pane1.XAxis.ScaleFontSpec.Size = 12;
            pane1.XAxis.Step = 50;
            pane1.YAxis.Max = 256;
            pane1.YAxis.IsShowGrid = true;
            pane1.YAxis.ScaleFontSpec.FontColor = Color.Black;
            pane1.YAxis.ScaleFontSpec.Size = 12;
            pane1.YAxis.Step = 50;

           
            


            //add a place holder line
            pane1.AddCurve("Channel 1", new double[1], new double[1], Color.Red, SymbolType.None);

            pane2 = new GraphPane();
            pane2.XAxis.Max = 512;
            pane2.XAxis.IsShowGrid = true;
            pane2.XAxis.ScaleFontSpec.FontColor = Color.Black;
            pane2.XAxis.ScaleFontSpec.Size = 12;
            pane2.XAxis.Step = 50;

            pane2.YAxis.Max = 256;
            pane2.YAxis.IsShowGrid = true;
            pane2.YAxis.ScaleFontSpec.FontColor = Color.Black;
            pane2.YAxis.ScaleFontSpec.Size = 12;
            pane2.YAxis.Step = 50;

            pane2.AddCurve("Channel 2", new double[1], new double[1], Color.Blue, SymbolType.None);


            pane3 = new GraphPane();
            pane3.XAxis.Max = 512;
            pane3.XAxis.IsShowGrid = true;
            pane3.XAxis.ScaleFontSpec.FontColor = Color.Black;
            pane3.XAxis.ScaleFontSpec.Size = 12;
            pane3.XAxis.Step = 50;

            pane3.YAxis.Max = 256;
            pane3.YAxis.IsShowGrid = true;
            pane3.YAxis.ScaleFontSpec.FontColor = Color.Black;
            pane3.YAxis.ScaleFontSpec.Size = 12;
            pane3.YAxis.Step = 50;

            pane3.AddCurve("Channel 3", new double[1], new double[1], Color.Blue, SymbolType.None);

            pane4 = new GraphPane();
            pane4.XAxis.Max = 512;
            pane4.XAxis.IsShowGrid = true;
            pane4.XAxis.ScaleFontSpec.FontColor = Color.Black;
            pane4.XAxis.ScaleFontSpec.Size = 12;
            pane4.XAxis.Step = 50;
            pane4.YAxis.Max = 256;
            pane4.YAxis.IsShowGrid = true;
            pane4.YAxis.ScaleFontSpec.FontColor = Color.Black;
            pane4.YAxis.ScaleFontSpec.Size = 12;
            pane4.YAxis.Step = 50;
            pane4.AddCurve("Channel 4", new double[1], new double[1], Color.Blue, SymbolType.None);



            myMaster.Add(pane1);
            myMaster.Add(pane2);
            myMaster.Add(pane3);
            myMaster.Add(pane4);

            //bundle channels in array
            
            panes[0]=pane1;
            panes[1] = pane2;
            panes[2] = pane3;
            panes[3] = pane4;

            //collect data labels
            data_labels[0] = channel1_info_label;
            data_labels[1] = channel2_info_label;
            data_labels[2] = channel3_info_label;
            data_labels[3] = channel4_info_label;



            // Tell ZedGraph to auto layout all the panes
            Graphics g = CreateGraphics();
            myMaster.AutoPaneLayout(g, PaneLayout.ForceSquare);
            myMaster.AxisChange(g);
            g.Dispose();


            #endregion


            leds = new PictureBox[16];
            leds[0] = this.control_panel.led1;
            leds[1] = this.control_panel.led2;
            leds[2] = this.control_panel.led3;
            leds[3] = this.control_panel.led4;
            leds[4] = this.control_panel.led5;
            leds[5] = this.control_panel.led6;
            leds[6] = this.control_panel.led7;
            leds[7] = this.control_panel.led8;

            //second set
            leds[8] = this.control_panel.led9;
            leds[9] = this.control_panel.led10;
            leds[10] = this.control_panel.led11;
            leds[11] = this.control_panel.led12;
            leds[12] = this.control_panel.led13;
            leds[13] = this.control_panel.led14;
            leds[14] = this.control_panel.led15;
            leds[15] = this.control_panel.led16;


            SetupFFTTab();


        }

        private void SetupFFTTab()
        {
            MasterPane myFFTMaster = zedGraphControlFFTs.MasterPane;
            myFFTMaster.PaneList.Clear();
            myFFTMaster.IsShowTitle = false;
            //Fill the pane background with a color gradient
            myFFTMaster.PaneFill = new Fill(Color.FromArgb(((System.Byte)(159)), ((System.Byte)(191)), ((System.Byte)(245))), Color.FromArgb(((System.Byte)(196)), System.Drawing.Color.FromArgb(((System.Byte)(223)), ((System.Byte)(234)), ((System.Byte)(251)))));
            //Set the margins and the space between panes to 10 points
            myFFTMaster.MarginAll = 10;
            myFFTMaster.InnerPaneGap = 10;
            myFFTMaster.Legend.IsVisible = false;
            aux_pane = new GraphPane();
            aux_pane.XAxis.Max = 512;
            aux_pane.XAxis.IsShowGrid = true;
            aux_pane.XAxis.ScaleFontSpec.FontColor = Color.Black;
            aux_pane.XAxis.ScaleFontSpec.Size = 12;
            aux_pane.XAxis.Step = 50;

            aux_pane.YAxis.Max = 600;
            aux_pane.YAxis.Min = 20;
            aux_pane.YAxis.IsShowGrid = true;
            aux_pane.YAxis.ScaleFontSpec.FontColor = Color.Black;
            aux_pane.YAxis.ScaleFontSpec.Size = 12;
            aux_pane.YAxis.Step = 50;

            aux_pane.AddCurve("AUX Plot", new double[1], new double[1], Color.Blue, SymbolType.None);


            zedGraphControlFFTs.MasterPane.Add(aux_pane);
            //myMaster.Add(FFTPane);
        }

        private void WriteUSBSettings()
        {
            //set settings 1 and 3


            if (usb.WriteSetting(1, 1))
            {
                //statusPanel1.Text = "setting 1 set to 16 bit mode";
            }
            else
                Log("error setting 1 to 16 bit mode");
            ///set 3


            if (usb.WriteSetting(3, 250))
            {
               // statusPanel1.Text = "setting 3 set to master mode";
            }
            else
               Log("error setting 3 set to master mode");

            
        }

        // Hook Events

        public void OnUSBConnectionChanged()
        {
            if (usb.IsOpen)
            {
                usbStatusStrip.Text = "USB COnnected";
                usbStatusStrip.Image = Properties.Resources.connect3;
                SetUSBControlsState(true);

                ///these are the quickusb settings for ben
                WriteUSBSettings();

            }
            else
            {
                usbStatusStrip.Text = "USB Disconnected";
                Log("The connection to the USB port was closed");
                usbStatusStrip.Image = Properties.Resources.try7;
                //SetUSBControlsState(false);
            }

        }

        private void SetUSBControlsState(bool enabled)
        {
           
         
            this.BeginInvoke((System.Threading.ThreadStart)delegate
            {
                if (!enabled)
                    this.control_panel.checkBoxContFeed.Checked = enabled;


                this.control_panel.checkBoxContFeed.Enabled = enabled;
                control_panel.groupBoxAmps.Enabled = enabled;
                this.control_panel.groupBoxSysStatus.Enabled = enabled;
                this.control_panel.buttonWriteData.Enabled = enabled;
            });
           
        }


        private void Poll_ReadData()
        {

            if (this.control_panel.checkBoxContFeed.Checked && !timerContFeed.Enabled)
            {
                timerContFeed.Start();
            }

            if (fio.GetAmpValuesFromUSB())
            {
               
                //find which plot we're working with
                signal_id = (int)fio.USBPacketData[0];

                for(int i=0; i<32; i++)
                    Console.Write(fio.USBPacketData[i].ToString() + " ");

                Console.WriteLine("");


                plot_position = signal_id;
                if (signal_id > 3)
                {
                
                    plot_position = 3; //default to plot 4
                }


                //Update the Graph count
                statusPanelUSBStatus.Text = "  " + graphCounter.ToString() + " Samples";
                graphCounter++;

                //Clear The previous graphs
                //myMaster.PaneList[0].CurveList.Clear();
                //myMaster.PaneList[1].CurveList.Clear();
                //myMaster.PaneList[2].CurveList.Clear();
                //myMaster.PaneList[3].CurveList.Clear();
                myMaster.PaneList[plot_position].CurveList.Clear();
                

                //Add a new line to the Plot
                curves[plot_position] = panes[plot_position].AddCurve("USB Input" + signal_id.ToString(), null, Color.Red, SymbolType.None);

                int bow_index = signal_id;
                if (bow_index <= 5)
                {
                    //Create binary file for DYDA then run DYDA and copy image to clipboard
                    //bens: @"C:\\Program Files (x86)\\MATLAB\\R2009a\\work\\bowstaff{0}.cdf"
                    string bow_path = String.Format(@"C:\\Program Files (x86)\\MATLAB\\R2009a\\work\\bowstaff{0}.cdf", bow_index);
                    MakeBowStaff(bow_path);
                    matlab.Execute(String.Format("fig_index={0}",bow_index));

                    if (matlab != null)
                    {
                        ThreadPool.QueueUserWorkItem(o => dyda()); //offload to worker thread so UI is not blocked

                    }
                }

                if (tabControl1.SelectedIndex == 0)
                {
                    PlotRegular();
                }

                //now create label text
                if (plot_position < 4)
                {
                    String lt = String.Format("Frequency Center: {0}Mhz\nFrequency Delta: {1} KHz\nSTDN Prob: {2}\nSGLS Prob: {3}\nSub Carrier det: {4}\nRanging Det: {5}",
                        fio.USBPacketData[2], fio.USBPacketData[3], fio.USBPacketData[4], fio.USBPacketData[5], fio.USBPacketData[6], 
                        fio.USBPacketData[7]);
                    
                    data_labels[plot_position].Text = lt;
                }

                //upate those 3 bytes things    
                if (signal_id == 4 || signal_id == 5)
                {
                    this.control_panel.labelRegister1.Text = fio.USBPacketData[4].ToString(); //2
                    this.control_panel.labelRegister2.Text = fio.USBPacketData[5].ToString(); //3
                    this.control_panel.labelRegister4.Text = fio.USBPacketData[6].ToString(); //4
                }


                
                //plot only when second tab is visible
                if (tabControl1.SelectedIndex == 2)
                {
                    PlotAux();
                    //zedGraphControlFFTs.Invalidate();
                }

                //Causes the graph to be redrawn
                Graph.Invalidate();

                DataBindLEDs(fio.USBprolog);

            }
            else
            {
                Log(usb.LastError);
                return;
            }
        }

        //this is run on worker thread
        public void dyda()
        {
            if (checkBoxMatlabEnabled.Checked)
                matlab.Execute("waveform0");
            if (checkBoxMatlabEnabled2.Checked)
                matlab.Execute("waveform1");
            if (checkBoxMatlabEnabled3.Checked)
                matlab.Execute("waveform2");
            if (checkBoxMatlabEnabled4.Checked)
                matlab.Execute("waveform3");
        }

        private void PlotRegular()
        {
            int x = 0;
            // first 8 are control data
            //round robbin to each plot
            //ben says first bit is plot position
           // int plot_position = plot_position;// (int)fio.USBPacketData[0];
            LineItem curve = curves[plot_position];
            for (int i = 9; i < fio.USBPacketData.Length; i++)
            {
                //prob start at 9th byte
                curve.AddPoint(x, fio.USBPacketData[i]);
                x++;
            }

            plot_counter++;
        }

        

       
        
       
        private void DataBindLEDs(bool [] bitArray)
        {
            for (int i = 0; i < 16; i++)
            {
                if (bitArray[i])
                {
                    leds[i].Image = on;
                }
                else
                {
                    leds[i].Image = off;
                }
            }

            //get the second byte of the resp in bits for second set of LEDS
            //bool [] led_bin = new bool[8];
            //BitArray bits = new BitArray(fio.USBPacketData);
            //for (int j = 0; j < 8; j++)
            //{
            //    led_bin[j] = bits[j+8]; //+8 to get the second byte of bits
            //}

            //same as first loop to databind leds icons
            //for (int i = 8; i < 16; i++)
            //{
            //    if (bitArray[i])
            //    {
            //        leds[i].Image = on;
            //    }
            //    else
            //    {
            //        leds[i].Image = off;
            //    }
            //}

        }

        private void MakeBowStaff(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(fio.USBPacketData);

            fs.Close();
            bw.Close();
        }


        float[] last_fft = new float[512];
       
        
        private void PlotAux()
        {

            aux_pane.CurveList.Clear();
            //check which to plot from combobox
            int index = comboBoxSignalSelect.SelectedIndex;

            if (index == signal_id) //then plot
            {
                aux_line = aux_pane.AddCurve(comboBoxSignalSelect.SelectedText, null, Color.FromKnownColor(KnownColor.MenuHighlight), SymbolType.None);
                int x = 0;
                for (int j = 9; j < fio.USBPacketData.Length; j++)
                {
                    aux_line.AddPoint(x, fio.USBPacketData[j]);
                    x++;
                }

                zedGraphControlFFTs.Invalidate();
            }

        }

      

        internal void ShowControlPanel(object sender, EventArgs e)
        {
            ControlPanel cp = new ControlPanel(ref usb);
            cp.Show();
        }

        private void buttonWriteData_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            /*
            byte[] writeData;
            if (fio.ReadMATLABBinaryFile("C:\\MATLAB701\\WORK\\COEFFS", out writeData))
            {
                //Send data to usb
                if (usb.Write(writeData))
                {
                    //all is good in the hood
                }
                else
                {
                    Log("-ERR USB send error");
                   
                }

            }
            else
            {
                Log( "ERROR FROM buttonWriteData_Click()");
            }*/
        }

        #region amps
 

        private void send_to_usb(byte[] bytes)
        {
        }



  


  

       
  

        internal bool SendAmpSettings()
        {
            //setup data structure to send to USB
            
            byte[] ampSettings = new byte[512];
            //pre-amble
            //TODO: ben says remove this
            ampSettings[0] = 0xd3;
            ampSettings[1] = 0x1D;

            //ampSettings[0] = 0x00;
            //ampSettings[1] = 0x00;

            //body
            ampSettings[2] = (byte)Int32.Parse(this.control_panel.textBoxAmp1.Text);
            ampSettings[3] = (byte)this.control_panel.trackBarFreq1.Value;

            ampSettings[4] = (byte)Int32.Parse(this.control_panel.textBoxAmp2.Text);
            ampSettings[5] = (byte)this.control_panel.trackBarFreq2.Value;

            ampSettings[6] = (byte)Int32.Parse(this.control_panel.textBoxAmp3.Text);
            ampSettings[7] = (byte)this.control_panel.trackBarFreq3.Value;

            ampSettings[8] = (byte)Int32.Parse(this.control_panel.textBoxAmp4.Text);
            ampSettings[9] = (byte)this.control_panel.trackBarFreq4.Value;

            //replace these
           // ampSettings[10] = (byte)Int32.Parse(this.control_panel.textBoxAmp5.Text);
            ampSettings[10] = (byte)Int32.Parse(this.control_panel.labelDeccheckBox_ctrl0_.Text);

            //ampSettings[11] = (byte)this.control_panel.trackBarFreq5.Value;
            ampSettings[11] = (byte)Int32.Parse(this.control_panel.textBoxRSetAge.Text);

           // ampSettings[12] = (byte)Int32.Parse(this.control_panel.textBoxAmp6.Text);
            ampSettings[12] = (byte)Int32.Parse(this.control_panel.textBoxFFTBinSet.Text);

            //ampSettings[13] = (byte)this.control_panel.trackBarFreq6.Value;
            ampSettings[13] = (byte)Int32.Parse(this.control_panel.labelDeccheckBox_ctrl3_.Text);
            //replace these

            ampSettings[14] = (byte)Int32.Parse(this.control_panel.labelDeccheckBoxCS0_.Text); //(byte)Int32.Parse(textBoxAmp7.Text);
            ampSettings[15] = (byte)Int32.Parse(this.control_panel.labelDeccheckBoxCS1_.Text); // byte)trackBarFreq7.Value;
            ampSettings[16] = (byte)Int32.Parse(this.control_panel.labelDeccheckBoxCS2_.Text); //byte)Int32.Parse(textBoxAmp8.Text);
            ampSettings[17] = (byte)Int32.Parse(this.control_panel.labelDeccheckBoxCS3_.Text); //(byte)trackBarFreq8.Value;
        
            //checkboxes
            ampSettings[18] = (byte)Int32.Parse(this.control_panel.labelDeccheckBoxBit.Text); //this val is already in decimal

            //post-amble
            ampSettings[19] = 0x5d;
            ampSettings[20] = 0x17;

            //pad rest with 0x00
            for (int i = 21; i < ampSettings.Length; i++)
            {
                ampSettings[i] = 0x00;
            }

            //Write the data
            if (!usb.Write(ampSettings))
            {
                return false;

            }
            else
            {
                Log("Amplifier Settings Updated");
                return true;
            }

        }
        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            usb.Close();
        }

        private void HookOfDeath(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.U)
            {
                //ControlPanel controlP = new ControlPanel(usb.Handle, ref usb);
                //controlP.Show();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                //SecretInputForm si = new SecretInputForm(this);
                //si.Show();

            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
              //  Options o = new Options();
               // o.Show();

            }
        }

        private void timerClearStatusBar_Tick(object sender, EventArgs e)
        {
            statusPanelInfo.Text = String.Empty;

        }


        internal int graphCounter;

        private int plot_counter;
        private int signal_id;
        private int plot_position;
        private void timerTick(object sender, EventArgs e)
        {
            if (this.control_panel.checkBoxContFeed.Checked)
            {
                Poll_ReadData();
            }

        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Ask Ben what to do with this");
            
        }

        private void toolStripMenuItemConnect_Click(object sender, EventArgs e)
        {
            if (!usb.Open(usb.FindDevices()))
            {
                Log("USB Connection could not be established: " + usb.LastError);
                SetUSBControlsState(false);
            }
        }

        private void toolStripMenuItemDisconnect_Click(object sender, EventArgs e)
        {
            usb.Close();
        }

        private void uSBControlPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowControlPanel(null, EventArgs.Empty);
        }

        private void timerStatusMessage_Tick(object sender, EventArgs e)
        {
            statusPanelInfo.Text = String.Empty;
           
        }

        internal void Log(string message)
        {
            timerStatusMessage.Stop();
            statusPanelInfo.Text = message;
            timerStatusMessage.Start();

        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxMatlabEnabled_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Graph_Load(object sender, EventArgs e)
        {

        }

        private void textBoxAmp2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAmp6_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxMATLABPlot_Click(object sender, EventArgs e)
        {

        }

        private void buttonWriteData_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonOpenCPanel_Click(object sender, EventArgs e)
        {
            control_panel.Show();
        }

        

 

       

        




    }
}
