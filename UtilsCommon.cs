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
using System.Text;
using System.Windows.Forms;

namespace JapaneseTextAnalysisTool
{
  public class UtilsCommon
  {
    /// <summary>
    /// Get the directory that the executable resides in.
    /// </summary>
    public static string getAppDir(bool addSlash)
    {
      string appDir = Application.ExecutablePath.Substring(
        0, Application.ExecutablePath.LastIndexOf(Path.DirectorySeparatorChar));

      if(addSlash)
      {
        appDir += Path.DirectorySeparatorChar;
      }

      return appDir;
    }


    /// <summary>
    /// Get the line ending type: CRLF, LF, or CR
    /// </summary>
    public static string getLineEnding(string text)
    {
      string lineEnding = "";

      if (text.Contains("\r\n"))
      {
        lineEnding = "\r\n";
      }
      else if (text.Contains("\n"))
      {
        lineEnding = "\n";
      }
      else
      {
        lineEnding = "\r";
      }

      return lineEnding;
    }


    /// <summary>
    ///  Get a list of non-hidden files in a directory that match the given file pattern.
    /// </summary>
    public static string[] getNonHiddenFilesInDir(string dir, string searchPatttern)
    {
      if (Directory.Exists(dir))
      {
        string[] subsFiles = Directory.GetFiles(dir, searchPatttern, SearchOption.TopDirectoryOnly);
        List<string> unHiddenFiles = new List<string>();

        foreach (string file in subsFiles)
        {
          if ((File.GetAttributes(file) & FileAttributes.Hidden) != FileAttributes.Hidden)
          {
            unHiddenFiles.Add(file);
          }
        }

        return unHiddenFiles.ToArray();
      }
      else
      {
        return new string[0];
      }
    }


    /// <summary>
    ///  Get a list of non-hidden files in a directory.
    /// </summary>
    public static string[] getNonHiddenFilesInDir(string dir)
    {
      return getNonHiddenFilesInDir(dir, "*");
    }


  }
}
