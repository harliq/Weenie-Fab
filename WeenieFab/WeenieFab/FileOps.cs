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
            foreach (string line in File.ReadLines(filepath))
            {
                //if (line.Contains("INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)"))
                //{
                //    File.ne
                //    string linedata = "";
                //    linedata = line.Replace("VALUES", "");
                //    linedata = linedata.Replace("(", "");
                //    linedata = linedata.Replace(");", "");
                //    linedata = linedata.Replace("'", "");
                //    string[] lootProfileValues = linedata.Split(',');
                //    profileData.DTdid = ConvertToInt(lootProfileValues[0]);


                //}
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



    }
}
