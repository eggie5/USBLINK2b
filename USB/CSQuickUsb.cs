#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Timers;


#endregion

namespace com.BearBrand
{
    public class CSQuickUsb : IDisposable
    {
        // Use interop to call the method necessary  
        // to clean up the unmanaged resource.
        /// <summary>
        /// Win API function for Dispose
        /// </summary>
        [DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        // Track whether Dispose has been called.
        private bool disposed = false;



        // Implement IDisposable
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    //put one of my pointers here

                    // System.Windows.Forms.MessageBox.Show("DISPOSING");
                }

                // Call the appropriate methods to clean up 
                // unmanaged resources here.
                // If disposing is false, 
                // only the following code is executed.
                CloseHandle(handle);
                handle = IntPtr.Zero;
                IsOpen = false;
            }
            disposed = true;
        }



        public delegate void usbEventHandler();
        public event usbEventHandler USBConnectionChanged;


        [DllImport("quickusb.dll", CharSet = CharSet.Ansi)]
        private static extern int QuickUsbFindModules(StringBuilder nameList, int bufferLength);

        [DllImport("quickusb.dll", CharSet = CharSet.Ansi)]
        private static extern int QuickUsbOpen(out IntPtr handle, string devName);

        [DllImport("quickusb.dll", CharSet = CharSet.Ansi)]
        private static extern int QuickUsbClose(IntPtr handle);

        [DllImport("quickusb.dll", CharSet = CharSet.Ansi)]
        private static extern int QuickUsbGetStringDescriptor(IntPtr Handle, int index, StringBuilder buffer, int len);

        [DllImport("quickusb.dll", CharSet = CharSet.Ansi)]
        private static extern int QuickUsbReadSetting(IntPtr Handle, ushort address, out ushort setting);

        [DllImport("quickusb.dll", CharSet = CharSet.Ansi)]
        private static extern int QuickUsbWriteSetting(IntPtr Handle, ushort address, ushort setting);

        [DllImport("quickusb.dll", CharSet = CharSet.Ansi)]
        private static extern int QuickUsbWriteData(IntPtr Handle, byte[] outData, int length);

        [DllImport("quickusb.dll", CharSet = CharSet.Ansi)]
        private static extern int QuickUsbReadData(IntPtr Handle, byte[] outData, out int length);

        [DllImport("quickusb.dll", CharSet = CharSet.Ansi)]
        private static extern int QuickUsbGetLastError(out int length);

        private IntPtr handle;

        System.Timers.Timer connectionChecker = new System.Timers.Timer(1000);

        public CSQuickUsb()
        {
            //set up the connection checker
            connectionChecker.Elapsed += new ElapsedEventHandler(OnTimedConnectionCheck);

            GC.KeepAlive(connectionChecker);
        }

        /// <summary>
        /// Gets the handle to the QuickUSB module
        /// </summary>
        public IntPtr Handle
        {
            get
            {
                return handle;
            }

            set
            {
                handle = value;
            }
        }

        private bool isOpen;

        public bool IsOpen
        {
            get
            {
                return isOpen;
            }
            set//this setter shouldn't be publicly available
            {
                isOpen = value;

                USBConnectionChanged();

                if (value)
                    connectionChecker.Start();

            }
        }

        //Checks every 1 second if device will return descriptor 
        //if not, device is assumed disconnects
        //this is sortof a hack, I need to impliment WM_DEVICECHANGE in future
        //http://code.google.com/p/csquickusb/issues/detail?id=1&can=2&q=
        private void OnTimedConnectionCheck(object source, ElapsedEventArgs e)
        {
            if (isOpen)
            {
                if (GetDeviceDescriptor(2) == null)
                {
                    connectionChecker.Stop();
                    //closing the USB connection, because
                    //it has been lost
                    Close();

                }
            }
        }

        private string lastError;

        /// <summary>
        /// Gets the last error that occured in the QuickUSB code
        /// Time of error included also
        /// </summary>
        /// <value></value>
        public string LastError
        {
            get
            {
                return lastError;
            }

            set
            {
                lastError = DateTime.Now.ToLongTimeString() + ": " + value;
            }
        }



        /// <summary>
        /// Finds all QuickUSB modules attached to computer
        /// </summary>
        /// <returns>String with device names or null for error</returns>
        public string FindDevices()
        {
            StringBuilder name = new StringBuilder();

            try
            {
                int result = QuickUsbFindModules(name, name.MaxCapacity);

                if (result != 0)
                {
                    return name.ToString();
                }

                if (result == 0 && name.ToString() == "")
                {
                    return "";
                }
            }
            catch (AccessViolationException ave)
            {
                LastError = ave.ToString();
                return null;
            }
            catch (DllNotFoundException dnfe)
            {
                LastError = dnfe.ToString();
                return null;
            }

            return null;
        }

        /// <summary>
        /// Open Connection to QuickUSB module
        /// </summary>
        public bool Open(string deviceName)
        {
            if (!IsOpen)
            {
                try
                {

                    int result = QuickUsbOpen(out handle, deviceName);

                    if (result == 0)
                    {
                        LastError = "Either the device is not attached/connected or it is not functioning.";
                        IsOpen = false;

                        return false;
                    }
                    else if (result != 0)
                    {
                        IsOpen = true;
                        disposed = false;

                        return true;
                    }
                    LastError = "Uknown open error";

                    return false;
                }
                catch (DllNotFoundException)
                {
                    LastError = "Cannot find the QuickUSB dll library. Please install QuickUsb Drivers.";
                    return false;
                }
            }


            return true;
        }

        //Closes the USB connection
        /// <summary>
        /// Close the connection to QuckUSB module
        /// </summary>
        public bool Close()
        {
            try
            {

                if (IsOpen)
                {
                    int result = QuickUsbClose(handle);

                    if (result != 0)
                    {
                        Dispose();
                        //IsOpen = false; dispose already calls this code

                        return true;
                    }

                    LastError = "Could not close QuickUsb handle";
                    return false;
                }

                //if it isn't open anyways, return true
                //dispose just in case
                Dispose();
                return true;
            }
            //If the QuickUSB drivers are not found
            catch (DllNotFoundException)
            {
                return true;
            }


        }

        /// <summary>
        /// Gets the USB device Descrpiter
        /// </summary>
        public string GetDeviceDescriptor(int option)
        {
            if (IsOpen)
            {
                int lenght = 512;

                StringBuilder text = new StringBuilder(lenght);

                int result = QuickUsbGetStringDescriptor(handle, option, text, lenght);

                if (result != 0)
                {
                    return text.ToString();
                }

                //IsOpen = false;
                return null;
            }

            LastError = "USB Connection not open";
            return null;

        }

        public bool ReadSetting(ushort address, out ushort setting)
        {

            if (IsOpen)
            {
                int result = QuickUsbReadSetting(handle, address, out setting);

                if (result != 0)
                    return true;
                else
                {

                    CheckConnection();
                }

                return false;
            }


            setting = 0;
            LastError = "Device is not open";
            return false;
        }

        private void CheckConnection()
        {
            //do something to see if the handle is valid
            //if not kill the connection, etc.
        }

        public bool WriteSetting(ushort address, ushort setting)
        {
            if (IsOpen)
            {
                int result = QuickUsbWriteSetting(handle, address, setting);

                if (result != 0)
                    return true;
                return false;
            }
            else
            {

                LastError = "Device is not open"; return false;
            }

        }

        /// <summary>
        /// Bulk write to QuckUSB module
        /// </summary>
        /// <param name="data">byte array with write data</param>
        public bool Write(byte[] data)
        {
            if (IsOpen)
            {
                int result = QuickUsbWriteData(handle, data, data.Length);

                if (result != 0)
                    return true;
                else if (result == 0)
                {
                    LastError = "QUSB returned 0";
                    return false;
                }
                else
                {
                    LastError = "Unknown error inside WriteData";
                    return false;
                }
            }
            else if (!IsOpen)
            {
                //IsOpen = false;
                Close();
                LastError = "USB connection not open, please open";
                return false;
            }
            else
            {
                //IsOpen = false;
                Close();
                LastError = "unknown write error";
                return false;
            }

        }

        /// <summary>
        /// Bulk Read from QuickUSB module
        /// </summary>
        /// <param name="data">predeclared byte array to fill with USB data</param>
        public bool Read(out byte[] data, int length)
        {
            try
            {
                if (IsOpen)
                {
                    byte[] readData = new byte[length];

                    int len = readData.Length;

                    int result = QuickUsbReadData(handle, readData, out len);
                    if (result == 0)
                    {
                        LastError = "USB Connection is not open";
                        //IsOpen = false;
                        Close();
                        data = readData;
                        return false;
                    }

                    data = readData;
                    return true;
                }
                else
                {
                    LastError = "Device not open";
                    data = new byte[length];
                    return false;
                }
            }
            catch (DllNotFoundException)
            {
                LastError = "Cannot find the QuickUSB dll library. Please install QuickUsb Drivers.";
                data = new byte[length];
                return false;
            }

        }
    }



}
