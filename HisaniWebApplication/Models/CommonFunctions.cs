using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HisaniWebApplication.Models
{
    public class CommonFunctions
    {
        public class Commonfn
        {
            private SqlConnection con;

            public Commonfn()
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HisaniDB"].ConnectionString);
            }

            // Exposes the connection (if needed)
            public SqlConnection Connection => con;

            // Executes a parameterized query (INSERT, UPDATE, DELETE, etc.)
            public void ExecuteQuery(SqlCommand cmd)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }

            // Existing Fetch method (unchanged, works for non-parameterized queries)
            public DataTable Fetch(string query)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                con.Close();
                return dt;
            }

            // Overloaded Fetch method to handle parameterized queries
            public DataTable Fetch(SqlCommand cmd)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                con.Close();
                return dt;
            }
            public int FetchScalar(SqlCommand cmd)
            {
                int result = 0;

                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    cmd.Connection = con;
                    object scalarResult = cmd.ExecuteScalar();

                    if (scalarResult != null)
                    {
                        result = Convert.ToInt32(scalarResult);
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    throw new Exception("Error in FetchScalar: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }

                return result;
            }

        }


    }
}
