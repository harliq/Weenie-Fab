using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.TextFormatting;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace WeenieFab
{
    public partial class MainWindow
    {
        public void ReadSQLFile(string filepath)
        {
            string headerblob = "";
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
            string line;

            // Regex Patterns
            var intPattern = @"\((\d+),\s*(\d+),\s*(-?\d+)\) \/\*(.*)\*\/*$";
            var boolPattern = @"\((\d+),\s*(\d+),\s*(\w+)\s*\)\s*\/\*\s*(.*)\s*\*\/*$";
            var floatPattern = @"\((\d+),\s*(\d+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+)\)\s*\/\*\s*(.*)\s*\*\/*$";
            var stringPattern = @"\((\d+),\s*(\d+),\s*'([a-zA-Z0-9_ ]*)'\)\s*\/\*\s*(.*)\s*\*\/.*$";
            var didPattern = @"\((\d+),\s*(\d+),\s*(-?\d+)\) \/\*(.*)\*\/*$";


            using (StreamReader sr = new StreamReader(filepath))
            {
                while ((line = sr.ReadLine()) != null) // Whatever logic you have
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
                        //int32Blob = ReadInt(sr);
                        int32Blob = ReadBlob(sr);
                        integerDataTable = DecodeSql.DecodeInt32(int32Blob);
                        integerDataTable.AcceptChanges();
                        dgInt32.DataContext = integerDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)"))
                    {
                        int64Blob = ReadBlob(sr);
                        integer64DataTable = DecodeSql.DecodeThreeValues(int64Blob,intPattern);
                        integer64DataTable.AcceptChanges();
                        dgInt64.DataContext = integerDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)"))
                    {
                        boolBlob = ReadBlob(sr);
                        boolDataTable = DecodeSql.DecodeThreeValues(boolBlob, boolPattern);
                        boolDataTable.AcceptChanges();
                        dgBool.DataContext = boolDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)"))
                    {
                        floatBlob = ReadBlob(sr);
                        floatDataTable = DecodeSql.DecodeThreeValues(floatBlob, floatPattern);
                        floatDataTable.AcceptChanges();
                        dgFloat.DataContext = floatDataTable;

                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)"))
                    {
                        stringBlob = ReadBlob(sr);
                        stringDataTable = DecodeSql.DecodeThreeValues(stringBlob, stringPattern);
                        stringDataTable.AcceptChanges();
                        dgString.DataContext = stringDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_d_i_d`"))
                    {
                        didBlob = ReadBlob(sr);
                        didDataTable = DecodeSql.DecodeThreeValues(didBlob, didPattern);
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

            // WeenieType
            header += $"INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)\n";
            header += $"VALUES ({tbWCID.Text}, '{tbWeenieName.Text}', {cbWeenieType.SelectedIndex}, {dateModified}) /* {weenieTypeDescription[1]} */;\n\n";


            header += $"INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)\n";
            header += TableToSql.ConvertTable(integerDataTable, tbWCID.Text);


            File.WriteAllText(filename, header);

        }

        public static string ReadBlob(StreamReader sr)
        {
            string blob = "";
            string line;
            string tLine;

            while ((line = sr.ReadLine()) != null) // Whatever logic you have
            {
                if (line == "" || line == "\t")
                    //if (line is null)
                    return blob;
                else
                {
                    tLine = line;
                    tLine = tLine.Replace("VALUES ", "");
                    tLine = tLine.Replace("     , ", "");
                    blob += tLine +"\r\n";
                }
            }
            return blob;
        }
    }
}
