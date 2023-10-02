//Mateus Bassi
using System.ComponentModel.DataAnnotations;
namespace BancoAPI.Models
{
    public class Transferencia
    {

        public int Id { get; set; }

        public decimal Valor { get; set; }
        public DateTime DataTransferencia { get; set; }

        // Chave estrangeira para a conta de origem
        public int ContaOrigemID { get; set; }
        public virtual Conta ContaOrigem { get; set; }  // "virtual" para carregar a entidade apenas quando são acessadas

        // Chave estrangeira para a conta de destino
        public int ContaDestinoID { get; set; }
        public virtual Conta ContaDestino { get; set; }

        public Transferencia() // Default
        {
            Valor = 0;
            // Date = "";
        }

    }
}
