using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Xunit;

namespace UnitTests
{
    public class BackEndUnitTests
    {
		[Fact]
		public SqlConnection CreateConnection()
		{
			SqlConnection conn = BackEnd.openConnection("data source=localhost;initial catalog=AdventureWorks2008;integrated security=SSPI");
			Assert.NotNull(conn);

			return conn;
		}

		[Fact]
		public void BadConnection()
		{
			Assert.Throws<SqlException>(() =>
				BackEnd.openConnection("data source=localhost;initial catalog=NoDatabase;integrated security=SSPI"));
		}

		[Fact]
		public void ReadSchema()
		{
			CreateConnection();
		}
    }
}
