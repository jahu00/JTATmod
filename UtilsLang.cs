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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace JapaneseTextAnalysisTool
{
  public class UtilsLang
  {

    public static bool containsAlpha(string str)
    {
      return Regex.IsMatch(str, @"^.*[a-zA-Z]+.*$");
    }


    /// <summary>
    /// Does this string contain any hiragana
    /// </summary>
    public static bool containsHiragana(string str)
    {
      return Regex.IsMatch(str, @"^.*\p{IsHiragana}+.*$", RegexOptions.Compiled);
    }

    public static bool containsHiragana(char c)
    {
      return (c >= '\u3040') && (c <= '\u309F');
    }


    /// <summary>
    /// Does this string contain any katakana
    /// </summary>
    public static bool containsKatakana(string str)
    {
      return Regex.IsMatch(str, @"^.*\p{IsKatakana}+.*$", RegexOptions.Compiled);
    }

    public static bool containsKatakana(char c)
    {
      return (c >= '\u30A0') && (c <= '\u30FF');
    }


    /// <summary>
    /// Does this string contain an ideagraph (like a kanji)
    /// </summary>
    public static bool containsIdeograph(string str)
    {
      return Regex.IsMatch(str, @"^.*\p{IsCJKUnifiedIdeographs}+.*$", RegexOptions.Compiled);
    }

    public static bool containsIdeograph(char c)
    {
      return (c >= '\u4E00') && (c <= '\u9FBF');
    }

    /// <summary>
    /// Does this string contain any Unicode (like a kanji)
    /// </summary>
    public static bool containsUnicode(string str)
    {
      // Note: 0x7F = 127, so anything ASCII is OK
      return Regex.IsMatch(str, @"^.*[\u007F-\uFFFF-[\s\p{P}\p{IsGreek}\x85]]+.*$");
    }


    private static int[] kanaHalf = 
      { 
        0x3092, 0x3041, 0x3043, 0x3045, 0x3047, 0x3049, 0x3083, 0x3085, 
        0x3087, 0x3063, 0x30FC, 0x3042, 0x3044, 0x3046, 0x3048, 0x304A,
        0x304B, 0x304D, 0x304F, 0x3051, 0x3053, 0x3055, 0x3057, 0x3059,
        0x305B, 0x305D, 0x305F, 0x3061, 0x3064, 0x3066, 0x3068, 0x306A,
        0x306B, 0x306C, 0x306D, 0x306E, 0x306F, 0x3072, 0x3075, 0x3078,
        0x307B, 0x307E, 0x307F, 0x3080, 0x3081, 0x3082, 0x3084, 0x3086,
        0x3088, 0x3089, 0x308A, 0x308B, 0x308C, 0x308D, 0x308F, 0x3093
      };

    private static int[] kanaVoiced = 
      {
        0x30F4, 0xFF74, 0xFF75, 0x304C, 0x304E, 0x3050, 0x3052, 0x3054,
        0x3056, 0x3058, 0x305A, 0x305C, 0x305E, 0x3060, 0x3062, 0x3065,
        0x3067, 0x3069, 0xFF85, 0xFF86, 0xFF87, 0xFF88, 0xFF89, 0x3070,
        0x3073, 0x3076, 0x3079, 0x307C
      };

    private static int[] kanaSemiVoiced = { 0x3071, 0x3074, 0x3077, 0x307A, 0x307D };

    /// <summary>
    /// Convert half and full-width katakana to hiragana.
    /// Note: Katakana 'vu' is never converted to hiragana.
    /// This was routine adapted from Yomichan/Rikaichan.
    /// </summary>
    public static string convertKatakanaToHiragana(string word)
    {
      StringBuilder result = new StringBuilder("", 35);

      int ordPrev = 0;

      for (int i = 0; i < word.Length; i++)
      {
        char theChar = (char)word[i];
        int ordCurr = (int)theChar;

        //if (ordCurr <= 0x3000)
        //{
        //  // Break upon hitting non-japanese characters
        //  break;
        //}

        if ((ordCurr >= 0x30A1) && (ordCurr <= 0x30F3))
        {
          // Full-width katakana to hiragana
          ordCurr -= 0x60;
        }
        else if ((ordCurr >= 0xFF66) && (ordCurr <= 0xFF9D))
        {
          // Half-width katakana to hiragana
          ordCurr = kanaHalf[ordCurr - 0xFF66];
        }
        else if (ordCurr == 0xFF9E)
        {
          // Voiced (used in half-width katakana) to hiragana
          if ((ordPrev >= 0xFF73) && (ordPrev <= 0xFF8E))
          {
            result.Remove(result.Length - 1, 1);
            ordCurr = kanaVoiced[ordPrev - 0xFF73];
          }
        }
        else if (ordCurr == 0xFF9F)
        {
          // Semi-voiced (used in half-width katakana) to hiragana
          if ((ordPrev >= 0xFF8A) && (ordPrev <= 0xFF8E))
          {
            result.Remove(result.Length - 1, 1);
            ordCurr = kanaSemiVoiced[ordPrev - 0xFF8A];
          }
        }
        else if (ordCurr == 0xFF5E)
        {
          // Ignore Japanese ~
          ordPrev = 0;
          continue;
        }

        result.Append((char)ordCurr);
        ordPrev = (int)theChar;
      }

      return result.ToString();
    }



    /// <summary>
    /// Convert hiragana to full-width katakana.
    /// </summary>
    public static string convertHiraganaTotKatakana(string word)
    {
      string result = "";

      for (int i = 0; i < word.Length; i++)
      {
        char theChar = (char)word[i];
        int ordCurr = (int)theChar;

        if ((ordCurr >= 0x3041) && (ordCurr <= 0x3094)) // ぁ-ゔ
        {
          ordCurr += 0x60;
        }
        else if (ordCurr == 0x309E) // ゞ - HIRAGANA VOICED ITERATION MARK
        {
          ordCurr = 0x30FE; // ヾ
        }

        result += ((char)ordCurr).ToString();
      }

      return result;
    }


  }
}
