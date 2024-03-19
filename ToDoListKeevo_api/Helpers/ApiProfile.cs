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
            CreateMap<TarefaDto, Tarefa>();
        }
    }
}