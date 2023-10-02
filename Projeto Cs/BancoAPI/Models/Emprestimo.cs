//Mateus Bassi
namespace BancoAPI.Models;

public class Emprestimo
{

    private int tx_juros{get;} // taxa de juros
    
    public int Valor_soli{ get; set; } // valor solicitaod
    public string Data_soli{ get; set; } // data da solicitacao
    public int n_parcelas{ get; set; }// numero de parcela
    
    public int ContaID { get; set; } // Add por William

    public virtual Conta Conta { get; set; }

    public Emprestimo() // Default
    {
        tx_juros = 20;
        Valor_soli = 0;
        Data_soli="";
        n_parcelas= 0;

    }
}
