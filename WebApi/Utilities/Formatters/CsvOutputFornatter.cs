using Entities.DataTranferObjects;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace WebApi.Utilities.Formatters
{
    public class CsvOutputFornatter : TextOutputFormatter
    {
        public CsvOutputFornatter()
        {
            SupportedMediaTypes.Add(Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);

        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(BookDto).IsAssignableFrom(type) ||
                typeof(IEnumerable<BookDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        private static void FormatCsv(StringBuilder buffer, BookDto book)
        {
            buffer.AppendLine($"{book.Id}, {book.Title}, {book.Price}");
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context,
             Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<BookDto>)
            {
                foreach (var book in (IEnumerable<BookDto>)context.Object)
                {
                    FormatCsv(buffer, book);
                }
            }
            else
            {
                FormatCsv(buffer, (BookDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
    }
}
