<%@ Page Title="" Language="C#" MasterPageFile="~/Vet/Vet.Master" AutoEventWireup="true" CodeBehind="Vet-HorseList.aspx.cs" Inherits="HisaniWebApplication.Vet.Vet_HorseList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .header-container {
            display: flex;
            align-items: center;
            justify-content: space-between;
            margin-bottom: 20px;
            padding-left:20vw;
            padding-right:20vw;
        }

        .horse-list-container {
            margin: 30px auto;
            display: flex;
            flex-direction: column;
            gap: 20px; /* Adds space between each tile */
            max-width: 800px; /* Limits the width of the list */
        }

        .horse-tile {
            padding: 15px;
            border-radius: 8px;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
            background-color: #fff;
            display: flex;
            justify-content: space-between;
            align-items: center;
            transition: transform 0.3s ease;
        }

        .horse-tile:hover {
            transform: scale(1.05);
        }

        .horse-tile h3 {
            font-size: 20px;
            color: #333;
            margin: 0;
            flex-grow: 1;
        }

        .view-details-btn, .add-horse-btn {
            padding: 8px 16px;
            font-size: 14px;
            background-color: #C6BF38;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
            transition: background-color 0.3s;
            border: none;
            cursor: pointer;
        }

        .view-details-btn:hover, .add-horse-btn:hover {
            background-color: #b3a332;
        }
    </style>

    <div class="header-container">
        <h1>Horse List</h1>
    </div>

    <div class="horse-list-container">
        <asp:Repeater ID="RepeaterHorseList" runat="server">
            <ItemTemplate>
                <div class="horse-tile">
                    <h3><%# Eval("HorseName") %></h3>
                    <a href='<%# "Vet-HorseDetails.aspx?HorseID=" + Eval("HorseID") %>' class="view-details-btn">View Details</a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

