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
using System.Text.RegularExpressions;


namespace JapaneseTextAnalysisTool
{
  public class FreqWord : Freq
  {
    public enum WordFreqMethod
    {
      MECAB,
      JPARSER,
    }

    private List<string> lastWordList = new List<string>();


    /// <summary>
    /// The list of words from the last parse.
    /// </summary>
    public List<string> LastWordList 
    {
      get 
      { 
        return lastWordList;  
      }
    }


    /// <summary>
    /// The method/tool used to extract words from the text.
    /// </summary>
    public WordFreqMethod ParseMethod { get; set; }


    /// <summary>
    /// If true, use kanji form of word even if it is normally written in kana 
    /// (as determined by presence of a (uk) tag in EDICT).
    /// 
    /// If false, use the kana form of word if it is normally written in kana.
    /// 
    /// For example:
    /// If true, ともに will be converted to 共に.
    /// If false,  共に will be converted to ともに.
    /// </summary>
    public bool Kanjify { get; set; }


    public FreqWord(string outFilename)
      : base(outFilename)
    {
      this.ParseMethod = WordFreqMethod.MECAB;
      this.Kanjify = false;
    }


    public override void addFileText(string text)
    {
      this.lastWordList.Clear();

      if (ParseMethod == WordFreqMethod.MECAB)
      {
        addWithMecab(text);
      }
      else
      {
        addWithJParser(text);
      }
    }


    /// <summary>
    /// Analyze the text with Mecab.
    /// </summary>
    private void addWithMecab(string text)
    {
      List<InfoMecab> mecabList = Mecab.parseFields(text, true);

      foreach (InfoMecab mecab in mecabList)
      {
        string word = mecab.Root.Trim();

        if ((word != "*")
          && (UtilsLang.containsIdeograph(word)
            || UtilsLang.containsHiragana(word)
            || UtilsLang.containsKatakana(word)))
        {
          addItemToFreqTable(word, mecab.Pos);
          this.lastWordList.Add(word);
        }
      }
    }


    /// <summary>
    /// Analyze the text with JParser.
    /// </summary>
    private void addWithJParser(string text)
    {
      // Split text into small chunks, otherwise JParser takes FOREVER!!! (minutes rather than seconds).
      const int linesPerParse = 100;

      string lineEnding = UtilsCommon.getLineEnding(text);
      string[] lines = text.Split(new string[] { lineEnding }, StringSplitOptions.RemoveEmptyEntries);
      string curText = "";

      for (int i = 0; i < lines.Length; i++)
      {
        curText += lines[i];

        // Parse text every linesPerParse lines or if the end is reached.
        if ((i % linesPerParse) == (linesPerParse - 1)
          || (i == lines.Length - 1))
        {
          List<InfoJParser> jparseList = JParser.parseFields(curText);

          foreach (InfoJParser info in jparseList)
          {
            string word = info.Deinflected.Trim();

            // If the word is normally written with kana, use the reading instead (if desired)
            if (!Kanjify 
              && info.Definition.Contains("(uk)"))
            {
              if (info.Reading != "")
              {
                word = info.Reading;
              }
              else
              {
                word = info.Word;
              }
            }

            string partOfSpeech = "";

            Match match = Regex.Match(info.Definition, @"^\((?<POS>.*?)\)");

            if (match.Success)
            {
              partOfSpeech = match.Groups["POS"].ToString().Trim();
            }

            if ((UtilsLang.containsIdeograph(word)
              || UtilsLang.containsHiragana(word)
              || UtilsLang.containsKatakana(word)))
            {
              addItemToFreqTable(word, partOfSpeech);
              this.lastWordList.Add(word);
            }
          }

          // Reset
          curText = "";
        }
      }
    }
  





  }
}
