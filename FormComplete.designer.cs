namespace JapaneseTextAnalysisTool
{
  partial class FormComplete
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComplete));
      this.buttonOpenOutputDir = new System.Windows.Forms.Button();
      this.buttonOK = new System.Windows.Forms.Button();
      this.textBoxCompleteMsg = new System.Windows.Forms.TextBox();
      this.textBoxRef = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // buttonOpenOutputDir
      // 
      this.buttonOpenOutputDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOpenOutputDir.Location = new System.Drawing.Point(12, 435);
      this.buttonOpenOutputDir.Name = "buttonOpenOutputDir";
      this.buttonOpenOutputDir.Size = new System.Drawing.Size(387, 23);
      this.buttonOpenOutputDir.TabIndex = 1;
      this.buttonOpenOutputDir.Text = "Open Output &Directory and Close Dialog";
      this.buttonOpenOutputDir.UseVisualStyleBackColor = true;
      this.buttonOpenOutputDir.Click += new System.EventHandler(this.buttonOpenOutputDir_Click);
      // 
      // buttonOK
      // 
      this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonOK.Location = new System.Drawing.Point(324, 463);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(75, 23);
      this.buttonOK.TabIndex = 0;
      this.buttonOK.Text = "&OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      // 
      // textBoxCompleteMsg
      // 
      this.textBoxCompleteMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxCompleteMsg.BackColor = System.Drawing.SystemColors.Control;
      this.textBoxCompleteMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBoxCompleteMsg.Location = new System.Drawing.Point(12, 11);
      this.textBoxCompleteMsg.Name = "textBoxCompleteMsg";
      this.textBoxCompleteMsg.ReadOnly = true;
      this.textBoxCompleteMsg.Size = new System.Drawing.Size(387, 13);
      this.textBoxCompleteMsg.TabIndex = 30;
      this.textBoxCompleteMsg.Text = "Success!";
      this.textBoxCompleteMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // textBoxRef
      // 
      this.textBoxRef.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxRef.BackColor = System.Drawing.Color.LemonChiffon;
      this.textBoxRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.textBoxRef.Location = new System.Drawing.Point(12, 30);
      this.textBoxRef.Multiline = true;
      this.textBoxRef.Name = "textBoxRef";
      this.textBoxRef.ReadOnly = true;
      this.textBoxRef.Size = new System.Drawing.Size(387, 399);
      this.textBoxRef.TabIndex = 30;
      this.textBoxRef.Text = resources.GetString("textBoxRef.Text");
      // 
      // FormComplete
      // 
      this.AcceptButton = this.buttonOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonOK;
      this.ClientSize = new System.Drawing.Size(411, 494);
      this.Controls.Add(this.textBoxRef);
      this.Controls.Add(this.textBoxCompleteMsg);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.buttonOpenOutputDir);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormComplete";
      this.ShowIcon = false;
      this.Text = "Complete";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonOpenOutputDir;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.TextBox textBoxCompleteMsg;
    private System.Windows.Forms.TextBox textBoxRef;
  }
}