namespace TypingPractice
{
    partial class FormTextSelect
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
            this.lblTitle    = new System.Windows.Forms.Label();
            this.listTexts   = new System.Windows.Forms.ListBox();
            this.btnOK       = new System.Windows.Forms.Button();
            this.btnCancel   = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ── lblTitle ───────────────────────────────────────────────
            this.lblTitle.Location  = new System.Drawing.Point(15, 15);
            this.lblTitle.Size      = new System.Drawing.Size(310, 28);
            this.lblTitle.Font      = new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);

            // ── listTexts ──────────────────────────────────────────────
            this.listTexts.Location       = new System.Drawing.Point(15, 50);
            this.listTexts.Size           = new System.Drawing.Size(310, 180);
            this.listTexts.Font           = new System.Drawing.Font("Arial", 12);
            this.listTexts.BorderStyle    = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listTexts.DoubleClick   += new System.EventHandler(this.listTexts_DoubleClick);

            // ── btnOK ──────────────────────────────────────────────────
            this.btnOK.Location  = new System.Drawing.Point(15, 248);
            this.btnOK.Size      = new System.Drawing.Size(140, 36);
            this.btnOK.Text      = "בחר";
            this.btnOK.Font      = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.Click    += new System.EventHandler(this.btnOK_Click);

            // ── btnCancel ──────────────────────────────────────────────
            this.btnCancel.Location  = new System.Drawing.Point(185, 248);
            this.btnCancel.Size      = new System.Drawing.Size(140, 36);
            this.btnCancel.Text      = "ביטול";
            this.btnCancel.Font      = new System.Drawing.Font("Arial", 11);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnCancel.Click    += new System.EventHandler(this.btnCancel_Click);

            // ── FormTextSelect ─────────────────────────────────────────
            this.ClientSize      = new System.Drawing.Size(340, 302);
            this.BackColor       = System.Drawing.Color.FromArgb(245, 245, 245);
            this.Text            = "בחירת טקסט";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.listTexts);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label   lblTitle;
        private System.Windows.Forms.ListBox listTexts;
        private System.Windows.Forms.Button  btnOK;
        private System.Windows.Forms.Button  btnCancel;
    }
}
