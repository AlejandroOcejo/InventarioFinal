namespace Inventario.Models;

using System.Text;
public class Movement{
    public string Action { get; set; }
    public int Quantity_Mov { get; set; }

    public decimal Price {get; set;}
    public DateTime Date { get; set; }
public Movement()
    {
    }
public Movement(string p_Action, int p_Quantity_mov,decimal p_price, DateTime p_Date){
    Action = p_Action;
    Quantity_Mov = p_Quantity_mov;
    Price = p_price;
    Date = p_Date;
}
}