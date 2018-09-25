<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Clinica.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Clinic | Login</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500" rel="stylesheet">
    <link href="styles/common.css" rel="stylesheet" />
    <link href="styles/Login.css" rel="stylesheet" />
</head>
<body>
    <div class="content-main">
        <form id="form1" runat="server" class="content-box">
            <div class="content-body">
                <div class="login-component">
                    <asp:Label ID="Label1" runat="server" Text="Login" CssClass="login-labels"></asp:Label>
                    <asp:TextBox ID="txtLogin" runat="server" CssClass="text-input login-input"></asp:TextBox>
                </div>
                <div class="login-component">
                    <asp:Label ID="Label2" runat="server" Text="Password" CssClass="login-labels"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="text-input login-input"></asp:TextBox>
                </div>
                <div class="login-component login-checkbox">
                    <asp:CheckBox ID="ckbSave" runat="server" Text="Save login?" />
                </div>
                <div class="login-component login-btns">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="btnRegister" runat="server" Text="Create an account?" CssClass="btn-text-only" OnClick="btnRegister_Click" />
                </div>
            </div>
        </form>
    </div>
</body>
</html>
