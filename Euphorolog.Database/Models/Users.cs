using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Euphorolog.Database.Models
{
    public class Users
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string eMail { get; set; }
        public string password { get; set; }
        public byte[]? passwordHash { get; set; }
        public byte[]? passwordSalt { get; set; }
        public DateTime? passChangedAt { get; set; }
        public ICollection<Stories>? stories { get; set; }
    }
}
