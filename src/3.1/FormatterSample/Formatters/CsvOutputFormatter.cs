using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FormatterSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace FormatterSample.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        /// <summary>
        /// 新增支援的 MIME
        /// </summary>
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        /// <summary>
        /// 判斷型別是否支援，在未指定 MIME 請況下，如型別支援即回傳 true
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected override bool CanWriteType(Type type)
        {
            if (typeof(Blog).IsAssignableFrom(type) || typeof(IEnumerable<Blog>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            // 如何取得注入的服務
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            var logger = serviceProvider.GetService(typeof(ILogger<CsvOutputFormatter>)) as ILogger;

            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            // 要輸出的資料在純 Object 裡，需轉型處理
            if (context.Object is IEnumerable<Blog> blogs)
            {
                foreach (Blog blog in blogs)
                {
                    FormatCsv(buffer, blog, logger);
                }
            }
            else
            {
                var blog = context.Object as Blog;
                FormatCsv(buffer, blog, logger);
            }

            await response.WriteAsync(buffer.ToString());
        }

        /// <summary>
        /// 轉換為 CSV 格式
        /// </summary>
        private static void FormatCsv(StringBuilder buffer, Blog blog, ILogger logger)
        {
            foreach (var blogPost in blog.Posts)
            {
                buffer.AppendLine($"\"{blog.Name}\",\"{blog.Description}\",\"{blogPost.Title}\"");
                logger.LogInformation($"Write \"{blog.Name}\",\"{blog.Description}\",\"{blogPost.Title}\"");
            }
        }
    }
}
