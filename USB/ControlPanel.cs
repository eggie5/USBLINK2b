using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using USBLiNK;
using com.BearBrand;

namespace com.AComm
{
    public partial class ControlPanel : Form
    {
        CSQuickUsb usb;
        public ControlPanel()
        {
            InitializeComponent();
            usb = new CSQuickUsb();
        }
        public ControlPanel(ref CSQuickUsb usbRef)
        {
            InitializeComponent();
            usb = usbRef;
        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {


        }

        private void buttonFindDevices_Click(object sender, EventArgs e)
        {
            string devices = usb.FindDevices();
            
            if (devices != "" && devices!=null)
            {
                listBox1.Items.Add(devices);
            }
            else 
            {
                listBox1.Items.Add("No devices found");
            }

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (!usb.Close())
            {
                MessageBox.Show(usb.LastError);
            }
            else if (!usb.IsOpen && usb.Handle==IntPtr.Zero)
            {
                listBox1.Items.Add("USB connection closed");
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (!usb.Open(textBoxModuleName.Text))
            {
                listBox1.Items.Add(usb.LastError);
            }
            else if (usb.IsOpen)
            {
                listBox1.Items.Add("Connected to device \"" + textBoxModuleName.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string des = usb.GetDeviceDescriptor(2);

            if (des == null)
                listBox1.Items.Add(usb.LastError);
            else
            listBox1.Items.Add(des);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i=listBox1.SelectedIndex;
            textBoxModuleName.Text = (string)listBox1.SelectedItem;
        }

        private void buttonReadSetting_Click(object sender, EventArgs e)
        {
            ushort settingVal=new ushort();
            try
            {
                if (usb.ReadSetting(Convert.ToUInt16(comboBoxSettings.Text), out settingVal))
                {

                    this.listBox1.Items.Add("Setting " + comboBoxSettings.Text + ": " + settingVal.ToString());
                }
                else
                    this.listBox1.Items.Add(usb.LastError);
            }
            catch (FormatException)
            {
                this.listBox1.Items.Add((usb.LastError="Invalid Input"));
            }
        }

        private void buttonSetSetting_Click(object sender, EventArgs e)
        {
            ushort settingVal = Convert.ToUInt16(this.textBoxSettingsSetValue.Text);

            if (usb.WriteSetting(Convert.ToUInt16(this.comboBoxSettingsSet.Text), settingVal))
            {
                this.listBox1.Items.Add("Set " + comboBoxSettingsSet.Text + ": " + settingVal.ToString());
            }
            else
                listBox1.Items.Add(usb.LastError);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

 
    }
}