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

        //Parametros especificos para aumentar o limite, Bloquear cartão ou Desbloquear
        public void Aumentar_Limite(decimal Novo_Limite)
        {
            Limite += Novo_Limite;           
        }
        public void Bloquear()
        {
            Bloqueado = true;
        }
        public void Desbloquear()
        {
            Bloqueado = false;
        }
        public CartaoCredito(int cartaoID, string num_cartao, DateTime data_val, string codigo_seg, decimal limite, bool bloqueado, int contaid)
        //Declaração do construtor com os parametros
        {
            CartaoID = cartaoID;
            Numero_Cartao = num_cartao;
            Data_Validade = data_val;
            Codigo_Seguranca = codigo_seg;
            Limite = limite;
            Bloqueado = bloqueado;
            ContaID = contaid;
        }        
    }
}
