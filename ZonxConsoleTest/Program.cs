using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using ZonxWinAPI;

namespace ZonxConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================Test Install Inf=================\r\n");


            var infPath = @"C:\ZonxVirtualDevice\X64\ZonxVirtualDevice.inf";

            //InstallHinf.InstallHinfSection(IntPtr.Zero, IntPtr.Zero, infPath, 0);


            //InstallHinf.SetupCopyOEMInf(infPath, null, 0, 0, null, 0, 0, null);

            InstallHinf.SetupUninstallOEMInfA(infPath, 0, IntPtr.Zero);

            Console.WriteLine("Press any key exit program");
            Console.ReadKey();
        }
    }
}
