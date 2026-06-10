using System;

namespace TypingPractice
{
    public class Score
    {
        public string PlayerName { get; }
        public int WPM { get; }
        public int Accuracy { get; }
        public string Level { get; }
        public DateTime Date { get; }

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

    }
}