using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
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

namespace Optika.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewPillsPage1.xaml
    /// </summary>
   
    public partial class NewPillsPage1 : Page
    {
        ObservableCollection<AllClass.ImageClass> AllPhoto=  new ObservableCollection<AllClass.ImageClass>();
        public NewPillsPage1(int id_Farm)
        {

            InitializeComponent();
        }

        private void DateBT_Click(object sender, RoutedEventArgs e)
        {
           /* if (ViewCalend.Visibility == Visibility.Hidden) ViewCalend.Visibility = Visibility.Visible;
            else ViewCalend.Visibility = Visibility.Hidden;*/

        }
        private void SaveBT_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Trim().Length > 0)
            {
                if (DescriptionBox.Text.Trim().Length > 0)
                {
                    if (PriceBox.Text.Trim().Length > 0)
                    {
                        if (CategoryBox.Text.Trim().Length>0)
                        {
                            float res = 0;
                            if (float.TryParse(PriceBox.Text.Trim(), out res)) 
                            {
                                int idPills = PlusPillsDB(NameBox.Text.Trim(), DescriptionBox.Text.Trim(), TagsBox.Text.Trim(), res);
                                if (idPills != 0)
                                {
                                    AllPlusCategDB(idPills, CategoryBox.Text.Trim());
                                    AllPhotoDB(idPills, AllPhoto);
                                    MessageBox.Show("таблетки зарегестрированы");
                                    NameBox.Text = null;
                                    DescriptionBox.Text = null;
                                    PriceBox.Text = null;
                                    CategoryBox.Text = null;
                                    TagsBox.Text = null;
                                    AllPhoto = new ObservableCollection<AllClass.ImageClass>();
                                    listImage.ItemsSource = AllPhoto;
                                }
                            }
                            else { MessageBox.Show("В цене не может быть букв"); }
                        }
                        else { MessageBox.Show("Пожалуйста заполните поле с категорией");}
                    }
                    else { MessageBox.Show("Пожалуйста заполните поле с цена");}
                }
                else { MessageBox.Show("Пожалуйста заполните поле с описанием"); }
            }
            else { MessageBox.Show("Пожалуйста заполните поле с названием");}

        }

        private void AllPhotoDB(int idPills, ObservableCollection<AllClass.ImageClass> Photos)
        {
            foreach (var photo in Photos)
            {
                PlusPhotoDB(idPills, photo.way);
            }

        }

            private void PlusPhotoDB(int idPills,string photoWay )
        {
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["OptikaDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("insert into dbo.Images(id_Pills,img) values(" + idPills.ToString() + ", (SELect BulkColumn from OPENROWSET(BULK N'" + photoWay + "', SINGLE_BLOB)AS image))", conn);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    sqlData.Read();

                /*  conn.Open();
                SqlCommand sqlCommand = new SqlCommand("exec PhotoPils @idPills,@Photo", conn);
                sqlCommand.Parameters.AddWithValue("@idPills",idPills);
                sqlCommand.Parameters.AddWithValue("@Photo",Photo);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                sqlData.Read();                 
                sqlData.Close();*/
            
                }
            }
           catch { MessageBox.Show("ошибка загрузки фотографий повторите позже");}
        }
        private int PlusPillsDB(string Name, string description, string tags, float Price)
        {
            int IdPills = 0;
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["OptikaDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("exec NewPils @Name ,@description ,@tags , @Price", conn);
                    sqlCommand.Parameters.AddWithValue("@Name", Name);
                    sqlCommand.Parameters.AddWithValue("@description", description);
                    sqlCommand.Parameters.AddWithValue("@tags", tags);
                    sqlCommand.Parameters.AddWithValue("@Price", Price);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    sqlData.Read();
                    if(!sqlData.IsDBNull(0))
                    IdPills = sqlData.GetInt32(0);
                    sqlData.Close();
                }
            }
            catch { MessageBox.Show("ошибка загрузки таблеток повторите позже"); IdPills = 0; }
            return IdPills;
        }


        private void AllPlusCategDB(int idPills, string AllNameCateg)
        {
            string[] words = AllNameCateg.Split(',');
            for (int i = 0; i < words.Length; i++)
                PlusCategDB(idPills, words[i]);

        }
        private void PlusCategDB(int idPills, string NameCateg)
        {
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["OptikaDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("exec PlusCateg @idPills,@NameCateg", conn);
                    sqlCommand.Parameters.AddWithValue("@idPills", idPills);
                    sqlCommand.Parameters.AddWithValue("@NameCateg",NameCateg);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    sqlData.Read();
                    sqlData.Close();
                }
            }
            catch { MessageBox.Show("ошибка загрузки категорий повторите позже"); }
        }


        private void PlusPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog iconAdmin = new OpenFileDialog();
            iconAdmin.Title = "Выберете понравившееся фото";
            iconAdmin.Filter = "Image files | *.PNG";
            if (iconAdmin.ShowDialog() == true)
            {
                AllClass.ImageClass image = new AllClass.ImageClass();
                image.way = iconAdmin.FileName;
                image.img = new BitmapImage(new Uri(image.way));
                AllPhoto.Add(image);
                listImage.ItemsSource = AllPhoto;
            }
        }
        //create procedure NewPils @Name nvarchar(MAX),@description nvarchar(Max),@tags nvarchar(max), @Price Money
        //PlusCateg @idPills nvarchar(MAX),@NameCateg nvarchar(Max)
        //PhotoPils @idPills int,@Photo image

    }
}
