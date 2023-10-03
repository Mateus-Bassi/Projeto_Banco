using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class SeguroController : ControllerBase
{
    private readonly BancoDbContext _context;

    public SeguroController(BancoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Seguro>>> GetSeguros()
    {
        if(_context is null) return NotFound();
        if(_context.Seguro is null) return NotFound();
        return await _context.Seguro.ToListAsync();
    }
}
