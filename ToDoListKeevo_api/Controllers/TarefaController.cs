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

        /// <summary>
        /// Método responsável para retornar todas as tarefas.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável para retornar todas as tarefas pelo Id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            try
            {
                var tarefa = _repo.GetTarefaById(id);
                if(tarefa == null) return NotFound();
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha: {ex.Message}");
            }
        }

        // Método para retornar tarefas por status.
        /// <summary>
        /// Método responsável para retornar todas as tarefas pelo status.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Método responsável para adicionar tarefas.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(TarefaRegistrarDto model) {
            try
            {
                var tarefa = _mapper.Map<Tarefa>(model);
                _repo.Add(tarefa);

                if (_repo.SaveChanges())
                {
                    return Created($"/api/tarefa/{model.Id}", _mapper.Map<TarefaDto>(tarefa));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha: {ex.Message}");
            }

            return BadRequest("Aluno não criado");
        }

        /// <summary>
        /// Método responsável para atualizar um recurso.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        //Método para atualizar um recurso.
        public IActionResult Put(int id, TarefaRegistrarDto model) {
            try
            {   
                var tarefa = _repo.GetTarefaById(id);
                if(tarefa == null) return NotFound();

                _mapper.Map(model, tarefa);

                _repo.Update(tarefa);
                if(_repo.SaveChanges()) {
                    return Created($"/api/tarefa/{model.Id}", _mapper.Map<TarefaDto>(tarefa));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha: {ex.Message}");
            }

            return BadRequest();
        }

        /// <summary>
        /// Método responsável para atualizar um recurso parcialmente.
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, TarefaPatchDto model) {
            try
            {
                var tarefa = _repo.GetTarefaById(id);
                if(tarefa == null) return NotFound();

                _mapper.Map(model, tarefa);
                
                _repo.Update(tarefa);
                if(_repo.SaveChanges()) {
                    return Created($"/api/tarefa/{model.Id}", _mapper.Map<TarefaPatchDto>(tarefa));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha: {ex.Message}");
            }

            return BadRequest();
        }

        /// <summary>
        /// Método responsável para deletar um recurso.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            try{
                var tarefa = _repo.GetTarefaById(id);
                if(tarefa == null) return NotFound();
                _repo.Delete(tarefa);
                if(_repo.SaveChanges()) {
                    return Ok("Aluno com id: " + tarefa.Id + " deletado com sucesso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha: {ex.Message}");
            }

            return BadRequest();
        }
    }
}