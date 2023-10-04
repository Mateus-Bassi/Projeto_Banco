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

    [HttpGet]
    [Route("buscar/{SeguroID}")]
    public async Task<ActionResult<Seguro>> GetSeguro(int seguroID)
    {
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
        if(_context is null) return NotFound();
        if(_context.Seguro is null) return NotFound();
        
        var v_seguro = await _context.Seguro.FindAsync(seguroID);
        if(v_seguro is null) return NotFound();

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
    [Route("detalhes/{SeguroID}")]
    public async Task<ActionResult<Seguro>> Detalhes(int seguroID)
    {
        if(_context is null) return NotFound();
        if(_context.Seguro is null) return NotFound();

        // Aqui, estamos incluindo o Cliente relacionado ao seguro para fornecer detalhes adicionais.
        var v_seguro = await _context.Seguro.FindAsync(seguroID);
        if (v_seguro is null) return NotFound();
        
        return v_seguro;
    }

    [HttpGet]
    [Route("alterarValorCoberto/{SeguroID}")]
    public async Task<ActionResult<Seguro>> AlterarValorCoberto(int seguroID, decimal novoValor)
    {
        if(_context is null) return NotFound();
        if(_context.Seguro is null) return NotFound();

        // Aqui, estamos incluindo o Cliente relacionado ao seguro para fornecer detalhes adicionais.
        var v_seguro = await _context.Seguro.FindAsync(seguroID);
        if (v_seguro is null) return NotFound();
        
        v_seguro.ValorCoberto = novoValor;
        _context.Seguro.Update(v_seguro);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("deletar/{SeguroID}")]
    public async Task<ActionResult> Deletar(int seguroID)
    {
        var v_seguro = await _context.Seguro.FindAsync(seguroID);
        if (v_seguro is null) return NotFound();

        _context.Seguro.Remove(v_seguro);

        await _context.SaveChangesAsync();
        return Ok();
    }
}
