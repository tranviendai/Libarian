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

        public ActorsController(UserManager<Libarian.Models.ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
    }
}
