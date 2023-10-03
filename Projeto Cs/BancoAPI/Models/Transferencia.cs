//Mateus Bassi
using System.ComponentModel.DataAnnotations;
namespace BancoAPI.Models
{
    public class Transferencia
    {

        public int id { get; set; }

        public double Valor { get; set; }
        public DateTime DataTransferencia { get; set; }

        // Chave estrangeira para a conta de destino
        public int ContaDestinoID { get; set; }
        public virtual Conta ContaDestino { get; set; }

        public Transferencia() // Default
        {
            ContaDestino = null;
            Valor = 0;
            DataTransferencia = DateTime.MinValue;
        }

    }
}
