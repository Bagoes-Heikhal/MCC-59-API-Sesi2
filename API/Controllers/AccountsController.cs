using API.Base;
using API.Models;
using API.Repository;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private AccountRepository accountRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public AccountsController(AccountRepository repository, IConfiguration configuration, MyContext context) : base(repository)
        {
            this.accountRepository = repository;
            this._configuration = configuration;
            this.context = context;
        }


        [HttpPost("Login")]
        public ActionResult Post(LoginVM loginVM)
        {
            var result = accountRepository.Login(loginVM);
            switch (result)
            {
                case 1:
                    var getUserData = (from a in context.Employees
                                       where a.Email == loginVM.Email || a.Phone == loginVM.Phone
                                       join b in context.AccountRoles on a.NIK equals b.AccountNIK
                                       join c in context.Roles on b.RoleId equals c.RoleId
                                       select new {
                                           Employee = a.Email,
                                           Role = c.Name
                                       }).ToList();
                    
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Email, getUserData[0].Employee)
                    };

                    foreach (var userRole in getUserData)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );

                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("TokenSecurity", idtoken.ToString()));

                    return Ok(new JWTokenVM { Token = idtoken, Messages = "Login Sucsses" });
                    //return Ok(new { Status = HttpStatusCode.OK, Token = idtoken, Profile = accountRepository.Profile(loginVM), Message = $"Login Berhasil" });
                case 2:
                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = $"Password salah" });
                case 3:
                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = $"Akun tidak ditemukan" });
                default:
                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = $"Gagal Login" });
            }
        }

        [HttpPut("ChangePassword")]
        public ActionResult ChangeOldPassword(ChangePasswordVM changePasswordVM)
        {
            try
            {
                var result = accountRepository.ChangePassword(changePasswordVM);
                switch (result)
                {
                    case 1:
                        return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Password berhasil diubah" });
                    case 2:
                        return BadRequest(new { status = HttpStatusCode.BadRequest, Message = $"Komfirmasi pasword salah" });
                    case 3:
                        return BadRequest(new { status = HttpStatusCode.BadRequest, Message = $"Password lama anda salah" });
                    case 4:
                        return BadRequest(new { status = HttpStatusCode.BadRequest, Message = $"Akun tidak ditemukan" });
                    default:
                        return BadRequest(new { status = HttpStatusCode.BadRequest, Message = $"Gagal mengubah Password" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = $"Akun tidak ditemukan" });
            }
        }

        [HttpPost("ForgotPassword")]
        public ActionResult ForgotOldPassword(ForgetPasswordVM forgetPasswordVM)
        {
            try
            {
                var result = accountRepository.ForgotPassword(forgetPasswordVM);
                switch (result)
                {
                    case 1:
                        return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Password berhasil diubah. Cek Email atau Spam" });
                    case 2:
                        return BadRequest(new { status = HttpStatusCode.BadRequest, Message = $"Gagal mengirim Email" });
                    case 3:
                        return BadRequest(new { status = HttpStatusCode.BadRequest, Message = $"Akun tidak ditemukan" });
                    default:
                        return BadRequest(new { status = HttpStatusCode.BadRequest, Message = $"Gagal melakukan perintah forgot password" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = $"Akun tidak ditemukan" });
            }
        }

        [HttpPost("SetRole")]
        public ActionResult SetRole(RoleVM roleVM)
        {
            var result = accountRepository.SetRole(roleVM);
            switch (result)
            {
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Role berhasil ditambahkan" });
                case 2:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, Message = "NIK tidak ditemukan" });
                case 3:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, Message = "Role tidak ditemukan" });
                case 4:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, Message = "Sudah memiliki Role yang ditambahkan" });
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, Message = "Gagal mengupload Role" });
            }
            
        }
        
        [HttpGet("Profile/{Key}")]
        public ActionResult GetProfile(LoginVM Key)
        {
            var result = accountRepository.Profile(Key);
            if (result != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data ditampilkan" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, Message = "Data belum tersedia" });
        }

        [Authorize(Roles = "Director")]
        [HttpGet("ping/admin")]
        public ActionResult TestJWT()
        {
            return Ok("Test Jwt berhasil");
        }



    }
}
