using BancoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Data;
public class BancoDbContext : DbContext
{
    public DbSet<Cliente>? Cliente { get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlite("DataSource=banco.db;Cache=Shared");
    }

}