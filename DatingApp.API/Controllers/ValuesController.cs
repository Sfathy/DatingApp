using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DateingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;

        }
        // GET api/values
        [HttpGet]
        public  IActionResult Get()
        {
           // var values =  _context.Values.ToList();
            List<Value> values = new List<Value>();
            Value v ;
            for (int i = 0; i < 3; i++)
            {
                v = new Value();
                v.Id = i;
                v.Name = "value"+i.ToString();
                values.Add(v);
            }
            //values.Add()
            //return Ok(  new string[] { "value1", "value2" });
            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var value = _context.Values.FirstOrDefault(x=>x.Id==id);
            return Ok(value);
            //return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
