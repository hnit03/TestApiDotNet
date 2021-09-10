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
    
    [Route("api/invoice")]
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

        [Authorize]
        [HttpGet("getAllOwnInvoice")]
        public ActionResult GetAllOwnInvoice([FromHeader] string authorization)
        {
            try
            {
                var decode = new JwtSecurityTokenHandler().ReadToken(authorization.Substring(7)) as JwtSecurityToken;
                var username = decode.Claims.FirstOrDefault(claim => claim.Type == "Username").Value;
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
