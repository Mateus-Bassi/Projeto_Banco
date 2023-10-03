using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CartaoCreditoController : ControllerBase
{
    private readonly BancoDbContext _context;

    public CartaoCreditoController(BancoDbContext context)
    {
        _context = context;
    }

    //HTTP GET para listar todos os cartões de crédito
    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<CartaoCredito>>> GetCartoes()
    {
        var cartoes = await _context.CartaoCredito.ToListAsync();

        if (cartoes == null || cartoes.Count == 0)
            return NotFound();

        return cartoes;
    }

    // HTTP GET para buscar um cartão de crédito por ID
    [HttpGet]
    [Route("buscar/{CartaoID}")]
    public async Task<ActionResult<CartaoCredito>> GetCartao(int cartaoID)
    {
        var cartao = await _context.CartaoCredito.FindAsync(cartaoID);

        if (cartao == null)
            return NotFound();

        return cartao;
    }

    //HTTP POST para criar um novo cartão de crédito
    [HttpPost]
    [Route("criar_cartao")]
    public async Task<ActionResult<CartaoCredito>> CriarCartao(CartaoCredito cartao)
    {
        if(_context is null) return NotFound();
        if(_context.CartaoCredito is null) return NotFound();

        if (cartao == null) return BadRequest("Dados inválidos para o cartão de crédito.");
        
        _context.CartaoCredito.Add(cartao);
        await _context.SaveChangesAsync();

        cartao.CartaoID = _context.CartaoCredito.Count() + 1;
        _context.CartaoCredito.Add(cartao);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("buscar", new {CartaoID = cartao.CartaoID}, cartao);
        
    }
    [HttpPost]
    [Route("AumentarLimite")]
    public async Task<ActionResult<Investimento>> AumentarLimite(decimal Limite)
    {
        
    }
}
