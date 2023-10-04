using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        // verifica se o cartão for nulo se for retornar notfound 
        if (cartao == null)
            return NotFound();

        return cartao;
    }

    //HTTP POST para criar um novo cartão de crédito
    [HttpPost]
    [Route("criar_cartao")]
    public async Task<ActionResult<CartaoCredito>> CriarCartao(CartaoCredito cartao)
    {
        // verifica se a variavel context é nula se for indica que não há uma instancia valida
        if(_context is null) return NotFound();
        // verifica se a context do cartão for nula, se for não há cartões de crédito disponíveis no contexto do banco de dados
        if(_context.CartaoCredito is null) return NotFound();

        if (cartao == null) return BadRequest("Dados inválidos para o cartão de crédito.");
        
        _context.CartaoCredito.Add(cartao);
        await _context.SaveChangesAsync();

        cartao.CartaoID = _context.CartaoCredito.Count() + 1;
        _context.CartaoCredito.Add(cartao);
        await _context.SaveChangesAsync();

        return Created("",cartao);
        
    } 
    //HTTP POST para aumentar o limite
    [HttpPost]
    [Route("AumentarLimite")]
    public async Task<ActionResult<CartaoCredito>> AumentarLimite(int cartaoID, decimal Limite)
    {
        var cartao = await _context.CartaoCredito.FindAsync(cartaoID);
        //Verifica se cartão for null o cartão nao é encontrado
        if (cartao == null)
            return NotFound("Cartão de crédito não encontrado.");
    
    // Aumenta o limite do cartão de crédito conforme o valor fornecido
        cartao.Limite += Limite;
    
    // Marca o estado do cartão como modificado no contexto
        _context.Entry(cartao).State = EntityState.Modified;
    
    // Salva as alterações no banco de dados
        await _context.SaveChangesAsync();

    //Retorna que o limite foi aumentado com sucesso
        return Ok("Limite aumentado com sucesso.");
    }
    //HTTP POST para bloquear e desbloquear o cartão de crédito 
    [HttpPost]
    [Route("Bloqueado")]
   public async Task<ActionResult<CartaoCredito>> Bloqueado(int cartaoID, bool bloquear)
    {
        var cartao = await _context.CartaoCredito.FindAsync(cartaoID);

        if (cartao == null) return NotFound("Cartão de crédito não encontrado.");

        cartao.Bloqueado = bloquear;

        _context.Entry(cartao).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        string status = bloquear ? "bloqueado" : "desbloqueado";
        return Ok($"O cartão foi {status} com sucesso.");
}
}
