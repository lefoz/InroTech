using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inrotech.Domain.Sim;
using System.Data;
using Inrotech.Domain.Taskmanager;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    public class TaskmanagerController : Controller
    {
        
        // GET: api/User
        [HttpGet]
        public string Get()
        {   
            return null;
        }
    }
}
