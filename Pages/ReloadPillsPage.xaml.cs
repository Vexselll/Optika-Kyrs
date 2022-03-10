using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для ReloadPillsPage.xaml
    /// </summary>
    public partial class ReloadPillsPage : Page
    {
        ObservableCollection<AllClass.PillsClass> AllPills = new ObservableCollection<AllClass.PillsClass>();
        int IDpills = 0;
        int idFarm = 0;
        string sortStr = " dbo.Pills.id_Pills";
        public ReloadPillsPage(int idPerson)
        {
            idFarm = idPerson;
            InitializeComponent();
            AllClass.PillsClass[] pills = AllPillsP(sortStr);
            foreach (var pil in pills)
            {
                AllPills.Add(pil);
            }
            listPills.ItemsSource = AllPills;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
         

        }

        private void Option_Click(object sender, RoutedEventArgs e)
        {
            AllPills = new ObservableCollection<AllClass.PillsClass>();
            sortStr = " dbo.Pills.[description]";
            AllClass.PillsClass[] pills = AllPillsP(sortStr);
            foreach (var pil in pills)
            {
                AllPills.Add(pil);
            }
            listPills.ItemsSource = AllPills;

        }

        private void Name_Click(object sender, RoutedEventArgs e)
        {
            AllPills = new ObservableCollection<AllClass.PillsClass>();

            sortStr = " dbo.Pills.Name_Pills";
            AllClass.PillsClass[] pills = AllPillsP(sortStr);
            foreach (var pil in pills)
            {
                AllPills.Add(pil);
            }
            listPills.ItemsSource = AllPills;

        }

        private void Price_Click(object sender, RoutedEventArgs e)
        {
            AllPills = new ObservableCollection<AllClass.PillsClass>();

            sortStr = "  dbo.Pills.Price";
            AllClass.PillsClass[] pills = AllPillsP(sortStr);
            foreach (var pil in pills)
            {
                AllPills.Add(pil);
            }
            listPills.ItemsSource = AllPills;

        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            AllPills = new ObservableCollection<AllClass.PillsClass>();
            sortStr = " summ";
            AllClass.PillsClass[] pills = AllPillsP(sortStr);
            foreach (var pil in pills)
            {
                AllPills.Add(pil);
            }
            listPills.ItemsSource = AllPills;

        }

        private AllClass.PillsClass[] AllPillsP( string sort)
        {

            AllClass.PillsClass[] pills; 
            Byte[] image = null;
            string constring = ConfigurationManager.ConnectionStrings["OptikaDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constring))
            {

                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("select COUNT(dbo.Pills.id_Pills) from  dbo.Pills ", conn);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                sqlData.Read();
                pills = new AllClass.PillsClass[(int)sqlData.GetValue(0)];
                sqlData.Close();
                sqlCommand = new SqlCommand("select (select TOP(1) dbo.Images.img from dbo.Images where dbo.Pills.id_Pills = dbo.Images.id_Pills ), dbo.Pills.id_Pills,dbo.Pills.Name_Pills,dbo.Pills.[description], dbo.Pills.Price, SUM(dbo.Pills_Numbers.[count]) as summ from  dbo.Pills left outer join dbo.Pills_Numbers ON dbo.Pills.id_Pills = dbo.Pills_Numbers.id_pills  group by  dbo.Pills.id_Pills,dbo.Pills.Name_Pills,dbo.Pills.[description], dbo.Pills.Price order by " + sort , conn);
                sqlData = sqlCommand.ExecuteReader();
                int i = 0;
                while (sqlData.Read())
                {
                    pills[i] = new AllClass.PillsClass();
                  
                    if (!sqlData.IsDBNull(0))
                    {
                       
                           image = (Byte[])sqlData[0];
                        MemoryStream mc = new MemoryStream();
                        mc.Write(image, 0, image.Length);
                        BitmapImage bmp = new BitmapImage();
                        bmp.BeginInit();
                        bmp.StreamSource = mc;
                        bmp.EndInit();
                        pills[i].img= bmp;
                    }
                    pills[i].idPills = sqlData.GetInt32(1);
                    pills[i].Name = sqlData.GetString(2);
                    pills[i].Option = sqlData.GetString(3);
                    pills[i].Price = sqlData.GetDecimal(4);
                    if (!sqlData.IsDBNull(5))
                    {
                        pills[i].Count = sqlData.GetInt32(5);
                    }
                        i++;
                }
                sqlData.Close();
                return pills;
            }
           

        }

        private void listPills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllClass.PillsClass pills = (AllClass.PillsClass)listPills.SelectedItem;
            if (pills != null)
            {
                ClosedBorder.Visibility = Visibility.Visible;
                CalEnd.DisplayDateEnd = DateTime.Now;
                PlusBorder.Visibility = Visibility.Visible;
                    IDpills = pills.idPills;
            }
           
        }

        private void SavePlus_Click(object sender, RoutedEventArgs e)
        {
            int res = 0;
            if (int.TryParse(CountPlus.Text.Trim(), out res))
            {
               
                PlusPillsDB(res, IDpills, CalEnd.SelectedDate, idFarm);
                ClosedBorder.Visibility = Visibility.Hidden;
                PlusBorder.Visibility = Visibility.Hidden;
                CountPlus.Text = null;
                AllPills = new ObservableCollection<AllClass.PillsClass>();
                AllClass.PillsClass[] pills = AllPillsP(sortStr);
                foreach (var pil in pills)
                {
                    AllPills.Add(pil);
                }
                listPills.ItemsSource = AllPills;
            }
        }


        private void PlusPillsDB(int Count, int pillsid, DateTime? date,int idPers)
        {
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["OptikaDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("exec PlusPills @count, @idPills , @dataImport , @idFarm", conn);
                    sqlCommand.Parameters.AddWithValue("@count", Count);
                    sqlCommand.Parameters.AddWithValue("@idPills", pillsid);
                    sqlCommand.Parameters.AddWithValue("@dataImport", date);
                    sqlCommand.Parameters.AddWithValue("@idFarm", idPers);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    sqlData.Read();
                    sqlData.Close();
                }
            }
            catch { MessageBox.Show("ошибка загрузки таблеток повторите позже");}
        }

        //procedure PlusPills @count int, @idPills int, @dataImport datetime, @idFarm int 

    }
}
