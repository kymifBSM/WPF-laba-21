using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
using System.Windows.Shapes;

namespace WPF_laba_21
{
    /// <summary>
    /// Логика взаимодействия для InsertWin.xaml
    /// </summary>
    public partial class InsertWin : Window
    {
        private string clickedToolBarValue;
        public InsertWin(string clickedToolBar)
        {
            InitializeComponent();
            this.clickedToolBarValue = clickedToolBar;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (clickedToolBarValue == "ToolBar_1")
            {
                string[] labels = { "Введите код", "Введите факультет", "Введите курс", "Введите количество групп" };

                Button insertBtn = new Button ()
                {
                    Height = 50,
                    Width = 90,
                    Margin = new Thickness(40, 20, 40, 0),
                    FontSize = 14,
                    Background = new SolidColorBrush(Colors.GreenYellow),
                    Content= "Добавить"
                };
                insertBtn.Click += Insert_Click;

                for (int i = 0; i < labels.Length; i++)
                {
                    TextBox textBox = new TextBox()
                    {
                        Height = 25,
                        Width = 170,
                        Margin = new Thickness(40, 0, 40, 0),
                        FontSize = 14,
                    };

                    Label label = new Label()
                    {
                        Content = labels[i],
                        Height = 27,
                        Width = 220,
                        Margin = new Thickness(52, 0, 40, 0),
                        FontSize = 14,
                    };

                    MainSP.Children.Add(label);
                    MainSP.Children.Add(textBox);
                }
                MainSP.Children.Add(insertBtn);

            } else if (clickedToolBarValue == "ToolBar_2")
            {
                string[] labels = { "Введите код", "Введите название группы", "Введите фамилию старосты", "Введите количество", "Введите ID факультета" };

                Button insertBtn = new Button()
                {
                    Height = 50,
                    Width = 90,
                    Margin = new Thickness(40, 20, 40, 0),
                    FontSize = 14,
                    Background = new SolidColorBrush(Colors.GreenYellow),
                    Content = "Добавить"
                };
                insertBtn.Click += Insert_Click;

                for (int i = 0; i < labels.Length; i++)
                {
                    TextBox textBox = new TextBox()
                    {
                        Height = 25,
                        Width = 170,
                        Margin = new Thickness(40, 0, 40, 0),
                        FontSize = 14,
                    };

                    Label label = new Label()
                    {
                        Content = labels[i],
                        Height = 27,
                        Width = 220,
                        Margin = new Thickness(52, 0, 40, 0),
                        FontSize = 14,
                    };

                    MainSP.Children.Add(label);
                    MainSP.Children.Add(textBox);
                }
                MainSP.Children.Add(insertBtn);
            } else
            {
                string[] labels = { "Введите код", "Введите ФИО", "Введите адрес", "Введите телефон", "Введите ID группы" };

                Button insertBtn = new Button()
                {
                    Height = 50,
                    Width = 90,
                    Margin = new Thickness(40, 20, 40, 0),
                    FontSize = 14,
                    Background = new SolidColorBrush(Colors.GreenYellow),
                    Content = "Добавить"
                };
                insertBtn.Click += Insert_Click;

                for (int i = 0; i < labels.Length; i++)
                {
                    TextBox textBox = new TextBox()
                    {
                        Height = 25,
                        Width = 170,
                        Margin = new Thickness(40, 0, 40, 0),
                        FontSize = 14,
                    };

                    Label label = new Label()
                    {
                        Content = labels[i],
                        Height = 27,
                        Width = 220,
                        Margin = new Thickness(52, 0, 40, 0),
                        FontSize = 14,
                    };

                    MainSP.Children.Add(label);
                    MainSP.Children.Add(textBox);
                }
                MainSP.Children.Add(insertBtn);
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\DBs\Database111.accdb";
            string insertQuery = "";

            if (clickedToolBarValue == "ToolBar_1")
            {
                insertQuery = "INSERT INTO Факультет (Код, Факультет, Курс, [Количество групп]) VALUES (?, ?, ?, ?)";
            }
            else if (clickedToolBarValue == "ToolBar_2")
            {
                insertQuery = "INSERT INTO Группа (Код, [Название группы], [Фамилия старосты], Количество, [Факультет ID]) VALUES (?, ?, ?, ?, ?)";
            }
            else if (clickedToolBarValue == "ToolBar_3")
            {
                insertQuery = "INSERT INTO Студенты (Код, ФИО, Адрес, Телефон, ID_GR) VALUES (?, ?, ?, ?, ?)";
            }

            string value1 = ((TextBox)MainSP.Children[1]).Text;
            string value2 = ((TextBox)MainSP.Children[3]).Text;
            string value3 = ((TextBox)MainSP.Children[5]).Text;
            string value4 = ((TextBox)MainSP.Children[7]).Text;
            string value5 = "";

            if (clickedToolBarValue == "ToolBar_2" || clickedToolBarValue == "ToolBar_3")
            {
                value5 = ((TextBox)MainSP.Children[9]).Text;
            }

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("param1", value1);
                    command.Parameters.AddWithValue("param2", value2);
                    command.Parameters.AddWithValue("param3", value3);
                    command.Parameters.AddWithValue("param4", value4);

                    if (clickedToolBarValue == "ToolBar_2" || clickedToolBarValue == "ToolBar_3")
                    {
                        command.Parameters.AddWithValue("param5", value5);
                    }

                    command.ExecuteNonQuery();

                    this.Close();
                }
            }
        }

    }
}
