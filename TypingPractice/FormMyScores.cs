using System;
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
            AppAssets.LoadFormIcon(this);
            ScoreTableHelper.MountTable(panelTable, gridScores, showPlayer: false);
            lblHeaderSubtitle.Text = "כל התוצאות של " + this.playerName;
            LoadScores();
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
