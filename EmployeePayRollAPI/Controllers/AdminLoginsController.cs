using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using EmployeePayRollAPI.Data;
using EmployeePayRollAPI.Models;

namespace EmployeePayRollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginsController : ControllerBase
    {
        private readonly EmployeePayRollAPIContext _context;

        public AdminLoginsController(EmployeePayRollAPIContext context)
        {
            _context = context;
        }

        // GET: api/AdminLogins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminLogin>>> GetAdminLogin()
        {
            return await _context.AdminLogin.ToListAsync();
        }

        // GET: api/AdminLogins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminLogin>> GetAdminLogin(string id)
        {
            var adminLogin = await _context.AdminLogin.FindAsync(id);

            if (adminLogin == null)
            {
                return NotFound();
            }

            return adminLogin;
        }

        //// PUT: api/AdminLogins/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAdminLogin(string id, AdminLogin adminLogin)
        //{
        //    if (id != adminLogin.Email)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(adminLogin).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AdminLoginExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/AdminLogins
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminLogin>> PostAdminLogin(AdminLogin adminLogin)
        {

            bool checkemail = _context.AdminLogin.Any(e => e.Email == adminLogin.Email);
            if(checkemail)
            {
                if(_context.AdminLogin.Where(x=>x.Email==adminLogin.Email).FirstOrDefault().Password==adminLogin.Password)
                {
                    return CreatedAtAction("GetAdminLogin", new { id = adminLogin.Email }, adminLogin);

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
            //_context.AdminLogin.Find(adminLogin.Email)
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    if (AdminLoginExists(adminLogin.Email))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            
        }

        //// DELETE: api/AdminLogins/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAdminLogin(string id)
        //{
        //    var adminLogin = await _context.AdminLogin.FindAsync(id);
        //    if (adminLogin == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.AdminLogin.Remove(adminLogin);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool AdminLoginExists(string id)
        {
            return _context.AdminLogin.Any(e => e.Email == id);
        }
    }
}
