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
    public async Task<ActionResult<IEnumerable<Agencia>>> GetAgencia()
    {
        return await _context.Agencia.Include(a => a.Endereco).ToListAsync();
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<Agencia>> GetAgencia(int id)
    {
        var agencia = await _context.Agencia.Include(a => a.Endereco).FirstOrDefaultAsync(a => a.AgenciaID == id);

        if (agencia == null)
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

        return CreatedAtAction("GetAgencia", new { id = agencia.AgenciaID }, agencia);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAgencia(int id, Agencia agencia)
    {
        if (id != agencia.AgenciaID)
        {
            return BadRequest();
        }

        _context.Entry(agencia).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AgenciaExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
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

    private bool AgenciaExists(int id)
    {
        return _context.Agencia.Any(e => e.AgenciaID == id);
    }
}

