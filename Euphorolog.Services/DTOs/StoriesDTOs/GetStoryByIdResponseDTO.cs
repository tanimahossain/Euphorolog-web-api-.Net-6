using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.DTOs.StoriesDTOs
{
    public class GetStoryByIdResponseDTO
    {
        public string storyId { get; set; }
        public string storyTitle { get; set; }
        public string authorName { get; set; }
        public string storyDescription { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
