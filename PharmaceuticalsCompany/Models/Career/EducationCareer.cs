using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PharmaceuticalsCompany.Models.Career
{
    public class EducationCareer
    {
        [Key]
        [Column(Order = 1)]
        public string User_Id { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Education_Id { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
