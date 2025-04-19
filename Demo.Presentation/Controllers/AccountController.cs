using Demo.DLL.Models.IdentityModel;
using Demo.Presentation.Utillites;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Email = Demo.Presentation.Utillites.Email;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager
        , SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var user = new ApplicationUser()
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,

            };

            var Result = _userManager.CreateAsync(user, viewModel.Password).Result;
            if (Result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError( string.Empty, error.Description);
                }

                return View( viewModel);
            }


        }
        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var User = _userManager.FindByEmailAsync(viewModel.Email).Result;

            if (User is not null)
            {
                bool Flag = _userManager.CheckPasswordAsync(User, viewModel.Password).Result;
                if (Flag)
                {
                    var Result = _signInManager.PasswordSignInAsync(User, viewModel.Password, viewModel.RememberMe, false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "your Account Is not allowed");
                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "your Account Is Locked out");
                    if (Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");

                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(viewModel);
        }

        #endregion

        #region SignOut
        [HttpGet]
        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region ForgetPassword
        [HttpGet]

        public IActionResult ForgetPassword()=>View();
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (User is not null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(User).Result;
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = viewModel.Email, Token }, Request.Scheme);
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = "Reset Password Link"  //ToDo
                    };

                    //send Email
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CkeckYourInbox));
                }

            }
            ModelState.AddModelError(string.Empty, "Invalid Operaction");
            return View(nameof(ForgetPassword), viewModel);
        }

            [HttpGet]
            public IActionResult CkeckYourInbox() => View();
            [HttpGet]
            public IActionResult ResetPassword (string email ,string Token)
            {
                TempData["email"] = email;
                TempData["Token"] = Token;
                return View();
            }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)return View(viewModel);
            string email=TempData["email"] as string ?? string.Empty;
            string Token = TempData["Token"] as string ?? string.Empty;
            var User = _userManager.FindByEmailAsync(email).Result;
            if (User is not null) 
            {
              var Result = _userManager.ResetPasswordAsync(User, Token, viewModel.Password).Result;
               if(Result.Succeeded) 
               return RedirectToAction(nameof(Login));
               else
                {
                    foreach(var error in  Result.Errors)
                        ModelState.AddModelError(string.Empty,error.Description);
                            
                }

            }
            return View(nameof(ResetPassword),viewModel);

        }

        #endregion

    }

}
