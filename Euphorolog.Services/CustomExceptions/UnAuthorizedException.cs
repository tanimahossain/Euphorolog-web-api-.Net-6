using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Services.CustomExceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string message) : base(message) { }
    }
}
