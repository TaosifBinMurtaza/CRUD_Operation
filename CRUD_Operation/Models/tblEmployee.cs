using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation.Models
{
    public class tblEmployee
    {
        [Key]
        public int employeeId { get; set; }
        public string employeeName { get; set;}

        public string employeeCode { get; set;}

        public int employeeSalary { get; set;}

    }
}
