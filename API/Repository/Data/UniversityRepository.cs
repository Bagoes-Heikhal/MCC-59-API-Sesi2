using API.Models;
using API.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        private readonly MyContext context;
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public KeyValuePair<List<string>, List<int>> GetEmployees()
        {
            var data = (from c in context.Profilings
                       join d in context.Educations on c.EducationId equals d.EducationId
                       join e in context.Universities on d.UniversityId equals e.UniversityId
                       group e by e.Name into f
                       select new 
                       { 
                            Name = f.Key,
                            Students = f.Count()
                       }).ToList();

            List<string> univName = new List<string>();
            List<int> univStudent = new List<int>();

            foreach (var item in data)
            {
                univName.Add(item.Name);
                univStudent.Add(item.Students);
            }
            
            return new KeyValuePair<List<string>, List<int>>(univName, univStudent);
        }


        public IEnumerable GetCountUniv()
        {
            var univ = (from p in context.Profilings
                        join ed in context.Educations on p.EducationId equals ed.EducationId
                        join u in context.Universities on ed.UniversityId equals u.UniversityId
                        group u by u.Name into cnt
                        select new
                        {
                            UniversityName = cnt.Key,
                            CountStudent = cnt.Count()
                        }).AsEnumerable();
            return univ.ToList();
        }
    }
}
