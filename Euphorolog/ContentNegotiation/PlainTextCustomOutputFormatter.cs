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
            if (typeof(GetStoryResponseDTO).IsAssignableFrom(type) || typeof(IEnumerable<GetStoryResponseDTO>).IsAssignableFrom(type))
                return base.CanWriteType(type);
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<GetStoryResponseDTO>)
            {
                IEnumerable<GetStoryResponseDTO> posts = (IEnumerable<GetStoryResponseDTO>)context.Object;
                foreach (GetStoryResponseDTO post in posts)
                {
                    ConvertToPlainText(buffer, post);
                }
            }
            else
            {
                ConvertToPlainText(buffer, (GetStoryResponseDTO)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }


        private static void ConvertToPlainText(StringBuilder buffer, GetStoryResponseDTO post)
        {
            buffer.AppendLine($"StoryID: {post.storyId}\nTitle: {post.storyTitle}\nAuthor: {post.authorName}\nDescription: {post.storyDescription}\ncreatedAt: {post.createdAt}\nupdatedAt: {post.updatedAt}\n");
        }
    }
}
