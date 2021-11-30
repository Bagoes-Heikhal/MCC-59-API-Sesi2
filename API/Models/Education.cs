using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_Education")]
    public class Education
    {
        [Key]
        public int EducationId { get; set; }
        public string Degree { get; set; }
        public int GPA { get; set; }

        [JsonIgnore]
        public virtual ICollection<Profiling> Profilings { get; set; }
        public int UniversityId { get; set; }

        [JsonIgnore]
        public virtual University University { get; set; }
    }
}
