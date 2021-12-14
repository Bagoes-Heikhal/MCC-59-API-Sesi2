using API.Hash;
using API.Models;
using API.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;
        public IConfiguration _configuration;
        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.context = myContext;
            this._configuration = configuration;
        }

        public int Login(LoginVM loginVM)
        {
            var dataPass = (from a in context.Employees
                            where a.Email == loginVM.Email || a.Phone == loginVM.Phone
                            join b in context.Accounts on a.NIK equals b.NIK
                            select new { Account = b, Employee = a}).FirstOrDefault();

            if (dataPass == null)
            {
                return 4;
            }
            else if (dataPass.Employee.Email != null || dataPass.Employee.Phone != null)
            {
                var NIK = dataPass.Account.NIK;
                var cekPassword = Hashing.ValidatePassword(loginVM.Password, dataPass.Account.Password);
                if (cekPassword == true)
                {
                    return 1;
                }
                return 2;
            }
            return 3;
            
        }

        public IEnumerable<ProfileVM> Profile(LoginVM loginVM)
        {
            var profileVMs = from a in context.Employees where a.Email == loginVM.Email || a.Phone == loginVM.Phone
                             join b in context.Accounts on a.NIK equals b.NIK
                             join c in context.Profilings on b.NIK equals c.NIK
                             join d in context.Educations on c.EducationId equals d.EducationId
                             join e in context.Universities on d.UniversityId equals e.UniversityId
                             select new ProfileVM()
                             {
                                 NIK = a.NIK,
                                 Fullname = a.FirstName + " " + a.LastName,
                                 Gender = (a.Gender == 0) ? "Male" : "Female",
                                 Phone = a.Phone,
                                 Salary = a.Salary,
                                 Email = a.Email,
                                 GPA = d.GPA,
                                 Degree = d.Degree,
                                 UniversityName = e.Name
                             };
            return profileVMs.ToList();
        }

        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var OlddPass = (from a in context.Employees where a.Email == changePasswordVM.Email
                           join b in context.Accounts on a.NIK equals b.NIK
                           select b.NIK).Single();

            var data = context.Accounts.Find(OlddPass);
            if (data != null)
            {
                if (Hashing.ValidatePassword(changePasswordVM.CurrentPassword, data.Password))
                {
                    if (changePasswordVM.NewPassword == changePasswordVM.ConfirmPassword)
                    {
                        data.Password = Hashing.HashPassword(changePasswordVM.NewPassword);
                        context.SaveChanges();
                        return 1;
                    }
                    return 2;
                }
                return 3;
            }
            return 4;
        }

        public int ForgotPassword(ForgetPasswordVM forgetPasswordVM)
        {
            var OlddPass = (from a in context.Employees
                            where a.Email == forgetPasswordVM.Email
                            join b in context.Accounts on a.NIK equals b.NIK
                            select new { Employee = a, Account = b }).Single();

            string uniqueString = Guid.NewGuid().ToString();
            OlddPass.Account.Password = Hashing.HashPassword(uniqueString);
            context.SaveChanges();

            StringBuilder sb = new StringBuilder();
            sb.Append("<p> Use Forgot Password Menu to change your Password <p>");
            sb.Append("<p> Your new password is :  <p>");
            sb.Append($"<h1> {uniqueString} <h1>");

            if (OlddPass != null)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("testemailbagoes@gmail.com");
                    mail.To.Add(forgetPasswordVM.Email);
                    mail.Subject = $"Forgot Password {DateTime.Now}";
                    mail.Body = sb.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new 
                            NetworkCredential("testemailbagoes@gmail.com", "test123~~");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception)
                {
                    return 2;
                }
                return 1;
            }
            return 3;
        }

        public int SetRole(RoleVM roleVM)
        {
            var cekNik = context.Accounts.Find(roleVM.NIK);
            if (cekNik == null)
            {
                return 2;
            }
            var cekRole = context.Roles.Find(roleVM.RoleId);
            if (cekRole == null)
            {
                return 3;
            }
            var cekCurrentRole = (from a in context.AccountRoles
                                where a.AccountNIK == roleVM.NIK && a.RoleId == roleVM.RoleId
                                  select a.AccountNIK).FirstOrDefault();
            if (cekCurrentRole != null)
            {
                return 4;
            }

            AccountRole accountRole = new AccountRole();
            {
                accountRole.AccountNIK = roleVM.NIK;
                accountRole.RoleId = roleVM.RoleId;
            }

            context.AccountRoles.Add(accountRole);

            return context.SaveChanges();
        }

    }
}
