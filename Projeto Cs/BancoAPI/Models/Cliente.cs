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
    // 
}