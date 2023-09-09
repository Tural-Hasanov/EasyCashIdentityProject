using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if (ModelState.IsValid)
            {
                AppUser appuser = new AppUser()
                {
                    UserName = appUserRegisterDto.Username,
                    Name = appUserRegisterDto.Name,
                    Surname = appUserRegisterDto.Surname,
                    Email = appUserRegisterDto.Email
                };
                var result = await _userManager.CreateAsync(appuser, appUserRegisterDto.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "ConfirmMail");
                }

            }
            return View();
        }
    }
}
