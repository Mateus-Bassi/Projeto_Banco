using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class MovimentacaoController : ControllerBase
{
    private readonly BancoDbContext _context;

    public MovimentacaoController(BancoDbContext context)
    {
        _context = context;
    }

}
