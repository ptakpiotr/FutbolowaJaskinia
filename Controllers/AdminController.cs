using AutoMapper;
using FutbolowaJaskinia.Models;
using FutbolowaJaskinia.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolowaJaskinia.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AdminController(ILogger<AdminController> logger, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            var output = new List<ListUsersDTO>();

            foreach (var user in users)
            {
                var mapped = _mapper.Map<EditUserDTO>(user);
                var userRoles = await _userManager.GetRolesAsync(user);
                var userClaims = (await _userManager.GetClaimsAsync(user)).Select(c => c.Type).ToList();

                output.Add(new ListUsersDTO
                {
                    User = mapped,
                    UserRoles = userRoles.ToList(),
                    UserClaims = userClaims
                });
            }

            return View(output);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("Error");
            }
            else
            {
                var editUserDTO = _mapper.Map<EditUserDTO>(user);

                return View(editUserDTO);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string userId, EditUserDTO dto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("Error");
            }
            else
            {
                var inter = _mapper.Map<EditUserInterDTO>(dto);
                _mapper.Map(inter, user);

                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("Error");
            }
            else
            {
                var res = await _userManager.DeleteAsync(user);

                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            else
            {
                var allRoles = _roleManager.Roles.ToList();
                var userRoles = await _userManager.GetRolesAsync(user);
                var output = new List<EditRoleDTO>();

                ViewData["userId"] = userId;

                foreach (var role in allRoles)
                {
                    output.Add(new EditRoleDTO { RoleName = role.Name, IsChosen = userRoles.Contains(role.Name) });
                }

                return View(output);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(string userId, List<EditRoleDTO> editRoles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            else
            {
                var allRoles = _roleManager.Roles.ToList();

                await _userManager.RemoveFromRolesAsync(user, allRoles.Select(r => r.Name));

                await _userManager.AddToRolesAsync(user, editRoles.Where(r => r.IsChosen == true).Select(r => r.RoleName));

                return RedirectToAction("Index", "Admin");
            }
        }
    }
}
