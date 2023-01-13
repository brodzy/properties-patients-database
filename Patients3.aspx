<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Patients3.aspx.cs" Inherits="properties_patients_database.Patients3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patients 3</title>
    <link href="site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h2><a href="Default.aspx">Home </a> - Patients 3 </h2>

        <hr />
        <p>
            Choose a Patient:
            <asp:DropDownList ID="ddPatients" runat="server" AutoPostBack="True">
            </asp:DropDownList>
        </p>
        <p>
            Visit &amp; Prescription Charges Charges:</p>
        <p>
            Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Visit&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Visit ID</p>
        <asp:TextBox ID="txtVisitAndPreCharges" runat="server" Height="193px" TextMode="MultiLine" 
            Width="371px"></asp:TextBox>

        <p>
            &nbsp;</p>
    </div>
        <asp:TextBox ID="txtMsg" runat="server" Height="195px" TextMode="MultiLine" Width="360px"></asp:TextBox>
    </form>
</body>
</html>
