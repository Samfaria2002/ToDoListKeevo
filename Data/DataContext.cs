using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using ToDoListKeevo.Models;

namespace ToDoListKeevo.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<Tarefa>()
                .Property(t => t.Prazo)
                .HasColumnType("date");

            builder.Entity<Tarefa>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusTarefa)Enum.Parse(typeof(StatusTarefa), v))
                .HasColumnType("text");

            builder.Entity<Tarefa>()
                .Property(e => e.Tipo)
                .HasConversion(
                    v => v.ToString(),
                    v => (TipoTarefa)Enum.Parse(typeof(TipoTarefa), v))
                .HasColumnType("text");
        }  

    }
}