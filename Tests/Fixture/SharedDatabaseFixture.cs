using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;

namespace Net6ShCart.Tests.Fixture
{
    public class SharedDatabaseFixture : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}