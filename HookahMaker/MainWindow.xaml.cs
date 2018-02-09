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
// usings necessary to make a Db connection
using System.Data;
using System.Data.Common;

namespace HookahMaker
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static DbProviderFactory dbfactory;
        internal static DbConnection dbconnection;
        internal static DbCommand dbcommand;


        public MainWindow()
        {
            InitializeComponent();
            if (TestMySQLConnection())
            {
                Title = "NANIMONO";
                Hookah hookah = new Hookah();
                hookah.Aroma.AromaQuantity = 3;
                hookah.CalculateTotalPrice();
            }
            else
            {
                Title = "NANI";
            }

            


        }

        private bool TestMySQLConnection()
        {
            bool success = true;
            dbfactory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
            dbconnection = dbfactory.CreateConnection();
            dbconnection.ConnectionString = "Server= localhost; DATABASE=mydb; UID=root; PASSWORD=password";
            // Ouverture de la connexion si nécessaire
            if (dbconnection.State != ConnectionState.Open)
            {
                try
                {
                    dbconnection.Open();
                }
                catch (Exception)
                {
                    success = false;
                }
                dbconnection.Close();
            }
            return success;
        }
    }
}
