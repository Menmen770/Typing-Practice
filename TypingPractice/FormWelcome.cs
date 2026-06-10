using System;
using System.Windows.Forms;

namespace TypingPractice
{
    public partial class FormWelcome : Form
    {
        public string PlayerName { get; private set; } = "Player";

        public FormWelcome()
        {
            InitializeComponent();
            AppAssets.LoadFormIcon(this);
            AppAssets.LoadLogo(logoPicture);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            PlayerName      = name.Length > 0 ? name : "Player";
            DialogResult    = DialogResult.OK;
            Close();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnStart_Click(sender, e);
        }
    }
}
