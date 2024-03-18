using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ToDoListKeevo.Models
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusTarefa
    {
        [Display(Name = "Pendente")]
        Pendente,

        [Display(Name = "Em Andamento")]
        EmAndamento,

        [Display(Name = "Concluída")]
        Concluida
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TipoTarefa
    {
        [Display(Name = "Estudo")]
        Estudo,

        [Display(Name = "Trabalho")]
        Trabalho,

        [Display(Name = "Lazer")]
        Lazer,

        [Display(Name = "Compras")]
        Compras,

        [Display(Name = "Pessoal")]
        Pessoal
    }


    public class Tarefa
    {

        public Tarefa(int id, string nome, DateTime prazo, StatusTarefa status, TipoTarefa tipo) 
        {
            Id = id;
            Nome = nome;
            Prazo = prazo;
            Status = status;
            Tipo = tipo;

            if(Prazo != null) {
                Prazo.ToString("dd/MM/yyyy");
            }

            if(prazo != null && prazo < DateTime.Now)
            {
                Status = StatusTarefa.Concluida;
            }
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Prazo { get; set; }

        [EnumDataType(typeof(StatusTarefa))]
        public StatusTarefa Status { get; set; }

        [EnumDataType(typeof(TipoTarefa))]
        public TipoTarefa Tipo { get; set; }

    }
}