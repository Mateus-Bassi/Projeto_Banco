using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class InvestimentoController : ControllerBase
{
    // Variável para armazenar o contexto do banco de dados
    private readonly BancoDbContext _context;

    // Construtor da classe que aceita o contexto do banco de dados como parâmetro
    public InvestimentoController(BancoDbContext context)
    {
        // Atribui o contexto do banco de dados à _context
        _context = context;
    }

    // HTTP GET para listar todos os investimentos
    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Investimento>>> GetInvestimentos()
    {
        // Aguarda a obtenção da lista de investimentos de forma assíncrona do banco de dados
        var investimentos = await _context.Investimento.ToListAsync();

        // Verifica se não foram encontrados investimentos ou se a lista está vazia 
        if (investimentos == null || investimentos.Count == 0)
            // Retorna não encontrado 
            return NotFound();

        // Caso contrário, retorna a lista de investimentos
        return investimentos;
    }

    // HTTP GET para buscar um investimento por ID
    [HttpGet]
    [Route("buscar/{InvestimentoID}")]
    public async Task<ActionResult<Investimento>> GetInvestimento(int investimentoID)
    {
        // Esta linha de código está realizando uma operação assíncrona para buscar um investimento no banco de dados com base no investimentoID. 
        // Permitindo que o programa aguarde a conclusão antes de prosseguir.
        var investimento = await _context.Investimento.FindAsync(investimentoID);

        // Verifica se o investimento for nulo, se for retorna notfound 
        if (investimento == null)
            return NotFound();

        // Caso contrário, retorna o investimento
        return investimento;
    }

    // HTTP POST para criar um novo investimento
    [HttpPost]
    [Route("criar")]
    public async Task<ActionResult<Investimento>> CriarInvestimento(Investimento investimento)
    {
        // Verifica se a variável investimento é nula, indicando dados inválidos
        if (investimento == null)
            return BadRequest("Dados inválidos para o investimento.");

        // Define a taxa do investimento com base no tipo
        if (investimento.Tipo == TipoInvestimento.CDB)
        {
            investimento.Taxa = 0.05;  // Exemplo de taxa para CDB
        }
        else if (investimento.Tipo == TipoInvestimento.TesouroDireto)
        {
            investimento.Taxa = 0.03;  // Exemplo de taxa para Tesouro Direto
        }
        else if (investimento.Tipo == TipoInvestimento.FundoImobiliario)
        {
            investimento.Taxa = 0.08;  // Exemplo de taxa para Fundo Imobiliário
        }
        else
        {
            return BadRequest("Tipo de investimento não localizado.");
        }

        // Adiciona o investimento ao banco de dados 
        _context.Investimento.Add(investimento);

        // Salva de forma assíncrona o banco de dados do investimento inserindo efetivamente 
        await _context.SaveChangesAsync();

        // Retorna uma resposta que indica que foi criado o investimento 
        return Created("", investimento);
    }
}