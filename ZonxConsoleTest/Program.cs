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

            var result = InstallHinf.SetupCopyOEMInf(infPath, null, 0, 0, null, 0, 0, null);

            Console.WriteLine(result ? "Successful" : "Error");

            Console.WriteLine("Press any key exit program");
            Console.ReadKey();
        }
    }
}
