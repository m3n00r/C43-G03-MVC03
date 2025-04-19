using Demo.DLL.Models.IdentityModel;
using Demo.Presentation.ViewModels.Roles;
using Demo.Presentation.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager, IWebHostEnvironment _environment) : Controller
    {

        #region Create

       [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = roleViewModel.Name
                });
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }
       


        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var rolesQuery = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue))
            {
                rolesQuery = rolesQuery.Where(E => E.Name.ToLower().Contains(SearchValue.ToLower()));
            }
            var rolesList = await rolesQuery.Select(
                E => new RoleViewModel
                {
                    Id = E.Id,
                    Name = E.Name
                   
                }).ToListAsync();
            return View(rolesList);
        }

        #endregion

        #region Details 
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null) return BadRequest();

            var Role = await _roleManager.FindByIdAsync(id);
            if (Role is null) return NotFound();

            var RoleViewModel = new RoleViewModel()

            {
                Id = Role.Id,
                Name = Role.Name
            };

            return View(RoleViewModel);

        }


        #endregion

        #region Update 

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if ( role is null)
                return NotFound();
            return View(new RoleViewModel
            {
              
                Id = role.Id,
               Name = role.Name
    
            });
        }

        [HttpPost]

        public async Task<IActionResult> Edit(string id, RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
                return View(roleViewModel);
            var message = string.Empty;
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                    return NotFound();
               role.Name = roleViewModel.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Role can not be updated";
            }

            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "Role can not be updated";
            }
            return View(roleViewModel);

        }

        #endregion

        #region Delete 
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound(); //404
            return View(new RoleViewModel
            {
   
                Id = id,
                Name = role.Name

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Action filter
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var message = string.Empty;
            try
            {
                if (role is not null)
                {
                    await _roleManager.DeleteAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                message = "An error happened while deleting the role";
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "An error happend when deleting the role";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        }

        #endregion
    }
}
