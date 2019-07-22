using DevConnectorBackEnd.Models.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DevConnectorBackEnd.Models
{
    public static class CommentsModel
    {
        private static string connectionString = "Data Source=GUILHERME\\SQLEXPRESS;Initial Catalog=ProjetoDevConnectorBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
       
        public static List<CommentsViewModel> getCommentById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Comentarios WHERE PostId = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);


                List<CommentsViewModel> comments = new List<CommentsViewModel>();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CommentsViewModel comm = new CommentsViewModel
                    {
                        Id = (int)reader["id"],
                        PostId = (int)reader["PostId"],
                        Comment = reader["Comentario"].ToString(),
                        UserId = reader["UserId"].ToString(),
                        UserName = reader["UserName"].ToString()
                    };
                    comments.Add(comm);
                }
                return comments;
            }
        }

        public static int AddComment(CommentsViewModel comment)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Comentarios(PostId, Comentario, UserId, UserName) VALUES (@PostId, @Comentario, @UserId, @UserName)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                cmd.Parameters.AddWithValue("@Comentario", comment.Comment);
                cmd.Parameters.AddWithValue("@UserId", comment.UserId);
                cmd.Parameters.AddWithValue("@UserName", comment.UserName);

                int ret = cmd.ExecuteNonQuery();

                return ret;
            }
        }
    }
}
