using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeePayRollAPI.Data;
using EmployeePayRollAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace EmployeePayRollAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminRegisterController : ControllerBase
    {
        private readonly EmployeePayRollAPIContext _context;

        public AdminRegisterController(EmployeePayRollAPIContext context)
        {
            _context = context;
        }
        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API    
        static readonly string[] scopeRequiredByApi = new string[] { "ReadWriteAccess" };

        // GET: api/Admin
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Registration>>> GetRegistration()
        //{
        //    //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        //    return await _context.Registration.ToListAsync();


        //}

        // GET: api/Admin/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Registration>> GetRegistration(int id)
        //{
        //  //  HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        //    var registration = await _context.Registration.FindAsync(id);

        //    if (registration == null)
        //    {
        //        return NotFound();
        //    }

        //    return registration;
        //}

        // PUT: api/Admin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRegistration(int id, Registration registration)
        //{
        //    if (id != registration.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(registration).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RegistrationExists(id))
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

        // POST: api/Admin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Api.ReadWrite,Api.ReadOnly")]
        [HttpPost]
        public async Task<ActionResult<Registration>> PostRegistration(Registration registration)
        {
            _context.Registration.Add(registration);           
            AdminLogin login = new AdminLogin();
            login.Email = registration.Email;
            login.Password = registration.Password;
            _context.AdminLogin.Add(login);
            await _context.SaveChangesAsync();

            //. return CreatedAtAction("AdminLogins/GetAdminLogin_1", login);
           
            return CreatedAtAction("GetRegistration", new { id = registration.Id }, registration);
        }
        public async Task<ActionResult<AdminLogin>> PostAdminLogin(AdminLogin adminLogin)
        {

            bool checkemail = _context.AdminLogin.Any(e => e.Email == adminLogin.Email);
            if (checkemail)
            {
                if (_context.AdminLogin.Where(x => x.Email == adminLogin.Email).FirstOrDefault().Password == adminLogin.Password)
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

        //// DELETE: api/Admin/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRegistration(int id)
        //{
        //    var registration = await _context.Registration.FindAsync(id);
        //    if (registration == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Registration.Remove(registration);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool RegistrationExists(int id)
        {
            return _context.Registration.Any(e => e.Id == id);
        }
    }
}
