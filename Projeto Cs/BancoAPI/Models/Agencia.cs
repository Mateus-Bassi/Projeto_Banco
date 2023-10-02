// Juan 
using System;
using System.Collections.Generic;

// Modificado por WIllima (me chama kkkk)


namespace BancoAPI.Models
{
    public class Agencia
    {
        // Chave Primária
        public int AgenciaID { get; set; }

        public string NumeroAgencia { get; set; }
        public string Nome { get; set; }
        
        // Relacionamento com Endereco
        public int EnderecoID { get; set; }  // Chave estrangeira
        public virtual Endereco Endereco { get; set; }  // Propriedade de navegação

        public ICollection<Conta> Contas { get; set; } 

        // Construtor com os parâmetros
        public Agencia(int id, string numeroAgencia, string nome, int enderecoID)
        {
            AgenciaID = id;
            NumeroAgencia = numeroAgencia;
            Nome = nome;
            EnderecoID = enderecoID;
        }
    }
}

