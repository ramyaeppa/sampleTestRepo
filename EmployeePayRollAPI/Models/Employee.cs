using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayRollAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        //public IEnumerable<DepartmentDetail> Department { get; set; }
        public string Department { get; set; }
        public double? Salary { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? StartDate { get; set; }
        public string Notes { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? UpdatedDate { get; set; }

    }

    //public class DepartmentDetail
    //{
    //    [Key]
    //    public int DeptID { get; set; }
    //    public string DeptName { get; set; }

    //}

}
