<%@ Page Title="Edit Vet" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="VetEdit.aspx.cs" Inherits="HisaniWebApplication.Trainer.VetEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .vet-form-container {
            max-width: 600px;
            margin: 30px auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }
        .vet-form-container h2 {
            font-size: 24px;
            color: #333;
            text-align: center;
            margin-bottom: 20px;
        }
        .form-group {
            margin-bottom: 15px;
            padding-right: 20px;
        }
        .form-group label {
            font-size: 16px;
            color: #555;
        }
        .form-group input {
            width: 100%;
            padding: 10px;
            font-size: 16px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }
        .form-group input:focus {
            border-color: #C6BF38;
        }
        .btn-container {
            text-align: center;
            margin-top: 20px;
        }
        .btn-container button {
            padding: 10px 20px;
            font-size: 16px;
            background-color: #C6BF38;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .btn-container button:hover {
            background-color: #b3a332;
        }
    </style>

    <div class="vet-form-container">
        <h2>Edit Vet</h2>
        <form method="post" action="VetEdit.aspx">
            <div class="form-group">
                <label for="vetName">Vet Name</label>
                <asp:TextBox ID="VetName" runat="server" Text="<%= VetName %>" required />
            </div>
            <div class="form-group">
                <label for="vetSpecialty">Specialty</label>
                <asp:TextBox ID="VetSpeciality" runat="server" Text="<%= VetSpecialty %>" required />
            </div>
            <div class="form-group">
                <label for="vetContact">Contact</label>
                <asp:TextBox ID="VetContact" runat="server" Text="<%= VetContact %>" required />
            </div>

            <div class="btn-container">
                <button type="submit" runat="server" onserverclick="SaveChanges">Save Changes</button>
            </div>
        </form>
    </div>
</asp:Content>
