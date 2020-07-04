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
        // DataGrid Events
        //private void dgInt32_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{


        //    var index = dgInt32.SelectedIndex;
        //    DataGridRow currentRowIndex = dgInt32.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
        //    if (index + 1 > integerDataTable.Rows.Count)
        //    {

        //    }
        //    else
        //    {
        //        DataRow dr = integerDataTable.Rows[currentRowIndex.GetIndex()];
        //        int cbindex = 0;
        //        Int32.TryParse(dr[0].ToString(), out cbindex);
        //        cbInt32Props.SelectedIndex = cbindex;
        //        tbValue.Text = dr[1].ToString();
        //    }
        //}
        private void dgInt32_RowSelected(object sender, RoutedEventArgs e)
        {
            // Lookup for the source to be DataGridCell
            //if (e.OriginalSource.GetType() == typeof(DataGridCell))
            //{

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



            // }
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
                // tbBoolValue.Text = dr[1].ToString();
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
                // int cbindex = 0;
                // Int32.TryParse(dr[0].ToString(), out cbindex);
                // cbSpellProps.SelectedIndex = cbindex;
                tbSpellId.Text = dr[0].ToString();
                tbSpellValue.Text = dr[1].ToString();
            }
        }


}
}
