namespace JapaneseTextAnalysisTool
{
  partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.combineFrequencyReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelInput = new System.Windows.Forms.Label();
            this.textBoxInFile = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelOperation = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCurrentFile = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarTotal = new System.Windows.Forms.ProgressBar();
            this.buttonInDir = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.labelTotalPercentage = new System.Windows.Forms.Label();
            this.buttonOutputFile = new System.Windows.Forms.Button();
            this.labelOutput = new System.Windows.Forms.Label();
            this.textBoxOutDir = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.numericUpDownTrimFront = new System.Windows.Forms.NumericUpDown();
            this.labelTrimFront = new System.Windows.Forms.Label();
            this.labelTrimBack = new System.Windows.Forms.Label();
            this.numericUpDownTrimBack = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelEncoding = new System.Windows.Forms.Label();
            this.comboBoxWordFreqMethod = new System.Windows.Forms.ComboBox();
            this.checkBoxKanjify = new System.Windows.Forms.CheckBox();
            this.labelParseMethod = new System.Windows.Forms.Label();
            this.checkBoxWordFreq = new System.Windows.Forms.CheckBox();
            this.checkBoxKanjiFreq = new System.Windows.Forms.CheckBox();
            this.checkBoxFormulaReadability = new System.Windows.Forms.CheckBox();
            this.checkBoxUserReadability = new System.Windows.Forms.CheckBox();
            this.checkBoxRemoveAozaroFormatting = new System.Windows.Forms.CheckBox();
            this.labelKnownWordsListCol = new System.Windows.Forms.Label();
            this.labelExtensions = new System.Windows.Forms.Label();
            this.checkBoxSubfolders = new System.Windows.Forms.CheckBox();
            this.labelWordRemovalListCol = new System.Windows.Forms.Label();
            this.labelWordRemoval = new System.Windows.Forms.Label();
            this.labelKnownWordsList = new System.Windows.Forms.Label();
            this.comboBoxEncoding = new System.Windows.Forms.ComboBox();
            this.groupBoxReport = new System.Windows.Forms.GroupBox();
            this.groupBoxPreProcessing = new System.Windows.Forms.GroupBox();
            this.buttonInFile = new System.Windows.Forms.Button();
            this.groupBoxWordFreqOptions = new System.Windows.Forms.GroupBox();
            this.numericUpDownWordRemovalListCol = new System.Windows.Forms.NumericUpDown();
            this.buttonWordRemovalListFile = new System.Windows.Forms.Button();
            this.textBoxWordRemoval = new System.Windows.Forms.TextBox();
            this.groupBoxUserReadabilityOptions = new System.Windows.Forms.GroupBox();
            this.numericUpDownKnownWordsListCol = new System.Windows.Forms.NumericUpDown();
            this.textBoxKnownWordsList = new System.Windows.Forms.TextBox();
            this.buttonKnownWordsListFile = new System.Windows.Forms.Button();
            this.backgroundWorkerMain = new System.ComponentModel.BackgroundWorker();
            this.textBoxExtensions = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxFolderSeparator = new System.Windows.Forms.TextBox();
            this.labelFolderSeparator = new System.Windows.Forms.Label();
            this.checkBoxSeparateReports = new System.Windows.Forms.CheckBox();
            this.menuStripMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrimFront)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrimBack)).BeginInit();
            this.groupBoxReport.SuspendLayout();
            this.groupBoxPreProcessing.SuspendLayout();
            this.groupBoxWordFreqOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWordRemovalListCol)).BeginInit();
            this.groupBoxUserReadabilityOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKnownWordsListCol)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStripMain.Size = new System.Drawing.Size(619, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItemExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItemExit
            // 
            this.exitToolStripMenuItemExit.Name = "exitToolStripMenuItemExit";
            this.exitToolStripMenuItemExit.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItemExit.Text = "E&xit";
            this.exitToolStripMenuItemExit.Click += new System.EventHandler(this.exitToolStripMenuItemExit_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combineFrequencyReportsToolStripMenuItem,
            this.diffToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // combineFrequencyReportsToolStripMenuItem
            // 
            this.combineFrequencyReportsToolStripMenuItem.Name = "combineFrequencyReportsToolStripMenuItem";
            this.combineFrequencyReportsToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.combineFrequencyReportsToolStripMenuItem.Text = "Combi&ne Frequency Reports...";
            this.combineFrequencyReportsToolStripMenuItem.Click += new System.EventHandler(this.combineFrequencyReportsToolStripMenuItem_Click);
            // 
            // diffToolStripMenuItem
            // 
            this.diffToolStripMenuItem.Name = "diffToolStripMenuItem";
            this.diffToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.diffToolStripMenuItem.Text = "&Compare Frequency Reports...";
            this.diffToolStripMenuItem.Click += new System.EventHandler(this.diffToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(9, 30);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(110, 13);
            this.labelInput.TabIndex = 1;
            this.labelInput.Text = "Input File or Directory:";
            this.toolTip1.SetToolTip(this.labelInput, "Specify either a single file to analyze or a \r\ndirectory that contains the files " +
        "to analyze.");
            // 
            // textBoxInFile
            // 
            this.textBoxInFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInFile.Location = new System.Drawing.Point(98, 46);
            this.textBoxInFile.Name = "textBoxInFile";
            this.textBoxInFile.Size = new System.Drawing.Size(419, 20);
            this.textBoxInFile.TabIndex = 5;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(518, 481);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(87, 23);
            this.buttonStart.TabIndex = 16;
            this.buttonStart.Text = "&Analyze!";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelOperation,
            this.toolStripStatusLabelCurrentFile});
            this.statusStrip1.Location = new System.Drawing.Point(0, 532);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(619, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelOperation
            // 
            this.toolStripStatusLabelOperation.AutoSize = false;
            this.toolStripStatusLabelOperation.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelOperation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabelOperation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripStatusLabelOperation.Name = "toolStripStatusLabelOperation";
            this.toolStripStatusLabelOperation.Size = new System.Drawing.Size(80, 17);
            this.toolStripStatusLabelOperation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelCurrentFile
            // 
            this.toolStripStatusLabelCurrentFile.Name = "toolStripStatusLabelCurrentFile";
            this.toolStripStatusLabelCurrentFile.Size = new System.Drawing.Size(0, 17);
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarTotal.Location = new System.Drawing.Point(50, 481);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Size = new System.Drawing.Size(433, 23);
            this.progressBarTotal.TabIndex = 15;
            this.progressBarTotal.Visible = false;
            // 
            // buttonInDir
            // 
            this.buttonInDir.Location = new System.Drawing.Point(54, 44);
            this.buttonInDir.Name = "buttonInDir";
            this.buttonInDir.Size = new System.Drawing.Size(42, 23);
            this.buttonInDir.TabIndex = 4;
            this.buttonInDir.Text = "&Dir...";
            this.buttonInDir.UseVisualStyleBackColor = true;
            this.buttonInDir.Click += new System.EventHandler(this.buttonInDir_Click);
            // 
            // labelTotalPercentage
            // 
            this.labelTotalPercentage.AutoSize = true;
            this.labelTotalPercentage.Location = new System.Drawing.Point(8, 486);
            this.labelTotalPercentage.Name = "labelTotalPercentage";
            this.labelTotalPercentage.Size = new System.Drawing.Size(30, 13);
            this.labelTotalPercentage.TabIndex = 14;
            this.labelTotalPercentage.Text = "0.0%";
            this.labelTotalPercentage.Visible = false;
            // 
            // buttonOutputFile
            // 
            this.buttonOutputFile.Location = new System.Drawing.Point(12, 163);
            this.buttonOutputFile.Name = "buttonOutputFile";
            this.buttonOutputFile.Size = new System.Drawing.Size(84, 23);
            this.buttonOutputFile.TabIndex = 8;
            this.buttonOutputFile.Text = "&Out Dir...";
            this.buttonOutputFile.UseVisualStyleBackColor = true;
            this.buttonOutputFile.Click += new System.EventHandler(this.buttonOutputFile_Click);
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(9, 147);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(87, 13);
            this.labelOutput.TabIndex = 7;
            this.labelOutput.Text = "Output Directory:";
            this.toolTip1.SetToolTip(this.labelOutput, "The directory where the reports will be placed.");
            // 
            // textBoxOutDir
            // 
            this.textBoxOutDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutDir.Location = new System.Drawing.Point(98, 165);
            this.textBoxOutDir.Name = "textBoxOutDir";
            this.textBoxOutDir.Size = new System.Drawing.Size(509, 20);
            this.textBoxOutDir.TabIndex = 9;
            // 
            // numericUpDownTrimFront
            // 
            this.numericUpDownTrimFront.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTrimFront.Location = new System.Drawing.Point(118, 19);
            this.numericUpDownTrimFront.Name = "numericUpDownTrimFront";
            this.numericUpDownTrimFront.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownTrimFront.TabIndex = 1;
            // 
            // labelTrimFront
            // 
            this.labelTrimFront.AutoSize = true;
            this.labelTrimFront.Location = new System.Drawing.Point(11, 21);
            this.labelTrimFront.Name = "labelTrimFront";
            this.labelTrimFront.Size = new System.Drawing.Size(101, 13);
            this.labelTrimFront.TabIndex = 0;
            this.labelTrimFront.Text = "Trim lines from front:";
            this.toolTip1.SetToolTip(this.labelTrimFront, "Number of lines to ignore from front of each file.\r\nCan be useful for removing he" +
        "ader information.");
            // 
            // labelTrimBack
            // 
            this.labelTrimBack.AutoSize = true;
            this.labelTrimBack.Location = new System.Drawing.Point(11, 47);
            this.labelTrimBack.Name = "labelTrimBack";
            this.labelTrimBack.Size = new System.Drawing.Size(104, 13);
            this.labelTrimBack.TabIndex = 2;
            this.labelTrimBack.Text = "Trim lines from back:";
            this.toolTip1.SetToolTip(this.labelTrimBack, "Number of lines to ignore from end of each file.\r\nCan be useful for removing publ" +
        "isher information.");
            // 
            // numericUpDownTrimBack
            // 
            this.numericUpDownTrimBack.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTrimBack.Location = new System.Drawing.Point(118, 45);
            this.numericUpDownTrimBack.Name = "numericUpDownTrimBack";
            this.numericUpDownTrimBack.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownTrimBack.TabIndex = 3;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 20000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // labelEncoding
            // 
            this.labelEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEncoding.AutoSize = true;
            this.labelEncoding.Location = new System.Drawing.Point(520, 30);
            this.labelEncoding.Name = "labelEncoding";
            this.labelEncoding.Size = new System.Drawing.Size(55, 13);
            this.labelEncoding.TabIndex = 2;
            this.labelEncoding.Text = "Encoding:";
            this.toolTip1.SetToolTip(this.labelEncoding, "The encoding of the input files.");
            // 
            // comboBoxWordFreqMethod
            // 
            this.comboBoxWordFreqMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWordFreqMethod.FormattingEnabled = true;
            this.comboBoxWordFreqMethod.Items.AddRange(new object[] {
            "MeCab",
            "JParser"});
            this.comboBoxWordFreqMethod.Location = new System.Drawing.Point(88, 19);
            this.comboBoxWordFreqMethod.Name = "comboBoxWordFreqMethod";
            this.comboBoxWordFreqMethod.Size = new System.Drawing.Size(85, 21);
            this.comboBoxWordFreqMethod.TabIndex = 1;
            this.toolTip1.SetToolTip(this.comboBoxWordFreqMethod, resources.GetString("comboBoxWordFreqMethod.ToolTip"));
            this.comboBoxWordFreqMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxWordFreqMethod_SelectedIndexChanged);
            // 
            // checkBoxKanjify
            // 
            this.checkBoxKanjify.AutoSize = true;
            this.checkBoxKanjify.Location = new System.Drawing.Point(194, 23);
            this.checkBoxKanjify.Name = "checkBoxKanjify";
            this.checkBoxKanjify.Size = new System.Drawing.Size(170, 17);
            this.checkBoxKanjify.TabIndex = 2;
            this.checkBoxKanjify.Text = "Always use kanji form of words";
            this.toolTip1.SetToolTip(this.checkBoxKanjify, resources.GetString("checkBoxKanjify.ToolTip"));
            this.checkBoxKanjify.UseVisualStyleBackColor = true;
            // 
            // labelParseMethod
            // 
            this.labelParseMethod.AutoSize = true;
            this.labelParseMethod.Location = new System.Drawing.Point(7, 22);
            this.labelParseMethod.Name = "labelParseMethod";
            this.labelParseMethod.Size = new System.Drawing.Size(75, 13);
            this.labelParseMethod.TabIndex = 0;
            this.labelParseMethod.Text = "Parse method:";
            this.toolTip1.SetToolTip(this.labelParseMethod, resources.GetString("labelParseMethod.ToolTip"));
            // 
            // checkBoxWordFreq
            // 
            this.checkBoxWordFreq.AutoSize = true;
            this.checkBoxWordFreq.Checked = true;
            this.checkBoxWordFreq.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWordFreq.Location = new System.Drawing.Point(10, 19);
            this.checkBoxWordFreq.Name = "checkBoxWordFreq";
            this.checkBoxWordFreq.Size = new System.Drawing.Size(102, 17);
            this.checkBoxWordFreq.TabIndex = 0;
            this.checkBoxWordFreq.Text = "Word frequency";
            this.toolTip1.SetToolTip(this.checkBoxWordFreq, resources.GetString("checkBoxWordFreq.ToolTip"));
            this.checkBoxWordFreq.UseVisualStyleBackColor = true;
            this.checkBoxWordFreq.CheckedChanged += new System.EventHandler(this.checkBoxWordFreq_CheckedChanged);
            // 
            // checkBoxKanjiFreq
            // 
            this.checkBoxKanjiFreq.AutoSize = true;
            this.checkBoxKanjiFreq.Location = new System.Drawing.Point(10, 42);
            this.checkBoxKanjiFreq.Name = "checkBoxKanjiFreq";
            this.checkBoxKanjiFreq.Size = new System.Drawing.Size(99, 17);
            this.checkBoxKanjiFreq.TabIndex = 1;
            this.checkBoxKanjiFreq.Text = "Kanji frequency";
            this.toolTip1.SetToolTip(this.checkBoxKanjiFreq, resources.GetString("checkBoxKanjiFreq.ToolTip"));
            this.checkBoxKanjiFreq.UseVisualStyleBackColor = true;
            // 
            // checkBoxFormulaReadability
            // 
            this.checkBoxFormulaReadability.AutoSize = true;
            this.checkBoxFormulaReadability.Location = new System.Drawing.Point(10, 65);
            this.checkBoxFormulaReadability.Name = "checkBoxFormulaReadability";
            this.checkBoxFormulaReadability.Size = new System.Drawing.Size(145, 17);
            this.checkBoxFormulaReadability.TabIndex = 2;
            this.checkBoxFormulaReadability.Text = "Formula-based readability";
            this.toolTip1.SetToolTip(this.checkBoxFormulaReadability, resources.GetString("checkBoxFormulaReadability.ToolTip"));
            this.checkBoxFormulaReadability.UseVisualStyleBackColor = true;
            // 
            // checkBoxUserReadability
            // 
            this.checkBoxUserReadability.AutoSize = true;
            this.checkBoxUserReadability.Location = new System.Drawing.Point(10, 87);
            this.checkBoxUserReadability.Name = "checkBoxUserReadability";
            this.checkBoxUserReadability.Size = new System.Drawing.Size(130, 17);
            this.checkBoxUserReadability.TabIndex = 3;
            this.checkBoxUserReadability.Text = "User-based readability";
            this.toolTip1.SetToolTip(this.checkBoxUserReadability, resources.GetString("checkBoxUserReadability.ToolTip"));
            this.checkBoxUserReadability.UseVisualStyleBackColor = true;
            this.checkBoxUserReadability.CheckedChanged += new System.EventHandler(this.checkBoxUserReadability_CheckedChanged);
            // 
            // checkBoxRemoveAozaroFormatting
            // 
            this.checkBoxRemoveAozaroFormatting.AutoSize = true;
            this.checkBoxRemoveAozaroFormatting.Checked = true;
            this.checkBoxRemoveAozaroFormatting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRemoveAozaroFormatting.Location = new System.Drawing.Point(14, 78);
            this.checkBoxRemoveAozaroFormatting.Name = "checkBoxRemoveAozaroFormatting";
            this.checkBoxRemoveAozaroFormatting.Size = new System.Drawing.Size(151, 17);
            this.checkBoxRemoveAozaroFormatting.TabIndex = 4;
            this.checkBoxRemoveAozaroFormatting.Text = "Remove Aozora formatting";
            this.toolTip1.SetToolTip(this.checkBoxRemoveAozaroFormatting, "Remove Aozora formatting and replace Aozora gaiji with utf-8 equivalents.");
            this.checkBoxRemoveAozaroFormatting.UseVisualStyleBackColor = true;
            // 
            // labelKnownWordsListCol
            // 
            this.labelKnownWordsListCol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelKnownWordsListCol.AutoSize = true;
            this.labelKnownWordsListCol.Location = new System.Drawing.Point(533, 19);
            this.labelKnownWordsListCol.Name = "labelKnownWordsListCol";
            this.labelKnownWordsListCol.Size = new System.Drawing.Size(45, 13);
            this.labelKnownWordsListCol.TabIndex = 19;
            this.labelKnownWordsListCol.Text = "Column:";
            this.toolTip1.SetToolTip(this.labelKnownWordsListCol, "If the file contains multiple tab-separated columns,\r\nselect the column that cont" +
        "ains the words.");
            // 
            // labelExtensions
            // 
            this.labelExtensions.AutoSize = true;
            this.labelExtensions.Location = new System.Drawing.Point(5, 22);
            this.labelExtensions.Name = "labelExtensions";
            this.labelExtensions.Size = new System.Drawing.Size(125, 13);
            this.labelExtensions.TabIndex = 19;
            this.labelExtensions.Text = "Limit to these extensions:";
            this.toolTip1.SetToolTip(this.labelExtensions, "Semicolon-separated list of file extensions\r\nto analyze when processing a directo" +
        "ry.\r\n\r\nLeave blank to process all files regardless of extension.\r\n");
            // 
            // checkBoxSubfolders
            // 
            this.checkBoxSubfolders.AutoSize = true;
            this.checkBoxSubfolders.Checked = true;
            this.checkBoxSubfolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSubfolders.Location = new System.Drawing.Point(307, 21);
            this.checkBoxSubfolders.Name = "checkBoxSubfolders";
            this.checkBoxSubfolders.Size = new System.Drawing.Size(76, 17);
            this.checkBoxSubfolders.TabIndex = 20;
            this.checkBoxSubfolders.Text = "Subfolders";
            this.toolTip1.SetToolTip(this.checkBoxSubfolders, "Search the provided directory and all of its sub-directories.\r\n\r\nUncheck to searc" +
        "h only the provided directory.");
            this.checkBoxSubfolders.UseVisualStyleBackColor = true;
            // 
            // labelWordRemovalListCol
            // 
            this.labelWordRemovalListCol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWordRemovalListCol.AutoSize = true;
            this.labelWordRemovalListCol.Location = new System.Drawing.Point(533, 47);
            this.labelWordRemovalListCol.Name = "labelWordRemovalListCol";
            this.labelWordRemovalListCol.Size = new System.Drawing.Size(45, 13);
            this.labelWordRemovalListCol.TabIndex = 19;
            this.labelWordRemovalListCol.Text = "Column:";
            this.toolTip1.SetToolTip(this.labelWordRemovalListCol, "If the file contains multiple tab-separated columns,\r\nselect the column that cont" +
        "ains the words.");
            // 
            // labelWordRemoval
            // 
            this.labelWordRemoval.AutoSize = true;
            this.labelWordRemoval.Location = new System.Drawing.Point(9, 47);
            this.labelWordRemoval.Name = "labelWordRemoval";
            this.labelWordRemoval.Size = new System.Drawing.Size(368, 13);
            this.labelWordRemoval.TabIndex = 0;
            this.labelWordRemoval.Text = "File that contains a list of words to remove from the report (one word per line):" +
    "";
            // 
            // labelKnownWordsList
            // 
            this.labelKnownWordsList.AutoSize = true;
            this.labelKnownWordsList.Location = new System.Drawing.Point(8, 19);
            this.labelKnownWordsList.Name = "labelKnownWordsList";
            this.labelKnownWordsList.Size = new System.Drawing.Size(354, 13);
            this.labelKnownWordsList.TabIndex = 0;
            this.labelKnownWordsList.Text = "File that contains a list of words that you already know (one word per line):";
            // 
            // comboBoxEncoding
            // 
            this.comboBoxEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncoding.FormattingEnabled = true;
            this.comboBoxEncoding.Items.AddRange(new object[] {
            "EUC-JP",
            "ISO-2022-JP",
            "Shift_JIS",
            "UTF-8",
            "UTF-16"});
            this.comboBoxEncoding.Location = new System.Drawing.Point(523, 46);
            this.comboBoxEncoding.MaxDropDownItems = 15;
            this.comboBoxEncoding.Name = "comboBoxEncoding";
            this.comboBoxEncoding.Size = new System.Drawing.Size(84, 21);
            this.comboBoxEncoding.TabIndex = 6;
            // 
            // groupBoxReport
            // 
            this.groupBoxReport.Controls.Add(this.checkBoxWordFreq);
            this.groupBoxReport.Controls.Add(this.checkBoxUserReadability);
            this.groupBoxReport.Controls.Add(this.checkBoxFormulaReadability);
            this.groupBoxReport.Controls.Add(this.checkBoxKanjiFreq);
            this.groupBoxReport.Location = new System.Drawing.Point(12, 190);
            this.groupBoxReport.Name = "groupBoxReport";
            this.groupBoxReport.Size = new System.Drawing.Size(163, 110);
            this.groupBoxReport.TabIndex = 10;
            this.groupBoxReport.TabStop = false;
            this.groupBoxReport.Text = "Reports to Generate:";
            // 
            // groupBoxPreProcessing
            // 
            this.groupBoxPreProcessing.Controls.Add(this.checkBoxRemoveAozaroFormatting);
            this.groupBoxPreProcessing.Controls.Add(this.labelTrimFront);
            this.groupBoxPreProcessing.Controls.Add(this.numericUpDownTrimBack);
            this.groupBoxPreProcessing.Controls.Add(this.labelTrimBack);
            this.groupBoxPreProcessing.Controls.Add(this.numericUpDownTrimFront);
            this.groupBoxPreProcessing.Location = new System.Drawing.Point(181, 191);
            this.groupBoxPreProcessing.Name = "groupBoxPreProcessing";
            this.groupBoxPreProcessing.Size = new System.Drawing.Size(183, 108);
            this.groupBoxPreProcessing.TabIndex = 12;
            this.groupBoxPreProcessing.TabStop = false;
            this.groupBoxPreProcessing.Text = "Pre-processing";
            // 
            // buttonInFile
            // 
            this.buttonInFile.Location = new System.Drawing.Point(12, 44);
            this.buttonInFile.Name = "buttonInFile";
            this.buttonInFile.Size = new System.Drawing.Size(42, 23);
            this.buttonInFile.TabIndex = 3;
            this.buttonInFile.Text = "&File...";
            this.buttonInFile.UseVisualStyleBackColor = true;
            this.buttonInFile.Click += new System.EventHandler(this.buttonInFile_Click);
            // 
            // groupBoxWordFreqOptions
            // 
            this.groupBoxWordFreqOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxWordFreqOptions.Controls.Add(this.numericUpDownWordRemovalListCol);
            this.groupBoxWordFreqOptions.Controls.Add(this.checkBoxKanjify);
            this.groupBoxWordFreqOptions.Controls.Add(this.labelWordRemoval);
            this.groupBoxWordFreqOptions.Controls.Add(this.labelWordRemovalListCol);
            this.groupBoxWordFreqOptions.Controls.Add(this.comboBoxWordFreqMethod);
            this.groupBoxWordFreqOptions.Controls.Add(this.buttonWordRemovalListFile);
            this.groupBoxWordFreqOptions.Controls.Add(this.labelParseMethod);
            this.groupBoxWordFreqOptions.Controls.Add(this.textBoxWordRemoval);
            this.groupBoxWordFreqOptions.Location = new System.Drawing.Point(10, 306);
            this.groupBoxWordFreqOptions.Name = "groupBoxWordFreqOptions";
            this.groupBoxWordFreqOptions.Size = new System.Drawing.Size(595, 96);
            this.groupBoxWordFreqOptions.TabIndex = 11;
            this.groupBoxWordFreqOptions.TabStop = false;
            this.groupBoxWordFreqOptions.Text = "Word Frequency Report Options:";
            // 
            // numericUpDownWordRemovalListCol
            // 
            this.numericUpDownWordRemovalListCol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownWordRemovalListCol.Location = new System.Drawing.Point(536, 66);
            this.numericUpDownWordRemovalListCol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWordRemovalListCol.Name = "numericUpDownWordRemovalListCol";
            this.numericUpDownWordRemovalListCol.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownWordRemovalListCol.TabIndex = 3;
            this.numericUpDownWordRemovalListCol.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonWordRemovalListFile
            // 
            this.buttonWordRemovalListFile.Location = new System.Drawing.Point(12, 63);
            this.buttonWordRemovalListFile.Name = "buttonWordRemovalListFile";
            this.buttonWordRemovalListFile.Size = new System.Drawing.Size(42, 23);
            this.buttonWordRemovalListFile.TabIndex = 1;
            this.buttonWordRemovalListFile.Text = "...";
            this.buttonWordRemovalListFile.UseVisualStyleBackColor = true;
            this.buttonWordRemovalListFile.Click += new System.EventHandler(this.buttonWordRemovalListFile_Click);
            // 
            // textBoxWordRemoval
            // 
            this.textBoxWordRemoval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWordRemoval.Location = new System.Drawing.Point(56, 65);
            this.textBoxWordRemoval.Name = "textBoxWordRemoval";
            this.textBoxWordRemoval.Size = new System.Drawing.Size(474, 20);
            this.textBoxWordRemoval.TabIndex = 2;
            // 
            // groupBoxUserReadabilityOptions
            // 
            this.groupBoxUserReadabilityOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxUserReadabilityOptions.Controls.Add(this.numericUpDownKnownWordsListCol);
            this.groupBoxUserReadabilityOptions.Controls.Add(this.labelKnownWordsListCol);
            this.groupBoxUserReadabilityOptions.Controls.Add(this.textBoxKnownWordsList);
            this.groupBoxUserReadabilityOptions.Controls.Add(this.buttonKnownWordsListFile);
            this.groupBoxUserReadabilityOptions.Controls.Add(this.labelKnownWordsList);
            this.groupBoxUserReadabilityOptions.Enabled = false;
            this.groupBoxUserReadabilityOptions.Location = new System.Drawing.Point(10, 408);
            this.groupBoxUserReadabilityOptions.Name = "groupBoxUserReadabilityOptions";
            this.groupBoxUserReadabilityOptions.Size = new System.Drawing.Size(595, 67);
            this.groupBoxUserReadabilityOptions.TabIndex = 13;
            this.groupBoxUserReadabilityOptions.TabStop = false;
            this.groupBoxUserReadabilityOptions.Text = "User-based Readability Report Options:";
            // 
            // numericUpDownKnownWordsListCol
            // 
            this.numericUpDownKnownWordsListCol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownKnownWordsListCol.Location = new System.Drawing.Point(536, 38);
            this.numericUpDownKnownWordsListCol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownKnownWordsListCol.Name = "numericUpDownKnownWordsListCol";
            this.numericUpDownKnownWordsListCol.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownKnownWordsListCol.TabIndex = 3;
            this.numericUpDownKnownWordsListCol.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBoxKnownWordsList
            // 
            this.textBoxKnownWordsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKnownWordsList.Location = new System.Drawing.Point(55, 37);
            this.textBoxKnownWordsList.Name = "textBoxKnownWordsList";
            this.textBoxKnownWordsList.Size = new System.Drawing.Size(475, 20);
            this.textBoxKnownWordsList.TabIndex = 2;
            // 
            // buttonKnownWordsListFile
            // 
            this.buttonKnownWordsListFile.Location = new System.Drawing.Point(10, 35);
            this.buttonKnownWordsListFile.Name = "buttonKnownWordsListFile";
            this.buttonKnownWordsListFile.Size = new System.Drawing.Size(42, 23);
            this.buttonKnownWordsListFile.TabIndex = 1;
            this.buttonKnownWordsListFile.Text = "...";
            this.buttonKnownWordsListFile.UseVisualStyleBackColor = true;
            this.buttonKnownWordsListFile.Click += new System.EventHandler(this.buttonKnownWordsListFile_Click);
            // 
            // backgroundWorkerMain
            // 
            this.backgroundWorkerMain.WorkerReportsProgress = true;
            this.backgroundWorkerMain.WorkerSupportsCancellation = true;
            this.backgroundWorkerMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMain_DoWork);
            this.backgroundWorkerMain.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerMain_ProgressChanged);
            this.backgroundWorkerMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMain_RunWorkerCompleted);
            // 
            // textBoxExtensions
            // 
            this.textBoxExtensions.Location = new System.Drawing.Point(136, 19);
            this.textBoxExtensions.Name = "textBoxExtensions";
            this.textBoxExtensions.Size = new System.Drawing.Size(165, 20);
            this.textBoxExtensions.TabIndex = 18;
            this.textBoxExtensions.Text = "txt;srt;ass;htm;html";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxFolderSeparator);
            this.groupBox1.Controls.Add(this.labelFolderSeparator);
            this.groupBox1.Controls.Add(this.checkBoxSeparateReports);
            this.groupBox1.Controls.Add(this.checkBoxSubfolders);
            this.groupBox1.Controls.Add(this.labelExtensions);
            this.groupBox1.Controls.Add(this.textBoxExtensions);
            this.groupBox1.Location = new System.Drawing.Point(12, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 72);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Directory Search Options:";
            // 
            // textBoxFolderSeparator
            // 
            this.textBoxFolderSeparator.Location = new System.Drawing.Point(136, 45);
            this.textBoxFolderSeparator.Name = "textBoxFolderSeparator";
            this.textBoxFolderSeparator.Size = new System.Drawing.Size(165, 20);
            this.textBoxFolderSeparator.TabIndex = 23;
            this.textBoxFolderSeparator.Text = " - ";
            // 
            // labelFolderSeparator
            // 
            this.labelFolderSeparator.AutoSize = true;
            this.labelFolderSeparator.Location = new System.Drawing.Point(5, 45);
            this.labelFolderSeparator.Name = "labelFolderSeparator";
            this.labelFolderSeparator.Size = new System.Drawing.Size(86, 13);
            this.labelFolderSeparator.TabIndex = 22;
            this.labelFolderSeparator.Text = "Folder separator:";
            this.toolTip1.SetToolTip(this.labelFolderSeparator, "Separator to be used between subfolder and file\r\nnames when generating separate r" +
        "eports for each\r\nfile.");
            // 
            // checkBoxSeparateReports
            // 
            this.checkBoxSeparateReports.AutoSize = true;
            this.checkBoxSeparateReports.Location = new System.Drawing.Point(307, 47);
            this.checkBoxSeparateReports.Name = "checkBoxSeparateReports";
            this.checkBoxSeparateReports.Size = new System.Drawing.Size(104, 17);
            this.checkBoxSeparateReports.TabIndex = 21;
            this.checkBoxSeparateReports.Text = "Separate reports";
            this.toolTip1.SetToolTip(this.checkBoxSeparateReports, resources.GetString("checkBoxSeparateReports.ToolTip"));
            this.checkBoxSeparateReports.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 554);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxUserReadabilityOptions);
            this.Controls.Add(this.groupBoxWordFreqOptions);
            this.Controls.Add(this.groupBoxPreProcessing);
            this.Controls.Add(this.groupBoxReport);
            this.Controls.Add(this.comboBoxEncoding);
            this.Controls.Add(this.labelEncoding);
            this.Controls.Add(this.buttonOutputFile);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.textBoxOutDir);
            this.Controls.Add(this.labelTotalPercentage);
            this.Controls.Add(this.buttonInFile);
            this.Controls.Add(this.buttonInDir);
            this.Controls.Add(this.progressBarTotal);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.textBoxInFile);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMain";
            this.Text = "cb\'s Japanese Text Analysis Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrimFront)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrimBack)).EndInit();
            this.groupBoxReport.ResumeLayout(false);
            this.groupBoxReport.PerformLayout();
            this.groupBoxPreProcessing.ResumeLayout(false);
            this.groupBoxPreProcessing.PerformLayout();
            this.groupBoxWordFreqOptions.ResumeLayout(false);
            this.groupBoxWordFreqOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWordRemovalListCol)).EndInit();
            this.groupBoxUserReadabilityOptions.ResumeLayout(false);
            this.groupBoxUserReadabilityOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKnownWordsListCol)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStripMain;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItemExit;
    private System.Windows.Forms.Label labelInput;
    private System.Windows.Forms.TextBox textBoxInFile;
    private System.Windows.Forms.Button buttonStart;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ProgressBar progressBarTotal;
    private System.Windows.Forms.Button buttonInDir;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    private System.Windows.Forms.Label labelTotalPercentage;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelOperation;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentFile;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.Button buttonOutputFile;
    private System.Windows.Forms.Label labelOutput;
    private System.Windows.Forms.TextBox textBoxOutDir;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.NumericUpDown numericUpDownTrimFront;
    private System.Windows.Forms.Label labelTrimFront;
    private System.Windows.Forms.Label labelTrimBack;
    private System.Windows.Forms.NumericUpDown numericUpDownTrimBack;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.ComboBox comboBoxEncoding;
    private System.Windows.Forms.Label labelEncoding;
    private System.Windows.Forms.CheckBox checkBoxWordFreq;
    private System.Windows.Forms.CheckBox checkBoxKanjiFreq;
    private System.Windows.Forms.CheckBox checkBoxFormulaReadability;
    private System.Windows.Forms.GroupBox groupBoxReport;
    private System.Windows.Forms.GroupBox groupBoxPreProcessing;
    private System.Windows.Forms.Button buttonInFile;
    private System.Windows.Forms.ComboBox comboBoxWordFreqMethod;
    private System.Windows.Forms.GroupBox groupBoxWordFreqOptions;
    private System.Windows.Forms.CheckBox checkBoxKanjify;
    private System.Windows.Forms.Label labelParseMethod;
    private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem diffToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem combineFrequencyReportsToolStripMenuItem;
    private System.Windows.Forms.CheckBox checkBoxUserReadability;
    private System.Windows.Forms.GroupBox groupBoxUserReadabilityOptions;
    private System.Windows.Forms.TextBox textBoxKnownWordsList;
    private System.Windows.Forms.Button buttonKnownWordsListFile;
    private System.Windows.Forms.Label labelKnownWordsList;
    private System.ComponentModel.BackgroundWorker backgroundWorkerMain;
    private System.Windows.Forms.TextBox textBoxExtensions;
    private System.Windows.Forms.Label labelExtensions;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox checkBoxSubfolders;
    private System.Windows.Forms.NumericUpDown numericUpDownKnownWordsListCol;
    private System.Windows.Forms.Label labelKnownWordsListCol;
    private System.Windows.Forms.CheckBox checkBoxRemoveAozaroFormatting;
    private System.Windows.Forms.NumericUpDown numericUpDownWordRemovalListCol;
    private System.Windows.Forms.Label labelWordRemoval;
    private System.Windows.Forms.Label labelWordRemovalListCol;
    private System.Windows.Forms.Button buttonWordRemovalListFile;
    private System.Windows.Forms.TextBox textBoxWordRemoval;
    private System.Windows.Forms.CheckBox checkBoxSeparateReports;
    private System.Windows.Forms.TextBox textBoxFolderSeparator;
    private System.Windows.Forms.Label labelFolderSeparator;
  }
}

