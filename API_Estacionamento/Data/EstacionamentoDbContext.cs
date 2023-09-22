using API_Estacionamento.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Estacionamento.Data;
public class EstacionamentoDbContext : DbContext
{
    public DbSet<Carro>? Carro { get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=estacionamento.db;Cache=Shared");
    }

}