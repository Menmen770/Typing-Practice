using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TypingPractice
{
    public class CircularTimer : PictureBox
    {
        private int _total   = 30;
        private int _current = 30;

        private static readonly Color ColorFull    = Color.FromArgb(70, 130, 180);  // כחול
        private static readonly Color ColorMid     = Color.FromArgb(255, 165,  0);  // כתום
        private static readonly Color ColorLow     = Color.FromArgb(220,  50, 50);  // אדום
        private static readonly Color ColorTrack   = Color.FromArgb(220, 220, 220); // רקע עיגול

        private static readonly Color PanelBg = Color.FromArgb(246, 247, 249);

        public CircularTimer()
        {
            this.Size           = new Size(80, 80);
            this.BackColor      = PanelBg;
            this.DoubleBuffered = true;
        }

        public void SetTime(int total, int current)
        {
            _total   = total > 0 ? total : 1;
            _current = current;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int margin    = 8;
            int thickness = 10;
            Rectangle rect = new Rectangle(margin, margin,
                                           Width  - margin * 2,
                                           Height - margin * 2);

            // עיגול רקע (מסלול)
            using (Pen trackPen = new Pen(ColorTrack, thickness))
                g.DrawEllipse(trackPen, rect);

            // קשת ההתקדמות
            float ratio  = (float)_current / _total;
            float degrees = ratio * 360f;
            Color arcColor = ratio > 0.5f ? ColorFull
                           : ratio > 0.25f ? ColorMid
                           : ColorLow;

            if (degrees > 0)
            {
                using (Pen arcPen = new Pen(arcColor, thickness)
                       { StartCap = LineCap.Round, EndCap = LineCap.Round })
                {
                    g.DrawArc(arcPen, rect, -90f, degrees);
                }
            }

            // מספר השניות במרכז — צבע משתנה לפי זמן שנותר
            Color textColor = ratio > 0.5f ? ColorFull
                            : ratio > 0.25f ? ColorMid
                            : ColorLow;
            string text     = _current.ToString();
            float  fontSize = Math.Max(8f, Width * 0.2f);
            using (Font f = new Font("Arial", fontSize, FontStyle.Bold))
            using (SolidBrush b = new SolidBrush(textColor))
            {
                SizeF sz = g.MeasureString(text, f);
                g.DrawString(text, f, b,
                             (Width  - sz.Width)  / 2f,
                             (Height - sz.Height) / 2f);
            }
        }
    }
}
