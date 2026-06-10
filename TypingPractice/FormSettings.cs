using System;

using System.Drawing;

using System.Globalization;

using System.IO;

using System.Windows.Forms;



namespace TypingPractice

{

    public partial class FormSettings : Form

    {

        public int SelectedTime { get; private set; } = 30;

        public static int GetSavedTimeSeconds() => ReadStoredSeconds();

        private static readonly string SettingsPath =

            Path.Combine(Application.StartupPath, "settings.txt");



        public FormSettings()

        {

            InitializeComponent();
            AppAssets.LoadFormIcon(this);
            LoadSettings();

        }



        private void LoadSettings()

        {

            int seconds = ReadStoredSeconds();

            SelectedTime = seconds;



            decimal minutes = (decimal)seconds / 60m;

            if (minutes < numMinutes.Minimum) minutes = numMinutes.Minimum;

            if (minutes > numMinutes.Maximum) minutes = numMinutes.Maximum;

            numMinutes.Value = minutes;

        }



        private static int ReadStoredSeconds()

        {

            if (!File.Exists(SettingsPath))

                return 30;



            try

            {

                string line = File.ReadAllText(SettingsPath).Trim();

                if (line.Length == 0)

                    return 30;



                if (line.Contains(".") || line.Contains(","))

                {

                    string normalized = line.Replace(',', '.');

                    if (decimal.TryParse(normalized, NumberStyles.Any,

                            CultureInfo.InvariantCulture, out decimal minutes))

                        return MinutesToSeconds(minutes);

                }



                if (int.TryParse(line, out int value))

                {

                    // ÿÿÿÿÿÿ ÿÿÿÿÿ ÿÿÿÿ: 0=30ÿ, 1=60ÿ, 2=90ÿ

                    if (value >= 0 && value <= 2)

                        return value == 0 ? 30 : value == 1 ? 60 : 90;

                    return value;

                }

            }

            catch { }



            return 30;

        }



        private static int MinutesToSeconds(decimal minutes)

        {

            return (int)(minutes * 60m);

        }



        private void btnSave_Click(object sender, EventArgs e)

        {

            SelectedTime = MinutesToSeconds(numMinutes.Value);



            try

            {

                File.WriteAllText(SettingsPath, SelectedTime.ToString(CultureInfo.InvariantCulture));

            }

            catch { }



            DialogResult = DialogResult.OK;

            Close();

        }



        private void btnClose_Click(object sender, EventArgs e)

        {

            Close();

        }

    }

}


