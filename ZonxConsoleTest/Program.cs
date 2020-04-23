using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using ZonxWinAPI;
using System.Linq;

namespace ZonxConsoleTest
{
    class Program
    {
        [DllImport("ZonxDeviceManage.dll", CharSet = CharSet.Ansi)]
        public static extern bool CreateDevice(string instanceId, string deviceDescription, out IntPtr handle);

        [DllImport("ZonxDeviceManage.dll", CharSet = CharSet.Ansi)]
        public static extern bool CloseDevice(IntPtr handle);

        static void Main(string[] args)
        {
            Console.WriteLine("=================Test Install Inf=================\r\n");


            //var infPath = @"C:\ZonxVirtualDevice\X64\ZonxVirtualDevice.inf";


            //var result = InstallHinf.SetupCopyOEMInf(infPath, null, 0, 0, null, 0, 0, null);

            //Console.WriteLine(result ? "Successful" : "Error");

            IntPtr fd;
            if (CreateDevice("ZonxVirtualDevice", "Zonx Virtual Device", out fd))
            {
                Console.WriteLine($"Press any key close Device :{fd}");
                Console.ReadKey();

                if(fd != IntPtr.Zero)
                {
                    //CloseDevice(fd);

                }
            }



            Console.WriteLine("Press any key exit program");
            Console.ReadKey();
        }
    }
}
