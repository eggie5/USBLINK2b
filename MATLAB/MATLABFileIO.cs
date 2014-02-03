#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using com.AComm;

#endregion

namespace USBLiNK
{
    /// <summary>
    /// Contains MATLAB and File IO Opperations
    /// </summary>
    public class MATLABFileIO
    {
        
        private Form1 guiRef;

        internal byte[] USBPacketData;
        
        private byte[] usbAmpValues;
        internal bool[] USBprolog = new bool[16];

       
        private const int DEFAULT_READ_LEN = 512;
        private const int IMG_READ_LEN = 57120;
        private int current_read_len = DEFAULT_READ_LEN;// or 57120 Bytes
        private bool image_packet;

        public bool ImagePacket
        {
            get { return image_packet && this.current_read_len==IMG_READ_LEN; }
        }


        public MATLABFileIO(Form1 reference)
        {
            guiRef = reference;

        }

        /// <summary>
        /// Reads the amp settings from the USB port
        /// and saves it to the global readData
        /// </summary>
        /// <returns>
        /// false if there is a USB problem, 
        /// true otherwise
        /// </returns>
        internal bool GetAmpValuesFromUSB()
        {

            //max according to docs is:
            //16777216 instead of 512 (16MB)
            byte [] read_bytes = new byte[current_read_len];

            if (!guiRef.usb.Read(out read_bytes, read_bytes.Length))
            {
                guiRef.statusPanelInfo.Text = guiRef.usb.LastError;
                return false;
            }

            //strip out the frist 2 bytes and save remainder to usbAmpValues
            usbAmpValues = new byte[read_bytes.Length];
            int x = 0;
            for (int i = 2; i < read_bytes.Length; i++)
            {
                usbAmpValues[x] = read_bytes[i];
                x++;
            }


            //this seems really redundant and I should consider
            //removing it for preformance concerns
            //USBPacketData = new byte[read_bytes.Length];
            //usbAmpValues.CopyTo(USBPacketData, 0);
            USBPacketData = usbAmpValues;
           

            //get the USB stream as bits from bytes
            BitArray USBStreamAsBits = new BitArray(usbAmpValues);
            
            //Get the USB prolog from the first byte of the USB stream
            //the 48th bit is the 7th byte and 64 is the end of the 8th byte
            //this is just a hackish way to get the bits of the bits of the 
            //prologue
            int xx = 0;
            for(int i=48; i<64; i++)
            {
                //setting global var
                USBprolog[xx]=USBStreamAsBits[i];
                xx++;
            }


            //now check the prolog for the singal 
            //that the next sample will be an image transfer
            //and if so increase the read size
            is_image_packet(usbAmpValues[6]);

            return true;

        }

        private void is_image_packet(byte signal)
        {
            if (signal == 255)
            {
                //the next sample will be image data so increase the read length
                this.current_read_len = IMG_READ_LEN;
                this.image_packet = true;
            }
            else
            {
                this.current_read_len = DEFAULT_READ_LEN; //default
                this.image_packet = false;
            }

        }

    
     

        //THIS method reads the binary file "COEFFS" from readCoeffsPath 
        //
        internal bool ReadMATLABBinaryFile(string readCoeffsPath, out byte [] writeData)
        {
           
             writeData = new byte[64];

            try
            {
                FileStream fs = new FileStream(readCoeffsPath, FileMode.Open, System.IO.FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                writeData = br.ReadBytes(writeData.Length);
                return true;
            }
            catch (FileNotFoundException)
            {
                guiRef.statusPanelInfo.Text = "-ERR (ReadMATLABWriteUSB) - Cannot find coeffs file at " + readCoeffsPath;
                return false;
            }
            catch (DirectoryNotFoundException)
            {
                guiRef.statusPanelInfo.Text = "-ERR (ReadMATLABWriteUSB) - Cannot find coeffs file at " + readCoeffsPath;
                return false;
            }
        
        }

        
    }
}
