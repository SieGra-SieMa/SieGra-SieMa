using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs;
using SieGraSieMa.Services;
using SieGraSieMa.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("create")]
        public ActionResult Create(AccountRequestDTO accountRequestDTO)
        {
            try
            {
                var account = _accountService.Create(accountRequestDTO);
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("authorize")]
        public IActionResult Authorize(CredentialsDTO credentialsDTO)
        {
            try
            {
                var account = _accountService.Authorize(credentialsDTO);
                return Ok(account);
            }
            catch (Exception)
            {
                return Forbid();
            }
        }
    }
}
