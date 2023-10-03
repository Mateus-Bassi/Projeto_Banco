//Mateus Bassi
using System.ComponentModel.DataAnnotations;
namespace BancoAPI.Models;
public class Cliente
{
    public int id { get; set; }

    public string Name { get; set; }
    public string CPF { get; set; }
    public string DataNascimento { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public virtual ICollection<Conta> Contas { get; set; } 
    public virtual ICollection<Endereco> Enderecos { get; set; } 
    public virtual ICollection<ClienteEndereco> ClienteEnderecos { get; set; } 
    public virtual ICollection<Seguro> Seguros { get; set; }    

    // 
    public Cliente() // Default 
    {
        Name = "";
        CPF = "";
        DataNascimento = "";
        Email = "";
        Telefone = "";
    }
}
