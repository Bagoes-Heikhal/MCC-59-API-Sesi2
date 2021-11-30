using API.Base;
using API.Models;
using API.Repository;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private AccountRoleRepository accountRoleRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public AcountRolesController(AccountRoleRepository repository, IConfiguration configuration, MyContext context) : base(repository)
        {
            this.accountRoleRepository = repository;
            this._configuration = configuration;
            this.context = context;
        }

        [Authorize(Roles = "Director")]
        [HttpGet("Ping/Director")]
        public ActionResult PingDirector()
        {
            return Ok("Ping Director");
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("Ping/Employee")]
        public ActionResult PingEmployee()
        {
            return Ok("Ping Employee");
        }

        [Authorize(Roles = "Director")]
        [HttpPost("AddManager")]
        public ActionResult SignManager(RoleVM roleVM)
        {
            var result = accountRoleRepository.SetRole(roleVM);
            switch (result)
            {
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Manager baru ditambahkan" });
                case 2:
                    return NotFound(new { status = HttpStatusCode.NotFound, Message = $"NIK tidak terdaftar" });
                case 3:
                    return NotFound(new { status = HttpStatusCode.NotFound, Message = $"Role tidak tersedia" });
                case 4:
                    return NotFound(new { status = HttpStatusCode.NotFound, Message = $"Sudah menjadi manager" });
                default:
                    return NotFound(new { status = HttpStatusCode.NotFound, Message = "Gagal menambahkan manager" });
            }
    }

    }
}
