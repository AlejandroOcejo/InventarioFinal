namespace Inventario.Service;
using Inventario.Models;

public interface IProductService
{
    void CreateProduct(string p_name, decimal p_price, string p_type, int p_quantity, List<Movement> listMovs);
    void AddProduct(string name, string action, int quantity);
    void RemoveProduct(string name, string action, int quantity);
    Product GetProduct(string name);
    void UpdateProduct(Product product);
    void SaveChanges();

    List<Product> GetAllProducts();
    string ProductHistory(string prodName);
}