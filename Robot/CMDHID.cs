using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Robot
{
    public class CMDHID
    {
        public CMDHID() { }

        // Commander_HID API
        [DllImport("CMDHidApi.dll")] public static extern int GetCommanderHIDnumber();
        [DllImport("CMDHidApi.dll")] public static extern int GetCommanderHIDName(int number, IntPtr name);
        [DllImport("CMDHidApi.dll")] public static extern int OpenCommanderHID(out IntPtr pHandle, byte[] name);
        [DllImport("CMDHidApi.dll")] public static extern int SendReceiveCommanderHID(IntPtr handle, IntPtr command, IntPtr reply);
        [DllImport("CMDHidApi.dll")] public static extern int CloseCommanderHID(IntPtr handle);
    }
}
