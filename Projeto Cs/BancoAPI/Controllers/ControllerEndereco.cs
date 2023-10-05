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
                //
            };

            return Ok(enderecos);
        }

        
        [HttpGet("{id}")]
        public ActionResult<Endereco> Get(int id)
        {
            //
            return Ok();
            
        }

        
        [HttpPost]
        public IActionResult Post(Endereco endereco)
        {
            //
            return CreatedAtAction("Get", new { id = endereco.EnderecoID }, endereco);
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, Endereco endereco)
        {
            //
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //
            return NoContent();
        }
    }
}
