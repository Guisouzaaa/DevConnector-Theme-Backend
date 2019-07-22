

using System.ComponentModel.DataAnnotations;

namespace DevConnectorBackEnd.Models.ViewModels
{
    public class CommentsViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        [Required]
        public string Comment { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
