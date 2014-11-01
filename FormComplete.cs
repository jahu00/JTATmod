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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JapaneseTextAnalysisTool
{
  public partial class FormComplete : Form
  {
    public string OutputDir { get; set; }


    public FormComplete(string completeMsg, string outputDir)
    {
      InitializeComponent();

      this.textBoxCompleteMsg.Text = completeMsg;
      this.OutputDir = outputDir;
    }


    public void removeRef()
    {
      this.textBoxRef.Visible = false;
      this.Height = 125;
    }


    private void buttonOpenOutputDir_Click(object sender, EventArgs e)
    {
      if (Directory.Exists(this.OutputDir))
      {
        Process.Start(String.Format(@"""{0}""", this.OutputDir));
      }

      this.Close();
    }


  }
}
