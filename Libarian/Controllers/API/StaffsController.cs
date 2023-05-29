using Librarian.Data;
using Librarian.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Librarian.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StaffsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly ApplicationDbContext _context;


        public StaffsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetList()
        {
            var list = await _userManager.Users.ToListAsync();

            return Ok(list);
        }
        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.sex == "Yes")
                user.sex = "No";
            else user.sex = "Yes";
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutApplicationUser(string id, ApplicationUser applicationUser)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            user.sex = applicationUser.sex;
            user.address = applicationUser.address;
            user.fullName = applicationUser.fullName;
            user.birthday = applicationUser.birthday;
            user.Email = applicationUser.Email;
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }

            return NoContent();
        }
        //GET
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetApplycationUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
