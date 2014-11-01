//  Copyright (C) 2012 Christopher Brochtrup
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
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace JapaneseTextAnalysisTool
{

  // --node-format options:
  //
  //   %s:  Type of morpheme (0=normal 1=unknown 2=beginning of sentence 3=end of sentence)
  //   %S:  Input sentence.
  //   %L:  Length of input sentence.
  //   %m:  morpheme
  //   %M:  Surface value, including any whitespace
  //   %h:  ID? Some number.
  //   %c:  cost? Some number.
  //   %H:  日本	[名詞,固有名詞,地域,国,*,*,日本,ニッポン,ニッポン] The stuff in the brackets
  //   %t:  ??? Some number.
  //   %P:  Always zeroes
  //   %pi: ID of morpheme.
  //   %pS: ????
  //   %ps: ???? beginning location in bytes?
  //   %pe: ???? ending location in bytes?
  //   %pC: ???? Some number.
  //   %pw: ???? Some number.
  //   %pc: ???? Some number.
  //   %pn: ???? Some number.
  //   %pb: ???? Always *
  //   %pP: ???? Some number, Always 0.00000.
  //   %pA: ???? Some number, Always 0.00000.
  //   %pB: ???? Some number, Always 0.00000.
  //   %pl: Length of word (divide by 3?)
  //   %pL: Length of word (divide by 3?)
  //   %phl: ???? Some number.
  //   %phr: ???? Some number.
  //   %f[N]:
  //   %f[N1,N2,N3...]: - Use multiple items
  //   %FC[N1,N2,N3...]: - Use multople items seperated by C (ex %F\t[0,2,3])
  //     Ex. 歩いた
  //     %f[0]: Part of speech (動詞)
  //     %f[1]: Part of speech, subtype 1 (自立)
  //     %f[2]: Part of speech, subtype 2 (*) <--- don't care
  //     %f[3]: Part of speech, subtype 3 (*) <--- don't care
  //     %f[4]: Rule - how this word is conjugated (五段・カ行イ音便)
  //     %f[5]: Conj - what conjugation it’s in  (連用タ接続)
  //     %f[6]: Dictionary/root form of word (歩く)
  //     %f[7]: Readingin - katakana (アルイ) <-- Use this reading
  //     %f[8]: Rronunciation - the reading, in katakana with the long-vowel marker  (アルイ)
  // Escapes: \0 \a \b \t \n \v \f \r \\ \s
  //
  // ---------------------------------------------------------------------------
  //
  // http://dotclue.org/archives/003720.html
  // 形容詞
  //     i-adjective; pos1 can be:
  // 
  //         自立 - independent adjective; if conj is ガル接続, just add an い, otherwise chop off any of き, く, くっ, かっ, けれ, し, or っ, and then add an い.
  //         非自立 includes いい, よかっ, にくい, ほしい, やすい, etc. Use reading as-is.
  //         接尾 consists entirely of っぽ and ぽ, as far as I can tell. Use reading as-is. 
  // 
  // 動詞
  //     verb; pos1 can be:
  // 
  //         非自立 includes て, いる, てる, くる, いく, しまう, ください, etc. Use reading as-is.
  //         接尾 includes れる, られる, そう, させ, しめ, 的, etc. Use reading as-is.
  //         自立 - the good stuff. If conj is 基本形, we’re already in dictionary form, and can declare victory. Otherwise, we need to look at rule:
  //             一段 - ichidan or “ru-dropping” verbs. Just drop the last ra-line character from reading if conj is not 連用形 or 未然形, and then add a ru.
  //             一段・クレル - kureru. We’re done.
  //             カ変・クル - kuru, possibly preceded by something like yatte. Replace everything after the -tte with kuru.
  //             カ変・来ル - kuru again, this time with kanji in surface. ditto.
  //             サ変・スル - suru. We’re done.
  //             サ変・－スル - something-suru; chop off the last sa-line character and add suru.
  //             五段・ワ行… There are a lot of these, but all you need to know is the third character, which I’ve helpfully marked in bold. MeCab has done all the hard work, so here’s all we have to do: chop off the last character of reading and replace it with the indicated line’s -u. So, ラ means る, マ means む, and don’t forget that ワ means う. Note that if there’s only one character in reading, just append rather than chopping. 
  // 
  // 助動詞
  //     auxiliary verb (ex: なる, ます, たい, べき, なく, らしい, etc)
  // EOS
  //     sentence-division marker (no text)
  // 副詞
  //     adverb; two known values for pos1:
  // 
  //         一般 includes まるで, もっと, よく, やがて, やはり, あっという間に, etc.
  //         助詞類接続 includes まったく, 少し, 必ず, ちょっと, etc. 
  // 
  // 助詞
  //     particle/postposition
  //     pos1 = 接続助詞 are connectors like the て in (verb)てくる
  // 
  // 名詞
  //     noun; common pos1 are:
  // 
  //         数 includes kanji/roman digits, 百, 千, 億, 何 as なん, ・, and 数
  //         接尾 suffix; includes センチ, どおり, だらけ, いっぱい, そう, ごろ, 人/じん, 内, 別, 名/めい, 君/くん, etc. pos2 further classifies 一般, 人名, 助数詞, 副詞可能, etc
  //         特殊 special; I’ve found only そ and そう
  //         代名詞 pronoun; 何, 俺, 僕, 君, 私, これ, だれ, みんな, etc
  //         非自立 not independent; 上, くせ, の, ん, 事, 筈/はず, etc. pos2 further classifies 一般, 副詞可能, 助動詞語幹, 形容動詞語幹
  //         サ変接続 v-suru (kanji/katakana); if reading is “*”, it’s random ascii
  //         副詞可能 adverb form; includes 今, あと, ほか, 今夜, 一番
  //         固有名詞 proper noun; if reading is “*”, foreign abbrev. or katakana word
  //         接続詞的 conjunction; consists exclusively of 対/たい in my samples
  //         動詞非自立的 includes ちょうだい, ごらん, and not much else
  //         形容動詞語幹 -na adjective stem
  //         ナイ形容詞語幹 pre-nai adj stem (しょうが, とんでも, 違い, etc) 
  // 
  // 記号
  //     symbol (ex: 、。・, full-width alphabetic, etc)
  // 
  // 接続詞
  //     conjunction (ex: そして, でも, たとえば, だから, 次に, 実は, etc)
  // 
  // 連体詞
  //     pre-noun adjective (ex: あの, こんな, 小さな, 同じ, ある, 我が, etc)
  // 
  // 感動詞
  //     interjection (ex: さあ, ええ, はい, どうぞ, なるほど, etc)
  // 
  // 接頭詞
  //     prefix (ex: お, ご, 全, 大, 真っ, 逆, 両, 最, 新, 悪, 初, etc)
  // 
  // フィラー
  //     filler word (なんか, あの, ええと, etc)


  // http://stackoverflow.com/questions/6365931/trying-to-get-libmecab-dll-mecab-to-work-with-c-sharp

  /// <summary>
  /// Used to parse a string with Mecab.
  /// </summary>
  public class Mecab
  {
    [DllImport("libmecab.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    //private extern static IntPtr mecab_new2(string arg); // Doesn't work
    private extern static IntPtr mecab_new2(byte[] str);
    [DllImport("libmecab.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private extern static IntPtr mecab_sparse_tostr(IntPtr m, byte[] str);
    [DllImport("libmecab.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private extern static void mecab_destroy(IntPtr m);


    /// <summary>
    /// Parse the provided input with the provided arguments. 
    /// </summary>
    public static string parse(string input, bool userDic)
    {
      string mecabUserDicLoc = String.Format(".{0}Mecab{0}mecab_user_dic.dic", Path.DirectorySeparatorChar);
      string mecabDicLoc = String.Format(".{0}Mecab{0}dic{0}ipadic", Path.DirectorySeparatorChar);
      string mecabRcFile = String.Format(".{0}Mecab{0}etc{0}mecabrc", Path.DirectorySeparatorChar);
      string mecabUserDicArg = "";

      if (userDic)
      {
        mecabUserDicArg = String.Format(@"-u {0}", mecabUserDicLoc);
      }

      string mecabArgs = String.Format(@"-r {0} {1} -d {2}",
        mecabRcFile,
        mecabUserDicArg,
        mecabDicLoc);

      // For some reason not all args work (for examaple -d)
      IntPtr mecab = mecab_new2(Encoding.UTF8.GetBytes(mecabArgs));
      IntPtr nativeStr = mecab_sparse_tostr(mecab, Encoding.UTF8.GetBytes(input));

      // Check parse failure
      if (nativeStr.ToInt32() == 0)
      {
        return "";
      }

      int size = nativeArraySize(nativeStr) - 1;
      byte[] data = new byte[size];
      Marshal.Copy(nativeStr, data, 0, size);

      mecab_destroy(mecab);

      string result = Encoding.UTF8.GetString(data);
      result = result.Replace("\n", System.Environment.NewLine);

      return result;
    }


    private static int nativeArraySize(IntPtr ptr)
    {
      int size = 0;
      while (Marshal.ReadByte(ptr, size) > 0)
        size++;

      return size;
    }


    /// <summary>
    /// Use input using mecab.exe.
    /// </summary>
    public static string parseWithExe(string input, bool userDic)
    {
      string parsedText = "";
      string mecabLoc = String.Format("{0}Mecab{1}bin{1}mecab.exe", UtilsCommon.getAppDir(true), Path.DirectorySeparatorChar);
      string mecabUserDicLoc = String.Format("{0}Mecab{1}mecab_user_dic.dic", UtilsCommon.getAppDir(true), Path.DirectorySeparatorChar);
      string mecabInputFile = String.Format("{0}Mecab{1}mecab_in_{2}.txt", UtilsCommon.getAppDir(true), Path.DirectorySeparatorChar, Guid.NewGuid());
      string mecabOutputFile = String.Format("{0}Mecab{1}mecab_out_{2}.txt", UtilsCommon.getAppDir(true), Path.DirectorySeparatorChar, Guid.NewGuid());
      string mecabDicLoc = String.Format("{0}Mecab{1}dic{1}ipadic", UtilsCommon.getAppDir(true), Path.DirectorySeparatorChar);
      string mecabRcFile = String.Format("{0}Mecab{1}etc{1}mecabrc", UtilsCommon.getAppDir(true), Path.DirectorySeparatorChar);
      string mecabUserDicArg = "";

      File.Delete(mecabOutputFile);

      // Write mecab input file without BOM
      using (StreamWriter writer = new StreamWriter(mecabInputFile, false, new UTF8Encoding(false)))
      {
        writer.Write(input);
      }

      if (userDic)
      {
        mecabUserDicArg = String.Format(@"-u ""{0}""", mecabUserDicLoc);
      }

      // Mecab the file
      string mecabArgs = String.Format(@"-r {0} {1} -d ""{2}"" -o ""{3}"" ""{4}""",
        mecabRcFile,
        mecabUserDicArg,
        mecabDicLoc,
        mecabOutputFile,
        mecabInputFile);

      Process mecabProc = new Process();
      mecabProc.StartInfo.FileName = mecabLoc;
      mecabProc.StartInfo.Arguments = mecabArgs;
      mecabProc.StartInfo.UseShellExecute = false;
      mecabProc.StartInfo.CreateNoWindow = true;
      mecabProc.Start();
      mecabProc.WaitForExit(); // Blocking

      if (File.Exists(mecabOutputFile))
      {
        using (StreamReader reader = new StreamReader(mecabOutputFile))
        {
          parsedText = reader.ReadToEnd();
        }
      }

      File.Delete(mecabInputFile);
      File.Delete(mecabOutputFile);

      return parsedText;
    }


    /// <summary>
    /// Parse out the useful fields of the mecab output
    /// </summary>
    public static List<InfoMecab> parseFields(string input, bool userDic)
    {
      List<InfoMecab> mecabList = new List<InfoMecab>();

      //string result = Mecab.parse(input, userDic);
      string result = parseWithExe(input, userDic);

      string[] lines = result.Split(new char[] { '\n' });

      foreach (string line in lines)
      {
        string[] fields = line.TrimEnd().Split(new char[] { ',', '\t' });

        InfoMecab infoMecab = new InfoMecab();

        if (fields.Length > 0)
        {
          infoMecab.Word = fields[0];
        }

        if (fields.Length > 4)
        {
          infoMecab.Pos = String.Format("{0},{1},{2},{3}", fields[1], fields[2], fields[3], fields[4]);
        }

        if (fields.Length > 7)
        {
          infoMecab.Root = fields[7];
        }

        if (fields.Length > 8)
        {
          infoMecab.Reading = UtilsLang.convertKatakanaToHiragana(fields[8]);
        }

        mecabList.Add(infoMecab);
      }

      if (mecabList[mecabList.Count - 1].Word == "EOS")
      {
        mecabList.RemoveAt(mecabList.Count - 1);
      }

      return mecabList;
    }





  }

}
