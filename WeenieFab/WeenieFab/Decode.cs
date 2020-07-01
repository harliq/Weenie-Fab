using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace WeenieFab
{
    public static class DecodeSql
    {

        public static DataTable DecodeInt32(string integerblob)
        {
            //  RegEx Pattern
            // \((\d+),\s*(\d+),\s*(\d+)\).*$
            // var intDataTable = DataTable;

            DataTable tempDataTable = new DataTable();
            

            tempDataTable.Columns.Add(new DataColumn("Property"));
            tempDataTable.Columns.Add(new DataColumn("Value"));
            tempDataTable.Columns.Add(new DataColumn("Description"));

            var pattern = @"\((\d+),\s*(\d+),\s*(-?\d+)\) \/\*(.*)\*\/$";

            foreach (var blobLine in integerblob.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var match = Regex.Match(blobLine, pattern);

                DataRow dr = tempDataTable.NewRow();

                dr[0] = match.Groups[2];
                dr[1] = match.Groups[3];
                dr[2] = match.Groups[4];
                tempDataTable.Rows.Add(dr);

            }
            string Nothing = "";
            return tempDataTable;
        }

        
    }
}
