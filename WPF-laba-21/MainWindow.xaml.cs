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
using System.Data.OleDb;
using System.Data;

namespace WPF_laba_21
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user.COMP-02.000\source\db\Database111.mdb";
            string[] queries = { "SELECT * FROM Факультет", "SELECT * FROM Группа", "SELECT * FROM Студенты" };
            DataGrid[] grids = { facultyGrid, groupGrid, studentGrid };

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                for (int i = 0; i < queries.Length; i++)
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter(queries[i], connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    grids[i].ItemsSource = dataTable.DefaultView;
                }
            }
        }

    }
}
