// William
using BancoAPI.Models;
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
        public DbSet<Movimentacao> Movimentacao { get; set; }
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
                .HasMany(cliente => cliente.Contas) // Especifica que Cliente tem uma relação com Conta (Um cliente para varias contas)
                .WithOne(conta => conta.Cliente) // Faz a espeficicação da relação da Conta com o Cliente (Uma conta para um cliente)
                .HasForeignKey(conta => conta.ClienteID); // Chave estrangeira

            // Relacionamento Agencia - Conta (UM PARA MUITOS)
            modelBuilder.Entity<Agencia>()
                .HasMany(agencia => agencia.Contas)
                .WithOne(conta => conta.Agencia)
                .HasForeignKey(conta => conta.AgenciaID);

            // Relacionamento Agencia - Endereço (UM PRA UM)
            modelBuilder.Entity<Agencia>()
                .HasOne(agencia => agencia.Endereco)
                .WithOne(endereco => endereco.Agencia)
                .HasForeignKey<Agencia>(agencia => agencia.EnderecoID); // DEVEMOS TER UM CAMPO EnderecoID NO ENDEREÇO!!!

            // Relacionamento Cliente - Seguro (UM PARA MUITOS)
            modelBuilder.Entity<Cliente>()
                .HasMany(cliente => cliente.Seguros)
                .WithOne(seguro => seguro.Cliente)
                .HasForeignKey(seguro => seguro.ClienteID);

            // Relacionamento Conta para Transferencia de Origem (UM PARA MUITOS)
            // modelBuilder.Entity<Conta>()
            //     .HasMany(conta => conta.TransferenciasOrigem)
            //     .WithOne(transferencia => transferencia.ContaOrigem)
            //     .HasForeignKey(transferencia => transferencia.ContaOrigemID);

            // Relacionamento de Conta para Transferências de Destino (UM PARA MUITOS)
            modelBuilder.Entity<Conta>()
                .HasMany(conta => conta.TransferenciasDestino)
                .WithOne(transferencia => transferencia.ContaDestino)
                .HasForeignKey(transferencia => transferencia.ContaDestinoID);

            // Relacionamento de Conta com Emprestimo (UM PARA MUITOS)
            modelBuilder.Entity<Conta>()
                .HasMany(conta => conta.Emprestimos) // Uma conta pode ter várias contas (faz referencia à coleção de emprestimos na class Conta)
                .WithOne(emprestimo => emprestimo.Conta) // Um empréstimo é relacionado à uma conta apenas
                .HasForeignKey(emprestimo => emprestimo.ContaID); // A chave estrangeira na class Emprestimo vai ser a ContaID

            // Relacionamento de Conta com CartaoCredito (UM PARA MUITOS)
            modelBuilder.Entity<Conta>()
                .HasMany(conta => conta.CartoesCredito)
                .WithOne(cartao => cartao.Conta)
                .HasForeignKey(cartao => cartao.ContaID);

            // Relacionamento de Conta com Investimento (UM PARA MUITOS)
            modelBuilder.Entity<Conta>()
                .HasMany(conta => conta.Investimentos)
                .WithOne(investimento => investimento.Conta)
                .HasForeignKey(investimento => investimento.ContaID);

            // Relacionamento de Conta com Movimentacoes (UM PARA MUITOS)
            modelBuilder.Entity<Conta>()
                .HasMany(conta => conta.Movimentacoes)
                .WithOne(movimentacao => movimentacao.Conta)
                .HasForeignKey(movimentacao => movimentacao.ContaID);
        }
    }
}
