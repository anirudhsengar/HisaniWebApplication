<%@ Page Title="Add Vet" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="VetAdd.aspx.cs" Inherits="HisaniWebApplication.Trainer.VetAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* General Styling */
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
        }

        .add-vet-container {
            max-width: 700px;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            background: #fff;
            box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.15);
            animation: fadeIn 0.5s ease-in-out;
        }

        .add-vet-container h2 {
            color: #333;
            font-size: 28px;
            text-align: center;
            margin-bottom: 20px;
            position: relative;
        }

        .add-vet-container h2::after {
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
        .form-group input[type="email"],
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
        .form-group input[type="email"]:focus,
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
    <div class="add-vet-container">
        <h2>Add New Vet</h2>
        <form method="post" action="VetAdd.aspx">
            <div class="form-group">
                <label for="vetEmail">Vet Email:</label>
                <asp:TextBox ID="vetEmail" runat="server" CssClass="form-control" placeholder="Enter vet email" type="email" />
            </div>
            <div class="form-group">
                <label for="vetName">Vet Name:</label>
                <asp:TextBox ID="vetName" runat="server" CssClass="form-control" placeholder="Enter vet name" />
            </div>
            <div class="form-group">
                <label for="vetSpeciality">Specialty:</label>
                <asp:TextBox ID="vetSpeciality" runat="server" CssClass="form-control" placeholder="Enter vet specialty" />
            </div>
            <div class="form-group">
                <label for="vetContact">Contact:</label>
                <asp:TextBox ID="vetContact" runat="server" CssClass="form-control" placeholder="Enter contact number" />
            </div>

            <div class="btn-container">
                <asp:Button 
                    ID="btnAddVet" 
                    runat="server" 
                    CssClass="btn-submit" 
                    Text="Add Vet" 
                    OnClick="btnAddVet_Click" 
                    OnClientClick="return validateForm();" 
                />
            </div>
        </form>
    </div>

    <script>
        function validateForm() {
            let vetEmail = document.getElementById('<%= vetEmail.ClientID %>').value.trim();
            let vetName = document.getElementById('<%= vetName.ClientID %>').value.trim();
            let vetSpeciality = document.getElementById('<%= vetSpeciality.ClientID %>').value.trim();
            let vetContact = document.getElementById('<%= vetContact.ClientID %>').value.trim();

            let errorMessage = '';
            let isValid = true;

            if (vetEmail.length === 0) {
                errorMessage = 'Vet email is required.';
                displayError('<%= vetEmail.ClientID %>', errorMessage);
                isValid = false;
            } else {
                clearError('<%= vetEmail.ClientID %>');
            }

            if (vetName.length < 3) {
                errorMessage = 'Vet name must be at least 3 characters long.';
                displayError('<%= vetName.ClientID %>', errorMessage);
                isValid = false;
            } else {
                clearError('<%= vetName.ClientID %>');
            }

            if (vetSpeciality.length < 3) {
                errorMessage = 'Specialty must be at least 3 characters long.';
                displayError('<%= vetSpeciality.ClientID %>', errorMessage);
                isValid = false;
            } else {
                clearError('<%= vetSpeciality.ClientID %>');
            }

            if (vetContact.length === 0 || isNaN(vetContact)) {
                errorMessage = 'Valid contact is required.';
                displayError('<%= vetContact.ClientID %>', errorMessage);
                isValid = false;
            } else {
                        clearError('<%= vetContact.ClientID %>');
            }

            // Return false if validation fails, preventing form submission
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