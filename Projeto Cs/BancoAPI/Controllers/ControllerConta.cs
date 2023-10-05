using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        // Retorna uma lista das contas que estão no nosso Banco de Dados
        if(_context is null) return NotFound();
        if(_context.Conta is null) return NotFound();
        return await _context.Conta.ToListAsync();
    }

    [HttpGet]
    [Route("buscar/{ContaID}")]
    public async Task<ActionResult<Conta>> GetConta(int contaID)
    {
        // Retorna uma conta especificada pelo ID
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
        // Altera a Conta especificada pelo ID
        // Recebe um novo objeto Conta com os atributos "novos"
        if(_context is null) return NotFound();
        if(_context.Conta is null) return NotFound();
        
        var contaExistente = await _context.Conta.FindAsync(ContaID);
        if(contaExistente is null) return NotFound();

        // Atribuindo para a Conta especificada os atributos do objeto conta
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
        // Criando uma conta e colocando no banco de dados
        if(_context is null) return NotFound();
        if(_context.Conta is null) return NotFound();

        _context.Conta.Add(conta);
        await _context.SaveChangesAsync();

        return Created("", conta);
    }


    [HttpPost]
    [Route("AtualizarTipoConta/{ContaID}")]
    public async Task<ActionResult> AtualizarTipoConta(int ContaID, TipoConta novoTipo)
    {
        // Atualizando o tipo da conta especificada pelo ID
        // Recebe também o tipo da Conta no parâmetro
        var conta = await _context.Conta.FindAsync(ContaID);
        if(conta == null) return NotFound();

        conta.TipoConta = novoTipo;
        await _context.SaveChangesAsync();

        return Ok();
    }
}
