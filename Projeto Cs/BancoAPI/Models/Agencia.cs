// Juan 
using System;
using System.Collections.Generic;


namespace BancoAPI.Models
{
    public enum TipoAgencia
    {
        NumeroAgencia,
        Nome,
        Endereco
    }

    public class Agencia
    {
        // Chave Primária
        public int AgenciaID { get; set; }

        public string NumeroAgencia { get; set; }
        public TipoAgencia TipoAgencia { get; set; }  // Tipo de agencia é um ENUM
        public string Nome { get; set; }
        public string Endereco { get; set; }

        // Chaves estrangeiras
        public int ClienteID { get; set; } // resolver se precisa aqui
        public int AgenciaID { get; set; } // resolver se precisa aqui 

        // Propriedades de navegação
        // Utilizamos dessa maneira em relaçõoes MUITOS PARA UM
        public virtual Cliente Cliente { get; set; }  // Navegação para a entidade Cliente
        public virtual Agencia Agencia { get; set; }  // Navegação para a entidade Agencia

            

            // Construtor com os parâmetros
            public Agencia(int id, string numeroAgencia, string nome, string endereco, TipoAgencia tipoAgencia)
            {
                AgenciaID = id;
                NumeroAgencia = numeroAgencia;
                Nome = nome;
                TipoAgencia = tipoAgencia;
                Endereco = endereco;
               

                
    }
}
}
