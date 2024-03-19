using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.EntityFrameworkCore;

namespace ToDoListKeevo_api.Models
{   
    //Enum para status da tarefa.
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

    //Enum para tipo da tarefa.
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

    //Enum para prioridade da tarefa.
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PrioridadeTarefa
    {
        [Display(Name = "Baixa")]
        Baixa,

        [Display(Name = "Média")]
        Media,

        [Display(Name = "Alta")]
        Alta
    }

    //Classe para modelar as tarefas
    //Atributos da tarefa.
    public class Tarefa
    {

        public Tarefa(int id, string nome, StatusTarefa status, TipoTarefa tipo, DateTime prazo, PrioridadeTarefa prioridade) 
        {
            Id = id;
            Nome = nome;
            Status = status;
            Tipo = tipo;
            Prazo = prazo;
            Prioridade = prioridade;

            if(Prazo != null) {
                Prazo.ToString("dd/MM/yyyy");
            }

            if(prazo != null && prazo < DateTime.Now)
            {
                Status = StatusTarefa.Concluida;
            }
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nome { get; set; }

        [EnumDataType(typeof(StatusTarefa))]
        public StatusTarefa Status { get; set; }

        [EnumDataType(typeof(TipoTarefa))]
        public TipoTarefa Tipo { get; set; }

        public DateTime Prazo { get; set; }

        [EnumDataType(typeof(PrioridadeTarefa))]
        public PrioridadeTarefa Prioridade { get; set; }

    }
}