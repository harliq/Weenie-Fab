using ACE.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WeenieFab
{
    public class TableToSql
    {
        public static string ConvertBiValueTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    if (counter == 1 && counter == rowcount)
                        sqltext += $" ({wcid},{row[0],11}) /* {row[1]} */;\n";
                    else if (counter == 1)
                        sqltext += $" ({wcid},{row[0],11}) /* {row[1]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],11}) /* {row[1]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],11}) /* {row[1]} */\n";
                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }

        public static string ConvertTriValueTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    if (counter == 1 && counter == rowcount)
                        sqltext += $" ({wcid},{row[0],4},{row[1],11}) /* {row[2]} */;\n";
                    else if (counter == 1)
                        sqltext += $" ({wcid},{row[0],4},{row[1],11}) /* {row[2]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],4},{row[1],11}) /* {row[2]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],4},{row[1],11}) /* {row[2]} */\n";
                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }

        public static string ConvertBooleanTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;
            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    string tempTrueFalse = row[1].ToString();
                    int padRight = 20 - tempTrueFalse.Length;
                    // string finalTrueFalse = tempTrueFalse.PadRight(6).PadLeft(6);
                    tempTrueFalse = tempTrueFalse.PadRight(5).PadLeft(6);

                    if (counter == 1 && counter == rowcount)
                        sqltext += $" ({wcid},{row[0],4},{tempTrueFalse}) /* {row[2]} */;\n";
                    else if (counter == 1)
                        sqltext += $" ({wcid},{row[0],4},{tempTrueFalse}) /* {row[2]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],4},{tempTrueFalse}) /* {row[2]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],4},{tempTrueFalse}) /* {row[2]} */\n";
                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }

        public static string ConvertFloatTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    if (counter == 1 && counter == rowcount)
                        sqltext += $" ({wcid},{row[0],4},{row[1],8}) /* {row[2]} */;\n";
                    else if (counter == 1)
                        sqltext += $" ({wcid},{row[0],4},{row[1],8}) /* {row[2]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],4},{row[1],8}) /* {row[2]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],4},{row[1],8}) /* {row[2]} */\n";
                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }

        public static string ConvertStringTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {

                    string tempWeenieString = row[1].ToString();
                    string finalWeenieString = "";
                    if (tempWeenieString.Contains("''"))
                        finalWeenieString = tempWeenieString;
                    else
                    {
                        if (tempWeenieString.Contains("'"))
                        {
                            finalWeenieString = tempWeenieString.Replace("'", "''");
                        }
                        else
                            finalWeenieString = tempWeenieString;
                    }

                    string sValue = "'" + finalWeenieString + "'";

                    if (counter == 1 && counter == rowcount)
                        sqltext += $" ({wcid},{row[0],4}, {sValue,1}) /* {row[2]} */;\n";
                    else if (counter == 1)
                        sqltext += $" ({wcid},{row[0],4}, {sValue,1}) /* {row[2]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],4}, {sValue,1}) /* {row[2]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],4}, {sValue,1}) /* {row[2]} */\n";
                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }

        public static string ConvertAttributeTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    if (counter == 1)
                        sqltext += $" ({wcid},{row[0],4},{row[1],4},{row[2],2},{row[3],2}) /* {row[4]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],4},{row[1],4},{row[2],2},{row[3],2}) /* {row[4]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],4},{row[1],4},{row[2],2},{row[3],2}) /* {row[4]} */\n";

                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }

        //  Attrib2 - Health, Stamina, Mana
        public static string ConvertAttribute2Table(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    if (counter == 1)
                        sqltext += $" ({wcid},{row[0],4},{row[1],6},{row[2],2},{row[3],2},{row[4],5}) /* {row[5]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],4},{row[1],6},{row[2],2},{row[3],2},{row[4],5}) /* {row[5]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],4},{row[1],6},{row[2],2},{row[3],2},{row[4],5}) /* {row[5]} */\n";

                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }

        public static string ConvertSkillsTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    if (counter == 1 && counter == rowcount)
                        sqltext += $" ({wcid},{row[0],3},{row[1],2},{row[2],2},{row[3],2},{row[4],4},{row[5],2},{row[6],2}) /* {row[7]} */;\n";
                    else if (counter == 1)
                        sqltext += $" ({wcid},{row[0],3},{row[1],2},{row[2],2},{row[3],2},{row[4],4},{row[5],2},{row[6],2}) /* {row[7]} */\n";

                    else
                            {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],3},{row[1],2},{row[2],2},{row[3],2},{row[4],4},{row[5],2},{row[6],2}) /* {row[7]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],3},{row[1],2},{row[2],2},{row[3],2},{row[4],4},{row[5],2},{row[6],2}) /* {row[7]} */\n";

                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }
        public static string ConvertBodyTable(string bodyparts, string wcid, string header)
        {
            int counter = 0;
            string sqltext = header + $"\nVALUES";

            foreach (var blobLine in bodyparts.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (counter == 0)
                    sqltext += $" {blobLine}\n";
                else
                    sqltext += $"     , {blobLine}\n";
                counter++;
            }
            sqltext += $"\n";
            return sqltext;
        }
        public static string ConvertSpellTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    if (counter == 1 && counter == rowcount)
                        sqltext += $" ({wcid},{row[0],6},{row[1],7}) /* {row[2]} */;\n";
                    else if (counter == 1)
                        sqltext += $" ({wcid},{row[0],6},{row[1],7}) /* {row[2]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],6},{row[1],7}) /* {row[2]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],6},{row[1],7}) /* {row[2]} */\n";
                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";

            return sqltext;
        }
        public static string ConvertCreateItemsTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    if (counter == 1 && counter == rowcount)
                        sqltext += $" ({wcid},{row[0],2},{row[1],6},{row[2],3},{row[3],2},{row[4],5},{row[5],6}) /* {row[6]} */;\n";
                    else if (counter == 1)
                        sqltext += $" ({wcid},{row[0],2},{row[1],6},{row[2],3},{row[3],2},{row[4],5},{row[5],6}) /* {row[6]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],2},{row[1],6},{row[2],3},{row[3],2},{row[4],5},{row[5],6}) /* {row[6]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],2},{row[1],6},{row[2],3},{row[3],2},{row[4],5},{row[5],6}) /* {row[6]} */\n";
                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";

            return sqltext;
        }
        public static string ConvertBodyPart(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    if (counter == 1)
                        sqltext += $" ({wcid},{row[0],3},{row[1],3},{row[2],3},{row[3],5},{row[4],5}," +   // 6 fields
                                   $"{row[5],5},{row[6],5},{row[7],5},{row[8],5},{row[9],5},{row[10],5},{row[11],5},{row[12],5}," +   // 8 fields
                                   $"{row[13],2}," +
                                   $"{row[14],5},{row[15],5},{row[16],5},{row[17],5},{row[18],5},{row[19],5}," +
                                   $"{row[20],5},{row[21],5},{row[22],5},{row[23],5},{row[24],5},{row[25],5})" +
                                   $" /* {row[26]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],3},{row[1],3},{row[2],3},{row[3],5},{row[4],5}," +   // 6 fields
                                       $"{row[5],5},{row[6],5},{row[7],5},{row[8],5},{row[9],5},{row[10],5},{row[11],5},{row[12],5}," +   // 8 fields
                                       $"{row[13],2}," +
                                       $"{row[14],5},{row[15],5},{row[16],5},{row[17],5},{row[18],5},{row[19],5}," +
                                       $"{row[20],5},{row[21],5},{row[22],5},{row[23],5},{row[24],5},{row[25],5})" +
                                       $" /* {row[26]} */;\n";

                        else
                            sqltext += $"     , ({wcid},{row[0],3},{row[1],3},{row[2],3},{row[3],5},{row[4],5}," +   // 6 fields
                                       $"{row[5],5},{row[6],5},{row[7],5},{row[8],5},{row[9],5},{row[10],5},{row[11],5},{row[12],5}," +   // 8 fields
                                       $"{row[13],2}," +
                                       $"{row[14],5},{row[15],5},{row[16],5},{row[17],5},{row[18],5},{row[19],5}," +
                                       $"{row[20],5},{row[21],5},{row[22],5},{row[23],5},{row[24],5},{row[25],5})" +
                                       $" /* {row[26]} */\n";

                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }
        public static string ConvertBookInfo(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    if (counter == 1)
                        sqltext += $" ({wcid},{row[0],2},{row[1],5});\n";
                    else
                    {
                    }
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }
        public static string ConvertBookPages(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {

                    string aName = "'" + row[2] + "'";
                    string aAccount = "'" + row[3] + "'";
                    string pageText = "'" + row[5] + "'";

                    if (counter == 1 && counter == rowcount)
                        sqltext += $" ({wcid},{row[0],2},{row[1],11}, {aName}, {aAccount}, {row[4]}, {pageText});\n";
                    else if (counter == 1)
                        sqltext += $" ({wcid},{row[0],2},{row[1],11}, {aName}, {aAccount}, {row[4]}, {pageText})\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],2},{row[1],11}, {aName}, {aAccount}, {row[4]}, {pageText});\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],2},{row[1],11}, {aName}, {aAccount}, {row[4]}, {pageText})\n";

                    }
                    counter++;
                }
            }
            //if (rowcount > 0)
            //    sqltext += $"\n";
            //else
            //{
            //}
            return sqltext;
        }
        public static string ConvertPositionTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {

                    long loc = MainWindow.ConvertToLong(row[1].ToString());
                    string locHex = loc.ToString("X");

                    // string toriginX = string.Format("F6", row[2].ToString());
                    float toriginX = MainWindow.ConvertToFloat(row[2].ToString());
                    float toriginY = MainWindow.ConvertToFloat(row[3].ToString());
                    float toriginZ = MainWindow.ConvertToFloat(row[4].ToString());

                    float tAngleW = MainWindow.ConvertToFloat(row[5].ToString());
                    float tAngleX = MainWindow.ConvertToFloat(row[6].ToString());
                    float tAngleY = MainWindow.ConvertToFloat(row[7].ToString());
                    float tAngleZ = MainWindow.ConvertToFloat(row[8].ToString());


                    string originX = toriginX.ToString("F6");
                    string originY = toriginY.ToString("F6");
                    string originZ = toriginZ.ToString("F6");

                    string angleW = tAngleW.ToString("F6");
                    string angleX = tAngleX.ToString("F6");
                    string angleY = tAngleY.ToString("F6");
                    string angleZ = tAngleZ.ToString("F6");

                    if (counter == 1 && counter == rowcount)
                    {
                        sqltext += $" ({wcid},{row[0],2}, 0x{locHex}, {row[2]}, {row[3]}, {row[4]}, {row[5]}, {row[6]}, {row[7]}, {row[8]}) /*{row[9]}*/\n";
                        sqltext += $"/* @teleloc 0x{locHex} [{originX} {originY} {originZ}] {angleW} {angleX} {angleY} {angleZ} */;\n";
                    }
                    else if (counter == 1)
                    {
                        sqltext += $" ({wcid},{row[0],3}, {row[1]}, {row[2]}, {row[3]}, {row[4]}, {row[5]}, {row[6]}, {row[7]}, {row[8]}) /*{row[9]}*/\n";
                        sqltext += $"/* @teleloc 0x{locHex} [{originX} {originY} {originZ}] {angleW} {angleX} {angleY} {angleZ} */\n";
                    }
                    else //Update
                    {
                        if (counter == rowcount)
                        {
                            sqltext += $", ({wcid},{row[0],3}, {row[1]}, {row[2]}, {row[3]}, {row[4]}, {row[5]}, {row[6]}, {row[7]}, {row[8]}) /*{row[9]}*/\n";
                            sqltext += $"/* @teleloc 0x{locHex} [{originX} {originY} {originZ}] {angleW} {angleX} {angleY} {angleZ} */;\n";
                        }
                        else
                        {
                            sqltext += $", ({wcid},{row[0],3}, {row[1]}, {row[2]}, {row[3]}, {row[4]}, {row[5]}, {row[6]}, {row[7]}, {row[8]}) /*{row[9]}*/\n";
                            sqltext += $"/* @teleloc 0x{locHex} [{originX} {originY} {originZ}] {angleW} {angleX} {angleY} {angleZ} */\n";
                        }

                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }
        public static string ConvertIidTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    // Check for Hex
                    string iidValue = "";
                    //int iidValue = 0;
                    string checkHex = row[1].ToString();
                    if (checkHex.Contains("x") || checkHex.Contains("X"))
                    {
                        // iidValue = (int)MainWindow.ConvertToDecimal(checkHex);
                        iidValue = checkHex;
                    }
                    else
                    {                       
                        iidValue = "0x" + MainWindow.ConvertToHex(checkHex);
                    }

                    if (counter == 1 && counter == rowcount)
                        sqltext += $" ({wcid},{row[0],4},{iidValue,11}) /* {row[2]} */;\n";
                    else if (counter == 1)
                        sqltext += $" ({wcid},{row[0],4},{iidValue,11}) /* {row[2]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],4},{iidValue,11}) /* {row[2]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],4},{iidValue,11}) /* {row[2]} */\n";
                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }
        public static string ConvertDidTable(DataTable dt, string wcid, string header)
        {
            string sqltext = "";

            int counter = 1;
            int rowcount = dt.Rows.Count;

            if (rowcount > 0)
            {
                sqltext = header + $"\nVALUES";
                foreach (DataRow row in dt.Rows)
                {
                    int propertyValue = MainWindow.ConvertToInteger(row[0].ToString());
                    // Check for Hex
                    string iidValue = "";
                    //int iidValue = 0;
                    string checkHex = row[1].ToString();
                    if (checkHex.Contains("x") || checkHex.Contains("X"))
                    {
                        // iidValue = (int)MainWindow.ConvertToDecimal(checkHex);
                        iidValue = checkHex;
                    }
                    else
                    {
                        if (IsHexProperty(propertyValue))
                            iidValue = "0x" + MainWindow.ConvertToHex(checkHex);
                        else
                            iidValue = checkHex;
                    }

                    if (counter == 1 && counter == rowcount)
                        sqltext += $" ({wcid},{row[0],4},{iidValue,11}) /* {row[2]} */;\n";
                    else if (counter == 1)
                        sqltext += $" ({wcid},{row[0],4},{iidValue,11}) /* {row[2]} */\n";
                    else
                    {
                        if (counter == rowcount)
                            sqltext += $"     , ({wcid},{row[0],4},{iidValue,11}) /* {row[2]} */;\n";
                        else
                            sqltext += $"     , ({wcid},{row[0],4},{iidValue,11}) /* {row[2]} */\n";
                    }
                    counter++;
                }
            }
            if (rowcount > 0)
                sqltext += $"\n";
            else
            {
            }
            return sqltext;
        }
        public static bool IsHexProperty(int property)
        {
            switch (property)
            {

                case 43:  //PropertyDataId.AccountHouseId:
                case 57:  //PropertyDataId.AlternateCurrency:
                case 56:  //PropertyDataId.AugmentationCreateItem:
                case 54:  //PropertyDataId.AugmentationEffect:
                case 58:  //PropertyDataId.BlueSurgeSpell:
                case 39:  //PropertyDataId.DeathSpell:
                case 35:  //PropertyDataId.DeathTreasureType:
                case 42:  //PropertyDataId.HouseId:
                case 37:  //PropertyDataId.ItemSkillLimit:
                case 41:  //PropertyDataId.ItemSpecializedOnly:
                case 47:  //PropertyDataId.LastPortal:
                case 31:  //PropertyDataId.LinkedPortalOne:
                case 48:  //PropertyDataId.LinkedPortalTwo:
                case 61:  //PropertyDataId.OlthoiDeathTreasureType:
                case 49:  //PropertyDataId.OriginalPortal:
                case 30:  //PropertyDataId.PhysicsScript:
                case 55:  //PropertyDataId.ProcSpell:
                case 60:  //PropertyDataId.RedSurgeSpell:
                case 44:  //PropertyDataId.RestrictionEffect:
                case 28:  //PropertyDataId.Spell:
                case 29:  //PropertyDataId.SpellComponent:
                case 38:  //PropertyDataId.UseCreateItem:
                case 23:  //PropertyDataId.UseSound:
                case 40:  //PropertyDataId.VendorsClassId:
                case 32:  //PropertyDataId.WieldedTreasureType:
                case 59:  //PropertyDataId.YellowSurgeSpell:

                case int i when i >= 8001:
                    return false;


                default:
                    return true;
            }

        }

    }
}
