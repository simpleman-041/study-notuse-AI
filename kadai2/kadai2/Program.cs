namespace kadai2
{

    public class CheckNumber
    {
        public int number;
        
        public int GetNumber()
        {
            Console.WriteLine("半角数字を入力しましょう");
            string userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out number))
            {
                Console.WriteLine("半角数字を入力しましょう");
                userInput = Console.ReadLine();
            }
            return number;
        }

        public string CheckEvenOdd(int number)
        {
            if (number % 2 == 0)
            {
                return "偶数";
            }
            else
            {
                return "奇数";
            }
        }

        public string CheckSign(int number)
        {
            if (number > 0)
            {
                return "正";
            }
            else if (number < 0)
            {
                return "負";
            }
            else
            {
                return "0";
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var checkNumber = new CheckNumber();
            int number = checkNumber.GetNumber();
            string evenOdd = checkNumber.CheckEvenOdd(number);
            string sign = checkNumber.CheckSign(number);

            Console.WriteLine($"あなたが入力した数字は{number}。{evenOdd}で{sign}という結果でした。");
        }
    }
}
