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

        // Indica que um novo recurso foi criado. Inclui um cabeçalho que aponta para o novo recurso.
        // nameof método de ação que lida com a obtenção de detalhes de uma única conta
        // new id especifica os valores de rota para o método de ação (o ASAP irá usar essas informações para construir a URL)
        // conta é o corpo da resposta, está retornando os detalhes da conta recém criada.
        return CreatedAtAction(nameof(GetConta), new { id = conta.ContaID }, conta);
    }


    [HttpPost]
    [Route("depositar/{ContaID}")]
    public async Task<ActionResult> Depositar(int ContaID, decimal valor)
    {
        var conta = await _context.Conta.FindAsync(ContaID);
        if(conta == null) return NotFound();

        conta.Saldo += valor;

        _context.Entry(conta).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok();
    }
}
