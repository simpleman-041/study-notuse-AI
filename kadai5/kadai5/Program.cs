using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace kadai5
{
    internal class TodoModel
    {
        public int Id { get; }
        public string Title { get; }
        public bool Done { get; private set; }

        public TodoModel(int id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;
        }

        public void ToggleDone()
        {
            if (this.Done == false)
            {
                this.Done = true;
            }
            else this.Done = false;

        }
    }

    internal class TodoData
    {
        private int nextId = 1;
        private readonly List<TodoModel> _todos;

        public IReadOnlyList<TodoModel> Todos => _todos; // 左辺の名前でアクセスされた時、右辺の内容を返す

        public TodoData()
        {
            _todos = new List<TodoModel> { };
        }

        public void AddTodo(string title)
        {
           
            var todo = new TodoModel(nextId, title, false);
            _todos.Add(todo);
            nextId++;
        }

        public bool RemoveTodo(int id)
        {
            var target = _todos.FirstOrDefault(item => item.Id == id);
            if (target != null)
            {
                _todos.Remove(target);
                return true;
            }
            return false;
        }

        public IReadOnlyList<TodoModel> ViewTodo()
        {
            return Todos;
        }

        public bool TodoToggleDone(int id)
        {
            var target = _todos.FirstOrDefault(item => item.Id == id);
            if (target != null)
            {
                target.ToggleDone();
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    internal class TodoManager
    {
        private TodoData _data = new TodoData(); //将来的に司令塔クラスでインスタンス生成し、それをコンストラクタでフィールドに入れ込む

        internal void AddCommand()
        {
            string title = "";
            do
            {
                Console.WriteLine("タイトルを入力しましょう");
                title = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(title));
            _data.AddTodo(title);
        }

        internal void RemoveCommand()
        {
            string userInput = "";
            int deleteId;
            do
            {
                Console.WriteLine($"削除したいTodoのIDを指定してね");
                userInput = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(userInput) || !int.TryParse(userInput, out deleteId));
            
            if (_data.RemoveTodo(deleteId))
            {
                Console.WriteLine("データは正常に削除されました。");
            }
            else
            {
                Console.WriteLine("対応するIDが見つかりませんでした。");
            }
        }

        internal void ViewCommand()
        {
            Console.WriteLine("以下に一覧表示を行います。");
            var todoList = _data.ViewTodo();
            foreach (var todo in todoList)
            {
                Console.WriteLine($"{todo.Id} | {todo.Title} | {todo.Done}");
            }
        }

        internal void DoneToggleCommand()
        {
            string userInput = "";
            int toggleId;
            Console.WriteLine("Done状態を変更したいIDを指定してください");
            do
            {
                Console.WriteLine("半角数字を入力しましょう");
                userInput = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(userInput) || !int.TryParse(userInput, out toggleId));

            if (_data.TodoToggleDone(toggleId))
            {
                Console.WriteLine("Doneを変更しました。");
            }
            else
            {
                Console.WriteLine("変更に失敗しました。");
            }
        }
    }

    internal class MenuCommand
    {
        private TodoManager _command = new TodoManager();
        private string _userInput;
        private string UserInput
        {
            get { return _userInput; }
            set { _userInput = value.ToUpperInvariant(); }
        }
        
        internal void Menu()
        {
            List<string> allowedCommand = new List<string>() {"ADD","REMOVE", "DONE", "VIEW","Q" };
            Console.WriteLine("Todo管理CLIツールへようこそ。以下のキーを入力して操作しましょう");
     
            while (UserInput != "Q")
            {
                do
                {
                    Console.WriteLine("追加：add | 削除：remove | 完了未完了の変更：done | 一覧表示：view | 終了：q");
                    UserInput = Console.ReadLine();
                }
                while (string.IsNullOrWhiteSpace(UserInput) || !allowedCommand.Contains(UserInput));

                if (UserInput == "ADD")
                {
                    _command.AddCommand();
                }
                else if (UserInput == "REMOVE")
                {
                    _command.RemoveCommand();
                }
                else if (UserInput == "DONE")
                {
                    _command.DoneToggleCommand();
                }
                else if (UserInput == "VIEW")
                {
                    _command.ViewCommand();
                }
            }
            Console.WriteLine("ツールを終了します。お疲れさまでした。");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var menu = new MenuCommand();
            menu.Menu();
        }
    }
}
