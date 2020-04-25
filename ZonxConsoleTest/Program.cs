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
        public static extern bool CreateDevice(string instanceId, string deviceDescription, ref IntPtr handle);

        [DllImport("ZonxDeviceManage.dll", CharSet = CharSet.Ansi)]
        public static extern bool CloseDevice(IntPtr handle);

        static void Main(string[] args)
        {
            Console.WriteLine("=================Test Install Inf=================\r\n");
            var infpath = @"c:\zonxvirtualdevice\x64\zonxvirtualdevice.inf";
            var result = InstallHinf.SetupCopyOEMInf(infpath, null, 0, 0, null, 0, 0, null);
            Console.WriteLine(result ? "successful install inf" : "install inf error");


            Console.WriteLine("=================Test create device and close device=================\r\n");
            IntPtr fd = IntPtr.Zero;
            if (CreateDevice("ZonxVirtualDevice", "Zonx Virtual Device", ref fd))
            {
                Console.WriteLine($"[C#] Press any key close Device :{fd}");
                Console.ReadKey();

                if(fd != IntPtr.Zero)
                {
                    Console.WriteLine("[C#] Closing the device!");
                    if(CloseDevice(fd))
                    {
                        Console.WriteLine("[C#] Close device successful ");
                    }
                }
            }


            Console.WriteLine("Press any key exit program");
            Console.ReadKey();
        }
    }
}
