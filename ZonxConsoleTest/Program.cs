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
        public static extern bool CreateDevice(string instanceId, string deviceDescription, ref int nDeviceStation);

        [DllImport("ZonxDeviceManage.dll", CharSet = CharSet.Ansi)]
        public static extern bool CloseDevice(int nDeviceStation);

        static void Main(string[] args)
        {
            Console.WriteLine("=================Test Install Inf=================\r\n");


            //var infPath = @"C:\ZonxVirtualDevice\X64\ZonxVirtualDevice.inf";


            //var result = InstallHinf.SetupCopyOEMInf(infPath, null, 0, 0, null, 0, 0, null);

            //Console.WriteLine(result ? "Successful" : "Error");

            int fd = 0;
            if (CreateDevice("ZonxVirtualDevice", "Zonx Virtual Device", ref fd))
            {
                Console.WriteLine("Press any key close Device");
                Console.ReadKey();

                CloseDevice(fd);
            }



            Console.WriteLine("Press any key exit program");
            Console.ReadKey();
        }
    }
}
