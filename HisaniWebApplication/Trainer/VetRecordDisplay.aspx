<%@ Page Title="Vet Records" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="VetRecordDisplay.aspx.cs" Inherits="HisaniWebApplication.Trainer.VetRecordDisplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .vet-record-table-container {
            max-width: 800px;
            margin: 30px auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }
        .vet-record-table-container h2 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        .add-record-btn {
            padding: 10px 20px;
            background-color: #C6BF38;
            color: white;
            border: none;
            cursor: pointer;
            font-size: 16px;
            border-radius: 5px;
            text-decoration: none;
        }
        table {
            width: 100%;
            border-collapse: collapse;
        }
        th, td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
        th {
            background-color: #f2f2f2;
            color: #333;
            font-weight: bold;
        }
        .delete-btn {
            background-color: red;
            color: white;
            padding: 5px 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
    </style>

    <div class="vet-record-table-container">
        <h2>
            Vet Records
            <a href="VetRecordAdd.aspx" class="add-record-btn">Add Record</a> <!-- Add Record button -->
        </h2>
        
        <table>
            <thead>
                <tr>
                    <th>Part Name</th>
                    <th>Date</th>
                    <th>Comments</th>
                    <th>Actions</th> <!-- Added column for Delete button -->
                </tr>
            </thead>
            <tbody>
                <%-- Loop through records dynamically --%>
                <asp:Repeater ID="vetRecordRepeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("PartName") %></td>
                            <td><%# Eval("RecordDate", "{0:yyyy-MM-dd}") %></td>
                            <td><%# Eval("Comment") %></td>
                            <td>
                                <asp:Button 
                                    ID="btnDelete" 
                                    runat="server" 
                                    Text="Delete" 
                                    CssClass="delete-btn" 
                                    OnClick="btnDelete_Click" 
                                    CommandArgument='<%# Eval("RecordID") %>' 
                                    OnClientClick='<%# "return confirmDelete(" + Eval("RecordID") + ");" %>' />

                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <script type="text/javascript">
        function confirmDelete(recordId) {
            if (confirm('Are you sure you want to delete this record?')) {
                // Redirect to the same page with the deleteRecordId query parameter
                window.location.href = 'VetRecordDisplay.aspx?deleteRecordId=' + recordId;
                return true;
            }
            return false; // Prevent deletion if the user cancels
        }
    </script>

</asp:Content>


