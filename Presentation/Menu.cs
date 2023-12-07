

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
    private static string productName;
    private static string productType;
    private static string productPrice;
    private static string ProductQuantity;

    private static string movementAction;
    private static string movementQuantity;
    private static string movementPrice;
    private static string movementDate;

    private static string movementDateDay;
    private static string movementDateTime;
    private static List<string> productNameList = new List<string> { };
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
                .PageSize(8)
                .AddChoices(new[] { "Crear Producto", "Añadir Producto", "Borrar Producto", "Buscar por Nombre", "Modificar Precio", "Mostrar todos los productos", "Mostrar Lista de movimientos", "Test", "Salir" }));

        return selection;
    }

    public static void ProcessSelection(string selection)
    {
        switch (selection)
        {
            case "Crear Producto":
                CreateProduct();
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

            case "Modificar Precio":
                UpdateProduct("Modificar Precio");
                break;

            case "Mostrar todos los productos":
                ShowAllProducts();
                break;

            case "Mostrar Lista de movimientos":
                ShowMovements();
                break;
            case "Test":
                productChoiceMenu();
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }

    public static void CreateProduct()
    {
        try
        {
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
        }
        catch (Exception exception)
        {
            Log error = new Log();
            error.WriteLog(exception);
            Console.WriteLine(exception.Message);
            throw;
        }

    }
    private static void AddOrRemoveProduct(string action)
    {
        try
        {
            Console.WriteLine($"Selecciona el producto al que quiera {action}: ");
            string productName = productChoiceMenu();

            var product = productService.GetProduct(productName);

            if (product != null)
            {
                if (action == "añadir")
                {
                    Console.WriteLine("Escribe la cantidad que quiera añadir al producto: ");
                    string quantityInput = Console.ReadLine();

                    if (int.TryParse(quantityInput, out int quantity))
                    {
                        product.addProd("Entrada adicional", quantity, product.Price, DateTime.Now);
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
                        product.removeProd("Productos Restados", quantity, product.Price, DateTime.Now);
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
        catch (Exception exception)
        {
            Log error = new Log();
            error.WriteLog(exception);
            Console.WriteLine(exception.Message);
            throw;
        }

    }
    public static void GetProduct()
    {
        try
        {
            string productName = productChoiceMenu();
            var product = productService.GetProduct(productName);
            var productString = product.ToString();
            var table = new Table();
            table.AddColumn(new TableColumn("Nombre").Centered());
            table.AddColumn(new TableColumn("Tipo").Centered());
            table.AddColumn(new TableColumn("Precio").Centered());
            table.AddColumn(new TableColumn("Cantidad").Centered());
            string[] splits = productString.Split(',');

            productName = splits[0];
            productType = splits[1];
            productPrice = splits[2];
            ProductQuantity = splits[3];

            table.AddRow(productName, productType, productPrice, ProductQuantity);
            AnsiConsole.Write(table);
        }
        catch (Exception exception)
        {
            Log error = new Log();
            error.WriteLog(exception);
            Console.WriteLine(exception.Message);
            throw;
        }

    }
    private static void UpdateAndSaveProduct(Product product)
    {
        productService.UpdateProduct(product);
        productService.SaveChanges();
    }

    private static void ShowAllProducts()
    {
        try
        {
            var table = new Table();
            var product = productService.GetAllProducts();


            table.AddColumn(new TableColumn("Nombre").Centered());
            table.AddColumn(new TableColumn("Tipo").Centered());
            table.AddColumn(new TableColumn("Precio").Centered());
            table.AddColumn(new TableColumn("Cantidad").Centered());
            foreach (var item in product)
            {
                var productString = item.ToString();
                string[] splits = productString.Split(',');

                productName = splits[0];
                productType = splits[1];
                productPrice = splits[2];
                ProductQuantity = splits[3];
                table.AddRow(productName, productType, productPrice, ProductQuantity);
            }


            AnsiConsole.Write(table);

        }


        catch (Exception exception)
        {
            Log error = new Log();
            error.WriteLog(exception);
            Console.WriteLine(exception.Message);
            throw;
        }
    }
    private static void UpdateProduct(string action)
    {
        try
        {
            Console.WriteLine($"Selecciona el producto al que quiera {action}: ");
            string productName = productChoiceMenu();

            var product = productService.GetProduct(productName);

            if (product != null)
            {
                if (action == "Modificar Precio")
                {
                    Console.WriteLine("Escriba el nuevo precio: ");
                    string priceInput = Console.ReadLine();

                    if (decimal.TryParse(priceInput, out decimal p_price))
                    {
                        product.updateProd("Cambio Precio", 0, p_price, DateTime.Now);
                    }
                    else
                    {
                        Console.WriteLine("Cantidad no válida.");
                        return;
                    }
                }

                UpdateAndSaveProduct(product);

                Console.WriteLine($"{action} del producto {productName}.");
            }
            else
            {
                Console.WriteLine($"No se encontró el producto con el nombre {productName}.");
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

    private static void ShowMovements()
    {
        Console.WriteLine($"Selecciona el producto del que quiera ver movimientos: ");

        string productName = productChoiceMenu();
        var product = productService.GetProduct(productName);

        var table = new Table();
        table.AddColumn(new TableColumn("Acción").Centered());
        table.AddColumn(new TableColumn("Cantidad").Centered());
        table.AddColumn(new TableColumn("Precio").Centered());
        table.AddColumn(new TableColumn("Fecha").Centered());
        table.AddColumn(new TableColumn("Hora").Centered());

        foreach (var movement in product.ListMovs)
        {
            var productString = movement.ToString();
            string[] splits = productString.Split(',');

            movementAction = splits[0];
            movementQuantity = splits[1];
            movementPrice = splits[2];
            movementDate = splits[3];

            string[] timeSplit = movementDate.Split(' ');
            movementDateDay = timeSplit[1];
            movementDateTime = timeSplit[2];

            table.AddRow(movementAction, movementQuantity, movementPrice, movementDateDay, movementDateTime);

        }
        AnsiConsole.Write(table);
    }


    private static string productChoiceMenu()
    {
        productNameList = new List<string>();
        var products = productService.GetAllProducts();
        foreach (var item in products)
        {
            var productString = item.ToString();
            string[] splits = productString.Split(',');
            productName = splits[0];
            productNameList.Add(productName);

        }
        var test = productNameList.Count;
        var selection = AnsiConsole.Prompt(

            new SelectionPrompt<string>()
                .Title("Lista de productos")
                .PageSize(Math.Max(productNameList.Count, 3))
                .AddChoices(productNameList.ToArray()));
        return selection;
    }
}