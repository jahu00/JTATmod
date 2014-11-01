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
using System.IO;
using System.Linq;
using System.Text;

namespace JapaneseTextAnalysisTool
{
  abstract public class Freq
  {
    protected Dictionary<string, InfoFreq> freqTable = new Dictionary<string, InfoFreq>();
    private string outFilename = "freq_out.txt";

    /*public string OutFilename
    {
        get
        {
            return outFilename;
        }
        set
        {
            outFilename = value;
        }
    }*/

    public Dictionary<string, InfoFreq> Table
    {
      get
      {
        return this.freqTable;
      }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public Freq(string outFilename)
    {
      this.outFilename = outFilename;
    }

    /// <summary>
    /// Add an item to the frequency table.
    /// </summary>
    protected void addItemToFreqTable(string item, string partOfSpeech)
    {
      if (freqTable.ContainsKey(item))
      {
        freqTable[item].Freq += 1;
      }
      else
      {
        freqTable.Add(item, new InfoFreq(1, partOfSpeech));
      }
    }

    /// <summary>
    /// Sort by # of hits.
    /// </summary>
    protected int sortFreq(InfoFreq x, InfoFreq y)
    {
      int result = 0;

      result = y.Freq.CompareTo(x.Freq);

      return result;
    }

    public void addExistingFreq(Freq freq)
    {
      foreach (String key in freq.Table.Keys)
      {
        if (freqTable.ContainsKey(key))
        {
          freqTable[key].Freq += freq.Table[key].Freq;
        }
        else
        {
          freqTable.Add(key, freq.Table[key]);
        }
      }
    }


    /// <summary>
    /// Generate a frequency report.
    /// </summary>
    public void generateReport(string outDir, bool showPartOfSpeech, bool clear = true)
    {
      ulong totalHits = 0;
      ulong cumulativeHits = 0;

      List<InfoFreq> infoFreqList = new List<InfoFreq>();

      // Convert freqTable to a list (so that it can be sorted)
      foreach (string word in freqTable.Keys)
      {
        infoFreqList.Add(new InfoFreq(word, freqTable[word].Freq, freqTable[word].PartOfSpeech));
        totalHits += freqTable[word].Freq;
      }

      // Sort by # of hits
      infoFreqList.Sort(sortFreq);

      // Write the sorted list to the final output file
      StreamWriter writer = new StreamWriter(Path.Combine(outDir, outFilename), false, Encoding.UTF8);

      foreach (InfoFreq infoFreq in infoFreqList)
      {
        double percentage = (infoFreq.Freq / (double)totalHits) * 100;

        cumulativeHits += infoFreq.Freq;
        double cumulativePercentage = (cumulativeHits / (double)totalHits) * 100;

        if (showPartOfSpeech)
        {
          writer.WriteLine(string.Format("{0}\t{1}\t{2:0.00000000}\t{3:0.00000000}\t{4}",
            infoFreq.Freq, infoFreq.Kanji, percentage, cumulativePercentage, infoFreq.PartOfSpeech));
        }
        else
        {
          writer.WriteLine(string.Format("{0}\t{1}\t{2:0.00000000}\t{3:0.00000000}",
            infoFreq.Freq, infoFreq.Kanji, percentage, cumulativePercentage));
        }
      }

      writer.Close();
      if (clear)
      {
          freqTable.Clear();
      }
    }


    /// <summary>
    /// Clear the frequency table.
    /// </summary>
    public void reset()
    {
      freqTable.Clear();
    }


    /// <summary>
    /// Add text to the frequency table.
    /// </summary>
    public abstract void addFileText(string text);


  }
}
