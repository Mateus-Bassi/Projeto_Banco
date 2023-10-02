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

    // Construtor para inicializar o contexto do banco de dados
    public CartaoCreditoController(BancoDbContext context)
    {
        _context = context;
    }

    // Método HTTP GET para listar todos os cartões de crédito
    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<CartaoCredito>>> GetCartoes()
    {
        // Obtem todos os cartões de crédito assincronamente
        var cartoes = await _context.CartaoCredito.ToListAsync();

        // Verifica se não há cartões ou a lista está vazia
        if (cartoes == null || cartoes.Count == 0)
            return NotFound();  

        return cartoes;  

    // Método HTTP GET para buscar um cartão de crédito por ID
    [HttpGet]
    [Route("buscar/{CartaoID}")]
    public async Task<ActionResult<CartaoCredito>> GetCartao(int cartaoID)
    {
        // Busca o cartão de crédito com o ID fornecido
        var cartao = await _context.CartaoCredito.FindAsync(cartaoID);

        // Verifica se o cartão não foi encontrado
        if (cartao == null)
            return NotFound();  

        return cartao;  
    }

    // Método HTTP POST para criar um novo cartão de crédito
    [HttpPost]
    [Route("criar")]
    public async Task<ActionResult<CartaoCredito>> CriarCartao([FromBody] CartaoCredito cartao)
    {
        // Verifica se os dados do cartão são válidos
        if (cartao == null)
            return BadRequest("Dados inválidos para o cartão de crédito.");  

        // Simula a atribuição de um ID
        cartao.ID = _context.CartaoCredito.Count() + 1;

        // Adiciona o novo cartão de crédito ao contexto e salva no banco de dados
        _context.CartaoCredito.Add(cartao);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("DefaultApi", new { id = cartao.ID }, cartao);
    }
}
