<%@ Page Title="Create Account" Language="C#" MasterPageFile="~/Authentication/Authentication.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="HisaniWebApplication.Authentication.CreateAccount" %>

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
        .login-box input[type="password"],
        .login-box select {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 14px;
            box-sizing: border-box;
        }

        .btn {
            width: 100%;
            background-color: #C6BF38;
            color: white;
            padding: 12px;
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

        .options{
            padding-bottom:20px;
        }

        /* Responsive Design */
        @media (max-width: 480px) {
            .login-box {
                width: 90%;
                padding: 20px;
            }

            .login-box img {
                max-width: 120px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <div class="login-box">
            <!-- Hisani Logo -->
            <img src="<%= ResolveUrl("~/Images/hisani-logo.png") %>" alt="Hisani Logo" />

            <!-- Welcome Text -->
            <h1>Create Your Account</h1>

            <!-- Registration Form -->
            <asp:Label runat="server" ID="lblMessage" ForeColor="Red" CssClass="message-label"></asp:Label>
            <form id="createAccountForm" onsubmit="return validateForm()">
                <label for="txtEmail">Email</label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Enter your email" required></asp:TextBox>

                <label for="txtPassword">Password</label>
                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="Enter your password" required></asp:TextBox>

                <label for="txtConfirmPassword">Confirm Password</label>
                <asp:TextBox runat="server" ID="txtConfirmPassword" CssClass="form-control" TextMode="Password" placeholder="Confirm your password" required></asp:TextBox>

                <label for="ddlUserType">Type of User</label>
                <asp:DropDownList runat="server" ID="ddlUserType" CssClass="form-control" required>
                    <asp:ListItem Text="-- Select User Type --" Value="" Enabled="false" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Trainer" Value="Trainer"></asp:ListItem>
                    <asp:ListItem Text="Vet" Value="Vet"></asp:ListItem>
                </asp:DropDownList>

                <div class="options">
                    Already have an account?
                    <a href="Login.aspx">Login</a>
                </div>

                <asp:Button runat="server" ID="btnCreateAccount" Text="Create Account" CssClass="btn" OnClick="btnCreateAccount_Click" />
            </form>
        </div>
    </div>

    <script>
        function validateForm() {
            const email = document.getElementById('<%= txtEmail.ClientID %>').value.trim();
            const password = document.getElementById('<%= txtPassword.ClientID %>').value.trim();
            const confirmPassword = document.getElementById('<%= txtConfirmPassword.ClientID %>').value.trim();
            const userType = document.getElementById('<%= ddlUserType.ClientID %>').value;

            const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (!email || !password || !confirmPassword || !userType) {
                alert("All fields are required.");
                return false;
            }

            if (!emailPattern.test(email)) {
                alert("Please enter a valid email address.");
                return false;
            }

            if (password.length < 8) {
                alert("Password must be at least 8 characters long.");
                return false;
            }

            if (!/[A-Z]/.test(password) || !/[a-z]/.test(password) || !/\d/.test(password)) {
                alert("Password must contain at least one uppercase letter, one lowercase letter, and one number.");
                return false;
            }

            if (password !== confirmPassword) {
                alert("Passwords do not match.");
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
