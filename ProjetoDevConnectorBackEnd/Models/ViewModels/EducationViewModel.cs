using System.ComponentModel.DataAnnotations;

namespace DevConnectorBackEnd.Models.ViewModels
{
    public class EducationViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string SchoolBootcamp { get; set; }
        public string DegreeCertificate { get; set; }
        public string FieldStudy { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        public bool CurrentSchool { get; set; }
        public string ProgramDescription { get; set; }
    }
}
