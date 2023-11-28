namespace Inventario.Models;

using System.Dynamic;
using System.Text;
public class Product
{
public string Name {get; set;}

public decimal Price {get; set;}

public  string Type  {get;set;}

 public int Quantity
    {
        get
        {
            int quantity = 0;
            foreach (var item in ListMovs)
            {
                quantity += item.Quantity_Mov;
            }

            return quantity;
        }
    set{}
    }

public List<Movement> ListMovs {get; set;} = new List<Movement>();

public Movement addProd (string p_action, int p_quantity, DateTime p_date){
    if (p_quantity <= 0)
    {
        throw new ArgumentOutOfRangeException(nameof(p_quantity),"La cantidad tiene que ser positiva");
    }
    var insert= new Movement(p_action,p_quantity,p_date);
    ListMovs.Add(insert);
    return insert;
    
}
public Movement removeProd (string p_action, int p_quantity, DateTime p_date){
    if (p_quantity <= 0)
    {
        throw new ArgumentOutOfRangeException(nameof(p_quantity),"La cantidad tiene que ser positiva");
    }
    var removal= new Movement(p_action,-p_quantity,p_date);
    ListMovs.Add(removal);
    return removal;
    
}
public Product (){}
public Product(string p_Name, decimal p_Price, string p_Type, int p_Quantity, List<Movement> p_listMovs){
    Name = p_Name;
    Price = p_Price;
    Type = p_Type;
    addProd("Primera entrada",p_Quantity,DateTime.Now);
    ListMovs = p_listMovs;
}
public string ToString(){
    var toString = $"El producto {Name} de Type {Type} con Price {Price} que hay {Quantity}";
    return toString;
}
public void getHistory(){
    
}
}
