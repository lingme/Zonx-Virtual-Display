using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using ZonxWinAPI;
using System.Linq;

namespace ZonxConsoleTest
{
    //扑克牌类型
    public enum PokerType
    {
        Hearts, //红桃
        Spade, //黑桃
        Grass, //草花
        Diamonds, //方块
    }

    //扑克牌 （结构对应 扑克牌类型对应 - 牌号）
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


            //构建16张牌
            var pokerList = new List<Poker>();

            //红桃
            pokerList.Add(new Poker(PokerType.Hearts,'A'));
            pokerList.Add(new Poker(PokerType.Hearts,'Q'));
            pokerList.Add(new Poker(PokerType.Hearts,'4'));

            //黑桃
            pokerList.Add(new Poker(PokerType.Spade, 'J'));
            pokerList.Add(new Poker(PokerType.Spade, '8'));
            pokerList.Add(new Poker(PokerType.Spade, '4'));
            pokerList.Add(new Poker(PokerType.Spade, '2'));
            pokerList.Add(new Poker(PokerType.Spade, '7'));
            pokerList.Add(new Poker(PokerType.Spade, '3'));

            //草花
            pokerList.Add(new Poker(PokerType.Grass, 'K'));
            pokerList.Add(new Poker(PokerType.Grass, 'Q'));
            pokerList.Add(new Poker(PokerType.Grass, '5'));
            pokerList.Add(new Poker(PokerType.Grass, '4'));
            pokerList.Add(new Poker(PokerType.Grass, '6'));

            //方块
            pokerList.Add(new Poker(PokerType.Diamonds, 'A'));
            pokerList.Add(new Poker(PokerType.Diamonds, '5'));

            var tempList = new List<Poker>();
            var removeList = new List<Poker>();
            
            //隐形条件，牌必须是16张，防止构建牌的时候写错了
            if (pokerList.Count == 16)
            {
                //第一句话，P先生知道的是点数,他说不知道这张牌,证明这个点数不是唯一的
                //（举例子，如果是2，只有黑桃里有2,P先生就会知道是黑桃2）即排除掉唯一的点数
                foreach (var item in pokerList.GroupBy(x => x.PokerNumber).Where(x => x.Count() > 1))
                {
                    foreach (var poker in item)
                    {
                        //这里整理出来所有点数不是唯一的牌
                        tempList.Add(poker);
                    }
                }


                foreach (var item in pokerList.GroupBy(x => x.PokerNumber).Where(x => x.Count() <= 1))
                {
                    foreach (var poker in item)
                    {
                        //这里整理出来所有点数是唯一的牌，为了第二句话用到。
                        removeList.Add(poker);
                    }
                }

                //Q先生知道的是花色,他说他知道P先生不会知道是什么牌,那么也就是说Q先生知道的花色里面,肯定没有:2,7,3,6,K,8,J.（如果有,那么P先生早就知道是什么牌了）
                //而包含这些数字的有黑桃,草花.所以Q先生知道的不是方块就是红桃.
                var charList = removeList.Select(x => x.PokerType).Distinct();
                tempList = tempList.Where(x => !charList.Contains(x.PokerType)).ToList();

                removeList.Clear();
                foreach (var item in tempList.GroupBy(x => x.PokerNumber).Where(x => x.Count() <= 1))
                {
                    foreach (var poker in item)
                    {
                        //得到这个集合，这个集合是上面已经去掉了唯一牌点数，但是又不包含和唯一牌号相同点数据的集合
                        removeList.Add(poker);
                    }
                }

                //P先生说他知道了,那么他知道的肯定不是A,因为方块和红桃都有A,他只能是Q,4,5中的一个.
                var no2List = new List<Poker>();
                foreach (var item in removeList.GroupBy(x => x.PokerType).Where(x => x.Count() > 1))
                {
                    foreach (var poker in item)
                    {
                        //得到这个集合，这里是不包含相同牌类型的组合
                        no2List.Add(poker);
                    }
                }

                var type = no2List.Select(x => x.PokerType).Distinct();
                var result = removeList.Where(x => !type.Contains(x.PokerType)); // 最后把包含相同牌类型的牌去掉，就达成了最后一个条件

                //看看是不是只剩下最后一张牌
                if(result.Count() == 1)
                {
                    //s输出结果
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
