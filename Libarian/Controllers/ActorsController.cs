using Libarian.Data;
using Libarian.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Libarian.Controllers
{
    public class ActorsController : Controller
    {
        private readonly UserManager<Libarian.Models.ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public ActorsController(UserManager<Libarian.Models.ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;

        }

        public async Task<IActionResult> List()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesModel = new List<UserRoleModel>();
            foreach (var user in users)
            {
                var thisModel = new UserRoleModel();
                thisModel.fullName = user.fullName;
                thisModel.address = user.address;
                thisModel.startProfile = user.startProfile;
                thisModel.birthday= user.birthday;
                thisModel.phone = user.PhoneNumber;
                thisModel.email = user.Email;
                thisModel.Roles = await GetUserRoles(user);
                userRolesModel.Add(thisModel);
            }
            return View(userRolesModel);
        }

        private async Task<IEnumerable<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        public async Task<IActionResult> Lock(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var applicationUser = await _db.Users.FirstOrDefaultAsync(m => m.Id ==id);
            if(applicationUser == null)
            {
                return NotFound();
            }
            applicationUser.LockoutEnd = DateTime.Now.AddYears(1000);
            await  _db.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> UnLock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var applicationUser = await _db.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            applicationUser.LockoutEnd = DateTime.Now;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }


    }
}
