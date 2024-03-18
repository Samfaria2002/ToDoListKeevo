using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListKeevo.Dto
{
    public class TarefaRegistrarDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Prazo { get; set; }

        [EnumDataType(typeof(StatusTarefa))]
        public StatusTarefa Status { get; set; }

        [EnumDataType(typeof(TipoTarefa))]
        public TipoTarefa Tipo { get; set; }
    }
}