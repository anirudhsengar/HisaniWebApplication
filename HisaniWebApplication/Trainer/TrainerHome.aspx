<%@ Page Title="" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="TrainerHome.aspx.cs" Inherits="HisaniWebApplication.Trainer.TrainerHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="homepage-container">
        <h1>Welcome, Trainer!</h1>
        <p>Manage your stable, horses, vet, and more all in one place.</p>

        <div class="stats-container">
            <div class="stat-card">
                <h2>Total Horses</h2>
                <p><asp:Label ID="lblTotalHorses" runat="server" Text="0"></asp:Label></p>
            </div>
            <div class="stat-card">
                <h2>Total Records</h2>
                <p><asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label></p>
            </div>
            <div class="stat-card">
                <h2>Today's Records</h2>
                <p><asp:Label ID="lblTodaysRecords" runat="server" Text="0"></asp:Label></p>
            </div>
        </div>
        
        <div class="actions-container">
            <h3>Quick Actions</h3>
            <div class="action-buttons">
                <a href="HorseAdd.aspx">Add Horse</a>
                <a href="VetRecordDisplay.aspx">View Records</a>
            </div>
        </div>
    </div>
</asp:Content>
