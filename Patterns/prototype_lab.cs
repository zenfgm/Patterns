using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Laba_06
{
    public interface IPrototype<T>
    {
        T Clone();
    }
    public class Section : IPrototype<Section>
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Section(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public Section Clone()
        {
            return new Section(this.Title, this.Content);
        }

        public override string ToString()
        {
            return $"Раздел: {Title}, Содержание: {Content}";
        }
    }
    public class Image : IPrototype<Image>
    {
        public string Url { get; set; }

        public Image(string url)
        {
            Url = url;
        }

        public Image Clone()
        {
            return new Image(this.Url);
        }

        public override string ToString()
        {
            return $"Изображение: {Url}";
        }
    }
    public class Document : IPrototype<Document>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Section> Sections { get; set; }
        public List<Image> Images { get; set; }

        public Document(string title, string content)
        {
            Title = title;
            Content = content;
            Sections = new List<Section>();
            Images = new List<Image>();
        }

        public void AddSection(Section section)
        {
            Sections.Add(section);
        }

        public void AddImage(Image image)
        {
            Images.Add(image);
        }

        public Document Clone()
        {
            Document clonedDocument = new Document(this.Title, this.Content);

            foreach (var section in this.Sections)
            {
                clonedDocument.AddSection(section.Clone());
            }

            foreach (var image in this.Images)
            {
                clonedDocument.AddImage(image.Clone());
            }

            return clonedDocument;
        }

        public override string ToString()
        {
            string sectionsStr = string.Join("\n", Sections);
            string imagesStr = string.Join("\n", Images);
            return $"Документ: {Title}\nСодержание: {Content}\nРазделы:\n{sectionsStr}\nИзображения:\n{imagesStr}";
        }
    }
    public class DocumentManager
    {
        public Document CreateDocument(Document prototype)
        {
            return prototype.Clone();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Document originalDocument = new Document("Отчет", "Основное содержание отчета");
            originalDocument.AddSection(new Section("Введение", "Текст введения"));
            originalDocument.AddSection(new Section("Заключение", "Текст заключения"));
            originalDocument.AddImage(new Image("image1.png"));
            originalDocument.AddImage(new Image("image2.png"));

            Console.WriteLine("Оригинальный документ:");
            Console.WriteLine(originalDocument);

            DocumentManager docManager = new DocumentManager();
            Document clonedDocument = docManager.CreateDocument(originalDocument);

            clonedDocument.Title = "Копия отчета";
            clonedDocument.AddSection(new Section("Новая секция", "Содержание новой секции"));
            clonedDocument.AddImage(new Image("image3.png"));

            Console.WriteLine("\nКлонированный и измененный документ:");
            Console.WriteLine(clonedDocument);
        }
    }

}
