namespace TypingPractice

{

    partial class FormSettings

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

            this.lblTitle = new System.Windows.Forms.Label();

            this.lblTime = new System.Windows.Forms.Label();

            this.numMinutes = new System.Windows.Forms.NumericUpDown();

            this.lblMinutesUnit = new System.Windows.Forms.Label();

            this.btnSave = new System.Windows.Forms.Button();

            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).BeginInit();

            this.SuspendLayout();



            // lblTitle

            this.lblTitle.Location = new System.Drawing.Point(20, 15);

            this.lblTitle.Size = new System.Drawing.Size(340, 30);

            this.lblTitle.Text = "⚙️ הגדרות משחק";

            this.lblTitle.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);

            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;



            // שורה אחת (RTL): מגבלת זמן המשימה:  [מספר]  דקות

            this.lblTime.Location = new System.Drawing.Point(175, 62);

            this.lblTime.Size = new System.Drawing.Size(185, 28);

            this.lblTime.Text = "מגבלת זמן המשימה:";

            this.lblTime.Font = new System.Drawing.Font("Arial", 11);

            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;



            this.numMinutes.Location = new System.Drawing.Point(88, 60);

            this.numMinutes.Size = new System.Drawing.Size(80, 28);

            this.numMinutes.Font = new System.Drawing.Font("Arial", 12);

            this.numMinutes.DecimalPlaces = 1;

            this.numMinutes.Increment = 0.5m;

            this.numMinutes.Minimum = 0.5m;

            this.numMinutes.Maximum = 10m;

            this.numMinutes.Value = 0.5m;

            this.numMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;



            this.lblMinutesUnit.Location = new System.Drawing.Point(28, 62);

            this.lblMinutesUnit.Size = new System.Drawing.Size(55, 28);

            this.lblMinutesUnit.Text = "דקות";

            this.lblMinutesUnit.Font = new System.Drawing.Font("Arial", 11);

            this.lblMinutesUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;



            // btnSave

            this.btnSave.Location = new System.Drawing.Point(60, 115);

            this.btnSave.Size = new System.Drawing.Size(110, 35);

            this.btnSave.Text = "שמור";

            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);



            // btnClose

            this.btnClose.Location = new System.Drawing.Point(200, 115);

            this.btnClose.Size = new System.Drawing.Size(110, 35);

            this.btnClose.Text = "ביטול";

            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);



            // Form

            this.ClientSize = new System.Drawing.Size(380, 170);

            this.Controls.Add(this.lblTitle);

            this.Controls.Add(this.lblTime);

            this.Controls.Add(this.lblMinutesUnit);

            this.Controls.Add(this.numMinutes);

            this.Controls.Add(this.btnSave);

            this.Controls.Add(this.btnClose);

            this.Text = "הגדרות";

            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).EndInit();

            this.ResumeLayout(false);

        }



        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblTime;

        private System.Windows.Forms.NumericUpDown numMinutes;

        private System.Windows.Forms.Label lblMinutesUnit;

        private System.Windows.Forms.Button btnSave;

        private System.Windows.Forms.Button btnClose;

    }

}


