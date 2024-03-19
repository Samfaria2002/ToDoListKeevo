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
    public class TarefaRegistrarDto
    {
        
        public int Id { get; set; }

        public string Nome { get; set; }

        public StatusTarefa Status { get; set; }

        public TipoTarefa Tipo { get; set; }

        public DateTime Prazo { get; set; }

        public PrioridadeTarefa Prioridade { get; set; }
    }
}