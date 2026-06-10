namespace TypingPractice
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip    = new System.Windows.Forms.MenuStrip();
            this.rtbText      = new System.Windows.Forms.RichTextBox();
            this.txtInput     = new System.Windows.Forms.TextBox();
            this.wpmCircle    = new TypingPractice.CircularStat();
            this.timerCircle  = new TypingPractice.CircularTimer();
            this.accCircle    = new TypingPractice.CircularStat();
            this.lblWpmTitle  = new System.Windows.Forms.Label();
            this.lblTimeTitle = new System.Windows.Forms.Label();
            this.lblAccTitle  = new System.Windows.Forms.Label();
            this.lblCategory  = new System.Windows.Forms.Label();
            this.lblProgress  = new System.Windows.Forms.Label();
            this.btnPause     = new System.Windows.Forms.Button();
            this.btnRestart   = new System.Windows.Forms.Button();
            this.btnMute      = new System.Windows.Forms.Button();
            this.btnScores    = new System.Windows.Forms.Button();
            this.btnSettings  = new System.Windows.Forms.Button();
            this.lblPlayer    = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // ?? MenuStrip ??????????????????????????????????????????????
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(50, 50, 60);
            this.menuStrip.ForeColor = System.Drawing.Color.White;
            this.menuStrip.Font      = new System.Drawing.Font("Arial", 10);
            this.menuStrip.Dock      = System.Windows.Forms.DockStyle.Top;

            var menuCategory = new System.Windows.Forms.ToolStripMenuItem("קטגוריה");
            menuCategory.ForeColor = System.Drawing.Color.White;

            var itemEnglish    = new System.Windows.Forms.ToolStripMenuItem("אנגלית");
            var itemHebrew     = new System.Windows.Forms.ToolStripMenuItem("עברית");
            var itemCode       = new System.Windows.Forms.ToolStripMenuItem("תכנות");
            var itemPython     = new System.Windows.Forms.ToolStripMenuItem("Python");
            var itemCSharp     = new System.Windows.Forms.ToolStripMenuItem("C#");
            var itemCpp        = new System.Windows.Forms.ToolStripMenuItem("C++");
            var itemJava       = new System.Windows.Forms.ToolStripMenuItem("Java");
            var itemJavaScript = new System.Windows.Forms.ToolStripMenuItem("JavaScript");

            itemCode.DropDownItems.Add(itemPython);
            itemCode.DropDownItems.Add(itemCSharp);
            itemCode.DropDownItems.Add(itemCpp);
            itemCode.DropDownItems.Add(itemJava);
            itemCode.DropDownItems.Add(itemJavaScript);

            menuCategory.DropDownItems.Add(itemEnglish);
            menuCategory.DropDownItems.Add(itemHebrew);
            menuCategory.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
            menuCategory.DropDownItems.Add(itemCode);

            itemEnglish.Click    += new System.EventHandler(this.menuEnglish_Click);
            itemHebrew.Click     += new System.EventHandler(this.menuHebrew_Click);
            itemPython.Click     += new System.EventHandler(this.menuPython_Click);
            itemCSharp.Click     += new System.EventHandler(this.menuCSharp_Click);
            itemCpp.Click        += new System.EventHandler(this.menuCpp_Click);
            itemJava.Click       += new System.EventHandler(this.menuJava_Click);
            itemJavaScript.Click += new System.EventHandler(this.menuJavaScript_Click);

            var menuMode = new System.Windows.Forms.ToolStripMenuItem("מצב");
            menuMode.ForeColor = System.Drawing.Color.White;

            this.menuSentences = new System.Windows.Forms.ToolStripMenuItem("משפטים");
            var itemPassage     = new System.Windows.Forms.ToolStripMenuItem("קטע ספרותי");
            var itemGibberish   = new System.Windows.Forms.ToolStripMenuItem("בלגן");

            this.menuLevelEasy = new System.Windows.Forms.ToolStripMenuItem("קל");
            this.menuLevelMedium = new System.Windows.Forms.ToolStripMenuItem("בינוני");
            this.menuLevelHard = new System.Windows.Forms.ToolStripMenuItem("קשה");

            this.menuLevelEasy.Click   += new System.EventHandler(this.menuLevelEasy_Click);
            this.menuLevelMedium.Click += new System.EventHandler(this.menuLevelMedium_Click);
            this.menuLevelHard.Click   += new System.EventHandler(this.menuLevelHard_Click);

            this.menuSentences.DropDownItems.Add(this.menuLevelEasy);
            this.menuSentences.DropDownItems.Add(this.menuLevelMedium);
            this.menuSentences.DropDownItems.Add(this.menuLevelHard);

            itemPassage.Click   += new System.EventHandler(this.menuPassage_Click);
            itemGibberish.Click += new System.EventHandler(this.menuGibberish_Click);

            menuMode.DropDownItems.Add(this.menuSentences);
            menuMode.DropDownItems.Add(itemPassage);
            menuMode.DropDownItems.Add(itemGibberish);

            var menuGame = new System.Windows.Forms.ToolStripMenuItem("משחק");
            menuGame.ForeColor = System.Drawing.Color.White;

            var itemScores   = new System.Windows.Forms.ToolStripMenuItem("שיאים");
            var itemMyScores = new System.Windows.Forms.ToolStripMenuItem("השיאים שלי");
            var itemSettings = new System.Windows.Forms.ToolStripMenuItem("הגדרות");
            var itemExit     = new System.Windows.Forms.ToolStripMenuItem("יציאה");

            itemScores.Click   += new System.EventHandler(this.btnScores_Click);
            itemMyScores.Click += new System.EventHandler(this.btnMyScores_Click);
            itemSettings.Click += new System.EventHandler(this.btnSettings_Click);
            itemExit.Click     += new System.EventHandler((s, e) => this.Close());

            menuGame.DropDownItems.Add(itemScores);
            menuGame.DropDownItems.Add(itemMyScores);
            menuGame.DropDownItems.Add(itemSettings);
            menuGame.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
            menuGame.DropDownItems.Add(itemExit);

            this.menuStrip.Items.Add(menuCategory);
            this.menuStrip.Items.Add(menuMode);
            this.menuStrip.Items.Add(menuGame);

            // ?? rtbText — תיבת טקסט מלאה ???????????????????????????????
            this.rtbText.Location    = new System.Drawing.Point(24, 38);
            this.rtbText.Size        = new System.Drawing.Size(748, 195);
            this.rtbText.Font        = new System.Drawing.Font("Consolas", 15);
            this.rtbText.ReadOnly    = true;
            this.rtbText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbText.BackColor   = System.Drawing.Color.White;
            this.rtbText.ScrollBars  = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbText.TabStop     = false;
            this.rtbText.Cursor      = System.Windows.Forms.Cursors.Default;
            this.rtbText.Text        = "בחר קטגוריה ומצב מהתפריט כדי להתחיל";
            this.rtbText.ForeColor   = System.Drawing.Color.Gray;
            this.rtbText.MouseDown  += new System.Windows.Forms.MouseEventHandler(
                (s, e) => this.txtInput.Focus());

            // ?? lblCategory + lblProgress (מיד מתחת לטקסט) ????????????
            this.lblCategory.Location  = new System.Drawing.Point(26, 238);
            this.lblCategory.Size      = new System.Drawing.Size(380, 17);
            this.lblCategory.Text      = "אנגלית";
            this.lblCategory.Font      = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(80, 140, 200);
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblProgress.Location  = new System.Drawing.Point(580, 238);
            this.lblProgress.Size      = new System.Drawing.Size(190, 17);
            this.lblProgress.BackColor = System.Drawing.Color.FromArgb(246, 247, 249);
            this.lblProgress.Text      = "";
            this.lblProgress.Font      = new System.Drawing.Font("Arial", 9);
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProgress.Visible   = false;

            // ?? 3 עיגולים ממורכזים 100×100 (WPM | זמן | דיוק) ?????????
            // רוחב כולל = 3×100 + 2×50 = 400  ?  x מתחיל ב-200  (800px)
            this.wpmCircle.Location  = new System.Drawing.Point(200, 262);
            this.wpmCircle.Size      = new System.Drawing.Size(100, 100);
            this.wpmCircle.BackColor = System.Drawing.Color.FromArgb(246, 247, 249);
            this.wpmCircle.TabStop   = false;
            this.wpmCircle.Initialize(150, "WPM",
                System.Drawing.Color.FromArgb(60, 180, 130));

            this.timerCircle.Location  = new System.Drawing.Point(350, 262);
            this.timerCircle.Size      = new System.Drawing.Size(100, 100);
            this.timerCircle.BackColor = System.Drawing.Color.FromArgb(246, 247, 249);
            this.timerCircle.TabStop   = false;

            this.accCircle.Location  = new System.Drawing.Point(500, 262);
            this.accCircle.Size      = new System.Drawing.Size(100, 100);
            this.accCircle.BackColor = System.Drawing.Color.FromArgb(246, 247, 249);
            this.accCircle.TabStop   = false;
            this.accCircle.Initialize(100, "דיוק %",
                System.Drawing.Color.FromArgb(225, 145, 55));

            // ?? תוויות מתחת לעיגולים ????????????????????????????????????
            this.lblWpmTitle.Location  = new System.Drawing.Point(200, 366);
            this.lblWpmTitle.Size      = new System.Drawing.Size(100, 18);
            this.lblWpmTitle.Text      = "WPM";
            this.lblWpmTitle.Font      = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);
            this.lblWpmTitle.ForeColor = System.Drawing.Color.FromArgb(60, 180, 130);
            this.lblWpmTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTimeTitle.Location  = new System.Drawing.Point(350, 366);
            this.lblTimeTitle.Size      = new System.Drawing.Size(100, 18);
            this.lblTimeTitle.Text      = "זמן";
            this.lblTimeTitle.Font      = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);
            this.lblTimeTitle.ForeColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.lblTimeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblAccTitle.Location  = new System.Drawing.Point(500, 366);
            this.lblAccTitle.Size      = new System.Drawing.Size(100, 18);
            this.lblAccTitle.Text      = "דיוק %";
            this.lblAccTitle.Font      = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);
            this.lblAccTitle.ForeColor = System.Drawing.Color.FromArgb(225, 145, 55);
            this.lblAccTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ?? txtInput (קלט מוסתר) ????????????????????????????????????
            this.txtInput.Location    = new System.Drawing.Point(0, 0);
            this.txtInput.Size        = new System.Drawing.Size(1, 1);
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInput.BackColor   = System.Drawing.SystemColors.Control;
            this.txtInput.TabStop     = false;
            this.txtInput.Enabled     = false;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            this.txtInput.KeyDown     += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);

            // ?? כפתורים ממורכזים ?????????????????????????????????????????
            // 3 × 140 + 2 × 20 = 460  ?  x מתחיל ב-170
            var btnStyle = new System.Action<System.Windows.Forms.Button, string, int>(
                (btn, txt, x) => {
                    btn.Location  = new System.Drawing.Point(x, 398);
                    btn.Size      = new System.Drawing.Size(140, 36);
                    btn.Text      = txt;
                    btn.Font      = new System.Drawing.Font("Arial", 11);
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
                });

            btnStyle(this.btnRestart, "?  חזרה",   170);
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(237, 248, 241);
            this.btnRestart.ForeColor = System.Drawing.Color.FromArgb(45, 140, 85);
            this.btnRestart.Enabled   = false;
            this.btnRestart.Click    += new System.EventHandler(this.btnRestart_Click);

            btnStyle(this.btnPause,   "?  עצור",  330);
            this.btnPause.BackColor = System.Drawing.Color.FromArgb(235, 242, 250);
            this.btnPause.ForeColor = System.Drawing.Color.FromArgb(60, 100, 160);
            this.btnPause.Enabled   = false;
            this.btnPause.Click    += new System.EventHandler(this.btnPause_Click);

            btnStyle(this.btnMute,    "??  השתק",  490);
            this.btnMute.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.btnMute.ForeColor = System.Drawing.Color.FromArgb(185, 185, 185);
            this.btnMute.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(225, 225, 225);
            this.btnMute.Enabled   = true;
            this.btnMute.Click    += new System.EventHandler(this.btnMute_Click);

            // ?? שורה תחתונה: שחקן + שיאים + הגדרות ?????????????????????
            this.btnMyScores = new System.Windows.Forms.Button();

            this.lblPlayer.Location  = new System.Drawing.Point(24, 446);
            this.lblPlayer.Size      = new System.Drawing.Size(400, 24);
            this.lblPlayer.Text      = "שחקן: —";
            this.lblPlayer.Font      = new System.Drawing.Font("Arial", 10);
            this.lblPlayer.ForeColor = System.Drawing.Color.FromArgb(110, 110, 110);

            this.btnScores.Location  = new System.Drawing.Point(432, 444);
            this.btnScores.Size      = new System.Drawing.Size(88, 26);
            this.btnScores.Text      = "?? שיאים";
            this.btnScores.Font      = new System.Drawing.Font("Arial", 9);
            this.btnScores.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.btnScores.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.btnScores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScores.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(210, 210, 210);
            this.btnScores.Click    += new System.EventHandler(this.btnScores_Click);

            this.btnMyScores.Location  = new System.Drawing.Point(526, 444);
            this.btnMyScores.Size      = new System.Drawing.Size(110, 26);
            this.btnMyScores.Text      = "?? השיאים שלי";
            this.btnMyScores.Font      = new System.Drawing.Font("Arial", 9);
            this.btnMyScores.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.btnMyScores.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.btnMyScores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMyScores.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(210, 210, 210);
            this.btnMyScores.Click    += new System.EventHandler(this.btnMyScores_Click);

            this.btnSettings.Location  = new System.Drawing.Point(642, 444);
            this.btnSettings.Size      = new System.Drawing.Size(104, 26);
            this.btnSettings.Text      = "?  הגדרות";
            this.btnSettings.Font      = new System.Drawing.Font("Arial", 9);
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(210, 210, 210);
            this.btnSettings.Click    += new System.EventHandler(this.btnSettings_Click);

            // ?? Form1 ???????????????????????????????????????????????????
            this.ClientSize    = new System.Drawing.Size(800, 480);
            this.BackColor     = System.Drawing.Color.FromArgb(246, 247, 249);
            this.Text          = "Typing Practice";
            this.Font          = new System.Drawing.Font("Arial", 9);
            this.MainMenuStrip = this.menuStrip;

            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.rtbText);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.wpmCircle);
            this.Controls.Add(this.timerCircle);
            this.Controls.Add(this.accCircle);
            this.Controls.Add(this.lblWpmTitle);
            this.Controls.Add(this.lblTimeTitle);
            this.Controls.Add(this.lblAccTitle);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnMute);
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.btnScores);
            this.Controls.Add(this.btnMyScores);
            this.Controls.Add(this.btnSettings);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip          menuStrip;
        private System.Windows.Forms.ToolStripMenuItem  menuSentences;
        private System.Windows.Forms.ToolStripMenuItem  menuLevelEasy;
        private System.Windows.Forms.ToolStripMenuItem  menuLevelMedium;
        private System.Windows.Forms.ToolStripMenuItem  menuLevelHard;
        private System.Windows.Forms.RichTextBox        rtbText;
        private System.Windows.Forms.TextBox            txtInput;
        private System.Windows.Forms.Label              lblCategory;
        private System.Windows.Forms.Label              lblProgress;
        private TypingPractice.CircularStat              wpmCircle;
        private TypingPractice.CircularTimer             timerCircle;
        private TypingPractice.CircularStat              accCircle;
        private System.Windows.Forms.Label              lblWpmTitle;
        private System.Windows.Forms.Label              lblTimeTitle;
        private System.Windows.Forms.Label              lblAccTitle;
        private System.Windows.Forms.Button             btnPause;
        private System.Windows.Forms.Button             btnRestart;
        private System.Windows.Forms.Button             btnMute;
        private System.Windows.Forms.Button             btnScores;
        private System.Windows.Forms.Button             btnMyScores;
        private System.Windows.Forms.Button             btnSettings;
        private System.Windows.Forms.Label              lblPlayer;
    }
}
