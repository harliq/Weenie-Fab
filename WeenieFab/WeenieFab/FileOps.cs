using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            string stringblob = "";

            string line;

            // string path = @"C:\Users\sam\Documents\GCProg\testReadFile.txt";
            using (StreamReader sr = new StreamReader(filepath))
            {
                while ((line = sr.ReadLine()) != null) // Whatever logic you have
                {
                    // string line = sr.ReadLine();
                    if (line.Contains("INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)"))
                        wcidblob += sr.ReadLine();
                    else if (line.Contains("INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)"))
                    {
                        while (sr.Peek() > 0)
                        {
                            if (line.Contains($"\n"))
                                break;
                            else
                            int32Blob += sr.ReadLine();
                            
                        }
                    }
                    else if (line.Contains("INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)"))
                    {
                        while (sr.Peek() > 0)
                        {
                            if (line.Contains($"\n"))
                                break;
                            else
                                int64Blob += sr.ReadLine();

                        }
                    }

                    // string invoice = InvoiceNumberFunc(sr);
                    // Use invoice
                }
                sr.Close();
            }

            



            //foreach (string line in File.ReadLines(filepath))
            //{
            //    //if (line.Contains("INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)"))
            //    //{
            //    //    File.ne
            //    //    string linedata = "";
            //    //    linedata = line.Replace("VALUES", "");
            //    //    linedata = linedata.Replace("(", "");
            //    //    linedata = linedata.Replace(");", "");
            //    //    linedata = linedata.Replace("'", "");
            //    //    string[] lootProfileValues = linedata.Split(',');
            //    //    profileData.DTdid = ConvertToInt(lootProfileValues[0]);


            //    //}
            //}


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



    }
}
