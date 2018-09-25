<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Clinica.Pages.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Clinic | Error</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500" rel="stylesheet">
    <link href="styles/common.css" rel="stylesheet" />
    <link href="styles/Error.css" rel="stylesheet" />
</head>
<body>
    <div class="content-main">
        <form id="form1" runat="server" class="content-box">
            <div class="content-header">
                <asp:Label ID="lblHeaderContent" runat="server"></asp:Label>
            </div>
            <div class="content-body">
                <asp:Label ID="lblBodyContent" runat="server"></asp:Label>
            <asp:Button ID="txtBack" runat="server" Text="Go back" OnClick="txtBack_Click" CssClass="btn btn-primary"/>
            </div>
        </form>
    </div>
</body>
</html>
