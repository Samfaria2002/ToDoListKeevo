using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ToDoListKeevo.Models;
using ToDoListKeevo.Data;
using ToDoListKeevo.Dto;

namespace ToDoListKeevo.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public TarefaController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Método para retornar todas as tarefas.
        [HttpGet]
        public IActionResult Get() {
            try
            {
                var tarefas = _repo.GetAllTarefas();
                return Ok(_mapper.Map<IEnumerable<TarefaDto>>(tarefas));
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        //Método para retornar tarefas por status.
        [HttpGet("byStatus")]
        public IActionResult GetByType(StatusTarefa status) {
            try
            {
                var tarefas = _repo.GetAllTarefasByStatus(status);
                if(tarefas == null) return NotFound();

                var tarefasDto = _mapper.Map<TarefaDto>(tarefas);
                return Ok(tarefasDto);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        //Método para adicionar tarefas.
        [HttpPost]
        public IActionResult Post(TarefaDto model) {
            try
            {
                var tarefa = _mapper.Map<Tarefa>(model);
                _repo.Add(tarefa);

                if(_repo.SaveChanges())
                {
                    return Created($"/api/tarefa/{tarefa.Id}", _mapper.Map<TarefaDto>(tarefa));
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
        public IActionResult Put(int id, TarefaDto model) {
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
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou ao atualizar dados.");
            }

            return BadRequest();
        }

        //O método para atualizar parcialmente um recurso.
        [HttpPatch("byId")]
        public IActionResult Patch(int id, TarefaDto model) {
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