﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static HisaniWebApplication.Models.CommonFunctions;

namespace HisaniWebApplication.Vet
{
    public partial class Vet_HorseDetails : System.Web.UI.Page
    {
        Commonfn fn = new Commonfn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Step 1: Get HorseID from query string
                string horseID = Request.QueryString["HorseID"];
                if (string.IsNullOrEmpty(horseID))
                {
                    Response.Write("Error: HorseID not provided.");
                    return;
                }

                // Step 2: Query the database for the horse details
                try
                {
                    string query = "SELECT HorseName, HorseBreed, Sex, DateOfBirth FROM Horse WHERE HorseID = @HorseID";
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@HorseID", horseID);

                    DataTable horseDetails = fn.Fetch(cmd);

                    if (horseDetails.Rows.Count > 0)
                    {
                        // Step 3: Populate the details on the page
                        lblHorseName.Text = horseDetails.Rows[0]["HorseName"].ToString();
                        lblHorseBreed.Text = horseDetails.Rows[0]["HorseBreed"].ToString();
                        lblHorseSex.Text = horseDetails.Rows[0]["Sex"].ToString();
                        lblHorseDOB.Text = Convert.ToDateTime(horseDetails.Rows[0]["DateOfBirth"]).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        Response.Write("Error: Horse not found.");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Error loading horse details: " + ex.Message);
                }
            }
        }
    }
}