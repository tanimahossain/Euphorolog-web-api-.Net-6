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
    public class HTMLCustomOutputFormatter : TextOutputFormatter
    {
        public HTMLCustomOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/html"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type? type)
        {
            if (typeof(GetStoryByIdResponseDTO).IsAssignableFrom(type) || typeof(IEnumerable<GetStoryByIdResponseDTO>).IsAssignableFrom(type))
                return base.CanWriteType(type);
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<GetStoryByIdResponseDTO>)
            {
                IEnumerable<GetStoryByIdResponseDTO> stories = (IEnumerable<GetStoryByIdResponseDTO>)context.Object;
                foreach (GetStoryByIdResponseDTO story in stories)
                {
                    ConvertToHTML(buffer, story);
                }
            }
            else
            {
                ConvertToHTML(buffer, (GetStoryByIdResponseDTO)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }


        private static void ConvertToHTML(StringBuilder buffer, GetStoryByIdResponseDTO story)
        {
            buffer.AppendLine($"<article><small>{story.storyId}</small><h2>{story.storyTitle}</h2><h5>{story.authorName}</h5><small>{story.createdAt}<br>{story.updatedAt}</small><br><big>{story.storyDescription}</big></article>");
        }
    }
}

