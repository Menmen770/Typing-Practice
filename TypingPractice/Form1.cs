using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TypingPractice
{
    public partial class Form1 : Form
    {
        private Timer gameTimer;
        private Timer blinkTimer;
        private bool  cursorOn    = true;
        private bool  gamePaused  = false;
        private Font  fontCursor;
        private Font  fontEnter;
        private GameManager manager;
        private SoundManager soundManager;

        private const int WM_SETREDRAW = 0x000B;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private Player currentPlayer;
        private string selectedLevel          = "בינוני";
        private int selectedTime              = 30;
        private TextCategory selectedCategory = TextCategory.Hebrew;
        private TextMode     selectedMode     = TextMode.Sentences;
        private string[]     passageLines     = null;

        public Form1(string playerName)
        {
            InitializeComponent();
            manager       = new GameManager();
            soundManager  = new SoundManager();
            currentPlayer = new Player(playerName);
            fontCursor    = new Font(rtbText.Font, FontStyle.Underline | FontStyle.Bold);
            fontEnter     = new Font(rtbText.Font, FontStyle.Bold);
            selectedTime  = FormSettings.GetSavedTimeSeconds();
            EnableDoubleBuffer(rtbText);
            wpmCircle.BringToFront();
            timerCircle.BringToFront();
            accCircle.BringToFront();
            SetupTimer();
            SetupTooltips();
            LoadAssets();
            lblPlayer.Text = "שחקן: " + currentPlayer.Name;
            UpdateLevelMenuChecks();
            SetCategory(TextCategory.Hebrew, "עברית");
        }

        private void SetupTimer()
        {
            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;

            blinkTimer = new Timer();
            blinkTimer.Interval = 530;
            blinkTimer.Tick += (s, e) =>
            {
                if (!manager.IsRunning || !IsLineComplete(txtInput.Text)) return;
                cursorOn = !cursorOn;
                UpdateTextDisplay(txtInput.Text);
            };
        }

        private static void EnableDoubleBuffer(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        private void LoadAssets()
        {
            try
            {
                string iconPath = Path.Combine(Application.StartupPath, "TypingPractice.ico");
                if (File.Exists(iconPath))
                    this.Icon = new System.Drawing.Icon(iconPath);
            }
            catch { }
        }

        private void SetupTooltips()
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnPause,    "עצור / המשך את המשחק");
            tt.SetToolTip(btnRestart,  "טען טקסט חדש / התחל מחדש");
            tt.SetToolTip(btnMute,     "השתק / הפעל צלילים");
            tt.SetToolTip(btnScores,   "טבלת 10 השיאים הכללית");
            tt.SetToolTip(btnMyScores, "כל התוצאות שלך");
            tt.SetToolTip(btnSettings, "שנה הגדרות משחק");
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            manager.Tick();
            timerCircle.SetTime(selectedTime, manager.TimeLeft);
            wpmCircle.SetValue(manager.GetWPM());

            if (!manager.IsRunning)
                EndGame();
        }

        // טוען טקסט חדש — הטיימר יתחיל רק עם הלחיצה הראשונה הנכונה
        private void PrepareGame()
        {
            if (selectedMode == TextMode.Passage && passageLines != null)
                manager.LoadPassage(passageLines);
            manager.PrepareGame(selectedTime, selectedLevel, selectedCategory, selectedMode);

            txtInput.Text    = "";
            txtInput.Enabled = true;
            prevTyped        = "";
            UpdateTextDisplay("");

            timerCircle.SetTime(selectedTime, selectedTime);
            wpmCircle.SetValue(0);
            accCircle.SetValue(100);
            lblPlayer.Text    = "שחקן: " + currentPlayer.Name;
            cursorOn           = true;
            gamePaused         = false;
            blinkTimer.Start();
            btnPause.Enabled   = false;
            btnPause.Text      = "?  עצור";
            btnRestart.Enabled = true;
            txtInput.Focus();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            if (!manager.IsRunning && !manager.IsReady) return;

            manager.UpdateTyping(txtInput.Text);

            // הפעל טיימר על הלחיצה הראשונה הנכונה
            if (manager.IsReady && txtInput.Text.Length > 0 &&
                txtInput.Text[0] == manager.CurrentText[0])
            {
                manager.BeginTimer();
                gameTimer.Start();
                btnPause.Enabled = true;
            }

            wpmCircle.SetValue(manager.GetWPM());
            accCircle.SetValue(manager.GetAccuracy());

            PlayKeySound(txtInput.Text);
            UpdateTextDisplay(txtInput.Text);
        }

        private string prevTyped = "";

        private void PlayKeySound(string typed)
        {
            if (typed.Length <= prevTyped.Length)
            {
                prevTyped = typed;
                return;
            }

            int  idx     = typed.Length - 1;
            char newChar = typed[idx];

            if (idx >= manager.CurrentText.Length || newChar != manager.CurrentText[idx])
                soundManager.PlayError();
            else if (newChar == ' ')
                soundManager.PlaySpace();
            else
                soundManager.PlayType();

            prevTyped = typed;
        }

        private bool IsLineComplete(string typed)
        {
            return typed.Length >= manager.CurrentText.Length
                && manager.CurrentText.Length > 0;
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (!manager.IsRunning && !manager.IsReady) return;

            e.SuppressKeyPress = true;

            if (IsLineComplete(txtInput.Text))
            {
                soundManager.PlayEnter();
                manager.NextText(txtInput.Text);
                txtInput.Text = "";
                prevTyped     = "";
                UpdateTextDisplay("");
            }
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            soundManager.Muted = !soundManager.Muted;
            if (soundManager.Muted)
            {
                btnMute.Text      = "??  השתק";
                btnMute.ForeColor = Color.FromArgb(185, 185, 185);
            }
            else
            {
                btnMute.Text      = "??  קול";
                btnMute.ForeColor = Color.FromArgb(80, 80, 80);
            }
        }

        private void UpdateTextDisplay(string typed)
        {
            SendMessage(rtbText.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            try
            {
                DrawTextDisplay(typed);
            }
            finally
            {
                SendMessage(rtbText.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
                rtbText.Refresh();
            }
        }

        private void DrawTextDisplay(string typed)
        {
            if (string.IsNullOrEmpty(manager.CurrentText))
            {
                rtbText.Clear();
                rtbText.SelectionColor = Color.Gray;
                rtbText.AppendText("לחץ התחל כדי להתחיל");
                lblProgress.Visible = false;
                return;
            }

            rtbText.Clear();

            // ?? שורות שכבר הושלמו (מעל) ???????????????????????????????
            CompletedLine[] prev = manager.GetPreviousLines(2);
            foreach (CompletedLine line in prev)
            {
                AppendColoredLine(line.Text, line.Typed, isCurrent: false);
                rtbText.AppendText("\n");
            }

            // ?? שורה נוכחית (באמצע) עם צביעה ?????????????????????????
            AppendColoredLine(manager.CurrentText, typed, isCurrent: true);

            // ?? שורות הבאות (מתחת) ????????????????????????????????????
            string[] upcoming = manager.GetUpcomingLines(3);
            for (int i = 0; i < upcoming.Length; i++)
            {
                rtbText.AppendText("\n");
                int gray = 200 + i * 15;
                AppendLine(upcoming[i],
                           Color.FromArgb(gray, gray, gray),
                           Color.White);
            }

            if (manager.LineTotal > 0)
            {
                lblProgress.Visible = true;
                lblProgress.Text    = (manager.LineIndex + 1) + " / " + manager.LineTotal;
            }
            else
            {
                lblProgress.Visible = false;
            }
        }

        private void AppendColoredLine(string text, string typed, bool isCurrent)
        {
            CharState[] states = manager.GetCharStates(text, typed);

            for (int i = 0; i < text.Length; i++)
            {
                rtbText.SelectionStart  = rtbText.TextLength;
                rtbText.SelectionLength = 0;
                rtbText.SelectionFont   = rtbText.Font;

                if (i < typed.Length)
                {
                    if (states[i] == CharState.Correct)
                    {
                        rtbText.SelectionColor     = Color.FromArgb(160, 160, 160);
                        rtbText.SelectionBackColor = Color.White;
                    }
                    else
                    {
                        rtbText.SelectionColor     = Color.Red;
                        rtbText.SelectionBackColor = Color.FromArgb(255, 220, 220);
                    }
                }
                else if (isCurrent && i == typed.Length)
                {
                    rtbText.SelectionColor     = Color.FromArgb(30, 160, 80);
                    rtbText.SelectionBackColor = Color.White;
                    rtbText.SelectionFont      = fontCursor;
                }
                else
                {
                    rtbText.SelectionColor     = Color.FromArgb(50, 50, 50);
                    rtbText.SelectionBackColor = Color.White;
                }

                rtbText.AppendText(text[i].ToString());
            }

            if (isCurrent && IsLineComplete(typed))
            {
                rtbText.SelectionStart      = rtbText.TextLength;
                rtbText.SelectionLength     = 0;
                rtbText.SelectionFont       = fontEnter;
                if (cursorOn)
                {
                    rtbText.SelectionColor     = Color.FromArgb(30, 160, 80);
                    rtbText.SelectionBackColor = Color.FromArgb(215, 245, 225);
                }
                else
                {
                    rtbText.SelectionColor     = Color.FromArgb(160, 160, 160);
                    rtbText.SelectionBackColor = Color.White;
                }
                rtbText.AppendText(" ?");
            }
        }

        private void AppendLine(string text, Color fore, Color back)
        {
            rtbText.SelectionStart      = rtbText.TextLength;
            rtbText.SelectionLength     = 0;
            rtbText.SelectionColor      = fore;
            rtbText.SelectionBackColor  = back;
            rtbText.AppendText(text);
        }

        private void EndGame()
        {
            gameTimer.Stop();
            blinkTimer.Stop();
            cursorOn   = true;
            gamePaused = false;
            manager.StopGame();
            btnPause.Enabled = false;
            btnPause.Text    = "?  עצור";
            txtInput.Enabled = false;

            int wpm      = manager.GetWPM();
            int accuracy = manager.GetAccuracy();

            currentPlayer.AddGame(wpm);
            lblPlayer.Text = currentPlayer.GetSummary();

            Score score = new Score(currentPlayer.Name, wpm, accuracy, selectedLevel);
            ScoreStore.SaveGame(score);

            MessageBox.Show(
                "סיימת!\n" +
                "WPM: " + wpm + "\n" +
                "דיוק: " + accuracy + "%\n" +
                "רמה: " + selectedLevel,
                "תוצאה",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            PrepareGame();
        }

        private void btnScores_Click(object sender, EventArgs e)
        {
            new FormScores().ShowDialog();
        }

        private void btnMyScores_Click(object sender, EventArgs e)
        {
            new FormMyScores(currentPlayer.Name).ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormSettings fs = new FormSettings();
            if (fs.ShowDialog() == DialogResult.OK)
            {
                selectedTime = fs.SelectedTime;
                timerCircle.SetTime(selectedTime, selectedTime);
            }
        }

        // ?? בחירת קטגוריה מה-MenuStrip ????????????????????????????????
        private string GetCategoryName()
        {
            string text = lblCategory.Text;
            int sep = text.IndexOf('|');
            return sep >= 0 ? text.Substring(0, sep).Trim() : text.Trim();
        }

        private void SetCategoryLabel(string categoryName)
        {
            if (selectedMode == TextMode.Sentences)
                lblCategory.Text = categoryName + " | משפטים — " + selectedLevel;
            else
                lblCategory.Text = categoryName;
        }

        private void UpdateLevelMenuChecks()
        {
            menuLevelEasy.Checked   = selectedLevel == "קל";
            menuLevelMedium.Checked = selectedLevel == "בינוני";
            menuLevelHard.Checked   = selectedLevel == "קשה";
        }

        private void SetCategory(TextCategory category, string displayName)
        {
            if (manager.IsRunning) return;
            selectedCategory = category;
            rtbText.RightToLeft = (category == TextCategory.Hebrew)
                                  ? RightToLeft.Yes : RightToLeft.No;
            SetCategoryLabel(displayName);
            PrepareGame();
        }

        private void SetMode(TextMode mode, string displayName)
        {
            if (manager.IsRunning) return;
            selectedMode = mode;
            lblCategory.Text = GetCategoryName() + " | " + displayName;
            PrepareGame();
        }

        private void SetSentencesLevel(string level)
        {
            if (manager.IsRunning) return;
            selectedLevel = level;
            selectedMode  = TextMode.Sentences;
            UpdateLevelMenuChecks();
            SetCategoryLabel(GetCategoryName());
            PrepareGame();
        }

        private void menuLevelEasy_Click(object sender, EventArgs e)   => SetSentencesLevel("קל");
        private void menuLevelMedium_Click(object sender, EventArgs e) => SetSentencesLevel("בינוני");
        private void menuLevelHard_Click(object sender, EventArgs e)   => SetSentencesLevel("קשה");

        private void menuPassage_Click(object sender, EventArgs e)
        {
            if (manager.IsRunning) return;

            bool isCode = selectedCategory != TextCategory.Hebrew &&
                          selectedCategory != TextCategory.English;
            if (isCode)
            {
                MessageBox.Show("קטע ספרותי זמין רק בעברית ואנגלית.",
                                "שים לב", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FormTextSelect fs = new FormTextSelect(selectedCategory);
            if (fs.ShowDialog(this) == DialogResult.OK)
            {
                TextLibrary lib = new TextLibrary();
                passageLines = lib.LoadPassageLines(fs.SelectedText.Filename);
                SetMode(TextMode.Passage, fs.SelectedText.DisplayName);
            }
        }

        private void menuGibberish_Click(object sender, EventArgs e) => SetMode(TextMode.Gibberish, "בלגן");

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!manager.IsRunning && !gamePaused) return;

            gamePaused = !gamePaused;
            if (gamePaused)
            {
                gameTimer.Stop();
                blinkTimer.Stop();
                btnPause.Text    = "?  המשך";
                txtInput.Enabled = false;
            }
            else
            {
                btnPause.Text    = "?  עצור";
                txtInput.Enabled = true;
                gameTimer.Start();
                blinkTimer.Start();
                txtInput.Focus();
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
            blinkTimer.Stop();
            manager.StopGame();
            btnPause.Enabled = false;
            btnPause.Text    = "?  עצור";
            PrepareGame();
        }

        private void menuEnglish_Click(object sender, EventArgs e)    => SetCategory(TextCategory.English,    "אנגלית");
        private void menuHebrew_Click(object sender, EventArgs e)     => SetCategory(TextCategory.Hebrew,     "עברית");
        private void menuPython_Click(object sender, EventArgs e)     => SetCategory(TextCategory.Python,     "Python");
        private void menuCSharp_Click(object sender, EventArgs e)     => SetCategory(TextCategory.CSharp,     "C#");
        private void menuCpp_Click(object sender, EventArgs e)        => SetCategory(TextCategory.Cpp,        "C++");
        private void menuJava_Click(object sender, EventArgs e)       => SetCategory(TextCategory.Java,       "Java");
        private void menuJavaScript_Click(object sender, EventArgs e) => SetCategory(TextCategory.JavaScript, "JavaScript");
    }
}
