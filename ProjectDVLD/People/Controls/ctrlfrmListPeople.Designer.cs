﻿namespace ProjectDVLD.UserControls
{
    partial class ctrlfrmListPeople
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlfrmListPeople));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.picBoxManagePeople = new System.Windows.Forms.PictureBox();
            this.labManagePeople = new System.Windows.Forms.Label();
            this.labFilterBy = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.picBoxAddPerson = new System.Windows.Forms.PictureBox();
            this.DGVGetAllPeople = new System.Windows.Forms.DataGridView();
            this.cmsPeople = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AddNewPersontoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.phoneCallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labRecords = new System.Windows.Forms.Label();
            this.labRecordsCount = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxManagePeople)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAddPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVGetAllPeople)).BeginInit();
            this.cmsPeople.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBoxManagePeople
            // 
            this.picBoxManagePeople.Image = ((System.Drawing.Image)(resources.GetObject("picBoxManagePeople.Image")));
            this.picBoxManagePeople.Location = new System.Drawing.Point(694, 27);
            this.picBoxManagePeople.Name = "picBoxManagePeople";
            this.picBoxManagePeople.Size = new System.Drawing.Size(228, 161);
            this.picBoxManagePeople.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxManagePeople.TabIndex = 0;
            this.picBoxManagePeople.TabStop = false;
            // 
            // labManagePeople
            // 
            this.labManagePeople.AutoSize = true;
            this.labManagePeople.Font = new System.Drawing.Font("Quicksand", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labManagePeople.ForeColor = System.Drawing.Color.Firebrick;
            this.labManagePeople.Location = new System.Drawing.Point(692, 190);
            this.labManagePeople.Name = "labManagePeople";
            this.labManagePeople.Size = new System.Drawing.Size(234, 43);
            this.labManagePeople.TabIndex = 1;
            this.labManagePeople.Text = "Manage People";
            // 
            // labFilterBy
            // 
            this.labFilterBy.AutoSize = true;
            this.labFilterBy.Font = new System.Drawing.Font("Quicksand Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFilterBy.Location = new System.Drawing.Point(24, 252);
            this.labFilterBy.Name = "labFilterBy";
            this.labFilterBy.Size = new System.Drawing.Size(78, 24);
            this.labFilterBy.TabIndex = 2;
            this.labFilterBy.Text = "Filter By :";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Quicksand Medium", 12F, System.Drawing.FontStyle.Bold);
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No",
            "First Name",
            "Second Name",
            "Third Name",
            "Last Name",
            "Nationality",
            "Gender",
            "Phone",
            "Email"});
            this.cbFilterBy.Location = new System.Drawing.Point(123, 252);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(180, 32);
            this.cbFilterBy.TabIndex = 3;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // picBoxAddPerson
            // 
            this.picBoxAddPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxAddPerson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBoxAddPerson.Image = ((System.Drawing.Image)(resources.GetObject("picBoxAddPerson.Image")));
            this.picBoxAddPerson.Location = new System.Drawing.Point(1492, 226);
            this.picBoxAddPerson.Name = "picBoxAddPerson";
            this.picBoxAddPerson.Size = new System.Drawing.Size(68, 50);
            this.picBoxAddPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxAddPerson.TabIndex = 4;
            this.picBoxAddPerson.TabStop = false;
            this.picBoxAddPerson.Click += new System.EventHandler(this.picBoxAddPerson_Click);
            // 
            // DGVGetAllPeople
            // 
            this.DGVGetAllPeople.AllowUserToAddRows = false;
            this.DGVGetAllPeople.AllowUserToDeleteRows = false;
            this.DGVGetAllPeople.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVGetAllPeople.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVGetAllPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVGetAllPeople.ContextMenuStrip = this.cmsPeople;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Quicksand Medium", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVGetAllPeople.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGVGetAllPeople.Location = new System.Drawing.Point(28, 293);
            this.DGVGetAllPeople.Name = "DGVGetAllPeople";
            this.DGVGetAllPeople.ReadOnly = true;
            this.DGVGetAllPeople.Size = new System.Drawing.Size(1532, 333);
            this.DGVGetAllPeople.TabIndex = 5;
            this.DGVGetAllPeople.DoubleClick += new System.EventHandler(this.DGVGetAllPeople_DoubleClick);
            // 
            // cmsPeople
            // 
            this.cmsPeople.Font = new System.Drawing.Font("Quicksand Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsPeople.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem,
            this.toolStripSeparator2,
            this.AddNewPersontoolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.sendEmailToolStripMenuItem,
            this.phoneCallToolStripMenuItem});
            this.cmsPeople.Name = "contextMenuStrip1";
            this.cmsPeople.Size = new System.Drawing.Size(228, 314);
            // 
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showDetailsToolStripMenuItem.Image")));
            this.showDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(227, 46);
            this.showDetailsToolStripMenuItem.Text = "&Show Details";
            this.showDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDetailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(224, 6);
            // 
            // AddNewPersontoolStripMenuItem
            // 
            this.AddNewPersontoolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddNewPersontoolStripMenuItem.Image")));
            this.AddNewPersontoolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.AddNewPersontoolStripMenuItem.Name = "AddNewPersontoolStripMenuItem";
            this.AddNewPersontoolStripMenuItem.Size = new System.Drawing.Size(227, 46);
            this.AddNewPersontoolStripMenuItem.Text = "Add &New Person";
            this.AddNewPersontoolStripMenuItem.Click += new System.EventHandler(this.AddNewPersontoolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripMenuItem.Image")));
            this.editToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(227, 46);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(227, 46);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(224, 6);
            // 
            // sendEmailToolStripMenuItem
            // 
            this.sendEmailToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sendEmailToolStripMenuItem.Image")));
            this.sendEmailToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sendEmailToolStripMenuItem.Name = "sendEmailToolStripMenuItem";
            this.sendEmailToolStripMenuItem.Size = new System.Drawing.Size(227, 46);
            this.sendEmailToolStripMenuItem.Text = "Send E&mail";
            this.sendEmailToolStripMenuItem.Click += new System.EventHandler(this.sendEmailToolStripMenuItem_Click);
            // 
            // phoneCallToolStripMenuItem
            // 
            this.phoneCallToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("phoneCallToolStripMenuItem.Image")));
            this.phoneCallToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.phoneCallToolStripMenuItem.Name = "phoneCallToolStripMenuItem";
            this.phoneCallToolStripMenuItem.Size = new System.Drawing.Size(227, 46);
            this.phoneCallToolStripMenuItem.Text = "Phone &Call";
            this.phoneCallToolStripMenuItem.Click += new System.EventHandler(this.phoneCallToolStripMenuItem_Click);
            // 
            // labRecords
            // 
            this.labRecords.AutoSize = true;
            this.labRecords.Font = new System.Drawing.Font("Quicksand Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labRecords.Location = new System.Drawing.Point(24, 640);
            this.labRecords.Name = "labRecords";
            this.labRecords.Size = new System.Drawing.Size(94, 24);
            this.labRecords.TabIndex = 6;
            this.labRecords.Text = "# Records :";
            // 
            // labRecordsCount
            // 
            this.labRecordsCount.AutoSize = true;
            this.labRecordsCount.Font = new System.Drawing.Font("Quicksand Medium", 12F, System.Drawing.FontStyle.Bold);
            this.labRecordsCount.Location = new System.Drawing.Point(137, 640);
            this.labRecordsCount.Name = "labRecordsCount";
            this.labRecordsCount.Size = new System.Drawing.Size(20, 24);
            this.labRecordsCount.TabIndex = 7;
            this.labRecordsCount.Text = "0";
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Quicksand Medium", 12F, System.Drawing.FontStyle.Bold);
            this.txtFilter.Location = new System.Drawing.Point(316, 252);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(240, 27);
            this.txtFilter.TabIndex = 8;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // ctrlfrmListPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.labRecordsCount);
            this.Controls.Add(this.labRecords);
            this.Controls.Add(this.DGVGetAllPeople);
            this.Controls.Add(this.picBoxAddPerson);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.labFilterBy);
            this.Controls.Add(this.labManagePeople);
            this.Controls.Add(this.picBoxManagePeople);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ctrlfrmListPeople";
            this.Size = new System.Drawing.Size(1600, 686);
            this.Load += new System.EventHandler(this.ctrlfrmListPeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxManagePeople)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAddPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVGetAllPeople)).EndInit();
            this.cmsPeople.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxManagePeople;
        private System.Windows.Forms.Label labManagePeople;
        private System.Windows.Forms.Label labFilterBy;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.PictureBox picBoxAddPerson;
        private System.Windows.Forms.DataGridView DGVGetAllPeople;
        private System.Windows.Forms.Label labRecords;
        private System.Windows.Forms.Label labRecordsCount;
        private System.Windows.Forms.ContextMenuStrip cmsPeople;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem AddNewPersontoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem sendEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem phoneCallToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFilter;
    }
}
