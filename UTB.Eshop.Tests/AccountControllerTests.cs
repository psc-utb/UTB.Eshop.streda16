using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Xunit;
using Moq;

using UTB.Eshop.Web.Areas.Security.Controllers;
using UTB.Eshop.Web.Controllers;
using UTB.Eshop.Web.Models.ApplicationServices.Abstraction;
using UTB.Eshop.Web.Models.ViewModels;
using UTB.Eshop.Tests.Helpers;

namespace UTB.Eshop.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task Login_ValidSuccess()
        {
            // Arrange
            var mockISecurityApplicationService = new Mock<ISecurityApplicationService>();
            mockISecurityApplicationService.Setup(security => security.Login(It.IsAny<LoginViewModel>()))
                                                                      //prvni verze, kdy proste rekneme, ze login projde a hotovo :-)
                                                                      .Returns(() => Task<bool>.Run(() => true));
                                                                      //druha verze, kdy si muzeme testovat, co je v LoginViewModel (toto delame spis jen tehdy, kdy mame setup v samostatne metode -> tzn., ze se pouziva ve vice testech, kde potrebujeme nekdy vratit true a nekdy false):
                                                                      //.Returns<LoginViewModel>((loginVM) => {return Task<bool>.Run(() =>
                                                                      //{
                                                                      //    if (loginVM.Username == "superadmin" && loginVM.Password == "123")
                                                                      //    { return true; }
                                                                      //    else
                                                                      //    { return false; }
                                                                      //});});


            LoginViewModel loginViewModel = new LoginViewModel()
            {
                Username = "superadmin",
                Password = "123"
            };


            AccountController controller = new AccountController(mockISecurityApplicationService.Object);

            //pokud chci vypnout validaci, tak nenastavuju ObjectValidator
            //(je to na vas, jak to u Unit Testu udelate, ale pokud v controlleru pouzivate TryValidateModel(model), tak jej nejak nastavit musite! (na vstup ObjectValidator muzete nastavit true a validace se neprovede ani skrz Vali) Popr. i tehdy, pokud chcete testovat pripad, kdy objekt na vstupu neni validni - tento pripad je ale zalezitost integracnich testu)
            //controller.ObjectValidator = new ObjectValidator(false);
            //controller.TryValidateModel(loginViewModel);

            //u testovani nevalidniho vstupu (pro unit testy) se pro oddeleni logiky validace a controlleru doporucuje primo pridat vlastni chybu do ModelState
            //controller.ModelState.AddModelError(nameof(LoginViewModel.Username), $"{nameof(LoginViewModel.Username)} is not set");

            IActionResult iActionResult = null;

            //Act
            iActionResult = await controller.Login(loginViewModel);


            // Assert
            RedirectToActionResult redirect = Assert.IsType<RedirectToActionResult>(iActionResult);
            Assert.Matches(redirect.ActionName, nameof(HomeController.Index));
            Assert.Matches(redirect.ControllerName, nameof(HomeController).Replace("Controller", String.Empty));
            Assert.Matches(redirect.RouteValues.Single((pair) => pair.Key == "area").Value.ToString(), String.Empty);


        }
    }
}
