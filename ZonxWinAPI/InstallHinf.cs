using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ZonxWinAPI
{
    public enum OemSourceMediaType
    {
        SPOST_NONE = 0,
        SPOST_PATH = 1,
        SPOST_URL = 2,
        SPOST_MAX = 3
    }

    public enum OemCopyStyle
    {
        SP_COPY_NEWER = 0x0000004,
        SP_COPY_NEWER_ONLY = 0x0010000,
        SP_COPY_OEMINF_CATALOG_ONLY = 0x0040000,
    }

    public class InstallHinf
    {
        private const int SC_MANAGER_CREATE_SERVICE = 2;
        private const int SERVICE_START = 16;
        private const int SERVICE_KERNEL_DRIVER = 1;
        private const int SERVICE_DEMAND_START = 3;
        private const int SERVICE_ERROR_IGNORE = 0;
        private readonly static IntPtr NULL = IntPtr.Zero;

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr OpenSCManager(string machineName, string databaseName, uint dwAccess);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, int dwDesiredAccess, int dwServiceType, int dwStartType, int dwErrorControl, string lpBinaryPathName, int lpLoadOrderGroup, int lpdwTagId, int lpDependencies, int lpServiceStartName, int lpPassword);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseServiceHandle(IntPtr hSCManager);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool StartService(IntPtr hService, int dwNumServiceArgs, string[] lpServiceArgVectors);

        [DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void InstallHinfSection(
            [In] IntPtr hwnd,
            [In] IntPtr ModuleHandle,
            [In, MarshalAs(UnmanagedType.LPWStr)] string CmdLineBuffer,
            int nCmdShow);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupCopyOEMInf(
            string SourceInfFileName,
            string OEMSourceMediaLocation,
            OemSourceMediaType OEMSourceMediaType,
            OemCopyStyle CopyStyle,
            string DestinationInfFileName,
            int DestinationInfFileNameSize,
            ref int RequiredSize,
            string DestinationInfFileNameComponent
        );
    }
}
