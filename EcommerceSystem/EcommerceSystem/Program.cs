using System;
using System.Collections.Generic;
using System.Linq;

// Product class
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

// Order class
public class Order
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public Order(int id, int productId, int quantity)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
    }
}

// E-commerce App
public class EcommerceApp
{
    public void Run()
    {
        // Seed products
        var products = new List<Product>
        {
            new Product(1, "Laptop", 5000m),
            new Product(2, "Smartphone", 3000m),
            new Product(3, "Headphones", 500m)
        };

        // Seed orders
        var orders = new List<Order>
        {
            new Order(1, 1, 2),  // 2 Laptops
            new Order(2, 2, 5),  // 5 Smartphones
            new Order(3, 3, 10)  // 10 Headphones
        };

        // Join products with orders and calculate total sales
        var salesReport = from order in orders
                          join product in products on order.ProductId equals product.Id
                          select new
                          {
                              ProductName = product.Name,
                              QuantitySold = order.Quantity,
                              TotalSales = order.Quantity * product.Price
                          };

        // Display report
        Console.WriteLine("\n--- E-commerce Sales Report ---");
        Console.WriteLine($"{"Product",-15} {"Quantity",-10} {"Total Sales",-10}");
        Console.WriteLine(new string('-', 40));

        foreach (var sale in salesReport)
        {
            Console.WriteLine($"{sale.ProductName,-15} {sale.QuantitySold,-10} {sale.TotalSales:C}");
        }
    }
}

class Program
{
    static void Main()
    {
        var app = new EcommerceApp();
        app.Run();
    }
}
