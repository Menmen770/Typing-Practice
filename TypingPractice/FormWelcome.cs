using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TypingPractice
{
    public partial class FormWelcome : Form
    {
        public string PlayerName { get; private set; } = "Player";

        public FormWelcome()
        {
            InitializeComponent();
            LoadAssets();
        }

        private void LoadAssets()
        {
            string dir = Application.StartupPath;
            try
            {
                string iconPath = Path.Combine(dir, "TypingPractice.ico");
                if (File.Exists(iconPath))
                    this.Icon = new Icon(iconPath);
            }
            catch { }

            try
            {
                string logoPath = Path.Combine(dir, "Resources", "logo.png");
                if (File.Exists(logoPath))
                    logoPicture.Image = Image.FromFile(logoPath);
            }
            catch { }
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
