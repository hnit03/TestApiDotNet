using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task3.IRepositories;
using Task3.Models;

namespace Task3.Ropositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DbContext context;
        private readonly DbSet<Invoice> dbSet;
        private readonly ILogger<InvoiceRepository> logger;

        public InvoiceRepository(DbContext context, ILogger<InvoiceRepository> logger)
        {
            this.context = context;
            dbSet = this.context.Set<Invoice>();
            this.logger = logger;
        }
        public IEnumerable<Invoice> GetAllInvoice(string username)
        {
            try
            {
                var invoiceList = from invoice in dbSet
                                  where invoice.Username.Equals(username)
                                  select invoice;
                return invoiceList;
            }
            catch (Exception ex)
            {
                logger.LogError("InvoiceRepository (GetAllInvoice): " + ex.Message);
                return null;
            }
        }
    }
}
