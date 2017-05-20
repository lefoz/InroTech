using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inrotech.Domain.UserDb;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserDb _val = new Grp_UserDatabase();

        // GET: api/user/name:password
        [HttpGet("{name}:{pass}")]
        public bool Get(string name, string pass)
        {
            return _val.GetUser(name, pass);
        }
    }
}
