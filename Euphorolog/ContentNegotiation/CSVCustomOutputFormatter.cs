using Euphorolog.Services.DTOs.StoriesDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.ContentNegotiation
{
    public class CSVCustomOutputFormatter : TextOutputFormatter
    {
        public CSVCustomOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type? type)
        {
            if (typeof(GetStoryResponseDTO).IsAssignableFrom(type) || typeof(IEnumerable<GetStoryResponseDTO>).IsAssignableFrom(type))
                return base.CanWriteType(type);
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            buffer.AppendLine($"StoryID,Title,Author,Story,Created At,Updated At");
            if (context.Object is IEnumerable<GetStoryResponseDTO>)
            {
                IEnumerable<GetStoryResponseDTO> stories = (IEnumerable<GetStoryResponseDTO>)context.Object;
                foreach (GetStoryResponseDTO story in stories)
                {
                    ConvertToCSV(buffer, story);
                }
            }
            else
            {
                ConvertToCSV(buffer, (GetStoryResponseDTO)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }


        private static void ConvertToCSV(StringBuilder buffer, GetStoryResponseDTO story)
        {
            buffer.AppendLine($"{story.storyId},{story.storyTitle},{story.authorName},{story.storyDescription},{story.createdAt},{story.updatedAt}");
        }
    }
}

