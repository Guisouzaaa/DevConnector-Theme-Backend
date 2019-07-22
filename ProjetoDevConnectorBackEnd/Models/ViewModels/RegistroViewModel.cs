using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectorBackEnd.Models.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Digite seu nome de usuário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o seu e-mail"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite sua senha"), DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Digite sua senha novamente"), Compare("Senha", ErrorMessage = "Senhas diferentes"), DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }
    }
}
