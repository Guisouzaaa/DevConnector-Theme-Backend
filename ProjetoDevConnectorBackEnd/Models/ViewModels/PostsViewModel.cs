

using System.ComponentModel.DataAnnotations;

namespace DevConnectorBackEnd.Models.ViewModels
{
    public class PostsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string PostBody { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPic { get; set; }
    }
}
