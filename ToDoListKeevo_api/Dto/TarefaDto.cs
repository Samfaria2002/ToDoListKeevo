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

    /*DTO (Data Transfer Object) é um padrão de projeto que tem 
    como objetivo transferir dados entre subsistemas de um software. */
    
    public class TarefaDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public StatusTarefa Status { get; set; }

        public TipoTarefa Tipo { get; set; }

        public DateTime Prazo { get; set; }

        public PrioridadeTarefa Prioridade { get; set; }

        //Conversão ímplicita de Tarefa para TarefaDto. Usada antes do AutoMapper.
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