using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListKeevo.Models;
using Microsoft.EntityFrameworkCore;

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

        public Tarefa[] GetAllTarefasById(int id) {
            IQueryable<Tarefa> query = _context.Tarefas;

            query = query.AsNoTracking().OrderBy(q => q.Id == id);

            return query.ToArray();
        }

        public Tarefa GetTarefaById(int id) {
            IQueryable<Tarefa> query = _context.Tarefas;

            query = query.AsNoTracking().OrderBy(q => q.Id)
                        .Where(q => q.Id == id);

            return query.FirstOrDefault();
        }

        public Tarefa[] GetAllTarefasByStatus(StatusTarefa status) {
            IQueryable<Tarefa> query = _context.Tarefas;

            query = query.AsNoTracking().OrderBy(q => q.Id).Where(q => q.Status == status);

            return query.ToArray();
        }

    }
}