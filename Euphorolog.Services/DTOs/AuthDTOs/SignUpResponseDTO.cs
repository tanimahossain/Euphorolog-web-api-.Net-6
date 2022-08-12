using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.DTOs.AuthDTOs
{
    public class SignUpResponseDTO
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string eMail { get; set; }
        public string token { get; set; }
    }
}
