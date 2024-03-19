using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListKeevo.Models;
using ToDoListKeevo.Dto;
using AutoMapper;

namespace ToDoListKeevo.Helpers
{
    public class ApiProfile : Profile
    {
        public ApiProfile(){
            CreateMap<TarefaDto, Tarefa>();
        }
    }
}