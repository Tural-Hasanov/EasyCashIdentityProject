using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {

            if (ModelState.IsValid)
            {
                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);
                AppUser appuser = new AppUser()
                {
                    UserName = appUserRegisterDto.Username,
                    Name = appUserRegisterDto.Name,
                    Surname = appUserRegisterDto.Surname,
                    Email = appUserRegisterDto.Email,
                    City = "Baku",
                    ImageUrl = "xxx",
                    District = "8km",
                    ConfirmCode = code
                };
                var result = await _userManager.CreateAsync(appuser, appUserRegisterDto.Password);
                if (result.Succeeded)
                {

                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin Tural", "projecttraversal@gmail.com");
                    MailboxAddress mailboxAddressTo = new MailboxAddress("Receiver", appuser.Email);

                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);
                    mimeMessage.Subject = "6 rəqəmli kodu gizli saxlayın heç kəslə paylaşmayın";
                    var bodybuilder = new BodyBuilder();
                    bodybuilder.TextBody = $"Qeydiyyat prosesini başa çatdırmaq üçün qeyd olunan kodu nəzərə alın: {code}";
                    mimeMessage.Body = bodybuilder.ToMessageBody();
                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("projecttraversal@gmail.com", "bhwhllqajeopknty");
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                    TempData["Mail"] = appUserRegisterDto.Email;

                    return RedirectToAction("Index", "ConfirmMail");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View();
        }
    }
}
