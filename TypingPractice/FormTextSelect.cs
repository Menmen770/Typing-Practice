using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TypingPractice
{
    public partial class FormTextSelect : Form
    {
        public TextInfo SelectedText { get; private set; }

        private TextCategory category;

        public FormTextSelect(TextCategory category)
        {
            InitializeComponent();
            LoadIcon();
            this.category = category;
            LoadTexts();
        }

        private void LoadIcon()
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "TypingPractice.ico");
                if (File.Exists(path))
                    this.Icon = new Icon(path);
            }
            catch { }
        }

        private void LoadTexts()
        {
            listTexts.Items.Clear();
            foreach (TextInfo t in TextLibrary.AvailablePassages)
            {
                if (t.Category == category)
                    listTexts.Items.Add(t);
            }

            if (listTexts.Items.Count > 0)
                listTexts.SelectedIndex = 0;

            string lang = (category == TextCategory.Hebrew) ? "עברית" : "English";
            lblTitle.Text = "בחר טקסט — " + lang;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listTexts.SelectedItem == null) return;
            SelectedText = (TextInfo)listTexts.SelectedItem;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void listTexts_DoubleClick(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }
    }
}
