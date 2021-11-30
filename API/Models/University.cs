using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    
    [Table("tb_m_University")]
    public class University
    {
        [Key]
        public int UniversityId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Education> Educations { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
