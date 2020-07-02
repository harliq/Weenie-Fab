using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WeenieFab
{
    public class TableToSql
    {
        public static string ConvertTriValueTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            //string type = "";

            int rowcount = dt.Rows.Count;
            if (dt != null)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    //if (row[0].ToString() == "")
                    //{
                    //    sqltext = "";
                    //    break;
                    //}
                    //else
                    //{
                    if (counter == 1)
                        sqltext += $" ({wcid}, {row[0],3}, {row[1],10}) /* {row[2]} - */\n";
                    else
                    {
                        if (counter == rowcount - 1)
                            sqltext += $"     , ({wcid}, {row[0],3}, {row[1],10}) /* {row[2]} - */;\n";
                        else if (counter == rowcount)
                            sqltext += "";
                        else
                            sqltext += $"     , ({wcid}, {row[0],3}, {row[1],10}) /* {row[2]} - */\n";

                    }
                    counter++;
                }
            }
            sqltext += $"\n";

            return sqltext;

        }



    }
}
