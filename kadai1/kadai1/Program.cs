using System.ComponentModel.Design;

namespace kadai1
{

    public class InputHelper
    {
        public int GetOneNumber()
        {
            Console.WriteLine("半角数字を入力しましょう");
            string UserInput = Console.ReadLine();
            int UserNumber = 0;
            while (!int.TryParse(UserInput, out UserNumber)) 
            {
                Console.WriteLine("半角数字を入れましょう!");
                UserInput = Console.ReadLine();
            }
            return UserNumber;
            
        }

        public string GetMark()
        {
            var allowed = new List<string> { "+", "-", "*", "/" };
            Console.WriteLine("半角で演算子を入力してください");
            string MarkInput = Console.ReadLine();
            while (string.IsNullOrEmpty(MarkInput) || !allowed.Contains(MarkInput))
            {
                Console.WriteLine("演算子を半角で入力してください");
                MarkInput = Console.ReadLine();
            }
            return MarkInput;
        }
    }
    

         

    internal class CalculatorApp
    {
        static void Main(string[] args)
        {
            var input = new InputHelper();
            int firstNumber = input.GetOneNumber();
            int secondNumber = input.GetOneNumber();
            string mark = input.GetMark();

            int result = mark switch
            {
                "+" => firstNumber + secondNumber,
                "-" => firstNumber - secondNumber,
                "*" => firstNumber * secondNumber,
                "/" => firstNumber / secondNumber,
            };
            Console.WriteLine($"計算結果は {result} です");
        }
    }
}
