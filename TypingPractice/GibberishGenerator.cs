using System;
using System.Text;

namespace TypingPractice
{
    public static class GibberishGenerator
    {
        private static readonly Random rnd = new Random();

        private static readonly char[] hebrewChars =
            "אבגדהוזחטיכלמנסעפצקרשת".ToCharArray();

        private static readonly char[] englishChars =
            "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        private static readonly char[] digits  = "0123456789".ToCharArray();
        private static readonly char[] symbols = "!@#$%-_=+;:,.?".ToCharArray();

        public static string Generate(bool isHebrew, int targetLength = 45)
        {
            char[] letters = isHebrew ? hebrewChars : englishChars;
            StringBuilder sb = new StringBuilder();
            int pos = 0;

            while (pos < targetLength)
            {
                int wordLen = rnd.Next(2, 7);

                for (int i = 0; i < wordLen && pos < targetLength; i++, pos++)
                {
                    int roll = rnd.Next(10);
                    if (roll < 6)
                        sb.Append(letters[rnd.Next(letters.Length)]);
                    else if (roll < 8)
                        sb.Append(digits[rnd.Next(digits.Length)]);
                    else
                        sb.Append(symbols[rnd.Next(symbols.Length)]);
                }

                if (pos < targetLength)
                {
                    sb.Append(' ');
                    pos++;
                }
            }

            return sb.ToString().Trim();
        }
    }
}
