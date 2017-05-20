using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inrotech.Domain.UserDb;
using System.Data;
using Inrotech.Domain.Taskmanager;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    public class TaskmanagerController : Controller
    {
        ITaskmanager tasks = new Sim_Taskmanager();
        
        // GET: api/taskmanager
        [HttpGet]
        public DataTable Get()
        {   
            return tasks.getTaskmanager();
        }
    }
}
