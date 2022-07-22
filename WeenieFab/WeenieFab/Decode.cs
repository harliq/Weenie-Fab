using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace WeenieFab
{
    public static class DecodeSql
    {
        public static DataTable DecodeTwoValuesInt(string integerblob, string pattern)
        {
            DataTable tempDataTable = new DataTable();

            DataColumn eventInt = new DataColumn("Event");
            DataColumn descript = new DataColumn("Description");

            eventInt.DataType = Type.GetType("System.Int32");

            tempDataTable.Columns.Add(eventInt);
            tempDataTable.Columns.Add(descript);

            foreach (var blobLine in integerblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (blobLine == "" || blobLine == "\r\n")
                    break;

                var match = Regex.Match(blobLine, pattern);
                string description = match.Groups[3].ToString();
                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = description.Trim();
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }
        public static DataTable DecodeThreeValuesInt(string integerblob, string pattern)
        {
            DataTable tempDataTable = new DataTable();

            DataColumn propertyInt = new DataColumn("Property");
            DataColumn valueInt = new DataColumn("Value");
            DataColumn descript = new DataColumn("Description");

            propertyInt.DataType = Type.GetType("System.Int32");
            valueInt.DataType = Type.GetType("System.Int32");

            tempDataTable.Columns.Add(propertyInt);
            tempDataTable.Columns.Add(valueInt);
            tempDataTable.Columns.Add(descript);

            foreach (var blobLine in integerblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (blobLine == "" || blobLine == "\r\n")
                    break;

                var match = Regex.Match(blobLine, pattern);
                string description = match.Groups[4].ToString();
                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = MainWindow.ConvertToInteger(match.Groups[3].ToString());
                dr[2] = description.Trim();
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }
        public static DataTable DecodeInstanceID(string integerblob, string pattern)
        {
            DataTable tempDataTable = new DataTable();

            DataColumn propertyInt = new DataColumn("Property");
            DataColumn valueInt = new DataColumn("Value");
            DataColumn descript = new DataColumn("Description");

            propertyInt.DataType = Type.GetType("System.Int32");
            valueInt.DataType = Type.GetType("System.Int32");

            tempDataTable.Columns.Add(propertyInt);
            tempDataTable.Columns.Add(valueInt);
            tempDataTable.Columns.Add(descript);

            foreach (var blobLine in integerblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (blobLine == "" || blobLine == "\r\n")
                    break;

                var match = Regex.Match(blobLine, pattern);
                string description = match.Groups[4].ToString();
                DataRow dr = tempDataTable.NewRow();

                long iidValue = CheckHex(match.Groups[3].ToString());

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = iidValue;
                dr[2] = description.Trim();
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }

        public static DataTable DecodeInt64(string integerblob, string pattern)
        {

            DataTable tempDataTable = MainWindow.integer64DataTable.Clone();
            tempDataTable.Clear();

            foreach (var blobLine in integerblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (blobLine == "" || blobLine == "\r\n")
                    break;

                var match = Regex.Match(blobLine, pattern);
                string description = match.Groups[4].ToString();
                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = MainWindow.ConvertToDecimal(match.Groups[3].ToString());
                dr[2] = description.Trim();
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }

        public static DataTable DecodeThreeValuesFloat(string floatblob, string pattern)
        {
            DataTable tempDataTable = new DataTable();

            DataColumn propertyInt = new DataColumn("Property");
            DataColumn valueFloat = new DataColumn("Value");
            DataColumn descript = new DataColumn("Description");

            propertyInt.DataType = Type.GetType("System.Int32");
            valueFloat.DataType = Type.GetType("System.Single");

            tempDataTable.Columns.Add(propertyInt);
            tempDataTable.Columns.Add(valueFloat);
            tempDataTable.Columns.Add(descript);

            foreach (var blobLine in floatblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var match = Regex.Match(blobLine, pattern);
                string description = match.Groups[4].ToString();

                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = MainWindow.ConvertToFloat(match.Groups[3].ToString());
                dr[2] = description.Trim();
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }
        public static DataTable DecodeThreeValuesBool(string boolblob, string pattern)
        {
            DataTable tempDataTable = MainWindow.boolDataTable.Clone();
            tempDataTable.Clear();

            foreach (var blobLine in boolblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var match = Regex.Match(blobLine, pattern);
                string description = match.Groups[4].ToString();
                bool tf;

                if (match.Groups[3].ToString() == "TRUE" || match.Groups[3].ToString() == "True" || match.Groups[3].ToString() == "true")
                    tf = true;
                else
                    tf = false;

                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = tf;
                dr[2] = description.Trim();
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }

        public static DataTable DecodeThreeValuesString(string floatblob, string pattern)
        {
            DataTable tempDataTable = MainWindow.stringDataTable.Clone();
            tempDataTable.Clear();

            foreach (var blobLine in floatblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (blobLine.Contains("\""))
                    pattern = @"\((\d+),\s*(\d+),\s*""(.*)""\)\s*\/\*\s*(.*)\s*\*\/.*.*$"; // For double quotes in strings
                else
                    pattern = @"\((\d+),\s*(\d+),\s*'(.*)'\)\s*\/\*\s*(.*)\s*\*\/.*.*$";

                var match = Regex.Match(blobLine, pattern);
                string description = match.Groups[4].ToString();

                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = match.Groups[3].ToString();
                dr[2] = description.Trim();
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }
        public static DataTable DecodeAttribute(string attribblob, string pattern)
        {

            DataTable tempDataTable = MainWindow.attributeDataTable.Clone();
            tempDataTable.Clear();

            foreach (var blobLine in attribblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var match = Regex.Match(blobLine, pattern);
                string description = match.Groups[6].ToString();

                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = MainWindow.ConvertToInteger(match.Groups[3].ToString());
                dr[2] = MainWindow.ConvertToInteger(match.Groups[4].ToString());
                dr[3] = MainWindow.ConvertToInteger(match.Groups[5].ToString());
                dr[4] = description.Trim();
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }
        public static DataTable DecodeAttributeTwo(string attribblob, string pattern)
        {

            DataTable tempDataTable = MainWindow.attribute2DataTable.Clone();
            tempDataTable.Clear();

            foreach (var blobLine in attribblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var match = Regex.Match(blobLine, pattern);
                string description = match.Groups[7].ToString();

                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = MainWindow.ConvertToInteger(match.Groups[3].ToString());
                dr[2] = MainWindow.ConvertToInteger(match.Groups[4].ToString());
                dr[3] = MainWindow.ConvertToInteger(match.Groups[5].ToString());
                dr[4] = MainWindow.ConvertToInteger(match.Groups[6].ToString());
                dr[5] = description.Trim();
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }
        public static DataTable DecodeSkills(string blob, string pattern)
        {

            DataTable tempDataTable = MainWindow.skillsDataTable.Clone();
            tempDataTable.Clear();

            foreach (var blobLine in blob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var match = Regex.Match(blobLine, pattern);
                string description = match.Groups[9].ToString();

                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = MainWindow.ConvertToInteger(match.Groups[3].ToString());
                dr[2] = MainWindow.ConvertToInteger(match.Groups[4].ToString());
                dr[3] = MainWindow.ConvertToInteger(match.Groups[5].ToString());
                dr[4] = MainWindow.ConvertToInteger(match.Groups[6].ToString());
                dr[5] = MainWindow.ConvertToInteger(match.Groups[7].ToString());
                dr[6] = match.Groups[8].ToString();
                dr[7] = description.Trim();

                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }
        public static DataTable DecodeCreateList(string blob, string pattern)
        {

            DataTable tempDataTable = MainWindow.createListDataTable.Clone();
            tempDataTable.Clear();

            foreach (var blobLine in blob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var match = Regex.Match(blobLine, pattern);

                bool tf;

                if (match.Groups[7].ToString() == "TRUE" || match.Groups[3].ToString() == "True" || match.Groups[3].ToString() == "true")
                    tf = true;
                else
                    tf = false;

                string description = match.Groups[8].ToString();

                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = MainWindow.ConvertToInteger(match.Groups[3].ToString());
                dr[2] = MainWindow.ConvertToInteger(match.Groups[4].ToString());
                dr[3] = MainWindow.ConvertToInteger(match.Groups[5].ToString());
                dr[4] = MainWindow.ConvertToFloat(match.Groups[6].ToString());
                dr[5] = tf;
                dr[6] = description.Trim();

                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }
        public static DataTable DecodeBodyPart(string blob, string pattern)
        {

            DataTable tempDataTable = MainWindow.bodypartsDataTable.Clone();
            tempDataTable.Clear();

            foreach (var blobLine in blob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var match = Regex.Match(blobLine, pattern);

                string description = match.Groups[28].ToString();

                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = MainWindow.ConvertToInteger(match.Groups[3].ToString());
                dr[2] = MainWindow.ConvertToInteger(match.Groups[4].ToString());

                dr[3] = MainWindow.ConvertToFloat(match.Groups[5].ToString());

                dr[4] = MainWindow.ConvertToInteger(match.Groups[6].ToString());
                dr[5] = MainWindow.ConvertToInteger(match.Groups[7].ToString());
                dr[6] = MainWindow.ConvertToInteger(match.Groups[8].ToString());
                dr[7] = MainWindow.ConvertToInteger(match.Groups[9].ToString());
                dr[8] = MainWindow.ConvertToInteger(match.Groups[10].ToString());
                dr[9] = MainWindow.ConvertToInteger(match.Groups[11].ToString());
                dr[10] = MainWindow.ConvertToInteger(match.Groups[12].ToString());
                dr[11] = MainWindow.ConvertToInteger(match.Groups[13].ToString());
                dr[12] = MainWindow.ConvertToInteger(match.Groups[14].ToString());

                dr[13] = MainWindow.ConvertToInteger(match.Groups[15].ToString());

                // Start Float Quads

                dr[14] = MainWindow.ConvertToFloat(match.Groups[16].ToString());
                dr[15] = MainWindow.ConvertToFloat(match.Groups[17].ToString());
                dr[16] = MainWindow.ConvertToFloat(match.Groups[18].ToString());

                dr[17] = MainWindow.ConvertToFloat(match.Groups[19].ToString());
                dr[18] = MainWindow.ConvertToFloat(match.Groups[20].ToString());
                dr[19] = MainWindow.ConvertToFloat(match.Groups[21].ToString());

                dr[20] = MainWindow.ConvertToFloat(match.Groups[22].ToString());
                dr[21] = MainWindow.ConvertToFloat(match.Groups[23].ToString());
                dr[22] = MainWindow.ConvertToFloat(match.Groups[24].ToString());

                dr[23] = MainWindow.ConvertToFloat(match.Groups[25].ToString());
                dr[24] = MainWindow.ConvertToFloat(match.Groups[26].ToString());
                dr[25] = MainWindow.ConvertToFloat(match.Groups[27].ToString());

                dr[26] = description.Trim();

                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }
        public static DataTable DecodeBookInfo(string blob, string pattern)
        {

            DataTable tempDataTable = MainWindow.bookInfoDataTable.Clone();
            tempDataTable.Clear();

            foreach (var blobLine in blob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var match = Regex.Match(blobLine, pattern);

                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = MainWindow.ConvertToInteger(match.Groups[3].ToString());
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }
        public static DataTable DecodeBookPage(string blob, string pattern)
        {

            DataTable tempDataTable = MainWindow.bookPagesDataTable.Clone();
            tempDataTable.Clear();

            string splitString = @"')";
            blob = blob.Replace(";", "");

            foreach (var blobLine in blob.Split(new string[] { splitString }, StringSplitOptions.RemoveEmptyEntries))
            {

                if (blobLine == "\r\n")
                    continue;
                var match = Regex.Match(blobLine, pattern);

                bool tf;

                if (match.Groups[7].ToString() == "TRUE" || match.Groups[3].ToString() == "True" || match.Groups[3].ToString() == "true")
                    tf = true;
                else
                    tf = false;

                DataRow dr = tempDataTable.NewRow();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                dr[1] = match.Groups[3].ToString();
                dr[2] = match.Groups[4].ToString();
                dr[3] = match.Groups[5].ToString();
                dr[4] = tf;
                dr[5] = match.Groups[7].ToString();
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }
        public static DataTable DecodePositions(string blob, string pattern)
        {

            DataTable tempDataTable = MainWindow.positionsDataTable.Clone();
            tempDataTable.Clear();

            foreach (var blobLine in blob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (blobLine.Contains("teleloc"))
                    break;
                var match = Regex.Match(blobLine, pattern);              

                DataRow dr = tempDataTable.NewRow();
                string cellID = CheckHex(match.Groups[3].ToString()).ToString();

                dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                //dr[1] = MainWindow.ConvertToLong(match.Groups[3].ToString());
                dr[1] = cellID;

                dr[2] = MainWindow.ConvertToFloat(match.Groups[4].ToString());
                dr[3] = MainWindow.ConvertToFloat(match.Groups[5].ToString());
                dr[4] = MainWindow.ConvertToFloat(match.Groups[6].ToString());

                dr[5] = MainWindow.ConvertToFloat(match.Groups[7].ToString());
                dr[6] = MainWindow.ConvertToFloat(match.Groups[8].ToString());
                dr[7] = MainWindow.ConvertToFloat(match.Groups[9].ToString());
                dr[8] = MainWindow.ConvertToFloat(match.Groups[10].ToString());

                dr[9] = match.Groups[11].ToString();

                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;

        }
        public static uint CheckHex(string checkhex)
        {
            // Check for Hex
            uint value = 0;
            string checkString = "";
            if (checkhex.Contains("0x"))
            {
                checkString = checkhex.Replace("0x", "");
                //value = MainWindow.ConvertHexToDecimal(checkHex);
                value = MainWindow.ConvertHexToDecimal(checkString);
            }
            else if (checkhex.Contains("0X"))
            {
                checkString = checkhex.Replace("0X", "");
                value = MainWindow.ConvertHexToDecimal(checkString);
            }
            else
            {
                checkString = checkhex;
                //value = MainWindow.ConvertHexToDecimal(checkString);
                int tvalue = MainWindow.ConvertToInteger(checkhex);
                value = (uint)tvalue;
            }

            return value;
        }
    }
}
