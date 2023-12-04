namespace Inventario.Data;
using Inventario.Models;
using System.Text.Json;
using System.Linq;
public class ProductRepository : IProductRepository
{
    public Dictionary<string, Product> products = new Dictionary<string, Product>();

    private string jsonFile = "Product.json";

    public ProductRepository()
    {
        LoadAccounts();
    }
    public void CreateProduct(string p_name, decimal p_price, string p_type, int p_quantity, List<Movement> listMovs)
    {
        Product product = new Product(p_name, p_price, p_type, p_quantity, listMovs);
        AddProduct(product);
    }

    public void AddProduct(Product product)
    {
        products[product.Name] = product;
    }

    public Product GetProduct(string name)
    {
        return products.TryGetValue(name, out var product) ? product : null;
    }
    public void UpdateProduct(Product product)
    {
        products[product.Name] = product;
    }
    public List<Product> GetAllProducts()
    {
        return products.Values.ToList();
    }
    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(products.Values, options);
            File.WriteAllText(jsonFile, jsonString);
        }
        catch (Exception exception)
        {
            Log error = new Log();
            error.WriteLog(exception);
            Console.WriteLine(exception.Message);
            throw;
        }

    }
    public void LoadAccounts()
    {
        try
        {
            if (File.Exists(jsonFile))
            {
                string jsonString = File.ReadAllText(jsonFile);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var f_products = JsonSerializer.Deserialize<IEnumerable<Product>>(jsonString, options);

                if (f_products != null)
                {
                    products = f_products.ToDictionary(d_prod => d_prod.Name);
                }
            }
        }
        catch (Exception exception)
        {
            Log error = new Log();
            error.WriteLog(exception);
            Console.WriteLine(exception.Message);
            throw;
        }

    }

}
