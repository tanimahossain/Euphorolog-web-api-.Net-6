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
    public class PlainTextCustomOutputFormatter : TextOutputFormatter
    {
        public PlainTextCustomOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));

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
                IEnumerable<GetStoryByIdResponseDTO> posts = (IEnumerable<GetStoryByIdResponseDTO>)context.Object;
                foreach (GetStoryByIdResponseDTO post in posts)
                {
                    ConvertToPlainText(buffer, post);
                }
            }
            else
            {
                ConvertToPlainText(buffer, (GetStoryByIdResponseDTO)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }


        private static void ConvertToPlainText(StringBuilder buffer, GetStoryByIdResponseDTO post)
        {
            buffer.AppendLine($"StoryID: {post.storyId}\nTitle: {post.storyTitle}\nAuthor: {post.authorName}\nDescription: {post.storyDescription}\ncreatedAt: {post.createdAt}\nupdatedAt: {post.updatedAt}\n");
        }
    }
}
