using Microsoft.AspNetCore.Identity;
using MinhasTarefasAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Repositories.V1.Contracts
{
    public interface IUsuarioRepository
    {
   
        void Cadastrar(ApplicationUser usuario, string senha);

        ApplicationUser Obter(string email, string senha);
        ApplicationUser Obter(string id);

    }
}
