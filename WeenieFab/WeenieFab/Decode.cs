using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace WeenieFab
{
    public static class DecodeSql
    {

        //public static DataTable DecodeInt32(string integerblob)
        //{
        //    //  RegEx Pattern
        //    // \((\d+),\s*(\d+),\s*(\d+)\).*$
        //    // var intDataTable = DataTable;

        //    DataTable tempDataTable = new DataTable();

        //    DataColumn property = new DataColumn("Property");
        //    DataColumn value = new DataColumn("Value");
        //    DataColumn descript = new DataColumn("Description");

        //    property.DataType = System.Type.GetType("System.Int32");
        //    value.DataType = System.Type.GetType("System.Int32");

        //    tempDataTable.Columns.Add(property);
        //    tempDataTable.Columns.Add(value);
        //    tempDataTable.Columns.Add(descript);

        //    var pattern = @"\((\d+),\s*(\d+),\s*(-?\d+)\) \/\*(.*)\*\/$";

        //    foreach (var blobLine in integerblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        var match = Regex.Match(blobLine, pattern);

        //        DataRow dr = tempDataTable.NewRow();

        //        dr[0] = MainWindow.ConvertToInteger(match.Groups[2].ToString());
        //        dr[1] = MainWindow.ConvertToInteger(match.Groups[3].ToString());
        //        dr[2] = match.Groups[4];
        //        tempDataTable.Rows.Add(dr);
                
        //    }
        //    // string Nothing = "";
        //    return tempDataTable;
        //}
        

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
            DataTable tempDataTable = new DataTable();

            DataColumn propertyInt = new DataColumn("Property");
            DataColumn valueBool = new DataColumn("Value");
            DataColumn descript = new DataColumn("Description");
            propertyInt.DataType = Type.GetType("System.Int32");
            valueBool.DataType = Type.GetType("System.Boolean");

            tempDataTable.Columns.Add(propertyInt);
            tempDataTable.Columns.Add(valueBool);
            tempDataTable.Columns.Add(descript);

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
            DataTable tempDataTable = new DataTable();

            DataColumn propertyInt = new DataColumn("Property");
            DataColumn valueString = new DataColumn("Value");
            DataColumn descript = new DataColumn("Description");
            propertyInt.DataType = Type.GetType("System.Int32");
            valueString.DataType = Type.GetType("System.String");

            tempDataTable.Columns.Add(propertyInt);
            tempDataTable.Columns.Add(valueString);
            tempDataTable.Columns.Add(descript);

            foreach (var blobLine in floatblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
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
            //DataTable tempDataTable = new DataTable();
            DataTable tempDataTable = MainWindow.attributeDataTable.Clone();
            tempDataTable.Clear();
            //DataColumn propertyInt = new DataColumn("Property");
            //DataColumn valueString = new DataColumn("Value");
            //DataColumn descript = new DataColumn("Description");
            //propertyInt.DataType = Type.GetType("System.Int32");
            //valueString.DataType = Type.GetType("System.String");

            //tempDataTable.Columns.Add(propertyInt);
            //tempDataTable.Columns.Add(valueString);
            //tempDataTable.Columns.Add(descript);

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
                dr[6] = MainWindow.ConvertToInteger(match.Groups[8].ToString());
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
    }
}
