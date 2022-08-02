using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Euphorolog.Database.Models
{
    public class Stories
    {
            [Key,Required]
            public string storyId { get; set; }
            public int storyNo { get; set; }
            public string storyTitle { get; set; }
            [ForeignKey("authorName")]
            public Users? Users { get; set; }
            public string authorName { get; set; }
            public string openingLines { get; set; }
            public string storyDescription { get; set; }
            public DateTime createdAt { get; set; }
            public DateTime updatedAt { get; set; }
    }
}
