<%@ Page Title="" Language="C#" MasterPageFile="~/Vet/Vet.Master" AutoEventWireup="true" CodeBehind="VetDashboard.aspx.cs" Inherits="HisaniWebApplication.Vet.VetDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Main content styling */
        .content {
            padding: 20px;
        }

        /* Styles for Vet Home Page */
        .homepage-container {
            padding: 30px;
            color: #333;
            background-color: #f5f5f5;
            font-family: Arial, sans-serif;
        }

        .homepage-container h1 {
            color: black;
            font-size: 36px;
        }

        .homepage-container p {
            font-size: 18px;
            margin-bottom: 20px;
        }

        .stats-container {
            display: flex;
            justify-content: space-between;
            gap: 20px;
            margin-top: 30px;
        }

        .stat-card {
            background-color: white;
            border: 2px solid #C6BF38;
            border-radius: 8px;
            text-align: center;
            padding: 20px;
            width: 30%;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
        }

        .stat-card h2 {
            color: black;
            font-size: 24px;
            margin-bottom: 10px;
        }

        .stat-card p {
            font-size: 32px;
            color: #C6BF38;
        }

        .actions-container {
            margin-top: 40px;
        }

        .actions-container h3 {
            color: black;
            font-size: 28px;
        }

        .action-buttons {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            margin-top: 15px;
        }

        .action-buttons a {
            text-decoration: none;
            background-color: #C6BF38;
            color: white;
            padding: 10px 20px;
            border-radius: 5px;
            font-size: 16px;
            box-shadow: 0px 3px 6px rgba(0, 0, 0, 0.1);
            transition: background-color 0.3s ease;
        }

        .action-buttons a:hover {
            background-color: #aaa34f;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="homepage-container">
        <h1>Welcome, Vet!</h1>
        <p>Manage your assigned horses, view records, and more all in one place.</p>

        <div class="stats-container">
            <div class="stat-card">
                <h2>Assigned Horses</h2>
                <p><asp:Label ID="lblAssignedHorses" runat="server" Text="0"></asp:Label></p>
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
                <a href="VetHorseList.aspx">View Horses</a>
                <a href="VetRecordDisplay.aspx">View Records</a>
            </div>
        </div>
    </div>
</asp:Content>
