using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TypingPractice
{
    public class CircularStat : PictureBox
    {
        private int    _value    = 0;
        private int    _max      = 100;
        private string _label    = "";
        private Color  _arcColor = Color.FromArgb(70, 180, 130);

        private static readonly Color ColorTrack = Color.FromArgb(220, 220, 220);

        private static readonly Color PanelBg = Color.FromArgb(246, 247, 249);

        public CircularStat()
        {
            this.BackColor      = PanelBg;
            this.DoubleBuffered = true;
        }

        public void Initialize(int max, string label, Color arcColor)
        {
            _max      = max > 0 ? max : 1;
            _label    = label;
            _arcColor = arcColor;
        }

        public void SetValue(int value)
        {
            _value = value;
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

            // קשת לפי ערך / מקסימום
            float ratio   = Math.Min(1f, _max > 0 ? (float)_value / _max : 0f);
            float degrees = ratio * 360f;

            if (degrees > 0)
            {
                using (Pen arcPen = new Pen(_arcColor, thickness)
                       { StartCap = LineCap.Round, EndCap = LineCap.Round })
                {
                    g.DrawArc(arcPen, rect, -90f, degrees);
                }
            }

            // מספר ממורכז — גודל גופן יחסי לגודל הפקד
            string numText  = _value.ToString();
            float  fontSize = Math.Max(8f, Width * 0.2f);
            using (Font numFont = new Font("Arial", fontSize, FontStyle.Bold))
            using (SolidBrush numBrush = new SolidBrush(Color.FromArgb(50, 50, 50)))
            {
                SizeF sz = g.MeasureString(numText, numFont);
                g.DrawString(numText, numFont, numBrush,
                             (Width  - sz.Width)  / 2f,
                             (Height - sz.Height) / 2f);
            }
        }
    }
}
