//Vinicius

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using BancoAPI.Models;

namespace BancoAPI.Models
{
    //Declaração do enum dos investimentos especificos
    public enum TipoInvestimento
    {
        CDB,
        TesouroDireto,
        FundoImobiliario
    }
    public class Investimento
    {
        //Chave primária
        public int InvestimentoID {get; set;}

        public TipoInvestimento Tipo {get; set;}
        public float ValorInicial {get; set;}
        public DateTime DataInvestimento {get; set;}
        public float Rentabilidade_Mensal {get; set;}
        public DateTime DataResgate {get; set;}
        public double Taxa {get; set;}

        //Chave estrangeira para associar uma conta ao investimento 
        public int ContaID {get; set;}
        //Permite que a classe Investimento navegue pela classe Conta
        public virtual Conta Conta {get; set;}

        //Construtor da Classe
        public Investimento(int investimentoID, TipoInvestimento tipo, DateTime datainvestimento, float rentabilidademensal, DateTime dataregaste, int contaID, float valorinicial = 0,  double taxa = 0 )
        //Declaração do construtor com os parametros
        {
            InvestimentoID = investimentoID;
            Tipo = tipo;
            ValorInicial = valorinicial;
            DataInvestimento = datainvestimento;
            Rentabilidade_Mensal = rentabilidademensal;
            DataResgate = dataregaste;
            ContaID = contaID;
            Taxa = taxa;
        }
    }
}

