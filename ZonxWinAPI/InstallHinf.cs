using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ZonxWinAPI
{
    public enum OEMSourceMediaType
    {
        SPOST_NONE = 0,
        SPOST_PATH = 1,
        SPOST_URL = 2,
        SPOST_MAX = 3
    }

    public enum SetupUOInfFlags : uint
    {
        NONE = 0x0000,
        SUOI_FORCEDELETE = 0x0001
    };

    public enum OEMCopyStyle
    {
        SP_COPY_DELETESOURCE = 0x0000001,
        SP_COPY_REPLACEONLY = 0x0000002,
        SP_COPY_NEWER = 0x0000004,
        SP_COPY_NEWER_OR_SAME = SP_COPY_NEWER,
        SP_COPY_NOOVERWRITE = 0x0000008,
        SP_COPY_NODECOMP = 0x0000010,
        SP_COPY_LANGUAGEAWARE = 0x0000020,
        SP_COPY_SOURCE_ABSOLUTE = 0x0000040,
        SP_COPY_SOURCEPATH_ABSOLUTE = 0x0000080,
        SP_COPY_IN_USE_NEEDS_REBOOT = 0x0000100,
        SP_COPY_FORCE_IN_USE = 0x0000200,
        SP_COPY_NOSKIP = 0x0000400,
        SP_FLAG_CABINETCONTINUATION = 0x0000800,
        SP_COPY_FORCE_NOOVERWRITE = 0x0001000,
        SP_COPY_FORCE_NEWER = 0x0002000,
        SP_COPY_WARNIFSKIP = 0x0004000,
        SP_COPY_NOBROWSE = 0x0008000,
        SP_COPY_NEWER_ONLY = 0x0010000,
        SP_COPY_SOURCE_SIS_MASTER = 0x0020000,
        SP_COPY_OEMINF_CATALOG_ONLY = 0x0040000,
        SP_COPY_REPLACE_BOOT_FILE = 0x0080000,
        SP_COPY_NOPRUNE = 0x0100000
    }

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
