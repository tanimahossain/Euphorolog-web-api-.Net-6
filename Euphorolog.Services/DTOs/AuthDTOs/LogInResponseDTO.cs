using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.DTOs.AuthDTOs
{
    public class LogInResponseDTO
    {
        public string userName { get; set; }
        public string token { get; set; }
    }
}
