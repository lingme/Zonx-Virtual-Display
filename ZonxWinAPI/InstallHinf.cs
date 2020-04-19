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
        [DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection", CallingConvention = CallingConvention.StdCall)]
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
