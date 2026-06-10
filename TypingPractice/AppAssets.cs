using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TypingPractice
{
    internal static class AppAssets
    {
        public static void LoadFormIcon(Form form)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "TypingPractice.ico");
                if (File.Exists(path))
                    form.Icon = new Icon(path);
            }
            catch { }
        }

        public static void LoadLogo(PictureBox pictureBox)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Resources", "logo.png");
                if (File.Exists(path))
                    pictureBox.Image = Image.FromFile(path);
            }
            catch { }
        }
    }
}
