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
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace JapaneseTextAnalysisTool
{
  public class JParser
  {
    /// <summary>
    /// Get RAW JParser output.
    /// </summary>
    public static string getRaw(string input)
    {
      string parsedText = "";
      string jParserLoc = String.Format("{0}JParser{1}jparser.exe", UtilsCommon.getAppDir(true), Path.DirectorySeparatorChar);
      string jParserInputFile = String.Format("{0}JParser{1}jparser_in_{2}.txt", UtilsCommon.getAppDir(true), Path.DirectorySeparatorChar, Guid.NewGuid());
      string jParserOutputFile = String.Format("{0}JParser{1}jparser_out_{2}.txt", UtilsCommon.getAppDir(true), Path.DirectorySeparatorChar, Guid.NewGuid());

      // Delete old output file
      File.Delete(jParserOutputFile);

      // Write input file without BOM
      StreamWriter writer = new StreamWriter(jParserInputFile, false, new UTF8Encoding(true));
      writer.Write(input);
      writer.Close();

      // Create the jParser arguments
      string jParserArgs = String.Format(@"{0} {1}",
        jParserInputFile, jParserOutputFile);

      // Run jParser
      Process proc = new Process();
      proc.StartInfo.FileName = jParserLoc;
      proc.StartInfo.Arguments = jParserArgs;
      proc.StartInfo.UseShellExecute = false;
      proc.StartInfo.CreateNoWindow = true;
      proc.StartInfo.WorkingDirectory = UtilsCommon.getAppDir(true) + "JParser";
      proc.Start();
      proc.WaitForExit(); // Blocking

      // Read the output of jParser
      if (File.Exists(jParserOutputFile))
      {
        StreamReader reader = new StreamReader(jParserOutputFile);
        parsedText = reader.ReadToEnd();
        reader.Close();
      }

      File.Delete(jParserInputFile);
      File.Delete(jParserOutputFile);

      return parsedText;
    }


    /// <summary>
    /// Parse the fields of the JParser output.
    /// </summary>
    public static List<InfoJParser> parseFields(string input)
    {
      List<InfoJParser> jParserList = new List<InfoJParser>();

      string result = JParser.getRaw(input);

      string[] lines = result.Split(new char[] { '\n' });

      foreach (string line in lines)
      {
        string[] fields = line.Split(new char[] { '\t' }, StringSplitOptions.None);

        if (fields.Length != 6)
        {
          break;
        }

        InfoJParser info = new InfoJParser();

        info.Word = fields[0];
        info.Reading = fields[1];
        info.Deinflected = fields[2];
        info.Conjugation = fields[3];
        info.Definition = fields[4];
        info.Pos = Convert.ToInt32(fields[5].TrimEnd());

        jParserList.Add(info);
      }

      return jParserList;
    }


  }
}
