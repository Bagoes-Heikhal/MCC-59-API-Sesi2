using API.Base;
using API.Models;
using API.Repository;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private EmployeeRepository employeeRepository;
        public IConfiguration _iconfiguration;
        public EmployeesController(EmployeeRepository repository, IConfiguration configuration) : base(repository)
        {
            this.employeeRepository = repository;
            this._iconfiguration = configuration;
        }


        [HttpPost("Register")]
        public ActionResult Post(RegisterVM registerVM)
        {
            var result = employeeRepository.Register(registerVM);
            switch (result)
            {
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data telah berhasil di buat" });
                case 2:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "NIK telah digunakan" });
                case 3:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Email telah digunakan" });
                case 4:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Nomer handpone telah digunakan" });
                case 5:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Universitas tidak terdaftar" });
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, Message = $"Gagal memasukan data " });
            };
        }

        [Authorize(Roles = "Director, Manager")]
        [HttpGet("RegisteredData")]
        public ActionResult GetRegister()
        {
            var result = employeeRepository.GetRegistered();
           

            if (result.Count() != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data ditampilkan" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, Message = "Data belum tersedia" });
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS berhasil");
        }
    }
}

