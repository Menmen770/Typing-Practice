using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TypingPractice
{
    public static class ScoreStore
    {
        public const int TopCount = 10;

        private static string ScoresPath =>
            Path.Combine(Application.StartupPath, "scores.dat");

        private static string MyScoresPath =>
            Path.Combine(Application.StartupPath, "my_scores.dat");

        public static List<Score> LoadTopScores()
        {
            EnsureDefaultTopScores();
            return LoadAll(ScoresPath).Take(TopCount).ToList();
        }

        public static List<Score> LoadPersonalScores(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                return new List<Score>();

            return LoadAll(MyScoresPath)
                .Where(s => string.Equals(s.PlayerName, playerName, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(s => s.WPM)
                .ThenByDescending(s => s.Date)
                .ToList();
        }

        public static void SaveGame(Score score)
        {
            SaveTopScore(score);
            SavePersonalScore(score);
        }

        private static void SaveTopScore(Score newScore)
        {
            var scores = LoadAll(ScoresPath);
            scores.Add(newScore);
            scores = scores
                .OrderByDescending(s => s.WPM)
                .ThenByDescending(s => s.Accuracy)
                .Take(TopCount)
                .ToList();
            WriteAll(ScoresPath, scores);
        }

        private static void SavePersonalScore(Score score)
        {
            var scores = LoadAll(MyScoresPath);
            scores.Add(score);
            WriteAll(MyScoresPath, scores);
        }

        public static void EnsureDefaultTopScores()
        {
            var existing = LoadAll(ScoresPath);

            if (existing.Count > TopCount)
            {
                existing = existing
                    .OrderByDescending(s => s.WPM)
                    .ThenByDescending(s => s.Accuracy)
                    .Take(TopCount)
                    .ToList();
                WriteAll(ScoresPath, existing);
                return;
            }

            if (existing.Count >= TopCount)
                return;

            var defaults = new[]
            {
                new Score("דני",  72, 96, "קשה",    "01/01/2026"),
                new Score("מיכל", 68, 94, "בינוני", "02/01/2026"),
                new Score("יוסי", 63, 97, "בינוני", "03/01/2026"),
                new Score("שרה",  58, 95, "בינוני", "04/01/2026"),
                new Score("אבי",  55, 92, "קל",     "05/01/2026"),
                new Score("נועה", 52, 98, "קל",     "06/01/2026"),
                new Score("רון",  48, 91, "קל",     "07/01/2026"),
                new Score("ליאת", 45, 93, "קל",     "08/01/2026"),
                new Score("עומר", 42, 89, "קל",     "09/01/2026"),
                new Score("מאיה", 38, 96, "קל",     "10/01/2026"),
            };

            foreach (Score seed in defaults)
            {
                if (existing.Count >= TopCount) break;
                existing.Add(seed);
            }

            existing = existing
                .OrderByDescending(s => s.WPM)
                .ThenByDescending(s => s.Accuracy)
                .Take(TopCount)
                .ToList();

            WriteAll(ScoresPath, existing);
        }

        private static List<Score> LoadAll(string path)
        {
            var scores = new List<Score>();
            if (!File.Exists(path)) return scores;

            try
            {
                using (var f = new FileStream(path, FileMode.Open))
                using (var br = new BinaryReader(f))
                {
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        string name     = br.ReadString();
                        int    wpm      = br.ReadInt32();
                        int    accuracy = br.ReadInt32();
                        string level    = br.ReadString();
                        string date     = br.ReadString();
                        scores.Add(new Score(name, wpm, accuracy, level, date));
                    }
                }
            }
            catch
            {
                try { File.Delete(path); } catch { }
            }

            return scores;
        }

        private static void WriteAll(string path, List<Score> scores)
        {
            try
            {
                using (var f = new FileStream(path, FileMode.Create))
                using (var bw = new BinaryWriter(f))
                {
                    foreach (Score s in scores)
                    {
                        bw.Write(s.PlayerName);
                        bw.Write(s.WPM);
                        bw.Write(s.Accuracy);
                        bw.Write(s.Level);
                        bw.Write(s.Date.ToString("dd/MM/yyyy"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "שגיאה בשמירת שיאים:\n" + ex.Message,
                    "שגיאה",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
