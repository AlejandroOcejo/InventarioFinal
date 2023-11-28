namespace Inventario.Models;

using System.Text;
public class Movement{
public string Action {get;}
public int Quantity_Mov {get;}
public DateTime Date {get;}

public Movement(string p_Action, int p_Quantity_mov, DateTime p_Date){
    Action = p_Action;
    Quantity_Mov = p_Quantity_mov;
    Date = p_Date;
}
}