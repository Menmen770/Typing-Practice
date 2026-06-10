using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TypingPractice
{
    public partial class FormScores : Form
    {
        public FormScores()
        {
            InitializeComponent();
            LoadIcon();
            ScoreTableHelper.MountTable(panelTable, gridScores, showPlayer: true);
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
            ScoreTableHelper.FillTopScores(gridScores, ScoreStore.LoadTopScores());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
