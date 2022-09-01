using Euphorolog.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.UnitTests.Repository.TestDbContext
{
    public class ContextGenerator
    {
        public static EuphorologContext Generate()
        {
            var optionBuilder = new DbContextOptionsBuilder<EuphorologContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var FakeContext = new EuphorologContext(optionBuilder.Options);
            FakeContext.Database.EnsureCreated();
            return FakeContext;
        }
    }
}
