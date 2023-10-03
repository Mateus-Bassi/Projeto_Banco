using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ControllerEmprestimo : ControllerBase
{
    private readonly BancoDbContext _context;

    public ControllerEmprestimo(BancoDbContext context)
    {
        _context = context;
    }

    [HttpGet()] // emprestimo por data busca
    [Route("Buscar/{Data_soli}")] // Busca a data do emprestimo
    public async Task<ActionResult<Emprestimo>> Buscar([FromRoute] string Data_soli)
    {
        if(_context.Emprestimo is null)
            return NotFound();
        var emprestimo = await _context.Emprestimo.FindAsync(Data_soli);
        if (emprestimo is null)
            return NotFound();
        return emprestimo;
    }
    [HttpGet()] // emprestimo por id busca
    [Route("Buscar/{id}")] // Busca a data do emprestimo
    public async Task<ActionResult<Emprestimo>> Search([FromRoute] int id)
    {
        if(_context.Emprestimo is null)
            return NotFound();
        var emprestimo = await _context.Emprestimo.FindAsync(id);
        if (emprestimo is null)
            return NotFound();
        return emprestimo;
    }
    [HttpPost] // Criacao de novo emprestimo
    [Route("Solicitar Emprestimo/ Valor")]
    public IActionResult Cadastrar(Emprestimo emprestimo)
    { 
        emprestimo.valor_pagar = emprestimo.Valor_soli * emprestimo.tx_juros;
        _context.Add(emprestimo);
        _context.SaveChanges();
        return Created("", emprestimo);
    }   
}


