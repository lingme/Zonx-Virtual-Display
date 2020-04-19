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


            var infPath = @"C:\ZonxVirtualDevice\ZonxVirtualDevice.inf";

            int size = 0;
            bool success = InstallHinf.SetupCopyOEMInf(infPath, "", OemSourceMediaType.SPOST_NONE, OemCopyStyle.SP_COPY_NEWER, null, 0, ref size, null);

            if (!success)
            {
                var errorCode = Marshal.GetLastWin32Error();
                var errorString = new Win32Exception(errorCode).Message;
                Console.WriteLine(errorString);
                Console.ReadLine();
            }

            Console.WriteLine("Press any key exit program");
            Console.ReadKey();
        }
    }
}
