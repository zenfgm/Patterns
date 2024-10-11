using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.HW_06
{
    public class Product : ICloneable
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public object Clone()
        {
            return new Product(Name, Price, Quantity);
        }

        public override string ToString()
        {
            return $"{Name} (x{Quantity}) - {Price} за каждый";
        }
    }
    public class Discount : ICloneable
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public Discount(string description, decimal amount)
        {
            Description = description;
            Amount = amount;
        }

        public object Clone()
        {
            return new Discount(Description, Amount);
        }

        public override string ToString()
        {
            return $"{Description}: -{Amount}";
        }
    }
    public class Order : ICloneable
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public decimal DeliveryCost { get; set; }
        public Discount AppliedDiscount { get; set; }
        public string PaymentMethod { get; set; }

        public Order(List<Product> products, decimal deliveryCost, Discount appliedDiscount, string paymentMethod)
        {
            Products = products;
            DeliveryCost = deliveryCost;
            AppliedDiscount = appliedDiscount;
            PaymentMethod = paymentMethod;
        }

        public object Clone()
        {
            var clonedProducts = new List<Product>();
            foreach (var product in Products)
            {
                clonedProducts.Add((Product)product.Clone());
            }

            var clonedDiscount = (Discount)AppliedDiscount?.Clone();

            return new Order(clonedProducts, DeliveryCost, clonedDiscount, PaymentMethod);
        }

        public override string ToString()
        {
            string productDetails = string.Join(", ", Products);
            string discountDetails = AppliedDiscount != null ? AppliedDiscount.ToString() : "Нет скидки";
            return $"Товары: {productDetails}\nСтоимость доставки: {DeliveryCost}\nСкидка: {discountDetails}\nСпособ оплаты: {PaymentMethod}";
        }
    }

}
