using EasyCashIdentityProject.BusinessLayer.Abstract;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.DataAccessLayer.Migrations;
using EasyCashIdentityProject.DtoLayer.Dtos.CustomerAccountProcessDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    [Authorize]
    public class SendMoneyController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerAccountProcessService _customerAccountProcessService;

        public SendMoneyController(UserManager<AppUser> userManager, ICustomerAccountProcessService customerAccountProcessService)
        {
            _userManager = userManager;
            _customerAccountProcessService = customerAccountProcessService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SendMoneyForCustomerAccountProcessDto sendMoneyForCustomerAccountProcessDto)
        {
            var context = new Context();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var receiverAccountNumberID = context.CustomerAccounts.Where(x => x.CustomerAccountNumber == sendMoneyForCustomerAccountProcessDto.ReceiverAccountNumber).Select(y => y.CustomerAccountID).FirstOrDefault();
            var customusersenderID = context.CustomerAccounts.Where(x => x.AppUserID == user.Id).Select(y => y.CustomerAccountID).FirstOrDefault();
            //sendMoneyForCustomerAccountProcessDto.SenderID = user.Id;
            //sendMoneyForCustomerAccountProcessDto.ProcessDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            //sendMoneyForCustomerAccountProcessDto.ProcessType = "Hediyye";
            //sendMoneyForCustomerAccountProcessDto.ReceiverID = receiverAccountNumberID;

            var values = new CustomerAccountProcess();
            values.ProcessDate= Convert.ToDateTime(DateTime.Now.ToShortDateString());
            //values.SenderID=user.Id;
            values.SenderID = customusersenderID;
            values.ProcessType = "Hediyye";
            values.ReceiverID = receiverAccountNumberID;
            values.Amount = sendMoneyForCustomerAccountProcessDto.Amount;

            _customerAccountProcessService.TInsert(values);


            return RedirectToAction("Index", "Deneme");
        }
    }
}
