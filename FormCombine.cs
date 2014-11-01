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
  public partial class FormCombine : Form
  {
    // Freq, #_of_hits
    private Dictionary<string, uint> freqTable = new Dictionary<string, uint>();

    private string lastDirPath = "";


    public FormCombine()
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


    private void buttonInDir_Click(object sender, EventArgs e)
    {
      this.textBoxInDir.Text = showFolderDialog(this.textBoxInDir.Text.Trim());
    }


    private void buttonOutDir_Click(object sender, EventArgs e)
    {
      this.textBoxOutDir.Text = showFolderDialog(this.textBoxOutDir.Text.Trim());
    }


    private void buttonCombine_Click(object sender, EventArgs e)
    {
      combineReports();
    }


    /// <summary>
    /// Combine frequency reports.
    /// </summary>
    private void combineReports()
    {
      string inDir = textBoxInDir.Text.Trim();
      string outDir = textBoxOutDir.Text.Trim();

      if (!Directory.Exists(inDir))
      {
        UtilsMsg.showErrMsg("Please enter a valid input directory.");
        return;
      }

      if (!Directory.Exists(outDir))
      {
        UtilsMsg.showErrMsg("Please enter a valid output directory.");
        return;
      }

      string[] inFiles = UtilsCommon.getNonHiddenFilesInDir(inDir, "*.txt");

      foreach (string file in inFiles)
      {
        parseFrequencyReport(file);
      }

      generateCombinedReport(outDir);

      FormComplete dlgComplete = new FormComplete("Done combining frequency reports.", outDir);
      dlgComplete.removeRef();
      dlgComplete.ShowDialog();
    }


    /// <summary>
    /// Parse the given frequency report and add to the dictionary.
    /// </summary>
    private void parseFrequencyReport(string file)
    {
      StreamReader reader = new StreamReader(file, Encoding.UTF8);

      string line = "";
      while ((line = reader.ReadLine()) != null)
      {
        string[] fields = line.Split(new char[] { '\t' });

        uint hits = Convert.ToUInt32(fields[0].Trim());
        string word = fields[1].Trim();

        if (freqTable.ContainsKey(word))
        {
          freqTable[word] += hits;
        }
        else
        {
          freqTable.Add(word, hits);
        }
      }

      reader.Close();
    }


    /// <summary>
    /// Generate a combined frequency report.
    /// </summary>
    public void generateCombinedReport(string outDir)
    {
      List<InfoFreq> infoFreqList = new List<InfoFreq>();

      // Convert freqTable to a list (so that it can be sorted)
      foreach (string word in freqTable.Keys)
      {
        infoFreqList.Add(new InfoFreq(word, freqTable[word]));
      }

      // Sort by # of hits
      infoFreqList.Sort(sortFreq);

      // Write the sorted list to the final output file
      StreamWriter writer = new StreamWriter(Path.Combine(outDir, "combined_freq_report.txt"), false, Encoding.UTF8);

      foreach (InfoFreq infoFreq in infoFreqList)
      {
        writer.WriteLine(string.Format("{0}\t{1}", infoFreq.Freq, infoFreq.Kanji));
      }

      writer.Close();

      freqTable.Clear();
    }


    /// <summary>
    /// Sort by # of hits.
    /// </summary>
    protected int sortFreq(InfoFreq x, InfoFreq y)
    {
      return y.Freq.CompareTo(x.Freq);
    }





  }
}
