<%@ Page Title="" Language="C#" MasterPageFile="~/Vet/Vet.Master" AutoEventWireup="true" CodeBehind="Vet-RecordAdd.aspx.cs" Inherits="HisaniWebApplication.Vet.Vet_RecordAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .vet-record-form-container {
            max-width: 600px;
            margin: 30px auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }
        .vet-record-form-container h2 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }
        .form-group {
            margin-bottom: 15px;
            padding-right:20px;
        }
        .form-group label {
            font-size: 16px;
            color: #555;
        }
        .form-group input, .form-group select, .form-group textarea {
            width: 100%;
            padding: 10px;
            font-size: 16px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }
        .form-group textarea {
            resize: vertical;
        }
        .btn-container {
            text-align: center;
            margin-top: 20px;
        }
        .btn-container input[type="submit"] {
            padding: 10px 20px;
            font-size: 16px;
            background-color: #C6BF38;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn-container input[type="submit"]:hover {
            background-color: #b3a332;
        }

    </style>

    <div class="vet-record-form-container">
        <h2>Add Vet Record</h2>
        <form method="post">
            <div class="form-group">
                <label for="partName">Part Name</label>
                <asp:DropDownList ID="partName" runat="server" required>
                    <asp:ListItem Value="Hoof">Hoof</asp:ListItem>
                    <asp:ListItem Value="Fetlock">Fetlock</asp:ListItem>
                    <asp:ListItem Value="Pastern">Pastern</asp:ListItem>
                    <asp:ListItem Value="Coronet">Coronet</asp:ListItem>
                    <asp:ListItem Value="Heel">Heel</asp:ListItem>
                    <asp:ListItem Value="Toe">Toe</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="date">Date</label>
                <asp:TextBox ID="date" runat="server" TextMode="Date" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="comments">Comments</label>
                <asp:TextBox ID="comments" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>
            <div class="btn-container">
                <asp:Button ID="btnAddRecord" runat="server" Text="Add Record" OnClick="btnAddRecord_Click" />
            </div>
        </form>
    </div>
</asp:Content>
