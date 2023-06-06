using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Operation.Models
{
    public class tblEmployeeAttendance
    {
        [Key]
        public int id { get; set; }

        public DateTime attendanceDate { get; set; }

        public int employeeId { get; set; }
        [ForeignKey("employeeId")]
        public tblEmployee employee { get; set; }

        public int isPresent { get; set; }
        public int isAbsent { get; set; }
        public int isOffday { get; set; }


    }
}
