using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypingPractice
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormWelcome welcome = new FormWelcome();
            if (welcome.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Application.Run(new Form1(welcome.PlayerName));
        }
    }
}
