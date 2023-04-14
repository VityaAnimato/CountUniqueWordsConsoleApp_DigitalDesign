using System.Text.RegularExpressions;

namespace CountUniqueWordsConsoleApp_DigitalDesign
{

    internal class Program
    {               
        static void Main(string[] args)
        {         
            Console.WriteLine("Загрузите свой текстовый файл в папку MyDocuments и введите имя файла, например => VoinaIMir.txt");
            string nameInputFile = Console.ReadLine();
            string pathInput = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), nameInputFile);
            string pathOutput = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Result.txt");

            string contents;
            using (StreamReader reader = new StreamReader(pathInput))
            {
                contents = reader.ReadToEnd();
            }

            var uniqueWordsAndThisCount = GetUniqueWordsAndThisCount(contents);

            uniqueWordsAndThisCount.Remove("");

            using (StreamWriter writer = new StreamWriter(pathOutput))
            {
                foreach (var item in uniqueWordsAndThisCount)
                {
                    writer.WriteLine($"{item.Key} : {item.Value}");
                }
            }

            Console.WriteLine("Результат в файле Result.txt");
        }

        static Dictionary<string, int> GetUniqueWordsAndThisCount(string inputString)
        {
            Char[] separators = new Char[] { ' ', '\r', '\n', '\t', ',', '.', ';', '!', '?', '–' };

            List<string> allWords = new List<string>();
            Dictionary<string, int> uniqueWordsAndThisCount = new Dictionary<string, int>();

            allWords = inputString.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
            allWords.Sort();

            for (int i = 0; i < allWords.Count; i++)
            {
                allWords[i] = allWords[i].ToLower().Trim();
                if (!IsAplpha(allWords[i]))
                    continue;

                if (!uniqueWordsAndThisCount.ContainsKey(allWords[i]))
                    uniqueWordsAndThisCount[allWords[i]] = 1;
                else
                    uniqueWordsAndThisCount[allWords[i]]++;
            }

            var orderedUniqueWordsAndThisCount = uniqueWordsAndThisCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return orderedUniqueWordsAndThisCount;
        }

        static bool IsAplpha(string inputString)
        {
            for (int i = 0; i < inputString.Length; i++)
            {
                if (Regex.IsMatch(inputString[i].ToString(), @"[а-я]$"))
                    continue;
                if (Regex.IsMatch(inputString[i].ToString(), @"[a-z]$"))
                    continue;

                if (Regex.IsMatch(inputString[i].ToString(), "^\\d{1}$"))
                    return false;
                if (Regex.IsMatch(inputString[i].ToString(), @"[\+\-\/\@\#\%\^\*\(\)\;\:\'\<\>\]\[\»\«]$"))
                    return false;               
            }
            return true;
        }
    }
}