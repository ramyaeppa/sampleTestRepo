using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayRollAPI.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? UpdatedDate { get; set; }

    }
}
