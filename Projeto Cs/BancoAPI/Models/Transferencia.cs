//Mateus Bassi
using System.ComponentModel.DataAnnotations;
namespace BancoAPI.Models;

public class Tranferencia
{

    // id do Cliente
    // Conta que vai tranferir 
    //Conta que vai receber 
    public int Valor{get; set;}
    public string Date{get; set;}

    public Tranferencia() // Default
    {
        Valor = 0;
        Date = "";
    }

}