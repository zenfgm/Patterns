using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public interface IDocument
    {
        void Open();
    }
    public class Report : IDocument
    {
        public void Open()
        {
            Console.WriteLine("Открытие отчета...");
        }
    }
    public class Resume : IDocument
    {
        public void Open()
        {
            Console.WriteLine("Открытие резюме...");
        }
    }
    public class Letter : IDocument
    {
        public void Open()
        {
            Console.WriteLine("Открытие письма...");
        }
    }
    public class Invoice : IDocument
    {
        public void Open()
        {
            Console.WriteLine("Открытие накладной...");
        }
    }
    public abstract class DocumentCreator
    {
        public abstract IDocument CreateDocument();
    }
    public class ReportCreator : DocumentCreator
    {
        public override IDocument CreateDocument()
        {
            return new Report();
        }
    }
    public class ResumeCreator : DocumentCreator
    {
        public override IDocument CreateDocument()
        {
            return new Resume();
        }
    }
    public class LetterCreator : DocumentCreator
    {
        public override IDocument CreateDocument()
        {
            return new Letter();
        }
    }
    public class InvoiceCreator : DocumentCreator
    {
        public override IDocument CreateDocument()
        {
            return new Invoice();
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Выберите тип документа: 1 - Отчет, 2 - Резюме, 3 - Письмо, 4 - Накладная");
            int choice = int.Parse(Console.ReadLine());

            DocumentCreator creator = null;

            switch (choice)
            {
                case 1:
                    creator = new ReportCreator();
                    break;
                case 2:
                    creator = new ResumeCreator();
                    break;
                case 3:
                    creator = new LetterCreator();
                    break;
                case 4:
                    creator = new InvoiceCreator();
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    return;
            }

            IDocument document = creator.CreateDocument();
            document.Open();
        }
    }
}
