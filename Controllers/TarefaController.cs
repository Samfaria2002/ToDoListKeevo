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

namespace ToDoListKeevo.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly DataContext _context;

        public TarefaController(DataContext context)
        {
            _context = context;
        }
    }
}