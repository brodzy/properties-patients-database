using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace properties_patients_database {
	public partial class Patients2 : System.Web.UI.Page {
		string dbType = "Access_Patients";

		protected void Page_Load(object sender, EventArgs e) {
			IDbCommand cmd = ConnectionFactory.GetCommand(dbType);
			GenerateVisitListSQL(cmd);

			try
			{

				cmd.Connection.Open();
				IDataReader dr = cmd.ExecuteReader();

				txtMsg.Text = "Attempting to read from: " + dbType + " database" + Environment.NewLine;
				txtMsg.Text += "IDbConnection.State: " + cmd.Connection.State.ToString() + Environment.NewLine; ;
				txtMsg.Text += "IDataReader.IsClosed: " + dr.IsClosed + Environment.NewLine;
				txtMsg.Text += "cmd.CommandText: " + cmd.CommandText + Environment.NewLine + Environment.NewLine;


				while(dr.Read()) {
					DateTime dtDate = (DateTime)dr.GetValue(0);
					String date = dtDate.ToString("MM/dd/yyyy");
					String lname = dr.GetString(1);
					decimal charge = dr.GetDecimal(2);

					string visit = String.Format("{0,10:0} {1,-14:0} {2,9:$0,0.00}", date, lname, charge);
					txtPatientsAboveAvg.Text += visit + Environment.NewLine;
				}

				dr.Close();
				cmd.Connection.Close();

			}
			catch (Exception ex)
			{
				txtMsg.Text += "\r\nError in SelectedIndexChanged\r\n";
				txtMsg.Text += ex.ToString();
			}
		}

		private void GenerateVisitListSQL(IDbCommand cmd)
		{
			cmd.CommandText =
				"SELECT " +
					"Visits.VisitDate, " +
					"Patients.LastName, " +
					"Visits.Charge " +
				"FROM " +
					"Patients " +
				"INNER JOIN " +
					"Visits " +
				"ON " +
					"Patients.PatientID = Visits.PatientID " +
				"ORDER BY " +
					"Visits.VisitDate Asc ";
		}

	}
}