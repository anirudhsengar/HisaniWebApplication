<%@ Page Title="Edit Horse" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="HorseEdit.aspx.cs" Inherits="HisaniWebApplication.Trainer.HorseEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* General Styling */
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
        }

        .edit-horse-container {
            max-width: 700px;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            background: #fff;
            box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.15);
            animation: fadeIn 0.5s ease-in-out;
        }

        .edit-horse-container h2 {
            color: #333;
            font-size: 28px;
            text-align: center;
            margin-bottom: 20px;
            position: relative;
        }

        .edit-horse-container h2::after {
            content: '';
            width: 200px;
            height: 3px;
            background: #C6BF38;
            display: block;
            margin: 10px auto 0;
            border-radius: 2px;
        }

        .form-group {
            margin-bottom: 20px;
            padding-right: 20px;
        }

        .form-group label {
            display: block;
            font-size: 16px;
            color: #555;
            margin-bottom: 8px;
        }

        .form-group input[type="text"], 
        .form-group input[type="number"], 
        .form-group input[type="date"], 
        .form-group select {
            width: 100%;
            padding: 12px;
            font-size: 16px;
            border-radius: 8px;
            border: 1px solid #ccc;
            box-shadow: inset 0 2px 5px rgba(0, 0, 0, 0.1);
            transition: border-color 0.3s, box-shadow 0.3s;
        }

        .form-group input[type="text"]:focus, 
        .form-group input[type="number"]:focus, 
        .form-group input[type="date"]:focus,
        .form-group select:focus {
            border-color: #00bcd4;
            box-shadow: 0 0 8px rgba(0, 188, 212, 0.4);
        }

        .btn-container {
            text-align: center;
            margin-top: 30px;
        }

        .btn-save {
            padding: 12px 25px;
            font-size: 16px;
            border-radius: 25px;
            background: #C6BF38;
            color: #fff;
            border: none;
            cursor: pointer;
            box-shadow: 0px 5px 15px #C6BF38;
            transition: all 0.3s ease;
        }

        .btn-save:hover {
            background: #C6BF38;
            box-shadow: 0px 10px 30px #C6BF38;
            transform: translateY(-3px);
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
    <div class="edit-horse-container">
        <h2>Edit Horse</h2>

        <div class="form-group">
            <label for="HorseName">Horse Name:</label>
            <asp:TextBox ID="HorseName" runat="server" CssClass="form-control" Required="true" />
            <asp:RequiredFieldValidator ID="rfvHorseName" runat="server" ControlToValidate="HorseName" 
                ErrorMessage="Horse Name is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <div class="form-group">
            <label for="HorseBreed">Breed:</label>
            <asp:TextBox ID="HorseBreed" runat="server" CssClass="form-control" Required="true" />
            <asp:RequiredFieldValidator ID="rfvHorseBreed" runat="server" ControlToValidate="HorseBreed" 
                ErrorMessage="Breed is required." ForeColor="Red" Display="Dynamic" />
        </div>

        <div class="form-group">
            <label for="HorseSex">Sex:</label>
            <asp:DropDownList ID="HorseSex" runat="server" CssClass="form-control" Required="true">
                <asp:ListItem Value="Male">Male</asp:ListItem>
                <asp:ListItem Value="Female">Female</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvHorseSex" runat="server" ControlToValidate="HorseSex" InitialValue="" 
                ErrorMessage="Please select the horse's sex." ForeColor="Red" Display="Dynamic" />
        </div>

        <div class="form-group">
            <label for="DateOfBirth">Date of Birth:</label>
            <asp:TextBox ID="DateOfBirth" runat="server" CssClass="form-control" TextMode="Date" Required="true" />
            <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="DateOfBirth" 
                ErrorMessage="Date of Birth is required." ForeColor="Red" Display="Dynamic" />
            <asp:CustomValidator ID="cvDateOfBirth" runat="server" ControlToValidate="DateOfBirth" 
                ErrorMessage="Date of Birth cannot be a future date." ForeColor="Red" OnServerValidate="ValidateDateOfBirth" />
        </div>

        <div class="btn-container">
            <asp:Button ID="btnSaveChanges" runat="server" CssClass="btn-save" Text="Save Changes" OnClick="btnSaveChanges_Click" />
        </div>
    </div>
</asp:Content>
