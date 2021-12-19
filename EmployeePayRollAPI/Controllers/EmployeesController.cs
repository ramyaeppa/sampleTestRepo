using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeePayRollAPI.Data;
using EmployeePayRollAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace EmployeePayRollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeePayRollAPIContext _context;

        private readonly ILogger<EmployeesController> _logger;

        private readonly IDistributedCache _distributedCache;

        public EmployeesController(EmployeePayRollAPIContext context, ILogger<EmployeesController> logger, IDistributedCache distributedCache)
        {
            _context = context;
            _logger = logger;
            _distributedCache = distributedCache;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            _logger.LogInformation("log in app insight");
            _logger.LogError("LogError in app insight");
            _logger.LogCritical("LogCritical in app insight");
            _logger.LogDebug("LogDebug in app insight");
            _logger.LogTrace("LogTrace in app insight");

            return await _context.Employee.ToListAsync();
        }

        // GET: api/Employees/5
        [Authorize(Roles = "Api.ReadWrite,Api.ReadOnly")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            Employee employee;
            string empStr = _distributedCache.GetString("empObj_" + id.ToString());

            if (!string.IsNullOrEmpty(empStr))
            {
                employee = JsonConvert.DeserializeObject<Employee>(empStr);
            }
            else
            {
                employee = await _context.Employee.FindAsync(id);
                if (employee != null)
                    await _distributedCache.SetStringAsync("empObj_" + id.ToString(), JsonConvert.SerializeObject(employee));

                if (employee == null)
                {
                    return NotFound();
                }

               
            }
            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            await _distributedCache.SetStringAsync("empObj_" + id.ToString(), JsonConvert.SerializeObject(emp));
           
            _context.Entry(employee).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
