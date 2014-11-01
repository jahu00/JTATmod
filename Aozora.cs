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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JapaneseTextAnalysisTool
{
  /// <summary>
  /// Convert various aozora format constructs to HTML
  /// </summary>
  public class Aozora
  {
    // Key   = Aozora gaiji code (ex 1-90-35)
    // Value = The utf-8 equivalent
    private Dictionary<string, string> gaijiTable = new Dictionary<string, string>();

    public Aozora()
    {
      // Load the gaiji codes from file. They are used by the addGaiji match evaluator.
      this.loadGaijiCodes();
    }

    enum RubyToken
    {
      KANJI,
      HIRAGANA,
      KATAKANA,
      RUBY_OPEN,
      RUBY_CLOSE,
      RUBY_START,
      ENG,
      PUNC,
      OTHER
    }


    /// <summary>
    /// Determine the token type of the given character
    /// </summary>
    private RubyToken lex(char c)
    {
      if ((c >= '\u4E00' && c <= '\u9FBF') || c == '\u3005')
        return RubyToken.KANJI;
      else if ((c >= '\u3040') && (c <= '\u309F'))
        return RubyToken.HIRAGANA;
      else if ((c >= '\u30A0') && (c <= '\u30FF'))
        return RubyToken.KATAKANA;
      else if (c == '《')
        return RubyToken.RUBY_OPEN;
      else if (c == '》')
        return RubyToken.RUBY_CLOSE;
      else if (c == '｜')
        return RubyToken.RUBY_START;
      else if (c >= 'A' && c <= 'z')
        return RubyToken.ENG;
      else if (((c >= '\u3000') && (c <= '\u303F')) || ((c >= '\0') && (c <= ' ')) || (c == '\"') || (c == '“'))
        return RubyToken.PUNC;
      else
        return RubyToken.OTHER;
    }


    /// <summary>
    /// Convert aozara ruby text into HTML5 ruby tags.
    /// 
    /// Describes the aozara ruby format (with examples):
    /// http://www.aozora.gr.jp/KOSAKU/MANUAL_2.html
    /// 
    /// HTML5 ruby tag info:
    /// http://www.useragentman.com/blog/2010/10/29/cross-browser-html5-ruby-annotations-using-css/
    /// http://html5doctor.com/ruby-rt-rp-element/
    /// 
    /// This routine was adapted from:
    /// http://www.chiark.greenend.org.uk/~pmaydell/misc/aozora_ruby.py
    /// 
    /// Example:
    /// 武州｜青梅《おうめ》の宿  ->  武州<ruby><rb>青梅</rb><rp>《</rp><rt>おうめ</rt><rp>》</rp></ruby>の宿
    /// </summary>
    public string addRuby(string text)
    {
      int offset = 0;
      bool rubyStartFound = false;
      bool ignoreNextClose = false;
      StringBuilder builder = new StringBuilder("", 50000);
      char prevC = ' ';

      while (offset < text.Length)
      {
        char c = text[offset];

        if (this.lex(c) == RubyToken.RUBY_START)
        {
		      rubyStartFound = true;
          builder.Append("<ruby><rb>");
        }
        else if(this.lex(c) == RubyToken.RUBY_OPEN)
        {
          if (!rubyStartFound)
          {
            // Correct for weird case where a close is followed by an open: スター刷毛《はけ》《たんぽ》、小筆
            // Or case where the first character in the file is an open
            if ((this.lex(prevC) == RubyToken.RUBY_CLOSE) || (offset == 0))
            {
              offset++;
              ignoreNextClose = true;
              continue;
            }

            offset--;
            RubyToken tokenType = this.lex(text[offset]);

            while ((this.lex(text[offset]) == tokenType) && (offset > 0))
            {
              builder.Remove(builder.ToString().Length - 1, 1);
              offset--;
            }

            rubyStartFound = true;
            builder.Append("<ruby><rb>");
          }
          else
          {
            builder.Append("</rb><rp>《</rp><rt>");
          }
        }
        else if (this.lex(c) == RubyToken.RUBY_CLOSE)
        {
          if (!ignoreNextClose)
          {
            builder.Append("</rt><rp>》</rp></ruby>");
            rubyStartFound = false;
          }
          else
          {
            ignoreNextClose = false;
          }
        }
        else
        {
          builder.Append(c);
        }

        prevC = c;

        offset++;
      }

      string outStr = builder.ToString();

      return outStr;
    }


    /// <summary>
    /// Remove ruby constructs.
    /// </summary>
    public string removeRuby(string text)
    {
      text = Regex.Replace(text, "《.*?》", "");
      text = Regex.Replace(text, "｜", "");

      return text;
    }


    /// <summary>
    /// Adds ruby tags for emphasis (傍点).
    /// 
    /// See the 【傍点】 section:
    /// http://www.aozora.gr.jp/KOSAKU/MANUAL_2.html
    /// 
    /// Example:
    /// 胡麻塩おやじ［＃「おやじ」に黒三角傍点］ ->  胡麻塩<ruby><rb>おやじ</rb><rp>《</rp><rt>▲▲▲</rt><rp>》</rp></ruby>
    /// </summary>
    public string addEmphasis(string text)
    {
      string outText = "";
      int index = 0;
      int prevIndex = 0;
      Match match;

      match = Regex.Match(text, @"［＃「(?<Kanji>[^［]*?)」に(?<Emphasis>(?:傍点)|(?:白ゴマ傍点)|(?:丸傍点)|(?:白丸傍点)|(?:黒三角傍点)|(?:白三角傍点)|(?:二重丸傍点)|(?:蛇の目傍点))］", RegexOptions.None);

      while (match.Success)
      {
        index = match.Index;
        string kanjiText = match.Groups["Kanji"].ToString();
        string emphasisType = match.Groups["Emphasis"].ToString();
        int lengthToCopy = index - prevIndex - kanjiText.Length;

        // If the formatting is bad (Ex: やじ［＃「おやじ」に黒三角傍点   should be   おやじ［＃「おやじ」に黒三角傍点
        if (lengthToCopy < 0)
        {
          lengthToCopy = index - prevIndex;
        }

        // Add all of the text before the emphasis text
        outText += text.Substring(prevIndex, lengthToCopy);

        string emphasisChar = "&#xFE45;";
        string emphasisStr = "";

        // Determine the emphasis character to use
        if (emphasisType == "傍点")
          emphasisChar = "&#xFE45;";
        else if (emphasisType == "白ゴマ傍点")
          emphasisChar = "&#xFE46;";
        else if (emphasisType == "丸傍点")
          emphasisChar = "●";
        else if (emphasisType == "白丸傍点")
          emphasisChar = "○";
        else if (emphasisType == "黒三角傍点")
          emphasisChar = "▲";
        else if (emphasisType == "白三角傍点")
          emphasisChar = "△";
        else if (emphasisType == "二重丸傍点")
          emphasisChar = "◎";
        else if (emphasisType == "蛇の目傍点")
          emphasisChar = "◉";

        // At one emphsis character for each kanji
        for (int i = 0; i < kanjiText.Length; i++)
        {
          emphasisStr += emphasisChar;
        }

        // Add the formatting emphasis text
        outText += String.Format("<ruby><rb>{0}</rb><rp>《</rp><rt>{1}</rt><rp>》</rp></ruby>", 
          kanjiText, emphasisStr);

        // Move past the Aozora formatting
        prevIndex = index + kanjiText.Length + 6 + emphasisType.Length;

        match = match.NextMatch();
      }

      // Add the remaining text
      outText += text.Substring(prevIndex, text.Length - prevIndex);

      return outText;
    }


    /// <summary>
    /// Removes ruby tags for emphasis (傍点).
    /// </summary>
    public string removeEmphasis(string text)
    {
      text = Regex.Replace(text, @"［＃「(?<Kanji>[^［]*?)」に(?<Emphasis>(?:傍点)|(?:白ゴマ傍点)|(?:丸傍点)|(?:白丸傍点)|(?:黒三角傍点)|(?:白三角傍点)|(?:二重丸傍点)|(?:蛇の目傍点))］", "");

      return text;
    }


    /// <summary>
    /// Adds underline (傍線) support.
    /// 
    /// Example:
    /// 返事をする自信［＃「自信」に傍線］ ----->  返事をする<u>自信</u>
    /// </summary>
    public string addUnderline(string text)
    {
      string outText = "";
      int index = 0;
      int prevIndex = 0;
      Match match;

      match = Regex.Match(text, "［＃「(?<Underline>[^［]*?)」に傍線］", RegexOptions.None);

      while (match.Success)
      {
        index = match.Index;
        string underlinedText = match.Groups["Underline"].ToString();
        int lengthToCopy = index - prevIndex - underlinedText.Length;

        // If the formatting is bad (Ex: 信［＃「自信」   so should be  自信［＃「自信」
        if (lengthToCopy < 0)
        {
          lengthToCopy = index - prevIndex;
        }

        // Add all of the text before the underlined text
        outText += text.Substring(prevIndex, lengthToCopy);

        // Add the formatting underlined text
        outText += String.Format("<u>{0}</u>", underlinedText);

        // Move past the Aozora formatting
        prevIndex = index + underlinedText.Length + 8; 

        match = match.NextMatch();
      }

      // Add the remaining text
      outText += text.Substring(prevIndex, text.Length - prevIndex);

      return outText;
    }


    /// <summary>
    /// Removes underline (傍線) constructs.
    /// </summary>
    public string removeUnderline(string text)
    {
      text = Regex.Replace(text, @"［＃「(?<Kanji>[^［]*?)」に傍線］", "");

      return text;
    }



    /// <summary>
    /// Convert aozora image formatting to html image tags
    /// 
    /// Example:
    /// ［＃挿絵（img/imagename.png）入る］  ->  <img src="img/imagename.jpg" />
    public string addImage(string text)
    {
      text = Regex.Replace(text, @"［＃(?:(?:挿絵)|(?:表紙))?（(?<Image>[^［]*?\.(?:(?:jpg)|(?:png)|(?:bmp)|(?:gif)))）(?:入る)?］",
        @"<img src=""$1"" />", RegexOptions.IgnoreCase);

      return text;
    }


    /// <summary>
    /// Remove aozora image formatting.
    /// </summary>
    public string removeImage(string text)
    {
      text = Regex.Replace(text, @"［＃(?:(?:挿絵)|(?:表紙))?（(?<Image>[^［]*?\.(?:(?:jpg)|(?:png)|(?:bmp)|(?:gif)))）(?:入る)?］",
        "", RegexOptions.IgnoreCase);

      return text;
    }


    /// <summary>
    /// Remove some aozora format contructs that JNF doesn't currently support.
    /// </summary>
    public string removeUnsupported(string text)
    {
      // Top indentation 
      text = Regex.Replace(text, @"［＃[^［]*?字下げ］", ""); // Start tag (［＃２字下げ］or ［＃ここから2字下げ］)
      text = Regex.Replace(text, @"［＃[^［]*?字下げ終わり］", ""); // End tag

      // Bottom indentation
      text = Regex.Replace(text, @"［＃[^［]*?字上げ］", ""); // Start tag
      text = text.Replace(@"［＃地付き］", ""); // End tag

      // Center
      text = text.Replace(@"［＃中央揃え］", "");

      // New page
      text = text.Replace(@"［＃改ページ］", "");

      // Revision
      text = text.Replace(@"［＃改丁］", "");
      

      return text;
    }


    /// <summary>
    /// Load the gaiji codes from file. Only utf-8 codes will be loaded. CID codes will be ignored.
    /// Tab-seperated input file looks something like this:
    /// ...
    /// 2-92-52 \UTF{9908}
    /// 2-92-53 \UTF{4b3b}
    /// 2-92-54 \CID{18959}
    /// 2-92-55 \UTF{9916}
    /// 2-92-56 \UTF{9917}
    /// 2-92-57 \CID{18962}
    /// 2-92-58 \UTF{991a}
    /// 2-92-59 \UTF{991b}
    /// ...
    /// </summary>
    private void loadGaijiCodes()
    {
      string curLine = "";

      this.gaijiTable.Clear();

      try
      {
        using (StreamReader reader = new StreamReader(
          String.Format(@"{0}{1}\Gaiji\gaiji_codes.txt", UtilsCommon.getAppDir(false), Path.DirectorySeparatorChar), Encoding.UTF8))
        {
          while ((curLine = reader.ReadLine()) != null)
          {
            Match match = Regex.Match(curLine, @"^(?<GaijiCode>.*?)\t\\UTF{(?<Utf8Code>.*?)}");

            if (match.Success)
            {
              string gaijiCode = match.Groups["GaijiCode"].ToString().Trim();
              string utf8Code = match.Groups["Utf8Code"].ToString().Trim();

              if (!this.gaijiTable.ContainsKey(gaijiCode))
              {
                try
                {
                  char utfChar = (char)int.Parse(utf8Code, NumberStyles.HexNumber);
                  this.gaijiTable.Add(gaijiCode, utfChar.ToString());
                }
                catch
                {
                  // Don't care
                }
              }
            }
          }
        }
      }
      catch
      {
        UtilsMsg.showErrMsg("Unable to load gaiji codes!");
      }
    }


    /// <summary>
    /// Match evaluator that replaces a gaiji with utf-8.
    /// </summary>
    private string gaijiMatchEval(Match match)
    {
      string gaijiCode = match.Groups["GaijiCode"].ToString().Trim();
      string utf8Code = "";

      // If the gaiji exists it the table, use it. 
      // Otherwise, return the original aozora gaiji construct.
      if (this.gaijiTable.ContainsKey(gaijiCode))
      {
        utf8Code = this.gaijiTable[gaijiCode];
      }
      else
      {
        utf8Code = match.Value;
      }

      return utf8Code;
    }


    /// <summary>
    /// Replace aozora gaiji constructs with utf-8 characters.
    /// 
    /// Example:
    /// ※［＃「皐＋羽」、第3水準1-90-35］   ------>   翺
    /// </summary>
    public string addGaiji(string text)
    {
      text = Regex.Replace(text, @"※?［＃「.*?(?<GaijiCode>\d\-\d{1,2}\-\d{1,2})］", this.gaijiMatchEval);

      // Also handle "〽", the part alternation mark
      text = text.Replace("※［＃歌記号］", "〽");

      return text;
    }


    /// <summary>
    /// Remove the comment between the 2 dashed lines at the head of the novel.
    /// </summary>
    public string removeComment(string text)
    {
      string lineEnding = UtilsCommon.getLineEnding(text);

      // Get the first 30 lines
      Match match = Regex.Match(text, "(?:.*" + lineEnding + "){1,30}");

      if (match.Success)
      {
        string headerText = match.Value;
        int numChars = headerText.Length;

        // Remove the comment in the first 30 lines
        headerText = Regex.Replace(headerText, @"--*.*?--*" + lineEnding, "", RegexOptions.Singleline);
        text = headerText + text.Substring(numChars);
      }

      return text;
    }




  }
}
