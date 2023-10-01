// William
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
            // Configuração da tabela intermediária ClienteEndereco
            modelBuilder.Entity<ClienteEndereco>()
                // Cria um objeti anônimo com duas propriedades. Essas propriedades em conjunto formam a chave primária de ClienteEndereco
                .HasKey(ce => new { ce.ClienteID, ce.EnderecoID }); // chave composta

            // Relacionamento ClienteEndereco - Cliente (MUITOS PARA MUITOS)
            modelBuilder.Entity<ClienteEndereco>()
                .HasOne(ce => ce.Cliente)
                .WithMany(c => c.ClienteEnderecos)
                .HasForeignKey(ce => ce.ClienteID);

            // Relacionamento ClienteEndereco - Endereco (MUITOS PARA MUITOS)
            modelBuilder.Entity<ClienteEndereco>()
                .HasOne(ce => ce.Endereco)
                .WithMany(e => e.ClienteEnderecos)
                .HasForeignKey(ce => ce.EnderecoID);



            // Relacionamento Cliente - Conta (UM PARA MUITOS)
            modelBuilder.Entity<Cliente>()
                .HasMany(cliente => cliente.Conta) // Especifica que Cliente tem uma relação com Conta (Um cliente para varias contas)
                .WithOne(conta => conta.Cliente) // Faz a espeficicação da relação da Conta com o Cliente (Uma conta para um cliente)
                .HasForeignKey<Conta>(conta => conta.ClienteID); // Chave estrangeira

            // Relacionamento Agencia - Conta (UM PARA MUITOS)
            modelBuilder.Entity<Agencia>()
                .HasMany(agencia => agencia.Conta)
                .WithOne(conta => conta.Agencia)
                .HasForeignKey<Conta>(conta => conta.AgenciaID);

            // Relacionamento Agencia - Endereço (UM PRA UM)
            modelBuilder.Entity<Endereco>()
                .HasOne(endereco => endereco.Agencia)
                .WithOne(agencia => agencia.Endereco)
                .HasForeignKey<Agencia>(agencia => agencia.CEP); // DEVEMOS TER UM CAMPO CEP NO ENDEREÇO!!!

            // Relacionamento Cliente - Seguro (UM PARA MUTIOS)
            modelBuilder.Entity<Cliente>()
                .HasMany(cliente => cliente.Seguro)
                .WithOne(seguro => seguro.Cliente)
                .HasForeignKey<Seguro>(seguro => seguro.ClienteID);

            // Relacionamento Conta para Transferencia de Origem (UM PARA MUITOS)
            modelBuilder.Entity<Conta>()
                .HasMany(conta => conta.TransferenciaOrigem)
                .WithOne(transferencia => transferencia.ContaOrigem)
                .HasForeignKey<Transferencia>(transferencia => transferencia.ContaOrigemID);

            // Relacionamento de Conta para Transferências de Destino (UM PARA MUITOS)
            modelBuilder.Entity<Conta>()
                .HasMany(conta => conta.TransferenciaDestino)
                .WithOne(transferencia => transferencia.ContaDestino)
                .HasForeignKey<Transferencia>(transferencia => transferencia.ContaDestinoID);

            // Relacionamento de Conta com Emprestimo (UM PARA MUITOS)
            modelBuilder.Entity<Conta>()
                .HasMany(conta => conta.Emprestimos) // Uma conta pode ter várias contas (faz referencia à coleção de emprestimos na class Conta)
                .WithOne(emprestimo => emprestimo.Conta) // Um empréstimo é relacionado à uma conta apenas
                .HasForeignKey<Emprestimo>(emprestimo => emprestimo.ContaID); // A chave estrangeira na class Emprestimo vai ser a ContaID

            // Relacionamento de Conta com CartaoCredito (UM PARA MUITOS)
            modelBuilder.Entity<Conta>()
                .HasMany(conta => conta.CartoesCredito)
                .WithOne(cartao => cartao.Conta)
                .HasForeignKey<CartaoCredito>(cartao => cartao.ContaID);

            // Relacionamento de Conta com Investimento (UM PARA MUITOS)
            modelBuilder.Entity<Conta>()
                .HasMany(conta => conta.Investimentos)
                .WithOne(investimento => investimento.Conta)
                .HasForeignKey<Investimento>(investimento => investimento.ContaID);

            modelBuilder.Entity<Conta>()
                .HasMany(conta => conta.Movimentacao)
                .WithOne(movimentacao => movimentacao.Conta)
                .HasForeignKey<Movimentacao>(movimentacao => movimentacao.ContaID);
        }
    }
}
