using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ContasController : ControllerBase
{
    private readonly BancoDbContext _context;

    public ContasController(BancoDbContext context)
    {
        _context = context;
    }

    // GET: api/Contas
    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Conta>>> GetContas()
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Conta is null) return NotFound();
        return await _context.Conta.ToListAsync();
    }

    // GET: api/Contas/5
    [HttpGet]
    [Route("buscar/{ContaID}")]
    public async Task<ActionResult<Conta>> GetConta(int contaID)
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Conta is null) return NotFound();
        var conta = await _context.Conta.FindAsync(contaID);
        if (_context.Conta is null) return NotFound();
        return carro;
    }

    [HttpPut]
    [Route("alterar/{ContaID}")]
    public async Task<ActionResult> Alterar(int ContaID, Conta conta)
    // Recebe o ID da conta a ser alterada e um objeto Conta com os valores que substituirão os valores da Conta com o ID
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Conta is null) return NotFound();
        
        var contaExistente = await _dbContext.Conta.FindAsync(conta.ContaID); // Procurando a conta pelo ID
        if(contaExistente is null) return NotFound(); // Caso não ache a conta com o ID

       // Atualizando os campos da conta existente com os valores da conta recebida.
        contaExistente.NumeroConta = conta.NumeroConta;
        contaExistente.TipoConta = conta.TipoConta;
        contaExistente.Saldo = conta.Saldo;
        contaExistente.DataAbertura = conta.DataAbertura;
        contaExistente.ClienteID = conta.ClienteID;
        contaExistente.AgenciaID = conta.AgenciaID;

        _dbContext.Conta.Update(contaExistente);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }


/*
    // POST: api/Contas
    // Adiciona uma nova conta
    [HttpPost]
    public async Task<ActionResult<Conta>> PostConta(Conta conta)
    {
        _context.Conta.Add(conta);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetConta", new { id = conta.Id }, conta);
    }

    // DELETE: api/Contas/5
    // Deleta uma conta
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConta(int id)
    {
        var conta = await _context.Conta.FindAsync(id);
        if (conta == null)
        {
            return NotFound();
        }

        _context.Conta.Remove(conta);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ContaExists(int id)
    {
        return _context.Conta.Any(e => e.Id == id);
    }
*/
}
