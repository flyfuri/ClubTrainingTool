using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MyWPFExtentions
{
    static class dataGridWPFgetValueByRowColumn
    {
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        public static string getValueTextStringByRowColIndexes(this DataGrid targetDataGrid , int rowIndex, int colIndex)
        {
            //get row via rowIndex
            DataGridRow row = (DataGridRow)targetDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                targetDataGrid.UpdateLayout();
                targetDataGrid.ScrollIntoView(targetDataGrid.Items[rowIndex]);
                row = (DataGridRow)targetDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            }

            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    targetDataGrid.ScrollIntoView(row, targetDataGrid.Columns[colIndex]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(colIndex);

                if (cell.Content.ToString().Contains("System.Windows") == true)
                {
                    if (cell.Content.ToString().Contains("TextBlock") == true)
                    {
                        return (cell.Content as TextBlock).Text;
                    }
                    else
                    {
                        return "error: cell content is not TextBlock";
                    }
                }
                else
                {
                    try
                    {
                        string tempstring = cell.Content.ToString();
                        return tempstring;
                    }
                    catch
                    {
                        return "catch error: cell content is not TextBlock";
                    }
                }
            }
            else
            {
                return "error: no datagrid row found";
            }
        }

        public static void setStringValueByRowColIndexes(this DataGrid targetDataGrid, int rowIndex, int colIndex, string valueToSet)
        {
            //get row via rowIndex
            DataGridRow row = (DataGridRow)targetDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                targetDataGrid.UpdateLayout();
                targetDataGrid.ScrollIntoView(targetDataGrid.Items[rowIndex]);
                row = (DataGridRow)targetDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            }

            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    targetDataGrid.ScrollIntoView(row, targetDataGrid.Columns[colIndex]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(colIndex);

                cell.Content = valueToSet;
                //return true;
            }
            else
            {
                //return false;
            }
        }

        public static void setBgColorByRowColIndexes(this DataGrid targetDataGrid, int rowIndex, int colIndex, SolidColorBrush bgBrush)
        {
            //get row via rowIndex
            DataGridRow row = (DataGridRow)targetDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                targetDataGrid.UpdateLayout();
                targetDataGrid.ScrollIntoView(targetDataGrid.Items[rowIndex]);
                row = (DataGridRow)targetDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            }

            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    targetDataGrid.ScrollIntoView(row, targetDataGrid.Columns[colIndex]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                //DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(colIndex);
                DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(colIndex) as DataGridCell;

                cell.Background = bgBrush;
                //return true;
            }
            else
            {
                //return false;
            }
        }
    }
}
