<%@ Page Title="" Language="C#" MasterPageFile="~/Vet/Vet.Master" AutoEventWireup="true" CodeBehind="Vet-HorseDetails.aspx.cs" Inherits="HisaniWebApplication.Vet.Vet_HorseDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* General Styling */
        body {
            font-family: 'Roboto', sans-serif;
            margin: 0;
            padding: 0;
        }

        .horse-details-container {
            max-width: 800px;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            background: #fff;
            box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.2);
            animation: fadeIn 0.5s ease-in-out;
        }

        .horse-details-container h2 {
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

        .horse-info {
            margin-top: 20px;
            line-height: 1.8;
        }

        .horse-info p {
            font-size: 18px;
            color: #555;
            background: rgba(198, 191, 56, 0.1);
            border-radius: 10px;
            padding: 10px 15px;
            margin-bottom: 10px;
        }

        .horse-info strong {
            color: #333;
        }

        .horse-info p:hover {
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
    <div class="horse-details-container">
        <h2>Horse Details</h2>

        <div class="horse-info">
            <p><strong>Horse Name:</strong> <asp:Label ID="lblHorseName" runat="server"></asp:Label></p>
            <p><strong>Horse Breed:</strong> <asp:Label ID="lblHorseBreed" runat="server"></asp:Label></p>
            <p><strong>Sex:</strong> <asp:Label ID="lblHorseSex" runat="server"></asp:Label></p>
            <p><strong>Date of Birth:</strong> <asp:Label ID="lblHorseDOB" runat="server"></asp:Label></p>
        </div>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>