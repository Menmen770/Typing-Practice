using System;
using System.Collections.Generic;

namespace TypingPractice

{

    public enum CharState { Untyped, Correct, Wrong }

    public struct CompletedLine
    {
        public string Text;
        public string Typed;
    }



    public class GameManager

    {

        public int TimeLeft { get; private set; }

        public int CorrectChars { get; private set; }

        public int WrongChars { get; private set; }

        public int TotalCorrectChars { get; private set; }

        public int TotalWrongChars { get; private set; }

        public bool IsRunning      { get; private set; }

        public bool IsReady        { get; private set; }

        public int  ElapsedSeconds { get; private set; }

        public string CurrentText { get; private set; }

        public string CurrentLevel { get; private set; }

        public TextCategory CurrentCategory { get; private set; }

        public TextMode     CurrentMode     { get; private set; }



        private TextLibrary library;

        private string[]    lineBuffer;

        private int         lineIndex;

        private List<CompletedLine> lineHistory;



        public GameManager()

        {

            library         = new TextLibrary();

            CurrentLevel    = "בינוני";

            CurrentCategory = TextCategory.English;

            CurrentMode     = TextMode.Sentences;

        }



        // טוען טקסט וממתין ללחיצה הראשונה (IsReady=true, IsRunning=false)

        public void PrepareGame(int seconds, string level, TextCategory category, TextMode mode)

        {

            CurrentLevel      = level;

            CurrentCategory   = category;

            CurrentMode       = mode;

            TimeLeft          = seconds;

            CorrectChars      = 0;

            WrongChars        = 0;

            TotalCorrectChars = 0;

            TotalWrongChars   = 0;

            IsRunning         = false;

            IsReady           = true;

            lineIndex         = 0;

            lineHistory       = new List<CompletedLine>();



            if (mode == TextMode.Passage && lineBuffer != null && lineBuffer.Length > 0)

                CurrentText = lineBuffer[0];

            else

            {

                lineBuffer  = library.BuildLineBuffer(category, level, mode);

                CurrentText = lineBuffer.Length > 0 ? lineBuffer[0] : "";

            }

        }



        // מתחיל את הטיימר לאחר PrepareGame

        public void BeginTimer()

        {

            if (!IsReady) return;

            IsReady        = false;

            IsRunning      = true;

            ElapsedSeconds = 0;

        }



        public void Tick()

        {

            if (!IsRunning) return;

            TimeLeft--;

            ElapsedSeconds++;

            if (TimeLeft <= 0)

                IsRunning = false;

        }



        public void UpdateTyping(string typed)

        {

            CorrectChars = 0;

            WrongChars = 0;

            for (int i = 0; i < typed.Length && i < CurrentText.Length; i++)

            {

                if (typed[i] == CurrentText[i])

                    CorrectChars++;

                else

                    WrongChars++;

            }

        }



        // WPM אמיתי: (תווים נכונים / 5) / (שניות שחלפו / 60)

        public int GetWPM()

        {

            if (ElapsedSeconds == 0) return 0;

            int totalChars = TotalCorrectChars + CorrectChars;

            return (totalChars * 60) / (5 * ElapsedSeconds);

        }



        public int GetAccuracy()

        {

            int correct = TotalCorrectChars + CorrectChars;

            int total   = correct + TotalWrongChars + WrongChars;

            return total > 0 ? (correct * 100 / total) : 100;

        }



        public void NextText(string typed)

        {

            if (!string.IsNullOrEmpty(CurrentText))

                lineHistory.Add(new CompletedLine { Text = CurrentText, Typed = typed ?? "" });

            TotalCorrectChars += CorrectChars;

            TotalWrongChars   += WrongChars;

            CorrectChars       = 0;

            WrongChars         = 0;



            if (lineBuffer != null && lineBuffer.Length > 0)

            {

                lineIndex   = (lineIndex + 1) % lineBuffer.Length;

                CurrentText = lineBuffer[lineIndex];

            }

            else

            {

                CurrentText = library.GetRandomText(CurrentCategory, CurrentLevel, CurrentMode);

            }

        }



        public int LineIndex => lineIndex;

        public int LineTotal => lineBuffer != null ? lineBuffer.Length : 0;



        public string[] LoadPassageLines(string filename) => library.LoadPassageLines(filename);

        public string[] GetUpcomingLines(int count)

        {

            if (lineBuffer == null) return new string[0];

            int available = Math.Min(count, lineBuffer.Length - lineIndex - 1);

            if (available <= 0) return new string[0];

            string[] result = new string[available];

            for (int i = 0; i < available; i++)

                result[i] = lineBuffer[lineIndex + 1 + i];

            return result;

        }



        public CompletedLine[] GetPreviousLines(int count)

        {

            if (lineHistory == null || lineHistory.Count == 0) return new CompletedLine[0];

            int start     = Math.Max(0, lineHistory.Count - count);

            int available = lineHistory.Count - start;

            CompletedLine[] result = new CompletedLine[available];

            for (int i = 0; i < available; i++)

                result[i] = lineHistory[start + i];

            return result;

        }



        public void LoadPassage(string[] lines)

        {

            lineBuffer  = lines;

            lineIndex   = 0;

            lineHistory = new List<CompletedLine>();

        }



        public void StopGame()

        {

            IsRunning = false;

            IsReady   = false;

        }



        public CharState[] GetCharStates(string text, string typed)

        {

            if (string.IsNullOrEmpty(text)) return new CharState[0];

            CharState[] states = new CharState[text.Length];

            for (int i = 0; i < text.Length; i++)

            {

                if (i < typed.Length)

                    states[i] = (typed[i] == text[i]) ? CharState.Correct : CharState.Wrong;

                else

                    states[i] = CharState.Untyped;

            }

            return states;

        }

    }

}


