using DevConnectorBackEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectorBackEnd.Models
{
    public static class ExpModel
    {
        private static string connectionString = "Data Source=GUILHERME\\SQLEXPRESS;Initial Catalog=ProjetoDevConnectorBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public static List<ExpViewModel> ListarExp(string id)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Experience WHERE UserId = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                List<ExpViewModel> exps = new List<ExpViewModel>();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ExpViewModel exp = new ExpViewModel
                    {
                        Id = (int)reader["Id"],
                        Company = reader["Company"].ToString(),
                        CurrentJob = (bool)reader["CurrentJob"],
                        FromDate = reader["FromDate"].ToString(),
                        ToDate = reader["ToDate"].ToString(),
                        JobDescription = reader["JobDescription"].ToString(),
                        JobTitle = reader["JobTitle"].ToString(),
                        Location = reader["Location"].ToString()
                    };
                    exps.Add(exp);
                }
                return exps;
            }
        }

        public static int AddExp(ExpViewModel exp)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Experience(Company, CurrentJob, FromDate, ToDate, JobDescription, JobTitle, Location, UserId) 
                            VALUES (@Company, @CurrentJob, @FromDate, @ToDate, @JobDescription, @JobTitle, @Location, @UserId)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Company", exp.Company);
                cmd.Parameters.AddWithValue("@CurrentJob", exp.CurrentJob);
                cmd.Parameters.AddWithValue("@FromDate", exp.FromDate);
                cmd.Parameters.AddWithValue("@ToDate", exp.ToDate);
                cmd.Parameters.AddWithValue("@JobDescription", exp.JobDescription);
                cmd.Parameters.AddWithValue("@JobTitle", exp.JobTitle);
                cmd.Parameters.AddWithValue("@Location", exp.Location);
                cmd.Parameters.AddWithValue("@UserId", exp.UserId);

                int ret = cmd.ExecuteNonQuery();

                return ret;
            }
        }

        public static int DeleteExp(int id)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = $"DELETE FROM Experience WHERE Id = @id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                int ret = cmd.ExecuteNonQuery();

                return ret;
            }
        }


    }
}
