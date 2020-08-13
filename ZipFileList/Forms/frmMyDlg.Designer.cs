namespace Kbg.NppPluginNET
{
    partial class frmMyDlg
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
			this.textBoxBaseFrom = new System.Windows.Forms.TextBox();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.buttonBaseFrom = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonArchiveName = new System.Windows.Forms.Button();
			this.textBoxArchiveName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonArchive = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.numericCompLevel = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericCompLevel)).BeginInit();
			this.SuspendLayout();
			// 
			// textBoxBaseFrom
			// 
			this.textBoxBaseFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxBaseFrom.Location = new System.Drawing.Point(12, 26);
			this.textBoxBaseFrom.Name = "textBoxBaseFrom";
			this.textBoxBaseFrom.Size = new System.Drawing.Size(287, 20);
			this.textBoxBaseFrom.TabIndex = 3;
			// 
			// buttonBaseFrom
			// 
			this.buttonBaseFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBaseFrom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonBaseFrom.Location = new System.Drawing.Point(305, 24);
			this.buttonBaseFrom.Name = "buttonBaseFrom";
			this.buttonBaseFrom.Size = new System.Drawing.Size(28, 23);
			this.buttonBaseFrom.TabIndex = 4;
			this.buttonBaseFrom.Text = "...";
			this.buttonBaseFrom.UseVisualStyleBackColor = true;
			this.buttonBaseFrom.Click += new System.EventHandler(this.buttonBaseFrom_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Save archive to:";
			// 
			// buttonArchiveName
			// 
			this.buttonArchiveName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonArchiveName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonArchiveName.Location = new System.Drawing.Point(305, 65);
			this.buttonArchiveName.Name = "buttonArchiveName";
			this.buttonArchiveName.Size = new System.Drawing.Size(28, 23);
			this.buttonArchiveName.TabIndex = 7;
			this.buttonArchiveName.Text = "...";
			this.buttonArchiveName.UseVisualStyleBackColor = true;
			this.buttonArchiveName.Click += new System.EventHandler(this.buttonArchiveName_Click);
			// 
			// textBoxArchiveName
			// 
			this.textBoxArchiveName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxArchiveName.Location = new System.Drawing.Point(12, 67);
			this.textBoxArchiveName.Name = "textBoxArchiveName";
			this.textBoxArchiveName.Size = new System.Drawing.Size(287, 20);
			this.textBoxArchiveName.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(186, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "For relative filenames, start process in:";
			// 
			// buttonArchive
			// 
			this.buttonArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonArchive.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonArchive.Location = new System.Drawing.Point(161, 121);
			this.buttonArchive.Name = "buttonArchive";
			this.buttonArchive.Size = new System.Drawing.Size(100, 23);
			this.buttonArchive.TabIndex = 11;
			this.buttonArchive.Text = "Create Archive";
			this.buttonArchive.UseVisualStyleBackColor = true;
			this.buttonArchive.Click += new System.EventHandler(this.buttonArchive_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(267, 121);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(66, 23);
			this.buttonCancel.TabIndex = 12;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 95);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "Compression Level";
			// 
			// numericCompLevel
			// 
			this.numericCompLevel.Location = new System.Drawing.Point(114, 93);
			this.numericCompLevel.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
			this.numericCompLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericCompLevel.Name = "numericCompLevel";
			this.numericCompLevel.Size = new System.Drawing.Size(33, 20);
			this.numericCompLevel.TabIndex = 14;
			this.numericCompLevel.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
			// 
			// frmMyDlg
			// 
			this.AcceptButton = this.buttonArchive;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(345, 156);
			this.Controls.Add(this.numericCompLevel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonArchive);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonArchiveName);
			this.Controls.Add(this.textBoxArchiveName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonBaseFrom);
			this.Controls.Add(this.textBoxBaseFrom);
			this.MinimumSize = new System.Drawing.Size(210, 195);
			this.Name = "frmMyDlg";
			this.Text = "Zip File List";
			this.Shown += new System.EventHandler(this.frmMyDlg_Shown);
			((System.ComponentModel.ISupportInitialize)(this.numericCompLevel)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxBaseFrom;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button buttonBaseFrom;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonArchiveName;
		private System.Windows.Forms.TextBox textBoxArchiveName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonArchive;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericCompLevel;
	}
}