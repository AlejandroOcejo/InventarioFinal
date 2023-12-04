namespace Inventario.Data;
using Inventario.Models;

public interface IProductRepository
{
        void CreateProduct(string p_name, decimal p_price, string p_type, int p_quantity, List<Movement> listMovs);
        void AddProduct(Product product);
        Product GetProduct(string name);
        void UpdateProduct(Product product);
        List<Product> GetAllProducts();
        void SaveChanges();
}