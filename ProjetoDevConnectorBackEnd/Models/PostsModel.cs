using DevConnectorBackEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectorBackEnd.Models
{
    public static class PostsModel
    {
        private static string connectionString = "Data Source=GUILHERME\\SQLEXPRESS;Initial Catalog=ProjetoDevConnectorBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static List<PostsViewModel> getAllPosts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Posts";
                SqlCommand cmd = new SqlCommand(sql, conn);

                List<PostsViewModel> posts = new List<PostsViewModel>();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PostsViewModel post = new PostsViewModel
                    {
                        Id = (int)reader["id"],
                        PostBody = reader["PostBody"].ToString(),
                        Likes = (int)reader["Likes"],
                        Dislikes = (int)reader["Dislikes"],
                        UserId = reader["UserId"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        UserPic = reader["UserPic"].ToString()
                    };
                    posts.Add(post);
                }
                return posts;
            }
        }

        public static PostsViewModel getPostById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Posts WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                PostsViewModel post = null;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    post = new PostsViewModel
                    {
                        Id = (int)reader["Id"],
                        PostBody = reader["PostBody"].ToString(),
                        Likes = (int)reader["Likes"],
                        Dislikes = (int)reader["Dislikes"],
                        UserId = reader["UserId"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        UserPic = reader["UserPic"].ToString()
                    };
                }
                return post;
            }
        }

        public static int AddPost(PostsViewModel post)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Posts (PostBody, Dislikes, Likes, UserId, UserName, UserPic) 
                            VALUES (@PostBody, @Dislikes, @Likes, @UserId, @UserName, @UserPic)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@PostBody", post.PostBody);
                cmd.Parameters.AddWithValue("@Dislikes", post.Dislikes);
                cmd.Parameters.AddWithValue("@Likes", post.Likes);
                cmd.Parameters.AddWithValue("@UserId", post.UserId);
                cmd.Parameters.AddWithValue("@UserName", post.UserName);
                cmd.Parameters.AddWithValue("@UserPic", post.UserPic == null ? "": post.UserPic);

                int ret = cmd.ExecuteNonQuery();

                return ret;
            }
        }

        public static int UpdatePostLikes(PostsViewModel post)
        {
            post.Likes++;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = $"UPDATE Posts SET Likes = @likes WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", post.Id);
                cmd.Parameters.AddWithValue("@Likes", post.Likes);

                int ret = cmd.ExecuteNonQuery();

                return ret;
            }
        }

        public static int UpdatePostDislikes(PostsViewModel post)
        {
            post.Dislikes++;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = $"UPDATE Posts SET Dislikes = @dislikes WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", post.Id);
                cmd.Parameters.AddWithValue("@Dislikes", post.Dislikes);


                int ret = cmd.ExecuteNonQuery();

                return ret;
            }
        }

    }
}
