// William

// Tabela intermediária que permite que um endereço tenha muitos clientes e vice-versa
using System;
namespace BancoAPI.Models;


public class ClienteEndereco
{
    public int ClienteID { get; set; }
    public Cliente Cliente { get; set; }

    public int EnderecoID { get; set; }
    public Endereco Endereco { get; set; }
}
