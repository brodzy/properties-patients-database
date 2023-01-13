using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace properties_patients_database {
	public partial class Patients3 : System.Web.UI.Page {
		string dbType = "Access_Patients";

		protected void Page_Load(object sender, EventArgs e) {
			if (!Page.IsPostBack)
			{
				DisplayPatients(ddPatients);
				// Select first team.
				ddPatients.SelectedIndex = 0;
				DisplayVisits();
			}
            else
            {
				DisplayVisits();
			}
			
		}
		
		private void DisplayVisits()
        {
			IDbCommand cmd = ConnectionFactory.GetCommand(dbType);
			GenerateSelectPatientsSQL(cmd, ddPatients);

			txtVisitAndPreCharges.Text = "";
			try
			{

				cmd.Connection.Open();
				IDataReader dr = cmd.ExecuteReader();

				txtMsg.Text = "Attempting to read from: " + dbType + " database" + Environment.NewLine;
				txtMsg.Text += "IDbConnection.State: " + cmd.Connection.State.ToString() + Environment.NewLine; ;
				txtMsg.Text += "IDataReader.IsClosed: " + dr.IsClosed + Environment.NewLine;
				txtMsg.Text += "cmd.CommandText: " + cmd.CommandText + Environment.NewLine + Environment.NewLine;

				while (dr.Read())
				{
					DateTime dtDate = (DateTime)dr.GetValue(0);
					String date = dtDate.ToString("MM/dd/yyyy");
					decimal charge = dr.GetDecimal(1);
					int id = dr.GetInt32(2);

					String visit = String.Format("{0,10:0} {1,-14:$0,0.00} {2,9:0}", date, charge, id);
					txtVisitAndPreCharges.Text += visit + Environment.NewLine;
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

		private void DisplayPatients(DropDownList dd)
		{
			IDataReader dr;
			IDbCommand cmd = ConnectionFactory.GetCommand(dbType);

			GeneratePatientsSQL(cmd);

			try
			{
				cmd.Connection.Open();
				dr = cmd.ExecuteReader();
				dd.DataSource = dr;
				dd.DataTextField = "LastName";
				dd.DataValueField = "PatientID";
				dd.DataBind();

				cmd.Connection.Close();
				dr.Close();
			}
			catch (Exception ex)
			{
				txtMsg.Text = "Error displaying teams\r\n";
				txtMsg.Text += ex.ToString();
			}

		}

		private void GenerateSelectPatientsSQL(IDbCommand cmd, DropDownList dd)
		{
			cmd.CommandText =
				"SELECT " +
					"Visits.VisitDate, " +
					"Visits.Charge, " +
					"Visits.VisitID " +
				"FROM " +
					"Visits " +
				"WHERE " +
					"Visits.PatientID = " + dd.SelectedValue + " " +
				"ORDER BY " +
					"Visits.VisitDate Asc ";
		}

		private void GeneratePatientsSQL(IDbCommand cmd)
		{
			// Set select SQL statement.
			cmd.CommandText =
				"SELECT " +
					"Patients.LastName, " +
					"Patients.PatientID " +
				"FROM " +
					"Patients " +
				"ORDER BY " +
					"Patients.LastName ASC";

		}
    }
}