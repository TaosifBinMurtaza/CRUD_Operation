using CRUD_Operation.Data;
using CRUD_Operation.DTOs;
using CRUD_Operation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CRUD_Operation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("/Update")]
        [HttpPost]
        public IActionResult UpdateEmployeeCode(int id, [FromBody] tblEmployeeDTO objDTO)
        {
            try
            {
                if (objDTO.employeeCode == null || objDTO.employeeCode == "")
                {
                    return BadRequest("Null");
                }
                else
                {
                    var result = _db.TblEmployees.FirstOrDefault(u => u.employeeId == id);
                    if (result != null)
                    {
                        var unique = _db.TblEmployees.FirstOrDefault(u => u.employeeCode == objDTO.employeeCode);
                        if (unique == null)
                        {
                            result.employeeCode = objDTO.employeeCode;
                            _db.TblEmployees.Update(result);
                            _db.SaveChanges();
                            return Ok("Employee Code have been created");
                        }

                        return BadRequest("Employee Code already exists");
                    }
                    return BadRequest("Employee does not exists");
                }
                

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Server Error");
            }

        }

        [Route("/GetBySalary")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _db.TblEmployees.OrderByDescending(u => u.employeeSalary);
            return Ok(result);
        }

        
        [Route("/AbsentEmployeeForAtLeastOneday")]
        [HttpGet]
        public IActionResult Absent()
        {
            var result = _db.TblEmployeeAttendances
              .Where(u => u.isAbsent == 1)
              .Include(u => u.employee)
              .GroupBy(x => x.employeeId)
              .Select(y => y.FirstOrDefault());

            return Ok(result);
        }
        [Route("/Report")]
        [HttpGet]
        public IActionResult Report()
        {   
            
            
            var result = _db.TblEmployeeAttendances.Include(u => u.employee)
                .GroupBy(u => new { u.attendanceDate.Month, u.employeeId, u.employee.employeeName , u.isPresent, u.isAbsent, u.isOffday })
                .Select(group => new {MonthName= CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month),
                    //group.Key.employeeId,
                    group.Key.employeeName ,
                    TotalPresent = group.Sum(s => s.isPresent),
                    TotalAbsent = group.Sum(s => s.isAbsent),
                    TotalOffday = group.Sum(s => s.isOffday) });
                        

            return Ok(result);
        }
    }
}
