<%@ Page Title="Vet Details" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="VetDetails.aspx.cs" Inherits="HisaniWebApplication.Trainer.VetDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* General Styling */
        body {
            font-family: 'Roboto', sans-serif;
            margin: 0;
            padding: 0;
        }

        .vet-details-container {
            max-width: 800px;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            background: #fff;
            box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.2);
            animation: fadeIn 0.5s ease-in-out;
        }

        .vet-details-container h2 {
            font-size: 28px;
            color: #333;
            margin-bottom: 20px;
            border-bottom: 2px solid #c6bf38;
            padding-bottom: 10px;
            display: inline-block;
        }

        .button-container {
            text-align: right;
            margin-bottom: 20px;
        }

        .btn {
            padding: 10px 20px;
            font-size: 16px;
            border: none;
            border-radius: 25px;
            cursor: pointer;
            margin-left: 10px;
            transition: all 0.3s ease;
            font-weight: bold;
        }

        .edit-btn {
            background: grey;
            color: #fff;
        }

        .edit-btn:hover {
            transform: translateY(-3px);
            box-shadow: 0 5px 15px grey;
        }

        .delete-btn {
            background: linear-gradient(90deg, #f85032, #e73827);
            color: #fff;
        }

        .delete-btn:hover {
            transform: translateY(-3px);
            box-shadow: 0 5px 15px rgba(232, 56, 39, 0.5);
        }

        .vet-info {
            margin-top: 20px;
            line-height: 1.8;
        }

        .vet-info p {
            font-size: 18px;
            color: #555;
            background: rgba(198, 191, 56, 0.1);
            border-radius: 10px;
            padding: 10px 15px;
            margin-bottom: 10px;
        }

        .vet-info strong {
            color: #333;
        }

        .vet-info p:hover {
            background: rgba(198, 191, 56, 0.2);
            transition: background 0.3s ease;
        }

        /* Animation */
        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="vet-details-container">
        <h2>Vet Details</h2>

        <!-- Buttons: Edit and Delete -->
        <div class="button-container">
            <!-- Edit Button -->
            <asp:Button ID="btnEditVet" runat="server" CssClass="btn edit-btn" 
                        Text="Edit Vet" OnClick="btnEditVet_Click" />

            <!-- Delete Button -->
            <asp:Button ID="btnDeleteVet" runat="server" CssClass="btn delete-btn" 
                        Text="Delete Vet" OnClick="btnDeleteVet_Click" 
                        OnClientClick="return confirm('Are you sure you want to delete this vet?');" />
        </div>

        <!-- Vet Information -->
        <div class="vet-info">
            <p><strong>Vet Name:</strong> <asp:Label ID="lblVetName" runat="server"></asp:Label></p>
            <p><strong>Speciality:</strong> <asp:Label ID="lblVetSpeciality" runat="server"></asp:Label></p>
            <p><strong>Contact:</strong> <asp:Label ID="lblVetContact" runat="server"></asp:Label></p>
        </div>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
