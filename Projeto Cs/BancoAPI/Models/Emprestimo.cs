//Mateus Bassi
namespace BancoAPI.Models;

public class Emprestimo
{

    public double tx_juros{get;} // taxa de juros
    
    public int id { get; set; }
    public double Valor_soli{ get; set; } // valor solicitaod
    public string Data_soli{ get; set; } // data da solicitacao
    public int n_parcelas{ get; set; }// numero de parcela

    public double valor_pagar{ get; set; }// numero de parcela
    
    public int ContaID { get; set; } // Add por William

    public virtual Conta Conta { get; set; }

    public Emprestimo() // Default
    {
        id = Conta.ContaID;
        tx_juros = 0.2;
        Valor_soli = 0;
        Data_soli="";
        n_parcelas= 10;
        valor_pagar = Valor_soli * tx_juros;
    }




}
