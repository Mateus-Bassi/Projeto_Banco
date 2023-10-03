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
        // GET: api/Endereco
        [HttpGet]
        public ActionResult<IEnumerable<Endereco>> Get()
        {
            // Aqui você pode implementar a lógica para retornar uma lista de endereços
            // Isso pode incluir a consulta a um banco de dados, por exemplo
            List<Endereco> enderecos = new List<Endereco>
            {
                new Endereco("Rua Principal", "123", "Centro", "Cidade1", "Estado1", "12345-678"),
                new Endereco("Rua Secundária", "456", "Subúrbio", "Cidade2", "Estado2", "98765-432")
            };

            return Ok(enderecos);
        }

        // GET: api/Endereco/5
        [HttpGet("{id}")]
        public ActionResult<Endereco> Get(int id)
        {
            // Aqui você pode implementar a lógica para retornar um endereço com o ID especificado
            // Isso também pode envolver consultas a um banco de dados
            var endereco = new Endereco("Rua Principal", "123", "Centro", "Cidade1", "Estado1", "12345-678");
            return Ok(endereco);
        }

        // POST: api/Endereco
        [HttpPost]
        public IActionResult Post([FromBody] Endereco endereco)
        {
            // Aqui você pode implementar a lógica para criar um novo endereço
            // Isso pode envolver a inserção do endereço em um banco de dados, por exemplo
            return CreatedAtAction("Get", new { id = endereco.Id }, endereco);
        }

        // PUT: api/Endereco/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Endereco endereco)
        {
            // Aqui você pode implementar a lógica para atualizar um endereço com o ID especificado
            // Isso pode envolver a atualização do endereço em um banco de dados, por exemplo
            return NoContent();
        }

        // DELETE: api/Endereco/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Aqui você pode implementar a lógica para excluir um endereço com o ID especificado
            // Isso pode envolver a exclusão do endereço de um banco de dados, por exemplo
            return NoContent();
        }
    }
}
