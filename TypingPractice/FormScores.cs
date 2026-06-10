using System;
using System.Windows.Forms;

namespace TypingPractice
{
    public partial class FormScores : Form
    {
        public FormScores()
        {
            InitializeComponent();
            AppAssets.LoadFormIcon(this);
            ScoreTableHelper.MountTable(panelTable, gridScores, showPlayer: true);
            LoadScores();
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
