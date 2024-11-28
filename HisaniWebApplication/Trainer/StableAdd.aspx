<%@ Page Title="Add Stable" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="StableAdd.aspx.cs" Inherits="HisaniWebApplication.Trainer.StableAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* General Styling */
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
        }

        .add-stable-container {
            max-width: 700px;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            background: #fff;
            box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.15);
            animation: fadeIn 0.5s ease-in-out;
        }

        .add-stable-container h2 {
            color: #333;
            font-size: 28px;
            text-align: center;
            margin-bottom: 20px;
            position: relative;
        }

        .add-stable-container h2::after {
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
            padding-right:20px;
        }

        .form-group label {
            display: block;
            font-size: 16px;
            color: #555;
            margin-bottom: 8px;
        }

        .form-group input[type="text"],
        .form-group input[type="number"] {
            width: 100%;
            padding: 12px;
            font-size: 16px;
            border-radius: 8px;
            border: 1px solid #ccc;
            box-shadow: inset 0 2px 5px rgba(0, 0, 0, 0.1);
            transition: border-color 0.3s, box-shadow 0.3s;
        }

        .form-group input[type="text"]:focus,
        .form-group input[type="number"]:focus {
            border-color: #00bcd4;
            box-shadow: 0 0 8px rgba(0, 188, 212, 0.4);
        }

        .btn-container {
            text-align: center;
            margin-top: 30px;
        }

        .btn-submit {
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

        .btn-submit:hover {
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
    <div class="add-stable-container">
        <h2>Add New Stable</h2>
        <div class="form-group">
            <label for="StableName">Stable Name:</label>
            <asp:TextBox ID="StableName" runat="server" CssClass="form-control" placeholder="Enter stable name" />
        </div>
        <div class="form-group">
            <label for="Location">Location:</label>
            <asp:TextBox ID="Location" runat="server" CssClass="form-control" placeholder="Enter location" />
        </div>
        <div class="form-group">
            <label for="Capacity">Capacity (No. of Horses):</label>
            <asp:TextBox ID="Capacity" runat="server" CssClass="form-control" 
                        TextMode="Number" placeholder="Enter capacity" oninput="validateCapacity(this)" />
        </div>

        <script>
            function validateCapacity(input) {
                if (input.value < 0) {
                    input.value = 0;  // Reset to 0 if the value is below 0
                }
            }
        </script>

        <div class="btn-container">
            <asp:Button 
                ID="btnAddStable" 
                runat="server" 
                CssClass="btn-submit" 
                Text="Add Stable" 
                OnClick="btnAddStable_Click" 
                OnClientClick="return validateForm();" 
            />
        </div>
    </div>

    <script>
        function validateForm() {
            let stableName = document.getElementById('<%= StableName.ClientID %>').value.trim();
        let location = document.getElementById('<%= Location.ClientID %>').value.trim();
        let capacity = document.getElementById('<%= Capacity.ClientID %>').value.trim();

        let errorMessage = '';
        let isValid = true;

        if (stableName.length < 3) {
            errorMessage = 'Stable name must be at least 3 characters long.';
            displayError('<%= StableName.ClientID %>', errorMessage);
            isValid = false;
        } else {
            clearError('<%= StableName.ClientID %>');
        }

        if (!location) {
            errorMessage = 'Location is required.';
            displayError('<%= Location.ClientID %>', errorMessage);
            isValid = false;
        } else {
            clearError('<%= Location.ClientID %>');
        }

        if (isNaN(capacity) || capacity <= 0) {
            errorMessage = 'Capacity must be a positive number.';
            displayError('<%= Capacity.ClientID %>', errorMessage);
            isValid = false;
        } else {
            clearError('<%= Capacity.ClientID %>');
            }

            return isValid;
        }

        function displayError(controlId, message) {
            let control = document.getElementById(controlId);
            let errorLabel = control.nextElementSibling;
            if (!errorLabel || !errorLabel.classList.contains('error-message')) {
                errorLabel = document.createElement('span');
                errorLabel.className = 'error-message';
                errorLabel.style.color = 'red';
                errorLabel.style.fontSize = '14px';
                control.parentNode.appendChild(errorLabel);
            }
            errorLabel.textContent = message;
        }

        function clearError(controlId) {
            let control = document.getElementById(controlId);
            let errorLabel = control.nextElementSibling;
            if (errorLabel && errorLabel.classList.contains('error-message')) {
                errorLabel.textContent = '';
            }
        }
    </script>

</asp:Content>
