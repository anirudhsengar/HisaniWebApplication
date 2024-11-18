<%@ Page Title="Horse Details" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="HorseDetails.aspx.cs" Inherits="HisaniWebApplication.Trainer.HorseDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .horse-details-container {
            max-width: 700px;
            margin: 40px auto;
            padding: 25px;
            border-radius: 12px;
            box-shadow: 0px 6px 15px rgba(0, 0, 0, 0.15);
            background-color: #f9f9f9;
            font-family: Arial, sans-serif;
        }
        .horse-details-container h2 {
            color: #444;
            font-size: 26px;
            text-align: center;
            margin-bottom: 25px;
            position: relative;
        }
        .details-group {
            margin-bottom: 20px;
        }
        .details-group label {
            display: block;
            font-size: 18px;
            color: #666;
            font-weight: bold;
            margin-bottom: 8px;
        }
        .details-group p {
            font-size: 16px;
            color: #333;
            padding: 10px;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 6px;
        }
        .btn-container {
            display: flex;
            justify-content: flex-end;
            gap: 15px;
        }
        .btn-edit, .btn-delete {
            padding: 12px 25px;
            font-size: 16px;
            border-radius: 6px;
            border: none;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        .btn-delete {
            background-color: #e74c3c;
            color: white;
        }
        .btn-delete:hover {
            background-color: #c0392b;
        }
        .confirmation-message {
            color: #e74c3c;
            font-size: 14px;
            text-align: center;
            margin-top: 20px;
        }
    </style>

    <div class="horse-details-container">
        <h2>Horse Details</h2>
        <div class="details-group">
            <label>Horse Name:</label>
            <p><asp:Label ID="lblHorseName" runat="server" Text=""></asp:Label></p>
        </div>
        <div class="details-group">
            <label>Horse Breed:</label>
            <p><asp:Label ID="lblHorseBreed" runat="server" Text=""></asp:Label></p>
        </div>
        <div class="details-group">
            <label>Sex:</label>
            <p><asp:Label ID="lblHorseSex" runat="server" Text=""></asp:Label></p>
        </div>
        <div class="details-group">
            <label>Date of Birth:</label>
            <p><asp:Label ID="lblHorseDOB" runat="server" Text=""></asp:Label></p>
        </div>
        <div class="btn-container">
            <!-- Delete Horse Button -->
            <asp:Button ID="btnDeleteHorse" runat="server" CssClass="btn-delete"
                OnClick="btnDeleteHorse_ServerClick"
                OnClientClick="return confirm('Are you sure you want to delete this horse?');" Text="Delete Horse" />
        </div>
        <asp:Label ID="lblConfirmationMessage" runat="server" CssClass="confirmation-message" Visible="false"></asp:Label>




    </div>

    

</asp:Content>
