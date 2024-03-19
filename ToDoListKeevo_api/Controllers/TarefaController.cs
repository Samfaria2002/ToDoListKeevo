using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ToDoListKeevo_api.Models;
using ToDoListKeevo_api.Data;
using ToDoListKeevo_api.Dto;
using ToDoListKeevo_api.Helpers;

namespace ToDoListKeevo_api.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public TarefaController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        //Método para retornar todas as tarefas.
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams) {
            try
            {
                var tarefas = await _repo.GetAllTarefasAsync(pageParams);
                var tarefasRetorno = _mapper.Map<IEnumerable<TarefaDto>>(tarefas);
                Response.AddPagination(tarefas.CurrentPage, tarefas.PageSize, tarefas.TotalCount, tarefas.TotalPages);
                return Ok(tarefasRetorno);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha: {ex.Message}");
            }
        }

        // Método para retornar tarefas por status.
        [HttpGet("byStatus")]
        public async Task<IActionResult> GetByStatus(StatusTarefa status)
        {
            try
            {
                var tarefas = await _repo.GetAllTarefasByStatusAsync(status);
                if (tarefas == null) return NotFound();
                return Ok(tarefas);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        //Método para adicionar tarefas.
        [HttpPost]
        public IActionResult Post(Tarefa model) {
            try
            {
                _repo.Add(model);

                if(_repo.SaveChanges())
                {
                    return Created($"/api/tarefa/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou ao adicionar dados.");
            }

            return BadRequest("Aluno não criado");
        }

        [HttpPut("byId")]
        //Método para atualizar um recurso.
        public IActionResult Put(int id, Tarefa model) {
            try
            {   
                var tarefa = _repo.GetTarefaById(id);
                if(tarefa == null) return NotFound();
                _repo.Update(tarefa);
                if(_repo.SaveChanges()) {
                    return Created($"/api/tarefa/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou ao atualizar dados.");
            }

            return BadRequest();
        }

        //O método para atualizar parcialmente um recurso.
        [HttpPatch("byId")]
        public IActionResult Patch(int id, TarefaPatchDto model) {
            try
            {
                var tarefa = _repo.GetTarefaById(id);
                if(tarefa == null) return NotFound();
                _repo.Update(tarefa);
                if(_repo.SaveChanges()) {
                    return Created($"/api/tarefa/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou ao atualizar dados.");
            }

            return BadRequest();
        }

        //Método para deletar um recurso.
        [HttpDelete("byId")]
        public IActionResult Delete(int id) {
            try{
                var tarefa = _repo.GetTarefaById(id);
                if(tarefa == null) return NotFound();
                _repo.Delete(tarefa);
                if(_repo.SaveChanges()) {
                    return Ok("Aluno com id: " + tarefa.Id + " deletado com sucesso.");
                }
            }
            catch (System.Exception) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou ao deletar dados.");
            }

            return BadRequest();
        }
    }
}