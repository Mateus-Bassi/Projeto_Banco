using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class MovimentacaoController : ControllerBase
{
    private readonly BancoDbContext _context;

    public MovimentacaoController(BancoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Movimentacao>>> GetMovimentacoes()
    {
        if(_context is null) return NotFound();
        if(_context.Movimentacao is null) return NotFound();
        return await _context.Movimentacao.ToListAsync();
    }

    [HttpGet]
    [Route("buscar/{MovimentacaoID}")]
    public async Task<ActionResult<Movimentacao>> GetMovimentacao(int movimentacaoID)
    {
        if(_context is null) return NotFound();
        if(_context.Movimentacao is null) return NotFound();
        var movimentacao = await _context.Movimentacao.FindAsync(movimentacaoID);
        if (movimentacao is null) return NotFound();
        return movimentacao;
    }

    [HttpPost]
    [Route("criar")]
    public async Task<ActionResult<Conta>> Criar(Movimentacao movimentacao)
    {
        if(_context is null) return NotFound();
        if(_context.Movimentacao is null) return NotFound();

        _context.Movimentacao.Add(movimentacao);
        await _context.SaveChangesAsync();

        return Created("", movimentacao);
    }

    [HttpPost]
    [Route("movimentar/{ContaID}")]
    public async Task<ActionResult> Movimentar(int ContaID, decimal valor, Movimentacao movimentacao)
    {
        var conta = await _context.Conta.FindAsync(ContaID);
        if(conta == null) return NotFound();

        if(movimentacao.Tipo == TipoMovimentacao.Saque)
        {
            if (valor <= 0)
            {
                return BadRequest("O valor de saque deve ser positivo.");
            }

            // Verifica se há saldo suficiente na conta para o saque
            if (conta.Saldo < valor)
            {
                return BadRequest("Saldo insuficiente para saque.");
            }

            conta.Saldo -= valor;
            await _context.SaveChangesAsync();

            return Ok();
        }
        else if(movimentacao.Tipo == TipoMovimentacao.Deposito)
        {
            if (valor <= 0)
            {
                return BadRequest("O valor de depósito deve ser positivo.");
            }

            conta.Saldo += valor;
            await _context.SaveChangesAsync();

            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}
