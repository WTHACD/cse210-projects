using System;
using System.Collections.Generic;

class Address
{
    private string Street { get; }
    private string City { get; }
    private string State { get; }
    private string Country { get; }

    public Address(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }

    public bool IsInUSA()
    {
        return Country.ToLower() == "usa";
    }

    public override string ToString()
    {
        return $"{Street}\n{City}, {State}\n{Country}";
    }
}

class Customer
{
    public string Name { get; }
    private Address Address { get; }

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public bool IsInUSA()
    {
        return Address.IsInUSA();
    }

    public string GetShippingLabel()
    {
        return $"Customer: {Name}\n{Address}";
    }
}

class Product
{
    public string Name { get; }
    public int ProductId { get; }
    private double Price { get; }
    private int Quantity { get; }

    public Product(string name, int productId, double price, int quantity)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public double GetTotalCost()
    {
        return Price * Quantity;
    }

    public string GetPackingLabel()
    {
        return $"{Name} (ID: {ProductId}) - Quantity: {Quantity}";
    }
}

class Order
{
    private List<Product> Products = new List<Product>();
    private Customer Customer;

    public Order(Customer customer)
    {
        Customer = customer;
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public double CalculateTotalCost()
    {
        double totalCost = 0;
        foreach (var product in Products)
        {
            totalCost += product.GetTotalCost();
        }
        totalCost += Customer.IsInUSA() ? 5 : 35;
        return totalCost;
    }

    public string GetPackingLabel()
    {
        string label = "Packing List:\n";
        foreach (var product in Products)
        {
            label += $"- {product.GetPackingLabel()}\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{Customer.GetShippingLabel()}";
    }
}

class Program
{
    static void Main()
    {
        Customer customer1 = new Customer("Alice Johnson", new Address("123 Main St", "New York", "NY", "USA"));
        Customer customer2 = new Customer("Bob Smith", new Address("456 Maple Ave", "Toronto", "ON", "Canada"));

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", 101, 1200, 1));
        order1.AddProduct(new Product("Mouse", 102, 25, 2));

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Phone", 201, 800, 1));
        order2.AddProduct(new Product("Headphones", 202, 100, 1));

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.CalculateTotalCost()}\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.CalculateTotalCost()}");
    }
}
