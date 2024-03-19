using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListKeevo_api.Models;
using ToDoListKeevo_api.Dto;
using AutoMapper;

namespace ToDoListKeevo_api.Helpers
{
    public class ApiProfile : Profile
    {
        public ApiProfile(){
            CreateMap<Tarefa, TarefaDto>();
            CreateMap<Tarefa, TarefaRegistrarDto>().ReverseMap();
            CreateMap<Tarefa, TarefaPatchDto>().ReverseMap();

            CreateMap<PageList<Tarefa>, IEnumerable<TarefaDto>>()
                .ConvertUsing(pageList => pageList.Select(t => new TarefaDto
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    Status = t.Status,
                    Tipo = t.Tipo,
                    Prazo = t.Prazo,
                    Prioridade = t.Prioridade
                }));
        }
    }
}