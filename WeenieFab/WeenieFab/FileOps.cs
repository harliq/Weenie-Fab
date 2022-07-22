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
using System.Windows;
using System.Reflection;
using System.Data;
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace WeenieFab
{
    public partial class MainWindow
    {
        public void OpenFile() 
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Weenie File";
            ofd.Filter = "All Weenie Types|*.sql;*.json|SQL files|*.sql|JSON files|*.json";            
            ofd.RestoreDirectory = true;

            if (WeenieFabUser.Default.UseFilePaths == false)
            {
                ofd.InitialDirectory = WeenieFabUser.Default.DefaultSqlPath;
            }

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                string ext = Path.GetExtension(ofd.FileName);

                //  Opening a JSON file is going to depend on if ACData is fixed for strings. 
                //  I have tried to work around what ACData does by attempting to do a temp file (didn't work issue with the way it handles strings),
                //  AND at this point, ACData also changes the file name from the default JSON name.  This causes an issue.  So disabling it for now.  
                //  Will revisit down the road.

                if (ext == ".json")
                {

                    FileInfo jfileinfo = new FileInfo(ofd.FileName);
                    DirectoryInfo directoryInfo = new DirectoryInfo(WeenieFabUser.Default.DefaultSqlPath);
                    MessageBox.Show("Json import not supported yet.  Please use converter.");


                    // *--- Area to uncomment for when it works --- *
                    ////ClearAllDataTables();

                    ////ClearAllFields();
                    ////ResetIndexAllDataGrids();

                    ////ACDataLib.Converter.json2sql(jfileinfo, null, directoryInfo);

                    //string sqlfilename = ofd.SafeFileName.Replace(".json", ".sql");

                    ////  Have to do all of this because ACData adds a zero infront of wcid (I am guessing it's padding zeros upto 5 places)
                    //string[] tsqlfilename = sqlfilename.Split(" ");
                    //string fmt = "00000.##";
                    //string twcid = ConvertToInteger(tsqlfilename[0]).ToString(fmt);
                    //sqlfilename = sqlfilename.Replace(tsqlfilename[0], twcid);

                    ////ReadSQLFile(WeenieFabUser.Default.DefaultSqlPath + @"\" + sqlfilename);

                }
                else if (ext == ".sql")
                {
                    ClearAllDataTables();
                    //ClearAllDataGrids();
                    ClearAllFields();
                    //ResetIndexAllDataGrids();
                    ReadSQLFile(ofd.FileName);
                    Globals.WeenieFileName = ofd.FileName;
                    this.Title = "WeenieFab - " + ofd.FileName;
                    //var dateTime = DateTime.Now;
                    txtBlockFileStatus.Text = "File Not saved ";
                }
                else
                    MessageBox.Show("File Extension Not Reconized");
            }
        }

        public void OpenSqlFile(string filename)
        {
            ClearAllDataTables();
            ClearAllFields();
            ReadSQLFile(filename);
            Globals.WeenieFileName = filename;
            this.Title = "WeenieFab - " + filename;
            //var dateTime = DateTime.Now;
            txtBlockFileStatus.Text = "File Not saved ";

        }

        public void SaveFile()
        {
            //string weenieName = GetSavedFileName(stringDataTable);
            //string weenieWCID = tbWCID.Text.PadLeft(5, '0');

            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Title = "Save Weenie File";
            //sfd.Filter = "SQL files|*.sql";
            //sfd.FileName = $"{weenieWCID} {weenieName}.sql";
            //sfd.InitialDirectory = WeenieFabUser.Default.DefaultSqlPath;

            //Nullable<bool> result = sfd.ShowDialog();          

            //if (result == true)
            //{
            //    WriteSQLFile(sfd.FileName);
            //    Globals.WeenieFileName = sfd.FileName;
            //    this.Title = "WeenieFab - " + sfd.FileName;
            //}

            if (Globals.WeenieFileName == "" || Globals.WeenieFileName == null)
                SaveFileAs();
            else
            {
                WriteSQLFile(Globals.WeenieFileName);
                var dateTime = DateTime.Now;
                txtBlockFileStatus.Text = "File Saved @ " + dateTime.ToString("hh:mm tt MM/dd/yyyy");
            }
            
        }

        public void SaveFileAs()
        {
            string weenieName = GetSavedFileName(stringDataTable);
            string weenieWCID = tbWCID.Text.PadLeft(5, '0');

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Weenie File";
            sfd.Filter = "SQL files|*.sql";
            sfd.FileName = $"{weenieWCID} {weenieName}.sql";
            sfd.RestoreDirectory = true;

            if (WeenieFabUser.Default.UseFilePaths == false)
            {
                sfd.InitialDirectory = WeenieFabUser.Default.DefaultSqlPath;
            }

            

            Nullable<bool> result = sfd.ShowDialog();

            if (result == true)
            {
                ProgressBarAnimation();
                WriteSQLFile(sfd.FileName);
                Globals.WeenieFileName = sfd.FileName;
                this.Title = "WeenieFab - " + sfd.FileName;
                var dateTime = DateTime.Now;
                txtBlockFileStatus.Text = "File Saved @ " + dateTime.ToString("hh:mm tt MM/dd/yyyy");
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
            string iidBlob = "";
            string attributeBlob = "";
            string attributeTwoBlob = "";
            string skillsBlob = "";
            string bodyPartBlob = "";
            string spellBookBlob = "";
            string emoteBlob = "";
            string createListBlob = "";
            string bookInfoBlob = "";
            string bookPageBlob = "";
            string positionsBlob = "";
            string generatorBlob = "";
            string eventFilterBlob = "";

            string line;

            // Regex Patterns
            var intPattern = @"\((\d+),\s*(\d+),\s*(-?\d+)\) \/\*(.*)\*\/*.*$";
            var boolPattern = @"\((\d+),\s*(\d+),\s*(\w+)\s*\)\s*\/\*\s*(.*)\s*\*\/*.*$";
            var floatPattern = @"\((\d+),\s*(\d+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+)\)\s*\/\*\s*(.*)\s*\*\/*.*$";
            var spellbookPattern = @"\((\d+),\s*(\d+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+).*\)\s*\/\*\s*(.*)\s*\*\/*.*$";
            var stringPattern = @"\((\d+),\s*(\d+),\s*'(.*)'\)\s*\/\*\s*(.*)\s*\*\/.*.*$";
            // var stringPattern = @"\((\d+),\s*(\d+),\s*'([a-zA-Z0-9_ .!?]*)'\)\s*\/\*\s*(.*)\s*\*\/.*.*$";
            // var didPattern = @"\((\d+),\s*(\d+),\s*(-?\d+)\) \/\*(.*)\*\/*.*$";
            // var didPattern = @"\((\d+),\s*(\d+),\s*(-?\d+)\) \/\*(.*)\*\/*.*$|\((\d+),\s*(\d+),\s*(-?0[xX][0-9a-fA-F]+)\) \/\*(.*)\*\/*.*$";

            var didPattern = @"\((\d+),\s*(\d+),\s*(-?[a-zA-Z0-9_.-]*)\) \/\*(.*)\*\/*.*$";

            var attribPattern = @"\((\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+)\) \/\*(.*)\*\/*.*$";
            var attrib2Pattern = @"\((\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+)\) \/\*(.*)\*\/*.*$";
            var skillsPattern = @"\((\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+)\) \/\*(.*)\*\/*.*$";
            //var skillsPattern = @"\((\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+)\) \/\*(.*)\*\/*.*$";
            var createListPattern = @"\((\d+),\s*(\d+),\s*(\d+),\s*(-?\d+),\s*(\d+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([a-zA-Z0-9_ ]*)\) \/\*(.*)\*\/*.*$";
            var bodyPartsPattern = @"\((\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*(\d+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+),\s*([-+]?[0-9]*\.[0-9]+|[0-9]+)\) \/\*(.*)\*\/*.*$";
            var bookInfoPattern = @"\((\d+),\s*(\d+),\s*(\d+)\).*$";
            var bookPagePattern = @"\((\d+),\s*(\d+),\s*(\d+),\s*'(.*)',\s*'(.*)',\s*(\w+),\s*'((?s:.*)).*$";
            //var positionsPatern = @"\((\d+),\s+(\d+),\s+(\d+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+)\)\s+\/\*(.*)\*\/*.*$";
            var positionsPatern = @"\((\d+),\s+(\d+),\s+(-?[a-zA-Z0-9_.-]*),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+),\s+([-+]?[0-9]*\.[0-9]+|[-+]?[0-9]+)\)\s+\/\*(.*)\*\/*.*$";
            var eventPattern = @"\((\d+),\s*(-?\d+)\) \/\*(.*)\*\/*.*$";
            try
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    while ((line = sr.ReadLine()) != null)
                    {

                        if (line.Contains("INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)"))
                        {
                            wcidblob += sr.ReadLine();
                            //string pattern = @"VALUES \s*\((\d+), '([\w -]*[a-zA-Z])?',\s*(\d+).*$";
                            string pattern = @"VALUES \s*\((\d+),\s*'(.*)',\s*(\d+).*$";
                            var match = Regex.Match(wcidblob, pattern);
                            tbWCID.Text = match.Groups[1].ToString();
                            tbWeenieName.Text = match.Groups[2].ToString();
                            cbWeenieType.SelectedIndex = ConvertToInteger(match.Groups[3].ToString());

                        }
                        else if (line.Contains("INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)"))
                        {

                            int32Blob = ReadBlob(sr);
                            integerDataTable = DecodeSql.DecodeThreeValuesInt(int32Blob, intPattern);
                            integerDataTable.AcceptChanges();
                            integerDataTable = ResortDataTable(integerDataTable, "Property", "ASC");
                            dgInt32.DataContext = integerDataTable;
                        }
                        else if (line.Contains("INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)"))
                        {
                            int64Blob = ReadBlob(sr);
                            integer64DataTable = DecodeSql.DecodeInt64(int64Blob, intPattern);
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
                            didDataTable = DecodeSql.DecodeInstanceID(didBlob, didPattern);
                            didDataTable.AcceptChanges();
                            didDataTable = ResortDataTable(didDataTable, "Property", "ASC");
                            dgDiD.DataContext = didDataTable;
                        }
                        else if (line.Contains("INSERT INTO `weenie_properties_i_i_d`"))
                        {
                            iidBlob = ReadBlob(sr);
                            iidDataTable = DecodeSql.DecodeInstanceID(iidBlob, didPattern);
                            iidDataTable.AcceptChanges();
                            iidDataTable = ResortDataTable(iidDataTable, "Property", "ASC");
                            dgIid.DataContext = iidDataTable;
                        }
                        else if (line.Contains("INSERT INTO `weenie_properties_position`"))
                        {
                            positionsBlob = ReadBlob(sr);
                            positionsDataTable = DecodeSql.DecodePositions(positionsBlob, positionsPatern);
                            positionsDataTable.AcceptChanges();
                            positionsDataTable = ResortDataTable(positionsDataTable, "PositionType", "ASC");
                            dgPosition.DataContext = positionsDataTable;
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

                            bodypartsDataTable = DecodeSql.DecodeBodyPart(bodyPartBlob, bodyPartsPattern);
                            bodypartsDataTable.AcceptChanges();
                            dgBodyParts.DataContext = bodypartsDataTable;

                        }
                        else if (line.Contains("INSERT INTO `weenie_properties_spell_book`"))
                        {
                            spellBookBlob = ReadBlob(sr);

                            spellDataTable = DecodeSql.DecodeThreeValuesFloat(spellBookBlob, spellbookPattern);
                            spellDataTable.AcceptChanges();
                            //spellDataTable = ResortDataTable(spellDataTable, "Property", "ASC");
                            dgSpell.DataContext = spellDataTable;

                        }
                        else if (line.Contains("INSERT INTO `weenie_properties_book`"))
                        {
                            bookInfoBlob = ReadBlob(sr);

                            bookInfoDataTable = DecodeSql.DecodeBookInfo(bookInfoBlob, bookInfoPattern);
                            bookInfoDataTable.AcceptChanges();
                            // bookInfoDataTable = ResortDataTable(spellDataTable, "Property", "ASC");
                            dgBookInfo.DataContext = bookInfoDataTable;

                        }
                        else if (line.Contains("INSERT INTO `weenie_properties_book_page_data`"))
                        {
                            bookPageBlob = ReadBlob(sr);

                            bookPagesDataTable = DecodeSql.DecodeBookPage(bookPageBlob, bookPagePattern);
                            bookInfoDataTable.AcceptChanges();
                            // bookInfoDataTable = ResortDataTable(spellDataTable, "Property", "ASC");
                            dgBookPages.DataContext = bookPagesDataTable;

                        }

                        else if (line.Contains("INSERT INTO `weenie_properties_emote_action`") || line.Contains("INSERT INTO `weenie_properties_emote`") || line.Contains("SET @parent_id = LAST_INSERT_ID()"))
                        {
                            emoteBlob += ReadEmoteBlob(line, sr);
                        }

                        else if (line.Contains("INSERT INTO `weenie_properties_generator`"))
                        {
                            
                            //generatorBlob += "INSERT INTO `weenie_properties_generator` (`object_Id`, `probability`, `weenie_Class_Id`, `delay`, `init_Create`, `max_Create`, `when_Create`, `where_Create`, `stack_Size`, `palette_Id`, `shade`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)\r\n";
                            generatorBlob += ReadEmoteBlob(line, sr);
                            tbGenerator.Text = generatorBlob;
                        }

                        else if (line.Contains("INSERT INTO `weenie_properties_create_list`"))
                        {

                            createListBlob = ReadBlob(sr);

                            createListDataTable = DecodeSql.DecodeCreateList(createListBlob, createListPattern);
                            createListDataTable.AcceptChanges();
                            dgCreateItems.DataContext = createListDataTable;
                            dgCreateItems.Items.Refresh();

                        }

                        else if (line.Contains("INSERT INTO `weenie_properties_event_filter`"))
                        {
                            eventFilterBlob = ReadBlob(sr);

                            eventDataTable = DecodeSql.DecodeTwoValuesInt(eventFilterBlob, eventPattern);
                            eventDataTable.AcceptChanges();
                            eventDataTable = ResortDataTable(eventDataTable, "Event", "ASC");
                        }

                    }

                    string tempES = "";
                    string[] emotes = emoteBlob.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);


                    try
                    {
                        var es = EmoteScriptLib.Converter.sql2es(emotes);

                        foreach (var esline in es)
                        {
                            tempES += esline + "\r\n";
                        }
                    }
                    catch (Exception ex)
                    {

                        LogError(ex);
                        MessageBoxButton buttons = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Error;
                        MessageBoxResult result = MessageBox.Show("ERROR READING EMOTES! Issue with Emote Script.  Please send WeenieFabErrorLog.txt to Harli Quinn on Discord", "ERROR!", buttons, icon);
                        //MessageBox.Show($"{ex.Message} \n {ex.StackTrace} \n {ex.Source} \n {ex.TargetSite}");
                        //throw;
                    }

                    rtbEmoteScript.Document.Blocks.Clear();
                    rtbEmoteScript.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(tempES)));

                    if (WeenieFabUser.Default.AutoLoadESFiles == true)
                    {
                        string esfile = WeenieFabUser.Default.DefaultESPath + @"\" + tbWCID.Text + ".es";
                        if (File.Exists(esfile) == true)
                        {
                            string esdata = File.ReadAllText(esfile);
                            rtbEmoteScript.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(esdata)));
                        }
                    }

                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                
                LogError(ex);
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result = MessageBox.Show("Issue with File. Please send WeenieFabErrorLog.txt to Harli Quinn on Discord", "ERROR!", buttons, icon);
                //MessageBox.Show($"{ex.Message} \n {ex.StackTrace} \n {ex.Source} \n {ex.TargetSite}");
                //throw;
            }
        }
        public void WriteSQLFile(string filename)
        {           
            string dateModified = string.Format("'{0:yyyy-MM-dd hh:mm:ss}'", DateTime.Now);
            string[] weenieTypeDescription = cbWeenieType.Text.Split(" ");

            // Header
            string header = $"DELETE FROM `weenie` WHERE `class_Id` = {tbWCID.Text} \n\n";
            string body = $"DELETE FROM `weenie` WHERE `class_Id` = {tbWCID.Text};\n\n";
           
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
            body += TableToSql.ConvertBooleanTable(boolDataTable, tbWCID.Text, header);
            
            // Float
            header = $"INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertFloatTable(floatDataTable, tbWCID.Text, header);

            // String
            header = $"INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertStringTable(stringDataTable, tbWCID.Text, header);

            // DiD
            header = $"INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertDidTable(didDataTable, tbWCID.Text, header);

            // IiD
            header = $"INSERT INTO `weenie_properties_i_i_d` (`object_Id`, `type`, `value`)";
            body += TableToSql.ConvertIidTable(iidDataTable, tbWCID.Text, header);
            
            // Positions
            header = $"INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)";
            body += TableToSql.ConvertPositionTable(positionsDataTable, tbWCID.Text, header);
         
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
            body += TableToSql.ConvertBodyPart(bodypartsDataTable, tbWCID.Text, header);

            //string bodyparts = new TextRange(rtbBodyParts.Document.ContentStart, rtbBodyParts.Document.ContentEnd).Text;
            //if (bodyparts != "")
            //    body += TableToSql.ConvertBodyTable(bodyparts, tbWCID.Text, header);

            // Spells
            header = $"INSERT INTO `weenie_properties_spell_book` (`object_Id`, `spell`, `probability`)";
            body += TableToSql.ConvertSpellTable(spellDataTable, tbWCID.Text, header);

            // Event
            header = $"INSERT INTO `weenie_properties_event_filter` (`object_Id`, `event`)";
            body += TableToSql.ConvertBiValueTable(eventDataTable, tbWCID.Text, header);

            // Emotes
            string tempES = new TextRange(rtbEmoteScript.Document.ContentStart, rtbEmoteScript.Document.ContentEnd).Text;
            
            // To fix the issue with Rich Text Boxes
            tempES = tempES.Replace("\n", "\r\n");
            tempES = tempES.Replace("\r\r\n", "\r\n");

            string[] saES = tempES.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int tWCID = ConvertToInteger(tbWCID.Text);
            string finalEmotes = "";

            // var eslist = EmoteScriptLib.Converter.es2sql(saES, ConvertToInteger(tbWCID.Text));

            try
            {
                var eslist = EmoteScriptLib.Converter.es2sql(saES, (uint)tWCID);
                foreach (var emoteline in eslist)
                {
                    finalEmotes += emoteline + "\r\n";
                }
            }
            catch (Exception ex)
            {

                LogError(ex);
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result = MessageBox.Show("FILE NOT SAVED WITH E MOTES! Please Copy Emotes and save outside of WF.  Issue with Emote Script.  Please send WeenieFabErrorLog.txt to Harli Quinn on Discord", "ERROR!", buttons, icon);
                //MessageBox.Show($"{ex.Message} \n {ex.StackTrace} \n {ex.Source} \n {ex.TargetSite}");
                //throw;
            }
            
            body += finalEmotes;

            // Create Items
            header = $"INSERT INTO `weenie_properties_create_list` (`object_Id`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`)";
            body += TableToSql.ConvertCreateItemsTable(createListDataTable, tbWCID.Text, header);

            // Book Info
            header = $"INSERT INTO `weenie_properties_book` (`object_Id`, `max_Num_Pages`, `max_Num_Chars_Per_Page`)";
            body += TableToSql.ConvertBookInfo(bookInfoDataTable, tbWCID.Text, header);

            // Book Pages
            header = $"INSERT INTO `weenie_properties_book_page_data` (`object_Id`, `page_Id`, `author_Id`, `author_Name`, `author_Account`, `ignore_Author`, `page_Text`)";
            body += TableToSql.ConvertBookPages(bookPagesDataTable, tbWCID.Text, header);

            // Generator

            if (tbGenerator.Text != "")
            {
                // header = $"INSERT INTO `weenie_properties_generator` (`object_Id`, `probability`, `weenie_Class_Id`, `delay`, `init_Create`, `max_Create`, `when_Create`, `where_Create`, `stack_Size`, `palette_Id`, `shade`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)";
                // body += header;
                body += tbGenerator.Text;
            }

            File.WriteAllText(filename, body);
        }

        public static string ReadBlob(StreamReader sr)
        {
            string blob = "";
            string line;
            string tLine;

            bool isMultiLineComment = false;
            while ((line = sr.ReadLine()) != null) 
            {
                if (isMultiLineComment)
                {
                    if (!line.Contains("*/"))
                        continue;
                    else
                    {
                        // This line ends the multi-line comment.
                        isMultiLineComment = false;
                        continue;
                    }
                }
                else if (line.Contains("/*") && !line.Contains("*/")) // This line starts a multi-line comment.
                {
                    if (line.EndsWith(" - "))
                        line = line.Remove(line.Length - 3); // Just some cleanup so it looks prettier in the description field.
                    line += " */"; // Close the comment so the code below can interpret it properly.
                    isMultiLineComment = true;
                }

                // Making this a little more tolerable of spaces and tabs being on blank line - This should help considerably.

                if (line == "" || line == "\r\n" || line == " " || line == "  " || line == "   " || line == "    " || line == "     " || line == "\t " || line == "\t")
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

        public static string ReadEmoteBlob(string readline, StreamReader sr)
        {
            string emoteBlob = readline + "\r\n";

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line == "" || line == "\t" || line == "\r\n")
                    return emoteBlob;
                else
                {
                    emoteBlob += line + "\r\n";
                }
            }
            return emoteBlob;
        }
        public static string ReadEmoteCreateListBlob(string readline, StreamReader sr)
        {
            string emoteBlob = readline + "\r\n";

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line == "" || line == "\t" || line == "\r\n")
                    return emoteBlob;
                else
                {
                    emoteBlob += line + "\r\n";
                }
            }
            return emoteBlob;
        }

        public static string OpenESFile()
        {
            string esdata = "";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open EmoteScript File";
            ofd.Filter = "ES files|*.es";
            ofd.RestoreDirectory = true;

            if (WeenieFabUser.Default.UseFilePaths == false)
            {
                ofd.InitialDirectory = WeenieFabUser.Default.DefaultESPath;
            }
            

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
            sfd.RestoreDirectory = true;

            if (WeenieFabUser.Default.UseFilePaths == false)
            {
                sfd.InitialDirectory = WeenieFabUser.Default.DefaultESPath;
            }

            Nullable<bool> result = sfd.ShowDialog();

            if (result == true)
            {
                File.WriteAllText(sfd.FileName, esdata);
            }

        }
        public static string GetSavedFileName(DataTable dt)
        {
            string weenieFN = "";

            foreach (DataRow row in dt.Rows)
            {
                string temp = row[0].ToString();

                if (temp == "1")
                    weenieFN = row[1].ToString();
                else
                {
                }

            }
            return weenieFN;

        }
        public static void LogError(Exception ex)
        {
            string errorfilepath = @"WeenieFabErrorLog.txt";
            // string errorfilepath = @"\WeenieFabErrorLog.txt";
            try
            {
                
                using (StreamWriter writer = new StreamWriter(errorfilepath, true))
                {
                    writer.WriteLine("============================================================================");
                    writer.WriteLine(DateTime.Now.ToString());
                    writer.WriteLine("Error: " + ex.Message);
                    writer.WriteLine("Source: " + ex.Source);
                    writer.WriteLine("Stack: " + ex.StackTrace);
                    if (ex.InnerException != null)
                    {
                        writer.WriteLine("Inner: " + ex.InnerException.Message);
                        writer.WriteLine("Inner Stack: " + ex.InnerException.StackTrace);
                    }
                    writer.WriteLine("============================================================================");
                    writer.WriteLine("");
                    writer.Close();
                }
            }
            catch (Exception logex)
            {
                MessageBox.Show($"{logex.Message} \n {logex.StackTrace} \n {logex.Source} \n {logex.TargetSite}");
            }
        }
        private void ProgressBarAnimation()
        {

            txtBlockProgressBar.Text = "Saving File...";
            Duration duration = new Duration(TimeSpan.FromSeconds(0.1));
            DoubleAnimation doubleanimation = new DoubleAnimation(100.0, duration);            
            pgBarOne.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);

        }
        private async void ProgressBarClearAnimation()
        {

            await Task.Delay(1000);

            txtBlockProgressBar.Text = "";
            pgBarOne.BeginAnimation(ProgressBar.ValueProperty, null);

        }

    }

}
