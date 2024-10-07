using NUnit.Framework;
using MantisTestAutomation.Utils;
using MantisTestAutomation.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MantisTestAutomation.Tests
{
    [TestFixture]
    public class NegativeTests : TestBase
    {
        [Test]
        public void InvalidLoginTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername("usuario_invalido");
            loginPage.EnterPassword("senha_invalida");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-danger")));

            Assert.IsTrue(errorMessage.Displayed, "A mensagem de erro não foi exibida.");
            TakeScreenshot("InvalidLoginTest_Passed");
        }
    }
}
