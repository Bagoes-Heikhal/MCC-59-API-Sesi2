using API.Models;
using API.ViewModel;
using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private AccountRepository accountRepository;

        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.accountRepository = repository;
        }

        //[HttpPost]
        //public JsonResult PostLogin(LoginVM register)
        //{
        //    var result = accountRepository.PostLogin(register);
        //    return Json(result);
        //}

        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await accountRepository.Auth(login);
            var token = jwtToken.Token;

            if (token == null)
            {
                //return RedirectToAction("Dashboard", "Employees");
                return Json(Url.Action("login", "Employees"));
            }

            HttpContext.Session.SetString("JWToken", token);
            //HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
            //HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");   
            return Json(Url.Action("Dashboard", "Employees"));
            //return RedirectToAction("Dashboard", "Employees");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            //var sessionData = HttpContext.Session.GetString("JWToken");

            //if (sessionData != null)
            //{
            //    HttpContext.Session.Clear();
            //    return RedirectToAction("Login", "Employees");
            //}

            return RedirectToAction("login", "Employees");
        }
    }
}
