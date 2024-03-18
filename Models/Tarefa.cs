using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListKeevo.Models
{

    public enum StatusTarefa
    {
        Pendente,
        EmAndamento,
        Concluida
    }

    public enum TipoTarefa
    {
        Estudo,
        Trabalho,
        Lazer,
        Compras,
        Pessoal
    }


    public class Tarefa
    {

        public Tarefa(int id, string nome, DateTime prazo, StatusTarefa status, TipoTarefa tipo) 
        {
            Id = id;
            Nome = nome;
            Prazo = prazo.ToUniversalTime();
            Status = status;
            Tipo = tipo;

            if(prazo != null && prazo < DateTime.Now)
            {
                Status = StatusTarefa.Concluida;
            }
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Prazo { get; set; }
        public StatusTarefa Status { get; set; }
        public TipoTarefa Tipo { get; set; }
    }
}