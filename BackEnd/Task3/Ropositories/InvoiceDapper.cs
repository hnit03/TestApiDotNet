using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Task3.IRepositories;
using Task3.Models;

namespace Task3.Ropositories
{
    public class InvoiceDapper : IInvoiceDapper
    {
        private readonly IDbConnection dbConnection;

        public InvoiceDapper(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public IEnumerable<Invoice> GetAllInvoice(string username)
        {
            string sql = "SELECT * FROM Invoice where username = @username";
            dbConnection.Open();
            var result =  dbConnection.Query<Invoice>(sql, new { username = username });
            return result;
        }
    }
}
