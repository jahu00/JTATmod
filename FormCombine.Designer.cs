namespace JapaneseTextAnalysisTool
{
  partial class FormCombine
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCombine));
      this.buttonOutDir = new System.Windows.Forms.Button();
      this.buttonInDir = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.textBoxOutDir = new System.Windows.Forms.TextBox();
      this.textBoxInDir = new System.Windows.Forms.TextBox();
      this.buttonCombine = new System.Windows.Forms.Button();
      this.textBoxHelp = new System.Windows.Forms.TextBox();
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.SuspendLayout();
      // 
      // buttonOutDir
      // 
      this.buttonOutDir.Location = new System.Drawing.Point(12, 101);
      this.buttonOutDir.Name = "buttonOutDir";
      this.buttonOutDir.Size = new System.Drawing.Size(75, 23);
      this.buttonOutDir.TabIndex = 9;
      this.buttonOutDir.Text = "&Out Dir...";
      this.buttonOutDir.UseVisualStyleBackColor = true;
      this.buttonOutDir.Click += new System.EventHandler(this.buttonOutDir_Click);
      // 
      // buttonInDir
      // 
      this.buttonInDir.Location = new System.Drawing.Point(12, 59);
      this.buttonInDir.Name = "buttonInDir";
      this.buttonInDir.Size = new System.Drawing.Size(75, 23);
      this.buttonInDir.TabIndex = 10;
      this.buttonInDir.Text = "&In Dir...";
      this.buttonInDir.UseVisualStyleBackColor = true;
      this.buttonInDir.Click += new System.EventHandler(this.buttonInDir_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 85);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(87, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Output Directory:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 43);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(526, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Directory that contains the frequency reports to combine (and nothing else). Repo" +
          "rts must have .txt extensions.\r\n";
      // 
      // textBoxOutDir
      // 
      this.textBoxOutDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxOutDir.Location = new System.Drawing.Point(93, 103);
      this.textBoxOutDir.Name = "textBoxOutDir";
      this.textBoxOutDir.Size = new System.Drawing.Size(467, 20);
      this.textBoxOutDir.TabIndex = 5;
      // 
      // textBoxInDir
      // 
      this.textBoxInDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxInDir.Location = new System.Drawing.Point(93, 61);
      this.textBoxInDir.Name = "textBoxInDir";
      this.textBoxInDir.Size = new System.Drawing.Size(467, 20);
      this.textBoxInDir.TabIndex = 6;
      // 
      // buttonCombine
      // 
      this.buttonCombine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCombine.Location = new System.Drawing.Point(485, 128);
      this.buttonCombine.Name = "buttonCombine";
      this.buttonCombine.Size = new System.Drawing.Size(75, 23);
      this.buttonCombine.TabIndex = 4;
      this.buttonCombine.Text = "&Combine!";
      this.buttonCombine.UseVisualStyleBackColor = true;
      this.buttonCombine.Click += new System.EventHandler(this.buttonCombine_Click);
      // 
      // textBoxHelp
      // 
      this.textBoxHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxHelp.BackColor = System.Drawing.Color.LemonChiffon;
      this.textBoxHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.textBoxHelp.Location = new System.Drawing.Point(12, 12);
      this.textBoxHelp.Multiline = true;
      this.textBoxHelp.Name = "textBoxHelp";
      this.textBoxHelp.ReadOnly = true;
      this.textBoxHelp.Size = new System.Drawing.Size(548, 23);
      this.textBoxHelp.TabIndex = 115;
      this.textBoxHelp.TabStop = false;
      this.textBoxHelp.Text = "Combine two or more frequency reports. A file named combined_freq_report.txt will" +
          " be output.";
      this.textBoxHelp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // FormCombine
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(572, 163);
      this.Controls.Add(this.textBoxHelp);
      this.Controls.Add(this.buttonOutDir);
      this.Controls.Add(this.buttonInDir);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBoxOutDir);
      this.Controls.Add(this.textBoxInDir);
      this.Controls.Add(this.buttonCombine);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormCombine";
      this.Text = "Combine Frequency Reports";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonOutDir;
    private System.Windows.Forms.Button buttonInDir;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxOutDir;
    private System.Windows.Forms.TextBox textBoxInDir;
    private System.Windows.Forms.Button buttonCombine;
    private System.Windows.Forms.TextBox textBoxHelp;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
  }
}