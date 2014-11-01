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
using System.Linq;
using System.Text;
using System.IO;

namespace JapaneseTextAnalysisTool
{
  public class UserReadability
  {
    private List<InfoUserReadability> readabilityList = new List<InfoUserReadability>();
    private string outFilename = "user_based_readability_report.txt";

    public List<InfoUserReadability> List 
    {
      get
      {
        return this.readabilityList;
      }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public UserReadability(string outFilename)
    {
      this.outFilename = outFilename;
    }


    /// <summary>
    /// Add results from other UserReadability object.
    /// </summary>
    public void addExisting(UserReadability other)
    {
      foreach (InfoUserReadability info in other.List)
      {
        this.readabilityList.Add(info);
      }
    }


    /// <summary>
    /// Add text to the readability table for given filename.
    /// </summary>
    public void addFileText(string filename, string text, List<string> userKnownWords, FreqWord.WordFreqMethod parseMethod, bool kanjify)
    {
      FreqWord freqWord = new FreqWord("dummy.txt");
      freqWord.ParseMethod = parseMethod;
      freqWord.Kanjify = kanjify;

      freqWord.addFileText(text);

      InfoUserReadability infoUserReadability = new InfoUserReadability(filename);
      infoUserReadability.updateFields(freqWord.LastWordList, userKnownWords);

      this.readabilityList.Add(infoUserReadability);
    }


    /// <summary>
    /// Add word list to the readability table for given filename.
    /// </summary>
    public void addFileWordList(string filename, List<string> wordList, List<string> userKnownWords)
    {
      InfoUserReadability infoUserReadability = new InfoUserReadability(filename);
      infoUserReadability.updateFields(wordList, userKnownWords);

      this.readabilityList.Add(infoUserReadability);
    }


    /// <summary>
    /// Generate the user-based readability report.
    /// Format:
    ///   Field 1: Readability expressed as a percentage (0-100) of the total number
    ///            of non-unique known words vs. the total number of non-unique words.
    ///   Field 2: Total number of non-unique words
    ///   Field 3: Total number of non-unique known words
    ///   Field 4: Total number of non-unique unknown words
    ///   Field 5: Readability expressed as a percentage (0-100) of the total number
    ///            of unique known words vs. the total number of unique words.
    ///   Field 6: Total number of unique words
    ///   Field 7: Total number of unique known words
    ///   Field 8: Total number of unique unknown words
    ///   Field 9: Filename
    /// </summary>
    public void generateReport(string outDir)
    {
      // Sort by readability
      this.readabilityList.Sort(this.sortByReadability);

      StreamWriter writer = new StreamWriter(Path.Combine(outDir, outFilename), false, Encoding.UTF8);

      foreach (InfoUserReadability readability in this.readabilityList)
      {
        writer.WriteLine(string.Format("{0:0.00}\t{1}\t{2}\t{3}\t{4:0.00}\t{5}\t{6}\t{7}\t{8}",
          readability.Readability,
          readability.TotalWords, 
          readability.TotalKnownWords,
          readability.TotalUnknownWords,
          readability.UniqueReadability,
          readability.UniqueTotalWords,
          readability.UniqueTotalKnownWords,
          readability.UniqueTotalUnknownWords,
          readability.Filename
          ));
      }

      writer.Close();

      this.readabilityList.Clear();
    }


    /// <summary>
    /// Clear the readability table.
    /// </summary>
    public void reset()
    {
      this.readabilityList.Clear();
    }


    /// <summary>
    /// Sort InfoUserReadability collection by Readability. 
    /// </summary>
    private int sortByReadability(InfoUserReadability x, InfoUserReadability y)
    {
      return y.Readability.CompareTo(x.Readability);
    }






  }
}
