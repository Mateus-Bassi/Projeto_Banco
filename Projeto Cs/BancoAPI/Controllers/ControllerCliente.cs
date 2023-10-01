using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BancoAPI.Models;

namespace BancoAPI.Controllers;

public class ControllerCliente : ControllerBase
{

    private BancoDbContext? _context;


    public ControllerCliente (BancoDbContext)
    {
        
    }

}