using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class AgenciaController : ControllerBase
{
    private readonly BancoDbContext _context;

    public AgenciaController(BancoDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Agencia>>> Listar()
    {
        if (_context is null || _context.Agencia is null)
        {
            return BadRequest("Contexto ou Agencia não encontrada");
        }

        var agencias = await _context.Agencia.ToListAsync();
        return Ok(agencias);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Agencia>> Get(int id)
    {
        if (_context is null || _context.Agencia is null)
        {
            return BadRequest("Contexto ou Agencia não encontrada");
        }

        var agencia = await _context.Agencia.FindAsync(id);
        if (agencia is null)
        {
            return NotFound();
        }

        return agencia;
    }

    
    [HttpPost]
    public async Task<ActionResult<Agencia>> PostAgencia(Agencia agencia)
    {
        _context.Agencia.Add(agencia);
        await _context.SaveChangesAsync();

        return Created("",agencia);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, Agencia agencia)
    {
        // Atribuindo para o agencia especifico os atributos do objeto agencia
        if(_context is null) return NotFound();
        if(_context.Agencia is null) return NotFound();
    
        var agenciaExistente = await _context.Agencia.FindAsync(id);
        if(agenciaExistente is null) return NotFound();

    
        agenciaExistente.NumeroAgencia = agencia.NumeroAgencia;
        agenciaExistente.Nome = agencia.Nome;
        agenciaExistente.EnderecoID = agencia.EnderecoID;

        _context.Agencia.Update(agenciaExistente);
        await _context.SaveChangesAsync();

        return Ok();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAgencia(int id)
    {
        var agencia = await _context.Agencia.FindAsync(id);
        if (agencia == null)
        {
            return NotFound();
        }

        _context.Agencia.Remove(agencia);
        await _context.SaveChangesAsync();

        return Ok();
    }
}

