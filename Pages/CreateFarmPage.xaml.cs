using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Optika.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateFarmPage.xaml
    /// </summary>
    public partial class CreateFarmPage : Page
    {
        public CreateFarmPage(int idPerson)
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Trim().Length > 0)
            {
                if (FirstNameBox.Text.Trim().Length > 0)
                {
                    if (logRegBox.Text.Trim().Length > 0)
                    {
                        if (passRegBox.Password.Trim().Length > 0)
                        {
                            if (logRegBox.Text.Trim().LastIndexOf('@') < logRegBox.Text.Trim().LastIndexOf('.'))
                            {
                                if (!Regex.IsMatch(logRegBox.Text.Trim(), @"\p{IsCyrillic}")) // регулярки нужно загуглить
                                {
                                    if (!Regex.IsMatch(passRegBox.Password.Trim(), @"\p{IsCyrillic}")) // регулярки нужно загуглить
                                    {
                                        int idPerson = 0;
                                        try
                                        {
                                            string constring = ConfigurationManager.ConnectionStrings["OptikaDB"].ConnectionString;
                                            using (SqlConnection conn = new SqlConnection(constring))
                                            {
                                                conn.Open();
                                                SqlCommand sqlCommand = new SqlCommand("exec  CreatePers @Name,@First_Name,@Middle_Name,@Login, @Password, 1", conn);
                                                sqlCommand.Parameters.AddWithValue("@Name", NameBox.Text.Trim());
                                                sqlCommand.Parameters.AddWithValue("@First_Name", FirstNameBox.Text.Trim());
                                                sqlCommand.Parameters.AddWithValue("@Middle_Name", MiddleNameBox.Text.Trim());
                                                sqlCommand.Parameters.AddWithValue("@Login", logRegBox.Text.Trim().ToLower());
                                                sqlCommand.Parameters.AddWithValue("@Password", passRegBox.Password.Trim().ToLower());
                                                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                                                sqlData.Read();
                                                idPerson = sqlData.GetInt32(0);
                                                sqlData.Close();
                                            }
                                        }
                                        catch { MessageBox.Show("ошибка базы данных войдите позже"); idPerson = 0; }
                                        if (idPerson > 0)
                                        {
                                            NameBox.Text = null;
                                            FirstNameBox.Text = null;
                                            MiddleNameBox.Text = null;
                                            logRegBox.Text = null;
                                            passRegBox.Password = null;
                                            MessageBox.Show("Пользрователь создан успешно");
                                        }
                                        else MessageBox.Show("Пользователь уже есть");

                                    }
                                    else MessageBox.Show("в пароле не может быть русских букв");
                                }
                                else MessageBox.Show("В логине не может быть русских букв");
                            }
                            else MessageBox.Show("Логин введен не корректно");
                        }
                        else MessageBox.Show("Введите пожалуйста пароль");
                    }
                    else MessageBox.Show("Введите пожалуйста логин");
                }
                else MessageBox.Show("Введите пожалуйста Фамилию");
            }
            else MessageBox.Show("Введите пожалуйста имя");


        }

    }
}
