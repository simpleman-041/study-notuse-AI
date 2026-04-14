namespace kadai4
{

    public class GuessNumberGame
    {
        public int _userNum;
        public int _cpuNum;
        public int _count = 0;
        public void GetCpuNumber()
        {
            var random = new Random();
            _cpuNum = random.Next(1, 1001);
        }

        public void GetUserNumber()
        {
            string userInput = "";
            bool isSuccess = true;
            do
            {
                Console.WriteLine("1~1000の間で予想した整数を入力してください！");
                userInput = Console.ReadLine();

                isSuccess = int.TryParse(userInput, out _userNum);

                if (!isSuccess)
                {
                    Console.WriteLine("正しい入力をお願いします！半角数字かつ、1~1000の間です。");
                }
                else if (_userNum < 1 || 1000 < _userNum)
                {
                    Console.WriteLine("1~1000の間で入力してください");
                }
            }
            while (!isSuccess || _userNum < 1 || 1000 < _userNum);
        }

        public void DisplayResult()
        {
            Console.WriteLine($"お疲れさまでした。正解は{_cpuNum}。正解までの試行回数は{_count}でした。");
        }

        public void GuessAnswer()
        {
                if (_cpuNum < _userNum)
                {
                    Console.WriteLine("ユーザーの数値がでかすぎます！");
                    _count++;
                }
                else if (_userNum < _cpuNum)
                {
                    Console.WriteLine("小さいよ～");
                    _count++;
                }
        }

        public void DoGame()
        {
            while (_cpuNum != _userNum)
            {
                GetUserNumber();
                GuessAnswer();
            }
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var guessGame = new GuessNumberGame();
            guessGame.GetCpuNumber();
            guessGame.DoGame();
            guessGame.DisplayResult();
        }
    }
}
