using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EducationRepository : GeneralRepository<MyContext, Education, int>
    {
        public EducationRepository(MyContext myContext) : base(myContext)
        {

        }

    }
}
