using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TypingPractice
{
    public enum TextCategory { English, Hebrew, Python, CSharp, Cpp, Java, JavaScript }
    public enum TextMode    { Sentences, Passage, Gibberish }

    public class TextLibrary
    {
        private static readonly Random rnd = new Random();

        public static readonly TextInfo[] AvailablePassages = new TextInfo[]
        {
            new TextInfo("שלמה המלך והדבורה",         "he_solomon.txt",      TextCategory.Hebrew),
            new TextInfo("עכבר הכרך ועכבר השדה",      "he_mouse.txt",        TextCategory.Hebrew),
            new TextInfo("הצפרדע שרצתה להיות שור",    "he_frog.txt",         TextCategory.Hebrew),
            new TextInfo("השועל והענבים",              "he_fox_grapes.txt",   TextCategory.Hebrew),
            new TextInfo("הנמלה והצרצר",               "he_ant.txt",          TextCategory.Hebrew),
            new TextInfo("הארנב והצב",                 "he_hare.txt",         TextCategory.Hebrew),
            new TextInfo("The Fox and the Lion",        "en_fox_lion.txt",     TextCategory.English),
            new TextInfo("The Tortoise and the Hare",   "en_hare.txt",         TextCategory.English),
            new TextInfo("The Ant and the Grasshopper", "en_ant.txt",          TextCategory.English),
            new TextInfo("The Boy Who Cried Wolf",      "en_wolf.txt",         TextCategory.English),
            new TextInfo("The Fox and the Grapes",      "en_fox_grapes.txt",   TextCategory.English),
            new TextInfo("The Lion and the Mouse",      "en_lion_mouse.txt",   TextCategory.English),
        };

        private static readonly Dictionary<TextCategory, string[]> easyTexts = new Dictionary<TextCategory, string[]>
        {
            [TextCategory.Python]     = new[] { "x = 10", "print(\"Hello World\")", "name = input()", "if x > 0:" },
            [TextCategory.CSharp]     = new[] { "int x = 0;", "Console.WriteLine(\"Hello World\");", "string name;", "bool found = false;" },
            [TextCategory.Cpp]        = new[] { "int x = 0;", "cout << \"Hello World\";", "cin >> name;", "return 0;" },
            [TextCategory.Java]       = new[] { "int x = 0;", "System.out.println(\"Hello World\");", "String name;", "return 0;" },
            [TextCategory.JavaScript] = new[] { "let x = 0;", "console.log(\"Hello World\");", "const name = \"\";", "return true;" },
        };

        private static readonly Dictionary<TextCategory, string[]> mediumTexts = new Dictionary<TextCategory, string[]>
        {
            [TextCategory.Python]     = new[] { "for i in range(10): print(i)", "def greet(name): return \"Hello \" + name", "my_list = [1, 2, 3, 4, 5]", "while count > 0: count -= 1" },
            [TextCategory.CSharp]     = new[] { "for (int i = 0; i < 10; i++)", "public string Name { get; set; }", "List<int> numbers = new List<int>();", "if (score > 100) Console.WriteLine(\"Win!\");" },
            [TextCategory.Cpp]        = new[] { "for (int i = 0; i < 10; i++)", "void printName(string name)", "vector<int> nums = {1, 2, 3};", "while (x > 0) { x--; }" },
            [TextCategory.Java]       = new[] { "for (int i = 0; i < 10; i++)", "public static void main(String[] args)", "import java.util.ArrayList;", "System.out.println(\"Score: \" + score);" },
            [TextCategory.JavaScript] = new[] { "for (let i = 0; i < 10; i++)", "function greet(name) { return \"Hi \" + name; }", "const arr = [1, 2, 3, 4, 5];", "if (score > 100) console.log(\"Win!\");" },
        };

        private static readonly Dictionary<TextCategory, string[]> hardTexts = new Dictionary<TextCategory, string[]>
        {
            [TextCategory.Python]     = new[] { "class Animal: def __init__(self, name): self.name = name", "sorted_list = sorted(my_list, key=lambda x: x[1])", "with open(\"file.txt\", \"r\") as f: data = f.read()", "result = [x**2 for x in range(10) if x % 2 == 0]" },
            [TextCategory.CSharp]     = new[] { "public class Player { public string Name { get; set; } }", "using (StreamWriter sw = new StreamWriter(\"file.txt\"))", "List<Score> scores = new List<Score>(); scores.Sort();", "private void btn_Click(object sender, EventArgs e) { }" },
            [TextCategory.Cpp]        = new[] { "class Animal { public: string name; void speak(); };", "std::sort(v.begin(), v.end(), [](int a, int b){ return a < b; });", "ifstream file(\"data.txt\"); while(getline(file, line)) { }", "template<typename T> T maxVal(T a, T b) { return a > b ? a : b; }" },
            [TextCategory.Java]       = new[] { "public class Main { public static void main(String[] args) { } }", "import java.util.ArrayList; import java.util.List;", "ArrayList<String> list = new ArrayList<>(); list.add(\"item\");", "try { readFile(); } catch (IOException e) { e.printStackTrace(); }" },
            [TextCategory.JavaScript] = new[] { "const greet = (name) => { return `Hello, ${name}!`; };", "import { useState } from 'react';", "fetch(url).then(res => res.json()).then(data => console.log(data));", "class Animal { constructor(name) { this.name = name; } }" },
        };

        private static string TextsDir =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "texts");

        private static bool IsCodeCategory(TextCategory category) =>
            category != TextCategory.English && category != TextCategory.Hebrew;

        public string[] LoadPassageLines(string filename)
        {
            string[] lines = LoadLinesFromFile(filename);
            return lines.Length > 0
                ? lines
                : new[] { "קובץ לא נמצא: " + filename };
        }

        private string[] LoadLinesFromFile(string filename)
        {
            string path = Path.Combine(TextsDir, filename);
            if (!File.Exists(path))
                return new string[0];

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            return Array.FindAll(lines, l => l.Trim().Length > 0);
        }

        private static string GetLevelSuffix(string level)
        {
            if (level == "קל")   return "easy";
            if (level == "קשה")  return "hard";
            return "medium";
        }

        private string GetSentenceFilename(TextCategory category, string level)
        {
            string lang = category == TextCategory.Hebrew ? "hebrew" : "english";
            return lang + "_sentences_" + GetLevelSuffix(level) + ".txt";
        }

        private string[] GetSentencePool(TextCategory category, string level) =>
            LoadLinesFromFile(GetSentenceFilename(category, level));

        private string GetRandomSentence(TextCategory category, string level)
        {
            string[] pool = GetSentencePool(category, level);
            return pool.Length > 0
                ? pool[rnd.Next(pool.Length)].Trim()
                : "אין טקסט זמין";
        }

        private string[] GetCodePool(TextCategory category, string level)
        {
            if (level == "קל")   return easyTexts[category];
            if (level == "קשה")  return hardTexts[category];
            return mediumTexts[category];
        }

        private static string[] Shuffle(string[] items)
        {
            if (items == null || items.Length <= 1)
                return items;

            string[] copy = (string[])items.Clone();
            for (int i = copy.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                string tmp = copy[i];
                copy[i] = copy[j];
                copy[j] = tmp;
            }
            return copy;
        }

        public string[] BuildLineBuffer(TextCategory category, string level, TextMode mode)
        {
            switch (mode)
            {
                case TextMode.Gibberish:
                    bool isHebrew = category == TextCategory.Hebrew;
                    var gibberish = new string[25];
                    for (int i = 0; i < gibberish.Length; i++)
                        gibberish[i] = GibberishGenerator.Generate(isHebrew);
                    return gibberish;

                default:
                    string[] pool = IsCodeCategory(category)
                        ? GetCodePool(category, level)
                        : GetSentencePool(category, level);
                    return Shuffle(pool);
            }
        }

        public string GetRandomText(TextCategory category, string level, TextMode mode)
        {
            if (IsCodeCategory(category))
            {
                string[] pool = GetCodePool(category, level);
                return pool[rnd.Next(pool.Length)];
            }

            if (mode == TextMode.Gibberish)
                return GibberishGenerator.Generate(category == TextCategory.Hebrew);

            return GetRandomSentence(category, level);
        }
    }
}
