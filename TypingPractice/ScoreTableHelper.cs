using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TypingPractice
{
    internal static class ScoreTableHelper
    {
        public static readonly Size FormClientSize = new Size(560, 546);

        private const int MaxVisibleRows = 10;
        private const int RowHeight      = 36;
        private const int HeaderHeight   = 40;

        private static readonly Color HeaderBg  = Color.FromArgb(45, 52, 64);
        private static readonly Color HeaderFg  = Color.White;
        private static readonly Color RowEven   = Color.FromArgb(250, 251, 253);
        private static readonly Color RowOdd    = Color.White;
        private static readonly Color GridLine  = Color.FromArgb(225, 229, 235);
        private static readonly Color TextMain  = Color.FromArgb(35, 40, 50);
        private static readonly Color TextMuted = Color.FromArgb(120, 128, 140);

        public static void MountTable(Panel host, DataGridView grid, bool showPlayer)
        {
            SetupGrid(grid, showPlayer);
            host.Controls.Add(grid);
            host.Resize += (s, e) => CenterGrid(host, grid);
            CenterGrid(host, grid);
        }

        private static void SetupGrid(DataGridView grid, bool showPlayer)
        {
            grid.ReadOnly                  = true;
            grid.AllowUserToAddRows        = false;
            grid.AllowUserToDeleteRows     = false;
            grid.AllowUserToResizeRows     = false;
            grid.AllowUserToResizeColumns  = false;
            grid.RowHeadersVisible         = false;
            grid.BorderStyle               = BorderStyle.FixedSingle;
            grid.BackgroundColor           = Color.White;
            grid.GridColor                 = GridLine;
            grid.CellBorderStyle           = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.SelectionMode             = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect               = false;
            grid.TabStop                   = false;
            grid.ScrollBars                = ScrollBars.None;
            grid.Dock                      = DockStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle  = DataGridViewHeaderBorderStyle.None;
            grid.RowTemplate.Height        = RowHeight;
            grid.Font                      = new Font("Arial", 10);
            grid.ColumnHeadersHeight       = HeaderHeight;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersDefaultCellStyle.BackColor = HeaderBg;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = HeaderFg;
            grid.ColumnHeadersDefaultCellStyle.Font      = new Font("Arial", 10, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.DefaultCellStyle.BackColor              = RowEven;
            grid.DefaultCellStyle.ForeColor              = TextMain;
            grid.DefaultCellStyle.SelectionBackColor     = Color.FromArgb(235, 240, 248);
            grid.DefaultCellStyle.SelectionForeColor     = TextMain;
            grid.DefaultCellStyle.Alignment              = DataGridViewContentAlignment.MiddleCenter;
            grid.AlternatingRowsDefaultCellStyle.BackColor = RowOdd;
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 240, 248);

            grid.Columns.Clear();
            grid.Columns.Add(BuildColumn("rank",  "#",     44));
            grid.Columns.Add(BuildColumn("wpm",   "WPM",   72));
            grid.Columns.Add(BuildColumn("acc",   "דיוק",  72));
            grid.Columns.Add(BuildColumn("level", "רמה",   88));
            grid.Columns.Add(BuildColumn("date",  "תאריך", 96));

            if (showPlayer)
                grid.Columns.Insert(1, BuildColumn("player", "שחקן", 130));

            grid.Columns["wpm"].DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            int width = 0;
            foreach (DataGridViewColumn col in grid.Columns)
                width += col.Width;
            grid.Width  = width + 2;
            grid.Height = HeaderHeight + MaxVisibleRows * RowHeight + 2;
        }

        private static DataGridViewTextBoxColumn BuildColumn(string name, string header, int width)
        {
            return new DataGridViewTextBoxColumn
            {
                Name         = name,
                HeaderText   = header,
                Width        = width,
                SortMode     = DataGridViewColumnSortMode.NotSortable,
                ReadOnly     = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Resizable    = DataGridViewTriState.False
            };
        }

        private static void CenterGrid(Panel host, DataGridView grid)
        {
            grid.Location = new Point(
                Math.Max(0, (host.ClientSize.Width  - grid.Width)  / 2),
                Math.Max(0, (host.ClientSize.Height - grid.Height) / 2));
        }

        public static void FillTopScores(DataGridView grid, IList<Score> scores)
        {
            grid.Rows.Clear();

            if (scores.Count == 0)
            {
                grid.Rows.Add("—", "אין שיאים", "—", "—", "—", "—");
                grid.Rows[0].DefaultCellStyle.ForeColor = TextMuted;
                ClearSelection(grid);
                return;
            }

            for (int i = 0; i < scores.Count; i++)
            {
                Score s = scores[i];
                grid.Rows.Add(
                    (i + 1).ToString(),
                    s.PlayerName,
                    s.WPM,
                    s.Accuracy + "%",
                    s.Level,
                    s.Date.ToString("dd/MM/yy"));
            }

            ClearSelection(grid);
        }

        public static void FillPersonalScores(DataGridView grid, IList<Score> scores)
        {
            grid.Rows.Clear();

            if (scores.Count == 0)
            {
                grid.Rows.Add("—", "—", "—", "—", "—");
                grid.Rows[0].Cells["wpm"].Value = "אין שיאים עדיין — שחק!";
                grid.Rows[0].DefaultCellStyle.ForeColor = TextMuted;
                ClearSelection(grid);
                return;
            }

            for (int i = 0; i < scores.Count; i++)
            {
                Score s = scores[i];
                grid.Rows.Add(
                    (i + 1).ToString(),
                    s.WPM,
                    s.Accuracy + "%",
                    s.Level,
                    s.Date.ToString("dd/MM/yy"));
            }

            ClearSelection(grid);
        }

        private static void ClearSelection(DataGridView grid)
        {
            grid.ClearSelection();
            grid.CurrentCell = null;
        }

        public static Panel CreateHeader(string title, string subtitle)
        {
            var panel = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 72,
                BackColor = HeaderBg
            };

            var lblTitle = new Label
            {
                Text      = title,
                ForeColor = Color.White,
                Font      = new Font("Arial", 15, FontStyle.Bold),
                Dock      = DockStyle.Top,
                Height    = 36,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblSub = new Label
            {
                Text      = subtitle,
                ForeColor = Color.FromArgb(180, 190, 205),
                Font      = new Font("Arial", 9),
                Dock      = DockStyle.Top,
                Height    = 24,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panel.Controls.Add(lblSub);
            panel.Controls.Add(lblTitle);
            return panel;
        }

        public static Button CreateCloseButton()
        {
            var btn = new Button
            {
                Text      = "סגור",
                Size      = new Size(120, 34),
                Font      = new Font("Arial", 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(240, 242, 246),
                ForeColor = Color.FromArgb(60, 68, 80),
                Cursor    = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(200, 206, 214);
            return btn;
        }

        public static void ApplyFormLayout(Form form, Panel panelFooter, Button btnClose)
        {
            form.ClientSize        = FormClientSize;
            form.FormBorderStyle   = FormBorderStyle.FixedSingle;
            form.MaximizeBox       = false;
            form.StartPosition     = FormStartPosition.CenterParent;
            form.BackColor         = Color.White;
            form.RightToLeft       = RightToLeft.Yes;
            form.RightToLeftLayout = true;

            panelFooter.Dock      = DockStyle.Bottom;
            panelFooter.Height    = 54;
            panelFooter.BackColor = Color.FromArgb(248, 249, 251);
            btnClose.Location     = new Point((FormClientSize.Width - btnClose.Width) / 2, 10);
        }
    }
}
