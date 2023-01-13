namespace properties_patients_database {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Summary description for Property
    /// </summary>
    public class Property
    {
        private Double listPrice;

        public Double ListPrice
        {
            get { return listPrice; }
            set { listPrice = value; }
        }
        private Double sqFeet;

        public Double SqFeet
        {
            get { return sqFeet; }
            set { sqFeet = value; }
        }
        private Double beds;

        public Double Beds
        {
            get { return beds; }
            set { beds = value; }
        }
        private Double baths;

        public Double Baths
        {
            get { return baths; }
            set { baths = value; }
        }
        private Double yearBuilt;

        public Double YearBuilt
        {
            get { return yearBuilt; }
            set { yearBuilt = value; }
        }

        private double pricePerSqFoot;

        public double PricePerSqFoot
        {
            get { return pricePerSqFoot; }
            set { pricePerSqFoot = value; }
        }


        public Property(Double price, Double sqFeet, Double beds, Double baths, Double year)
        {
            this.listPrice = price;
            this.sqFeet = sqFeet;
            this.beds = beds;
            this.baths = baths;
            this.yearBuilt = year;
            pricePerSqFoot = this.listPrice / this.sqFeet;
        }

    }
}