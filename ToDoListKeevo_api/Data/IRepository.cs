using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListKeevo_api.Models;
using ToDoListKeevo_api.Helpers;

namespace ToDoListKeevo_api.Data
{   
    //Interface para o repositório
    //O repositório é uma camada de abstração que faz a comunicação entre a aplicação e o banco de dados.
    public interface IRepository
    {
        void Add<T>(T entity) where T : class {}

        void Update<T>(T entity) where T : class {}

        void Delete<T>(T entity) where T : class {}

        bool SaveChanges();

        void ReorderId();
        
        
        Task<PageList<Tarefa>> GetAllTarefasAsync(PageParams pageParam);

        Tarefa[] GetAllTarefas();

        Tarefa[] GetAllTarefasById(int id);

        Tarefa GetTarefaById(int id);

        Task<Tarefa[]> GetAllTarefasByStatusAsync(StatusTarefa Status);
    }
}