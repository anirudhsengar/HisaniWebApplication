<%@ Page Title="" Language="C#" MasterPageFile="~/Vet/Vet.Master" AutoEventWireup="true" CodeBehind="Vet-HorseList.aspx.cs" Inherits="HisaniWebApplication.Vet.Vet_HorseList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* General Styling */
        body {
            font-family: 'Poppins', sans-serif;
            background-color: #f9f9f9;
            margin: 0;
            padding: 0;
        }

        /* Header Styling */
        .header-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 20px;
            background-color: #fff;
            border-bottom: 2px solid #ddd;
            box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.1);
        }

        .header-container h1 {
            font-size: 24px;
            color: #333;
            margin: 0;
        }

        .add-horse-btn {
            padding: 10px 20px;
            font-size: 14px;
            background-color: #C6BF38;
            color: #fff;
            border-radius: 5px;
            border: none;
            cursor: pointer;
            transition: background-color 0.3s ease, box-shadow 0.3s ease;
        }

        .add-horse-btn:hover {
            background-color: #b3a332;
            box-shadow: 0 4px 10px rgba(198, 191, 56, 0.3);
        }

        /* Horse List Container */
        .horse-list-container {
            max-width: 1000px;
            margin: 40px auto;
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
            gap: 20px;
            padding: 0 20px;
        }

        /* Horse Tile Styling */
        .horse-tile {
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
            overflow: hidden;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

        .horse-tile:hover {
            transform: translateY(-5px);
            box-shadow: 0px 6px 20px rgba(0, 0, 0, 0.15);
        }

        .horse-tile h3 {
            font-size: 18px;
            color: #333;
            margin: 0;
            padding: 15px;
            background-color: #f7f7f7;
            border-bottom: 2px solid #ddd;
        }

        .tile-actions {
            padding: 15px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .view-details-btn {
            padding: 8px 16px;
            font-size: 14px;
            background-color: #C6BF38;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
            border: none;
            transition: background-color 0.3s ease, box-shadow 0.3s ease;
        }

        .view-details-btn:hover {
            background-color: #b3a332;
            box-shadow: 0px 4px 10px rgba(198, 191, 56, 0.3);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Header Section -->
    <div class="header-container">
        <h1>Horse List</h1>
    </div>

    <!-- Horse List Section -->
    <div class="horse-list-container">
        <asp:Repeater ID="RepeaterHorseList" runat="server">
            <ItemTemplate>
                <div class="horse-tile">
                    <h3><%# Eval("HorseName") %></h3>
                    <div class="tile-actions">
                        <asp:Literal
                            EnableViewState="false" />
                        <a href='<%# "Vet-HorseDetails.aspx?HorseID=" + Eval("HorseID") %>' class="view-details-btn">View Details</a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
