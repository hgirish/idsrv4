using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleApi
{
    [Route("[controller]")][Authorize]
    public class IdentityController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Json(User.Claims.Select(c => new { c.Type, c.Value }));
        }

       
    }
}
