using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ControllerTranferencia : ControllerBase
{
    private readonly BancoDbContext _context;

    public ControllerTranferencia(BancoDbContext context)
    {
        _context = context;
    }
    [HttpGet()]
    [Route("buscar/{Valor}")] // Busca o Valor da transferencia 
    public async Task<ActionResult<Transferencia>> Buscar([FromRoute] double Valor)
    {
        if(_context.Transferencia is null)
            return NotFound();
        var transferencia = await _context.Transferencia.FindAsync(Valor);
        if (transferencia is null)
            return NotFound();
        return transferencia;
    }
    [HttpPost]
    [Route("Nova Transferencia/ {ContaDestinoId}")]
    public IActionResult Cadastrar(Transferencia Valor)
    {
        _context.Add(Valor);
        _context.SaveChanges();
        return Created("", Valor);
    } 
}