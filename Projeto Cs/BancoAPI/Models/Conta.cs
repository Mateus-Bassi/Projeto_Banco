// William
using System;
using System.Collections.Generic;

namespace BancoAPI.Models
{
    public enum TipoConta
    {
        Poupanca,
        Corrente,
        Salario
    }

    public class Conta
    {
        // Chave Primária
        public int ContaID { get; set; }

        public string NumeroConta { get; set; }
        public TipoConta TipoConta { get; set; }  // Tipo de conta é um ENUM
        public decimal Saldo { get; set; }
        public DateTime DataAbertura { get; set; }

        // Chaves estrangeiras
        public int ClienteID { get; set; } 
        public int AgenciaID { get; set; }

        // Propriedades de navegação
        // Utilizamos dessa maneira em relaçõoes MUITOS PARA UM
        public virtual Cliente Cliente { get; set; }  // Navegação para a entidade Cliente
        public virtual Agencia Agencia { get; set; }  // Navegação para a entidade Agencia

        // Utilizamos dessa maneira em relações UM PARA MUITOS
        public ICollection<Transferencia> TransferenciaOrigem { get; set; } // Transferências onde esta conta é a origem
        public ICollection<Transferencia> TransferenciaDestino { get; set; } // Transferências onde esta conta é o destino
        public ICollection<Emprestimo> Emprestimos { get; set; } // Emprestimos que são vinculados à essa conta
        public ICollection<CartaoCredito> CartoesCredito { get; set; } ;// Cartões de crédito que são vinculados à essa conta
        public ICollection<Investimento> Investimentos { get; set; } // Investimentos que são vinculados à essa conta
        public ICollection<Movimentacao> Movimentacoes { get; set; } // Investimentos que são vinculados à essa conta


        // Construtor padrão, sem parâmetros
            public Conta()
            {
                TransferenciaOrigem = new List<Transferencia>();
                TransferenciaDestino = new List<Transferencia>();
                Emprestimos = new List<Emprestimo>();
                CartoesCredito = new List<CartaoCredito>();
                Investimentos = new List<Investimento>();
            }

            // Construtor com os parâmetros
            public Conta(int id, string numeroConta, TipoConta tipoConta, decimal saldo, DateTime dataAbertura, int clienteID, int agenciaID)
            {
                Id = id;
                NumeroConta = numeroConta;
                TipoConta = tipoConta;
                Saldo = saldo;
                DataAbertura = dataAbertura;
                ClienteID = clienteID;
                AgenciaID = agenciaID;

                // Inicializando coleções
                TransferenciaOrigem = new List<Transferencia>();
                TransferenciaDestino = new List<Transferencia>();
                Emprestimos = new List<Emprestimo>();
                CartoesCredito = new List<CartaoCredito>();
                Investimentos = new List<Investimento>();
            }
    }
}
