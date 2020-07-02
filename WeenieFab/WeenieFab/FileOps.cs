using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.TextFormatting;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace WeenieFab
{
    public partial class MainWindow
    {
        public void ReadSQLFile(string filepath)
        {
            // string headerblob = "";
            string wcidblob = "";
            string int32Blob = "";
            string int64Blob = "";
            string floatBlob = "";
            string boolBlob = "";
            string stringBlob = "";
            string didBlob = "";
            string attributeBlob = "";
            string attributeTwoBlob = "";
            string skillsBlob = "";
            string bodyPartBlob = "";
            string spellBookBlob = "";
            string emoteBlob = "";
            string line;

            // Regex Patterns
            var intPattern = @"\((\d+),\s*(\d+),\s*(-?\d+)\) \/\*(.*)\*\/*$";
            var boolPattern = @"\((\d+),\s*(\d+),\s*(\w+)\s*\)\s*\/\*\s*(.*)\s*\*\/*$";
            var floatPattern = @"\((\d+),\s*(\d+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+)\)\s*\/\*\s*(.*)\s*\*\/*$";
            var stringPattern = @"\((\d+),\s*(\d+),\s*'([a-zA-Z0-9_ ]*)'\)\s*\/\*\s*(.*)\s*\*\/.*$";
            var didPattern = @"\((\d+),\s*(\d+),\s*(-?\d+)\) \/\*(.*)\*\/*$";


            using (StreamReader sr = new StreamReader(filepath))
            {
                while ((line = sr.ReadLine()) != null) 
                {

                    if (line.Contains("INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)"))
                    {
                        wcidblob += sr.ReadLine();
                        string pattern = @"VALUES \s*\((\d+), '([\w -]*[a-zA-Z])?',\s*(\d+).*$";
                        var match = Regex.Match(wcidblob, pattern);
                        tbWCID.Text = match.Groups[1].ToString();
                        tbWeenieName.Text = match.Groups[2].ToString();
                        cbWeenieType.SelectedIndex = int.Parse(match.Groups[3].ToString());

                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)"))
                    {
                        
                        int32Blob = ReadBlob(sr);
                        integerDataTable = DecodeSql.DecodeThreeValuesInt(int32Blob,intPattern);
                        integerDataTable.AcceptChanges();
                        dgInt32.DataContext = integerDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)"))
                    {
                        int64Blob = ReadBlob(sr);
                        integer64DataTable = DecodeSql.DecodeThreeValuesInt(int64Blob,intPattern);
                        integer64DataTable.AcceptChanges();
                        dgInt64.DataContext = integerDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)"))
                    {
                        boolBlob = ReadBlob(sr);
                        boolDataTable = DecodeSql.DecodeThreeValuesBool(boolBlob, boolPattern);
                        boolDataTable.AcceptChanges();
                        dgBool.DataContext = boolDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)"))
                    {
                        floatBlob = ReadBlob(sr);
                        floatDataTable = DecodeSql.DecodeThreeValuesFloat(floatBlob, floatPattern);
                        floatDataTable.AcceptChanges();
                        dgFloat.DataContext = floatDataTable;

                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)"))
                    {
                        stringBlob = ReadBlob(sr);
                        stringDataTable = DecodeSql.DecodeThreeValuesString(stringBlob, stringPattern);
                        stringDataTable.AcceptChanges();
                        dgString.DataContext = stringDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_d_i_d`"))
                    {
                        didBlob = ReadBlob(sr);
                        didDataTable = DecodeSql.DecodeThreeValuesInt(didBlob, didPattern);
                        didDataTable.AcceptChanges();
                        dgDiD.DataContext = didDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_attribute`"))
                    {
                        attributeBlob = ReadBlob(sr);
                    }
                    else if (line.Contains("weenie_properties_attribute_2nd`"))
                    {
                        attributeTwoBlob = ReadBlob(sr);
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_skill`"))
                    {
                        skillsBlob = ReadBlob(sr);
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_body_part`"))
                    {
                        bodyPartBlob = ReadBlob(sr);
                        rtbBodyParts.Document.Blocks.Clear();
                        rtbBodyParts.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(bodyPartBlob)));
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_spell_book`"))
                    {
                        spellBookBlob = ReadBlob(sr);

                        spellDataTable = DecodeSql.DecodeThreeValuesFloat(spellBookBlob, floatPattern);
                        spellDataTable.AcceptChanges();
                        dgSpell.DataContext = spellDataTable;

                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_emote_action`"))
                    {
                        emoteBlob = ReadEmoteBlob(sr);
                        string tempES = "";
                        string[] emotes = emoteBlob.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        var es = EmoteScriptLib.Converter.sql2es(emotes);

                        foreach (var esline in es)
                        {
                            tempES += esline + "\r\n";
                        }

                        rtbEmoteScript.Document.Blocks.Clear();
                        //rtbEmoteScript.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(emoteBlob)));
                        rtbEmoteScript.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(tempES)));
                    }
                }
                sr.Close();
            }
        }


        public void WriteSQLFile(string filename)
        {
            string dateModified = string.Format("'{0:yyyy-MM-dd hh:mm:ss}'", DateTime.Now);
            string[] weenieTypeDescription = cbWeenieType.Text.Split(" ");

            // Header
            string header = $"DELETE FROM `weenie` WHERE `class_Id` = {tbWCID.Text} \n\n";
            string body = $"DELETE FROM `weenie` WHERE `class_Id` = {tbWCID.Text} \n\n";

            // WeenieType
            body += $"INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)\n";
            body += $"VALUES ({tbWCID.Text}, '{tbWeenieName.Text}', {cbWeenieType.SelectedIndex}, {dateModified}) /* {weenieTypeDescription[1]} */;\n\n";

            // Integers
            header = $"INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertTriValueTable(integerDataTable, tbWCID.Text, header);
            dgInt32.ItemsSource = integerDataTable.DefaultView;

            header = $"INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertTriValueTable(integer64DataTable, tbWCID.Text, header);

            // Boolean
            header = $"INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertTriValueTable(boolDataTable, tbWCID.Text, header);

            // Float
            header = $"INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertTriValueTable(floatDataTable, tbWCID.Text, header);

            // String
            header = $"INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertTriValueTable(didDataTable, tbWCID.Text, header);

            // Body Parts
            header = $"INSERT INTO `weenie_properties_body_part` (`object_Id`, `key`, `d_Type`, `d_Val`, `d_Var`, `base_Armor`, `armor_Vs_Slash`, `armor_Vs_Pierce`, `armor_Vs_Bludgeon`, `armor_Vs_Cold`, `armor_Vs_Fire`, `armor_Vs_Acid`, `armor_Vs_Electric`, `armor_Vs_Nether`, `b_h`, `h_l_f`, `m_l_f`, `l_l_f`, `h_r_f`, `m_r_f`, `l_r_f`, `h_l_b`, `m_l_b`, `l_l_b`, `h_r_b`, `m_r_b`, `l_r_b`)";            
            string richTextBoxContents = new TextRange(rtbBodyParts.Document.ContentStart, rtbBodyParts.Document.ContentEnd).Text;
            body += TableToSql.ConvertBodyTable(richTextBoxContents, tbWCID.Text, header);
            File.WriteAllText(filename, body);

            // Emotes


        }

        public static string ReadBlob(StreamReader sr)
        {
            string blob = "";
            string line;
            string tLine;

            while ((line = sr.ReadLine()) != null) 
            {
                if (line == "" || line == "\t" || line =="\r\n")                  
                    return blob;
                else
                {
                    tLine = line;
                    tLine = tLine.Replace("VALUES ", "");
                    tLine = tLine.Replace("     , ", "");
                    blob += tLine +"\r\n";
                }
            }
            // Test to see if this removes the extra blank - Nope, need to revert;
            // string finalblob = Regex.Replace(blob, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            return blob;
        }

        public static string ReadEmoteBlob(StreamReader sr)
        {
            string blob = "";
            string line;
            // string tLine;

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("INSERT INTO `weenie_properties_create_list`"))
                    return blob;
                else
                {
                    blob += line + "\r\n";
                }
            }
            // Test to see if this removes the extra blank - Nope, need to revert;
            
            return blob;
        }
    }
}
