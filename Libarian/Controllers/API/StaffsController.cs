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
        /*[HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user. == null)
            {
                return NotFound();
            }
            if (libraryCard.cardStatus == "Yes")
                libraryCard.cardStatus = "No";
            else libraryCard.cardStatus = "Yes";
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryCardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/
        
    }
}
