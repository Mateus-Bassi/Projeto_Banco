//Vinicius
using System;
using System.Collections.Generic;

namespace BancoAPI.Models
{
    public class CartaoCredito
    {   
        //Chave primaria da classe
        public int CartaoID {get; set;}

        public string Numero_Cartao {get; set;}
        public DateTime Data_Validade {get; set;}
        public string Codigo_Seguranca {get; set;}
        public decimal Limite {get; set;}
        public bool Bloqueado {get; set;}

        //Chave estrangeira para associar uma conta ao cartão 
        public int ContaID {get; set;}
        //Permite que a classe Cartão navegue pela classe Conta
        public Conta Conta {get; set;}

        public CartaoCredito(int cartaoID, string numcartao, DateTime dataval, string codigoseg, decimal limite, bool bloqueado, int contaid)
        //Declaração do construtor com os parametros
        {
            CartaoID = cartaoID;
            Numero_Cartao = numcartao;
            Data_Validade = dataval;
            Codigo_Seguranca = codigoseg;
            Limite = limite;
            Bloqueado = bloqueado;
            ContaID = contaid;
        }        
    }
}
