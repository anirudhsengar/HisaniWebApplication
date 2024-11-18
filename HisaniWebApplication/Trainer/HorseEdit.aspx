<%@ Page Title="Edit Horse" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="HorseEdit.aspx.cs" Inherits="HisaniWebApplication.Trainer.HorseEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .edit-horse-container {
            max-width: 600px;
            margin: 30px auto;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }
        .edit-horse-container h2 {
            color: #333;
            font-size: 24px;
            text-align: center;
            margin-bottom: 20px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
            font-size: 16px;
            color: #555;
            margin-bottom: 5px;
        }
        .form-group input[type="text"], .form-group input[type="number"], .form-group select {
            width: 100%;
            padding: 10px;
            font-size: 16px;
            border-radius: 5px;
            border: 1px solid #ccc;
        }
        .btn-container {
            text-align: center;
            margin-top: 20px;
        }
        .btn-save {
            padding: 10px 20px;
            font-size: 16px;
            border-radius: 5px;
            background-color: #C6BF38;
            color: #fff;
            border: none;
            cursor: pointer;
            transition: background-color 0.3s;
        }
        .btn-save:hover {
            background-color: #b3a332;
        }
    </style>

    <div class="edit-horse-container">
        <h2>Edit Horse</h2>
        <div class="form-group">
            <label for="HorseName">Horse Name:</label>
            <asp:TextBox ID="HorseName" runat="server" CssClass="form-control" Required="true" />
        </div>
        <div class="form-group">
            <label for="HorseBreed">Breed:</label>
            <asp:TextBox ID="HorseBreed" runat="server" CssClass="form-control" Required="true" />
        </div>
        <div class="form-group">
            <label for="HorseSex">Sex:</label>
            <asp:DropDownList ID="HorseSex" runat="server" CssClass="form-control" Required="true">
                <asp:ListItem Value="Male">Male</asp:ListItem>
                <asp:ListItem Value="Female">Female</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="DateOfBirth">Date of Birth:</label>
            <asp:TextBox ID="DateOfBirth" runat="server" CssClass="form-control" TextMode="Date" Required="true" />
        </div>
        <div class="btn-container">
            <asp:Button ID="btnSaveChanges" runat="server" CssClass="btn-save" Text="Save Changes" OnClick="btnSaveChanges_Click" />
        </div>
    </div>
</asp:Content>
