<%@ Page Title="" Language="C#" MasterPageFile="~/Vet/Vet.Master" AutoEventWireup="true" CodeBehind="Vet-RecordEdit.aspx.cs" Inherits="HisaniWebApplication.Vet.Vet_RecordEdit" %>
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

    <div class="vet-record-form-container">
        <h2>Edit Vet Record</h2>
        <form method="post" action="VetRecordEdit.aspx">
            <div class="form-group">
                <label for="partName">Part Name</label>
                <select id="partName" name="partName" required>
                    <option value="Hoof">Hoof</option>
                    <option value="Fetlock">Fetlock</option>
                    <option value="Pastern">Pastern</option>
                    <option value="Coronet">Coronet</option>
                    <option value="Heel">Heel</option>
                    <option value="Toe">Toe</option>
                </select>
            </div>
            <div class="form-group">
                <label for="vetName">Vet Name</label>
                <input type="text" id="vetName" name="vetName" value="Dr. Jane Smith" required />
            </div>
            <div class="form-group">
                <label for="date">Date</label>
                <input type="date" id="date" name="date" value="2024-11-05" required />
            </div>
            <div class="form-group">
                <label for="comments">Comments</label>
                <textarea id="comments" name="comments" rows="4">Routine checkup and trimming</textarea>
            </div>
            <div class="btn-container">
                <button type="submit">Save Changes</button>
            </div>
        </form>
    </div>
</asp:Content>
