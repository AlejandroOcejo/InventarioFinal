namespace Inventario.Data;
using Inventario.Models;
public interface IProductRepository
{
        void AddProduct(Product product);
        Product GetProduct(string name);
        void UpdateProduct(Product product);
        void SaveChanges();
}