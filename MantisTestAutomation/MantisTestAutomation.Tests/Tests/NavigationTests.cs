using NUnit.Framework;
using MantisTestAutomation.Utils;
using MantisTestAutomation.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace MantisTestAutomation.Tests
{
    [TestFixture]
    public class NavigationTests : TestBase
    {
        [Test]
        public void SidebarLinksNavigationTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername(ConfigReader.Username);
            loginPage.EnterPassword(ConfigReader.Password);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            var sidebarLinks = new Dictionary<string, string>
            {
                { "my_view_page.php", "/my_view_page.php" },
                { "view_all_bug_page.php", "/view_all_bug_page.php" },
                { "bug_report_page.php", "/bug_report_page.php" },
                { "changelog_page.php", "/changelog_page.php" },
                { "roadmap_page.php", "/roadmap_page.php" }
            };

            foreach (var link in sidebarLinks)
            {
                try
                {
                    IWebElement sidebarLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector($"a[href='{link.Value}']")));
                    sidebarLink.Click();

                    Assert.IsTrue(driver.Url.Contains(link.Key), $"Falha ao navegar para '{link.Key}'.");

                    TakeScreenshot($"SidebarLinksNavigationTest_{link.Key}_Passed");

                    driver.Navigate().Back();
                }
                catch (Exception ex)
                {
                    TakeScreenshot($"SidebarLinksNavigationTest_{link.Key}_Failed");
                    Assert.Fail($"Teste 'SidebarLinksNavigationTest' falhou ao acessar '{link.Key}': " + ex.Message);
                }
            }
        }

        [Test]
        public void AccessMyAccountTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername(ConfigReader.Username);
            loginPage.EnterPassword(ConfigReader.Password);

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

                IWebElement userMenu = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.dropdown-toggle")));
                userMenu.Click();

                IWebElement myAccountLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Conta Pessoal")));
                myAccountLink.Click();

                Assert.IsTrue(driver.Url.Contains("account_page.php"), "Falha ao acessar 'Conta Pessoal'.");

                TakeScreenshot("AccessMyAccountTest_Passed");
            }
            catch (Exception ex)
            {
                TakeScreenshot("AccessMyAccountTest_Failed");
                Assert.Fail("Teste 'AccessMyAccountTest' falhou: " + ex.Message);
            }
        }
    }
}
