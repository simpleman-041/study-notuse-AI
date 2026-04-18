

using System.Security.Cryptography.X509Certificates;

namespace kadai3
{

    public class JankenMaker
    {
        public int win;
        public int draw;
        public int lose;
        public int repeatJanken = 3;

        public int GetUserHand()
        {
            string userHand = "";
            List<string> allowedHand = new List<string> { "グー", "チョキ", "パー" };

            while (!allowedHand.Contains(userHand))
            {
                Console.WriteLine("カタカナでグー、チョキ、パーのいずれかを入力してください！");
                userHand = Console.ReadLine();
            }
            if (userHand == "グー") return 0;
            if (userHand == "チョキ") return 1;
            return 2;
        }

        public int GetCpuHand()
        {
            Random random = new Random();
            int cpuHand = random.Next(0, 3);
            return cpuHand;
        }

        public void PrintResult()
        {
            Console.WriteLine("お疲れさまでした、、、！");
            Console.WriteLine($"結果はあなたが{win}勝、{draw}引き分け、{lose}敗という結果でした！");
        }

        public void PlayJanken()
        {
            for (int i = 0; i < repeatJanken; i++)
            {
                int user = GetUserHand();
                int cpu = GetCpuHand();
                Console.WriteLine("じゃんけんぽん！");

                int result = (user - cpu + 3) % 3;

                if (result == 0)
                {
                    Console.WriteLine("あいこです!");
                    draw += 1;
                }
                else if (result == 1)
                {
                    Console.WriteLine("私の勝ち!");
                    lose += 1;
                }
                else
                {
                    Console.WriteLine("あなたの勝ちです！");
                    win += 1;
                }
            }
        }
    }

        internal class Program
        {
            static void Main(string[] args)
            {
                var janken = new JankenMaker();
                janken.PlayJanken();
                janken.PrintResult();
            }
        }
    }
