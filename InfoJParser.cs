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
  public class InfoJParser
  {
    /// <summary>
    /// The word. In inflected form (if applicable).
    /// </summary>
    public string Word { get; set; }

    /// <summary>
    /// The reading of the word. Blank if word in all kana.
    /// </summary>
    public string Reading { get; set; }

    /// <summary>
    /// Deinflected or kanjified form of the word.
    /// </summary>
    public string Deinflected { get; set; }

    /// <summary>
    /// Conjugation rule.
    /// </summary>
    public string Conjugation { get; set; }

    /// <summary>
    /// EDICT definition.
    /// </summary>
    public string Definition { get; set; }

    /// <summary>
    /// 0 = Don't know, 6 = Particle, 8 = new line
    /// </summary>
    public int Pos { get; set; } 
  }
}
