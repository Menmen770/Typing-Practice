namespace TypingPractice
{
    partial class FormMyScores
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
            this.panelHeader       = ScoreTableHelper.CreateHeader(
                "📊  השיאים שלי",
                "כל התוצאות שלך");
            this.lblHeaderSubtitle = this.panelHeader.Controls[0] as System.Windows.Forms.Label;
            this.panelTable        = new System.Windows.Forms.Panel();
            this.gridScores        = new System.Windows.Forms.DataGridView();
            this.panelFooter       = new System.Windows.Forms.Panel();
            this.btnClose          = ScoreTableHelper.CreateCloseButton();
            this.panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridScores)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();

            this.panelTable.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.panelTable.Padding   = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.panelTable.BackColor = System.Drawing.Color.White;

            this.panelFooter.Controls.Add(this.btnClose);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.Controls.Add(this.panelTable);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Text = "השיאים שלי";
            ScoreTableHelper.ApplyFormLayout(this, this.panelFooter, this.btnClose);

            this.panelTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridScores)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.DataGridView gridScores;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnClose;
    }
}
