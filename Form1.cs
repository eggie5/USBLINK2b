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

namespace com.AComm
{
    public partial class Form1 : Form
    {
        internal EngMATAccess ema;
        internal CSQuickUsb usb;
        MATLABFileIO fio;
        LineItem channel1_curve;
        LineItem myFFT;
        GraphPane channel1;
        GraphPane channel2;
        GraphPane channel3;
        GraphPane channel4;
        MasterPane myMaster;
        PictureBox[] leds;
        Bitmap on = Properties.Resources.onled;
        Bitmap off = Properties.Resources.off;
        LineItem[] curves = new LineItem[4];

        public Form1()
        {
            InitializeComponent();

            usb = new CSQuickUsb();
            usb.USBConnectionChanged += new CSQuickUsb.usbEventHandler(OnUSBConnectionChanged);
            usbStatusStrip.Text = "USB Status: Disconnected";
            
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ema = new EngMATAccess();

                //TEMP SETTINGS
                ema.Evaluate("fs=1400"); //FOR BEN'S FUNCTION
                ema.Evaluate("cd work"); //FOR MAT LAB 7.0.1
                ///
            }
            catch (DllNotFoundException)
            {

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

            channel1 = new GraphPane();
            channel1.XAxis.Max = 512;
            channel1.XAxis.IsShowGrid = true;
            channel1.XAxis.ScaleFontSpec.FontColor = Color.Black;
            channel1.XAxis.ScaleFontSpec.Size = 12;
            channel1.XAxis.Step = 50;
            channel1.YAxis.Max = 256;
            channel1.YAxis.IsShowGrid = true;
            channel1.YAxis.ScaleFontSpec.FontColor = Color.Black;
            channel1.YAxis.ScaleFontSpec.Size = 12;
            channel1.YAxis.Step = 50;

            ZedGraph.GraphItem i = new ZedGraph.TextItem("HELLO", 0, 0);
            myMaster.GraphItemList.Add(i);
            


            //add a place holder line
            channel1.AddCurve("Channel 1", new double[1], new double[1], Color.Red, SymbolType.None);





            channel2 = new GraphPane();
            channel2.XAxis.Max = 512;
            channel2.XAxis.IsShowGrid = true;
            channel2.XAxis.ScaleFontSpec.FontColor = Color.Black;
            channel2.XAxis.ScaleFontSpec.Size = 12;
            channel2.XAxis.Step = 50;

            channel2.YAxis.Max = 256;
            channel2.YAxis.IsShowGrid = true;
            channel2.YAxis.ScaleFontSpec.FontColor = Color.Black;
            channel2.YAxis.ScaleFontSpec.Size = 12;
            channel2.YAxis.Step = 50;

            channel2.AddCurve("Channel 2", new double[1], new double[1], Color.Blue, SymbolType.None);


            channel3 = new GraphPane();
            channel3.XAxis.Max = 512;
            channel3.XAxis.IsShowGrid = true;
            channel3.XAxis.ScaleFontSpec.FontColor = Color.Black;
            channel3.XAxis.ScaleFontSpec.Size = 12;
            channel3.XAxis.Step = 50;

            channel3.YAxis.Max = 256;
            channel3.YAxis.IsShowGrid = true;
            channel3.YAxis.ScaleFontSpec.FontColor = Color.Black;
            channel3.YAxis.ScaleFontSpec.Size = 12;
            channel3.YAxis.Step = 50;

            channel3.AddCurve("Channel 3", new double[1], new double[1], Color.Blue, SymbolType.None);

            channel4 = new GraphPane();
            channel4.XAxis.Max = 512;
            channel4.XAxis.IsShowGrid = true;
            channel4.XAxis.ScaleFontSpec.FontColor = Color.Black;
            channel4.XAxis.ScaleFontSpec.Size = 12;
            channel4.XAxis.Step = 50;
            channel4.YAxis.Max = 256;
            channel4.YAxis.IsShowGrid = true;
            channel4.YAxis.ScaleFontSpec.FontColor = Color.Black;
            channel4.YAxis.ScaleFontSpec.Size = 12;
            channel4.YAxis.Step = 50;
            channel4.AddCurve("Channel 4", new double[1], new double[1], Color.Blue, SymbolType.None);



            myMaster.Add(channel1);
            myMaster.Add(channel2);
            myMaster.Add(channel3);
            myMaster.Add(channel4);



            // Tell ZedGraph to auto layout all the panes
            Graphics g = CreateGraphics();
            myMaster.AutoPaneLayout(g, PaneLayout.ForceSquare);
            myMaster.AxisChange(g);
            g.Dispose();


            #endregion


            leds = new PictureBox[16];
            leds[0] = led1;
            leds[1] = led2;
            leds[2] = led3;
            leds[3] = led4;
            leds[4] = led5;
            leds[5] = led6;
            leds[6] = led7;
            leds[7] = led8;

            //second set
            leds[8] = led9;
            leds[9] = led10;
            leds[10] = led11;
            leds[11] = led12;
            leds[12] = led13;
            leds[13] = led14;
            leds[14] = led15;
            leds[15] = led16;




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
                    checkBoxContFeed.Checked = enabled;


                checkBoxContFeed.Enabled = enabled;
                groupBoxAmps.Enabled = enabled;
                groupBoxSysStatus.Enabled = enabled;
                buttonWriteData.Enabled = enabled;
            });
           
        }


        private void Poll_ReadData()
        {

            if (checkBoxContFeed.Checked && !timerContFeed.Enabled)
            {
                timerContFeed.Start();
            }

            if (fio.GetAmpValuesFromUSB())
            {
                //upate those 3 bytes things    
                labelRegister1.Text = fio.GraphPoints[4].ToString(); //2
                labelRegister2.Text = fio.GraphPoints[5].ToString(); //3
                labelRegister4.Text = fio.GraphPoints[6].ToString(); //4

                //Update the Graph count
                statusPanelUSBStatus.Text = "  " + graphCounter.ToString() + " Samples";
                graphCounter++;

                //Clear The previous graphs
                myMaster.PaneList[0].CurveList.Clear();
                myMaster.PaneList[1].CurveList.Clear();

                //Add a new line to the Plot
                for (int i = 0; i < 4; i++)
                {
                    curves[i] = channel1.AddCurve("USB Input"+i.ToString(), null, Color.Red, SymbolType.None);
                }
             

                //Create binary file for DYDA then run DYDA and copy image to clipboard
                MakeBowStaff(@"bowstaff2.cdf");
                if (ema != null && checkBoxMatlabEnabled.Checked)
                {
                    ema.Evaluate("DYDA");
                   
                }

                PlotRegular();
                
                //PlotFFT();

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

        private void PlotRegular()
        {
            int x = 0;
            // first 8 are control data
            //round robbin to each plot
            LineItem curve = curves[plot_counter % 4];
            for (int i = 8; i < fio.GraphPoints.Length; i++)
            {
                curve.AddPoint(x, fio.GraphPoints[i]);
                x++;
            }

            plot_counter++;
        }

        

       
        
       
        private void DataBindLEDs(bool [] bitArray)
        {
            for (int i = 0; i < 8; i++)
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
            bool [] led_bin = new bool[8];
            BitArray bits = new BitArray(fio.GraphPoints);
            for (int j = 0; j < 8; j++)
            {
                led_bin[j] = bits[j+8]; //+8 to get the second byte of bits
            }

            //same as first loop to databind leds icons
            for (int i = 8; i < leds.Length; i++)
            {
                if (bits[i])
                {
                    leds[i].Image = on;
                }
                else
                {
                    leds[i].Image = off;
                }
            }

        }

        private void MakeBowStaff(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(fio.GraphPoints);

            fs.Close();
            bw.Close();
        }


        private void PlotFFT()
        {
            myFFT = channel2.AddCurve("FFT Output", null, Color.FromKnownColor(KnownColor.MenuHighlight), SymbolType.None);
            float[] fft = new float[fio.GraphPoints.Length];
            fio.GraphPoints.CopyTo(fft, 0);
            
            Fourier.FFT(fft, 256, FourierDirection.Forward);

            
            for (int i = 0; i < fft.Length; i++)
            {
                fft[i] = 20*((float)Math.Log10((Math.Abs(fft[i]))));
            }

            int j = 0;
            foreach(float point in fft)
            {
                myFFT.AddPoint(j, point);
                j++;

            }

            Graph.Invalidate();
           
        }

      

        private void ShowControlPanel(object sender, EventArgs e)
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
        private void trackBarFreq_Scroll(object sender, EventArgs e)
        {
            TrackBar trackbar = (TrackBar)sender;
            //double result = (trackbar.Value * .0557) * (1 + 6.079 * y);

            //Controls["labelAmpValue1"].Text = trackbar.Value.ToString();

            string num = trackbar.Name.Substring(trackbar.Name.Length - 1);

            groupBoxAmps.Controls["labelAmpValue"+num].Text = trackbar.Value.ToString();

            //SEND TO USB

            if (!SendAmpSettings())
            {
                statusPanelInfo.Text = usb.LastError;
            }

        }

        private void send_to_usb(byte[] bytes)
        {
        }



  


  

        private byte amp4;
        private void checkBoxBit_CheckedChanged(object sender, EventArgs e)
        {
            //find out which checkbox this is
            CheckBox checkbox = (CheckBox)sender;
            string name = checkbox.Name;
            string prefix = name.Substring(0,name.Length - 1);
          

            StringBuilder sb = new StringBuilder(8);

            //loop throught all checkboxes to get values
            for (int i = 0; i < 8; i++)
            {
                sb.Append(Convert.ToInt32(((CheckBox)groupBoxAmps.Controls[prefix + i.ToString()]).Checked).ToString());
            }

            Label checkboxLabel = (Label)groupBoxAmps.Controls["label" + prefix];
            checkboxLabel.Text = sb.ToString();

            amp4 = Convert.ToByte(Conversion.BinToUInt(sb.ToString()));

            string prefix2 = "labelDec" + name;

           

            //labelAmpValue4DecimalVal.Text = amp4.ToString();
            Label decLabel = (Label)groupBoxAmps.Controls["labelDec" + prefix];
            decLabel.Text = amp4.ToString();

            //SEND TO USB

            if (!SendAmpSettings())
            {
                Log(usb.LastError);
            }
        }
        private bool SendAmpSettings()
        {
            //setup data structure to send to USB
            
            byte[] ampSettings = new byte[512];
            //pre-amble
            ampSettings[0] = 0xd3;
            ampSettings[1] = 0x1D;

            //body
            ampSettings[2] = (byte)Int32.Parse(textBoxAmp1.Text);
            ampSettings[3] = (byte)trackBarFreq1.Value;

            ampSettings[4] = (byte)Int32.Parse(textBoxAmp2.Text);
            ampSettings[5] = (byte)trackBarFreq2.Value;

            ampSettings[6] = (byte)Int32.Parse(textBoxAmp3.Text);
            ampSettings[7] = (byte)trackBarFreq3.Value;

            ampSettings[8] = (byte)Int32.Parse(textBoxAmp4.Text);
            ampSettings[9] = (byte)trackBarFreq4.Value;

            ampSettings[10] = (byte)Int32.Parse(textBoxAmp5.Text);
            ampSettings[11] = (byte)trackBarFreq5.Value;

            ampSettings[12] = (byte)Int32.Parse(textBoxAmp6.Text);
            ampSettings[13] = (byte)trackBarFreq6.Value;

            ampSettings[14] = (byte)Int32.Parse(labelDeccheckBoxCS0_.Text); //(byte)Int32.Parse(textBoxAmp7.Text);
            ampSettings[15] = (byte)Int32.Parse(labelDeccheckBoxCS1_.Text); // byte)trackBarFreq7.Value;
            ampSettings[16] = (byte)Int32.Parse(labelDeccheckBoxCS2_.Text); //byte)Int32.Parse(textBoxAmp8.Text);
            ampSettings[17] = (byte)Int32.Parse(labelDeccheckBoxCS3_.Text); //(byte)trackBarFreq8.Value;
        
            //checkboxes
            ampSettings[18] = (byte)Int32.Parse(labelDeccheckBoxBit.Text); //this val is already in decimal

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

        private void checkBoxContFeed_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxContFeed.Checked && !timerContFeed.Enabled)
            {
                timerContFeed.Start();

                statusPanelInfo.Image = Properties.Resources.control_play_blue;
                

            }
            else if (timerContFeed.Enabled)
            {

            
                timerContFeed.Stop();
                statusPanelUSBStatus.Image = null;
                graphCounter = 0;

                
            }

        }

        int graphCounter;

        private int plot_counter;
        private void timerTick(object sender, EventArgs e)
        {
            if (checkBoxContFeed.Checked)
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

        private void textBoxAmp_TextChanged(object sender, EventArgs e)
        {
            //SEND TO USB

            if (!SendAmpSettings())
            {
                statusPanelInfo.Text = usb.LastError;
            }
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

        

 

       

        




    }
}
