using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task3.IRepositories;

namespace Task3.Controllers
{
    [Route("api/invoiceDapper")]
    [ApiController]
    public class InvoiceDapperController : ControllerBase
    {
        private readonly IInvoiceDapper invoiceDapper;
        private readonly ILogger<InvoiceDapperController> logger;

        public InvoiceDapperController( ILogger<InvoiceDapperController> logger, IInvoiceDapper invoiceDapper)
        {
            this.logger = logger;
            this.invoiceDapper = invoiceDapper;
        }

        [HttpGet("getAllOwnInvoice")]
        public ActionResult GetAllOwnInvoice([FromHeader] string authorization)
        {
            try
            {

                var decode = new JwtSecurityTokenHandler().ReadToken(authorization.Substring(7)) as JwtSecurityToken;
                var username = decode.Claims.FirstOrDefault(claim => claim.Type == "Username").Value;
                var invoiceList = invoiceDapper.GetAllInvoice(username);
                return Ok(new { invoiceList });

            }
            catch (Exception ex)
            {
                logger.LogError("InvoiceDapperController (GetAllOwnInvoice): " + ex.Message);
                return BadRequest();
            }

        }
    }
}
