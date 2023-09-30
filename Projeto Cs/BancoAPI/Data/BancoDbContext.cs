using BancoAPI.Models;  // ou o namespace onde seus modelos estão
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Data
{
    public class BancoDbContext : DbContext
    {
        // Tabelas
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Conta> Conta { get; set; }
        public DbSet<Agencia> Agencia { get; set; }
        public DbSet<Movimentacao> Movimentacoe { get; set; }
        public DbSet<Transferencia> Transferencia { get; set; }
        public DbSet<Emprestimo> Emprestimo { get; set; }
        public DbSet<CartaoCredito> CartaoCredito { get; set; }
        public DbSet<Investimento> Investimento { get; set; }
        public DbSet<Seguro> Seguro { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=banco.db;Cache=Shared");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento Cliente - Endereco
            modelBuilder.Entity<Cliente>()
                .HasOne(cliente => cliente.Endereco) // Especifica que Cliente tem relação com Endereço
                .WithOne(endereco => endereco.Cliente) // Faz a especificação inversa, ou seja, do Endereço com o Cliente
                .HasForeignKey<Endereco>(endereco => endereco.ClienteID); // Especifica que a relação é representada por uma chave estrangeira.

            // Relacionamento Cliente - Conta
            modelBuilder.Entity<Cliente>()
                .HasMany(cliente => cliente.Conta) // Especifica que Cliente tem uma relação com Conta (Um cliente para varias contas)
                .WithOne(conta => conta.Cliente) // Faz a espeficicação da relação da Conta com o Cliente (Uma conta para um cliente)
                .HasForeignKey<Conta>(conta => conta.ClienteID); // Chave estrangeira

            modelBuilder.Entity<Agencia>()
                .HasMany(agencia => agencia.Conta)
                .WithOne(conta => conta.Agencia)
                .HasForeignKey<Agencia>(conta => conta.ContaID)

            // Relacionamento Cliente Agencia
            modelBuilder.Entity<Conta>()
                .HasOne(c => c.Agencia)
                .WithMany()  // assumindo que uma Agência pode ter várias Contas
                .HasForeignKey(c => c.AgenciaID);

            // Relacionamento Movimentacao - Conta
            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Conta)
                .WithMany()  // assumindo que uma Conta pode ter várias Movimentações
                .HasForeignKey(m => m.ContaID);

            // Relacionamento Transferencia - Conta (ContaOrigem e ContaDestino)
            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.ContaOrigem)
                .WithMany()  // assumindo que uma Conta pode originar várias Transferências
                .HasForeignKey(t => t.ContaOrigemID);

            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.ContaDestino)
                .WithMany()  // assumindo que uma Conta pode ser destino de várias Transferências
                .HasForeignKey(t => t.ContaDestinoID);

            // Relacionamento Emprestimo - Conta
            modelBuilder.Entity<Emprestimo>()
                .HasOne(e => e.Conta)
                .WithMany()  // assumindo que uma Conta pode ter vários Empréstimos
                .HasForeignKey(e => e.ContaID);

            // Relacionamento CartaoCredito - Conta
            modelBuilder.Entity<CartaoCredito>()
                .HasOne(cc => cc.Conta)
                .WithMany()  // assumindo que uma Conta pode ter vários Cartões de Crédito
                .HasForeignKey(cc => cc.ContaID);

            // Relacionamento Investimento - Conta
            modelBuilder.Entity<Investimento>()
                .HasOne(i => i.Conta)
                .WithMany()  // assumindo que uma Conta pode ter vários Investimentos
                .HasForeignKey(i => i.ContaID);

            // Relacionamento Seguro - Cliente
            modelBuilder.Entity<Seguro>()
                .HasOne(s => s.Cliente)
                .WithMany()  // assumindo que um Cliente pode ter vários Seguros
                .HasForeignKey(s => s.ClienteID);

            // Relacionamento Agencia - Endereco
            modelBuilder.Entity<Agencia>()
                .HasOne(a => a.Endereco)
                .WithOne()  // assumindo que um Endereço pertence a uma única Agência
                .OnDelete(DeleteBehavior.Cascade); // O endereço será excluído se a agência for excluída
        }

    }
}
