namespace ProjectDVLD.QuickView
{
    partial class frmQuickView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TotelPeoplepanel = new System.Windows.Forms.Panel();
            this.labTotlePeopleNumber = new System.Windows.Forms.Label();
            this.labTitlePeople = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labTotleUserNumber = new System.Windows.Forms.Label();
            this.labTitleUser = new System.Windows.Forms.Label();
            this.TotelPeoplepanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TotelPeoplepanel
            // 
            this.TotelPeoplepanel.BackColor = System.Drawing.Color.SlateGray;
            this.TotelPeoplepanel.Controls.Add(this.labTotlePeopleNumber);
            this.TotelPeoplepanel.Controls.Add(this.labTitlePeople);
            this.TotelPeoplepanel.Location = new System.Drawing.Point(107, 106);
            this.TotelPeoplepanel.Name = "TotelPeoplepanel";
            this.TotelPeoplepanel.Size = new System.Drawing.Size(280, 148);
            this.TotelPeoplepanel.TabIndex = 0;
            // 
            // labTotlePeopleNumber
            // 
            this.labTotlePeopleNumber.AutoSize = true;
            this.labTotlePeopleNumber.BackColor = System.Drawing.Color.LightSteelBlue;
            this.labTotlePeopleNumber.Font = new System.Drawing.Font("Quicksand Medium", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTotlePeopleNumber.ForeColor = System.Drawing.Color.White;
            this.labTotlePeopleNumber.Location = new System.Drawing.Point(107, 66);
            this.labTotlePeopleNumber.Name = "labTotlePeopleNumber";
            this.labTotlePeopleNumber.Padding = new System.Windows.Forms.Padding(13, 8, 13, 8);
            this.labTotlePeopleNumber.Size = new System.Drawing.Size(62, 59);
            this.labTotlePeopleNumber.TabIndex = 1;
            this.labTotlePeopleNumber.Text = "0";
            // 
            // labTitlePeople
            // 
            this.labTitlePeople.AutoSize = true;
            this.labTitlePeople.Font = new System.Drawing.Font("Quicksand", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitlePeople.ForeColor = System.Drawing.Color.White;
            this.labTitlePeople.Location = new System.Drawing.Point(44, 12);
            this.labTitlePeople.Name = "labTitlePeople";
            this.labTitlePeople.Size = new System.Drawing.Size(192, 43);
            this.labTitlePeople.TabIndex = 0;
            this.labTitlePeople.Text = "Totle People";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SlateGray;
            this.panel1.Controls.Add(this.labTotleUserNumber);
            this.panel1.Controls.Add(this.labTitleUser);
            this.panel1.Location = new System.Drawing.Point(634, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 148);
            this.panel1.TabIndex = 2;
            // 
            // labTotleUserNumber
            // 
            this.labTotleUserNumber.AutoSize = true;
            this.labTotleUserNumber.BackColor = System.Drawing.Color.LightSteelBlue;
            this.labTotleUserNumber.Font = new System.Drawing.Font("Quicksand Medium", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTotleUserNumber.ForeColor = System.Drawing.Color.White;
            this.labTotleUserNumber.Location = new System.Drawing.Point(111, 66);
            this.labTotleUserNumber.Name = "labTotleUserNumber";
            this.labTotleUserNumber.Padding = new System.Windows.Forms.Padding(13, 8, 13, 8);
            this.labTotleUserNumber.Size = new System.Drawing.Size(62, 59);
            this.labTotleUserNumber.TabIndex = 1;
            this.labTotleUserNumber.Text = "0";
            // 
            // labTitleUser
            // 
            this.labTitleUser.AutoSize = true;
            this.labTitleUser.Font = new System.Drawing.Font("Quicksand", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitleUser.ForeColor = System.Drawing.Color.White;
            this.labTitleUser.Location = new System.Drawing.Point(44, 12);
            this.labTitleUser.Name = "labTitleUser";
            this.labTitleUser.Size = new System.Drawing.Size(175, 43);
            this.labTitleUser.TabIndex = 0;
            this.labTitleUser.Text = "Totle Users";
            // 
            // frmQuickView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 639);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TotelPeoplepanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmQuickView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick View";
            this.Load += new System.EventHandler(this.frmQuickView_Load);
            this.TotelPeoplepanel.ResumeLayout(false);
            this.TotelPeoplepanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TotelPeoplepanel;
        private System.Windows.Forms.Label labTitlePeople;
        private System.Windows.Forms.Label labTotlePeopleNumber;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labTotleUserNumber;
        private System.Windows.Forms.Label labTitleUser;
    }
}