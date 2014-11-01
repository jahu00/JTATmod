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
  public class InfoFormulaReadability
  {
    /// <summary>
    /// The file that is being analzyed for readability.
    /// </summary>
    public string Filename { get; set; }


    /// <summary>
    /// The Hayashi readability score. Higher scores imply that the work is more readable/easier.
    /// </summary>
    public double HayashiScore { get; set; }


    /// <summary>
    /// 1-6:   Elementary school (6 years)
    /// 7-9:   Junior high school (3 years)
    /// 10-12: High school (3 years)
    /// 13-:   Beyond high school
    /// </summary>
    public int Obi2Score { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    public InfoFormulaReadability(string filename, double hayashiScore, int obi2Score)
    {
      this.Filename = filename;
      this.HayashiScore = hayashiScore;
      this.Obi2Score = obi2Score;
    }



  }
}
