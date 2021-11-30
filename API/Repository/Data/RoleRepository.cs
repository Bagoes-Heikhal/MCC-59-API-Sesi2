using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RoleRepository : GeneralRepository<MyContext, Role, int>
    {
        public RoleRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
