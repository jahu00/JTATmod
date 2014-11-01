namespace JapaneseTextAnalysisTool
{
  partial class FormCompare
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCompare));
      this.buttonDiff = new System.Windows.Forms.Button();
      this.buttonOutDir = new System.Windows.Forms.Button();
      this.buttonFileB = new System.Windows.Forms.Button();
      this.buttonFileA = new System.Windows.Forms.Button();
      this.textBoxOutDir = new System.Windows.Forms.TextBox();
      this.textBoxFileB = new System.Windows.Forms.TextBox();
      this.textBoxFileA = new System.Windows.Forms.TextBox();
      this.textBoxHelp = new System.Windows.Forms.TextBox();
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.SuspendLayout();
      // 
      // buttonDiff
      // 
      this.buttonDiff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonDiff.Location = new System.Drawing.Point(454, 163);
      this.buttonDiff.Name = "buttonDiff";
      this.buttonDiff.Size = new System.Drawing.Size(75, 23);
      this.buttonDiff.TabIndex = 23;
      this.buttonDiff.Text = "Compare!";
      this.buttonDiff.UseVisualStyleBackColor = true;
      this.buttonDiff.Click += new System.EventHandler(this.buttonCompare_Click);
      // 
      // buttonOutDir
      // 
      this.buttonOutDir.Location = new System.Drawing.Point(12, 135);
      this.buttonOutDir.Name = "buttonOutDir";
      this.buttonOutDir.Size = new System.Drawing.Size(75, 23);
      this.buttonOutDir.TabIndex = 20;
      this.buttonOutDir.Text = "&Output Dir...";
      this.buttonOutDir.UseVisualStyleBackColor = true;
      this.buttonOutDir.Click += new System.EventHandler(this.buttonOutDir_Click);
      // 
      // buttonFileB
      // 
      this.buttonFileB.Location = new System.Drawing.Point(12, 109);
      this.buttonFileB.Name = "buttonFileB";
      this.buttonFileB.Size = new System.Drawing.Size(75, 23);
      this.buttonFileB.TabIndex = 21;
      this.buttonFileB.Text = "&Report B...";
      this.buttonFileB.UseVisualStyleBackColor = true;
      this.buttonFileB.Click += new System.EventHandler(this.buttonFileB_Click);
      // 
      // buttonFileA
      // 
      this.buttonFileA.Location = new System.Drawing.Point(12, 83);
      this.buttonFileA.Name = "buttonFileA";
      this.buttonFileA.Size = new System.Drawing.Size(75, 23);
      this.buttonFileA.TabIndex = 22;
      this.buttonFileA.Text = "&Report A...";
      this.buttonFileA.UseVisualStyleBackColor = true;
      this.buttonFileA.Click += new System.EventHandler(this.buttonFileA_Click);
      // 
      // textBoxOutDir
      // 
      this.textBoxOutDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxOutDir.Location = new System.Drawing.Point(93, 137);
      this.textBoxOutDir.Name = "textBoxOutDir";
      this.textBoxOutDir.Size = new System.Drawing.Size(436, 20);
      this.textBoxOutDir.TabIndex = 14;
      // 
      // textBoxFileB
      // 
      this.textBoxFileB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxFileB.Location = new System.Drawing.Point(93, 111);
      this.textBoxFileB.Name = "textBoxFileB";
      this.textBoxFileB.Size = new System.Drawing.Size(436, 20);
      this.textBoxFileB.TabIndex = 16;
      // 
      // textBoxFileA
      // 
      this.textBoxFileA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxFileA.Location = new System.Drawing.Point(93, 85);
      this.textBoxFileA.Name = "textBoxFileA";
      this.textBoxFileA.Size = new System.Drawing.Size(436, 20);
      this.textBoxFileA.TabIndex = 15;
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
      this.textBoxHelp.Size = new System.Drawing.Size(517, 62);
      this.textBoxHelp.TabIndex = 114;
      this.textBoxHelp.TabStop = false;
      this.textBoxHelp.Text = resources.GetString("textBoxHelp.Text");
      // 
      // openFileDialog
      // 
      this.openFileDialog.FileName = "openFileDialog1";
      // 
      // FormCompare
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(541, 198);
      this.Controls.Add(this.textBoxHelp);
      this.Controls.Add(this.buttonDiff);
      this.Controls.Add(this.buttonOutDir);
      this.Controls.Add(this.buttonFileB);
      this.Controls.Add(this.buttonFileA);
      this.Controls.Add(this.textBoxOutDir);
      this.Controls.Add(this.textBoxFileB);
      this.Controls.Add(this.textBoxFileA);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormCompare";
      this.Text = "Compare Frequency Reports";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonDiff;
    private System.Windows.Forms.Button buttonOutDir;
    private System.Windows.Forms.Button buttonFileB;
    private System.Windows.Forms.Button buttonFileA;
    private System.Windows.Forms.TextBox textBoxOutDir;
    private System.Windows.Forms.TextBox textBoxFileB;
    private System.Windows.Forms.TextBox textBoxFileA;
    private System.Windows.Forms.TextBox textBoxHelp;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
  }
}