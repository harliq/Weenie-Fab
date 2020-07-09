using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.TextFormatting;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using Microsoft.Win32;
using System.ComponentModel;
using WeenieFab.Properties;
using EmoteScriptLib;

namespace WeenieFab
{
    public partial class MainWindow
    {
        public void OpenFile() 
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Weenie File";
            ofd.Filter = "SQL files|*.sql";
            ofd.InitialDirectory = WeenieFabUser.Default.DefaultSqlPath;
            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                ClearAllDataTables();
                //ClearAllDataGrids();
                ClearAllFields();
                ResetIndexAllDataGrids();
                ReadSQLFile(ofd.FileName);
                
            }
        }

        public void SaveFile()
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Weenie File";
            sfd.Filter = "SQL files|*.sql";
            sfd.FileName = tbWCID.Text + $".sql";
            sfd.InitialDirectory = WeenieFabUser.Default.DefaultSqlPath;

            Nullable<bool> result = sfd.ShowDialog();

            if (result == true)
            {
                WriteSQLFile(sfd.FileName);
            }
        }
        
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
            string createListBlob = "";
            string line;

            // Regex Patterns
            var intPattern = @"\((\d+),\s*(\d+),\s*(-?\d+)\) \/\*(.*)\*\/*.*$";
            var boolPattern = @"\((\d+),\s*(\d+),\s*(\w+)\s*\)\s*\/\*\s*(.*)\s*\*\/*.*$";
            var floatPattern = @"\((\d+),\s*(\d+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+)\)\s*\/\*\s*(.*)\s*\*\/*.*$"; // Spells also uses same pattern.
            var stringPattern = @"\((\d+),\s*(\d+),\s*'([a-zA-Z0-9_ .!?]*)'\)\s*\/\*\s*(.*)\s*\*\/.*.*$";
            var didPattern = @"\((\d+),\s*(\d+),\s*(-?\d+)\) \/\*(.*)\*\/*.*$";
            var attribPattern = @"\((\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+)\) \/\*(.*)\*\/*.*$";
            var attrib2Pattern = @"\((\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+)\) \/\*(.*)\*\/*.*$";
            var skillsPattern = @"\((\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+)\) \/\*(.*)\*\/*.*$";
            var createListPattern = @"\((\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([a-zA-Z0-9_ ]*)\) \/\*(.*)\*\/*.*$";

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
                        integerDataTable = ResortDataTable(integerDataTable, "Property", "ASC");
                        dgInt32.DataContext = integerDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)"))
                    {
                        int64Blob = ReadBlob(sr);
                        integer64DataTable = DecodeSql.DecodeThreeValuesInt(int64Blob,intPattern);
                        integer64DataTable.AcceptChanges();
                        integer64DataTable = ResortDataTable(integer64DataTable, "Property", "ASC");
                        dgInt64.DataContext = integer64DataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)"))
                    {
                        boolBlob = ReadBlob(sr);
                        boolDataTable = DecodeSql.DecodeThreeValuesBool(boolBlob, boolPattern);
                        boolDataTable.AcceptChanges();
                        boolDataTable = ResortDataTable(boolDataTable, "Property", "ASC");
                        dgBool.DataContext = boolDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)"))
                    {
                        floatBlob = ReadBlob(sr);
                        floatDataTable = DecodeSql.DecodeThreeValuesFloat(floatBlob, floatPattern);
                        floatDataTable.AcceptChanges();
                        floatDataTable = ResortDataTable(floatDataTable, "Property", "ASC");
                        dgFloat.DataContext = floatDataTable;

                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)"))
                    {
                        stringBlob = ReadBlob(sr);
                        stringDataTable = DecodeSql.DecodeThreeValuesString(stringBlob, stringPattern);
                        stringDataTable.AcceptChanges();
                        stringDataTable = ResortDataTable(stringDataTable, "Property", "ASC");
                        dgString.DataContext = stringDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_d_i_d`"))
                    {
                        didBlob = ReadBlob(sr);
                        didDataTable = DecodeSql.DecodeThreeValuesInt(didBlob, didPattern);
                        didDataTable.AcceptChanges();
                        didDataTable = ResortDataTable(didDataTable, "Property", "ASC");
                        dgDiD.DataContext = didDataTable;
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_attribute`"))
                    {
                        attributeBlob = ReadBlob(sr);

                        attributeDataTable = DecodeSql.DecodeAttribute(attributeBlob, attribPattern);
                        attributeDataTable.AcceptChanges();
                        dgAttributes.DataContext = attributeDataTable;
                        updateAttribs();
                    }
                    else if (line.Contains("weenie_properties_attribute_2nd`"))
                    {
                        attributeTwoBlob = ReadBlob(sr);
                        attribute2DataTable = DecodeSql.DecodeAttributeTwo(attributeTwoBlob, attrib2Pattern);
                        attribute2DataTable.AcceptChanges();
                        dgAttributesTwo.DataContext = attribute2DataTable;
                        updateAttribs2();
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_skill`"))
                    {
                        skillsBlob = ReadBlob(sr);

                        skillsDataTable = DecodeSql.DecodeSkills(skillsBlob, skillsPattern);
                        skillsDataTable.AcceptChanges();
                        dgSkills.DataContext = skillsDataTable;
                        // dgSkills.Items.Refresh();
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
                        spellDataTable = ResortDataTable(spellDataTable, "Property", "ASC");
                        dgSpell.DataContext = spellDataTable;

                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_emote_action`") || line.Contains("INSERT INTO `weenie_properties_emote`"))
                    {
                        // emoteBlob = ReadEmoteBlob(sr);
                        ReadEmoteCreateListBlob(line, sr, out emoteBlob, out createListBlob);
                        createListDataTable = DecodeSql.DecodeCreateList(createListBlob, createListPattern);
                        createListDataTable.AcceptChanges();
                        dgCreateItems.DataContext = createListDataTable;
                        dgCreateItems.Items.Refresh();

                        string tempES = "";
                        string[] emotes = emoteBlob.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        var es = EmoteScriptLib.Converter.sql2es(emotes);

                        foreach (var esline in es)
                        {
                            tempES += esline + "\r\n";
                        }

                        rtbEmoteScript.Document.Blocks.Clear();
                        rtbEmoteScript.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(tempES)));
                    }
                    //else if (line.Contains("INSERT INTO `weenie_properties_create_list`"))
                    //{
                    //    createListBlob = ReadBlob(sr);

                    //    createListDataTable = DecodeSql.DecodeThreeValuesFloat(createListBlob, createListPattern);
                    //    createListDataTable.AcceptChanges();
                    //    // createListDataTable = ResortDataTable(spellDataTable, "Property", "ASC");
                    //    dgCreateItems.DataContext = createListDataTable;

                    //}
                }

                if (WeenieFabUser.Default.AutoLoadESFiles == true)
                {
                    string esfile = WeenieFabUser.Default.DefaultESPath + @"\" + tbWCID.Text +".es";
                    if (File.Exists(esfile) == true)
                    {
                        string esdata = File.ReadAllText(esfile);
                        rtbEmoteScript.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(esdata)));
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
            string body = $"DELETE FROM `weenie` WHERE `class_Id` = {tbWCID.Text}; \n\n";

            // WeenieType
            body += $"INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)\n";
            body += $"VALUES ({tbWCID.Text}, '{tbWeenieName.Text}', {cbWeenieType.SelectedIndex}, {dateModified}) /* {weenieTypeDescription[1]} */;\n\n";

            // Integer32
            header = $"INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertTriValueTable(integerDataTable, tbWCID.Text, header);

            // Integer64
            header = $"INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertTriValueTable(integer64DataTable, tbWCID.Text, header);

            // Boolean
            header = $"INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertTriValueTable(boolDataTable, tbWCID.Text, header);

            // Float
            header = $"INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertTriValueTable(floatDataTable, tbWCID.Text, header);

            // String
            header = $"INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertStringTable(stringDataTable, tbWCID.Text, header);

            // DiD
            header = $"INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertTriValueTable(didDataTable, tbWCID.Text, header);

            // Attributes
            header = $"INSERT INTO `weenie_properties_attribute` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`)";
            body += TableToSql.ConvertAttributeTable(attributeDataTable, tbWCID.Text, header);

            // Attributes 2 - Health, Stam, Mana
            header = $"INSERT INTO `weenie_properties_attribute_2nd` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`, `current_Level`)";
            body += TableToSql.ConvertAttribute2Table(attribute2DataTable, tbWCID.Text, header);

            // Skills
            header = $"INSERT INTO `weenie_properties_skill` (`object_Id`, `type`, `level_From_P_P`, `s_a_c`, `p_p`, `init_Level`, `resistance_At_Last_Check`, `last_Used_Time`)";
            body += TableToSql.ConvertSkillsTable(skillsDataTable, tbWCID.Text, header);

            // Body Parts
            header = $"INSERT INTO `weenie_properties_body_part` (`object_Id`, `key`, `d_Type`, `d_Val`, `d_Var`, `base_Armor`, `armor_Vs_Slash`, `armor_Vs_Pierce`, `armor_Vs_Bludgeon`, `armor_Vs_Cold`, `armor_Vs_Fire`, `armor_Vs_Acid`, `armor_Vs_Electric`, `armor_Vs_Nether`, `b_h`, `h_l_f`, `m_l_f`, `l_l_f`, `h_r_f`, `m_r_f`, `l_r_f`, `h_l_b`, `m_l_b`, `l_l_b`, `h_r_b`, `m_r_b`, `l_r_b`)";            
            string bodyparts = new TextRange(rtbBodyParts.Document.ContentStart, rtbBodyParts.Document.ContentEnd).Text;
            if (bodyparts != "")         
                body += TableToSql.ConvertBodyTable(bodyparts, tbWCID.Text, header);

            // Spells
            header = $"INSERT INTO `weenie_properties_spell_book` (`object_Id`, `spell`, `probability`)";
            body += TableToSql.ConvertSpellTable(spellDataTable, tbWCID.Text, header);

            // Emotes
            string tempES = new TextRange(rtbEmoteScript.Document.ContentStart, rtbEmoteScript.Document.ContentEnd).Text;
            string[] saES = tempES.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int tWCID = ConvertToInteger(tbWCID.Text);
            string finalEmotes = "";

                // var eslist = EmoteScriptLib.Converter.es2sql(saES, ConvertToInteger(tbWCID.Text));
            var eslist = EmoteScriptLib.Converter.es2sql(saES, (uint)tWCID);

            foreach (var emoteline in eslist)
            {
                finalEmotes += emoteline + "\r\n";
            }

            body += finalEmotes;

            // Create Items
            header = $"INSERT INTO `weenie_properties_create_list` (`object_Id`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`)";
            body += TableToSql.ConvertCreateItemsTable(createListDataTable, tbWCID.Text, header);


            File.WriteAllText(filename, body);
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
            return blob;
        }

        public static string ReadEmoteBlob(StreamReader sr)
        {
            string blob = "";
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("INSERT INTO `weenie_properties_create_list`"))
                    return blob;
                else
                {
                    blob += line + "\r\n";
                }
            }
            
            return blob;
        }
        public static void ReadEmoteCreateListBlob(string readline, StreamReader sr, out string emoteBlob, out string createListBlob)
        {
            emoteBlob = readline + "\r\n";
            createListBlob = "";

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("INSERT INTO `weenie_properties_create_list`"))
                    createListBlob = ReadBlob(sr);
                else
                {
                    emoteBlob += line + "\r\n";
                }
            }

        }

        public static string OpenESFile()
        {
            string esdata = "";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open EmoteScript File";
            ofd.Filter = "ES files|*.es";
            ofd.InitialDirectory = WeenieFabUser.Default.DefaultESPath;
            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                esdata = File.ReadAllText(ofd.FileName);
            }

            return esdata;
        }
        public void SaveESFile(string esdata)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save EmoteScript File";
            sfd.Filter = "ES files|*.es";
            sfd.FileName = tbWCID.Text + $".es";
            sfd.InitialDirectory = WeenieFabUser.Default.DefaultESPath;

            Nullable<bool> result = sfd.ShowDialog();

            if (result == true)
            {
                File.WriteAllText(sfd.FileName, esdata);
            }

        }
    }
}
