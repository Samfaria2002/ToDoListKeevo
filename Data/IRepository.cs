using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListKeevo.Controllers;
using ToDoListKeevo.Models;

namespace ToDoListKeevo.Data
{
    /*Para manter o código organizado, é uma boa prática criar uma 
    interface para o repositório.
    Isso permite que o repositório seja injetado em outras 
    classes, como o controlador.*/
    public class IRepository
    {
        void Add<T>(T entity) where T : class {}
        void Update<T>(T entity) where T : class {}
        void Delete<T>(T entity) where T : class {}
        bool SaveChanges();
        
        Tarefa[] GetAllTarefas();
        Tarefa[] GetAllTarefasByStatus(TipoTarefa tipo);
    }
}