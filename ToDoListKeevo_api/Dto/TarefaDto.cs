using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoListKeevo_api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ToDoListKeevo_api.Dto
{
    public class TarefaDto
    {
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

        //Conversão ímplicita de Tarefa para TarefaDto.
        //Usada antes do AutoMapper.
        /*
        public static implicit operator TarefaDto(Tarefa tarefa) {
        return new TarefaDto {
            Id = tarefa.Id,
            Nome = tarefa.Nome,
            Status = tarefa.Status,
            Tipo = tarefa.Tipo,
            Prazo = tarefa.Prazo,
            Prioridade = tarefa.Prioridade
        };
        */
    }
    
}