using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WeenieFab.Properties;

namespace WeenieFab
{
    public partial class MainWindow : Window
    {
        private void dgInt32_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgInt32.SelectedIndex;
            DataGridRow currentRowIndex = dgInt32.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > integerDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = integerDataTable.Rows[currentRowIndex.GetIndex()];
                int cbindex = 0;
                Int32.TryParse(dr[0].ToString(), out cbindex);
                cbInt32Props.SelectedIndex = cbindex;
                tbValue.Text = dr[1].ToString();
            }
        }
        private void dgInt64_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgInt64.SelectedIndex;
            DataGridRow currentRowIndex = dgInt64.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > integer64DataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = integer64DataTable.Rows[currentRowIndex.GetIndex()];
                int cbindex = 0;
                Int32.TryParse(dr[0].ToString(), out cbindex);
                cbInt64Props.SelectedIndex = cbindex;
                tb64Value.Text = dr[1].ToString();
            }
        }
        private void dgBool_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgBool.SelectedIndex;
            DataGridRow currentRowIndex = dgBool.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > boolDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = boolDataTable.Rows[currentRowIndex.GetIndex()];
                int cbindex = 0;
                Int32.TryParse(dr[0].ToString(), out cbindex);
                cbBoolProps.SelectedIndex = cbindex;
                if (dr[1].ToString() == "True")
                    rbTrue.IsChecked = true;
                else
                    rbFalse.IsChecked = true;
            }
        }
        private void dgFloat_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgFloat.SelectedIndex;
            DataGridRow currentRowIndex = dgFloat.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > floatDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = floatDataTable.Rows[currentRowIndex.GetIndex()];
                int cbindex = 0;
                Int32.TryParse(dr[0].ToString(), out cbindex);
                cbFloatProps.SelectedIndex = cbindex;
                tbFloatValue.Text = dr[1].ToString();
            }
        }

        private void dgString_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgString.SelectedIndex;
            DataGridRow currentRowIndex = dgString.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > stringDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = stringDataTable.Rows[currentRowIndex.GetIndex()];
                int cbindex = 0;
                Int32.TryParse(dr[0].ToString(), out cbindex);
                cbStringProps.SelectedIndex = cbindex;
                tbStringValue.Text = dr[1].ToString();
            }
        }

        private void dgDiD_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgDiD.SelectedIndex;
            DataGridRow currentRowIndex = dgDiD.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > didDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = didDataTable.Rows[currentRowIndex.GetIndex()];
                int cbindex = 0;
                Int32.TryParse(dr[0].ToString(), out cbindex);
                cbDiDProps.SelectedIndex = cbindex;
                tbDiDValue.Text = dr[1].ToString();
            }
        }

        private void dgSpell_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgSpell.SelectedIndex;
            DataGridRow currentRowIndex = dgSpell.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > spellDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = spellDataTable.Rows[currentRowIndex.GetIndex()];
                tbSpellId.Text = dr[0].ToString();
                tbSpellValue.Text = dr[1].ToString();
                tbSpellDescription.Text = dr[2].ToString();
            }
        }

        private void dgSkills_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgSkills.SelectedIndex;
            DataGridRow currentRowIndex = dgSkills.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > skillsDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = skillsDataTable.Rows[currentRowIndex.GetIndex()];
                cbSkillType.SelectedIndex = ConvertToInteger(dr[0].ToString());
                if (dr[2].ToString() == "3")
                    rdbSpec.IsChecked = true;
                else
                    rdbTrained.IsChecked = true;
                tbSkillLevel.Text = dr[4].ToString();
                SelectedFinalSkillCalc();
            }
        }
        private void dgCreateList_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgCreateItems.SelectedIndex;
            DataGridRow currentRowIndex = dgCreateItems.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > createListDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = createListDataTable.Rows[currentRowIndex.GetIndex()];

                tbCreateItemsDestType.Text = dr[0].ToString();
                tbCreateItemsWCID.Text = dr[1].ToString();
                tbCreateItemsStackSize.Text = dr[2].ToString();
                tbCreateItemsPalette.Text = dr[3].ToString();
                tbCreateItemsDropRate.Text = dr[4].ToString();
                tbCreateItemsDescription.Text = dr[6].ToString();
            }
        }
        private void dgBodyParts_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgBodyParts.SelectedIndex;
            DataGridRow currentRowIndex = dgBodyParts.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > bodypartsDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = bodypartsDataTable.Rows[currentRowIndex.GetIndex()];
                cbBodyPart.SelectedIndex = ConvertToInteger(dr[0].ToString());
                int damageType = ConvertToInteger(dr[1].ToString());
                switch (damageType)
                {
                    case 0:
                        cbBodyPartDamageType.SelectedIndex = 0;
                        break;
                    case 1:
                        cbBodyPartDamageType.SelectedIndex = 1;
                        break;
                    case 2:
                        cbBodyPartDamageType.SelectedIndex = 2;
                        break;
                    case 4:
                        cbBodyPartDamageType.SelectedIndex = 3;
                        break;
                    case 8:
                        cbBodyPartDamageType.SelectedIndex = 4;
                        break;
                    case 16:
                        cbBodyPartDamageType.SelectedIndex = 5;
                        break;
                    case 32:
                        cbBodyPartDamageType.SelectedIndex = 6;
                        break;
                    case 64:
                        cbBodyPartDamageType.SelectedIndex = 7;
                        break;
                    case 128:
                        cbBodyPartDamageType.SelectedIndex = 8;
                        break;
                    case 256:
                        cbBodyPartDamageType.SelectedIndex = 9;
                        break;
                    case 512:
                        cbBodyPartDamageType.SelectedIndex = 10;
                        break;
                    case 1024:
                        cbBodyPartDamageType.SelectedIndex = 11;
                        break;
                    case 268435456:
                        cbBodyPartDamageType.SelectedIndex = 12;
                        break;
                    default:
                        break;
                }

                tbBodyPartDamageValue.Text = dr[2].ToString();
                tbBodyPartDamageVariance.Text = dr[3].ToString();

                tbBodyPartArmorLevel.Text = dr[4].ToString();
                tbBodyPartArmorLevelSlash.Text = dr[5].ToString();
                tbBodyPartArmorLevelPierce.Text = dr[6].ToString();
                tbBodyPartArmorLevelBludgeon.Text = dr[7].ToString();
                tbBodyPartArmorLevelCold.Text = dr[8].ToString();
                tbBodyPartArmorLevelFire.Text = dr[9].ToString();
                tbBodyPartArmorLevelAcid.Text = dr[10].ToString();
                tbBodyPartArmorLevelElectric.Text = dr[11].ToString();
                tbBodyPartArmorLevelNether.Text = dr[12].ToString();

                tbBodyPartBase_Height.Text = dr[13].ToString();

                tbBodyPartQuadHighLF.Text = dr[14].ToString();
                tbBodyPartQuadMiddleLF.Text = dr[15].ToString();
                tbBodyPartQuadLowLF.Text = dr[16].ToString();

                tbBodyPartQuadHighRF.Text = dr[17].ToString();
                tbBodyPartQuadMiddleRF.Text = dr[18].ToString();
                tbBodyPartQuadLowRF.Text = dr[19].ToString();

                tbBodyPartQuadHighLB.Text = dr[20].ToString();
                tbBodyPartQuadMiddleLB.Text = dr[21].ToString();
                tbBodyPartQuadLowLB.Text = dr[22].ToString();

                tbBodyPartQuadHighRB.Text = dr[23].ToString();
                tbBodyPartQuadMiddleRB.Text = dr[24].ToString();
                tbBodyPartQuadLowRB.Text = dr[25].ToString();
            }
        }
        // Books
        private void dgBookInfo_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgBookInfo.SelectedIndex;
            DataGridRow currentRowIndex = dgBookInfo.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > bookInfoDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = bookInfoDataTable.Rows[currentRowIndex.GetIndex()];
                tbMaxPages.Text = dr[0].ToString();
                tbMaxChars.Text = dr[1].ToString();
            }
        }
        private void dgBookPages_RowSelected(object sender, RoutedEventArgs e)
        {

            var index = dgBookPages.SelectedIndex;
            DataGridRow currentRowIndex = dgBookPages.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > bookPagesDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = bookPagesDataTable.Rows[currentRowIndex.GetIndex()];
                tbPageID.Text = dr[0].ToString();
                tbAuthorName.Text = dr[2].ToString();

                if (dr[4].ToString() == "True")
                    rdbBookTrue.IsChecked = true;
                else
                    rdbBookFalse.IsChecked = true;

                tbPageText.Text = dr[5].ToString();
            }
        }
        // Instance IDs
        private void dgIid_RowSelected(object sender, RoutedEventArgs e)
        {

            var index = dgIid.SelectedIndex;
            DataGridRow currentRowIndex = dgIid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > iidDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = iidDataTable.Rows[currentRowIndex.GetIndex()];
                cbIidProps.SelectedIndex = ConvertToInteger(dr[0].ToString());
                tbiidValue.Text = dr[1].ToString();
            }
        }
        // Positions
        private void dgPosition_RowSelected(object sender, RoutedEventArgs e)
        {
            var index = dgPosition.SelectedIndex;
            DataGridRow currentRowIndex = dgPosition.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > positionsDataTable.Rows.Count)
            {
            }
            else
            {
                DataRow dr = positionsDataTable.Rows[currentRowIndex.GetIndex()];
                cbPosition.SelectedIndex = ConvertToInteger(dr[0].ToString());
                
                tbCellID.Text = dr[1].ToString();
                tbOriginX.Text = dr[2].ToString();
                tbOriginY.Text = dr[3].ToString(); 
                tbOriginZ.Text = dr[4].ToString();

                tbAngleW.Text = dr[5].ToString();
                tbAngleX.Text = dr[6].ToString();
                tbAngleY.Text = dr[7].ToString();
                tbAngleZ.Text = dr[8].ToString();
            }
        }
        // Update Attribs Events      
        private void updateAttribs()  // Updates Attribs - May need a better way to do this.
        {
            int i = 1;
            foreach (DataRow row in attributeDataTable.Rows)
            {
                if (i == 1)
                    tbAttribStrength.Text = row[1].ToString();
                else if (i == 2)
                    tbAttribEndurance.Text = row[1].ToString();
                else if (i == 3)
                    tbAttribQuickness.Text = row[1].ToString();
                else if (i == 4)
                    tbAttribCoordination.Text = row[1].ToString();
                else if (i == 5)
                    tbAttribFocus.Text = row[1].ToString();
                else if (i == 6)
                    tbAttribSelf.Text = row[1].ToString();
                i++;
            }
        }
        private void updateAttribs2()  // Updates Health, Stamina, Mana
        {
            int i = 1;
            foreach (DataRow row in attribute2DataTable.Rows)
            {
                if (i == 1)
                {
                    tbHealthInitLevel.Text = row[1].ToString();
                    tbHealthCurrentLevel.Text = row[4].ToString();
                }
                else if (i == 2)
                {
                    tbStaminaInitLevel.Text = row[1].ToString();
                    tbStaminaCurrentLevel.Text = row[4].ToString();
                }

                else if (i == 3)
                {
                    tbManaInitLevel.Text = row[1].ToString();
                    tbManaCurrentLevel.Text = row[4].ToString();
                }
                i++;
            }
        }
        public void SelectedFinalSkillCalc()
        {
            // Skill Formulas based on Attribs

            int strength = ConvertToInteger(tbAttribStrength.Text);
            int endur = ConvertToInteger(tbAttribEndurance.Text);
            int coord = ConvertToInteger(tbAttribCoordination.Text);
            int quick = ConvertToInteger(tbAttribQuickness.Text);
            int focus = ConvertToInteger(tbAttribFocus.Text);
            int self = ConvertToInteger(tbAttribSelf.Text);


            if (WeenieFabUser.Default.AutoCalcSkill == true)
            {
                switch (cbSkillType.SelectedIndex)
                {

                    case 6:  // MeleeD
                    case 46: // Finesse Weapons
                    case 51: // Sneak Attack
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((quick + coord) / 3)).ToString();
                        break;
                    case 7:  // MissileD
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((quick + coord) / 5)).ToString();
                        break;
                    case 14:  // Arcane Lore
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + (focus / 3)).ToString();
                        break;
                    case 15:  // Magic D
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((focus + self) / 7)).ToString();
                        break;
                    case 16:  // Mana C
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((focus + self) / 6)).ToString();
                        break;
                    case 18:  // Item Appraisal - Item Tink
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((focus + coord) / 2)).ToString();
                        break;
                    case 19:  // Personal Appraisal - Assess Person
                    case 20:  // Deception
                    case 27:  // Creature Appraisal - Assess Creature
                    case 35:  // Leadership
                    case 36:  // Loyalty
                    case 40:  // Salvaging
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text)).ToString();
                        break;
                    case 21:  // Healing
                    case 23:  // Lockpick
                    case 37:  // Fletching
                    case 38:  // Alchemy
                    case 39:  // Cooking
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((focus + coord) / 3)).ToString();
                        break;
                    case 22:  // Jump
                    case 48:  // Shield
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((strength + coord) / 2)).ToString();
                        break;
                    case 24:  // Run
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + quick).ToString();
                        break;
                    case 28:  // Weapon Appraisal - Weapon Tink
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((focus + strength) / 2)).ToString();
                        break;
                    case 29:  // Armor Appraisal - Armor Tink
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((focus + endur) / 2)).ToString();
                        break;
                    case 30:  // Magic Item Appraisal - Magic Item Tink
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + focus).ToString();
                        break;
                    case 31:  // Creature Magic
                    case 32:  // Item Magic
                    case 33:  // Life Magic
                    case 34:  // War Magic
                    case 43:  // Void Magic
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((focus + self) / 4)).ToString();
                        break;
                    case 41:  // Two Hand
                    case 44:  // Heavy Weapons
                    case 45:  // Light Weapons
                    case 52:  // Dirty Fighting
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((strength + coord) / 3)).ToString();
                        break;
                    case 47:  // Missile Weapons
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + coord / 2).ToString();
                        break;
                    case 49:  // Dual Wield
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((coord * 2) / 3)).ToString();
                        break;
                    case 50:  // Recklessness
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((strength + quick) / 3)).ToString();
                        break;
                    case 54:  // Summoning
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((endur + self) / 3)).ToString();
                        break;
                    case 1: // Axe
                    case 5: // Mace
                    case 9: // Spear
                    case 10: // Staff
                    case 11: // Sword
                    case 13: // Unarmed Combat
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((strength + coord) / 3)).ToString();
                        break;
                    case 2: // Bow
                    case 3: // Crossbow
                    case 12: // Thrown Weapon
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + coord / 2).ToString();
                        break;
                    case 4: // Dagger
                        tbSkillFinalLevel.Text = (ConvertToInteger(tbSkillLevel.Text) + ((quick + coord) / 3)).ToString();
                        break;
                    // Ignored (Unused)
                    case 0:
                    case 8:
                    case 17:
                    case 25:
                    case 26:
                    case 42:
                    case 53:
                    default:
                        break;
                }
            }
        }
    }
}
