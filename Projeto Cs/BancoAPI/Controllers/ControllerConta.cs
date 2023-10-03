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

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Conta>>> GetContas()
    {
        if(_context is null) return NotFound();
        if(_context.Conta is null) return NotFound();
        return await _context.Conta.ToListAsync();
    }

    [HttpGet]
    [Route("buscar/{ContaID}")]
    public async Task<ActionResult<Conta>> GetConta(int contaID)
    {
        if(_context is null) return NotFound();
        if(_context.Conta is null) return NotFound();
        var conta = await _context.Conta.FindAsync(contaID);
        if (conta is null) return NotFound();
        return conta;
    }

    [HttpPut]
    [Route("alterar/{ContaID}")]
    public async Task<ActionResult> Alterar(int ContaID, Conta conta)
    {
        if(_context is null) return NotFound();
        if(_context.Conta is null) return NotFound();
        
        var contaExistente = await _context.Conta.FindAsync(ContaID);
        if(contaExistente is null) return NotFound();

        contaExistente.NumeroConta = conta.NumeroConta;
        contaExistente.TipoConta = conta.TipoConta;
        contaExistente.Saldo = conta.Saldo;
        contaExistente.DataAbertura = conta.DataAbertura;
        contaExistente.ClienteID = conta.ClienteID;
        contaExistente.AgenciaID = conta.AgenciaID;

        _context.Conta.Update(contaExistente);
        await _context.SaveChangesAsync();

        return Ok();
    }


    [HttpPost]
    [Route("criar")]
    public async Task<ActionResult<Conta>> Criar(Conta conta)
    {
        if(_context is null) return NotFound();
        if(_context.Conta is null) return NotFound();

        _context.Conta.Add(conta);
        await _context.SaveChangesAsync();

        return Created("", conta);
    }


    [HttpPost]
    [Route("depositar/{ContaID}")]
    public async Task<ActionResult> Depositar(int ContaID, decimal valor)
    {
        var conta = await _context.Conta.FindAsync(ContaID);
        if(conta == null) return NotFound();

        // Verifica se o valor de depósito é válido
        if (valor <= 0)
        {
            return BadRequest("O valor de depósito deve ser positivo.");
        }

        conta.Saldo += valor;
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    [Route("sacar/{ContaID}")]
    public async Task<ActionResult> Sacar(int ContaID, decimal valor)
    {
        var conta = await _context.Conta.FindAsync(ContaID);
        if(conta == null) return NotFound();

        // Verifica se o valor de saque é válido
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


    [HttpPost]
    [Route("AtualizarTipoConta/{ContaID}")]
    public async Task<ActionResult> AtualizarTipoConta(int ContaID, TipoConta novoTipo)
    {
        var conta = await _context.Conta.FindAsync(ContaID);
        if(conta == null) return NotFound();

        conta.TipoConta = novoTipo;
        await _context.SaveChangesAsync();

        return Ok();
    }
}
