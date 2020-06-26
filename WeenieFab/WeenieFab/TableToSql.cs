using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WeenieFab
{
    public class TableToSql
    {
        public static string ConvertTable(DataTable dt, string wcid)
        {
            string sqltext = $"VALUES";

            int counter = 0;
            //string type = "";


            foreach (DataRow row in dt.Rows)
                
            {
                if (counter == 0)
                    sqltext += $" ({wcid}, {row[0],3}, {row[1],10}) /* {row[2]} - */\n";
                else
                    sqltext += $"     , ({wcid}, {row[0],3}, {row[1],10}) /* {row[2]} - */\n";
                counter++;
            }

            sqltext += $"\n";

            return sqltext;

        }



    }
}
