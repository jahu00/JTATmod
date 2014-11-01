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

namespace JapaneseTextAnalysisTool
{
  public class InfoUserReadability
  {
    /// <summary>
    /// The file that is being analzyed for readability.
    /// </summary>
    public string Filename { get; set; }


    //
    // Non-unique
    //

    /// <summary>
    /// Readability expressed as a percentage (0-100) of the total number
    /// of non-unique known words vs. the total number of non-unique words.
    /// </summary>
    public double Readability
    {
      get
      {
        if (this.TotalWords == 0)
        {
          return 0;
        }
        else
        {
          return (this.TotalKnownWords / (double)this.TotalWords) * 100;
        }
      }
    }

    /// <summary>
    /// Total number of non-unique words.
    /// </summary>
    public ulong TotalWords { get; set; }


    /// <summary>
    /// Total number of non-unique known words.
    /// </summary>
    public ulong TotalKnownWords { get; set; }


    /// <summary>
    /// Total number of non-unique unknown words.
    /// </summary>
    public ulong TotalUnknownWords
    {
      get
      {
        return this.TotalWords - this.TotalKnownWords;
      }
    }

    //
    // Unique
    //

    /// <summary>
    /// Readability expressed as a percentage (0-100) of the total number
    /// of unique known words vs. the total number of unique words.
    /// </summary>
    public double UniqueReadability
    { 
      get
      {
        if (this.UniqueTotalWords == 0)
        {
          return 0;
        }
        else
        {
          return (this.UniqueTotalKnownWords / (double)this.UniqueTotalWords) * 100;
        }
      }
    }

    /// <summary>
    /// Total number of unique words.
    /// </summary>
    public ulong UniqueTotalWords { get; set; }

    /// <summary>
    /// Total number of unique known words.
    /// </summary>
    public ulong UniqueTotalKnownWords { get; set; }

    /// <summary>
    /// Total number of unique unknown words.
    /// </summary>
    public ulong UniqueTotalUnknownWords
    {
      get
      {
        return UniqueTotalWords - UniqueTotalKnownWords;
      }
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    public InfoUserReadability(string filename)
    {
      this.Filename = filename;
    }


    /// <summary>
    /// Update the unique/non-unique properties based on the
    /// word list and the list of user's known words.
    /// </summary>
    public void updateFields(List<string> wordList, List<string> userKnownWords)
    {
      // Key   = Word
      // Value = Count of words in the text 
      Dictionary<string, ulong> wordsDic = new Dictionary<string, ulong>();

      this.TotalWords = (ulong)wordList.Count;

      // Populate dictionary.
      foreach (string word in wordList)
      {
        if (!wordsDic.ContainsKey(word))
        {
          wordsDic.Add(word, 0);
        }

        wordsDic[word]++;
      }

      UniqueTotalWords = (ulong)wordsDic.Keys.Count;

      // Update known words properties.
      foreach (string word in userKnownWords)
      {
        if (wordsDic.ContainsKey(word))
        {
          this.TotalKnownWords += wordsDic[word];
          this.UniqueTotalKnownWords++;
        }
      }

    }



  }
}
