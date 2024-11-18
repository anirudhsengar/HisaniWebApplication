﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="StableEdit.aspx.cs" Inherits="HisaniWebApplication.Trainer.StableEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .edit-stable-container {
            max-width: 600px;
            margin: 30px auto;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }

        .edit-stable-container h2 {
            color: #333;
            font-size: 24px;
            text-align: center;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 15px;
            padding-right:20px;
        }

        .form-group label {
            display: block;
            font-size: 16px;
            color: #555;
            margin-bottom: 5px;
        }

        .form-group input[type="text"],
        .form-group input[type="number"] {
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

    <div class="edit-stable-container">
        <div class="edit-stable-container">
        <h2>Edit Stable</h2>
        <div class="form-group">
            <label for="StableName">Stable Name:</label>
            <asp:TextBox ID="StableName" runat="server" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="Location">Location:</label>
            <asp:TextBox ID="Location" runat="server" CssClass="form-control" />
        </div>
    
        <div class="form-group">
            <label for="Capacity">Capacity (No. of Horses):</label>
            <asp:TextBox ID="Capacity" runat="server" CssClass="form-control" TextMode="Number" oninput="validateCapacity(this)" />
        </div>

        <script>
            function validateCapacity(input) {
                if (input.value < 0) {
                    input.value = 0;  // Reset to 0 if the value is below 0
                }
            }
        </script>

        <div class="btn-container">
            <asp:Button ID="btnEditStable" runat="server" CssClass="btn-save" Text="Update Stable" OnClick="btnEditStable_Click"/>
        </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
</asp:Content>