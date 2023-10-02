using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class InvestimentoController : ControllerBase
{
    private readonly BancoDbContext _context;

    public InvestimentoController(BancoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Investimento>>> GetInvestimentos()
    {
        var investimentos = await _context.Investimento.ToListAsync();

        if (investimentos == null || investimentos.Count == 0)
            return NotFound();

        return investimentos;
    }

    [HttpGet]
    [Route("buscar/{InvestimentoID}")]
    public async Task<ActionResult<Investimento>> GetInvestimento(int investimentoID)
    {
        var investimento = await _context.Investimento.FindAsync(investimentoID);

        if (investimento == null)
            return NotFound();

        return investimento;
    }

    [HttpPost]
    [Route("criar")]
    public async Task<ActionResult<Investimento>> CriarInvestimento([FromBody] Investimento investimento)
    {
        if (investimento == null)
            return BadRequest("Dados inválidos para o investimento.");

        investimento.ID = _context.Investimento.Count() + 1;
        _context.Investimento.Add(investimento);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("DefaultApi", new { id = investimento.ID }, investimento);
    }
}
