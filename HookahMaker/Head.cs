using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HookahMaker
{
    class Head
    {
        #region Class Fields

        private int iHeadType; // The type of the head, for example : a better quality means a better designed head, and naturally a higher price ;)
        private string sHeadName; // The name of the Head, only to help user to identify the head they want
        private string sHeadProducer; // The producer of the Head
        private double dHeadPrice; // price of the head



        #endregion Class Fields

        #region Properties

        /// <summary>
        /// Returns or modify the head quality
        /// </summary>
        public int HeadType
        {
            get { return iHeadType; }
            set
            {
                //Test of the value, can't go lower than 1
                if (value >= 1)
                {
                    iHeadType = value;
                }
            }
        }

        /// <summary>
        /// Returns or modify the head name
        /// </summary>
        public string HeadName
        {
            get { return sHeadName; }
            set
            {
                // Test of the value, can't be an empty string
                if (value != "")
                {
                    sHeadName = value;
                }
            }
        }

        /// <summary>
        /// Returns or modify the head producer
        /// </summary>
        public string HeadProducer
        {
            get { return sHeadProducer; }
            set
            {
                // Test of the value, can't be an empty string again
                if (value != "")
                {
                    sHeadProducer = value;
                }
            }
        }

        /// <summary>
        /// Returns the head price
        /// </summary>
        public double HeadPrice
        {
            get { return dHeadPrice; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instanciate a Head object, if no parameters are specified, a default head is used instead
        /// </summary>
        /// <param name="headtype">The ID of the head, this param is optionnal</param>
        public Head(int headtype = 1)
        {
            this.HeadType = headtype;
            var datareader = Hookah.LoadById(this,headtype);

            if (datareader.HasRows)
            {
                datareader.Read();
                this.HeadName = datareader["HeadName"].ToString();
                this.HeadProducer = datareader["HeadProducer"].ToString();
                this.dHeadPrice = (double)datareader["HeadPrice"];
                MainWindow.dbconnection.Close();
            }
        }

        /// <summary>
        /// Instanciate a Head object with params
        /// </summary>
        /// <param name="headname">Head's Name</param>
        public Head(string headname)
        {
            this.HeadName = headname;
            var datareader = Hookah.LoadByName(this, headname);

            if (datareader.HasRows)
            {
                this.iHeadType = (int)datareader["HeadType"];
                this.HeadProducer = datareader["HeadProducer"].ToString();
                this.dHeadPrice = (double)datareader["HeadPrice"];
                MainWindow.dbconnection.Close();
            }
        }

        #endregion Constructors

        #region Class Methods


        #endregion Class Methods
    }
}
