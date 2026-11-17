using System;
using System.Collections.Generic;
using System.Text;

public class Order
{
    private Customer _customer;
    private List<Product> _products;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal GetTotalPrice()
    {
        decimal total = 0;

        foreach (Product product in _products)
        {
            total += product.GetTotalCost();
        }

        return total + GetShippingCost();
    }

    public string GetPackingLabel()
    {
        StringBuilder label = new StringBuilder();
        label.AppendLine("Packing Label:");

        foreach (Product product in _products)
        {
            label.AppendLine($"- {product.Name} (ID: {product.ProductId})");
        }

        return label.ToString().TrimEnd();
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{_customer.Name}\n{_customer.Address.GetFullAddress()}";
    }

    private decimal GetShippingCost()
    {
        return _customer.IsInUSA() ? 5m : 35m;
    }
}
