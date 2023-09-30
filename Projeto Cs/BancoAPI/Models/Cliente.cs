//Mateus Bassi
using System.ComponentModel.DataAnnotations;
namespace BancoAPI.Models;
public class Cliente
{
    // Campo privado para armazenar o ID gerado
    private int _id;
    // Propriedade somente leitura para o ID
    public int Id
    {
        get { return _id; }
    }
    // Campos publicos
    public string Name { get; set; }
    public string CPF { get; set; }
    public string DataNascimento { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public virtual ICollection<Conta> Contas { get; set; } // Adicionado por William (me chama kkkk)
    public ICollection<Endereco> Enderecos { get; set; } // Adicionado por William (me chama kkkk)
    public ICollection<ClienteEndereco> ClienteEnderecos { get; set; } // Adicionado por William (me chama kkkk)
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
