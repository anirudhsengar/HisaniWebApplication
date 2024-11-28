<%@ Page Title="Trainer Dashboard" Language="C#" MasterPageFile="~/Trainer/Trainer.Master" AutoEventWireup="true" CodeBehind="TrainerHome.aspx.cs" Inherits="HisaniWebApplication.Trainer.TrainerHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-adapter-date-fns"></script>
    <style>
        .homepage-container {
            padding: 30px;
            padding-top: 10px;
            color: #333;
            background-color: #f5f5f5;
            font-family: Arial, sans-serif;
        }

        .homepage-container h1 {
            color: black;
            font-size: 36px;
            margin-bottom: 20px;
        }

        .stats-grid {
            display: grid;
            grid-template-columns: repeat(2, 1fr); /* 2 columns layout */
            gap: 20px;
            margin-top: 30px;
        }

        .stat-card {
            background-color: white;
            border-radius: 8px;
            text-align: center;
            padding: 20px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            width: 95%;
            height: 300px; /* Consistent height */
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
        }

        .stat-card canvas {
            width: 100% !important;
            height: 100% !important;
            max-height: 250px;
        }

        @media (max-width: 768px) {
            .stats-grid {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="homepage-container">
        <h1>Welcome, Trainer!</h1>
        <p>Here's an overview of your stable and website statistics.</p>
        
        <h2>Stable Statistics</h2>
        <div class="stats-grid">
            <div class="stat-card">
                <h2>Horse Date of Birth</h2>
                <canvas id="horseDobChart"></canvas>
            </div>
            <div class="stat-card">
                <h2>Horse Gender Distribution</h2>
                <canvas id="horseGenderChart"></canvas>
            </div>
            <div class="stat-card">
                <h2>Vet Records Today</h2>
                <p style="font-size: 36px; color: #C6BF38;"><%= DailyRecords %></p>
            </div>
            <div class="stat-card">
                <h2>Current Horses</h2>
                <p style="font-size: 36px; color: #C6BF38;"><%= CurrentHorsesCount %></p>
            </div>
        </div>
        
        <h2>Website Statistics</h2>
        <div class="stats-grid">
            <div class="stat-card">
                <h2>Number of Users</h2>
                <p style="font-size: 36px; color: #C6BF38;"><%= TotalUsers %></p>
            </div>
            <div class="stat-card">
                <h2>User Distribution</h2>
                <canvas id="userDistributionChart"></canvas>
            </div>
            <div class="stat-card">
                <h2>Logins</h2>
                <p style="font-size: 36px; color: #C6BF38;"><%= TotalLogins %></p>
            </div>
            <div class="stat-card">
                <h2>Uploads</h2>
                <canvas id="recordsHorsesChart"></canvas>
            </div>

        </div>
    </div>

    <script>
        // Horse Date of Birth
        const horseDobData = {
            labels: <%= HorseNamesJson %>,
            datasets: [{
                label: 'Date of Birth',
                data: <%= HorseDobJson %>,
                backgroundColor: '#C6BF38',
            }]
        };
        new Chart(document.getElementById('horseDobChart'), {
            type: 'scatter',
            data: horseDobData,
            options: {
                responsive: true,
                scales: {
                    x: { type: 'time', title: { display: true, text: 'Date of Birth' } },
                    y: { title: { display: true, text: 'Horse Index' } },
                },
            },
        });

        // Horse Gender Distribution
        const horseGenderData = {
            labels: <%= HorseGenderLabelsJson %>,
            datasets: [{
                data: <%= HorseGenderDataJson %>,
                backgroundColor: ['#C6BF38', '#aaa34f']
            }]
        };
        new Chart(document.getElementById('horseGenderChart'), {
            type: 'pie',
            data: horseGenderData,
            options: { responsive: true }
        });

        // User Distribution
        const userDistributionData = {
            labels: ['Trainer', 'Vet'],
            datasets: [{
                data: <%= UserDistributionJson %>,
                backgroundColor: ['#C6BF38', '#aaa34f']
            }]
        };
        new Chart(document.getElementById('userDistributionChart'), {
            type: 'doughnut',
            data: userDistributionData,
            options: { responsive: true }
        });

        // Records and Horses Count
        const recordsHorsesData = {
            labels: ['Records', 'Horses'],
            datasets: [{
                label: 'Count',
                data: <%= UploadsJson %>,
                backgroundColor: ['#C6BF38', '#aaa34f']
            }]
        };
        new Chart(document.getElementById('recordsHorsesChart'), {
            type: 'bar',
            data: recordsHorsesData,
            options: {
                responsive: true,
                scales: {
                    y: { beginAtZero: true, title: { display: true, text: 'Count' } },
                    x: { title: { display: true, text: 'Category' } },
                },
            },
        });

    </script>
</asp:Content>
