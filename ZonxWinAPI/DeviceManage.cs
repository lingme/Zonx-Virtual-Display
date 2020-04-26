using System;
using System.Runtime.InteropServices;

namespace ZonxWinAPI
{
    public class DeviceManage
    {
        [DllImport("ZonxDeviceManage.dll", CharSet = CharSet.Ansi)]
        public static extern bool CreateDevice(string instanceId, string deviceDescription, out IntPtr handle);

        [DllImport("ZonxDeviceManage.dll", CharSet = CharSet.Ansi)]
        public static extern bool CloseDevice(IntPtr handle);
    }
}
