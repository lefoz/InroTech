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
        Sim_Taskmanager tasks = new Inrotech.Domain.Taskmanager.Sim_Taskmanager();
        
        // GET: api/User
        [HttpGet]
        public Sim_Taskmanager Get()
        {   
            return tasks;
        }
    }
}
