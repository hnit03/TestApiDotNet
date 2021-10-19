using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task3.IRepositories;
using Task3.Models;

namespace Task3.Controllers
{
    
    [Route("api/product")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly ILogger<InvoiceController> logger;

        public InvoiceController(IInvoiceRepository invoiceRepository, ILogger<InvoiceController> logger)
        {
            this.invoiceRepository = invoiceRepository;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult GetAllProduct([FromHeader] string authorization)
        {
            try
            {
                var invoiceList = invoiceRepository.GetAllInvoice(username);
                return Ok(new { invoiceList });
            }
            catch (Exception ex)
            {
                logger.LogError("InvoiceController (GetAllOwnInvoice): " + ex.Message);
                return BadRequest();
            }

        }
    }
}
