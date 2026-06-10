using System;

namespace TypingPractice
{
    public class Player
    {
        public string Name { get; }
        public int TotalGames { get; private set; }
        public int TotalWPM { get; private set; }
        public int BestWPM { get; private set; }

        public Player(string name)
        {
            Name = name;
            TotalGames = 0;
            TotalWPM = 0;
            BestWPM = 0;
        }

        public void AddGame(int wpm)
        {
            TotalGames++;
            TotalWPM += wpm;
            if (wpm > BestWPM)
                BestWPM = wpm;
        }

        public int GetAverageWPM()
        {
            if (TotalGames == 0) return 0;
            return TotalWPM / TotalGames;
        }

        public string GetSummary()
        {
            return "שחקן: " + Name +
                   " | משחקים: " + TotalGames +
                   " | ממוצע WPM: " + GetAverageWPM() +
                   " | שיא: " + BestWPM;
        }
    }
}