using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using item1 = System.Windows.Forms;

namespace WpfGrid_r3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //GridView myGridView = new GridView();
           CreateGridView();
        }
        public void CreateGridView()
        {
            // Create a GridView 
            GridView grdView = new GridView();
            grdView.AllowsColumnReorder = true;
            grdView.ColumnHeaderToolTip = "ProcessToolTip";
            //StackPnl.MouseDown += new MouseButtonEventHandler(StackPanel_MouseDown);

            GridViewColumn processColumn = new GridViewColumn();
            processColumn.DisplayMemberBinding = new Binding("Process");
            processColumn.Header = "Process";
            processColumn.Width = 450;
            grdView.Columns.Add(processColumn);

            GridViewColumn idColumn = new GridViewColumn();
            idColumn.DisplayMemberBinding = new Binding("ID");
            idColumn.Header = "ID";
            idColumn.Width = 103;
            grdView.Columns.Add(idColumn);

            GridViewColumn PagedMemorySizeColumn = new GridViewColumn();
            PagedMemorySizeColumn.DisplayMemberBinding = new Binding("PagedMemorySize");
            PagedMemorySizeColumn.Header = "Paged Memory Size";
            PagedMemorySizeColumn.Width = 150;
            grdView.Columns.Add(PagedMemorySizeColumn);

            ListView1.View = grdView;

            //Create current process list
            Process[] processlist = Process.GetProcesses();
            //Create List of ProcessOptions
            List<ProcessOptions> items = new List<ProcessOptions>();
            foreach (Process theprocess in processlist)
            {
                items.Add(new ProcessOptions() { Process = theprocess.ProcessName, ID = theprocess.Id,
                                                 PagedMemorySize = theprocess.PagedMemorySize});
                }
            ListView1.ItemsSource = items;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            CreateGridView();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //txtBoxSearch
            //MessageBox.Show(txtBoxSearch.Text + " [Length =" + txtBoxSearch.Text.Length.ToString() + "]");
            // Create a GridView 
            GridView grdView = new GridView();
            grdView.AllowsColumnReorder = true;
            grdView.ColumnHeaderToolTip = "Process";

            GridViewColumn processColumn = new GridViewColumn();
            processColumn.DisplayMemberBinding = new Binding("Process");
            processColumn.Header = "Process";
            processColumn.Width = 450;
            grdView.Columns.Add(processColumn);

            GridViewColumn idColumn = new GridViewColumn();
            idColumn.DisplayMemberBinding = new Binding("ID");
            idColumn.Header = "ID";
            idColumn.Width = 103;
            grdView.Columns.Add(idColumn);

            GridViewColumn PagedMemorySizeColumn = new GridViewColumn();
            PagedMemorySizeColumn.DisplayMemberBinding = new Binding("PagedMemorySize");
            PagedMemorySizeColumn.Header = "Paged Memory Size";
            PagedMemorySizeColumn.Width = 150;
            grdView.Columns.Add(PagedMemorySizeColumn);

            ListView1.View = grdView;

            //Create current process list
            Process[] processlist = Process.GetProcesses();
            //Create List of ProcessOptions
            List<ProcessOptions> items = new List<ProcessOptions>();
            foreach (Process theprocess in processlist)
            {
                items.Add(new ProcessOptions()
                {
                    Process = theprocess.ProcessName,
                    ID = theprocess.Id,
                    PagedMemorySize = theprocess.PagedMemorySize
                });
            }
            ListView1.ItemsSource = items;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListView1.ItemsSource);
            view.Filter = UserFilter;
            CollectionViewSource.GetDefaultView(ListView1.ItemsSource).Refresh();

        }
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtBoxSearch.Text))
                return true;
            else
                return ((item as ProcessOptions).Process.IndexOf(txtBoxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);//||
                        //ToString((item as ProcessOptions).ID).IndexOf(txtBoxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        //(item as ProcessOptions).Process.IndexOf(txtBoxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }


    }
    public class ProcessOptions
    {
        public string Process { get; set; }

        public int ID { get; set; }

        public int PagedMemorySize { get; set; }
    }

}
