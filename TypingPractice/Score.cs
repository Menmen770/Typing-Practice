using System;

namespace TypingPractice
{
    public class Score
    {
        public string PlayerName { get; set; }
        public int WPM { get; set; }
        public int Accuracy { get; set; }
        public string Level { get; set; }
        public DateTime Date { get; set; }

        public Score(string playerName, int wpm, int accuracy, string level)
        {
            PlayerName = playerName;
            WPM        = wpm;
            Accuracy   = accuracy;
            Level      = level;
            Date       = DateTime.Now;
        }

        public Score(string playerName, int wpm, int accuracy, string level, string dateStr)
        {
            PlayerName = playerName;
            WPM        = wpm;
            Accuracy   = accuracy;
            Level      = level;
            DateTime.TryParse(dateStr, out DateTime d);
            Date = d;
        }

        public string ToDisplayString()
        {
            return PlayerName + " | WPM: " + WPM +
                   " | דיוק: " + Accuracy + "%" +
                   " | " + Level +
                   " | " + Date.ToString("dd/MM/yyyy");
        }
    }
}