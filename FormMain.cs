//  Copyright (C) 2012-2014 Christopher Brochtrup
//
//  This file is part of cb's Japanese Text Analysis Tool.
//
//  cb's Japanese Text Analysis Tool is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  cb's Japanese Text Analysis Tool is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with cb's Japanese Text Analysis Tool.  If not, see <http://www.gnu.org/licenses/>.
//
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;


namespace JapaneseTextAnalysisTool
{

  public partial class FormMain : Form
  {
    private string inFile = "";
    private bool searchSubFolders = true;
    private bool genSeparateReports = false;
    private string genFolderSeparator = " - ";
    private Encoding inFileEncoding;
    private List<string> extensions = new List<string>();
    private string outDir = "";
    private int chopLinesFront = 0;
    private int chopLinesBack = 0;
    private bool removeAozoraFormatting = true;
    private string knownWordsFile = "";
    private string wordRemovalFile = "";
    private int knownWordsFileColumn = 1;
    private int wordRemovalFileColumn = 1;
    private string lastDirPath = "";
    private bool isInputFile = false;
    private bool genWordFreqReport = true;
    private bool genKanjiFreqReport = false;
    private bool genFormulaReadabilityReport = false;
    private bool genUserReadabilityReport = true;

    private FreqWord freqWord = new FreqWord("word_freq_report.txt");
    private FreqKanji freqKanji = new FreqKanji("kanji_freq_report.txt");
    private FormulaReadability formulaReadability = new FormulaReadability("formula_based_readability_report.txt");
    private UserReadability userReadability = new UserReadability("user_based_readability_report.txt");
    private Object wordFreqLock = new Object();
    private Object kanjiFreqLock = new Object();
    private Object formulaReadabilityLock = new Object();
    private Object userReadabilityLock = new Object();

    // Words that user already knows
    List<string> knownWordsList = new List<string>();

    // Words that the user would like to remove from the Word Frequency and User-based Readability reports.
    List<string> wordRemovalList = new List<string>();


    /// <summary>
    /// Constructor.
    /// </summary>
    public FormMain()
    {
      InitializeComponent();

      this.readSettingsFile();

      this.ActiveControl = this.textBoxInFile;
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if(this.backgroundWorkerMain.IsBusy)
      {
        this.backgroundWorkerMain.CancelAsync();
      }
    }

    private void exitToolStripMenuItemExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      UtilsMsg.showInfoMsg(String.Format("Version: {0}\nAuthor: {1}\nContact: {2}\n",
        UtilsAssembly.Version, UtilsAssembly.Author, "cb4960@gmail.com"));
    }


