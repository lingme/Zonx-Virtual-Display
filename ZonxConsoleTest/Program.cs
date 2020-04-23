using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using ZonxWinAPI;
using System.Linq;

namespace ZonxConsoleTest
{
    public enum PokerType
    {
        Hearts, //红桃
        Spade, //黑桃
        Grass, //草花
        Diamonds, //方块
    }

    public class Poker
    {
        public PokerType PokerType;
        public char PokerNumber;

        public Poker(PokerType pokerType, char pokerNumber)
        {
            PokerType = pokerType;
            PokerNumber = pokerNumber;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================Start=================\r\n");


            var pokerList = new List<Poker>();

            pokerList.Add(new Poker(PokerType.Hearts,'A'));
            pokerList.Add(new Poker(PokerType.Hearts,'Q'));
            pokerList.Add(new Poker(PokerType.Hearts,'4'));

            pokerList.Add(new Poker(PokerType.Spade, 'J'));
            pokerList.Add(new Poker(PokerType.Spade, '8'));
            pokerList.Add(new Poker(PokerType.Spade, '4'));
            pokerList.Add(new Poker(PokerType.Spade, '2'));
            pokerList.Add(new Poker(PokerType.Spade, '7'));
            pokerList.Add(new Poker(PokerType.Spade, '3'));

            pokerList.Add(new Poker(PokerType.Grass, 'K'));
            pokerList.Add(new Poker(PokerType.Grass, 'Q'));
            pokerList.Add(new Poker(PokerType.Grass, '5'));
            pokerList.Add(new Poker(PokerType.Grass, '4'));
            pokerList.Add(new Poker(PokerType.Grass, '6'));

            pokerList.Add(new Poker(PokerType.Diamonds, 'A'));
            pokerList.Add(new Poker(PokerType.Diamonds, '5'));

            var tempList = new List<Poker>();
            var removeList = new List<Poker>();
            if (pokerList.Count == 16)
            {
                foreach (var item in pokerList.GroupBy(x => x.PokerNumber).Where(x => x.Count() > 1))
                {
                    foreach (var poker in item)
                    {
                        tempList.Add(poker);
                    }
                }

                foreach (var item in pokerList.GroupBy(x => x.PokerNumber).Where(x => x.Count() <= 1))
                {
                    foreach (var poker in item)
                    {
                        removeList.Add(poker);
                    }
                }

                var charList = removeList.Select(x => x.PokerType).Distinct();
                tempList = tempList.Where(x => !charList.Contains(x.PokerType)).ToList();

                removeList.Clear();
                foreach (var item in tempList.GroupBy(x => x.PokerNumber).Where(x => x.Count() <= 1))
                {
                    foreach (var poker in item)
                    {
                        removeList.Add(poker);
                    }
                }

                var no2List = new List<Poker>();
                foreach (var item in removeList.GroupBy(x => x.PokerType).Where(x => x.Count() > 1))
                {
                    foreach (var poker in item)
                    {
                        no2List.Add(poker);
                    }
                }

                var type = no2List.Select(x => x.PokerType).Distinct();
                var result = removeList.Where(x => !type.Contains(x.PokerType));

                if(result.Count() == 1)
                {
                    Console.WriteLine($"{result.FirstOrDefault().PokerType} - {result.FirstOrDefault().PokerNumber}");
                }

            }
            Console.WriteLine("\r\n\r\nPress any key exit program");
            Console.ReadKey();
        }
    }


    //class Program
    //{
    //    [DllImport("ZonxDeviceManage.dll", CharSet = CharSet.Ansi)] 
    //    public static extern bool CreateDevice(string instanceId, string deviceDescription, ref int nDeviceStation);

    //    [DllImport("ZonxDeviceManage.dll", CharSet = CharSet.Ansi)]
    //    public static extern bool CloseDevice(int nDeviceStation);





    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("=================Test Install Inf=================\r\n");


    //        //var infPath = @"C:\ZonxVirtualDevice\X64\ZonxVirtualDevice.inf";


    //        //var result = InstallHinf.SetupCopyOEMInf(infPath, null, 0, 0, null, 0, 0, null);

    //        //Console.WriteLine(result ? "Successful" : "Error");

    //        //int fd = 0;
    //        //if (CreateDevice("ZonxVirtualDevice", "Zonx Virtual Device", ref fd))
    //        //{
    //        //    Console.WriteLine("Press any key close Device");
    //        //    Console.ReadKey();

    //        //    CloseDevice(fd);
    //        //}


    //        Console.WriteLine("Press any key exit program");
    //        Console.ReadKey();
    //    }
    //}
}
