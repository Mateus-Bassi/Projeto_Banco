using System.ComponentModel.DataAnnotations;

namespace API_Estacionamento.Models;
public class Carro
{
    [Key]
    public string? Placa { get; set; }
    public string? Descricao { get; set; }
}