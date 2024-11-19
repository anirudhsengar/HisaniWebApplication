<%@ Page Title="Vet Details" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="VetDetails.aspx.cs" Inherits="HisaniWebApplication.Trainer.VetDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .vet-details-container {
            max-width: 600px;
            margin: 30px auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }
        .vet-details-container h2 {
            font-size: 24px;
            color: #333;
            text-align: center;
            margin-bottom: 20px;
        }
        .vet-details {
            margin-bottom: 15px;
            font-size: 18px;
            color: #555;
        }
        .vet-details strong {
            font-weight: bold;
        }
        .btn-container {
            display: flex;
            justify-content: space-between;
            margin-top: 20px;
        }
        .btn-container a {
            padding: 10px 20px;
            border-radius: 5px;
            color: white;
            text-decoration: none;
        }
        .edit-btn {
            background-color: lightgray;
        }
        .delete-btn {
            background-color: red;
        }
        .btn-container a:hover {
            opacity: 0.8;
        }
    </style>

    <div class="vet-details-container">
        <h2>Vet Details</h2>
        <div class="vet-details">
            <strong>Vet Name:</strong> <asp:Label ID="VetName" runat="server" Text="N/A" />
        </div>
        <div class="vet-details">
            <strong>Speciality:</strong> <asp:Label ID="VetSpeciality" runat="server" Text="N/A" />
        </div>
        <div class="vet-details">
            <strong>Contact:</strong> <asp:Label ID="VetContact" runat="server" Text="N/A" />
        </div>

        <div class="btn-container">
            <a href="VetEdit.aspx" class="edit-btn">Edit</a>
            <a href="javascript:void(0);" class="delete-btn" onclick="deleteVet()">Delete</a>
        </div>
    </div>

    <script>
        function deleteVet() {
            if (confirm('Are you sure you want to delete this vet?')) {
                window.location.href = "VetDetails.aspx?delete=true";
            }
        }
    </script>
</asp:Content>
