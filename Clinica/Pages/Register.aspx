<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Clinica.Pages.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Clinic | Register</title>
    <link href="styles/common.css" rel="stylesheet" />
    <link href="styles/Login.css" rel="stylesheet" />
    <link href="styles/Register.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500" rel="stylesheet">
</head>
<body>
    <div class="content-main">
        <form id="form1" runat="server" class="content-box">
            <div class="content-header">
                <asp:Label ID="Label1" runat="server" Text="Register new user"></asp:Label>
            </div>
            <div class="content-body">
                <div class="login-component">

                    <asp:Label ID="Label2" runat="server" Text="Login" CssClass="login-labels"></asp:Label>
                    <asp:TextBox ID="txtLogin" runat="server" CssClass="text-input"></asp:TextBox>

                </div>
                <div class="login-component">

                    <asp:Label ID="Label3" runat="server" Text="Password"  CssClass="login-labels"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="text-input" TextMode="Password"></asp:TextBox>

                </div>
                <div class="login-component">

                    <asp:Label ID="Label4" runat="server" Text="Repeat password"  CssClass="login-labels"></asp:Label>
                    <asp:TextBox ID="txtPasswordConf" runat="server" CssClass="text-input" TextMode="Password"></asp:TextBox>

                </div>
                <div class="login-component login-btns">

                    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click"/>
                    <asp:Button ID="btnCancel" runat="server" Text="cancel"  CssClass="btn-text-only" OnClick="btnCancel_Click"/>

                </div>
            </div>
        </form>
    </div>
</body>
</html>
