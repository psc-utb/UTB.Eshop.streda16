using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTB.Eshop.Web.Controllers;
using UTB.Eshop.Web.Models.ApplicationServices.Abstraction;
using UTB.Eshop.Web.Models.ViewModels;

namespace UTB.Eshop.Web.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        ISecurityApplicationService _securityService;
        public AccountController(ISecurityApplicationService securityService)
        {
            _securityService = securityService;
        }


        public IActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel registerVM)
        //{
        //    return View();
        //}


        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel loginVM)
        //{
        //    return View();
        //}


        //public async Task<IActionResult> Logout()
        //{
        //    return View();
        //}

    }
}
