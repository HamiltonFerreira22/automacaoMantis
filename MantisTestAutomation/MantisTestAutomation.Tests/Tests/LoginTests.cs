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
    public class LoginTests : TestBase
    {
        [Test]
        public void SuccessfulLoginTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername(ConfigReader.Username);
            loginPage.EnterPassword(ConfigReader.Password);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement userInfo = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a.dropdown-toggle > i.fa-user")));
            Assert.IsTrue(IsElementPresent(By.CssSelector("a.dropdown-toggle > i.fa-user")), $"Login falhou: usuário não está logado.");

            TakeScreenshot("SuccessfulLoginTest_Passed");
        }

        [Test]
        public void UnsuccessfulLoginTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername("usuario_invalido");
            loginPage.EnterPassword("senha_invalida");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-danger")));

            Assert.IsTrue(errorMessage.Displayed, "A mensagem de erro não foi exibida.");
            TakeScreenshot("UnsuccessfulLoginTest_Passed");
        }

        [Test]
        public void LogoutTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername(ConfigReader.Username);
            loginPage.EnterPassword(ConfigReader.Password);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            IWebElement userMenu = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.dropdown-toggle")));
            userMenu.Click();

            IWebElement logoutLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Sair")));
            logoutLink.Click();

            Assert.IsTrue(driver.Url.Contains("login_page.php"), "Falha ao fazer logout.");

            TakeScreenshot("LogoutTest_Passed");
        }
    }
}
