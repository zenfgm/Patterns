using System;

namespace Patterns
{
    public interface IBeverage
    {
        string Description { get; }
        double Cost();
    }

    public class Espresso : IBeverage
    {
        public string Description => "Espresso";
        public double Cost() => 1.50;
    }

    public class Tea : IBeverage
    {
        public string Description => "Tea";
        public double Cost() => 1.00;
    }

    public abstract class BeverageDecorator : IBeverage
    {
        protected IBeverage Beverage;
        protected BeverageDecorator(IBeverage beverage) { Beverage = beverage; }
        public virtual string Description => Beverage.Description;
        public virtual double Cost() => Beverage.Cost();
    }

    public class Milk : BeverageDecorator
    {
        public Milk(IBeverage beverage) : base(beverage) { }
        public override string Description => Beverage.Description + ", Milk";
        public override double Cost() => Beverage.Cost() + 0.25;
    }

    public class Sugar : BeverageDecorator
    {
        public Sugar(IBeverage beverage) : base(beverage) { }
        public override string Description => Beverage.Description + ", Sugar";
        public override double Cost() => Beverage.Cost() + 0.15;
    }

    public class WhippedCream : BeverageDecorator
    {
        public WhippedCream(IBeverage beverage) : base(beverage) { }
        public override string Description => Beverage.Description + ", Whipped Cream";
        public override double Cost() => Beverage.Cost() + 0.50;
    }

    public static class BeverageTest
    {
        public static void Run()
        {
            IBeverage beverage = new Espresso();
            beverage = new Milk(beverage);
            beverage = new Sugar(beverage);
            Console.WriteLine($"{beverage.Description} - Cost: ${beverage.Cost():0.00}");
        }
    }
}

namespace PaymentSystem
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
    }

    public class PayPalPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Processing payment of ${amount} via PayPal.");
        }
    }

    public class StripePaymentService
    {
        public void MakeTransaction(double totalAmount)
        {
            Console.WriteLine($"Processing payment of ${totalAmount} via Stripe.");
        }
    }

    public class StripePaymentAdapter : IPaymentProcessor
    {
        private StripePaymentService _stripeService;
        public StripePaymentAdapter(StripePaymentService stripeService)
        {
            _stripeService = stripeService;
        }
        public void ProcessPayment(double amount)
        {
            _stripeService.MakeTransaction(amount);
        }
    }

    public static class PaymentTest
    {
        public static void Run()
        {
            IPaymentProcessor paypalProcessor = new PayPalPaymentProcessor();
            paypalProcessor.ProcessPayment(20.0);

            IPaymentProcessor stripeProcessor = new StripePaymentAdapter(new StripePaymentService());
            stripeProcessor.ProcessPayment(15.0);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Patterns.BeverageTest.Run();
            PaymentSystem.PaymentTest.Run();
        }
    }
}
