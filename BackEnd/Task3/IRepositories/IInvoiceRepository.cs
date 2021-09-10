using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task3.Models;

namespace Task3.IRepositories
{
    public interface IInvoiceRepository
    {
        public IEnumerable<Invoice> GetAllInvoice(string username);
    }
}
