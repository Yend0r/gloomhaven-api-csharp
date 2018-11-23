using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloomChars.Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Bearded.Monads;
using Microsoft.AspNetCore.Authorization;

namespace GloomChars.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IAuthService _authSvc;

        public ValuesController(IAuthService authSvc)
        {
            _authSvc = authSvc;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //var authResult = _authSvc.Authenticate("rod.buchan@gmail.com", "test1234");

            //var xx = authResult.Unify(user => user.Email, err => err);

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize(Roles="ssssss")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            return "from post";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
