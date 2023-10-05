using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CartaoCreditoController : ControllerBase
{
    // Variável para armazenar o contexto do banco de dados
    private readonly BancoDbContext _context;

    // Construtor da classe que aceita o contexto do banco de dados como parâmetro
    public CartaoCreditoController(BancoDbContext context)
    {
        // Atribui o contexto do banco de dados à _context
        _context = context;
    }

    //HTTP GET para listar todos os cartões de crédito
    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<CartaoCredito>>> GetCartoes()
    {
        // Aguarda a obtenção da lista de cartões de crédito de forma assíncrona do banco de dados
        var cartoes = await _context.CartaoCredito.ToListAsync();
        
        //Verifica se não foram encontrados nenhum cartão ou se a lista está vazia 
        if (cartoes == null || cartoes.Count == 0)
            //Retorna nao encontrado 
            return NotFound(); 

        //Caso contrario retorna a lista de cartões
        return cartoes;
    }

    // HTTP GET para buscar um cartão de crédito por ID
    [HttpGet]
    [Route("buscar/{CartaoID}")]
    public async Task<ActionResult<CartaoCredito>> GetCartao(int cartaoID)
    {
        //Esta linha de código está realizando uma operação assíncrona para buscar um cartão de crédito no banco de dados com base no cartaoID. 
        //Permitindo que o programa aguarde a conclusão antes de prosseguir.
        var cartao = await _context.CartaoCredito.FindAsync(cartaoID);

        // verifica se o cartão for nulo se for retornar notfound 
        if (cartao == null)
            return NotFound();

        //Caso contrario retorna o cartão de crédito
        return cartao;
    }

    //HTTP POST para criar um novo cartão de crédito
    [HttpPost]
    [Route("criar_cartao")]
    public async Task<ActionResult<CartaoCredito>> CriarCartao(CartaoCredito cartao)
    {
        // Verifica se a variavel context é nula se for indica que não há uma instancia valida
        if(_context is null) return NotFound();

        // Verifica se a context do cartão for nula, se for não há cartões de crédito disponíveis no contexto do banco de dados
        if(_context.CartaoCredito is null) return NotFound();

        // Se verificar e for null retorna BadRequest mostrando ao usuario que os dados para criar o cartão foram invalidados 
        if (cartao == null) return BadRequest("Dados inválidos para o cartão de crédito.");

        //Caso de certo nessa linha de codigo adiciona o cartão ao banco de dados 
        _context.CartaoCredito.Add(cartao);

        //Aqui salvamos de forma assincrona o banco de dados do cartão inserindo efetivamente 
        await _context.SaveChangesAsync();

        //Definimos o CartaoID como objeto do cartao e incrementamos o número de cartões existentes no banco e adicionamos 1 para obter um novo ID único

        cartao.CartaoID = _context.CartaoCredito.Count() + 1;
        
        //Adiciona o objeto cartões a colecao de cartões do banco de dados
        _context.CartaoCredito.Add(cartao);

        //Com o await, o programa aguarda até que a operação SaveChangesAsync() seja concluída e as alterações sejam salvas no banco de dados antes de prosseguir. 
        await _context.SaveChangesAsync();

        //Retorna uma resposta que indica que foi criado o cartão de credito 
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
        // Procura o cartão pelo cartãoID
        var cartao = await _context.CartaoCredito.FindAsync(cartaoID);

            //Com o await o programa aguarda até que a operação FindAsync seja concluída e o resultado esteja disponível antes de prosseguir

        if (cartao == null) return NotFound("Cartão de crédito não encontrado.");
        
        //Aqui atualiza o cartão para bloqueado ou desbloqueado
        cartao.Bloqueado = bloquear;

        // Marca o cartão como modificado
        _context.Entry(cartao).State = EntityState.Modified;

        //Salva as alterações do estado do cartão 
        await _context.SaveChangesAsync();

            //Com o await, o programa aguarda até que a operação SaveChangesAsync() seja concluída e as alterações sejam salvas no banco de dados antes de prosseguir. 

        //Essa string gera uma mensagem se o cartão foi bloqueado ou desbloqueado para o usuario
        string status = bloquear ? "bloqueado" : "desbloqueado";

        //Retorna uma resposta se o cartão foi bloqueado ou desbloqueado com sucesso  
        return Ok($"O cartão foi {status} com sucesso.");
}
}
