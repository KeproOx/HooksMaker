using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HookahMaker
{
    class Aroma
    {
        #region Class Fields

        private int iAromaType; // The type of the aroma (ID in the DB)
        private string sAromaName; // The aroma name, example : Banana power
        private string sAromaIngredients; // Rhe aroma ingredients (not necessary)
        private string sAromaProducer; // The aroma producer, where it comes from
        private int iAromaQuantity; // The quantity of aroma into the "head" of the Hookah, can go up to 3 levels "1 : a little, 2 : good amount, 3 : charged to the max (price up !)"
        private double dAromaCalculatedPrice = 0.0; // the calculated price of the aroma
        private double dAromaPrice = 0.0; // initial Price of the aroma

        #endregion Class Fields

        #region Properties

        /// <summary>
        /// Returns or modify the aroma type
        /// </summary>
        public int AromaType
        {
            get { return iAromaType; }
            set
            {
                if (value >= 1)
                {
                    iAromaType = value;
                }
            }
        }


        /// <summary>
        /// Obtains or modify the aroma's name
        /// </summary>
        public string AromaName
        {
            get { return sAromaName; }
            set
            {
                //Testing if the string contains only letters
                if (Hookah.isTextOnlyLetters(value))
                {
                    // Testing the value, if null, does nothing
                    if (value != "")
                    {
                        sAromaName = value;
                    } 
                }
            }
        }

        /// <summary>
        /// Obtains or modify the aroma's ingredients
        /// </summary>
        public string AromaIngredients
        {
            get { return sAromaIngredients; }
            set
            {
                //Testing if the string contains only letters
                if (Hookah.isTextOnlyLetters(value))
                {
                    // Testing the value, if null, does nothing
                    if (value != "")
                    {
                        sAromaIngredients = value;
                    } 
                }
            }
        }

        /// <summary>
        /// Obtains or modify the aroma's producer
        /// </summary>
        public string AromaProducer
        {
            get { return sAromaProducer; }
            set
            {
                //Testing if the string contains only letters
                if (Hookah.isTextOnlyLetters(value))
                {
                    // Testing the value, if null, does nothing
                    if (value != "")
                    {
                        sAromaProducer = value;
                    } 
                }
            }
        }

        /// <summary>
        /// Obtains or modify the aroma's quantity
        /// </summary>
        public int AromaQuantity
        {
            get { return iAromaQuantity; }
            set
            {
                //Testing the value, it can't go lower than 1 and upper than 3
                if (value >= 1 && value <= 3)
                {
                    iAromaQuantity = value;
                }
                //if the price is already been taken out of the DB, calculate the new price
                if (dAromaPrice != 0.0)
                {
                    CalculatePrice();
                    
                }
            }
        }

        /// <summary>
        /// Returns the calculated price of the aroma depending the value of AromaQuantity
        /// </summary>
        public double AromaPrice
        {
            get { return dAromaCalculatedPrice; }
        }
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor loading an aroma by his ID, can be initiated without param
        /// </summary>
        /// <param name="aromatype">ID of the wanted aroma,</param>
        public Aroma(int aromatype = 1, int aromaquantity=2)
        {
            this.AromaType = aromatype;
            this.AromaQuantity = aromaquantity;
            var datareader = Hookah.LoadById(this, aromatype);
            //Attibuting values into the class
            if (datareader.HasRows)
            {
                datareader.Read();
                this.AromaIngredients = datareader["AromaIngredients"].ToString();
                this.AromaName = datareader["AromaName"].ToString();
                this.AromaProducer = datareader["AromaProducer"].ToString();
                this.dAromaPrice = (double)datareader["AromaPrice"];            
                //Closing the connection
                MainWindow.dbconnection.Close();
                //Calculating the price with the corresponding factor
                CalculatePrice();
            }


        }

        /// <summary>
        /// Constructor loading an aroma by his name
        /// </summary>
        /// <param name="aromaname">Name of the wanted aroma</param>
        public Aroma(string aromaname,int aromaquantity = 2)
        {
            this.AromaName = aromaname;
            this.AromaQuantity = aromaquantity;
            var datareader = Hookah.LoadByName(this,aromaname);

            //Attibuting values into the class
            if (datareader.HasRows)
            {
                datareader.Read();
                this.iAromaType = int.Parse(datareader["AromaType"].ToString());
                this.AromaIngredients = datareader["AromaIngredients"].ToString();
                this.AromaProducer = datareader["AromaProducer"].ToString();
                this.dAromaPrice = (double)datareader["AromaPrice"];
                //Calculating the price with the corresponding factor
                CalculatePrice();
            }

            MainWindow.dbconnection.Close();
        }

        #endregion Constructors

        #region Class Methods
        /// <summary>
        /// Calculate a new price depending of the aroma quantity
        /// </summary>
        /// <param name="price">The basic price of the aroma</param>
        private void CalculatePrice()
        {
            double CalculatedPrice = 0.0;
            if (iAromaQuantity == 1)
                CalculatedPrice = dAromaPrice * 0.75;
            else if (iAromaQuantity == 2)
                CalculatedPrice = dAromaPrice;
            else if (iAromaQuantity == 3)
                CalculatedPrice = dAromaPrice * 1.25;
            this.dAromaCalculatedPrice = CalculatedPrice;
        }

        /// <summary>
        /// Currently not working, will implement in the future
        /// </summary>
        public void Update()
        {
            Hookah.Modify(this, this.AromaType);
        }

        #endregion Class Methods
    }
}
