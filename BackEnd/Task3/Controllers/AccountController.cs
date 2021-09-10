using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task3.IRepositories;
using Task3.Models;
using Task3.IServices;

namespace Task3.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly ILogger<AccountController> logger;
        private readonly IAuthenticationService service;

        public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger, IAuthenticationService service)
        {
            this.accountRepository = accountRepository;
            this.logger = logger;
            this.service = service;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] Member member)
        {
            try
            {
                var user = accountRepository.Login(member.Username, member.Password);
                if(user != null)
                {
                    var token = service.GenerateJSONWebToken(user);
                    if (token != null)
                    {
                        return Ok(new { token, username = user.Username });
                    }
                    else
                    {
                        return BadRequest();
                    }
                    

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                logger.LogError("AccountController (Login): " + ex.Message);
                return BadRequest();
            }

        }
    }
}
