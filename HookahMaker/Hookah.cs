using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Data.Common;

namespace HookahMaker
{
    class Hookah
    {

        #region Class Fields

        private int CoalNumber; // The number of coal into the hookah (up to 3, minimum is 2)
        private Aroma hookahAroma; // The aroma the user has chose
        private Head hookahHead; // The head the user has chose
        private Base hookahBase; // The base the user choosed
        private Hose hookahHose; // the hose the user choosed
        private Calud hookahCalud; // Optionnal
        private double dtotalPrice; // total price of the hookah


        #endregion Class Fields

        #region Properties

        /// <summary>
        /// Returns the actual aroma
        /// </summary>
        public Aroma Aroma
        {
            get { return hookahAroma; }
        }

        /// <summary>
        /// Returns the actual base
        /// </summary>
        public Base Base
        {
            get { return hookahBase; }
        }

        /// <summary>
        /// Returns the actual calud
        /// </summary>
        public Calud Calud
        {
            get { return hookahCalud; }
        }

        /// <summary>
        /// Returns the actual head
        /// </summary>
        public Head Head
        {
            get { return hookahHead; }
        }

        /// <summary>
        /// Returns the actual hose
        /// </summary>
        public Hose Hose
        {
            get { return hookahHose; }
        }

        public double TotalPrice
        {
            get { return dtotalPrice; }
        }


        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instanciate all components with defaults values
        /// </summary>
        public Hookah()
        {
            this.CoalNumber = 3;
            hookahAroma = new Aroma();
            hookahBase = new Base();
            hookahCalud = new Calud();
            hookahHead = new Head();
            hookahHose = new Hose();
            CalculateTotalPrice();
        }

        #endregion Constructors

        #region Class Methods

        /// <summary>
        /// Method calculating the total price
        /// </summary>
        public void CalculateTotalPrice()
        {
            dtotalPrice = hookahAroma.AromaPrice + hookahBase.BasePrice + hookahCalud.CaludPrice + hookahHead.HeadPrice + hookahHose.HosePrice;
        }

        /// <summary>
        /// Method for loading objects by id
        /// </summary>
        /// <param name="sender">type of object to be loaded</param>
        /// <param name="id">Id of the object</param>
        internal static DbDataReader LoadById(object sender, int id)
        {
            //Get the connection from mainWindow and create a command with it
            MainWindow.dbcommand = MainWindow.dbconnection.CreateCommand();
            DbDataReader datareader;
            //Testing which object is transmitted before executing the command
            string sTableName = "";
            if (sender.GetType() == typeof(Aroma))      
                sTableName = "Aroma";            
            else if (sender.GetType() == typeof(Base))
                sTableName = "Base";
            else if (sender.GetType() == typeof(Calud))
                sTableName = "Calud";
            else if (sender.GetType() == typeof(Head))
                sTableName = "Head";
            else if (sender.GetType() == typeof(Hose))
                sTableName = "Hose";

            //parameting the command
            
            MainWindow.dbcommand.CommandText = "SELECT * FROM " + sTableName + " WHERE " + sTableName + "Type =" + id + ";";

            //Opening the connection in case it's closed
            if (MainWindow.dbconnection.State != ConnectionState.Open)
                MainWindow.dbconnection.Open();

            datareader = MainWindow.dbcommand.ExecuteReader();
            return datareader;

        }

        /// <summary>
        /// Method for loading objects by name
        /// </summary>
        /// <param name="sender">type of the object to be loaded</param>
        /// <param name="name">name of the object in the DB</param>
        /// <returns></returns>
        internal static DbDataReader LoadByName(object sender, string name)
        {
            //Get the connection from mainWindow and create a command with it
            MainWindow.dbcommand = MainWindow.dbconnection.CreateCommand();
            DbDataReader datareader;
            //Testing which object is transmitted before executing the command
            string sTableName = "";
            if (sender.GetType() == typeof(Aroma))
                sTableName = "Aroma";
            else if (sender.GetType() == typeof(Base))
                sTableName = "Base";
            else if (sender.GetType() == typeof(Calud))
                sTableName = "Calud";
            else if (sender.GetType() == typeof(Head))
                sTableName = "Head";
            else if (sender.GetType() == typeof(Hose))
                sTableName = "Hose";

            //parameting the command

            MainWindow.dbcommand.CommandText = "SELECT * FROM " + sTableName + " WHERE " + sTableName + "Name ='" + name + "';";

            //Opening the connection in case it's closed
            if (MainWindow.dbconnection.State != ConnectionState.Open)
                MainWindow.dbconnection.Open();

            datareader = MainWindow.dbcommand.ExecuteReader();
            return datareader;
        }

        /// <summary>
        /// Static method for testing if the text given contains anything other than letters
        /// </summary>
        /// <param name="text">The text to examine</param>
        /// <returns>Returns true if only characters are found, false if only one other thing is present</returns>
        internal static bool isTextOnlyLetters(string text)
        {
            List<bool> letterOk = new List<bool>();
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    letterOk.Add(true);
                }
                else if (c == ',' || c == ' ' || c == '\'')
                {
                    letterOk.Add(true);
                }
                else
                {
                    letterOk.Add(false);
                }
            }
            if (letterOk.Contains(false))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //Under are the methods needed to upload the component to the DB

        /// <summary>
        /// Method modifying the object in the DB, currently not working
        /// </summary>
        internal static void Modify(object sender, int id)
        {
            //Get the connection from mainWindow and create a command with it
            MainWindow.dbcommand = MainWindow.dbconnection.CreateCommand();
            DbDataReader datareader;

            //creating all of the objects
            Aroma aroma;
            Base baseobj;
            Calud calud;
            Head head;
            Hose hose;


            //Testing which object is transmitted before executing the command and casting it to access his properties
            string sTableName = "";
            if (sender.GetType() == typeof(Aroma))
            {
                sTableName = "Aroma";
                aroma = (Aroma)sender;
                MainWindow.dbcommand.CommandText = "UPDATE " + sTableName + " SET " +sTableName+"Name ='" + aroma.AromaName+ "', "+sTableName+"";
            }
            else if (sender.GetType() == typeof(Base))
            {
                sTableName = "Base";
                baseobj = (Base)sender;
            }
            else if (sender.GetType() == typeof(Calud))
            {
                sTableName = "Calud";
                calud = (Calud)sender;
            }
            else if (sender.GetType() == typeof(Head))
            {
                sTableName = "Head";
                head = (Head)sender;
            }
            else if (sender.GetType() == typeof(Hose))
            {
                sTableName = "Hose";
                hose = (Hose)sender;
            }
            //parameting the command
            MainWindow.dbcommand.CommandText = "UPDATE " + sTableName + " SET ";
        }

        /// <summary>
        /// Method uploading a new object into the DB
        /// </summary>
        internal static void Upload()
        {
            
        }

        /// <summary>
        /// Method that will check if the record is already existing, preventing modifying a non-existant record
        /// </summary>
        private void isExisting()
        {

        }
        #endregion Class Methods
    }
}
