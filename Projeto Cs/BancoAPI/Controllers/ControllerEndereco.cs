using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class ControllerEndereco : ControllerBase
{
    private readonly BancoDbContext _context;

    public ControllerEndereco(BancoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Endereco>>> Get()
    {
        if (_context is null || _context.Endereco is null)
        {
            return BadRequest("Contexto ou Endereco não encontrado");
        }

        var enderecos = await _context.Endereco.ToListAsync();
        return Ok(enderecos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Endereco>> Get(int id)
    {
        if (_context is null || _context.Endereco is null)
        {
            return BadRequest("Contexto ou Endereco não encontrado");
        }

        var endereco = await _context.Endereco.FindAsync(id);
        if (endereco is null)
        {
            return NotFound();
        }

        return endereco;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(Endereco endereco)
    {
        if (_context is null || _context.Endereco is null)
        {
            return BadRequest("Contexto ou Endereco não encontrado");
        }

            _context.Endereco.Add(endereco);
    await _context.SaveChangesAsync();

    return Created("", endereco);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, Endereco endereco)
    {
        // Atribuindo para o Endereco especifico os atributos do objeto endereco
        if(_context is null) return NotFound();
        if(_context.Endereco is null) return NotFound();
    
        var enderecoExistente = await _context.Endereco.FindAsync(id);
        if(enderecoExistente is null) return NotFound();

    
        enderecoExistente.Rua = endereco.Rua;
        enderecoExistente.Numero = endereco.Numero;
        enderecoExistente.Bairro = endereco.Bairro;
        enderecoExistente.Cidade = endereco.Cidade;
        enderecoExistente.Estado = endereco.Estado;
        enderecoExistente.CEP = endereco.CEP;

        _context.Endereco.Update(enderecoExistente);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [Route("deletar/{EnderecoID}")]
    public async Task<ActionResult> Deletar(int enderecoID)
    {   // deleta o endereco especificado pelo ID
        var v_endereco = await _context.Endereco.FindAsync(enderecoID);
        if (v_endereco is null) return NotFound();

        _context.Endereco.Remove(v_endereco);

        await _context.SaveChangesAsync();
        return Ok();

    }
}

