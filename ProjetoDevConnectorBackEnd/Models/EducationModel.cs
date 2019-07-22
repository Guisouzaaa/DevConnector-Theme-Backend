using DevConnectorBackEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectorBackEnd.Models
{
    public static class EducationModel
    {
        private static string connectionString = "Data Source=GUILHERME\\SQLEXPRESS;Initial Catalog=ProjetoDevConnectorBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static List<EducationViewModel> GetEducation(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Education WHERE UserId = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                List<EducationViewModel> exps = new List<EducationViewModel>();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EducationViewModel exp = new EducationViewModel
                    {
                        Id = (int)reader["Id"],
                        SchoolBootcamp = reader["SchoolBootcamp"].ToString(),
                        CurrentSchool = (bool)reader["CurrentSchool"],
                        FromDate = reader["FromDate"].ToString(),
                        ToDate = reader["ToDate"].ToString(),
                        DegreeCertificate = reader["DegreeCertificate"].ToString(),
                        FieldStudy = reader["FieldStudy"].ToString(),
                        ProgramDescription = reader["ProgramDescription"].ToString()
                    };
                    exps.Add(exp);
                }
                return exps;
            }
        }

        public static int AddEducation(EducationViewModel edc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Education(SchoolBootcamp, CurrentSchool, FromDate, ToDate, DegreeCertificate, FieldStudy, ProgramDescription, UserId) 
                            VALUES (@SchoolBootcamp, @CurrentSchool, @FromDate, @ToDate, @DegreeCertificate, @FieldStudy, @ProgramDescription, @UserId)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@SchoolBootcamp", edc.SchoolBootcamp);
                cmd.Parameters.AddWithValue("@CurrentSchool", edc.CurrentSchool);
                cmd.Parameters.AddWithValue("@FromDate", edc.FromDate);
                cmd.Parameters.AddWithValue("@ToDate", edc.ToDate);
                cmd.Parameters.AddWithValue("@DegreeCertificate", edc.DegreeCertificate);
                cmd.Parameters.AddWithValue("@FieldStudy", edc.FieldStudy);
                cmd.Parameters.AddWithValue("@ProgramDescription", edc.ProgramDescription);
                cmd.Parameters.AddWithValue("@UserId", edc.UserId);

                int ret = cmd.ExecuteNonQuery();

                return ret;
            }
        }

        public static int DeleteEducation(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = $"DELETE FROM Education WHERE Id = @id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                int ret = cmd.ExecuteNonQuery();

                return ret;
            }
        }
    }
}
