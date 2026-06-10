using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TypingPractice
{
    public partial class FormMyScores : Form
    {
        private readonly string playerName;

        public FormMyScores(string playerName)
        {
            this.playerName = playerName ?? "";
            InitializeComponent();
            LoadIcon();
            ScoreTableHelper.MountTable(panelTable, gridScores, showPlayer: false);
            lblHeaderSubtitle.Text = "כל התוצאות של " + this.playerName;
            LoadScores();
        }

        private void LoadIcon()
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "TypingPractice.ico");
                if (File.Exists(path))
                    Icon = new Icon(path);
            }
            catch { }
        }

        private void LoadScores()
        {
            ScoreTableHelper.FillPersonalScores(gridScores, ScoreStore.LoadPersonalScores(playerName));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
