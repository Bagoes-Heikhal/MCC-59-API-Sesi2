using API.Models;
using API.ViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext context;
        public IConfiguration _configuration;
        public AccountRoleRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.context = myContext;
            this._configuration = configuration;
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
            var cekIfManager = (from a in context.AccountRoles
                               where a.AccountNIK == roleVM.NIK && a.RoleId == 2
                               select a.AccountNIK).FirstOrDefault();
            if (cekIfManager != null)
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
