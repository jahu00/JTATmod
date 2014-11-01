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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace JapaneseTextAnalysisTool
{
  // Hayashi and Obi-2 readability stats
  public class FormulaReadability
  {
    List<InfoFormulaReadability> readabilityList = new List<InfoFormulaReadability>();
    private string outFilename = "formula_based_readability_report.txt";

    private enum CharType
    {
      KANJI,
      HIRAGANA,
      KATAKANA,
      ROMAN,
      OTHER
    }


    public List<InfoFormulaReadability> List 
    {
      get
      {
        return this.readabilityList;
      }
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    public FormulaReadability(string outFilename)
    {
      this.outFilename = outFilename;
    }

    /// <summary>
    /// Count the number of occurances of a word in a string.
    /// </summary>
    private int countOccurances(string text, string pattern)
    {
      int count = 0;
      int i = 0;

      while ((i = text.IndexOf(pattern, i)) != -1)
      {
        i += pattern.Length;
        count++;
      }

      return count;
    }


    /// <summary>
    /// Get the type of character.
    /// </summary>
    private CharType getCharType(char c)
    {
      if (UtilsLang.containsIdeograph(c))
      {
        return CharType.KANJI;
      }
      else if (UtilsLang.containsHiragana(c))
      {
        return CharType.HIRAGANA;
      }
      else if (UtilsLang.containsKatakana(c))
      {
        return CharType.KATAKANA;
      }
      else if (((c >= '!') && (c <= '/')) || ((c >= ':') && (c <= '~')) || ((c >= '¡') && (c <= 'ÿ'))) // TODO: Should all these symbols be used for roman?
      {
        return CharType.ROMAN;
      }
      else
      {
        return CharType.OTHER;
      }
    }


    /// <summary>
    /// Calculate the hayashi score.
    /// 
    /// For Japanese, the critical factors are: sentence length, length of runs of Roman letters and symbols 
    /// and of the different Japanese characters (Hiragana, Kanji and Katakana), and the ratio of 
    /// tooten(comma) to kuten(period). The original formula used 10 factors, the following is only based off of six.
    /// Readability Score = -(0.12 * LS) - (1.37 * LA) + (7.4 * LH) - (23.18 * LC) - (5.4 * LK) - (4.67 * CP) + 115.79
    /// LS = length of the sentences
    /// LA = average number of Roman letters and symbols per run
    /// LH = average number of Hiragana characters per run
    /// LC = average number of Kanji character per run
    /// LK = average number of Katakana character per run
    /// CP = ratio of tooten (comma) to kuten (period)
    /// Run = a continuous string of the same type of character
    /// http://www.ideosity.com/SEO/SEO-Readability-Tests.aspx#Hayashi
    /// 
    /// </summary>
    private double calcHayashiScore(string text)
    {
      string lineEnding = UtilsCommon.getLineEnding(text);

      int numEndQuotes = countOccurances(text, "」" + lineEnding);
      numEndQuotes += countOccurances(text, "』" + lineEnding);
      int numKuten = countOccurances(text, "。");
      int numTouten = countOccurances(text, "、");

      CharType runType = this.getCharType(text[0]);
      int runCount = -1;

      int numKanjiChars = 0;
      int numHiraganaChars = 0;
      int numKatakanaChars = 0;
      int numRomanChars = 0;

      int numKanjiRuns = 0;
      int numHiraganaRuns = 0;
      int numKatakanaRuns = 0;
      int numRomanRuns = 0;

      for (int i = 0; i < text.Length; i++)
      {
        char c = text[i];
        CharType type = getCharType(c);
        runCount++;

        if (type != runType)
        {
          if (runType == CharType.KANJI)
          {
            numKanjiChars += runCount;
            numKanjiRuns++;
          }
          else if (runType == CharType.HIRAGANA)
          {
            numHiraganaChars += runCount;
            numHiraganaRuns++;
          }
          else if (runType == CharType.KATAKANA)
          {
            numKatakanaChars += runCount;
            numKatakanaRuns++;
          }
          else if (runType == CharType.ROMAN)
          {
            numRomanChars += runCount;
            numRomanRuns++;
          }

          runCount = 0;
          runType = type;
        }
      }

      // LS: Length of the sentences
      double LS = (double)text.Length / Math.Max(1, (numEndQuotes + numKuten));

      // LA: Average number of Roman letters and symbols per run
      double LA = numRomanChars / (double)Math.Max(1, numRomanRuns);

      // LH: Average number of Hiragana characters per run
      double LH = numHiraganaChars / (double)Math.Max(1, numHiraganaRuns);

      // LC: Average number of Kanji character per run
      double LC = numKanjiChars / (double)Math.Max(1, numKanjiRuns);

      // LK: Average number of Katakana character per run
      double LK = numKatakanaChars / (double)Math.Max(1, numKatakanaRuns);

      // CP: Ratio of tooten (comma) to kuten (period)
      double CP = numTouten / (double)Math.Max(1, numKuten);

      double score = -(0.12 * LS) - (1.37 * LA) + (7.4 * LH) - (23.18 * LC) - (5.4 * LK) - (4.67 * CP) + 115.79;

      return score;
    }


    /// <summary>
    /// Calculate the Obi-2 level.
    /// 
    /// 1-6:   Elementary school (6 years)
    /// 7-9:   Junior high school (3 years)
    /// 10-12: High school (3 years)
    /// 13-:   Beyond high school
    /// http://kotoba.nuee.nagoya-u.ac.jp/sc/obi2/obi_e.html
    /// </summary>
    private int calcObi2Level(string text)
    {
      string tempFileName = Path.GetTempPath() + "~temp_readability_" +  Guid.NewGuid() + ".txt";
      string obi2dir = UtilsCommon.getAppDir(true) + "obi2";

      using (StreamWriter writer = new StreamWriter(tempFileName))
      {
        writer.WriteLine(text);
      }

      Process proc = new Process();
      proc.StartInfo.FileName = Path.Combine(obi2dir, "obi2.exe");
      proc.StartInfo.WorkingDirectory = obi2dir;
      proc.StartInfo.Arguments = String.Format(@"-k W ""{0}""", tempFileName);
      proc.StartInfo.UseShellExecute = false;
      proc.StartInfo.RedirectStandardError = true;
      proc.StartInfo.RedirectStandardOutput = true;
      proc.StartInfo.CreateNoWindow = true;
      proc.Start();

      string output = proc.StandardOutput.ReadToEnd();

      string[] fields = output.Split();

      int level;
      int numOperativeBigrams;

      try
      {
        level = Convert.ToInt32(fields[0].Trim());
        numOperativeBigrams = Convert.ToInt32(fields[1].Trim());
      }
      catch
      {
        level = -1;
      }

      return level;
    }



    /// <summary>
    /// Add text to the readability table.
    /// </summary>
    public void addFileText(string filename, string text)
    {
      double hayashiScore = calcHayashiScore(text);
      int obiScore = calcObi2Level(text);

      InfoFormulaReadability infoReadability = new InfoFormulaReadability(filename, hayashiScore, obiScore);

      readabilityList.Add(infoReadability);
    }


    /// <summary>
    /// Add results from other FormulaReadability object.
    /// </summary>
    public void addExisting(FormulaReadability other)
    {
      foreach (InfoFormulaReadability info in other.List)
      {
        this.readabilityList.Add(info);
      }
    }


    /// <summary>
    /// Generate the formula-based readability report.
    /// </summary>
    public void generateReport(string outDir)
    {
      List<InfoFormulaReadability> finalList = new List<InfoFormulaReadability>();
      Dictionary<int, List<InfoFormulaReadability>> obi2Groups = new Dictionary<int, List<InfoFormulaReadability>>();

      // Sort original list by Obi-2
      readabilityList.Sort(this.sortObi2Score);

      // Add each readability to it's own obi-2 group
      foreach (InfoFormulaReadability readability in readabilityList)
      {
        if (!obi2Groups.ContainsKey(readability.Obi2Score))
        {
          obi2Groups.Add(readability.Obi2Score, new List<InfoFormulaReadability>());
        }

        obi2Groups[readability.Obi2Score].Add(readability);
      }

      // Sort each group by Hayashi and then add each group to the final list
      foreach (int group in obi2Groups.Keys)
      {
        obi2Groups[group].Sort(sortHiyashiScore);
        finalList.AddRange(obi2Groups[group]);
      }
      
      StreamWriter writer = new StreamWriter(Path.Combine(outDir, outFilename), false, Encoding.UTF8);

      foreach (InfoFormulaReadability readability in finalList)
      {
        writer.WriteLine(string.Format("{0}\t{1}\t{2}", 
          readability.Obi2Score, readability.HayashiScore, readability.Filename));
      }

      writer.Close();

      readabilityList.Clear();
    }


    /// <summary>
    /// Clear the readability table.
    /// </summary>
    public void reset()
    {
      readabilityList.Clear();
    }


    /// <summary>
    /// Sort by Obi2.
    /// </summary>
    private int sortObi2Score(InfoFormulaReadability x, InfoFormulaReadability y)
    {
      int result = 0;

      result = y.Obi2Score.CompareTo(x.Obi2Score) * -1;

      return result;
    }


    /// <summary>
    /// Sort by Hiayashi score.
    /// </summary>
    private int sortHiyashiScore(InfoFormulaReadability x, InfoFormulaReadability y)
    {
      int result = 0;

      result = y.HayashiScore.CompareTo(x.HayashiScore);

      return result;
    }


 


  }
}
