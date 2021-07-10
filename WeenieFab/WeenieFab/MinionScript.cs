using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WeenieFab
{
    public partial class MainWindow : Window
    {

        public static Dictionary<int, float> minionSpeed = new Dictionary<int, float>();

        public void UpdateMinionDamage()
        {
            AddWeeniesDict();
            ModifyDamage(bodypartsDataTable);
            SaveFile();

            System.Windows.Application.Current.Shutdown();

        }

        public static void ModifyDamage(DataTable dt)
        {
            int minionWCID = ConvertToInteger(Globals.WeenieWCID);
            _ = minionSpeed.TryGetValue(minionWCID, out float attackSpeed);
            float finalAttackSpeed = attackSpeed + (float)0.5;

            // Formula
            // damage * (waspDamagePerSecond / (damage *(1 / finalAttackSpeed)
            foreach (DataRow row in dt.Rows)
            {
                float damage = (int)row[2];
                // float variance = (float)0.5;
                int finalDamage = 0;
                float waspDamagePerSecond = 0;
                float avgDelay = (float)0.5;


                // Wasp Damage Per second Formula
                // (damage * (1 / (Animation Strike Speed + Avg Delay))) * MultiStrike
                // Only 200 wasp swarms have multi strike
                // Avg Delay is the same across all minions per riggs


                if (damage != 0)
                {
                    switch (damage)
                    {
                        case 50:
                        case 80:
                            waspDamagePerSecond = damage *(1 / ((float)0.5 + avgDelay));
                            break;
                        case 100:
                        case 125:
                        case 150:
                        case 180:
                            waspDamagePerSecond = damage * (1 / ((float)0.3 + avgDelay));
                            break;
                        case 200:
                            waspDamagePerSecond = (damage * (1 / ((float)0.3 + avgDelay))) * 3;
                            break;

                        default:
                            finalDamage = (int)Math.Round(damage);
                            waspDamagePerSecond = damage * 1;
                            break;
                    }
                    float calcedDamage = damage * (waspDamagePerSecond / (damage * (1 / finalAttackSpeed)));
                    finalDamage = (int)Math.Round(calcedDamage);

                    row[2] = finalDamage;
                    // row[3] = variance;
                    bodypartsDataTable.AcceptChanges();
                }
            }

        }

        public static void AddWeeniesDict()
        {
            minionSpeed.Add(48943, (float)1.125);
            minionSpeed.Add(48949, (float)0.6);
            minionSpeed.Add(48950, (float)1.1);
            minionSpeed.Add(48951, (float)0.625);
            minionSpeed.Add(48952, (float)0.6);
            minionSpeed.Add(48953, (float)0.6);
            minionSpeed.Add(48955, (float)0.6);
            minionSpeed.Add(48958, (float)0.625);
            minionSpeed.Add(48960, (float)1.4);
            minionSpeed.Add(48962, (float)1.35);
            minionSpeed.Add(48964, (float)0.8);
            minionSpeed.Add(48966, (float)0.7);
            minionSpeed.Add(48968, (float)0.7);
            minionSpeed.Add(48970, (float)0.7);
            minionSpeed.Add(49000, (float)1.125);
            minionSpeed.Add(49003, (float)1.1);
            minionSpeed.Add(49004, (float)0.625);
            minionSpeed.Add(49005, (float)0.6);
            minionSpeed.Add(49006, (float)0.6);
            minionSpeed.Add(49007, (float)0.6);
            minionSpeed.Add(49008, (float)0.6);
            minionSpeed.Add(49009, (float)1.125);
            minionSpeed.Add(49010, (float)1.1);
            minionSpeed.Add(49011, (float)0.625);
            minionSpeed.Add(49012, (float)0.6);
            minionSpeed.Add(49013, (float)0.6);
            minionSpeed.Add(49014, (float)0.6);
            minionSpeed.Add(49015, (float)0.6);
            minionSpeed.Add(49016, (float)1.125);
            minionSpeed.Add(49017, (float)1.1);
            minionSpeed.Add(49018, (float)0.625);
            minionSpeed.Add(49019, (float)0.6);
            minionSpeed.Add(49020, (float)0.6);
            minionSpeed.Add(49021, (float)0.6);
            minionSpeed.Add(49022, (float)0.6);
            minionSpeed.Add(49023, (float)1.125);
            minionSpeed.Add(49024, (float)1.1);
            minionSpeed.Add(49025, (float)0.625);
            minionSpeed.Add(49026, (float)0.6);
            minionSpeed.Add(49027, (float)0.6);
            minionSpeed.Add(49028, (float)0.6);
            minionSpeed.Add(49029, (float)0.6);
            minionSpeed.Add(49030, (float)0.6);
            minionSpeed.Add(49031, (float)1.4);
            minionSpeed.Add(49032, (float)1.35);
            minionSpeed.Add(49033, (float)0.8);
            minionSpeed.Add(49034, (float)0.7);
            minionSpeed.Add(49035, (float)0.7);
            minionSpeed.Add(49036, (float)0.7);
            minionSpeed.Add(49037, (float)0.6);
            minionSpeed.Add(49038, (float)1.4);
            minionSpeed.Add(49039, (float)1.35);
            minionSpeed.Add(49040, (float)0.8);
            minionSpeed.Add(49041, (float)0.7);
            minionSpeed.Add(49042, (float)0.7);
            minionSpeed.Add(49043, (float)0.7);
            minionSpeed.Add(49044, (float)0.6);
            minionSpeed.Add(49045, (float)1.4);
            minionSpeed.Add(49046, (float)1.35);
            minionSpeed.Add(49047, (float)0.8);
            minionSpeed.Add(49048, (float)0.7);
            minionSpeed.Add(49049, (float)0.7);
            minionSpeed.Add(49050, (float)0.7);
            minionSpeed.Add(49051, (float)0.825);
            minionSpeed.Add(49052, (float)1.7);
            minionSpeed.Add(49053, (float)1.625);
            minionSpeed.Add(49054, (float)0.925);
            minionSpeed.Add(49055, (float)0.825);
            minionSpeed.Add(49056, (float)0.825);
            minionSpeed.Add(49057, (float)0.825);
            minionSpeed.Add(49058, (float)0.825);
            minionSpeed.Add(49059, (float)1.7);
            minionSpeed.Add(49060, (float)1.625);
            minionSpeed.Add(49061, (float)0.925);
            minionSpeed.Add(49062, (float)0.825);
            minionSpeed.Add(49063, (float)0.825);
            minionSpeed.Add(49064, (float)0.825);
            minionSpeed.Add(49065, (float)0.825);
            minionSpeed.Add(49066, (float)1.7);
            minionSpeed.Add(49067, (float)1.625);
            minionSpeed.Add(49068, (float)0.925);
            minionSpeed.Add(49069, (float)0.825);
            minionSpeed.Add(49070, (float)0.825);
            minionSpeed.Add(49071, (float)0.825);
            minionSpeed.Add(49072, (float)0.825);
            minionSpeed.Add(49073, (float)1.7);
            minionSpeed.Add(49074, (float)1.625);
            minionSpeed.Add(49075, (float)0.925);
            minionSpeed.Add(49076, (float)0.825);
            minionSpeed.Add(49077, (float)0.825);
            minionSpeed.Add(49078, (float)0.825);
            minionSpeed.Add(49079, (float)1.2);
            minionSpeed.Add(49080, (float)2.4);
            minionSpeed.Add(49081, (float)2.4);
            minionSpeed.Add(49082, (float)1.4);
            minionSpeed.Add(49083, (float)1.2);
            minionSpeed.Add(49084, (float)1.2);
            minionSpeed.Add(49085, (float)1.2);
            minionSpeed.Add(49086, (float)1.2);
            minionSpeed.Add(49087, (float)2.4);
            minionSpeed.Add(49088, (float)2.4);
            minionSpeed.Add(49089, (float)1.4);
            minionSpeed.Add(49090, (float)1.2);
            minionSpeed.Add(49091, (float)1.2);
            minionSpeed.Add(49092, (float)1.2);
            minionSpeed.Add(49093, (float)1.2);
            minionSpeed.Add(49094, (float)2.4);
            minionSpeed.Add(49095, (float)2.4);
            minionSpeed.Add(49096, (float)1.4);
            minionSpeed.Add(49097, (float)1.2);
            minionSpeed.Add(49098, (float)1.2);
            minionSpeed.Add(49099, (float)1.2);
            minionSpeed.Add(49100, (float)1.2);
            minionSpeed.Add(49101, (float)2.4);
            minionSpeed.Add(49102, (float)2.4);
            minionSpeed.Add(49103, (float)1.4);
            minionSpeed.Add(49104, (float)1.2);
            minionSpeed.Add(49105, (float)1.2);
            minionSpeed.Add(49106, (float)1.2);
            minionSpeed.Add(49107, (float)0.666666667);
            minionSpeed.Add(49108, (float)1.333333333);
            minionSpeed.Add(49109, (float)1.3);
            minionSpeed.Add(49110, (float)0.766666667);
            minionSpeed.Add(49111, (float)0.666666667);
            minionSpeed.Add(49112, (float)0.666666667);
            minionSpeed.Add(49113, (float)0.666666667);
            minionSpeed.Add(49114, (float)0.666666667);
            minionSpeed.Add(49115, (float)1.333333333);
            minionSpeed.Add(49116, (float)1.3);
            minionSpeed.Add(49117, (float)0.766666667);
            minionSpeed.Add(49118, (float)0.666666667);
            minionSpeed.Add(49119, (float)0.666666667);
            minionSpeed.Add(49120, (float)0.666666667);
            minionSpeed.Add(49121, (float)0.666666667);
            minionSpeed.Add(49122, (float)1.333333333);
            minionSpeed.Add(49123, (float)1.3);
            minionSpeed.Add(49124, (float)0.766666667);
            minionSpeed.Add(49125, (float)0.666666667);
            minionSpeed.Add(49126, (float)0.666666667);
            minionSpeed.Add(49127, (float)0.666666667);
            minionSpeed.Add(49128, (float)0.666666667);
            minionSpeed.Add(49129, (float)1.333333333);
            minionSpeed.Add(49130, (float)1.3);
            minionSpeed.Add(49131, (float)0.766666667);
            minionSpeed.Add(49132, (float)0.666666667);
            minionSpeed.Add(49133, (float)0.666666667);
            minionSpeed.Add(49134, (float)0.666666667);
            minionSpeed.Add(49163, (float)0.6);
            minionSpeed.Add(49164, (float)1.125);
            minionSpeed.Add(49165, (float)1.1);
            minionSpeed.Add(49166, (float)0.625);
            minionSpeed.Add(49167, (float)0.6);
            minionSpeed.Add(49168, (float)0.6);
            minionSpeed.Add(49169, (float)0.6);
            minionSpeed.Add(49170, (float)0.6);
            minionSpeed.Add(49171, (float)1.125);
            minionSpeed.Add(49172, (float)1.1);
            minionSpeed.Add(49173, (float)0.625);
            minionSpeed.Add(49174, (float)0.6);
            minionSpeed.Add(49175, (float)0.6);
            minionSpeed.Add(49176, (float)0.6);
            minionSpeed.Add(49177, (float)0.6);
            minionSpeed.Add(49178, (float)1.125);
            minionSpeed.Add(49179, (float)1.1);
            minionSpeed.Add(49180, (float)0.625);
            minionSpeed.Add(49181, (float)0.6);
            minionSpeed.Add(49182, (float)0.6);
            minionSpeed.Add(49183, (float)0.6);
            minionSpeed.Add(49184, (float)0.7);
            minionSpeed.Add(49185, (float)1.433333333);
            minionSpeed.Add(49186, (float)1.433333333);
            minionSpeed.Add(49187, (float)0.8);
            minionSpeed.Add(49188, (float)0.766666667);
            minionSpeed.Add(49189, (float)0.766666667);
            minionSpeed.Add(49190, (float)0.766666667);
            minionSpeed.Add(49191, (float)0.7);
            minionSpeed.Add(49192, (float)1.433333333);
            minionSpeed.Add(49193, (float)1.433333333);
            minionSpeed.Add(49194, (float)0.8);
            minionSpeed.Add(49195, (float)0.766666667);
            minionSpeed.Add(49196, (float)0.766666667);
            minionSpeed.Add(49197, (float)0.766666667);
            minionSpeed.Add(49198, (float)0.7);
            minionSpeed.Add(49199, (float)1.433333333);
            minionSpeed.Add(49200, (float)1.433333333);
            minionSpeed.Add(49201, (float)0.8);
            minionSpeed.Add(49202, (float)0.766666667);
            minionSpeed.Add(49203, (float)0.766666667);
            minionSpeed.Add(49204, (float)0.766666667);
            minionSpeed.Add(49205, (float)0.7);
            minionSpeed.Add(49206, (float)1.433333333);
            minionSpeed.Add(49207, (float)1.433333333);
            minionSpeed.Add(49208, (float)0.8);
            minionSpeed.Add(49209, (float)0.766666667);
            minionSpeed.Add(49210, (float)0.766666667);
            minionSpeed.Add(49211, (float)0.766666667);
            minionSpeed.Add(49393, (float)0.675);
            minionSpeed.Add(49394, (float)1.8);
            minionSpeed.Add(49395, (float)1.725);
            minionSpeed.Add(49396, (float)1.025);
            minionSpeed.Add(49397, (float)0.9);
            minionSpeed.Add(49398, (float)0.9);
            minionSpeed.Add(49399, (float)0.9);
            minionSpeed.Add(49400, (float)0.675);
            minionSpeed.Add(49401, (float)1.8);
            minionSpeed.Add(49402, (float)1.725);
            minionSpeed.Add(49403, (float)1.025);
            minionSpeed.Add(49404, (float)0.9);
            minionSpeed.Add(49405, (float)0.9);
            minionSpeed.Add(49406, (float)0.9);
            minionSpeed.Add(49407, (float)0.675);
            minionSpeed.Add(49408, (float)1.8);
            minionSpeed.Add(49409, (float)1.725);
            minionSpeed.Add(49410, (float)1.025);
            minionSpeed.Add(49411, (float)0.9);
            minionSpeed.Add(49412, (float)0.9);
            minionSpeed.Add(49413, (float)0.9);
            minionSpeed.Add(49414, (float)0.675);
            minionSpeed.Add(49415, (float)1.8);
            minionSpeed.Add(49416, (float)1.725);
            minionSpeed.Add(49417, (float)1.025);
            minionSpeed.Add(49418, (float)0.9);
            minionSpeed.Add(49419, (float)0.9);
            minionSpeed.Add(49420, (float)0.9);



        }






    }
}
