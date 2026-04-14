namespace kadai6
{
    internal class StudentScore
    {
        private readonly string _name;
        private readonly int _score;

        public string Name => _name;
        public int Score => _score;

        public StudentScore(string name, int score)
        {
            _name = name;
            if (score < 0 || score > 100)
            {
                throw new ArgumentOutOfRangeException("点数は0から100の間でしか入力できません。");
            }
            _score = score;
        }
    }

    internal class GradeBook
    {
        private readonly List<StudentScore> _studentscore  = new List<StudentScore> { };
        public IReadOnlyList<StudentScore> StudentsScore => _studentscore;

        internal void AddStudentData(string name, int score)
        {
            var student = new StudentScore(name, score);
            _studentscore.Add(student);
        }

        private bool IsDataEmpty()
        {
            return StudentsScore.Count == 0;
        }

        internal float GetAverage()
        {
            if (IsDataEmpty()) return -1;
            float totalScore = StudentsScore.Sum(s => s.Score);
            int totalStudent = StudentsScore.Count;

            return totalScore / totalStudent;
        }

        internal int GetMaxScore()
        {
            if (IsDataEmpty()) return -1;
            int maxScore = StudentsScore.Max(s => s.Score);
            return maxScore;
        }

        internal int GetMinScore()
        {
            if (IsDataEmpty()) return -1;   
            int minScore = StudentsScore.Min(s => s.Score);
            return minScore;
        }

        internal List<StudentScore> GetPassStudents()
        {
            List<StudentScore>  passStudents = StudentsScore
                .Where(s => s.Score >= 60)
                .ToList();
            return passStudents;
        }
    }

    internal class StudentService
    {
        private readonly GradeBook _gradeBook = new GradeBook();
        internal void AddCommand()
        {
            string name = "";
            string userNumInput = "";
            int score = 0;
            do
            {
                Console.WriteLine("生徒名を入力しましょう");
                name = Console.ReadLine();
                Console.WriteLine("次に点数を半角数字で入力しましょう");
                userNumInput = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(name) || !int.TryParse(userNumInput, out score) || score < 0 || score > 100);

            _gradeBook.AddStudentData(name, score);
        }

        internal void ShowAverage()
        {
            Console.WriteLine($"平均点：{_gradeBook.GetAverage()}");
        }

        internal void ShowMaxScore()
        {
            Console.WriteLine($"最高点：{_gradeBook.GetMaxScore()}");
        }
        internal void ShowMinScore()
        {
            Console.WriteLine($"最低点：{_gradeBook.GetMinScore()}");
        }

        internal void ShowPass() 
        {
            var passStudents = _gradeBook.GetPassStudents();
            Console.WriteLine("合格者名 | 得点");
            foreach (StudentScore studentScore in passStudents)
            {
                Console.WriteLine($"{studentScore.Name} | {studentScore.Score}");
            }
        }

        internal string SelectMenu()
        {
            string userInput = "";
            List<string> menuSelect = new List<string>() {"ADD", "AVERAGE","MAX", "MIN", "PASS", "Q" };
            do
            {
                Console.WriteLine("行いたい処理を半角英語入力で指定しましょう。");
                Console.WriteLine("add：生徒登録 | average：平均点出力 | max：最高点 | min：最低点 | pass：合格者一覧 | q：終了");
                Console.WriteLine("生徒が誰も登録されていない場合、点数の出力において-1と表示されます。一覧には何も表示されません。");

                userInput = (Console.ReadLine() ?? "").ToUpper();
            }
            while (string.IsNullOrWhiteSpace(userInput) || !menuSelect.Contains(userInput));

            return userInput;
        }
    }

    internal class StudentScoreFlow
    {
        static void Main(string[] args)
        {
            var service = new StudentService();
            string userInput = "";
            while (userInput != "Q")
            {
                userInput = service.SelectMenu();

                if (userInput == "ADD") service.AddCommand();
                if (userInput == "AVERAGE") service.ShowAverage();
                if (userInput == "MAX") service.ShowMaxScore();
                if (userInput == "MIN") service.ShowMinScore();
                if (userInput == "PASS") service.ShowPass();
            }
            Console.WriteLine("プログラムを終了します。");
        }
    }


}
