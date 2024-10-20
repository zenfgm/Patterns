using Patterns.HW_06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Patterns
{
    public interface IReportBuilder
    {
        void SetHeader(string header);
        void SetContent(string content);
        void SetFooter(string footer);
        void AddSection(string sectionName, string sectionContent);
        void SetStyle(ReportStyle style);
        Report GetReport();
    }
    public class TextReportBuilder : IReportBuilder
    {
        private Report _report = new Report();

        public void SetHeader(string header)
        {
            _report.Header = $"*** {header} ***\n";
        }

        public void SetContent(string content)
        {
            _report.Content = content + "\n";
        }

        public void SetFooter(string footer)
        {
            _report.Footer = $"\n--- {footer} ---";
        }

        public void AddSection(string sectionName, string sectionContent)
        {
            _report.Sections.Add($"{sectionName}:\n{sectionContent}\n");
        }

        public void SetStyle(ReportStyle style)
        {
        }

        public Report GetReport()
        {
            return _report;
        }
    }
    public class HtmlReportBuilder : IReportBuilder
    {
        private Report _report = new Report();

        public void SetHeader(string header)
        {
            _report.Header = $"<h1>{header}</h1>";
        }

        public void SetContent(string content)
        {
            _report.Content = $"<p>{content}</p>";
        }

        public void SetFooter(string footer)
        {
            _report.Footer = $"<footer>{footer}</footer>";
        }

        public void AddSection(string sectionName, string sectionContent)
        {
            _report.Sections.Add($"<h2>{sectionName}</h2><p>{sectionContent}</p>");
        }

        public void SetStyle(ReportStyle style)
        {
            _report.Style = $"<style>body {{ background-color: {style.BackgroundColor}; color: {style.FontColor}; font-size: {style.FontSize}px; }}</style>";
        }

        public Report GetReport()
        {
            return _report;
        }
    }
    public class PdfReportBuilder : IReportBuilder
    {
        private Document _pdfDocument;
        private MemoryStream _stream;
        private PdfWriter _writer;

        public PdfReportBuilder()
        {
            _pdfDocument = new Document();
            _stream = new MemoryStream();
            _writer = PdfWriter.GetInstance(_pdfDocument, _stream);
            _pdfDocument.Open();
        }

        public void SetHeader(string header)
        {
            _pdfDocument.Add(new Paragraph(header, FontFactory.GetFont("Arial", 16, BaseColor.BLACK)));
        }

        public void SetContent(string content)
        {
            _pdfDocument.Add(new Paragraph(content));
        }

        public void SetFooter(string footer)
        {
            _pdfDocument.Add(new Paragraph(footer, FontFactory.GetFont("Arial", 10, BaseColor.GRAY)));
        }

        public void AddSection(string sectionName, string sectionContent)
        {
            _pdfDocument.Add(new Paragraph($"{sectionName}: {sectionContent}"));
        }

        public void SetStyle(ReportStyle style)
        {
        }

        public Report GetReport()
        {
            _pdfDocument.Close();
            return new Report
            {
                PdfContent = _stream.ToArray()
            };
        }
    }
    public class ReportStyle
    {
        public string BackgroundColor { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
    }
    public class Report
    {
        public string Header { get; set; }
        public string Content { get; set; }
        public string Footer { get; set; }
        public List<string> Sections { get; set; } = new List<string>();
        public string Style { get; set; }
        public byte[] PdfContent { get; set; }

        public void Export(string fileName)
        {
            if (PdfContent != null)
            {
                File.WriteAllBytes(fileName, PdfContent);
            }
            else
            {
                File.WriteAllText(fileName, Header + Content + string.Join("\n", Sections) + Footer);
            }
        }
    }
    public class ReportDirector
    {
        public void ConstructReport(IReportBuilder builder, ReportStyle style)
        {
            builder.SetHeader("Отчет");
            builder.SetContent("Основное содержание отчета.");
            builder.AddSection("Раздел 1", "Содержимое раздела 1");
            builder.AddSection("Раздел 2", "Содержимое раздела 2");
            builder.SetFooter("Конец отчета");
            builder.SetStyle(style);
        }
    }
    class Program
    {
        static void Main()
        {
            ReportDirector director = new ReportDirector();
            ReportStyle style = new ReportStyle
            {
                BackgroundColor = "#ffffff",
                FontColor = "#000000",
                FontSize = 12
            };

            IReportBuilder textBuilder = new TextReportBuilder();
            director.ConstructReport(textBuilder, style);
            Report textReport = textBuilder.GetReport();
            textReport.Export("report.txt");

            IReportBuilder htmlBuilder = new HtmlReportBuilder();
            director.ConstructReport(htmlBuilder, style);
            Report htmlReport = htmlBuilder.GetReport();
            htmlReport.Export("report.html");

            IReportBuilder pdfBuilder = new PdfReportBuilder();
            director.ConstructReport(pdfBuilder, style);
            Report pdfReport = pdfBuilder.GetReport();
            pdfReport.Export("report.pdf");
        }
    }

}
