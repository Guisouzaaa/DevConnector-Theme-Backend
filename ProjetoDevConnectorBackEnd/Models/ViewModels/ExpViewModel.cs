using System.ComponentModel.DataAnnotations;

namespace DevConnectorBackEnd.Models.ViewModels
{
    public class ExpViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string Company { get; set; }
        public string Location { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        public bool CurrentJob { get; set; }
        public string JobDescription { get; set; }
    }
}
