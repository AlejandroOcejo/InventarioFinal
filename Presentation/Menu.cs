

namespace Inventario.Presentation;
using Spectre.Console;

using System;
using Inventario.Service;
using Inventario.Data;
using Inventario.Models;
using Microsoft.Extensions.DependencyInjection;
public static class Menu
{

    private static IProductService productService;

    public static void Main()
    {
        ConfigureServices();

        while (true)
        {
            var selection = DisplayMenu();

            if (selection == "Salir")
            {
                Console.WriteLine("Saliendo del programa");
                break;
            }

            ProcessSelection(selection);
        }
    }

    private static void ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IProductService, ProductService>();
        serviceCollection.AddSingleton<IProductRepository, ProductRepository>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        productService = serviceProvider.GetService<IProductService>();
    }

    public static string DisplayMenu()
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Menú de Opciones")
                .PageSize(6)
                .AddChoices(new[] { "Crear Producto", "Añadir Producto", "Borrar Producto", "Buscar por Nombre", "Mostrar todos los productos", "Salir" }));

        return selection;
    }

    public static void ProcessSelection(string selection)
    {
        switch (selection)
        {
            case "Crear Producto":
                Console.WriteLine("Escribe el nombre del producto que quiera añadir: ");
                string name = Console.ReadLine();
                Console.WriteLine("Escribe el precio del producto que quiera añadir: ");
                string price = Console.ReadLine();
                Console.WriteLine("Escribe el tipo del producto que quiera añadir: ");
                string type = Console.ReadLine();
                Console.WriteLine("Escribe la cantidad del producto que quiera añadir: ");
                string quantity = Console.ReadLine();
                decimal nprice;
                int nquantity;
                List<Movement> listMovs = new List<Movement>();

                if (decimal.TryParse(price, out nprice) && int.TryParse(quantity, out nquantity))
                {
                    productService.CreateProduct(name, nprice, type, nquantity, listMovs);
                    Console.WriteLine($"Producto '{name}' creado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Entrada no válida para precio o cantidad.");
                }
                break;

            case "Añadir Producto":
                AddOrRemoveProduct("añadir");
                break;

            case "Borrar Producto":
                AddOrRemoveProduct("borrar");
                break;


            case "Buscar por Nombre":
                GetProduct();
                break;

            case "Mostrar todos los productos":
                Console.WriteLine("5");
                break;

            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }
    private static void AddOrRemoveProduct(string action)
    {
        Console.WriteLine($"Escribe el nombre del producto al que quiera {action}: ");
        string productName = Console.ReadLine();

        var product = productService.GetProduct(productName);

        if (product != null)
        {
            if (action == "añadir")
            {
                Console.WriteLine("Escribe la cantidad que quiera añadir al producto: ");
                string quantityInput = Console.ReadLine();

                if (int.TryParse(quantityInput, out int quantity))
                {
                    product.addProd("Entrada adicional", quantity, DateTime.Now);
                }
                else
                {
                    Console.WriteLine("Cantidad no válida.");
                    return;
                }
            }
            else if (action == "borrar")
            {
                Console.WriteLine("Escribe la cantidad que quiera quitar al producto: ");
                string quantityInput = Console.ReadLine();

                if (int.TryParse(quantityInput, out int quantity))
                {
                    product.removeProd("Productos Restados", quantity, DateTime.Now);
                }
                else
                {
                    Console.WriteLine("Cantidad no válida.");
                    return;
                }
            }


            UpdateAndSaveProduct(product);

            Console.WriteLine($"Se {action}on al producto {productName}.");
        }
        else
        {
            Console.WriteLine($"No se encontró el producto con el nombre {productName}.");
        }
    }
    public static void GetProduct()
    {
        Console.WriteLine($"Escribe el nombre del producto que quiera buscar: ");
        string productName = Console.ReadLine();
         var product = productService.GetProduct(productName);
        Console.WriteLine(product.ToString());

    }
    private static void UpdateAndSaveProduct(Product product)
    {
        productService.UpdateProduct(product);
        productService.SaveChanges();
    }
}
