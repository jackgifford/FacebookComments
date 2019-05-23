using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.Gui.Models;

namespace Tree.Gui.Services
{
    public class OutputService
    {
        /// <summary>
        /// Formats we support for printing
        /// </summary>
        public enum PrintFormats
        {
            Html,
            Json, // This is a lie ;)
        }

        public void Print(Comment head, string filePath, PrintFormats selected)
        {
            string body;
            switch (selected)
            {
                case PrintFormats.Html:
                    body = PrettyPrint(head);
                    break;

                case PrintFormats.Json:
                    body = JsonPrint(head);
                    break;

                default:
                    throw new Exception("Should never hit");
            }

            var sw = new StreamWriter(filePath);

            sw.Write(body);
            sw.Flush();
            sw.Dispose();
        }

        // Write to html
        private string PrettyPrint(Comment head)
        {
            var sb = new StringBuilder();

            sb.AppendLine(@"<ul>");
            sb.AppendLine($"<li>{head.CommentText}</li>");

            foreach (var child in head.Children)
            {
                sb.Append(PrettyPrint(child));
            }

            sb.AppendLine(@"</ul>");

            return sb.ToString();
        }

        private string JsonPrint(Comment head)
        {
            throw new NotImplementedException();
        }
    }
}