    private void diffToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FormCompare diffDialog = new FormCompare();
      diffDialog.Show();
    }


    private void combineFrequencyReportsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FormCombine combineDialog = new FormCombine();
      combineDialog.Show();
    }


    private void buttonInFile_Click(object sender, EventArgs e)
    {
      this.textBoxInFile.Text = showFileDialog(this.textBoxInFile.Text.Trim(),
        "HTML Files (*.htm;*.html)|*.htm;*.html|Subtitle Files (*.ass;*.srt)|*.ass;*.srt|Text Files (*.txt)|*.txt|All Files (*.*)|*.*", 3);
    }


    private void buttonInDir_Click(object sender, EventArgs e)
    {
      this.textBoxInFile.Text = showFolderDialog(this.textBoxInFile.Text.Trim());
    }


    private void buttonOutputFile_Click(object sender, EventArgs e)
    {
      this.textBoxOutDir.Text = showFolderDialog(this.textBoxOutDir.Text.Trim());
    }


    private void checkBoxWordFreq_CheckedChanged(object sender, EventArgs e)
    {
      this.groupBoxWordFreqOptions.Enabled = this.checkBoxWordFreq.Checked || this.checkBoxUserReadability.Checked;
    }


    private void checkBoxUserReadability_CheckedChanged(object sender, EventArgs e)
    {
      this.groupBoxWordFreqOptions.Enabled = this.checkBoxWordFreq.Checked || this.checkBoxUserReadability.Checked;
      this.groupBoxUserReadabilityOptions.Enabled = this.checkBoxUserReadability.Checked;
    }


    private void comboBoxWordFreqMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
      checkBoxKanjify.Enabled =
        ((FreqWord.WordFreqMethod)comboBoxWordFreqMethod.SelectedIndex == FreqWord.WordFreqMethod.JPARSER);
    }


    private void buttonKnownWordsListFile_Click(object sender, EventArgs e)
    {
      this.textBoxKnownWordsList.Text = showFileDialog(this.textBoxInFile.Text.Trim(),
        "Text Files (*.txt;*.tsv)|*.txt;*.tsv|All Files (*.*)|*.*", 1);
    }

    private void buttonWordRemovalListFile_Click(object sender, EventArgs e)
    {
      this.textBoxWordRemoval.Text = showFileDialog(this.textBoxInFile.Text.Trim(),
        "Text Files (*.txt;*.tsv)|*.txt;*.tsv|All Files (*.*)|*.*", 1);
    }


    private void buttonStart_Click(object sender, EventArgs e)
    {
      this.gatherGUI();

      if (!Directory.Exists(this.inFile) && !File.Exists(this.inFile))
      {
        UtilsMsg.showErrMsg("Please enter a valid input file or directory.");
        return;
      }

      if (!Directory.Exists(this.outDir))
      {
        UtilsMsg.showErrMsg("Please enter a valid output directory.");
        return;
      }

      if (!genWordFreqReport && !genKanjiFreqReport && !genFormulaReadabilityReport && !genUserReadabilityReport)
      {
        UtilsMsg.showErrMsg("Please check at least one of the report options.");
        return;
      }

      if (this.genUserReadabilityReport && !File.Exists(this.knownWordsFile))
      {
        UtilsMsg.showErrMsg("To generate a User-based Readability Report, you must enter\r\n"
                          + "a file containing the list of words that you already know.");
        return;
      }

      this.knownWordsList.Clear();

      if (this.genUserReadabilityReport 
        && !this.readWordList(this.knownWordsFile, this.knownWordsFileColumn, this.knownWordsList))
      {
        UtilsMsg.showErrMsg(@"Unable to read the ""Known Words"" file.");
        return;
      }


      this.wordRemovalList.Clear();

      if ((this.genWordFreqReport)
        && !this.readWordList(this.wordRemovalFile, this.wordRemovalFileColumn, this.wordRemovalList))
      {
        UtilsMsg.showErrMsg(@"Unable to read the ""Word Removal"" file.");
        return;
      }


      if (this.buttonStart.Text == "S&TOP")
      {
        this.backgroundWorkerMain.CancelAsync();
      }
      else
      {
        this.freqKanji.reset();
        this.freqWord.reset();
        this.formulaReadability.reset();
        this.userReadability.reset();

        this.buttonStart.Text = "S&TOP";
        this.toolStripStatusLabelOperation.Text = "In Progress...";
        this.labelTotalPercentage.Visible = !File.Exists(this.inFile);
        this.progressBarTotal.Visible = !File.Exists(this.inFile);
        this.backgroundWorkerMain.RunWorkerAsync();
      }
    }


    /// <summary>
    /// Open folder dialog (starting at the current file) and get the selected file
    /// </summary>
    private string showFileDialog(string currentFilePattern, string filter, int filterIndex)
    {
      string selectedFile = currentFilePattern;
      string curDir = "";

      this.openFileDialog.FileName = "";
      this.openFileDialog.Filter = filter;
      this.openFileDialog.FilterIndex = filterIndex;

      try
      {
        curDir = Path.GetDirectoryName(currentFilePattern);
      }
      catch
      {
        curDir = "";
      }

      if (Directory.Exists(curDir))
      {
        this.openFileDialog.InitialDirectory = curDir;
      }
      else if (lastDirPath != "")
      {
        this.openFileDialog.InitialDirectory = lastDirPath;
      }

      if (this.openFileDialog.ShowDialog() == DialogResult.OK)
      {
        selectedFile = this.openFileDialog.FileName;
        lastDirPath = Path.GetDirectoryName(selectedFile);
      }

      return selectedFile;
    }


    /// <summary>
    /// Open folder dialog (starting at the last folder) and get the selected folder
    /// </summary>
    private string showFolderDialog(string currentPath)
    {
      string ret = currentPath;

      string folderPath = "";

      try
      {
        folderPath = Path.GetDirectoryName(currentPath);
      }
      catch
      {
        folderPath = "";
      }

      if (Directory.Exists(currentPath))
      {
        this.folderBrowserDialog.SelectedPath = currentPath;
      }
      else if ((folderPath != "") && Directory.Exists(folderPath))
      {
        this.folderBrowserDialog.SelectedPath = Path.GetDirectoryName(currentPath);
      }
      else if (lastDirPath != "")
      {
        this.folderBrowserDialog.SelectedPath = lastDirPath;
      }

      if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        ret = this.folderBrowserDialog.SelectedPath;
        lastDirPath = this.folderBrowserDialog.SelectedPath;
      }

      return ret;
    }


    /// <summary>
    /// Read the default settings.
    /// </summary>
    private void readSettingsFile()
    {
      Match match;

      if (File.Exists("settings.txt"))
      {
        StreamReader settingsFile = new StreamReader("settings.txt", Encoding.UTF8);
        string line = "";

        while ((line = settingsFile.ReadLine()) != null)
        {
          match = Regex.Match(line, @"^(?<Setting>.*?)=(?<Value>.*)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

          if (!match.Success)
          {
            continue;
          }

          string setting = match.Groups["Setting"].ToString().Trim().ToLower();
          string value = match.Groups["Value"].ToString().Trim();

          if (setting == "input")
          {
            this.textBoxInFile.Text = value;
          }
          else if (setting == "encoding")
          {
            if ((value.ToUpper() == "SHIFT_JIS")
               || (value.ToUpper() == "UTF-8")
               || (value.ToUpper() == "UTF-16")
               || (value.ToUpper() == "UTF-32")
               || (value.ToUpper() == "EUC-JP")
               || (value.ToUpper() == "ISO-2022-JP"))
            {
              this.comboBoxEncoding.Text = value;
            }
            else
            {
              this.comboBoxEncoding.Text = "SHIFT_JIS";
            }
          }
          else if (setting == "extensions")
          {
            this.textBoxExtensions.Text = value;
          }
          else if (setting == "sub_folders")
          {
            this.checkBoxSubfolders.Checked = (value.ToUpper() == "TRUE");
          }
          else if (setting == "separate_reports")
          {
              this.checkBoxSeparateReports.Checked = (value.ToUpper() == "TRUE");
          }
          else if (setting == "folder_separator")
          {
              this.textBoxFolderSeparator.Text = value;
          }
          else if (setting == "out_dir")
          {
            this.textBoxOutDir.Text = value;
          }
          else if (setting == "report_word_freq")
          {
            this.checkBoxWordFreq.Checked = (value.ToUpper() == "TRUE");
          }
          else if (setting == "report_kanji_freq")
          {
            this.checkBoxKanjiFreq.Checked = (value.ToUpper() == "TRUE");
          }
          else if (setting == "report_formula_based_readability")
          {
            this.checkBoxFormulaReadability.Checked = (value.ToUpper() == "TRUE");
          }
          else if (setting == "report_user_based_readability")
          {
            this.checkBoxUserReadability.Checked = (value.ToUpper() == "TRUE");
          }
          else if (setting == "word_freq_method")
          {
            string method = value.ToUpper();

            if (method == "JPARSER")
            {
              this.comboBoxWordFreqMethod.SelectedIndex = (int)FreqWord.WordFreqMethod.JPARSER;
            }
            else
            {
              this.comboBoxWordFreqMethod.SelectedIndex = (int)FreqWord.WordFreqMethod.MECAB;
            }
          }
          else if (setting == "kanjify")
          {
            this.checkBoxKanjify.Checked = (value.ToUpper() == "TRUE");
          }
          else if (setting == "trim_front")
          {
            this.numericUpDownTrimFront.Value = Convert.ToDecimal(value);
          }
          else if (setting == "trim_back")
          {
            this.numericUpDownTrimBack.Value = Convert.ToDecimal(value);
          }
          else if (setting == "remove_aozora_formatting")
          {
            this.checkBoxRemoveAozaroFormatting.Checked = (value.ToUpper() == "TRUE");
          }
          else if (setting == "known_words_list")
          {
            this.textBoxKnownWordsList.Text = value.Trim();
          }
          else if (setting == "known_words_list_column")
          {
            this.numericUpDownKnownWordsListCol.Value = Convert.ToDecimal(value);
          }
          else if (setting == "word_removal_list")
          {
            this.textBoxWordRemoval.Text = value.Trim();
          }
          else if (setting == "word_removal_list_column")
          {
            this.numericUpDownWordRemovalListCol.Value = Convert.ToDecimal(value);
          }
        }

        settingsFile.Close();
      }

      if (this.textBoxOutDir.Text.Trim() == "")
      {
        this.textBoxOutDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      }
    }


    /// <summary>
    /// Gather information form the GUI.
    /// </summary>
    private void gatherGUI()
    {
      this.inFile = this.textBoxInFile.Text.Trim().TrimEnd(new char[] { '/', '\\' });
      this.isInputFile = !Directory.Exists(this.inFile);

      switch (this.comboBoxEncoding.Text.Trim())
      {
        case "EUC-JP": this.inFileEncoding = Encoding.GetEncoding(20932); break;
        case "ISO-2022-JP": this.inFileEncoding = Encoding.GetEncoding(50220); break;
        case "Shift_JIS": this.inFileEncoding = Encoding.GetEncoding(932); break;
        case "UTF-8": this.inFileEncoding = Encoding.GetEncoding(65001); break;
        case "UTF-16": this.inFileEncoding = Encoding.GetEncoding(1200); break;
        case "UTF-32": this.inFileEncoding = Encoding.GetEncoding(12000); break;
        default: this.inFileEncoding = Encoding.GetEncoding(0); break;
      }

      this.extensions.Clear();
      this.extensions.AddRange(this.textBoxExtensions.Text.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

      for (int i = 0; i < this.extensions.Count; i++)
      {
        this.extensions[i] = this.extensions[i].Trim();
      }

      this.searchSubFolders = this.checkBoxSubfolders.Checked;
      this.genSeparateReports = this.checkBoxSeparateReports.Checked;
      this.genFolderSeparator = this.textBoxFolderSeparator.Text;
      this.outDir = this.textBoxOutDir.Text.Trim();
      
      this.genWordFreqReport = this.checkBoxWordFreq.Checked;
      this.genUserReadabilityReport = this.checkBoxUserReadability.Checked;
      this.genKanjiFreqReport = this.checkBoxKanjiFreq.Checked;
      this.genFormulaReadabilityReport = this.checkBoxFormulaReadability.Checked;

      this.freqWord.ParseMethod = (FreqWord.WordFreqMethod)this.comboBoxWordFreqMethod.SelectedIndex;
      this.freqWord.Kanjify = this.checkBoxKanjify.Checked;

      this.chopLinesFront = (int)this.numericUpDownTrimFront.Value;
      this.chopLinesBack = (int)this.numericUpDownTrimBack.Value;
      this.removeAozoraFormatting = this.checkBoxRemoveAozaroFormatting.Checked;
     
      this.knownWordsFile = this.textBoxKnownWordsList.Text.Trim();
      this.knownWordsFileColumn = (int)this.numericUpDownKnownWordsListCol.Value;

      this.wordRemovalFile = this.textBoxWordRemoval.Text.Trim();
      this.wordRemovalFileColumn = (int)this.numericUpDownWordRemovalListCol.Value;
    }


    /// <summary>
    /// Read in the file that contains words in the provided column, and populates the provided list.
    /// </summary>
    private bool readWordList(string theFile, int column, List<string> theList)
    {
      // Kanji = Expression.
      // Value = unused.
      Dictionary<string, bool> theTable = new Dictionary<string, bool>();

      // If the user didn't specify a file, return OK
      if (theFile == "")
      {
        return true;
      }
      // Else if the file doesn't exist
      else if (!File.Exists(theFile))
      {
        return false;
      }

      try
      {
        StreamReader reader = new StreamReader(theFile, Encoding.UTF8);
        string line = "";

        while ((line = reader.ReadLine()) != null)
        {
          string[] fields = line.Split(new char[] { '\t' });

          if (fields.Length >= column)
          {
            string word = fields[column - 1].Trim();

            // Disallow blanks and duplicates
            if ((word != "") && !theTable.ContainsKey(word))
            {
              theTable.Add(word, true);
            }
          }
          else
          {
            throw new Exception("");
          }
        }

        reader.Close();
      }
      catch
      {
        return false;
      }

      foreach (string word in theTable.Keys)
      {
        theList.Add(word);
      }

      return true;
    }


    /// <summary>
    /// Generate the reports.
    /// </summary>
    public void generateReports(FileInfo fi)
    {
      string filename = "dummy";
      if (!this.isInputFile)
      {
        filename = Path.GetFileNameWithoutExtension(fi.FullName);
        if (fi.DirectoryName.Length > this.inFile.Length)
        {
            string subfolders = fi.DirectoryName.Substring(this.inFile.Length + 1).Replace(@"\", this.genFolderSeparator);
            filename = subfolders + this.genFolderSeparator + filename;
        }
      }

      FreqWord localWordFreq = new FreqWord(filename + ".word_freq.txt");
      localWordFreq.ParseMethod = this.freqWord.ParseMethod;
      localWordFreq.Kanjify = this.freqWord.Kanjify;

      // Read in the entire file
      StreamReader reader = new StreamReader(fi.FullName, this.inFileEncoding);
      string fileContent = reader.ReadToEnd();
      reader.Close();

      // Chops lines if needed
      if ((this.chopLinesFront > 0) || (this.chopLinesBack > 0))
      {
        string lineEnding = UtilsCommon.getLineEnding(fileContent);
        string[] fileLines = fileContent.Split(new string[] { lineEnding }, StringSplitOptions.None);

        // Chop off the first X and last Y lines to remove header info and such.
        StringBuilder builder = new StringBuilder();

        for (int i = this.chopLinesFront; i < fileLines.Length - this.chopLinesBack; i++)
        {
          builder.AppendLine(fileLines[i].Trim());
        }

        fileContent = builder.ToString();
      }

      if (this.removeAozoraFormatting)
      {
        // Remove some of the aozora formatting
        Aozora aozora = new Aozora();
        fileContent = aozora.removeRuby(fileContent);
        fileContent = aozora.removeComment(fileContent);
        fileContent = aozora.removeUnsupported(fileContent);
        fileContent = aozora.removeEmphasis(fileContent);
        fileContent = aozora.removeUnderline(fileContent);
        fileContent = aozora.removeImage(fileContent);

        // Replace aozora gaiji with UTF-8
        fileContent = aozora.addGaiji(fileContent);
      }

      if (this.genWordFreqReport)
      {
        localWordFreq.addFileText(fileContent);
        if (this.genSeparateReports)
        {
            localWordFreq.generateReport(this.outDir, true, false);
        }
        lock (wordFreqLock)
        {
          this.freqWord.addExistingFreq(localWordFreq);
        }
      }

      if (this.genKanjiFreqReport)
      {
        FreqKanji localKanjiFreq = new FreqKanji(filename + ".kanji_freq.txt");
        localKanjiFreq.addFileText(fileContent);
        if (this.genSeparateReports)
        {
            localKanjiFreq.generateReport(this.outDir, false, false);
        }
        lock (kanjiFreqLock)
        {
          this.freqKanji.addExistingFreq(localKanjiFreq);
        }
      }

      if (this.genFormulaReadabilityReport)
      {
        FormulaReadability localForumulaReadability = new FormulaReadability("dummy_formula_readability.txt");
        localForumulaReadability.addFileText(fi.Name, fileContent);

        lock (formulaReadabilityLock)
        {
          this.formulaReadability.addExisting(localForumulaReadability);
        }
      }

      if (this.genUserReadabilityReport)
      {
        UserReadability localUserReadability = new UserReadability("dummy_user_readability.txt");

        // If text was already parsed for the word frequency report, just use that
        if (this.genWordFreqReport)
        {
          List<string> wordList = new List<string>();

          // Copy the wordList (due to pass by reference issues)
          foreach (string word in localWordFreq.LastWordList)
          {
            wordList.Add(word);
          }

          localUserReadability.addFileWordList(fi.Name, wordList, this.knownWordsList);
        }
        else // Otherwise parse the text
        {
          localUserReadability.addFileText(fi.Name, fileContent, this.knownWordsList,
            this.freqWord.ParseMethod, this.freqWord.Kanjify);
        }
        this.userReadability.addExisting(localUserReadability);
      }
    }


    /// <summary>
    /// Get the list of files to analyze.
    /// </summary>
    private List<FileInfo> getFilesToAnalyze()
    {
      List<FileInfo> fileList = new List<FileInfo>();

      // If the user specified a single file
      if (this.isInputFile)
      {
        fileList.Add(new FileInfo(this.inFile));
      }
      else // Directory of files
      {
        DirectoryInfo root = new DirectoryInfo(this.inFile);
        List<FileInfo> tempFileList = new List<FileInfo>();
        SearchOption searchOption = SearchOption.TopDirectoryOnly;

        if (this.searchSubFolders)
        {
          searchOption = SearchOption.AllDirectories;
        }

        // Get list of files
        tempFileList.AddRange(root.GetFiles("*", searchOption));

        // Check file extension against the user-specified list of extensions
        if (extensions.Count > 0)
        {
          foreach (FileInfo fi in tempFileList)
          {
            foreach (string ext in this.extensions)
            {
              if (ext.ToLower() == fi.Extension.Substring(1).ToLower())
              {
                fileList.Add(fi);
                break;
              }
            }
          }
        }
        else
        {
          fileList = tempFileList;
        }
      }

      return fileList;
    }


    /// <summary>
    /// Analyze each file in its own thread.
    /// </summary>
    private void backgroundWorkerMain_DoWork(object sender, DoWorkEventArgs e)
    {
      BackgroundWorker worker = sender as BackgroundWorker;

      List<FileInfo> fileList = this.getFilesToAnalyze();

      if (fileList.Count == 0)
      {
        e.Result = "No files";
        return;
      }

      // When count reaches zero, all files have been analyzed
      long counter = fileList.Count;

      // Used to block until the last thread has been completed
      ManualResetEvent resetEvent = new ManualResetEvent(false);
      bool cancelled = false;

      ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);

      // Operate on each file
      foreach (FileInfo fi in fileList)
      {
        if (cancelled)
        {
          break;
        }

        if (worker.CancellationPending)
        {
          e.Cancel = true;
          cancelled = true;
          resetEvent.Set();
          break;
        }     

        // Analyze file in separate thread
        ThreadPool.QueueUserWorkItem(new WaitCallback((object data) =>
        {
          FileInfo curFileInfo = (FileInfo)((object[])data)[0];
          InfoProgress progress = new InfoProgress(fileList.Count, curFileInfo.Name);

          if (cancelled)
          {
            return;
          }

          // Analyze the file
          this.generateReports(curFileInfo);

          if (worker.CancellationPending)
          {
            e.Cancel = true;
            cancelled = true;
            resetEvent.Set();
            return;
          }

          // Decrements counter atomically
          Interlocked.Decrement(ref counter);

          if (cancelled)
          {
            return;
          }

          worker.ReportProgress(0, progress);

          if (Interlocked.Read(ref counter) == 0)
          {
            resetEvent.Set();
          }
        }), new object[] { fi });
      }

      resetEvent.WaitOne();
    }


    private void backgroundWorkerMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      InfoProgress progress = (InfoProgress)e.UserState;

      this.progressBarTotal.Maximum = progress.TotalFileCount;
      this.progressBarTotal.Increment(1);

      this.labelTotalPercentage.Text = String.Format("{0:##.#%}", (this.progressBarTotal.Value / (float)progress.TotalFileCount));

      this.toolStripStatusLabelCurrentFile.Text = String.Format("{0}/{1}  {2}",
        this.progressBarTotal.Value, progress.TotalFileCount, progress.CurFile);
    }


    private void backgroundWorkerMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Cancelled)
      {
        // Do nothing
      }
      else if (e.Error != null)
      {
      
      }
      else // All files analyzed
      {
        if ((string)e.Result == "No files")
        {
          UtilsMsg.showInfoMsg("No files were found to analyze.");
        }
        else
        {
          FormComplete dlgComplete = new FormComplete("", "");

          if (this.genWordFreqReport)
          {
            foreach (string word in this.wordRemovalList)
            {
              if (this.freqWord.Table.ContainsKey(word))
              {
                this.freqWord.Table.Remove(word);
              }
            }

            this.freqWord.generateReport(this.outDir, true);
          }

          if (this.genKanjiFreqReport)
          {
            this.freqKanji.generateReport(this.outDir, false);
          }

          if (this.genFormulaReadabilityReport)
          {
            this.formulaReadability.generateReport(this.outDir);
          }

          if (this.genUserReadabilityReport)
          {
            this.userReadability.generateReport(this.outDir);
          }

          dlgComplete = new FormComplete("All reports generated successfully!", this.outDir);
          dlgComplete.ShowDialog();
        }
      }

      this.toolStripStatusLabelCurrentFile.Text = "";
      this.toolStripStatusLabelOperation.Text = "Ready";
      this.labelTotalPercentage.Text = "0.0%";
      this.progressBarTotal.Value = 0;
      this.buttonStart.Text = "&Analyze!";
      this.labelTotalPercentage.Visible = false;
      this.progressBarTotal.Visible = false;
    }


  }


  public class InfoProgress
  {
    public int TotalFileCount { get; set; }
    public string CurFile { get; set; }

    public InfoProgress(int totalFileCount, string curFile)
    {
      TotalFileCount = totalFileCount;
      CurFile = curFile;
    }
  }




}


