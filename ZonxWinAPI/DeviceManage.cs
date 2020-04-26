using System;
using System.Runtime.InteropServices;

namespace ZonxWinAPI
{
    public class DeviceManage
    {
        private const string ZonxDeviceManageDll = "ZonxDeviceManage.dll";

        [DllImport(ZonxDeviceManageDll, CharSet = CharSet.Ansi)]
        public static extern bool CreateDevice(string instanceId, string deviceDescription, out IntPtr handle);

        [DllImport(ZonxDeviceManageDll, CharSet = CharSet.Ansi)]
        public static extern bool CloseDevice(IntPtr handle);
    }
}
