using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.DTOs.AuthDTOs
{
    public class SignUpRequestDTO
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string eMail { get; set; }
        public string password { get; set; }
    }
}
