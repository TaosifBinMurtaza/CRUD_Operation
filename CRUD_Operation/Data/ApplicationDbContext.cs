using CRUD_Operation.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operation.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<tblEmployee> TblEmployees { get; set; }
        public DbSet<tblEmployeeAttendance> TblEmployeeAttendances { get; set; }
    }
}
