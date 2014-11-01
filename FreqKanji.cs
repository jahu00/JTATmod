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
  public class FreqKanji : Freq
  {
    public FreqKanji(string outFilename)
      : base(outFilename)
    {

    }


    public override void addFileText(string text)
    {
      for (int i = 0; i < text.Length; i++)
      {
        if(UtilsLang.containsIdeograph(text[i]))
        {
          addItemToFreqTable(text[i].ToString(), "");
        }
      }
    }








  }
}
