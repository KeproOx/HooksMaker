using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HookahMaker
{
    class Base
    {

        #region Class Fields

        private int iBaseType; // The type of the base, prices varying :))
        private string sBaseName; // The name of the base, only serves to the client to have a name in mind
        private string sBaseProducer; // The name of the base producer
        private double dBasePrice; // the price of the base

        #endregion Class Fields

        #region Properties


        /// <summary>
        /// Returns or modify the base's quality
        /// </summary>
        public int BaseType
        {
            get { return iBaseType; }
            set
            {
                //Value test, needs to be superior to 1
                if (value >= 1)
                {
                    iBaseType = value;
                }
            }
        }

        /// <summary>
        /// Returns or modify the base's name
        /// </summary>
        public string BaseName
        {
            get { return sBaseName; }
            set
            {
                //First test of the chain, making it sure it contains only letters
                if (Hookah.isTextOnlyLetters(value))
                {
                    // value test, can't be ""
                    if (value != "")
                    {
                        sBaseName = value;
                    } 
                }
            }
        }

        /// <summary>
        /// Returns or modify the base's producer
        /// </summary>
        public string BaseProducer
        {
            get { return sBaseProducer; }
            set
            {
                if (Hookah.isTextOnlyLetters(value))
                {
                    // value test, can't be ""
                    if (value != "")
                    {
                        sBaseProducer = value;
                    } 
                }
            }
        }

        /// <summary>
        /// Returns the price of the base
        /// </summary>
        public double BasePrice
        {
            get { return dBasePrice; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor loading by ID the wanted base
        /// </summary>
        /// <param name="basetype">Id of the base, optionnal param</param>
        public Base(int basetype = 1)
        {
            this.BaseType = basetype;
            var datareader = Hookah.LoadById(this, basetype);

            if (datareader.HasRows)
            {
                datareader.Read();
                this.BaseName = datareader["BaseName"].ToString();
                this.BaseProducer = datareader["BaseProducer"].ToString();
                this.dBasePrice = (double)datareader["BasePrice"];
                MainWindow.dbconnection.Close();
            }
        }

        /// <summary>
        /// Constructor loading by ID the wanted base
        /// </summary>
        /// <param name="basetype"></param>
        public Base(string basename)
        {
            this.BaseName = basename;
            var datareader = Hookah.LoadByName(this,basename);

            if (datareader.HasRows)
            {
                datareader.Read();
                this.iBaseType = (int)datareader["BaseType"];
                this.BaseProducer = datareader["BaseProducer"].ToString();
                this.dBasePrice = (double)datareader["BasePrice"];
                MainWindow.dbconnection.Close();
            }
        }

        #endregion Constructors

        #region Class Methods

        #endregion Class Methods
    }
}
