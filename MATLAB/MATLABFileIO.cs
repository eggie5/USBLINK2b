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
       
        internal byte[] GraphPoints;
        
        private byte[] usbAmpValues;
        internal bool[] USBprolog = new bool[8];


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

            usbAmpValues = new byte[512];

            if (!guiRef.usb.Read(out usbAmpValues, 512))
            {

                guiRef.statusPanelInfo.Text = guiRef.usb.LastError;

                return false;
            }

            
            #region sorting algorthim
            /*graphValues = new ArrayList();

            //Add 4 0's to arraylist
            newData = new ArrayList();
            for (int i = 0; i < 4; i++)
            {
                newData.Add((byte)0);
            }

            //cut off the last 4 elements
            for (int i = 0; i < usbAmpValues.Length-4; i++)
            {
                newData.Add(usbAmpValues[i]);
            }

            //Seperate into 16 length Arraylist and then arrange them
            ArrayList groupe = new ArrayList();

            int e = 0;
            int incrementer = 16;

            foreach (byte num in newData)
            {
                groupe.Add(num);
                e++;

                if (e == incrementer)
                {
                    ReArrange(groupe);
                    groupe.Clear();
                    incrementer += 16;
                }
            }
             



            //Check last groupe to see if it's 16 long
            //If false, add 0's 'till it is.

            if (groupe.Count != 16 && graphValues.Count!=512)
            {
                int ELEDIF = 16 - groupe.Count;

                //add 0's to end of groupe to fill 16 spaces

                for (int i = 0; i < ELEDIF; i++)
                {
                    groupe.Add((byte)0);
                }

                //Add last groupe to main arraylist
                ReArrange(groupe);
            }

            //do ben's last rearrange thing
            FinalizeData();*/
#endregion


            GraphPoints= new byte[512];
            //graphValues.CopyTo(GraphPoints);

            //this seems really redundant and I should consider
            //removing it for preformance concerns
            usbAmpValues.CopyTo(GraphPoints, 0);
           
            


            //get the USB stream as bits from bytes
            BitArray USBStreamAsBits = new BitArray(GraphPoints);
            
            //Get the USB prolog from the first byte of the USB stream
            for(int i=0; i<8; i++)
            {
                //setting global var
                USBprolog[i]=USBStreamAsBits[i];
            }

            return true;

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
