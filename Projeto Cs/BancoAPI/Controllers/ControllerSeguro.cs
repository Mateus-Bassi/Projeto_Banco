using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Controller do Seguro

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
        // Retorna uma lista dos Seguros presente no Banco de Dados
        if(_context is null) return NotFound();
        if(_context.Seguro is null) return NotFound();
        return await _context.Seguro.ToListAsync();
    }

    [HttpGet]
    [Route("buscar/{SeguroID}")]
    public async Task<ActionResult<Seguro>> GetSeguro(int seguroID)
    {
        // Retorna um seguro especificado pelo ID
        if(_context is null) return NotFound();
        if(_context.Seguro is null) return NotFound();
        var v_seguro = await _context.Seguro.FindAsync(seguroID);
        if (v_seguro is null) return NotFound();
        return v_seguro;
    }


    [HttpPut]
    [Route("alterar/{SeguroID}")]
    public async Task<ActionResult> Alterar(int seguroID, Seguro seguro)
    {
        // Altera um seguro especificado pelo ID
        // Recebe um objeto Seguro que irá substituir o seguro com o ID enviado
        if(_context is null) return NotFound();
        if(_context.Seguro is null) return NotFound();
        
        var v_seguro = await _context.Seguro.FindAsync(seguroID);
        if(v_seguro is null) return NotFound();

        // Atribuindo os novos atributos
        v_seguro.Tipo = seguro.Tipo;
        v_seguro.ValorCoberto = seguro.ValorCoberto;
        v_seguro.Premio = seguro.Premio;
        v_seguro.DataInicio = seguro.DataInicio;
        v_seguro.DataFim = seguro.DataFim;
        v_seguro.ClienteID = seguro.ClienteID;

        _context.Seguro.Update(v_seguro);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    [Route("Renovar/{SeguroID}")]
    public async Task<ActionResult> Renovar(int seguroID, DateTime dataFim)
    {
        // Renova determinado seguro, mudando sua data de FIM
        if(_context is null) return NotFound();
        if(_context.Seguro is null) return NotFound();
        
        var v_seguro = await _context.Seguro.FindAsync(seguroID);
        if(v_seguro is null) return NotFound();

        v_seguro.DataFim = dataFim;

        _context.Seguro.Update(v_seguro);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    [Route("alterarValorCoberto/{SeguroID}")]
    public async Task<ActionResult<Seguro>> AlterarValorCoberto(int seguroID, decimal novoValor)
    {
        // Altera o valor de cobertura de um seguro especificado pelo ID
        if(_context is null) return NotFound();
        if(_context.Seguro is null) return NotFound();

        var v_seguro = await _context.Seguro.FindAsync(seguroID);
        if (v_seguro is null) return NotFound();
        
        // Mudando o ValorCoberto
        v_seguro.ValorCoberto = novoValor;
        _context.Seguro.Update(v_seguro);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("deletar/{SeguroID}")]
    public async Task<ActionResult> Deletar(int seguroID)
    {
        // Excluindo determinado seguro pelo seu ID
        var v_seguro = await _context.Seguro.FindAsync(seguroID);
        if (v_seguro is null) return NotFound();

        _context.Seguro.Remove(v_seguro);

        await _context.SaveChangesAsync();
        return Ok();
    }
}
