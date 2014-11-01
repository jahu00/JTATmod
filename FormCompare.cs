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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JapaneseTextAnalysisTool
{
  public partial class FormCompare : Form
  {
    // Freq, #_of_hits
    private Dictionary<string, uint> freqTableA = new Dictionary<string, uint>();
    private Dictionary<string, uint> freqTableB = new Dictionary<string, uint>();

    private string lastDirPath = "";

    public FormCompare()
    {
      InitializeComponent();
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


    private void buttonFileA_Click(object sender, EventArgs e)
    {
      this.textBoxFileA.Text = showFileDialog(this.textBoxFileA.Text.Trim(),
        "Text Files (*.txt)|*.txt|All Files (*.*)|*.*", 1);
    }


    private void buttonFileB_Click(object sender, EventArgs e)
    {
      this.textBoxFileB.Text = showFileDialog(this.textBoxFileB.Text.Trim(),
        "Text Files (*.txt)|*.txt|All Files (*.*)|*.*", 1);
    }


    private void buttonOutDir_Click(object sender, EventArgs e)
    {
      this.textBoxOutDir.Text = showFolderDialog(this.textBoxOutDir.Text.Trim());
    }


    private void buttonCompare_Click(object sender, EventArgs e)
    {
      compareFrequencyReports();
    }


    /// <summary>
    /// Compare the two frequency reports.
    /// </summary>
    private void compareFrequencyReports()
    {
      string fileA = textBoxFileA.Text.Trim();
      string fileB = textBoxFileB.Text.Trim();
      string outDir = textBoxOutDir.Text.Trim();

      if (!File.Exists(fileA))
      {
        UtilsMsg.showErrMsg("Please enter a valid Report A.");
        return;
      }

      if (!File.Exists(fileB))
      {
        UtilsMsg.showErrMsg("Please enter a valid Report B.");
        return;
      }

      if (!Directory.Exists(outDir))
      {
        UtilsMsg.showErrMsg("Please enter a valid output directory.");
        return;
      }

      parseFrequencyReport(freqTableA, fileA);
      parseFrequencyReport(freqTableB, fileB);

      List<InfoFreqCompare> onlyA = generateIntersectionReport(
        freqTableA, freqTableB, Path.Combine(outDir, "ReportA_Only.txt"));
      List<InfoFreqCompare> onlyB = generateIntersectionReport(
        freqTableB, freqTableA, Path.Combine(outDir, "ReportB_Only.txt"));

      removeUniqueToReport(onlyA, freqTableA);
      removeUniqueToReport(onlyB, freqTableB);

      generateDiffReport(freqTableA, freqTableB, Path.Combine(outDir, "Report_Comparison.txt"));

      FormComplete dlgComplete = new FormComplete("Done comparing frequency reports.", outDir);
      dlgComplete.removeRef();
      dlgComplete.ShowDialog();

    }


    /// <summary>
    /// Parse the given frequency report and add to the given dictionary.
    /// </summary>
    private void parseFrequencyReport(Dictionary<string, uint> table, string file)
    {
      table.Clear();

      StreamReader reader = new StreamReader(file, Encoding.UTF8);

      string line = "";
      while ((line = reader.ReadLine()) != null)
      {
        string[] fields = line.Split(new char[] { '\t' });

        uint hits = Convert.ToUInt32(fields[0].Trim());
        string word = fields[1].Trim();

        if (table.ContainsKey(word))
        {
          table[word] += hits;
        }
        else
        {
          table.Add(word, hits);
        }
      }

      reader.Close();
    }


    /// <summary>
    /// Generate a reported that contains the words in tableA but not in tableB.
    /// </summary>
    private List<InfoFreqCompare> generateIntersectionReport(
      Dictionary<string, uint> tableA, Dictionary<string, uint> tableB, string outFile)
    {
      List<InfoFreqCompare> infoFreqList = new List<InfoFreqCompare>();

      foreach (string word in tableA.Keys)
      {
        if (!tableB.ContainsKey(word))
        {
          infoFreqList.Add(new InfoFreqCompare(word, tableA[word]));
        }
      }

      // Sort by # of hits
      infoFreqList.Sort(sortFreq);

      StreamWriter writer = new StreamWriter(outFile, false, Encoding.UTF8);

      foreach (InfoFreqCompare infoFreq in infoFreqList)
      {
        writer.WriteLine(string.Format("{0}\t{1}", infoFreq.Freq, infoFreq.Kanji));
      }

      writer.Close();

      return infoFreqList;
    }


    /// <summary>
    /// Remove words in provided list from provided table.
    /// </summary>
    private void removeUniqueToReport(List<InfoFreqCompare> infoFreqList, Dictionary<string, uint> table)
    {
      foreach (InfoFreqCompare infoFreq in infoFreqList)
      {
        if (table.ContainsKey(infoFreq.Kanji))
        {
          table.Remove(infoFreq.Kanji);
        }
      }
    }


    /// <summary>
    /// Generate a report of the differences between the two provided dictionaries in the format:
    /// tableA freq {tab} tableB freq {tab} word
    private void generateDiffReport(
      Dictionary<string, uint> tableA, Dictionary<string, uint> tableB, string outFile)
    {
      List<InfoFreqCompare> infoFreqList = new List<InfoFreqCompare>();

      foreach (string word in tableA.Keys)
      {
        infoFreqList.Add(new InfoFreqCompare(word, tableA[word], tableB[word]));
      }

      // Sort by # of hits in Table A
      infoFreqList.Sort(sortFreq);

      StreamWriter writer = new StreamWriter(outFile, false, Encoding.UTF8);

      foreach (InfoFreqCompare infoFreq in infoFreqList)
      {
        writer.WriteLine(string.Format("{0}\t{1}\t{2}",
          infoFreq.Freq, infoFreq.FreqB, infoFreq.Kanji));
      }

      writer.Close();
    }


    /// <summary>
    /// Sort by # of hits.
    /// </summary>
    protected int sortFreq(InfoFreqCompare x, InfoFreqCompare y)
    {
      return y.Freq.CompareTo(x.Freq);
    }
  }

  
  public class InfoFreqCompare
  {
    public string Kanji { get; set; }
    public ulong Freq { get; set; }
    public ulong FreqB { get; set; }

    public InfoFreqCompare(string kanji, ulong freq)
    {
      Kanji = kanji;
      Freq = freq;
      FreqB = 0;
    }

    public InfoFreqCompare(string kanji, ulong freq, ulong freqB)
    {
      Kanji = kanji;
      Freq = freq;
      FreqB = freqB;
    }
  }
}