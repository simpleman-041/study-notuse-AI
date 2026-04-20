namespace exercise8
{

    internal class Book 
    {
        public string Title { get; private set; }
        public bool CanRent { get; private set; }

        public Book(string title)
        {
            Title = title; 
            CanRent = true;
        }

        internal bool Rent()
        {
            if (CanRent)
            {
                this.CanRent = false;
                return true;
            }
            return false;
            
        }

        internal bool Return()
        {
            if (!CanRent)
            {
                this.CanRent = true;
                return true;
            }
            return false;
        }
    }

    internal class BookRepository
    {
        private List<Book> _books = new List<Book>();
        public IReadOnlyList<Book> Books => _books;

        internal void AddBook(string title)
        {
            var book = new Book(title);
            _books.Add(book);
        }

        internal bool RentalBook(string title)
        {
            var target = Books.FirstOrDefault(b => b.Title == title);

            if (target != null)
            {
                bool IsComplete = target.Rent(); 
                return IsComplete;
            }
            return false;
        }

        internal bool ReturnBook(string title)
        {
            var target = Books.FirstOrDefault(b => b.Title == title);

            if (target != null)
            {
                bool IsComplete = target.Return();
                return IsComplete;
            }
            return false;
        }

    }

    internal class ConsoleHandler
    {
        private BookRepository _bookRepo = new BookRepository();

        internal string TitleInput()
        {
            string userInput = "";
            do
            {
                Console.WriteLine("対象のタイトルを入力してください");
                userInput = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(userInput));

            return userInput;
        }

        internal void AddCommand()
        {
            string title = TitleInput();
            _bookRepo.AddBook(title);
            Console.WriteLine("追加処理に成功しました。");
        }

        internal void RentalCommand()
        {
            string title = TitleInput();
            if (_bookRepo.RentalBook(title))
            {
                Console.WriteLine("貸出に成功しました。");
            }
            else
            {
                Console.WriteLine("タイトルに誤りがあるか、既に貸し出されています");
            }
        }

        internal void ReturnCommand()
        {
            string title = TitleInput();
            if (_bookRepo.ReturnBook(title))
            {
                Console.WriteLine("返却に成功しました。");
            }
            else
            {
                Console.WriteLine("タイトルに誤りがあるか、既に返却されています");
            }
        }

        internal void ViewCommand()
        {
            foreach (Book book in _bookRepo.Books)
            {
                string status = book.CanRent ? "貸出可能" : "貸出中";
                Console.WriteLine($"{book.Title} | {status}");
            }
        }

        internal string MenuCommand()
        {
            string userInput = "";
            List<string> command = new List<string>() { "ADD", "RENTAL", "RETURN", "VIEW", "Q"};
            do
            {
                userInput = (Console.ReadLine() ?? "").ToUpper();
            } while (string.IsNullOrWhiteSpace(userInput) || !command.Contains(userInput));

            return userInput;
        }
    }
    internal class LibraryFlow
    {
        static void Main(string[] args)
        {
            var consoleHandler = new ConsoleHandler();
            
            while (true) 
            {
                string commandInput = consoleHandler.MenuCommand();
                if (commandInput == "ADD") consoleHandler.AddCommand();
                if (commandInput == "RENTAL") consoleHandler.RentalCommand();
                if (commandInput == "RETURN") consoleHandler.ReturnCommand();
                if (commandInput == "VIEW") consoleHandler.ViewCommand();
                if (commandInput == "Q") break;
            }
            Console.WriteLine("処理を終了します。お疲れさまでした。");
        }
    }
}
