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
        //Nessa classe, é feito o mapeamento entre os modelos e os DTOs.
        public ApiProfile(){
            CreateMap<Tarefa, TarefaDto>();
            CreateMap<Tarefa, TarefaRegistrarDto>().ReverseMap();
            CreateMap<Tarefa, TarefaPatchDto>().ReverseMap();

            //Aqui é feito o mapeamento entre PageList e IEnumerable.
            //Como o PageList é uma lista de páginas, é necessário fazer um mapeamento para IEnumerable.
            //Caso o mapeamento não seja feito, retorno do método GetAllTarefasAsync retornará um erro.
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