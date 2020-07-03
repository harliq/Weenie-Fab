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
            }
            else if (result == MessageBoxResult.No)
            {
                ClearAllDataTables();
                ClearAllFields();
            }
            else
            {
            }
        }
        private void btnOpenSqlFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        private void btnSaveSqlFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

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

                // integerDataTable.Rows.Add(ConvertToIntRow(cbInt32Props.SelectedIndex.ToString(), tbValue.Text, description[1]));
                integerDataTable.Rows.Add(dr);
                integerDataTable = ResortDataTable(integerDataTable, "Property", "ASC");
                dgInt32.ItemsSource = integerDataTable.DefaultView;
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
            catch (Exception)
            {
                MessageBox.Show("You can not delete that row!");
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
 
            catch (Exception)
            {
                MessageBox.Show("The row you selected is blank");
            }
        }
        // Integer 64
        private void btnAddInt64_Click(object sender, RoutedEventArgs e)
        {
            // search for duplicate property
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

                // integer64DataTable.Rows.Add(ConvertToIntRow(cbInt32Props.SelectedIndex.ToString(), tbValue.Text, description[1]));

                integer64DataTable.Rows.Add(dr);
                integer64DataTable = ResortDataTable(integer64DataTable, "Property", "ASC");
                dgInt64.ItemsSource = integer64DataTable.DefaultView;
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

                dr[0] = ConvertToInteger(cbInt64Props.SelectedIndex.ToString());
                dr[1] = ConvertToInteger(tb64Value.Text);
                dr[2] = description[1];

                integer64DataTable.AcceptChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("The row you selected is blank");
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
            catch (Exception)
            {
                MessageBox.Show("You can not delete that row!");
            }
        }
        // Boolean
        private void btnAddBool_Click(object sender, RoutedEventArgs e)
        {
            // search for duplicate property
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
                //if (rbTrue.IsChecked == true)
                //    dr[1] = "True";
                //else
                //    dr[1] = "False";
                dr[2] = description[1];

                boolDataTable.Rows.Add(dr);
                boolDataTable = ResortDataTable(boolDataTable, "Property", "ASC");
                dgBool.ItemsSource = boolDataTable.DefaultView;
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
                //if (rbTrue.IsChecked == true)
                //    dr[1] = "True";
                //else
                //    dr[1] = "False";
                dr[2] = description[1];

                boolDataTable.AcceptChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("The row you selected is blank");
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
            catch (Exception)
            {
                MessageBox.Show("You can not delete that row!");
            }
        }
        // Float
        private void btnAddFloat_Click(object sender, RoutedEventArgs e)
        {
            // search for duplicate property
            if (SearchForDuplicateProps(floatDataTable, cbBoolProps.SelectedIndex.ToString()))
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
                dgFloat.ItemsSource = floatDataTable.DefaultView;
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
            catch (Exception)
            {
                MessageBox.Show("The row you selected is blank");
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
            catch (Exception)
            {
                MessageBox.Show("You can not delete that row!");
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
                dgString.ItemsSource = stringDataTable.DefaultView;
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
            catch (Exception)
            {
                MessageBox.Show("The row you selected is blank");
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
            catch (Exception)
            {
                MessageBox.Show("You can not delete that row!");
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

                // boolDataTable.Rows.Add(ConvertToIntRow(cbInt32Props.SelectedIndex.ToString(), tbValue.Text, description[1]));

                didDataTable.Rows.Add(dr);
                didDataTable = ResortDataTable(didDataTable, "Property", "ASC");
                dgDiD.ItemsSource = didDataTable.DefaultView;
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
            catch (Exception)
            {
                MessageBox.Show("The row you selected is blank");
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
            catch (Exception)
            {
                MessageBox.Show("You can not delete that row!");
            }
        }


        // Spell
        private void btnAddSpell_Click(object sender, RoutedEventArgs e)
        {
            // search for duplicate property
            if (SearchForDuplicateProps(spellDataTable, tbSpellId.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                // string[] description = cbBoolProps.Text.Split(" ");
                DataRow dr = spellDataTable.NewRow();
                dr[0] = ConvertToInteger(tbSpellId.Text);
                dr[1] = ConvertToFloat(tbSpellValue.Text); // Spell Probablilty
                dr[2] = "Test"; //description[1];

                spellDataTable.Rows.Add(dr);
                spellDataTable = ResortDataTable(spellDataTable, "Property", "ASC");
                dgSpell.ItemsSource = spellDataTable.DefaultView;
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

                // string[] description = cbSpellProps.Text.Split(" ");

                dr[0] = ConvertToInteger(tbSpellId.Text);
                dr[1] = ConvertToFloat(tbSpellValue.Text);
                // dr[2] = description[1];

                spellDataTable.AcceptChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("The row you selected is blank");
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
            catch (Exception)
            {
                MessageBox.Show("You can not delete that row!");
            }
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
