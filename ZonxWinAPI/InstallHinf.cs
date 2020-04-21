using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ZonxWinAPI
{
    public class InstallHinf
    {
        [DllImport("setupapi.dll")]
        public static extern bool SetupCopyOEMInf(
            string SourceInfFileName,
            string OEMSourceMediaLocation,
            int OEMSourceMediaType,
            int CopyStyle,
            string DestinationInfFileName,
            int DestinationInfFileNameSize,
            int RequiredSize,
            string DestinationInfFileNameComponent);

        [DllImport("newdev.dll")]
        public static extern bool UpdateDriverForPlugAndPlayDevices(
            IntPtr hwndParent,
            string HardwareId,
            string FullInfPath,
            uint InstallFlags,
            bool bRebootRequired);

        [DllImport("setupapi.dll")]
        public static extern bool SetupUninstallOEMInfA(
            string InfFileName,
            int Flags,
            IntPtr Reserved);
    }
}
