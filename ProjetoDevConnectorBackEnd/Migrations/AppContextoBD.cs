using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectorBackEnd.Models
{
    public class AppContextoBD : IdentityDbContext<UsuarioModel>
    {
        public AppContextoBD(DbContextOptions<AppContextoBD> options)
        : base(options)
        {
        }

    }
}
