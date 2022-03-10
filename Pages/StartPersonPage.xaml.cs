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
    /// Логика взаимодействия для StartPersonPage.xaml
    /// </summary>
    public partial class StartPersonPage : Page
    {
        ObservableCollection<AllClass.PillsClass> AllPills = new ObservableCollection<AllClass.PillsClass>();
        int IDpills = 0;
        int idFarm = 0;
        string sortStr = " dbo.Pills.id_Pills";
        int idcateg = 0;
        Frame frame1;
        public StartPersonPage(int idPers, int idCateg, Frame frame)
        {
            frame1 = frame;
            idFarm = idPers;
            InitializeComponent();
            AllClass.PillsClass[] pills = AllPillsP(sortStr, idCateg);
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
            AllClass.PillsClass[] pills = AllPillsP(sortStr, idcateg);
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
            AllClass.PillsClass[] pills = AllPillsP(sortStr, idcateg);
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
            AllClass.PillsClass[] pills = AllPillsP(sortStr, idcateg);
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
            AllClass.PillsClass[] pills = AllPillsP(sortStr, idcateg);
            foreach (var pil in pills)
            {
                AllPills.Add(pil);
            }
            listPills.ItemsSource = AllPills;

        }

        private AllClass.PillsClass[] AllPillsP(string sort, int id_cat)
        {
            string Searchstr = null;
            if (id_cat > 0)
            {
                Searchstr = "WHERE dbo.categAll.id_categ=" + id_cat.ToString();

            }
            AllClass.PillsClass[] pills;
            Byte[] image = null;
            string constring = ConfigurationManager.ConnectionStrings["OptikaDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constring))
            {

                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("select  COUNT(coll) from (select COUNT(dbo.categAll.id_Pills) as coll from dbo.categAll inner join dbo.Pills ON dbo.Pills.id_Pills=dbo.categAll.id_Pills "+ Searchstr + " group by dbo.categAll.id_Pills ) as bd", conn);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                sqlData.Read();
                pills = new AllClass.PillsClass[(int)sqlData.GetValue(0)];
                sqlData.Close();
                sqlCommand = new SqlCommand("select (select TOP(1) dbo.Images.img from dbo.Images where dbo.Pills.id_Pills = dbo.Images.id_Pills ), dbo.Pills.id_Pills,dbo.Pills.Name_Pills,dbo.Pills.[description], dbo.Pills.Price,(select SUM(dbo.Pills_Numbers.[count]) from dbo.Pills_Numbers where dbo.Pills_Numbers.id_pills= dbo.Pills.id_Pills) from  dbo.Pills left outer join dbo.Pills_Numbers ON dbo.Pills.id_Pills = dbo.Pills_Numbers.id_pills inner join dbo.categAll ON dbo.Pills.id_Pills=dbo.categAll.id_Pills "+ Searchstr + " group by  dbo.Pills.id_Pills,dbo.Pills.Name_Pills,dbo.Pills.[description], dbo.Pills.Price order by " + sort, conn);
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
            frame1.Navigate(new PersPages.WindowPillsPage(idFarm,pills.idPills,pills.Count));
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
                AllClass.PillsClass[] pills = AllPillsP(sortStr, idcateg);
                foreach (var pil in pills)
                {
                    AllPills.Add(pil);
                }
                listPills.ItemsSource = AllPills;
            }
        }


        private void PlusPillsDB(int Count, int pillsid, DateTime? date, int idPers)
        {
           
        }

        //procedure PlusPills @count int, @idPills int, @dataImport datetime, @idFarm int 

    }
}

