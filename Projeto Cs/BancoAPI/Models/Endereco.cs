// Coloca EndereçoID como chave primaria, ao inves de CEP
using System;
namespace BancoAPI.Models{
    public class Endereco
    {
        public int EnderecoID { get; set; }

        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        public virtual Agencia Agencia { get; set; }  // Navegação para a entidade Agencia
        public ICollection<ClienteEndereco> ClienteEnderecos { get; set; } 

        // Construtor para inicialização opcional
        public Endereco(int enderecoID,string rua, string numero, string bairro, string cidade, string estado, string cep)
        {
            EnderecoID = enderecoID;
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
        }
    }
}
