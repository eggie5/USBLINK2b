using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bin;

namespace com.AComm
{
    public partial class CPanel : Form
    {
        Form1 main_form_ref;
        public CPanel(Form1 main_ui_ref)
        {

            InitializeComponent();

            main_form_ref = main_ui_ref;
        }

        private void CPanel_Load(object sender, EventArgs e)
        {

        }

        private byte amp4;
        private void checkBoxBit_CheckedChanged(object sender, EventArgs e)
        {
            //find out which checkbox this is
            CheckBox checkbox = (CheckBox)sender;
            string name = checkbox.Name;
            string prefix = name.Substring(0, name.Length - 1);


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

            if (!this.main_form_ref.SendAmpSettings())
            {
                this.main_form_ref.Log(this.main_form_ref.usb.LastError);
            }
        }

        private void textBoxAmp_TextChanged(object sender, EventArgs e)
        {
            //SEND TO USB

            if (!this.main_form_ref.SendAmpSettings())
            {
                this.main_form_ref.statusPanelInfo.Text = this.main_form_ref.usb.LastError;
            }
        }

        private void trackBarFreq_Scroll(object sender, EventArgs e)
        {
            TrackBar trackbar = (TrackBar)sender;
            //double result = (trackbar.Value * .0557) * (1 + 6.079 * y);

            //Controls["labelAmpValue1"].Text = trackbar.Value.ToString();

            string num = trackbar.Name.Substring(trackbar.Name.Length - 1);

            groupBoxAmps.Controls["labelAmpValue" + num].Text = trackbar.Value.ToString();

            //SEND TO USB

            if (!this.main_form_ref.SendAmpSettings())
            {
                this.main_form_ref.statusPanelInfo.Text = this.main_form_ref.usb.LastError;
            }

        }

        private void ShowControlPanel(object sender, EventArgs e)
        {
            main_form_ref.ShowControlPanel(null, EventArgs.Empty);
        }

        private void checkBoxContFeed_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxContFeed.Checked && !main_form_ref.timerContFeed.Enabled)
            {
                main_form_ref.timerContFeed.Start();

                main_form_ref.statusPanelInfo.Image = Properties.Resources.control_play_blue;


            }
            else if (main_form_ref.timerContFeed.Enabled)
            {


                main_form_ref.timerContFeed.Stop();
                main_form_ref.statusPanelUSBStatus.Image = null;
                main_form_ref.graphCounter = 0;


            }

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


    }
}
