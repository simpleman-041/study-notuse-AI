using System.ComponentModel.Design;

namespace kadai7
{
    internal class BankAccount
    {
        public string Name { get; private set; }
        public int Balance { get; private set; }

        private List<string> _history;
        public IReadOnlyList<string> History => _history;
        
        public BankAccount(string name, int balance)
        {
            this.Name = name;
            this.Balance = balance;
            _history = new List<string>();
        }

        public void DepositAccount(int paidIn)
        {
            this.Balance = Balance + paidIn;
            string newHistory = $"入金：{paidIn}円";
            _history.Add(newHistory);
        }

        public bool WithdrawAccount(int paidOut)
        {
            if (this.Balance - paidOut < 0) 
            {
                return false;
            }
            this.Balance -= paidOut;
            string newHistory = $"出金：{paidOut}円";
            _history.Add(newHistory);
            return true;
        }
    }

    internal class ConsoleHandler
    {
        internal BankAccount InitAccountCommand()
        {
            string userName = "";
            do
            {
                Console.WriteLine("口座を開設します。あなたの名前を入力してください。");
                userName = Console.ReadLine();
            }while (string.IsNullOrWhiteSpace(userName));
            var userAccount = new BankAccount(userName,0);
            return userAccount;
        }

        internal void DepositCommand(BankAccount account)
        {
            string userInput = "";
            int paidIn = 0;
            do
            {
                Console.WriteLine("入金額を半角数字で入力しましょう");
                userInput = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(userInput) || !int.TryParse(userInput,  out paidIn) || paidIn < 1);
            account.DepositAccount(paidIn);
            Console.WriteLine($"入金に成功しました。現在の残高は{account.Balance}円です。");
        }

        internal void WithdrawCommand(BankAccount account)
        {
            string userInput = "";
            int paidOut = 0;
            do
            {
                Console.WriteLine("出金額を半角数字で入力しましょう");
                userInput = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(userInput) || !int.TryParse(userInput, out paidOut) || paidOut < 1);
            bool canWithdraw = account.WithdrawAccount(paidOut);
            if (!canWithdraw)
            {
                Console.WriteLine("残高が不足しています。");
                Console.WriteLine("お手数ですが最初からやり直してください");
            }
            else
            {
                Console.WriteLine($"出金に成功しました。現在の残高は{account.Balance}円です。");
            }

        }

        internal void ViewHistory(BankAccount acount)
        {
            IEnumerable <string> historyList = acount.History;
            foreach (string history in historyList)
            {
                Console.WriteLine(history);
            }
        }

        internal void ViewBalance(BankAccount account)
        {
            Console.WriteLine($"残金：{account.Balance}");
        }

        internal string MenuCommand()
        { 
            string userInput = "";
            List<string> command = new List<string>() { "RESET", "DEPOSIT", "WITHDRAW", "BALANCE", "HISTORY", "Q"};
            do
            {
                Console.WriteLine("操作したいコマンドを入力してください");
                Console.WriteLine("初期化:reset | 入金:deposit | 出金:withdraw | 残高確認:balance | 履歴確認:history | 終了:q");
                userInput = (Console.ReadLine() ?? "").ToUpper();
            } while (string.IsNullOrWhiteSpace(userInput) || !command.Contains(userInput));
            return userInput;
        }

    }

    internal class BankRunner
    {
        static void Main(string[] args)
        {
            var consoleHandler = new ConsoleHandler();
            var userAccount = consoleHandler.InitAccountCommand();
            string userInput = "";

            while(true)
            {
                userInput = consoleHandler.MenuCommand();
                if (userInput == "RESET")
                {
                    userAccount = consoleHandler.InitAccountCommand();
                }
                if (userInput == "DEPOSIT") consoleHandler.DepositCommand(userAccount);
                if (userInput == "WITHDRAW") consoleHandler.WithdrawCommand(userAccount);
                if (userInput == "BALANCE") consoleHandler.ViewBalance(userAccount);
                if (userInput == "HISTORY") consoleHandler.ViewHistory(userAccount);
                if (userInput == "Q") break;
            }
            Console.WriteLine("操作を終了します。お疲れさまでした");
            
        }
    }
}
