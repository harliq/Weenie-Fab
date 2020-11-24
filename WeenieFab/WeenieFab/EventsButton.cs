using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WeenieFab
{
    public partial class MainWindow : Window
    {
        // Button Events
        // ****Toolbar Buttons****
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButton buttons = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show("Save current Weenie?", "Possible Unsaved Changes", buttons, icon);
            if (result == MessageBoxResult.Yes)
            {
                SaveFile();
                ClearAllDataTables();
                ClearAllFields();
                this.Title = "WeenieFab";
            }
            else if (result == MessageBoxResult.No)
            {
                ClearAllDataTables();
                ClearAllFields();
                this.Title = "WeenieFab";
            }
            else
            {
            }

            txtBlockFileStatus.Text = "New File not saved";
            Globals.WeenieFileName = "";
        }
        private void btnOpenSqlFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }
        private void btnSaveSqlFile_Click(object sender, RoutedEventArgs e)
        {
            ProgressBarAnimation();
            SaveFile();
            ProgressBarClearAnimation();
        }
        private void btnSaveAsSqlFile_Click(object sender, RoutedEventArgs e)
        {          
            SaveFileAs();
            ProgressBarClearAnimation();
        }
        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            Options winOptions = new Options();
            winOptions.Owner = this;
            winOptions.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Converter winConverter = new Converter();
            winConverter.Owner = this;
            winConverter.Show();
        }

        private void btnLootProfiler_Click(object sender, RoutedEventArgs e)
        {

            LootProfiler lootProfiler = new LootProfiler();
            lootProfiler.Owner = this;
            lootProfiler.Show();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            Help winHelp = new Help();
            winHelp.Owner = this;
            winHelp.Show();
        }
        // Form Buttons
        // Integer 32
        private void btnAddInt32_Click(object sender, RoutedEventArgs e)
        {
            // search for duplicate property
            if (SearchForDuplicateProps(integerDataTable, cbInt32Props.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbInt32Props.Text.Split(" ");
                DataRow dr = integerDataTable.NewRow();
                dr[0] = ConvertToInteger(cbInt32Props.SelectedIndex.ToString());
                dr[1] = ConvertToInteger(tbValue.Text);
                dr[2] = description[1];

                integerDataTable.Rows.Add(dr);
                integerDataTable = ResortDataTable(integerDataTable, "Property", "ASC");
                dgInt32.DataContext = integerDataTable.DefaultView;
                dgInt32.Items.Refresh();
            }
        }           
        private void btnInt32Remove_Click(object sender, RoutedEventArgs e)
        {
            var index = dgInt32.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgInt32.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = integerDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                integerDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
            dgInt32.Items.Refresh();
        }
        private void btnUpdateInt32_Click(object sender, RoutedEventArgs e)
        {
            var index = dgInt32.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }
            try
            {
                DataGridRow currentRowIndex = dgInt32.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = integerDataTable.Rows[currentRowIndex.GetIndex()];

                string[] description = cbInt32Props.Text.Split(" ");

                dr[0] = ConvertToInteger(cbInt32Props.SelectedIndex.ToString());
                dr[1] = ConvertToInteger(tbValue.Text);
                dr[2] = description[1];

                integerDataTable.AcceptChanges();
                dgInt32.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }
        }
        // Integer 64
        private void btnAddInt64_Click(object sender, RoutedEventArgs e)
        {
            if (SearchForDuplicateProps(integer64DataTable, cbInt64Props.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbInt64Props.Text.Split(" ");
                DataRow dr = integer64DataTable.NewRow();
                dr[0] = ConvertToInteger(cbInt64Props.SelectedIndex.ToString());
                dr[1] = ConvertToInteger(tb64Value.Text);
                dr[2] = description[1];

                integer64DataTable.Rows.Add(dr);
                integer64DataTable = ResortDataTable(integer64DataTable, "Property", "ASC");
                dgInt64.DataContext = integer64DataTable.DefaultView;
                dgInt64.Items.Refresh();               
            }
        }
        private void btnUpdateInt64_Click(object sender, RoutedEventArgs e)
        {
            var index = dgInt64.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }
            try
            {
                DataGridRow currentRowIndex = dgInt64.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = integer64DataTable.Rows[currentRowIndex.GetIndex()];

                string[] description = cbInt64Props.Text.Split(" ");

                decimal test = ConvertToDecimal(tb64Value.Text);

                dr[0] = ConvertToInteger(cbInt64Props.SelectedIndex.ToString());
                dr[1] = ConvertToDecimal(tb64Value.Text);
                dr[2] = description[1];

                integer64DataTable.AcceptChanges();
                dgInt64.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
                // MessageBox.Show($"{ex.Message} \n {ex.StackTrace} \n {ex.Source} \n {ex.TargetSite}");
            }
        }
        private void btnRemoveInt64_Click(object sender, RoutedEventArgs e)
        {
            var index = dgInt64.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgInt64.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = integer64DataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                integer64DataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
        }
        // Boolean
        private void btnAddBool_Click(object sender, RoutedEventArgs e)
        {

            if (SearchForDuplicateProps(boolDataTable, cbBoolProps.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbBoolProps.Text.Split(" ");
                DataRow dr = boolDataTable.NewRow();
                dr[0] = ConvertToInteger(cbBoolProps.SelectedIndex.ToString());
                dr[1] = rbTrue.IsChecked;
                dr[2] = description[1];

                boolDataTable.Rows.Add(dr);
                boolDataTable = ResortDataTable(boolDataTable, "Property", "ASC");
                dgBool.DataContext = boolDataTable.DefaultView;
                dgBool.Items.Refresh();

            }
        }
        private void btnUpdateBool_Click(object sender, RoutedEventArgs e)
        {
            var index = dgBool.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }
            try
            {
                DataGridRow currentRowIndex = dgBool.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = boolDataTable.Rows[currentRowIndex.GetIndex()];

                string[] description = cbBoolProps.Text.Split(" ");

                dr[0] = ConvertToInteger(cbBoolProps.SelectedIndex.ToString());
                dr[1] = rbTrue.IsChecked;
                dr[2] = description[1];

                boolDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }
        }
        private void btnRemoveBool_Click(object sender, RoutedEventArgs e)
        {
            var index = dgBool.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgBool.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = boolDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                boolDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
        }
        // Float
        private void btnAddFloat_Click(object sender, RoutedEventArgs e)
        {

            if (SearchForDuplicateProps(floatDataTable, cbFloatProps.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbFloatProps.Text.Split(" ");
                DataRow dr = floatDataTable.NewRow();
                dr[0] = ConvertToInteger(cbFloatProps.SelectedIndex.ToString());
                dr[1] = ConvertToFloat(tbFloatValue.Text);
                dr[2] = description[1];

                floatDataTable.Rows.Add(dr);
                floatDataTable = ResortDataTable(floatDataTable, "Property", "ASC");
                dgFloat.DataContext = floatDataTable.DefaultView;
                dgFloat.Items.Refresh();
            }
        }
        private void btnUpdateFloat_Click(object sender, RoutedEventArgs e)
        {
            var index = dgFloat.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }
            try
            {
                DataGridRow currentRowIndex = dgFloat.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = floatDataTable.Rows[currentRowIndex.GetIndex()];

                string[] description = cbFloatProps.Text.Split(" ");

                dr[0] = ConvertToInteger(cbFloatProps.SelectedIndex.ToString());
                dr[1] = ConvertToFloat(tbFloatValue.Text);
                dr[2] = description[1];

                floatDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }
        }
        private void btnRemoveFloat_Click(object sender, RoutedEventArgs e)
        {
            var index = dgFloat.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgFloat.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = floatDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                floatDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
        }

        // Strings
        private void btnAddString_Click(object sender, RoutedEventArgs e)
        {
            // search for duplicate property
            if (SearchForDuplicateProps(stringDataTable, cbStringProps.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbStringProps.Text.Split(" ");
                DataRow dr = stringDataTable.NewRow();
                dr[0] = ConvertToInteger(cbStringProps.SelectedIndex.ToString());
                dr[1] = tbStringValue.Text;
                dr[2] = description[1];

                stringDataTable.Rows.Add(dr);

                stringDataTable = ResortDataTable(stringDataTable, "Property", "ASC");
                dgString.DataContext = stringDataTable.DefaultView;
                dgString.Items.Refresh();
            }
        }
        private void btnUpdateString_Click(object sender, RoutedEventArgs e)
        {
            var index = dgString.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }
            try
            {
                DataGridRow currentRowIndex = dgString.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = stringDataTable.Rows[currentRowIndex.GetIndex()];

                string[] description = cbStringProps.Text.Split(" ");

                dr[0] = ConvertToInteger(cbStringProps.SelectedIndex.ToString());
                dr[1] = tbStringValue.Text;
                dr[2] = description[1];

                stringDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }
        }
        private void btnRemoveString_Click(object sender, RoutedEventArgs e)
        {
            var index = dgString.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgString.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = stringDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                stringDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
        }
        // DiD
        private void btnAddDiD_Click(object sender, RoutedEventArgs e)
        {
            // search for duplicate property
            if (SearchForDuplicateProps(didDataTable, cbDiDProps.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbDiDProps.Text.Split(" ");
                DataRow dr = didDataTable.NewRow();
                dr[0] = ConvertToInteger(cbDiDProps.SelectedIndex.ToString());
                dr[1] = ConvertToInteger(tbDiDValue.Text);
                dr[2] = description[1];

                didDataTable.Rows.Add(dr);
                didDataTable = ResortDataTable(didDataTable, "Property", "ASC");
                dgDiD.DataContext = didDataTable.DefaultView;
                dgDiD.Items.Refresh();
             
            }
        }
        private void btnUpdateDiD_Click(object sender, RoutedEventArgs e)
        {
            var index = dgDiD.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }
            try
            {
                DataGridRow currentRowIndex = dgDiD.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = didDataTable.Rows[currentRowIndex.GetIndex()];

                string[] description = cbDiDProps.Text.Split(" ");

                dr[0] = ConvertToInteger(cbDiDProps.SelectedIndex.ToString());
                dr[1] = ConvertToInteger(tbDiDValue.Text);
                dr[2] = description[1];

                didDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }
        }
        private void btnRemoveDiD_Click(object sender, RoutedEventArgs e)
        {
            var index = dgDiD.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgDiD.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = didDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                didDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
        }
        // Instance ID
        private void btnAddIid_Click(object sender, RoutedEventArgs e)
        {
            if (SearchForDuplicateProps(iidDataTable, cbIidProps.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbIidProps.Text.Split(" ");
                DataRow dr = iidDataTable.NewRow();
                dr[0] = ConvertToInteger(cbIidProps.SelectedIndex.ToString());
                dr[1] = ConvertToInteger(tbiidValue.Text);
                dr[2] = description[1];

                iidDataTable.Rows.Add(dr);
                iidDataTable = ResortDataTable(iidDataTable, "Property", "ASC");
                dgIid.DataContext = iidDataTable.DefaultView;
                dgIid.Items.Refresh();

            }
        }
        private void btnUpdateIid_Click(object sender, RoutedEventArgs e)
        {
            var index = dgIid.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }
            try
            {
                DataGridRow currentRowIndex = dgIid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = iidDataTable.Rows[currentRowIndex.GetIndex()];

                string[] description = cbIidProps.Text.Split(" ");

                dr[0] = ConvertToInteger(cbIidProps.SelectedIndex.ToString());
                dr[1] = ConvertToInteger(tbiidValue.Text);
                dr[2] = description[1];

                iidDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }
        }
        private void btnRemoveIid_Click(object sender, RoutedEventArgs e)
        {

            var index = dgIid.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgIid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = iidDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                iidDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }

        }
        // Spells Tab
        private void btnAddSpell_Click(object sender, RoutedEventArgs e)
        {
            if (SearchForDuplicateProps(spellDataTable, tbSpellId.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                DataRow dr = spellDataTable.NewRow();
                dr[0] = ConvertToInteger(tbSpellId.Text);
                dr[1] = ConvertToFloat(tbSpellValue.Text); // Spell Probablilty
                dr[2] = tbSpellDescription.Text;

                spellDataTable.Rows.Add(dr);
                dgSpell.Items.Refresh();
            }
        }
        private void btnUpdateSpell_Click(object sender, RoutedEventArgs e)
        {
            var index = dgSpell.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }
            try
            {
                DataGridRow currentRowIndex = dgSpell.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = spellDataTable.Rows[currentRowIndex.GetIndex()];

                dr[0] = ConvertToInteger(tbSpellId.Text);
                dr[1] = ConvertToFloat(tbSpellValue.Text);
                dr[2] = tbSpellDescription.Text;

                spellDataTable.AcceptChanges();
                dgSpell.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }
        }
        private void btnRemoveSpell_Click(object sender, RoutedEventArgs e)
        {
            var index = dgSpell.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgSpell.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = spellDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                spellDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
        }
        // Attributes and Skills Tab
        // Attributes
        private void btnAttribDefaults_Click(object sender, RoutedEventArgs e)
        {
            attributeDataTable.Clear();           
            
            for (int i = 1; i < 7; i++)
            {
                DataRow attribRow = attributeDataTable.NewRow();

                if (i == 1)
                {
                    attribRow[0] = 1;
                    attribRow[4] = "Strength";
                }
                else if (i == 2)
                {
                    attribRow[0] = 2;
                    attribRow[4] = "Endurance";
                }
                else if (i == 3)
                {
                    attribRow[0] = 3;
                    attribRow[4] = "Quickness";
                }
                else if (i == 4)
                {
                    attribRow[0] = 4;
                    attribRow[4] = "Coordination";
                }
                else if (i == 5)
                {
                    attribRow[0] = 5;
                    attribRow[4] = "Focus";
                }
                else if (i == 6)
                {
                    attribRow[0] = 6;
                    attribRow[4] = "Self";
                }

                attribRow[1] = 200;
                attribRow[2] = 0;
                attribRow[3] = 0;

                attributeDataTable.Rows.Add(attribRow);               
            }

            dgAttributes.Items.Refresh();
            updateAttribs();

        }
        private void btnAttribUpdate_Click(object sender, RoutedEventArgs e)
        {
            attributeDataTable.Clear();

            for (int i = 1; i < 7; i++)
            {
                DataRow attribRow = attributeDataTable.NewRow();

                if (i == 1)
                {
                    attribRow[0] = 1;
                    attribRow[1] = ConvertToInteger(tbAttribStrength.Text);
                    attribRow[4] = "Strength";                  
                }
                else if (i == 2)
                {
                    attribRow[0] = 2;
                    attribRow[1] = ConvertToInteger(tbAttribEndurance.Text);
                    attribRow[4] = "Endurance";
                }
                else if (i == 3)
                {
                    attribRow[0] = 3;
                    attribRow[1] = ConvertToInteger(tbAttribQuickness.Text);
                    attribRow[4] = "Quickness";
                }
                else if (i == 4)
                {
                    attribRow[0] = 4;
                    attribRow[1] = ConvertToInteger(tbAttribCoordination.Text);
                    attribRow[4] = "Coordination";
                }
                else if (i == 5)
                {
                    attribRow[0] = 5;
                    attribRow[1] = ConvertToInteger(tbAttribFocus.Text);
                    attribRow[4] = "Focus";
                }
                else if (i == 6)
                {
                    attribRow[0] = 6;
                    attribRow[1] = ConvertToInteger(tbAttribSelf.Text);
                    attribRow[4] = "Self";
                }
               
                attribRow[2] = 0;
                attribRow[3] = 0;

                attributeDataTable.Rows.Add(attribRow);
            }
        }
        private void btnAttribClear_Click(object sender, RoutedEventArgs e)
        {
            attributeDataTable.Clear();
            ClearAttributeFields();

        }

        // Attributes 2 - Health, Stamina, Mana
        private void btnAttrib2Defaults_Click(object sender, RoutedEventArgs e)
        {
            attribute2DataTable.Clear();

            for (int i = 1; i < 4; i++)
            {
                DataRow attrib2Row = attribute2DataTable.NewRow();

                if (i == 1)
                {
                    attrib2Row[0] = 1;
                    attrib2Row[1] = 200;
                    attrib2Row[4] = 300;
                    attrib2Row[5] = "MaxHealth";
                }
                else if (i == 2)
                {
                    attrib2Row[0] = 3;
                    attrib2Row[1] = 200;
                    attrib2Row[4] = 400;
                    attrib2Row[5] = "MaxStamina";
                }
                else if (i == 3)
                {
                    attrib2Row[0] = 5;
                    attrib2Row[1] = 200;
                    attrib2Row[4] = 400;
                    attrib2Row[5] = "MaxMana";
                }

                attrib2Row[2] = 0;
                attrib2Row[3] = 0;

                attribute2DataTable.Rows.Add(attrib2Row);
            }
            dgAttributesTwo.Items.Refresh();
            updateAttribs2();
        }

        private void btnAttrib2Update_Click(object sender, RoutedEventArgs e)
        {
            attribute2DataTable.Clear();

            for (int i = 1; i < 4; i++)
            {
                DataRow attrib2Row = attribute2DataTable.NewRow();

                if (i == 1)
                {
                    attrib2Row[0] = 1;
                    attrib2Row[1] = ConvertToInteger(tbHealthInitLevel.Text);
                    attrib2Row[4] = ConvertToInteger(tbHealthCurrentLevel.Text);
                    attrib2Row[5] = "MaxHealth";
                }
                else if (i == 2)
                {
                    attrib2Row[0] = 3;
                    attrib2Row[1] = ConvertToInteger(tbStaminaInitLevel.Text);
                    attrib2Row[4] = ConvertToInteger(tbStaminaCurrentLevel.Text);
                    attrib2Row[5] = "MaxStamina";
                }
                else if (i == 3)
                {
                    attrib2Row[0] = 5;
                    attrib2Row[1] = ConvertToInteger(tbManaInitLevel.Text);
                    attrib2Row[4] = ConvertToInteger(tbManaCurrentLevel.Text);
                    attrib2Row[5] = "MaxMana";
                }

                attrib2Row[2] = 0;
                attrib2Row[3] = 0;

                attribute2DataTable.Rows.Add(attrib2Row);
            }
        }
        private void btnAttrib2Clear_Click(object sender, RoutedEventArgs e)
        {

            attribute2DataTable.Clear();
            ClearAttribute2Fields();

        }
        // Skills
        private void btnSkillsAdd_Click(object sender, RoutedEventArgs e)
        {
            // Search for duplicate skill
            if (SearchForDuplicateProps(skillsDataTable, cbSkillType.SelectedIndex.ToString()))
            {
                MessageBox.Show("Skill Already Exits");
            }
            else
            {
                // Pad 20 Left Skill, Pad 12 Right Spec/Train
                string[] tdescription = cbSkillType.Text.Split(" ");
                string trainSpec ="";
                if (rdbSpec.IsChecked == true)
                    trainSpec = "Specialized";
                else
                    trainSpec = "Trained";
                string description = tdescription[1].PadRight(20, ' ') + trainSpec.PadLeft(12, ' ');

                DataRow dr = skillsDataTable.NewRow();
                dr[0] = ConvertToInteger(cbSkillType.SelectedIndex.ToString());
                dr[1] = 0;
                if (rdbSpec.IsChecked == true)
                    dr[2] = 3;
                else
                    dr[2] = 2;
                dr[3] = 0;
                dr[4] = ConvertToInteger(tbSkillLevel.Text);
                dr[5] = 0;
                dr[6] = 0;
                dr[7] = description;

                skillsDataTable.Rows.Add(dr);

                skillsDataTable = ResortDataTable(skillsDataTable, "Type", "ASC");
                dgSkills.DataContext = skillsDataTable.DefaultView;
                dgSkills.Items.Refresh();
            }
        }

        private void btnSkillsUpdate_Click(object sender, RoutedEventArgs e)
        {
            var index = dgSkills.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }

            try
            {
                DataGridRow currentRowIndex = dgSkills.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = skillsDataTable.Rows[currentRowIndex.GetIndex()];

                string[] tdescription = cbSkillType.Text.Split(" ");
                string trainSpec = "";
                if (rdbSpec.IsChecked == true)
                    trainSpec = "Specialized";
                else
                    trainSpec = "Trained";
                string description = tdescription[1].PadRight(20, ' ') + trainSpec.PadLeft(12, ' ');

                dr[0] = ConvertToInteger(cbSkillType.SelectedIndex.ToString());
                dr[1] = 0;
                if (rdbSpec.IsChecked == true)
                    dr[2] = 3;
                else
                    dr[2] = 2;
                dr[3] = 0;
                dr[4] = ConvertToInteger(tbSkillLevel.Text);
                dr[5] = 0;
                dr[6] = 0;
                dr[7] = description;

                skillsDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }

        }
        private void btnSkillsRemove_Click(object sender, RoutedEventArgs e)
        {
            var index = dgSkills.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgSkills.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = skillsDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                skillsDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
        }
        // Create Items
        private void btnAddCreateItem_Click(object sender, RoutedEventArgs e)
        {

            DataRow dr = createListDataTable.NewRow();
            dr[0] = ConvertToInteger(tbCreateItemsDestType.Text);
            dr[1] = ConvertToInteger(tbCreateItemsWCID.Text);
            dr[2] = ConvertToInteger(tbCreateItemsStackSize.Text);
            dr[3] = ConvertToInteger(tbCreateItemsPalette.Text);
            dr[4] = ConvertToFloat(tbCreateItemsDropRate.Text);
            dr[5] = false;
            dr[6] = tbCreateItemsDescription.Text;

            createListDataTable.Rows.Add(dr);

        }
        private void btnUpdateCreateItem_Click(object sender, RoutedEventArgs e)
        {
            var index = dgCreateItems.SelectedIndex;

            try
            {
                DataGridRow currentRowIndex = dgCreateItems.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = createListDataTable.Rows[currentRowIndex.GetIndex()];

                dr[0] = ConvertToInteger(tbCreateItemsDestType.Text);
                dr[1] = ConvertToInteger(tbCreateItemsWCID.Text);
                dr[2] = ConvertToInteger(tbCreateItemsStackSize.Text);
                dr[3] = ConvertToInteger(tbCreateItemsPalette.Text);
                dr[4] = ConvertToFloat(tbCreateItemsDropRate.Text);
                dr[5] = false;
                dr[6] = tbCreateItemsDescription.Text;

                createListDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }
        }
        private void btnRemoveCreateItem_Click(object sender, RoutedEventArgs e)
        {
            var index = dgCreateItems.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgCreateItems.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = createListDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                createListDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
        }

        // BodyParts
        private void btnAddBodyPart_Click(object sender, RoutedEventArgs e)
        {

            string[] bodyPartDescription = cbBodyPart.Text.Split(" ");
            string[] damageType = cbBodyPartDamageType.Text.Split(" ");

            DataRow dr = bodypartsDataTable.NewRow();

            dr[0] = ConvertToInteger(bodyPartDescription[0].ToString());
            dr[1] = ConvertToInteger(damageType[0].ToString());
            dr[2] = ConvertToInteger(tbBodyPartDamageValue.Text);
            dr[3] = ConvertToFloat(tbBodyPartDamageVariance.Text);
            dr[4] = ConvertToInteger(tbBodyPartArmorLevel.Text);
            dr[5] = ConvertToInteger(tbBodyPartBase_Height.Text);

            dr[6] = ConvertToFloat(tbBodyPartQuadHighLF.Text);
            dr[7] = ConvertToFloat(tbBodyPartQuadMiddleLF.Text);
            dr[8] = ConvertToFloat(tbBodyPartQuadLowLF.Text);

            dr[9] = ConvertToFloat(tbBodyPartQuadHighRF.Text);
            dr[10] = ConvertToFloat(tbBodyPartQuadMiddleRF.Text);
            dr[11] = ConvertToFloat(tbBodyPartQuadLowRF.Text);

            dr[12] = ConvertToFloat(tbBodyPartQuadHighLB.Text);
            dr[13] = ConvertToFloat(tbBodyPartQuadMiddleLB.Text);
            dr[14] = ConvertToFloat(tbBodyPartQuadLowLB.Text);

            dr[15] = ConvertToFloat(tbBodyPartQuadHighRB.Text);
            dr[16] = ConvertToFloat(tbBodyPartQuadMiddleRB.Text);
            dr[17] = ConvertToFloat(tbBodyPartQuadLowRB.Text);

            dr[18] = $"{bodyPartDescription[1].ToString()} - {damageType[1]}";

            bodypartsDataTable.Rows.Add(dr);
            
            dgBodyParts.DataContext = bodypartsDataTable.DefaultView;
            dgBodyParts.Items.Refresh();
        }
        private void btnUpdateBodyPart_Click(object sender, RoutedEventArgs e)
        {


            var index = dgBodyParts.SelectedIndex;

            try
            {
                DataGridRow currentRowIndex = dgBodyParts.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = bodypartsDataTable.Rows[currentRowIndex.GetIndex()];

                string[] bodyPartDescription = cbBodyPart.Text.Split(" ");
                string[] damageType = cbBodyPartDamageType.Text.Split(" ");

                dr[0] = ConvertToInteger(bodyPartDescription[0].ToString());
                dr[1] = ConvertToInteger(damageType[0].ToString());
                dr[2] = ConvertToInteger(tbBodyPartDamageValue.Text);
                dr[3] = ConvertToFloat(tbBodyPartDamageVariance.Text);
                dr[4] = ConvertToInteger(tbBodyPartArmorLevel.Text);
                dr[5] = ConvertToInteger(tbBodyPartBase_Height.Text);

                dr[6] = ConvertToFloat(tbBodyPartQuadHighLF.Text);
                dr[7] = ConvertToFloat(tbBodyPartQuadMiddleLF.Text);
                dr[8] = ConvertToFloat(tbBodyPartQuadLowLF.Text);

                dr[9] = ConvertToFloat(tbBodyPartQuadHighRF.Text);
                dr[10] = ConvertToFloat(tbBodyPartQuadMiddleRF.Text);
                dr[11] = ConvertToFloat(tbBodyPartQuadLowRF.Text);

                dr[12] = ConvertToFloat(tbBodyPartQuadHighLB.Text);
                dr[13] = ConvertToFloat(tbBodyPartQuadMiddleLB.Text);
                dr[14] = ConvertToFloat(tbBodyPartQuadLowLB.Text);

                dr[15] = ConvertToFloat(tbBodyPartQuadHighRB.Text);
                dr[16] = ConvertToFloat(tbBodyPartQuadMiddleRB.Text);
                dr[17] = ConvertToFloat(tbBodyPartQuadLowRB.Text);

                dr[18] = $"{bodyPartDescription[1].ToString()} - {damageType[1]}";


                bodypartsDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }
        }
        private void btnRemoveBodyPart_Click(object sender, RoutedEventArgs e)
        {
            var index = dgBodyParts.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgBodyParts.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = bodypartsDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                bodypartsDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
        }
        private void btnBodyPartHelpWiki_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start https://github.com/ACEmulator/ACE/wiki/How-Body-Parts-Work"));
        }

        // ES Tab
        private void btnLoadES_Click(object sender, RoutedEventArgs e)
        {
            string emotescript = OpenESFile();
            rtbEmoteScript.Document.Blocks.Clear();
            rtbEmoteScript.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(emotescript)));
            
        }
        private void btnSaveES_Click(object sender, RoutedEventArgs e)
        {

            string emotescript = new TextRange(rtbEmoteScript.Document.ContentStart, rtbEmoteScript.Document.ContentEnd).Text;
            SaveESFile(emotescript);
 
        }
        private void btnClearES_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButton buttons = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show("Clear the current Emote Script?", "WARNING!", buttons, icon);
            if (result == MessageBoxResult.Yes)
            {
                rtbEmoteScript.Document.Blocks.Clear();
 
            }
            else if (result == MessageBoxResult.No)
            {
            }
            else
            {
            }

        }
        // Book Tab
        // Book
        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            DataRow dr = bookInfoDataTable.NewRow();
            dr[0] = ConvertToInteger(tbMaxPages.Text);
            dr[1] = ConvertToInteger(tbMaxChars.Text);
            
            bookInfoDataTable.Rows.Add(dr);
        }

        private void btnUpateBook_Click(object sender, RoutedEventArgs e)
        {
            var index = dgBookInfo.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgBookInfo.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = bookInfoDataTable.Rows[currentRowIndex.GetIndex()];

                dr[0] = ConvertToInteger(tbMaxPages.Text);
                dr[1] = ConvertToInteger(tbMaxChars.Text);

                bookInfoDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }
        }
        private void btnRemoveBook_Click(object sender, RoutedEventArgs e)
        {
            bookInfoDataTable.Clear();
        }
        // Book Pages
        private void btnAddPage_Click(object sender, RoutedEventArgs e)
        {
            DataRow dr = bookPagesDataTable.NewRow();
            dr[0] = ConvertToInteger(tbPageID.Text);
            dr[1] = "4294967295";
            dr[2] = tbAuthorName.Text;
            dr[3] = "prewritten";
            dr[4] = rdbBookTrue.IsChecked;
            dr[5] = tbPageText.Text;

            bookPagesDataTable.Rows.Add(dr);
        }

        private void btnUpdatePage_Click(object sender, RoutedEventArgs e)
        {

            var index = dgBookPages.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgBookPages.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = bookPagesDataTable.Rows[currentRowIndex.GetIndex()];

                dr[0] = ConvertToInteger(tbPageID.Text);
                dr[1] = "4294967295";
                dr[2] = tbAuthorName.Text;
                dr[3] = "prewritten";
                dr[4] = rdbBookTrue.IsChecked;
                dr[5] = tbPageText.Text;

                bookInfoDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                LogError(ex);
            }

        }
        private void btnRemovePage_Click(object sender, RoutedEventArgs e)
        {
            var index = dgBookPages.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgBookPages.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = bookPagesDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                bodypartsDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
        }

        // Positions Tab
        private void btnAddPosition_Click(object sender, RoutedEventArgs e)
        {
            DataRow dr = positionsDataTable.NewRow();

            string[] description = cbPosition.Text.Split(" ");

            dr[0] = ConvertToInteger(cbPosition.SelectedIndex.ToString());

            dr[1] = ConvertToInteger(tbCellID.Text);

            dr[2] = ConvertToFloat(tbOriginX.Text);
            dr[3] = ConvertToFloat(tbOriginY.Text);
            dr[4] = ConvertToFloat(tbOriginZ.Text);

            dr[5] = ConvertToFloat(tbAngleW.Text);
            dr[6] = ConvertToFloat(tbAngleX.Text);
            dr[7] = ConvertToFloat(tbAngleY.Text);
            dr[8] = ConvertToFloat(tbAngleZ.Text);

            dr[9] = description[1];         

            positionsDataTable.Rows.Add(dr);
        }
        private void btnUpdatePosition_Click(object sender, RoutedEventArgs e)
        {
            var index = dgPosition.SelectedIndex;
            DataGridRow currentRowIndex = dgPosition.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            DataRow dr = positionsDataTable.Rows[currentRowIndex.GetIndex()];

            string[] description = cbPosition.Text.Split(" ");

            dr[0] = ConvertToInteger(cbPosition.SelectedIndex.ToString());

            dr[1] = ConvertToInteger(tbCellID.Text);

            dr[2] = ConvertToFloat(tbOriginX.Text);
            dr[3] = ConvertToFloat(tbOriginY.Text);
            dr[4] = ConvertToFloat(tbOriginZ.Text);

            dr[5] = ConvertToFloat(tbAngleW.Text);
            dr[6] = ConvertToFloat(tbAngleX.Text);
            dr[7] = ConvertToFloat(tbAngleY.Text);
            dr[8] = ConvertToFloat(tbAngleZ.Text);

            dr[9] = description[1];

            positionsDataTable.AcceptChanges();

        }
        private void btnRemovePosition_Click(object sender, RoutedEventArgs e)
        {
            var index = dgPosition.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgPosition.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = positionsDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                positionsDataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You can not delete that row!");
                LogError(ex);
            }
        }
        private void btnUseLoc_Click(object sender, RoutedEventArgs e)
        {
            string rgxPattern = @"([a-zA-Z0-9]+) \[([-+]?[0-9]*\.[0-9]+|[0-9]+)\s([-+]?[0-9]*\.[0-9]+|[0-9]+)\s([-+]?[0-9]*\.[0-9]+|[0-9]+)\]\s([-+]?[0-9]*\.[0-9]+|[0-9]+)\s([-+]?[0-9]*\.[0-9]+|[0-9]+)\s([-+]?[0-9]*\.[0-9]+|[0-9]+)\s([-+]?[0-9]*\.[0-9]+|[0-9]+)$";

            var match = Regex.Match(tbPositionLoc.Text, rgxPattern);
            string tloc = match.Groups[1].ToString().Replace("0x", "");

            int loc = int.Parse(tloc, System.Globalization.NumberStyles.HexNumber);

            tbCellID.Text = loc.ToString();
            tbOriginX.Text = match.Groups[2].ToString();
            tbOriginY.Text = match.Groups[3].ToString();
            tbOriginZ.Text = match.Groups[4].ToString();
            tbAngleW.Text = match.Groups[5].ToString();
            tbAngleX.Text = match.Groups[6].ToString();
            tbAngleY.Text = match.Groups[7].ToString();
            tbAngleZ.Text = match.Groups[8].ToString();           
        }

        // Clear Attrib Fields
        public void ClearAttributeFields()
        {
            tbAttribStrength.Text = "";
            tbAttribEndurance.Text = "";
            tbAttribQuickness.Text = "";
            tbAttribCoordination.Text = "";
            tbAttribFocus.Text = "";
            tbAttribSelf.Text = "";
        }

        public void ClearAttribute2Fields()
        {
            tbHealthCurrentLevel.Text = "";
            tbHealthInitLevel.Text = "";
            tbStaminaCurrentLevel.Text = "";
            tbStaminaInitLevel.Text = "";
            tbManaCurrentLevel.Text = "";
            tbManaInitLevel.Text = "";
        }

        // Converter
        private DataRow ConvertToIntRow(string property, string propertyValue, string description)
        {
            DataTable tempdt = new DataTable();

            DataColumn propertyCol = new DataColumn("Property");
            DataColumn valueCol = new DataColumn("Value");
            DataColumn descriptCol = new DataColumn("Description");

            propertyCol.DataType = System.Type.GetType("System.Int32");
            valueCol.DataType = System.Type.GetType("System.Int32");

            tempdt.Columns.Add(propertyCol);
            tempdt.Columns.Add(valueCol);
            tempdt.Columns.Add(descriptCol);

            DataRow tdr = tempdt.NewRow();
            tdr[0] = ConvertToInteger(property);
            tdr[1] = ConvertToInteger(propertyValue);
            tdr[2] = description;

            return tdr;
        }
    }
}
