using Euphorolog.Database.Models;
using Microsoft.EntityFrameworkCore;
namespace Euphorolog.Database.Context
{
    public class EuphorologContext: DbContext
    {
        public EuphorologContext(DbContextOptions<EuphorologContext> options) : base (options)
        {
        }
        public DbSet<Stories> stories { get; set; }
        public DbSet<Users> users { get; set; }

    }
}
