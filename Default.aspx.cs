using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace properties_patients_database { //Sorting won't update in textbox
	public partial class Default : System.Web.UI.Page {
        readonly string dbType = "Access_Properties";
		protected void Page_Load(object sender, EventArgs e)
		{

			if (!Page.IsPostBack)
            {
				displayTable();
            }
            else
            {
				displayTable();
			}

		}

		private void displayTable()
        {
			IDbCommand cmd = ConnectionFactory.GetCommand(dbType);
			txtProperties.Text = "";
			if (rblSortType.SelectedValue.Equals("Sq. Feet"))
			{
				GetSQLSqFeet(cmd);
			}
			else
			{
				GetSQLPrice(cmd);
			}

			try
			{

				cmd.Connection.Open();
				IDataReader dr = cmd.ExecuteReader();

				txtMsg.Text = "Attempting to read from: " + dbType + " database" + Environment.NewLine;
				txtMsg.Text += "IDbConnection.State: " + cmd.Connection.State.ToString() + Environment.NewLine; ;
				txtMsg.Text += "IDataReader.IsClosed: " + dr.IsClosed + Environment.NewLine;
				txtMsg.Text += "cmd.CommandText: " + cmd.CommandText + Environment.NewLine + Environment.NewLine;


				//Uses the DisplayPropertiesList method to read from the database and return a List of Property objects.
				List<Property> properties = BuildPropertyList(dr);

				dr.Close();
				cmd.Connection.Close();

				//Updating labels of number of properties, average price, and above average price
				int numProperties = properties.Count;
				lblNumProperties.Text = String.Format(numProperties.ToString()) + Environment.NewLine;
				double avgPrice = GetAveragePrice(properties);
				lblAveragePrice.Text = String.Format("${0:0.##}", avgPrice) + Environment.NewLine;
				int aboveAvgPrice = GetAboveAveragePrice(properties);
				lblNumAboveAvgPrice.Text = String.Format(aboveAvgPrice.ToString()) + Environment.NewLine;

				DisplayPropertiesList(properties);

			}
			catch (Exception ex)
			{
				txtMsg.Text += "\r\nError in SelectedIndexChanged\r\n";
				txtMsg.Text += ex.ToString();
			}
		}

		private void GetSQLPrice(IDbCommand cmd)
		{
			cmd.CommandText =
				"SELECT " +
					"Properties.ListPrice, " +
					"Properties.SqFeet, " +
					"Properties.Beds, " +
					"Properties.Baths, " +
					"Properties.YearBuilt " +
				"FROM " +
					"Properties " +
				"ORDER BY " +
					"Properties.ListPrice Asc";
		}

		private void GetSQLSqFeet(IDbCommand cmd)
		{
			cmd.CommandText =
				"SELECT " +
					"Properties.ListPrice, " +
					"Properties.SqFeet, " +
					"Properties.Beds, " +
					"Properties.Baths, " +
					"Properties.YearBuilt " +
				"FROM " +
					"Properties " +
				"ORDER BY " +
					"Properties.SqFeet Asc";
		}

		private List<Property> BuildPropertyList(IDataReader dr)
		{
			List<Property> properties = new List<Property>();

			// Read the data from the data reader. Note that this is one-pass, forward only.
			while (dr.Read())
			{
				double price = dr.GetDouble(0);
				double sqFeet = dr.GetDouble(1);
				double beds = dr.GetDouble(2);
				double baths = dr.GetDouble(3);
				double year = dr.GetDouble(4);

				Property p = new Property(price, sqFeet, beds, baths, year);
				properties.Add(p);
			}
			return properties;
		}

		private void DisplayPropertiesList(List<Property> properties)
		{
			foreach (Property p in properties)
			{
				String prop = String.Format("{0,8:$0,0} {1,5:0}   {2,2:0}    {3,2:0}    {4,4:0}      {5,6:$0.00}", p.ListPrice, p.SqFeet, p.Beds, p.Baths, p.YearBuilt, p.PricePerSqFoot);
				txtProperties.Text += prop + Environment.NewLine;
			}
		}

		private double GetAveragePrice(List<Property> properties)
		{
			double sum = 0.0;

			foreach (Property p in properties) { 
			
				sum += p.ListPrice;
			}

			return sum / properties.Count;
		}

		private int GetAboveAveragePrice(List<Property> properties)
		{
			int count = 0;
			double avgPrice = GetAveragePrice(properties);
			foreach(Property p in properties)
            {
				if(p.ListPrice > avgPrice)
                {
					count++;
                }
            }

			return count;
		}


	}
}