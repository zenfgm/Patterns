using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.HW_06
{
    public interface IReportBuilder
    {
        IReportBuilder SetHeader(string header);
        IReportBuilder SetContent(string content);
        IReportBuilder SetFooter(string footer);
        Report GetReport();
    }
    public class Report
    {
        public string Header { get; set; }
        public string Content { get; set; }
        public string Footer { get; set; }

        public void Display()
        {
            Console.WriteLine($"{Header}{Content}{Footer}");
        }
    }
    public class TextReportBuilder : IReportBuilder
    {
        private Report _report = new Report();

        public IReportBuilder SetHeader(string header)
        {
            _report.Header = $"=== {header} ===\n";
            return this;
        }

        public IReportBuilder SetContent(string content)
        {
            _report.Content = $"{content}\n";
            return this;
        }

        public IReportBuilder SetFooter(string footer)
        {
            _report.Footer = $"\n--- {footer} ---";
            return this;
        }

        public Report GetReport()
        {
            return _report;
        }
    }
    public class HtmlReportBuilder : IReportBuilder
    {
        private Report _report = new Report();

        public IReportBuilder SetHeader(string header)
        {
            _report.Header = $"<h1>{header}</h1>\n";
            return this;
        }

        public IReportBuilder SetContent(string content)
        {
            _report.Content = $"<p>{content}</p>\n";
            return this;
        }

        public IReportBuilder SetFooter(string footer)
        {
            _report.Footer = $"<footer>{footer}</footer>";
            return this;
        }

        public Report GetReport()
        {
            return _report;
        }
    }
    public class ReportDirector
    {
        public void ConstructReport(IReportBuilder builder, string header, string content, string footer)
        {
            builder.SetHeader(header)
                   .SetContent(content)
                   .SetFooter(footer);
        }
    }
}
