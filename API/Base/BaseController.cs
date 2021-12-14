using API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = repository.Get();
            if (result.Count() != 0)
            {
                //return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data ditampilkan" });
                return Ok(result);

            }
            return NotFound(new { status = HttpStatusCode.NotFound, Message = $"Data belum tersedia" });
        }

        [HttpGet("{Key}")]
        public ActionResult Get(Key key)
        {
            var result = repository.Get(key);
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound(new { status = HttpStatusCode.NotFound, Message = $"Data tidak ditemukan 11" });
        }

        [HttpDelete("{Key}")]
        public ActionResult Delete(Key key)
        {
            var result = repository.Delete(key);
            try
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data telah dihaspus" });
            }
            catch (NullReferenceException)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, Message = $"Data tidak ditemukan" });
            }
        }

            [HttpPut]
            public ActionResult Update(Entity entity, Key key)
            {
                var result = repository.Update(entity, key);
                try
                {
                    return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data terupdate" });
                }
                catch (Exception)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Gagal update" });
                }
            }

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            try {
                var result = repository.Insert(entity);
                return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data telah berhasil di buat" });
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Gagal memasukan data" });
            }
        }
    }
}
