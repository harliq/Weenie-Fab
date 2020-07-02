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
                dr[0] = cbInt32Props.SelectedIndex.ToString();
                dr[1] = tbValue.Text;
                dr[2] = description[1];

                integerDataTable.Rows.Add(dr);
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

                dr[0] = cbInt32Props.SelectedIndex.ToString();
                dr[1] = tbValue.Text;
                dr[2] = description[1];

                integerDataTable.AcceptChanges();
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
                dr[0] = cbInt64Props.SelectedIndex.ToString();
                dr[1] = tb64Value.Text;
                dr[2] = description[1];

                integer64DataTable.Rows.Add(dr);
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

                dr[0] = cbInt64Props.SelectedIndex.ToString();
                dr[1] = tb64Value.Text;
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
                dr[0] = cbBoolProps.SelectedIndex.ToString();
                if (rbTrue.IsChecked == true)
                    dr[1] = "True";
                else
                    dr[1] = "False";
                dr[2] = description[1];

                boolDataTable.Rows.Add(dr);
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

                dr[0] = cbBoolProps.SelectedIndex.ToString();
                if (rbTrue.IsChecked == true)
                    dr[1] = "True";
                else
                    dr[1] = "False";
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
            if (SearchForDuplicateProps(boolDataTable, cbBoolProps.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbBoolProps.Text.Split(" ");
                DataRow dr = boolDataTable.NewRow();
                dr[0] = cbBoolProps.SelectedIndex.ToString();
                dr[1] = tbFloatValue.Text;
                dr[2] = description[1];

                boolDataTable.Rows.Add(dr);
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

                dr[0] = cbFloatProps.SelectedIndex.ToString();
                dr[1] = tbFloatValue.Text;
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
                dr[0] = cbStringProps.SelectedIndex.ToString();
                dr[1] = tbStringValue.Text;
                dr[2] = description[1];

                stringDataTable.Rows.Add(dr);
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

                dr[0] = cbStringProps.SelectedIndex.ToString();
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
            if (SearchForDuplicateProps(boolDataTable, cbBoolProps.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbBoolProps.Text.Split(" ");
                DataRow dr = boolDataTable.NewRow();
                dr[0] = cbBoolProps.SelectedIndex.ToString();
                dr[1] = tbDiDValue.Text;
                dr[2] = description[1];

                boolDataTable.Rows.Add(dr);
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

                dr[0] = cbDiDProps.SelectedIndex.ToString();
                dr[1] = tbDiDValue.Text;
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
            if (SearchForDuplicateProps(spellDataTable, cbSpellProps.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbBoolProps.Text.Split(" ");
                DataRow dr = boolDataTable.NewRow();
                dr[0] = cbBoolProps.SelectedIndex.ToString();
                dr[1] = tbSpellValue.Text;
                dr[2] = description[1];

                boolDataTable.Rows.Add(dr);
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

                string[] description = cbSpellProps.Text.Split(" ");

                dr[0] = cbSpellProps.SelectedIndex.ToString();
                dr[1] = tbSpellValue.Text;
                dr[2] = description[1];

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


    }
}
