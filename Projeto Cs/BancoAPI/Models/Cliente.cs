//Mateus Bassi
using System.ComponentModel.DataAnnotations;
namespace BancoAPI.Models;
public class Cliente
{
    // Modificado por william (me chama)
    public int Id { get; set; }

    public string Name { get; set; }
    public string CPF { get; set; }
    public string DataNascimento { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public virtual ICollection<Conta> Contas { get; set; } // Adicionado por William (me chama kkkk)
    public virtual ICollection<Endereco> Enderecos { get; set; } // Adicionado por William (me chama kkkk)
    public virtual ICollection<ClienteEndereco> ClienteEnderecos { get; set; } // Adicionado por William (me chama kkkk)
    public virtual ICollection<Seguro> Seguros { get; set; }    // Adicionado por William (me chama kkkk)

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
