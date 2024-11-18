<%@ Page Title="Login" Language="C#" MasterPageFile="~/Authentication/Authentication.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HisaniWebApplication.Authentication.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100%;
        }

        .login-box {
            background-color: rgba(255, 255, 255, 0.9);
            border-radius: 12px;
            box-shadow: 0px 4px 15px rgba(0, 0, 0, 0.2);
            width: 400px;
            padding: 20px;

            text-align: center;
        }

        .login-box img {
            max-width: 200px;
        }

        .login-box h1 {
            color: #333;
            font-size: 24px;
            margin-bottom: 20px;
        }

        .login-box label {
            font-weight: bold;
            display: block;
            text-align: left;
            margin-bottom: 5px;
            color: #555;
        }

        .login-box input[type="text"],
        .login-box input[type="password"] {
            width: 95%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 14px;
        }

        .login-box a {
            display: inline-block;
            margin-top: 5px;
            font-size: 14px;
            color: #007bff;
            text-decoration: none;
            font-size:16px;
        }

        .login-box a:hover {
            text-decoration: underline;
        }

        .btn {
            width: 100%;
            background-color: #C6BF38;
            color: white;
            padding: 10px 10px;
            border: none;
            border-radius: 6px;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn:hover {
            background-color: #aaa34f;
        }

        .login-box .options {
            margin-top: 20px;
        }
        .options{
            padding-bottom:20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <div class="login-box">
            <!-- Hisani Logo -->
            <img src="<%= ResolveUrl("~/Images/hisani-logo.png") %>" alt="Hisani Logo" />

            <!-- Welcome Text -->
            <h1>Welcome to Hisani</h1>

            <!-- Login Form -->
            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
            <label for="txtUsername">Email</label>
            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Enter your email"></asp:TextBox>

            <label for="txtPassword">Password</label>
            <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="Enter your password"></asp:TextBox>

            <div class="options">
                Don't have an account?
                <a href="CreateAccount.aspx">Create Account</a>
            </div>

            <asp:Button runat="server" ID="btnLogin" Text="Login" CssClass="btn" OnClick="btnLogin_Click"/>
        </div>
    </div>
</asp:Content>

