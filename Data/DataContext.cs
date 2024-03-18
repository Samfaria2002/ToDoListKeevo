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
                .HasData(new List<Tarefa>(){
                    new Tarefa(1, "Estudar C#", new DateTime(2024, 3, 25, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.EmAndamento, TipoTarefa.Estudo),
                    new Tarefa(2, "Estudar PHP", new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.EmAndamento, TipoTarefa.Estudo), // Exemplo com data nula
                    new Tarefa(3, "Estudar Angular", new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.Concluida, TipoTarefa.Estudo),
                    new Tarefa(4, "Comprar leite", new DateTime(2024, 3, 17, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.Pendente, TipoTarefa.Compras),
                    new Tarefa(5, "Limpar a casa", new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.Concluida, TipoTarefa.Pessoal), // Exemplo com data nula
                    new Tarefa(6, "Cinema com amigos", new DateTime(2024, 4, 4, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.EmAndamento, TipoTarefa.Lazer),
                    new Tarefa(7, "Consertar o teclado", new DateTime(2024, 3, 19, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.Pendente, TipoTarefa.Pessoal),
                    new Tarefa(8, "Subir para o docker", new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.EmAndamento, TipoTarefa.Trabalho), // Exemplo com data nula
                    new Tarefa(9, "Fazer o almoço", new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.Concluida, TipoTarefa.Pessoal), // Exemplo com data nula
                    new Tarefa(10, "Aula quinta a noite", new DateTime(2024, 3, 21, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.Pendente, TipoTarefa.Estudo),
                    new Tarefa(11, "Trocar a lente", new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.Pendente, TipoTarefa.Pessoal), // Exemplo com data nula
                    new Tarefa(12, "Comprar farinha", new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc), StatusTarefa.Concluida, TipoTarefa.Compras) // Exemplo com data nula
                });
            
        }  

    }
}