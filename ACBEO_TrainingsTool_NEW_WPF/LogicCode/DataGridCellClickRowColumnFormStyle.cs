using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    class DataGridCellClickRowColumnFormStyle
    {
        private MouseButtonEventArgs _wpf_e;

        private int _RowIndex;
        private int _ColumnIndex;
        private bool _isCell;
        private DataGridCell _act_cellObject;

       private DependencyObject dep;

        public MouseButtonEventArgs wpf_e
        {
            get
            {
                return _wpf_e;
            }
            set
            {
                _wpf_e = value;
                _RowIndex = -1;
                _ColumnIndex = -1;
                _act_cellObject = null;
                _isCell = false;
                /*if ((_wpf_e.OriginalSource as DependencyObject) is DataGridCell == false)
                {
                    return;
                }*/
                getClickedCell(_wpf_e);
                getClickedRowIndex(_wpf_e);
                getClickedColIndex();
            }
        }

        public int RowIndex
        {
            get
            {
                return _RowIndex;
            }
        }
        public int ColumnIndex
        {
            get
            {
                return _ColumnIndex;
            }
        }

        public DataGridCell clickedCell
        {
            get
            {
                return _act_cellObject;
            }
        }

        public bool isCell
        {
            get
            {
                return _isCell;
            }
        }

        private void getClickedCell(MouseButtonEventArgs e)
        {
            //dep = (DependencyObject)e.OriginalSource;
            dep = (e.OriginalSource as DependencyObject);
            // iteratively traverse the visual tree
            while ((dep != null) &
            !(dep is DataGridCell))/*& 
            !(dep is DataGridColumnHeader))*/
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
            {
                _act_cellObject = null;
                return;
            }
            /*if (dep is DataGridColumnHeader)
            {
                DataGridColumnHeader columnHeader = dep as DataGridColumnHeader;
                // do something
            }*/

            if (dep is DataGridCell)
            {
                _act_cellObject = dep as DataGridCell;
            }
            else
            {
                _act_cellObject = null;
            }
        }

        private void getClickedRowIndex(MouseButtonEventArgs e)
        {
            getClickedCell(wpf_e);

            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;

                // navigate further up the tree
                while ((dep != null) & !(dep is DataGridRow))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                DataGridRow row = dep as DataGridRow;
                DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as DataGrid;

                _isCell = true;
                _RowIndex = dataGrid.ItemContainerGenerator.IndexFromContainer(row);
            }
            else
            {
                _isCell = false;
                _RowIndex = -1;
            }
        }

        private void getClickedColIndex()
        {
            if (_act_cellObject != null)
            {
                _isCell = true;
                _ColumnIndex = _act_cellObject.Column.DisplayIndex;
            }
            else
            {
                _isCell = false;
                _ColumnIndex = -1;
            }
        }
    }          
}
