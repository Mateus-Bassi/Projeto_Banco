using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Estacionamento.Controllers;

[ApiController]
[Route("[controller]")]
public class CarroController : ControllerBase
{
    private EstacionamentoDbContext _context;
    public CarroController(EstacionamentoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Carro>>> Listar()
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Carro is null) return NotFound();
        return await _dbContext.Carro.ToListAsync();
    }
    
    /*[HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Carro>>> Listar()
    {
        if (_context.Carro is null) return NotFound();
        return await _context.Carro.ToListAsync();
    }
    */

    [HttpGet]
    [Route("buscar/{placa}")]

    public async Task<ActionResult<Carro>> Buscar(string placa)
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Carro is null) return NotFound();
        var carro = await _context.Carro.FindAsync(placa);
        if (_context.Carro is null) return NotFound();
        return carro;
    }
    [HttpPost]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(Carro carro)
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Carro is null) return NotFound();
        await _context.AddAsync(carro);
        await _context.SaveChangesAsync();
        return Created("", carro);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Carro carro)
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Carro is null) return NotFound();
//        var carro = await _dbContext.FindAsync(carro.Placa);
//        if(carro is null) return NotFound();
        _dbContext.Carro.Update(carro);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
    
    /*[HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar(Carro carro)
    {
        _context.Carro.Update(carro);
        await _context.SaveChangesAsync();
        return Ok();
    }
    */
    [HttpDelete]
    [Route("excluir/{placa}")]
    public async Task<IActionResult> Excluir(string placa)
    {
        var carro = await _context.Carro.FindAsync(placa);
        if (carro is null) return NotFound();
        _context.Carro.Remove(carro);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("modificardescricao/{placa}")]
    public async Task<IActionResult> ModificarDescricao(string placa, [FromForm] string descricao)
    {
        var carro = await _context.Carro.FindAsync(placa);
        if (carro is null) return NotFound();
        carro.Descricao = descricao;
        await _context.SaveChangesAsync();
        return Ok();
    }
}
