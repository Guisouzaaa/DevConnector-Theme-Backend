using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectorBackEnd.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Digite seu email"), EmailAddress(ErrorMessage = "Digite um email válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite sua senha"), DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
