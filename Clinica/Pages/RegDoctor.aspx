<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Template.Master" AutoEventWireup="true" CodeBehind="RegDoctor.aspx.cs" Inherits="Clinica.Pages.RegDoctor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="styles/Doctor.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Doctor"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="template-component">
        <asp:Label ID="Label2" runat="server" Text="Id" CssClass="template-label"></asp:Label>
        <asp:TextBox ID="txtId" runat="server" CssClass="text-input" TextMode="Number"></asp:TextBox>
    </div>
    <div class="template-component">
        <asp:Label ID="Label3" runat="server" Text="Name" CssClass="template-label"></asp:Label>
        <asp:TextBox ID="txtName" runat="server" CssClass="text-input"></asp:TextBox>
    </div>
    <div class="template-component">
        <asp:Label ID="Label4" runat="server" Text="Date of birth" CssClass="template-label"></asp:Label>
        <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="text-input" TextMode="Date" ></asp:TextBox>
    </div>
    <div class="template-component">
        <asp:Label ID="Label5" runat="server" Text="Gender" CssClass="template-label"></asp:Label>
        <div class="template-radiobtn">
            <asp:RadioButton ID="rbtMale" runat="server" Text="Male" GroupName="genderGrp"/>
            <asp:RadioButton ID="rbtFemale" runat="server" Text="Female" GroupName="genderGrp" />
        </div>
    </div>
    <div class="template-component">
        <asp:Label ID="Label6" runat="server" Text="Telephone" CssClass="template-label"></asp:Label>
        <asp:TextBox ID="txtTelephone" runat="server" CssClass="text-input"></asp:TextBox>
    </div>
    <div class="template-component">
        <asp:Label ID="Label7" runat="server" Text="Zipcode" CssClass="template-label"></asp:Label>
        <asp:TextBox ID="txtZipcode" runat="server" CssClass="text-input"></asp:TextBox>
    </div>
    <div class="template-component">
        <asp:Label ID="Label8" runat="server" Text="Specialty" CssClass="template-label"></asp:Label>
        <asp:TextBox ID="txtSpecialty" runat="server" CssClass="text-input"></asp:TextBox>
    </div>
    <div class="template-component">
        <asp:Label ID="Label9" runat="server" Text="Salary" CssClass="template-label"></asp:Label>
        <asp:TextBox ID="txtSalary" runat="server" CssClass="text-input"></asp:TextBox>
    </div>
    <div class="template-component template-btn">
        <asp:Button ID="txtRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="txtRegister_Click"/>
        <asp:Button ID="txtUpdate" runat="server" Text="Update" CssClass="btn btn-secondary" OnClick="txtUpdate_Click"/>
        <asp:Button ID="txtDelete" runat="server" Text="Delete" CssClass="btn btn-secondary" OnClick="txtDelete_Click"/>
        <asp:Button ID="txtLoad" runat="server" Text="Load" CssClass="btn btn-secondary" OnClick="txtLoad_Click"/>
    </div>
</asp:Content>
