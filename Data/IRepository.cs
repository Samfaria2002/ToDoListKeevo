using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListKeevo.Models;

namespace ToDoListKeevo.Data
{   
    //Interface para o repositório
    //O repositório é uma camada de abstração que faz a comunicação entre a aplicação e o banco de dados.
    public interface IRepository
    {
        void Add<T>(T entity) where T : class {}

        void Update<T>(T entity) where T : class {}

        void Delete<T>(T entity) where T : class {}

        bool SaveChanges();
        
        
        Tarefa[] GetAllTarefas();

        Tarefa[] GetAllTarefasById(int id);

        Tarefa GetTarefaById(int id);

        Tarefa[] GetAllTarefasByStatus(StatusTarefa Status);
    }
}