namespace Inventario.Models;

using System.Dynamic;
using System.Text;
public class Product
{
    public string Name { get; set; }

      public decimal Price
    {
        get
        {
            decimal price = 0;
            foreach (var item in ListMovs)
            {
                price = item.Price;
            }

            return price;
        }
        set { }
    }

    public string Type { get; set; }

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
        set { }
    }

    public List<Movement> ListMovs { get; set; } = new List<Movement>();

    public Movement addProd(string p_action, int p_quantity, decimal p_price, DateTime p_date)
    {
        if (p_quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(p_quantity), "La cantidad tiene que ser positiva");
        }
        var insert = new Movement(p_action, p_quantity, p_price, p_date);
        ListMovs.Add(insert);
        return insert;

    }
    public Movement updateProd(string p_action, int p_quantity, decimal p_price, DateTime p_date)
    {
        if (p_price <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(p_price), "El precio tiene que ser mayor que 0");
        }
        var insert = new Movement(p_action, p_quantity, p_price, p_date);
        ListMovs.Add(insert);
        return insert;
    }

    public Movement removeProd(string p_action, int p_quantity, decimal p_price, DateTime p_date)
    {
        if (p_quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(p_quantity), "La cantidad tiene que ser positiva");
        }
        var removal = new Movement(p_action, -p_quantity, p_price, p_date);
        ListMovs.Add(removal);
        return removal;

    }
    public Product() { }
    public Product(string p_Name, decimal p_Price, string p_Type, int p_Quantity, List<Movement> p_listMovs)
    {
        Name = p_Name;
        Price = p_Price;
        Type = p_Type;
        Quantity = p_Quantity;
        ListMovs = p_listMovs ?? new List<Movement>();
        addProd("Primera entrada", p_Quantity, p_Price, DateTime.Now);
    }
    public string ToString()
    {
        var toString = $"{Name}, {Type}, {Price}, {Quantity}";
        return toString;
    }
    public string getHistory()
    {
        var sample = "sample";
        return sample;
    }
}
