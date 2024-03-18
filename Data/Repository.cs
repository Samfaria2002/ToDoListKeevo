using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListKeevo.Models;

namespace ToDoListKeevo.Data
{
    /*O repositório herda de IRepository, é a classe que se 
    comunica com o banco de dados. Ele é responsável pelas ações de CRUD.
    */
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }
        
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class {
            _context.Remove(entity);
        }

        public bool SaveChanges() {
            return(_context.SaveChanges() > 0);
        }

        public Tarefa[] GetAllTarefas() {
            IQueryable<Tarefa> query = _context.Tarefas;

            query = query.AsNoTracking().OrderBy(q => q.Id);
            return query.ToArray();
        }

        public Tarefa[] GetAllTarefasByStatus(TipoTarefa tipo) {
            IQueryable<Tarefa> query = _context.Tarefas;

            query = query.AsNoTracking().OrderBy(q => q.Id). where(q => q.Tipo == tipo);

            return query.ToArray();
        }


    }
}