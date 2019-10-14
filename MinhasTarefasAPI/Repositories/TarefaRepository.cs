using MinhasTarefasAPI.Database;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly MinhasTarefasContext _banco;
        public TarefaRepository(MinhasTarefasContext banco)
        {
            _banco = banco;
        }

        public List<Tarefa> Restauracao(ApplicationUser usuario, DateTime dataUltimSincronizacao)
        {
            var query = _banco.Tarefas.Where(a => a.UsuarioId == usuario.Id).AsQueryable();
            if (dataUltimSincronizacao == null)
            {
                query.Where(a => a.Criado >= dataUltimSincronizacao || a.Atualizado >= dataUltimSincronizacao);
            }

            return query.ToList<Tarefa>();
        }

        public List<Tarefa> Sincronizacao(List<Tarefa> tarefas)
        {
            var tarefasExcluidasAtualizadas = tarefas.Where(a => a.IdTarefaApi != 0).ToList();
            var novasTarefas = tarefas.Where(a => a.IdTarefaApi == 0).ToList();
            if (novasTarefas.Count() > 0)
            {
                foreach (var tarefa in novasTarefas)
                {
                    _banco.Tarefas.Add(tarefa);
                }
            }

            
            if (tarefasExcluidasAtualizadas.Count() > 0)
            {
                foreach (var tarefa in novasTarefas)
                {
                    _banco.Tarefas.Update(tarefa);
                }    
            }
            _banco.SaveChanges();

            return novasTarefas.ToList();
        }
    }
}
