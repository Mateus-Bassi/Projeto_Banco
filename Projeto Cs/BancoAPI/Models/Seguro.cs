using System;
using System.Collections.Generic;

namespace BancoAPI.Models
{
    public enum TipoSeguro
    {
        Vida,
        Residencial,
        Veiculo
    }

    public class Seguro
    {
        // Chave primária
        public int SeguroID { get; set; }
    
        public TipoSeguro Tipo { get; set; }
        public decimal ValorCoberto { get; set; }
        public decimal Premio { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        // Chave Estrangeira
        public int ClienteID { get; set; }

        // Propriedade de navegação para Cliente
        public virtual Cliente Cliente { get; set; }

        // Construtor com parâmetros
        public Seguro(int seguroID, TipoSeguro tipo, decimal valorCoberto, decimal premio, DateTime dataInicio, DateTime dataFim, int clienteID)
        {
            SeguroID = seguroID;
            Tipo = tipo;
            ValorCoberto = valorCoberto;
            Premio = premio;
            DataInicio = dataInicio;
            DataFim = dataFim;
            ClienteID = clienteID;
        }

        // Construtor sem parâmetros
        // Protected para garantir que não seja chamado externamente (é usado pelo EF apenas)
        protected Seguro()
        {
        }
    }
}
