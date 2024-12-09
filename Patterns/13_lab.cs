using System;
using System.Collections.Generic;

namespace LogisticsSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> stockItems = new List<string> { "Товар1", "Товар2", "Товар3" };
            List<string> orderItems = new List<string> { "Товар1", "Товар4" };

            bool isOrderValid = CheckStock(stockItems, orderItems);

            if (isOrderValid)
            {
                Console.WriteLine("Все товары в наличии. Заказ подтвержден.");
            }
            else
            {
                Console.WriteLine("Некоторые товары недоступны. Отправлено уведомление клиенту.");
            }
        }

        static bool CheckStock(List<string> stockItems, List<string> orderItems)
        {
            foreach (var item in orderItems)
            {
                if (!stockItems.Contains(item))
                {
                    Console.WriteLine($"Товар {item} недоступен.");
                    return false;
                }
            }
            return true;
        }
    }
}
