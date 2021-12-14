using API.Base;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        private UniversityRepository universityRepository;
        public UniversitiesController(UniversityRepository repository) : base(repository)
        {
            this.universityRepository = repository;
        }


        [HttpGet("CountEmployeeUniversity")]
        public ActionResult CountEmployee()
        {
            var result = universityRepository.GetEmployees();
            if (result.Key != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data ditampilkan" });
            }
                return NotFound(new { status = HttpStatusCode.NotFound, Message = "Data belum tersedia" });
        }

        [HttpGet("CountUniv")]
        public ActionResult Counting()
        {
            var hitung = universityRepository.GetCountUniv();
            if (hitung != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = hitung, message = "Data Ditemukan" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = hitung, message = "Data tak ditemukan" });
        }
    }
}
