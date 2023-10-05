using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerEndereco : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<IEnumerable<Endereco>> Get()
        {
            
            List<Endereco> enderecos = new List<Endereco>
            {
                new Endereco("Rua Principal", "123", "Centro", "Cidade1", "Estado1", "12345-678"),
                new Endereco("Rua Secundária", "456", "Subúrbio", "Cidade2", "Estado2", "98765-432")
            };

            return Ok(enderecos);
        }

        
        [HttpGet("{id}")]
        public ActionResult<Endereco> Get(int id)
        {
            
            var endereco = new Endereco("Rua Principal", "123", "Centro", "Cidade1", "Estado1", "12345-678");
            return Ok(endereco);
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] Endereco endereco)
        {
            
            return CreatedAtAction("Get", new { id = endereco.Id }, endereco);
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Endereco endereco)
        {
            
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            return NoContent();
        }
    }
}
