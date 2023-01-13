<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Patients2.aspx.cs" Inherits="properties_patients_database.Patients2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patients 2</title>
    <link href="site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>
            <a href="Default.aspx">Home</a>- Patients 2</h2>
           <hr />
        <p>
			List of Visits sorted by date</p>
		<p>
			Date&nbsp; PatientLastName Charges</p>
        <asp:TextBox ID="txtPatientsAboveAvg" runat="server" Height="270px" TextMode="MultiLine" 
            Width="414px"></asp:TextBox>

    </div>
        <asp:TextBox ID="txtMsg" runat="server" Height="297px" TextMode="MultiLine" Width="517px"></asp:TextBox>
    </form>
</body>
</html>
