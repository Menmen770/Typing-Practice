namespace TypingPractice
{
    partial class FormWelcome
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.logoPicture = new System.Windows.Forms.PictureBox();
            this.lblTitle    = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblPrompt   = new System.Windows.Forms.Label();
            this.txtName     = new System.Windows.Forms.TextBox();
            this.btnStart    = new System.Windows.Forms.Button();
            this.lblHint     = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // ── logoPicture — לוגו גדול ממורכז בראש הדף ────────────────
            this.logoPicture.Location  = new System.Drawing.Point(130, 18);
            this.logoPicture.Size      = new System.Drawing.Size(160, 160);
            this.logoPicture.SizeMode  = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPicture.BackColor = System.Drawing.Color.Transparent;
            this.logoPicture.TabStop   = false;

            // ── lblTitle ───────────────────────────────────────────────
            this.lblTitle.Location  = new System.Drawing.Point(0, 190);
            this.lblTitle.Size      = new System.Drawing.Size(420, 48);
            this.lblTitle.Text      = "Typing Practice";
            this.lblTitle.Font      = new System.Drawing.Font("Courier New", 22, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── lblSubtitle ────────────────────────────────────────────
            this.lblSubtitle.Location  = new System.Drawing.Point(0, 238);
            this.lblSubtitle.Size      = new System.Drawing.Size(420, 28);
            this.lblSubtitle.Text      = "תרגל הקלדה — אנגלית, עברית ושפות תכנות";
            this.lblSubtitle.Font      = new System.Drawing.Font("Arial", 10);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(140, 140, 140);
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── lblPrompt ──────────────────────────────────────────────
            this.lblPrompt.Location  = new System.Drawing.Point(80, 282);
            this.lblPrompt.Size      = new System.Drawing.Size(260, 25);
            this.lblPrompt.Text      = "הכנס את שמך כדי להתחיל:";
            this.lblPrompt.Font      = new System.Drawing.Font("Arial", 11);
            this.lblPrompt.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── txtName ────────────────────────────────────────────────
            this.txtName.Location    = new System.Drawing.Point(110, 313);
            this.txtName.Size        = new System.Drawing.Size(200, 30);
            this.txtName.Font        = new System.Drawing.Font("Arial", 13);
            this.txtName.Text        = "Player";
            this.txtName.TextAlign   = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.KeyPress   += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);

            // ── btnStart ───────────────────────────────────────────────
            this.btnStart.Location  = new System.Drawing.Point(135, 362);
            this.btnStart.Size      = new System.Drawing.Size(150, 42);
            this.btnStart.Text      = "התחל לשחק!";
            this.btnStart.Font      = new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Bold);
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnStart.Click    += new System.EventHandler(this.btnStart_Click);

            // ── lblHint ────────────────────────────────────────────────
            this.lblHint.Location  = new System.Drawing.Point(0, 420);
            this.lblHint.Size      = new System.Drawing.Size(420, 22);
            this.lblHint.Text      = "אפשר גם ללחוץ Enter";
            this.lblHint.Font      = new System.Drawing.Font("Arial", 9);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── FormWelcome ────────────────────────────────────────────
            this.ClientSize        = new System.Drawing.Size(420, 460);
            this.BackColor         = System.Drawing.Color.FromArgb(245, 245, 245);
            this.Text              = "Typing Practice";
            this.FormBorderStyle   = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox       = false;
            this.StartPosition     = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Controls.Add(this.logoPicture);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblHint);

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.PictureBox logoPicture;
        private System.Windows.Forms.Label   lblTitle;
        private System.Windows.Forms.Label   lblSubtitle;
        private System.Windows.Forms.Label   lblPrompt;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button  btnStart;
        private System.Windows.Forms.Label   lblHint;
    }
}
