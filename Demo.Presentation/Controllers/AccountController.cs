using Demo.DLL.Models.IdentityModel;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser>_userManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register() => View(); 

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid) return View(viewModel);

            var user = new ApplicationUser() 
            {
                UserName = viewModel.UserName ,
                Email = viewModel.Email ,
                FirstName = viewModel.FirstName ,
                LastName = viewModel.LastName ,

            };

            var Result = _userManager.CreateAsync(user,viewModel.Password).Result;
            if (Result.Succeeded) 
                return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(key: string.Empty, errorMessage: error.Description);
                }

                return View(model: viewModel);
            }


        }
        #endregion
    }
}
