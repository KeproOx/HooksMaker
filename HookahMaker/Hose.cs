using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HookahMaker
{
    class Hose
    {
        #region Class Fields

        private int iHoseType; // The quality of the Hose, means a different looking hose, or different materials
        private string sHoseName; // The hose name
        private string sHoseProducer; // The hose's producer
        private double dHosePrice; // The hose's price

        #endregion Class Fields

        #region Properties

        /// <summary>
        /// Returns or modify the hose Type
        /// </summary>
        public int HoseType
        {
            get { return iHoseType; }
            set
            {
                //value needs to be at least 1
                if (value >= 1)
                {
                    iHoseType = value;
                }
            }
        }

        /// <summary>
        /// Returns or modify the hose name
        /// </summary>
        public string HoseName
        {
            get { return sHoseName; }
            set
            {
                //Verification steps
                if (Hookah.isTextOnlyLetters(value))
                {
                    if (value != null)
                    {
                        sHoseName = value;
                    }
                }
            }
        }

        /// <summary>
        /// Returns or modify the hose producer
        /// </summary>
        public string HoseProducer
        {
            get { return sHoseProducer; }
            set
            {
                //Verification steps
                if (Hookah.isTextOnlyLetters(value))
                {
                    if (value != null)
                    {
                        sHoseProducer = value;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the price of the hose
        /// </summary>
        public double HosePrice
        {
            get { return dHosePrice; }
        }


        #endregion Properties

        #region Constructors

        /// <summary>
        /// Basic constructor loading a default hose
        /// </summary>
        /// <param name="hosetype">Optional param, you can precise what type of hose you want</param>
        public Hose(int hosetype = 1)
        {
            this.HoseType = hosetype;
            var datareader = Hookah.LoadById(this, hosetype);

            if (datareader.HasRows)
            {
                datareader.Read();

                this.HoseName = datareader["HoseName"].ToString();
                this.HoseProducer = datareader["HoseProducer"].ToString();
                this.dHosePrice = (double)datareader["HosePrice"];
                MainWindow.dbconnection.Close();
            }
        }

        /// <summary>
        /// Constructor loading a hose by his name
        /// </summary>
        /// <param name="hosename">the name of the hose</param>
        public Hose(string hosename)
        {
            this.HoseName = hosename;
            var datareader = Hookah.LoadByName(this, hosename);

            if (datareader.HasRows)
            {
                datareader.Read();

                this.iHoseType = (int)datareader["HoseType"];
                this.HoseProducer = datareader["HoseProducer"].ToString();
                this.dHosePrice = (double)datareader["HosePrice"];
                MainWindow.dbconnection.Close();
            }
        }

        #endregion Constructors

        #region Class Methods

        #endregion Class Methods
    }
}
