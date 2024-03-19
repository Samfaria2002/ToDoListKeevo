using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListKeevo_api.Models;
using ToDoListKeevo_api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ToDoListKeevo_api.Data
{
    //O repositório é uma camada de abstração que faz a comunicação entre a aplicação e o banco de dados.
    //Aqui estão os métodos que fazem a comunicação com o banco de dados.
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

        public async Task<PageList<Tarefa>> GetAllTarefasAsync(PageParams pageParam) {
            IQueryable<Tarefa> query = _context.Tarefas;

            query = query.AsNoTracking().OrderBy(q => q.Id);
            return await PageList<Tarefa>.CreateAsync(query, pageParam.PageNumber, pageParam.PageSize);
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

        public async Task<Tarefa[]> GetAllTarefasByStatusAsync(StatusTarefa status) {
            IQueryable<Tarefa> query = _context.Tarefas;

            query = query.AsNoTracking().OrderBy(q => q.Id).Where(q => q.Status == status);

            return await query.ToArrayAsync();
        }

    }
}