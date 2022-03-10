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

namespace Optika.PersPages
{
    /// <summary>
    /// Логика взаимодействия для BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Page
    {
        ObservableCollection<AllClass.PillsClass> AllPills = new ObservableCollection<AllClass.PillsClass>();
     
        int idFarm = 0;
        string sortStr = " dbo.Pills.id_Pills";
     
        public BasketPage(int idPerson)
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

        private AllClass.PillsClass[] AllPillsP(string sort)
        {
            
            AllClass.PillsClass[] pills;
            Byte[] image = null;
            string constring = ConfigurationManager.ConnectionStrings["OptikaDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constring))
            {

                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("select COUNT(dbo.Pills.id_Pills) from  dbo.Pills inner join dbo.Orders ON dbo.Orders.id_Pills = dbo.Pills.id_Pills where dbo.Orders.id_Person = " + idFarm.ToString()+ "AND dbo.Orders.Sold = 0", conn);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                sqlData.Read();
                pills = new AllClass.PillsClass[(int)sqlData.GetValue(0)];
                sqlData.Close();
                sqlCommand = new SqlCommand("select  (select TOP(1) dbo.Images.img from dbo.Images where dbo.Pills.id_Pills = dbo.Images.id_Pills ), dbo.Pills.id_Pills,dbo.Pills.Name_Pills,dbo.Pills.[description], dbo.Pills.Price  from  dbo.Pills left outer join dbo.Pills_Numbers ON dbo.Pills.id_Pills = dbo.Pills_Numbers.id_pills inner join dbo.categAll ON dbo.Pills.id_Pills=dbo.categAll.id_Pills inner join dbo.Orders ON dbo.Orders.id_Pills=dbo.Pills.id_Pills where dbo.Orders.id_Person ="+ idFarm.ToString() + " AND dbo.Orders.Sold = 0  group by  dbo.Pills.id_Pills,dbo.Pills.Name_Pills,dbo.Pills.[description], dbo.Pills.Price  order by" + sort, conn);
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
                        pills[i].img = bmp;
                    }
                    pills[i].idPills = sqlData.GetInt32(1);
                    pills[i].Name = sqlData.GetString(2);
                    pills[i].Option = sqlData.GetString(3);
                    pills[i].Price = sqlData.GetDecimal(4);
                  
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
                string constring = ConfigurationManager.ConnectionStrings["OptikaDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(constring))
                {

                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("update dbo.Orders set sold = 1 where id_Pills =" + pills.idPills.ToString() + " AND id_Person =" + idFarm.ToString(), conn);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    sqlData.Read();
                }
                AllPills = new ObservableCollection<AllClass.PillsClass>();
                AllClass.PillsClass[] pills1 = AllPillsP(sortStr);
                foreach (var pil in pills1)
                {
                    AllPills.Add(pil);
                }
                listPills.ItemsSource = AllPills;

                MessageBox.Show("Поздравляем с покупкой!");
            }
        }

       

        private void PlusPillsDB(int Count, int pillsid, DateTime? date, int idPers)
        {

        }

        //procedure PlusPills @count int, @idPills int, @dataImport datetime, @idFarm int 

    }
}

