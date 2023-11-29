namespace Inventario.Presentation;
using Spectre.Console;

using System;
using Inventario.Service;
using Inventario.Data;
using Microsoft.Extensions.DependencyInjection;

public static class Menu
{
    public static void Main()
    {
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

    public static string DisplayMenu()
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Menú de Opciones")
                .PageSize(6)
                .AddChoices(new[] { "Añadir Producto", "Eliminar Producto", "Modificar Producto", "Buscar por Nombre", "Mostrar todos los productos", "Salir" }));

        return selection;
    }

    public static void ProcessSelection(string selection)
    {
        switch (selection)
        {
            case "Añadir Producto":
                Console.WriteLine("Escribe el nombre del producto que quiera añadir: ");
                string name = Console.ReadLine();
                string action = "Entrada";
                Console.WriteLine("Escribe la cantidad del producto que quiera añadir: ");
                string quantity = Console.ReadLine();
                int number;
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddTransient<IProductService, ProductService>();
                serviceCollection.AddSingleton<IProductRepository, ProductRepository>();

                var serviceProvider = serviceCollection.BuildServiceProvider();
                var productService = serviceProvider.GetService<IProductService>();
                if (int.TryParse(quantity, out number))
                {


                    productService.AddProduct(name, action, number);
                }

                break;
            case "Eliminar Producto":
                Console.WriteLine("2");

                break;
            case "Modificar Producto":
                Console.WriteLine("3");

                break;
            case "Buscar por Nombre":
                Console.WriteLine("4");

                break;
            case "Mostrar todos los productos":
                Console.WriteLine("5");

                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }
}
