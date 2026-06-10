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

        // ── רשימת הטקסטים הזמינים לקטע ספרותי ──────────────────────────
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

        // ── טעינת כל שורות הטקסט לפי שם קובץ ───────────────────────────
        public string[] LoadPassageLines(string filename)
        {
            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "texts", filename);
            if (!File.Exists(path))
                return new[] { "קובץ לא נמצא: " + filename };
            string[] all = File.ReadAllLines(path, Encoding.UTF8);
            return Array.FindAll(all, l => l.Trim().Length > 0);
        }

        // ── טקסטים קצרים (משפטים) — hardcoded ─────────────────────────
        private Dictionary<TextCategory, string[]> easyTexts = new Dictionary<TextCategory, string[]>
        {
            [TextCategory.English] = new[] {
                "the cat sat on the mat",
                "i love to code every day",
                "hello world this is fun",
                "the sun is bright today",
            },
            [TextCategory.Hebrew] = new[] {
                "השמש זורחת בבוקר",
                "אני אוהב ללמוד תכנות",
                "המחשב הוא כלי עבודה",
                "הספר מונח על השולחן",
            },
            [TextCategory.Python]     = new[] { "x = 10", "print(\"Hello World\")", "name = input()", "if x > 0:" },
            [TextCategory.CSharp]     = new[] { "int x = 0;", "Console.WriteLine(\"Hello World\");", "string name;", "bool found = false;" },
            [TextCategory.Cpp]        = new[] { "int x = 0;", "cout << \"Hello World\";", "cin >> name;", "return 0;" },
            [TextCategory.Java]       = new[] { "int x = 0;", "System.out.println(\"Hello World\");", "String name;", "return 0;" },
            [TextCategory.JavaScript] = new[] { "let x = 0;", "console.log(\"Hello World\");", "const name = \"\";", "return true;" },
        };

        private Dictionary<TextCategory, string[]> mediumTexts = new Dictionary<TextCategory, string[]>
        {
            [TextCategory.English] = new[] {
                "the quick brown fox jumps over the lazy dog",
                "practice makes perfect every single day",
                "windows forms is fun to learn and use",
                "programming is the art of solving problems",
            },
            [TextCategory.Hebrew] = new[] {
                "תכנות זה לא רק כתיבת קוד אלא פתרון בעיות",
                "כדי להצליח צריך להתאמן בכל יום",
                "המרצה ביקש לבנות פרויקט מסודר ומאורגן",
                "ממשק משתמש טוב חשוב לא פחות מהקוד עצמו",
            },
            [TextCategory.Python]     = new[] { "for i in range(10): print(i)", "def greet(name): return \"Hello \" + name", "my_list = [1, 2, 3, 4, 5]", "while count > 0: count -= 1" },
            [TextCategory.CSharp]     = new[] { "for (int i = 0; i < 10; i++)", "public string Name { get; set; }", "List<int> numbers = new List<int>();", "if (score > 100) Console.WriteLine(\"Win!\");" },
            [TextCategory.Cpp]        = new[] { "for (int i = 0; i < 10; i++)", "void printName(string name)", "vector<int> nums = {1, 2, 3};", "while (x > 0) { x--; }" },
            [TextCategory.Java]       = new[] { "for (int i = 0; i < 10; i++)", "public static void main(String[] args)", "import java.util.ArrayList;", "System.out.println(\"Score: \" + score);" },
            [TextCategory.JavaScript] = new[] { "for (let i = 0; i < 10; i++)", "function greet(name) { return \"Hi \" + name; }", "const arr = [1, 2, 3, 4, 5];", "if (score > 100) console.log(\"Win!\");" },
        };

        private Dictionary<TextCategory, string[]> hardTexts = new Dictionary<TextCategory, string[]>
        {
            [TextCategory.English] = new[] {
                "the complex algorithm runs in O(n log n) time",
                "visual studio supports multiple programming languages",
                "binary files store data as sequences of bytes",
                "object oriented programming uses classes and objects",
            },
            [TextCategory.Hebrew] = new[] {
                "אלגוריתם מיון בועות עובר על המערך שוב ושוב עד שהוא ממוין",
                "תכנות מונחה עצמים מבוסס על מחלקות ירושה ופולימורפיזם",
                "מבנה נתונים מתאים יכול לשפר משמעותית את ביצועי התוכנית",
                "פיתוח תוכנה מחייב תכנון מוקדם ותיעוד מסודר של הקוד",
            },
            [TextCategory.Python]     = new[] { "class Animal: def __init__(self, name): self.name = name", "sorted_list = sorted(my_list, key=lambda x: x[1])", "with open(\"file.txt\", \"r\") as f: data = f.read()", "result = [x**2 for x in range(10) if x % 2 == 0]" },
            [TextCategory.CSharp]     = new[] { "public class Player { public string Name { get; set; } }", "using (StreamWriter sw = new StreamWriter(\"file.txt\"))", "List<Score> scores = new List<Score>(); scores.Sort();", "private void btn_Click(object sender, EventArgs e) { }" },
            [TextCategory.Cpp]        = new[] { "class Animal { public: string name; void speak(); };", "std::sort(v.begin(), v.end(), [](int a, int b){ return a < b; });", "ifstream file(\"data.txt\"); while(getline(file, line)) { }", "template<typename T> T maxVal(T a, T b) { return a > b ? a : b; }" },
            [TextCategory.Java]       = new[] { "public class Main { public static void main(String[] args) { } }", "import java.util.ArrayList; import java.util.List;", "ArrayList<String> list = new ArrayList<>(); list.add(\"item\");", "try { readFile(); } catch (IOException e) { e.printStackTrace(); }" },
            [TextCategory.JavaScript] = new[] { "const greet = (name) => { return `Hello, ${name}!`; };", "import { useState } from 'react';", "fetch(url).then(res => res.json()).then(data => console.log(data));", "class Animal { constructor(name) { this.name = name; } }" },
        };

        // ── קריאה מקובץ ─────────────────────────────────────────────────
        private static string TextsDir =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "texts");

        private string[] LoadLinesFromFile(string filename)
        {
            string path = Path.Combine(TextsDir, filename);
            if (!File.Exists(path))
                return new string[0];

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            return Array.FindAll(lines, l => l.Trim().Length > 0);
        }

        private string GetLineFromFile(string filename)
        {
            string[] valid = LoadLinesFromFile(filename);
            return valid.Length > 0
                ? valid[rnd.Next(valid.Length)].Trim()
                : "קובץ טקסט לא נמצא: " + filename;
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

        // מאחד קובץ + מילון לפי רמה
        private string GetRandomSentence(TextCategory category, string level)
        {
            var pool = new List<string>();
            pool.AddRange(LoadLinesFromFile(GetSentenceFilename(category, level)));
            pool.AddRange(GetDictionaryPool(category, level));

            if (pool.Count == 0)
                return "אין טקסט זמין";

            return pool[rnd.Next(pool.Count)].Trim();
        }

        private string[] GetDictionaryPool(TextCategory category, string level)
        {
            if (level == "קל")         return easyTexts[category];
            if (level == "קשה")        return hardTexts[category];
            return mediumTexts[category];
        }

        private string[] GetSentencePool(TextCategory category, string level)
        {
            var pool = new List<string>();
            pool.AddRange(LoadLinesFromFile(GetSentenceFilename(category, level)));
            pool.AddRange(GetDictionaryPool(category, level));
            return pool.ToArray();
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

                default: // Sentences + קוד
                    bool isCode = category != TextCategory.English &&
                                  category != TextCategory.Hebrew;
                    string[] pool = isCode
                        ? GetDictionaryPool(category, level)
                        : GetSentencePool(category, level);
                    return Shuffle(pool);
            }
        }

        // ── API ראשי ─────────────────────────────────────────────────────
        public string GetRandomText(TextCategory category, string level, TextMode mode)
        {
            bool isCode = category != TextCategory.English &&
                          category != TextCategory.Hebrew;

            // קטגוריות קוד — תמיד משפטים, mode לא רלוונטי
            if (isCode)
                return GetFromDictionary(category, level);

            switch (mode)
            {
                case TextMode.Gibberish:
                    return GibberishGenerator.Generate(category == TextCategory.Hebrew);

                case TextMode.Passage:
                    string file = category == TextCategory.Hebrew
                        ? "hebrew_passage.txt"
                        : "english_passage.txt";
                    return GetLineFromFile(file);

                default: // Sentences — קובץ לפי רמה + מילון
                    return GetRandomSentence(category, level);
            }
        }

        private string GetFromDictionary(TextCategory category, string level)
        {
            string[] pool = GetDictionaryPool(category, level);
            return pool[rnd.Next(pool.Length)];
        }

        // תאימות לאחור
        public string GetRandomText(TextCategory category, string level)
            => GetRandomText(category, level, TextMode.Sentences);

        public string GetRandomText(string level)
            => GetRandomText(TextCategory.English, level, TextMode.Sentences);
    }
}
