﻿namespace Inventario.Service;
using Inventario.Models;
using Inventario.Data;
public class ProductService : IProductService
{

    private IProductRepository Repository;
    public ProductService(IProductRepository repository)
    {
        Repository = repository;
    }
    public void CreateProduct(string p_name, decimal p_price, string p_type, int quantity, List<Movement> listMovs)
    {
        Product product = new Product(p_name, p_price, p_type, quantity, listMovs);
        Repository.AddProduct(product);
        Repository.SaveChanges();
    }
    public Product GetProduct(string name)
    {
        return Repository.GetProduct(name);
    }

    public List<Product> GetAllProducts()
    {
        return Repository.GetAllProducts();
    }
    public void UpdateProduct(Product product)
    {
        Repository.UpdateProduct(product);
    }

    public void SaveChanges()
    {
        Repository.SaveChanges();
    }
    public void AddProduct(string p_name, string p_action, decimal p_Price, int p_quantity)
    {
        try
        {
            var product = Repository.GetProduct(p_name);
            if (product == null)
            {
                product.addProd(p_action, p_quantity, p_Price, DateTime.Now);
                Repository.UpdateProduct(product);
                Repository.SaveChanges();
            }
            else
            {
                Console.WriteLine("El producto no se encontró en el repositorio.");
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
    public void UpdateProduct(string p_name, string p_action, decimal p_Price, int p_quantity)
    {
        try
        {
            var product = Repository.GetProduct(p_name);
            if (product == null)
            {
                product.updateProd(p_action, p_quantity, p_Price, DateTime.Now);
                Repository.UpdateProduct(product);
                Repository.SaveChanges();
            }
            else
            {
                Console.WriteLine("El producto no se encontró en el repositorio.");
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
    public void RemoveProduct(string p_name, string p_action, decimal p_Price, int p_quantity)
    {
        try
        {
            var product = Repository.GetProduct(p_name);
            product.removeProd(p_action, p_quantity, p_Price, DateTime.Now);
            Repository.UpdateProduct(product);
            Repository.SaveChanges();
        }
        catch (Exception exception)
        {
            Log error = new Log();
            error.WriteLog(exception);
            Console.WriteLine(exception.Message);
            throw;
        }

    }
    public string ProductHistory(string p_name)
    {
        var product = Repository.GetProduct(p_name);

        return product.getHistory();
    }

}
