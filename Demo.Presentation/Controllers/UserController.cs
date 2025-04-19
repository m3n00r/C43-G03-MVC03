using Demo.BLL.DataTransFerObjects.EmployeeDtos;
using Demo.DLL.Models.EmployeeModel;
using System;
using Demo.DLL.Models.IdentityModel;
using Demo.DLL.Models.Shared.Enums;
using Demo.Presentation.ViewModels;
using Demo.Presentation.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager, IWebHostEnvironment _environment) : Controller
    {
        private readonly IWebHostEnvironment environment = _environment;

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var usersQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue))
            {
                usersQuery = usersQuery.Where(E => E.Email.ToLower().Contains(SearchValue.ToLower()));
            }
            var usersList = await usersQuery.Select(
                E => new UserViewModel
                {
                    Id = E.Id,
                    FirstName = E.FirstName,
                    LastName = E.LastName,
                    Email = E.Email
                }).ToListAsync();
            foreach (var user in usersList)
            {
                user.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
            }
            return View(usersList);
        }

        #endregion

        #region Details 
        [HttpGet]
        public  async Task<IActionResult>Details(string? id)
        {
            if (id is null) return BadRequest();

            var User =await _userManager.FindByIdAsync(id);
            if (User is null) return NotFound();

            var userViewModel = new UserViewModel()

            {
                Id =User .Id,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                Roles = _userManager.GetRolesAsync(User).Result

            };

            return View(userViewModel);

        }


        #endregion

        #region Update

        [HttpGet] 
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest(); 
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound(); 
            return View(new UserViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Roles = _userManager.GetRolesAsync(user).Result
            });
        }

        [HttpPost]
   
        public async Task<IActionResult> Edit(string id, UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return View(userViewModel);
            var message = string.Empty;
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                    return NotFound();
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Email = userViewModel.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "User can not be updated";
            }

            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "User can not be updated";
            }
            return View(userViewModel);

        }

        #endregion

        #region Delete 
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound(); //404
            return View(new UserViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Action filter
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var message = string.Empty;
            try
            {
                if (user is not null)
                {
                    await _userManager.DeleteAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                message = "An error happened while deleting the user";
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "An error happend when deleting the user";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        }

        #endregion
    }
}
