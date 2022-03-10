using System;
using System.Collections.Generic;
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

namespace Optika.PersPages
{
    /// <summary>
    /// Логика взаимодействия для SelfPage.xaml
    /// </summary>
    public partial class SelfPage : Page
    {
        public SelfPage(int idPers)
        {
            InitializeComponent();
            string constring = ConfigurationManager.ConnectionStrings["OptikaDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constring))
            {

                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("select dbo.Person.[Name], dbo.Person.First_Name,dbo.Person.Middle_Name,dbo.Person.[Login],dbo.Person.[Password] from dbo.Person where dbo.Person.id_Person =" + idPers.ToString(), conn);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                if (sqlData.Read())
                {
                    Name.Text = sqlData.GetString(0);
                    First_Name.Text= sqlData.GetString(1);
                    Middle_Name.Text = sqlData.GetString(2);
                    Login.Text = sqlData.GetString(3);
                    Password.Text= sqlData.GetString(4);
                }

            }
        }
    }
}
