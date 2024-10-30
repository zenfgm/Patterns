using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Patterns._09_lab;

namespace Patterns
{
    internal class _09_lab
    {
        public interface IBeverage
        {
            double GetCost(); 
            string GetDescription();
        }

        public class Coffee : IBeverage
        {
            public double GetCost()
            {
                return 50.0;
            }

            public string GetDescription()
            {
                return "Coffee";
            }
        }

        public abstract class BeverageDecorator : IBeverage
        {
            protected IBeverage _beverage;

            public BeverageDecorator(IBeverage beverage)
            {
                _beverage = beverage;
            }

            public virtual double GetCost()
            {
                return _beverage.GetCost();
            }

            public virtual string GetDescription()
            {
                return _beverage.GetDescription();
            }
        }

        public class MilkDecorator : BeverageDecorator
        {
            public MilkDecorator(IBeverage beverage) : base(beverage) { }

            public override double GetCost()
            {
                return base.GetCost() + 10.0;
            }

            public override string GetDescription()
            {
                return base.GetDescription() + ", Milk";
            }
        }

        public class SugarDecorator : BeverageDecorator
        {
            public SugarDecorator(IBeverage beverage) : base(beverage) { }

            public override double GetCost()
            {
                return base.GetCost() + 5.0;
            }

            public override string GetDescription()
            {
                return base.GetDescription() + ", Sugar";
            }
        }

        public class ChocolateDecorator : BeverageDecorator
        {
            public ChocolateDecorator(IBeverage beverage) : base(beverage) { }

            public override double GetCost()
            {
                return base.GetCost() + 15.0;
            }

            public override string GetDescription()
            {
                return base.GetDescription() + ", Chocolate";
            }
        }

        public class VanillaDecorator : BeverageDecorator
        {
            public VanillaDecorator(IBeverage beverage) : base(beverage) { }

            public override double GetCost()
            {
                return base.GetCost() + 7.0;
            }

            public override string GetDescription()
            {
                return base.GetDescription() + ", Vanilla";
            }
        }

        public class CinnamonDecorator : BeverageDecorator
        {
            public CinnamonDecorator(IBeverage beverage) : base(beverage) { }

            public override double GetCost()
            {
                return base.GetCost() + 8.0;
            }

            public override string GetDescription()
            {
                return base.GetDescription() + ", Cinnamon";
            }
        }

        public interface IPaymentProcessor
        {
            void ProcessPayment(double amount);
            void RefundPayment(double amount);
        }

        public class InternalPaymentProcessor : IPaymentProcessor
        {
            public void ProcessPayment(double amount)
            {
                Console.WriteLine($"Processing payment of {amount} via internal system.");
            }

            public void RefundPayment(double amount)
            {
                Console.WriteLine($"Refunding payment of {amount} via internal system.");
            }
        }

        public class MastercardPaymentSystem
        {
            public void MakePayment(double amount)
            {
                Console.WriteLine($"Making payment of {amount} via Mastercard Payment System.");
            }

            public void MakeRefund(double amount)
            {
                Console.WriteLine($"Making refund of {amount} via Mastercard Payment System.");
            }
        }

        public class VisaPaymentSystemB
        {
            public void SendPayment(double amount)
            {
                Console.WriteLine($"Sending payment of {amount} via Visa Payment System.");
            }

            public void ProcessRefund(double amount)
            {
                Console.WriteLine($"Processing refund of {amount} via Visa Payment System.");
            }
        }

        public class PaymentAdapterA : IPaymentProcessor
        {
            private MastercardPaymentSystem _mastercardSystem;

            public PaymentAdapterA(MastercardPaymentSystem mastercardSystem)
            {
                _mastercardSystem = mastercardSystem;
            }

            public void ProcessPayment(double amount)
            {
                _mastercardSystem.MakePayment(amount);
            }

            public void RefundPayment(double amount)
            {
                _mastercardSystem.MakeRefund(amount);
            }
        }

        public class PaymentAdapterB : IPaymentProcessor
        {
            private VisaPaymentSystemB _visaSystem;

            public PaymentAdapterB(VisaPaymentSystemB externalSystemB)
            {
                _visaSystem = externalSystemB;
            }

            public void ProcessPayment(double amount)
            {
                _visaSystem.SendPayment(amount);
            }

            public void RefundPayment(double amount)
            {
                _visaSystem.ProcessRefund(amount);
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                IBeverage beverage = new Coffee();
                Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");

                beverage = new MilkDecorator(beverage);
                Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");

                beverage = new SugarDecorator(beverage);
                Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");

                beverage = new ChocolateDecorator(beverage);
                Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");

                beverage = new VanillaDecorator(beverage);
                Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");

                beverage = new CinnamonDecorator(beverage);
                Console.WriteLine($"{beverage.GetDescription()} : {beverage.GetCost()}");

                IPaymentProcessor internalProcessor = new InternalPaymentProcessor();
                internalProcessor.ProcessPayment(100.0);
                internalProcessor.RefundPayment(50.0);

                MastercardPaymentSystem mastercardSystem = new MastercardPaymentSystem();
                IPaymentProcessor adapterA = new PaymentAdapterA(mastercardSystem);
                adapterA.ProcessPayment(200.0);
                adapterA.RefundPayment(100.0);

                VisaPaymentSystemB visaSystem = new VisaPaymentSystemB();
                IPaymentProcessor adapterB = new PaymentAdapterB(visaSystem);
                adapterB.ProcessPayment(300.0);
                adapterB.RefundPayment(150.0);
            }
        }
    }
}
