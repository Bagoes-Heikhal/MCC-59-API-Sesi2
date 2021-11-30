using API.Hash;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            var cekNik = context.Employees.Find(registerVM.NIK);
            if (cekNik != null)
            {
                return 2;
            }

            var cekEmail = context.Employees.Where(a => a.Email == registerVM.Email).FirstOrDefault();
            if (cekEmail != null)
            {
                return 3;
            }

            var cekPhone = context.Employees.Where(a => a.Phone == registerVM.Phone).FirstOrDefault();
            if (cekPhone != null)
            {
                return 4;
            }

            Employee employee = new Employee();
            {
                employee.NIK = registerVM.NIK;
                employee.FirstName = registerVM.FirstName;
                employee.LastName = registerVM.LastName;
                employee.Gender = (registerVM.Gender == "Male")? Gender.Male : Gender.Female;
                employee.BirthDate = registerVM.BirthDate;
                employee.Email = registerVM.Email;
                employee.Salary = registerVM.Salary;
                employee.Phone = registerVM.Phone;
            }
            
            Account account = new Account();
            {
                account.NIK = registerVM.NIK;
                account.Password = Hashing.HashPassword(registerVM.Password);
            }
            
            var UniversityId = context.Universities.Find(registerVM.UniversityId);
            if (UniversityId == null)
            {
                return 5;
            }

            Education education = new Education();
            {
                education.GPA = registerVM.GPA;
                education.Degree = registerVM.Degree;
                education.UniversityId = registerVM.UniversityId;
            }

            context.Accounts.Add(account);
            context.Employees.Add(employee);
            context.Educations.Add(education);
            context.SaveChanges();

            Profiling profiling = new Profiling();
            {
                profiling.EducationId = education.EducationId;
                profiling.NIK = registerVM.NIK;
            }
            context.Profilings.Add(profiling);

            AccountRole accountRole = new AccountRole();
            {
                accountRole.AccountNIK = registerVM.NIK;
                accountRole.RoleId = 3; //3 Employee
            }
            context.AccountRoles.Add(accountRole);
            context.SaveChanges();
            return 1;
        }

        public IEnumerable<ProfileVM> GetRegistered()
        {
            var register = from a in context.Employees 
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
            return register.ToList();
        }
    }
}