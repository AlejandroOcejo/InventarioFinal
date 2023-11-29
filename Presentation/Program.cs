using System.Collections.Generic;
using Inventario.Models;
List<Movement> Lista= new List<Movement>();
Product Zapatilla = new Product("Zapa",50,"Calzado",20,Lista);

Console.WriteLine(Zapatilla.ToString());
