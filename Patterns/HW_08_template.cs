using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    abstract class Beverage
    {
        // Шаблонный метод — определяет последовательность шагов
        public void PrepareRecipe()
        {
            BoilWater();
            Brew();
            PourInCup();
            if (CustomerWantsCondiments())
            {
                AddCondiments();
            }
        }

        // Общие шаги для всех напитковu
        private void BoilWater()
        {
            Console.WriteLine("Кипятим воду...");
        }

        private void PourInCup()
        {
            Console.WriteLine("Наливаем в чашку...");
        }

        protected abstract void Brew();
        protected abstract void AddCondiments();

        protected virtual bool CustomerWantsCondiments()
        {
            return true;
        }
    }
    class Tea : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Завариваем чай...");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Добавляем лимон...");
        }
    }
    class Coffee : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Готовим кофе...");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Добавляем сахар и молоко...");
        }

        protected override bool CustomerWantsCondiments()
        {
            Console.Write("\r\nХотите ли вы к кофе молоко и сахар (да/нет)? ");
            string userInput = Console.ReadLine()?.ToLower();

            while (userInput != "да" && userInput != "нет")
            {
                Console.Write("Пожалуйста, введите «да» или «нет»: ");
                userInput = Console.ReadLine()?.ToLower();
            }

            return userInput == "да";
        }
    }
    class HotChocolate : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Смешиваем горячий шоколадный порошок...");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Добавляем взбитые сливки...");
        }

        protected override bool CustomerWantsCondiments()
        {
            return true;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Процесс приготовление чая...");
            Beverage tea = new Tea();
            tea.PrepareRecipe();

            Console.WriteLine("\nПроцесс приготовление кофе...");
            Beverage coffee = new Coffee();
            coffee.PrepareRecipe();

            Console.WriteLine("\nГотовим горячий шоколад...");
            Beverage hotChocolate = new HotChocolate();
            hotChocolate.PrepareRecipe();
        }
    }

}
