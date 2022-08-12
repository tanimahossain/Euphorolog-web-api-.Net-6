﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.DTOs.UsersDTOs
{
    public class UpdateUserInfoResponseDTO
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string eMail { get; set; }
        public string? token { get; set; }
    }
}
