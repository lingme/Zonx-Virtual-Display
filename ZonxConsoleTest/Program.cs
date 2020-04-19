using System;
using System.Runtime.InteropServices;

namespace ZonxConsoleTest
{
    class Program
    {
        [DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection", CallingConvention = CallingConvention.StdCall)]
        public static extern void InstallHinfSection(
            [In] IntPtr hwnd,
            [In] IntPtr ModuleHandle,
            [In, MarshalAs(UnmanagedType.LPWStr)] string CmdLineBuffer,
            int nCmdShow);

        static void Main(string[] args)
        {
            Console.WriteLine("=================Test");
        }
    }
}
