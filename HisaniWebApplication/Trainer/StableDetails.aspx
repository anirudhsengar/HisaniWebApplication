<%@ Page Title="Stable Details" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="StableDetails.aspx.cs" Inherits="HisaniWebApplication.Trainer.StableDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .stable-details-container {
            max-width: 800px;
            margin: 30px auto;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }

        .stable-details-container h2 {
            display: inline-block;
            margin: 0;
            color: #333;
            font-size: 24px;
        }

        .button-container {
            float: right;
            margin-top: -5px;
        }

        .btn {
            padding: 10px 20px;
            font-size: 16px;
            border-radius: 5px;
            border: none;
            cursor: pointer;
            margin-left: 10px;
            transition: background-color 0.3s;
        }

        .edit-btn {
            background-color: #d3d3d3;
            color: #333;
        }

        .edit-btn:hover {
            background-color: #b0b0b0;
        }

        .delete-btn {
            background-color: #ff4d4d;
            color: #fff;
        }

        .delete-btn:hover {
            background-color: #e60000;
        }

        .stable-info {
            margin-top: 20px;
        }

        .stable-info p {
            font-size: 18px;
            color: #555;
        }

    </style>
    <script type="text/javascript">
        // JavaScript function for delete confirmation
        function confirmDelete() {
            var confirmation = confirm("Are you sure you want to delete this stable?");
            if (confirmation) {
                window.location.href = 'StableDetails.aspx?delete=true';  // Redirect to delete the stable
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="stable-details-container">
        <h2>Stable Details</h2>
        <div class="button-container">
            <!-- Edit Button redirects to StableEdit.aspx -->
            <asp:Button ID="btnEditStable" runat="server" CssClass="btn edit-btn" 
            Text="Edit Stable" 
            OnClick="btnEditStable_Click"  />



            <!-- Delete Button triggers JavaScript confirmation dialog -->
            <asp:Button ID="btnDeleteStable" runat="server" CssClass="btn delete-btn" 
            Text="Delete Stable" 
            OnClick="btnDeleteStable_Click" 
            OnClientClick="return confirm('Are you sure you want to delete this stable?');" />

        </div>
        <div class="stable-info">
            <p><strong>Stable Name:</strong> <asp:Label ID="lblStableName" runat="server"></asp:Label></p>
            <p><strong>Location:</strong> <asp:Label ID="lblLocation" runat="server"></asp:Label></p>
            <p><strong>Capacity:</strong> <asp:Label ID="lblCapacity" runat="server"></asp:Label></p>
        </div>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    </div>

</asp:Content>
