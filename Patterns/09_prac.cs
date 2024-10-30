using System;
using System.Collections.Generic;
using System.IO;

namespace Patterns
{
    internal class _09_prac
    {
        public interface IReport
        {
            string Generate();
        }

        public class SalesReport : IReport
        {
            public string Generate() => "Sales Data: Total sales for the month are $10,000";
        }

        public class UserReport : IReport
        {
            public string Generate() => "User Data: Total registered users: 5,000";
        }

        public abstract class ReportDecorator : IReport
        {
            private readonly IReport report;
            protected ReportDecorator(IReport report) => this.report = report;

            public virtual string Generate() => report.Generate();
        }

        public class DateFilterDecorator : ReportDecorator
        {
            private readonly DateTime startDate;
            private readonly DateTime endDate;

            public DateFilterDecorator(IReport report, DateTime startDate, DateTime endDate)
                : base(report)
            {
                this.startDate = startDate;
                this.endDate = endDate;
            }

            public override string Generate() => $"Filtered from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}: " + base.Generate();
        }

        public class SortingDecorator : ReportDecorator
        {
            private readonly string criteria;

            public SortingDecorator(IReport report, string criteria) : base(report)
            {
                this.criteria = criteria;
            }

            public override string Generate() => $"Sorted by {criteria}: " + base.Generate();
        }

        public class CsvExportDecorator : ReportDecorator
        {
            public CsvExportDecorator(IReport report) : base(report) { }

            public override string Generate()
            {
                var data = base.Generate();
                File.WriteAllText("report.csv", data.Replace(" ", ","));
                return "Exported report to CSV.";
            }
        }

        public class PdfExportDecorator : ReportDecorator
        {
            public PdfExportDecorator(IReport report) : base(report) { }

            public override string Generate()
            {
                var data = base.Generate();
                return "Exported report to PDF.";
            }
        }

        public interface IInternalDeliveryService
        {
            void DeliverOrder(string orderId);
            string GetDeliveryStatus(string orderId);
        }

        public class InternalDeliveryService : IInternalDeliveryService
        {
            public void DeliverOrder(string orderId) => Console.WriteLine("Internal Delivery for Order: " + orderId);
            public string GetDeliveryStatus(string orderId) => "Internal Status for Order " + orderId;
        }

        public class GlovoLogisticService
        {
            public void ShipItem(int itemId) => Console.WriteLine("Glovo shipping item " + itemId);
            public string TrackShipment(int itemId) => "Glovo status for item " + itemId;
        }

        public class LogisticAdapterGlovo : IInternalDeliveryService
        {
            private readonly GlovoLogisticService glovoService;

            public LogisticAdapterGlovo(GlovoLogisticService service) => glovoService = service;

            public void DeliverOrder(string orderId) => glovoService.ShipItem(int.Parse(orderId));
            public string GetDeliveryStatus(string orderId) => glovoService.TrackShipment(int.Parse(orderId));
        }

        public class WoltLogisticsServiceB
        {
            public void SendPackage(string packageInfo) => Console.WriteLine("Wolt sending package " + packageInfo);
            public string CheckPackageStatus(string trackingCode) => "Wolt status for package " + trackingCode;
        }

        public class LogisticAdapterB : IInternalDeliveryService
        {
            private readonly WoltLogisticsServiceB externalServiceB;

            public LogisticAdapterB(WoltLogisticsServiceB service) => externalServiceB = service;

            public void DeliverOrder(string orderId) => externalServiceB.SendPackage(orderId);
            public string GetDeliveryStatus(string orderId) => externalServiceB.CheckPackageStatus(orderId);
        }

        public class DeliveryServiceFactory
        {
            public static IInternalDeliveryService GetDeliveryService(string type)
            {
                return type switch
                {
                    "Glovo" => new LogisticAdapterGlovo(new GlovoLogisticService()),
                    "Wolt" => new LogisticAdapterB(new WoltLogisticsServiceB()),
                    _ => new InternalDeliveryService(),
                };
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                IReport report = new SalesReport();
                report = new DateFilterDecorator(report, DateTime.Now.AddMonths(-1), DateTime.Now);
                report = new SortingDecorator(report, "date");
                report = new CsvExportDecorator(report);
                Console.WriteLine(report.Generate());


                IInternalDeliveryService deliveryService = DeliveryServiceFactory.GetDeliveryService("Glovo");
                deliveryService.DeliverOrder("12345");
                Console.WriteLine(deliveryService.GetDeliveryStatus("12345"));

                deliveryService = DeliveryServiceFactory.GetDeliveryService("Wolt");
                deliveryService.DeliverOrder("67890");
                Console.WriteLine(deliveryService.GetDeliveryStatus("67890"));
            }
        }
    }
}
