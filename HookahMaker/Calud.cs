using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HookahMaker
{
    class Calud
    {

        #region Class Fields

        private int iCaludType; // Type of the calud
        private string sCaludName; // The name of the calud
        private string sCaludProducer; // The producer of the calud
        private double dCaludPrice; // price of the calud

        #endregion Class Fields

        #region Properties

        /// <summary>
        /// Returns or modify the Calud type
        /// </summary>
        public int CaludType
        {
            get { return iCaludType; }
            set
            {
                //The value needs to be at least 1 but not lower
                if (value >= 1)
                {
                    iCaludType = value;
                }
            }
        }

        /// <summary>
        /// Returns or modify the Calud name
        /// </summary>
        public string CaludName
        {
            get { return sCaludName; }
            set
            {
                if (Hookah.isTextOnlyLetters(value))
                {
                    //Value can't be an empty string
                    if (value != "")
                    {
                        sCaludName = value;
                    } 
                }
            }
        }

        /// <summary>
        /// Returns or modify the calud producer
        /// </summary>
        public string CaludProducer
        {
            get { return sCaludProducer; }
            set
            {
                if (Hookah.isTextOnlyLetters(value))
                {
                    //Value can't be an empty string
                    if (value != "")
                    {
                        sCaludProducer = value;
                    } 
                }
            }
        }

        /// <summary>
        /// Returns the calud price
        /// </summary>
        public double CaludPrice
        {
            get { return dCaludPrice; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Simple constructor with one optionnal param
        /// </summary>
        /// <param name="caludtype">the ID of the calud, optionnal param</param>
        public Calud(int caludtype = 1)
        {
            this.CaludType = caludtype;
            var datareader = Hookah.LoadById(this, caludtype);

            if (datareader.HasRows)
            {
                datareader.Read();
                this.CaludName = datareader["CaludName"].ToString();
                this.CaludProducer = datareader["CaludProducer"].ToString();
                this.dCaludPrice = (double)datareader["CaludPrice"];
                MainWindow.dbconnection.Close();
            }
        }

        /// <summary>
        /// Constructor loading a calud by his name
        /// </summary>
        /// <param name="caludname">name of the calud in the DB</param>
        public Calud(string caludname)
        {
            this.CaludName = caludname;
            var datareader = Hookah.LoadByName(this, caludname);
            if (datareader.HasRows)
            {
                datareader.Read();
                this.iCaludType = (int)datareader["CaludType"];
                this.CaludProducer = datareader["CaludProducer"].ToString();
                this.dCaludPrice = (double)datareader["CaludPrice"];
                MainWindow.dbconnection.Close();
            }
        }
        #endregion Constructors

        #region Class Methods

        #endregion Class Methods
    }
}
