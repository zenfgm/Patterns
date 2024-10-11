using Patterns.HW_06;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
public class ConfigurationManager
{
    private static ConfigurationManager instance = null;
    private static readonly object lockObj = new object();
    private Dictionary<string, string> settings;

    private ConfigurationManager()
    {
        settings = new Dictionary<string, string>();
        LoadSettings();
    }

    public static ConfigurationManager GetInstance()
    {
        if (instance == null)
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = new ConfigurationManager();
                }
            }
        }
        return instance;
    }

    private void LoadSettings()
    {
        if (File.Exists("config.txt"))
        {
            var lines = File.ReadAllLines("config.txt");
            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    settings[parts[0].Trim()] = parts[1].Trim();
                }
            }
        }
    }

    public void SaveSettings()
    {
        using (StreamWriter writer = new StreamWriter("config.txt"))
        {
            foreach (var kvp in settings)
            {
                writer.WriteLine($"{kvp.Key}={kvp.Value}");
            }
        }
    }

    public string GetSetting(string key)
    {
        if (settings.ContainsKey(key))
        {
            return settings[key];
        }
        return null;
    }

    public void SetSetting(string key, string value)
    {
        if (settings.ContainsKey(key))
        {
            settings[key] = value;
        }
        else
        {
            settings.Add(key, value);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        //----------Одиночка----------
        Console.WriteLine("----------Singleton----------");
        Console.WriteLine("-----------------------------");
        ConfigurationManager config1 = ConfigurationManager.GetInstance();
        ConfigurationManager config2 = ConfigurationManager.GetInstance();

        Console.WriteLine(ReferenceEquals(config1, config2));

        config1.SetSetting("Theme", "Dark");
        config1.SaveSettings();

        Console.WriteLine(config2.GetSetting("Theme"));

        //----------Строитель------------
        Console.WriteLine("----------Builder------------");
        Console.WriteLine("-----------------------------");
        ReportDirector director = new ReportDirector();

        IReportBuilder textBuilder = new TextReportBuilder();
        director.ConstructReport(textBuilder, "Текстовый отчет ", "Это простой текстовый отчет.", "Текст нижнего колонтитула");
        Report textReport = textBuilder.GetReport();
        Console.WriteLine("Текстовый отчет:");
        textReport.Display();

        IReportBuilder htmlBuilder = new HtmlReportBuilder();
        director.ConstructReport(htmlBuilder, "HTML-отчет", "Это отчет в формате HTML.", "HTML нижнего колонтитула");
        Report htmlReport = htmlBuilder.GetReport();
        Console.WriteLine("\nHTML-отчет:");
        htmlReport.Display();


        // ---------------Прототип------------
        Console.WriteLine("----------Prototype----------");
        Console.WriteLine("-----------------------------");
        var products = new List<Product>
        {
            new Product("Ноутбук", 1500.99m, 1),
            new Product("Компьютерная мышь", 25.50m, 2)
        };

        var discount = new Discount("Летняя распродажа", 100m);

        var originalOrder = new Order(products, 20m, discount, "Кредитная карта");
        Console.WriteLine("Исходный заказ:");
        Console.WriteLine(originalOrder);

        var clonedOrder = (Order)originalOrder.Clone();

        clonedOrder.Products[0].Quantity = 2;
        clonedOrder.DeliveryCost = 15m;
        clonedOrder.PaymentMethod = "PayPal";

        Console.WriteLine("\nКлонированный заказ с изменениями:");
        Console.WriteLine(clonedOrder);

    }
}
