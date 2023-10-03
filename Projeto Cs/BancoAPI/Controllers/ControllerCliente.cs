using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ControllerCliente : ControllerBase
{
    private readonly BancoDbContext _context;

    public ControllerCliente(BancoDbContext context)
    {
        _context = context;
    }

[HttpGet()]
    [Route("buscar/{CPF}")]
    public async Task<ActionResult<Cliente>> Buscar([FromRoute] string CPF)
    {
        if(_context.Cliente is null)
            return NotFound();
        var cliente = await _context.Cliente.FindAsync(CPF);
        if (cliente is null)
            return NotFound();
        return cliente;
    }

    [HttpPost]
    [Route("Cadastrar/ Cliente")]
    public IActionResult Cadastrar(Cliente cliente)
    {
        _context.Add(cliente);
        _context.SaveChanges();
        return Created("", cliente);
    }   
    [HttpPut]
    [Route("Alterar Cadastro")]
    public async Task<ActionResult> Alterar(int id, Cliente cliente)
    {
        //Verificar cliente
        if(_context is null) return NotFound();
        if(_context.Cliente is null) return NotFound();

        //Verifica id
        var contaExistente = await _context.Conta.FindAsync(id);
        if(contaExistente is null) return NotFound();

        //Faz o up do cadastro
        _context.Cliente.Update(cliente);
        await _context.SaveChangesAsync();

        //Retorna
        return Ok();
    }
}