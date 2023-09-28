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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ToolBar[] tbs = { ToolBar_1, ToolBar_2, ToolBar_3 };

            foreach (ToolBar tb in tbs)
            {
                Button bckLstBtn = new Button
                {
                    Width = 23,
                    Height = 25,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                Button bckBtn = new Button
                {
                    Width = 23,
                    Height = 25,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                Button nxtLstBtn = new Button
                {
                    Width = 23,
                    Height = 25,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                Button nxtBtn = new Button
                {
                    Width = 23,
                    Height = 25,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                Button plsBtn = new Button
                {
                    Width = 23,
                    Height = 25,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                Button crssBtn = new Button
                {
                    Width = 23,
                    Height = 25,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                plsBtn.Click += Insert_Click;
                crssBtn.Click += Delete_Click;

                TextBox tbPg = new TextBox
                {
                    Width = 50,
                    Height = 25,
                    HorizontalContentAlignment = HorizontalAlignment.Left
                };

                Label lblPg = new Label
                {
                    Width = 30,
                    Height = 25,
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                Image imgBckLst = new Image
                {
                    Source = new BitmapImage(new Uri("imgLftLst.jpg", UriKind.Relative)),
                    Width = 50,
                    Height = 50,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Image imgBck = new Image
                {
                    Source = new BitmapImage(new Uri("imgLft.png", UriKind.Relative)),
                    Width = 25,
                    Height = 25,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Image imgNxt = new Image
                {
                    Source = new BitmapImage(new Uri("imgRght.jpg", UriKind.Relative)),
                    Width = 25,
                    Height = 25,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Image imgNxtLst = new Image
                {
                    Source = new BitmapImage(new Uri("imgRghtLst.jpg", UriKind.Relative)),
                    Width = 50,
                    Height = 50,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Image imgPls = new Image
                {
                    Source = new BitmapImage(new Uri("pls.jpg", UriKind.Relative)),
                    Width = 25,
                    Height = 25,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Image imgCrss = new Image
                {
                    Source = new BitmapImage(new Uri("crss.jpg", UriKind.Relative)),
                    Width = 25,
                    Height = 25,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                bckLstBtn.Content = imgBckLst;
                bckBtn.Content = imgBck;
                nxtBtn.Content = imgNxt;
                nxtLstBtn.Content = imgNxtLst;
                plsBtn.Content = imgPls;
                crssBtn.Content = imgCrss;

                tb.Items.Add(bckLstBtn);
                tb.Items.Add(bckBtn);
                tb.Items.Add(tbPg);
                tb.Items.Add(lblPg);
                tb.Items.Add(nxtBtn);
                tb.Items.Add(nxtLstBtn);
                tb.Items.Add(plsBtn);
                tb.Items.Add(crssBtn);
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// <c>БД ПОДКЛЮЧАЕМ И ЗАПОЛНЯЕМ</c>
        /// </summary>
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
        /// <summary>
        /// <c>ДЕЛАЕТ ПОСИМВОЛЬНЫЙ И ТОЧНЫЙ ПОИСК(через запросы в Access)</c>
        /// </summary>
        private void Tables_Search(object sender, RoutedEventArgs e) 
        {
            string searchSymbols = TB_Sym_Search.Text;
            string searchAccuracy = TB_Acc_Search.Text;

            DataView facultyView = facultyGrid.ItemsSource as DataView;
            DataView groupView = groupGrid.ItemsSource as DataView;
            DataView studentView = studentGrid.ItemsSource as DataView;

            if (!string.IsNullOrWhiteSpace(searchSymbols))
            {
                facultyView.RowFilter = $"Convert(Код, 'System.String') LIKE '%{searchSymbols}%' " +
                    $"OR Факультет LIKE '%{searchSymbols}%' " +
                    $"OR Convert(Курс, 'System.String') LIKE '%{searchSymbols}%'" +
                    $"OR Convert([Количество групп], 'System.String') LIKE '%{searchSymbols}%'";

                groupView.RowFilter = $"Convert(Код, 'System.String') LIKE '%{searchSymbols}%' " +
                    $"OR [Название группы] LIKE '%{searchSymbols}%' " +
                    $"OR [Фамилия старосты] LIKE '%{searchSymbols}%'" +
                    $"OR Convert(Количество, 'System.String') LIKE '%{searchSymbols}%'"+
                    $"OR Convert([Факультет ID], 'System.String') LIKE '%{searchSymbols}%'";

                studentView.RowFilter = $"Convert(Код, 'System.String') LIKE '%{searchSymbols}%' " +
                    $"OR ФИО LIKE '%{searchSymbols}%' " +
                    $"OR Адрес LIKE '%{searchSymbols}%'" +
                    $"OR Convert(Телефон, 'System.String') LIKE '%{searchSymbols}%'" +
                    $"OR Convert(ID_GR, 'System.String') LIKE '%{searchSymbols}%'";

            } else if (!string.IsNullOrWhiteSpace(searchAccuracy)) 
            {
                facultyView.RowFilter = $"Convert(Код, 'System.String') = '{searchAccuracy}'" +
                    $"OR Факультет = '{searchAccuracy}'" +
                    $"OR Convert(Курс, 'System.String') = '{searchAccuracy}'" +
                    $"OR Convert([Количество групп], 'System.String') = '{searchAccuracy}'";

                groupView.RowFilter = $"Convert(Код, 'System.String') = '{searchAccuracy}' " +
                    $"OR [Название группы] = '{searchAccuracy}' " +
                    $"OR [Фамилия старосты] = '{searchAccuracy}'" +
                    $"OR Convert(Количество, 'System.String') = '{searchAccuracy}'" +
                    $"OR Convert([Факультет ID], 'System.String') = '{searchAccuracy}'";

                studentView.RowFilter = $"Convert(Код, 'System.String') = '{searchAccuracy}' " +
                    $"OR ФИО = '{searchAccuracy}' " +
                    $"OR Адрес = '{searchAccuracy}'" +
                    $"OR Convert(Телефон, 'System.String') = '{searchAccuracy}'" +
                    $"OR Convert(ID_GR, 'System.String') = '{searchAccuracy}'";
            } else
            {
                facultyView.RowFilter = "";
                groupView.RowFilter = "";
                studentView.RowFilter = "";
            }
        }
    }
}
