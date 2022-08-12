using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.DTOs.UsersDTOs
{
    public class UpdateUserInfoRequestDTO
    {
        public string? fullName { get; set; }
        public string? password { get; set; }
    }
}
