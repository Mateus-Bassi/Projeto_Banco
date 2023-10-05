//Vinicius 
using System;
using System.Collections.Generic;
using BancoAPI.Models;

namespace BancoAPI.Models
{
    //Declaração do enum dos investimentos especificos
    public enum TipoInvestimento
    {
        CDB,
        Tesouro_Direto,
        Fundo_Imobiliario
    }
    public class Investimento
    {
        //Chave primária
        public int InvestimentoID {get; set;}

        public TipoInvestimento Tipo {get; set;}
        public decimal ValorInicial {get; set;}
        public DateTime DataInvestimento {get; set;}
        public decimal Rentabilidade_Mensal {get; set;}
        public DateTime DataResgate {get; set;}

        //Chave estrangeira para associar uma conta ao investimento 
        public int ContaID {get; set;}
        //Permite que a classe Investimento navegue pela classe Conta
        public virtual Conta Conta {get; set;}

        //Construtor da Classe
        public Investimento(int investimentoID, TipoInvestimento tipo, decimal valorinicial, DateTime datainvestimento, decimal rentabilidademensal, DateTime dataregaste, int contaID)
        //Declaração do construtor com os parametros
        {
            InvestimentoID = investimentoID;
            Tipo = tipo;
            ValorInicial = valorinicial;
            DataInvestimento = datainvestimento;
            Rentabilidade_Mensal = rentabilidademensal;
            DataResgate = dataregaste;
            ContaID = contaID;
        }
    }
}

