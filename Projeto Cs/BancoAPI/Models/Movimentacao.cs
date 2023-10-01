using System;
namespace BancoAPI.Models
{
    public enum TipoMovimentacao
    {
        Saque,
        Deposito,
        Transferencia
    }

    public class Movimentacao
    {
        // Chave primária
        public int MovimentacaoID { get; set; }
        
        public TipoMovimentacao Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataMovimentacao { get; set; }

        // Chave estrangeira
        public int ContaID { get; set; }

        // Propriedade de navegação
        public virtual Conta Conta { get; set; }

        // Construtor com parâmetros
        public Movimentacao(int id, TipoMovimentacao tipo, decimal valor, DateTime dataMovimentacao, int contaID)
        {
            MovimentacaoID = id;
            Tipo = tipo;
            Valor = valor;
            DataMovimentacao = dataMovimentacao;
            ContaID = contaID;
        }

        // Construtor sem parâmetros
        // Protected para garantir que não seja chamado externamente (é usado pelo EF apenas)
        protected Movimentacao()
        {
        }
    }
}
