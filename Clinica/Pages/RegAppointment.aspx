<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Template.Master" AutoEventWireup="true" CodeBehind="RegAppointment.aspx.cs" Inherits="Clinica.Pages.RegAppointment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="styles/Appointment.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="template-component">
        <asp:Label ID="Label2" runat="server" Text="Appointment Id" CssClass="template-label"></asp:Label>
        <asp:TextBox ID="txtId" runat="server" CssClass="text-input" TextMode="Number"></asp:TextBox>
    </div>
    <div class="template-component">
        <asp:Label ID="Label3" runat="server" Text="Date" CssClass="template-label"></asp:Label>
        <asp:TextBox ID="txtDate" runat="server" CssClass="text-input" TextMode="Date"></asp:TextBox>
    </div>
    <div class="template-component">
        <asp:Label ID="Label4" runat="server" Text="diagnosis" CssClass="template-label"></asp:Label>
        <asp:TextBox ID="txtDiagnosis" runat="server" CssClass="text-input text-multi" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="template-component">
        <asp:Label ID="Label6" runat="server" Text="Prescription" CssClass="template-label"></asp:Label>
        <asp:TextBox ID="txtPrescription" runat="server" CssClass="text-input text-multi" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="template-component">
        <asp:Label ID="Label7" runat="server" Text="Patient" CssClass="template-label"></asp:Label>
        <asp:DropDownList ID="dplPatient" runat="server" CssClass="text-input dropdownlist"></asp:DropDownList>
    </div>
    <div class="template-component">
        <asp:Label ID="Label8" runat="server" Text="Doctor" CssClass="template-label"></asp:Label>
        <asp:DropDownList ID="dplDoctor" runat="server" CssClass="text-input dropdownlist"></asp:DropDownList>
    </div>
    <div class="template-component template-btn">
        <asp:Button ID="txtRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="txtRegister_Click"/>
        <asp:Button ID="txtUpdate" runat="server" Text="Update" CssClass="btn btn-secondary" OnClick="txtUpdate_Click"/>
        <asp:Button ID="txtDelete" runat="server" Text="Delete" CssClass="btn btn-secondary" OnClick="txtDelete_Click"/>
        <asp:Button ID="txtLoad" runat="server" Text="Load" CssClass="btn btn-secondary" OnClick="txtLoad_Click"/>
    </div>
</asp:Content>
