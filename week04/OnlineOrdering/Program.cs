using System;

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("123 Main St", "Rexburg", "ID", "USA");
        Customer customer1 = new Customer("Jane Doe", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Mechanical Keyboard", "P001", 49.99m, 1));
        order1.AddProduct(new Product("Wireless Mouse", "P002", 19.99m, 2));
        order1.AddProduct(new Product("24-inch Monitor", "P003", 199.99m, 1));

        Address address2 = new Address("10 King Street", "Toronto", "ON", "Canada");
        Customer customer2 = new Customer("John Smith", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Notebook", "P004", 4.50m, 3));
        order2.AddProduct(new Product("Pen Set", "P005", 7.25m, 2));

        DisplayOrder(order1);
        DisplayOrder(order2);
    }

    static void DisplayOrder(Order order)
    {
        Console.WriteLine();
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine();
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine();
        Console.WriteLine($"Total Price: ${order.GetTotalPrice():F2}");
        Console.WriteLine(new string('-', 40));
    }
}
