using NUnit.Framework;
using MantisTestAutomation.Utils;
using MantisTestAutomation.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Collections.Generic;

namespace MantisTestAutomation.Tests
{
    [TestFixture]
    public class IssueReportingTests : TestBase
    {
        [Test]
        public void ReportIssueTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername(ConfigReader.Username);
            loginPage.EnterPassword(ConfigReader.Password);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            IWebElement reportIssueButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/bug_report_page.php']")));
            reportIssueButton.Click();

            Assert.IsTrue(driver.Url.Contains("bug_report_page.php"), "Falha ao acessar a página 'Relatar Problema'.");

            IWebElement categorySelect = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("category_id")));
            new SelectElement(categorySelect).SelectByIndex(1);

            IWebElement summaryInput = driver.FindElement(By.Id("summary"));
            summaryInput.SendKeys("Título do problema de teste");

            IWebElement descriptionInput = driver.FindElement(By.Id("description"));
            descriptionInput.SendKeys("Descrição detalhada do problema de teste.");

            IWebElement submitButton = driver.FindElement(By.XPath("//input[@value='Enviar incidência']"));
            submitButton.Click();

            CapturePageSource("ReportIssueTest_SuccessPage");

            if (IsElementPresent(By.CssSelector(".alert-success")))
            {
                IWebElement successMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-success")));
                Assert.IsTrue(successMessage.Displayed, "O problema não foi criado com sucesso.");
            }
            else
            {
                Assert.Fail("Mensagem de sucesso não encontrada após criar o problema.");
            }

            TakeScreenshot("ReportIssueTest_Passed");
        }

        [Test]
        public void ReportIssueLimitTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername(ConfigReader.Username);
            loginPage.EnterPassword(ConfigReader.Password);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            int issueCount = 11;

            for (int i = 1; i <= issueCount; i++)
            {
                try
                {
                    IWebElement reportIssueButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/bug_report_page.php']")));
                    reportIssueButton.Click();

                    Assert.IsTrue(driver.Url.Contains("bug_report_page.php"), "Falha ao acessar a página 'Relatar Problema'.");

                    IWebElement categorySelect = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("category_id")));
                    new SelectElement(categorySelect).SelectByIndex(1);

                    IWebElement summaryInput = driver.FindElement(By.Id("summary"));
                    summaryInput.SendKeys($"Título do problema de teste {i}");

                    IWebElement descriptionInput = driver.FindElement(By.Id("description"));
                    descriptionInput.SendKeys($"Descrição detalhada do problema de teste {i}.");

                    IWebElement submitButton = driver.FindElement(By.XPath("//input[@value='Enviar incidência']"));
                    submitButton.Click();

                    if (IsElementPresent(By.CssSelector(".alert-danger")))
                    {
                        IWebElement errorMessage = driver.FindElement(By.CssSelector(".alert-danger"));
                        string errorText = errorMessage.Text;
                        Assert.IsTrue(errorText.Contains("APPLICATION ERROR #27"), "A mensagem de erro esperada não foi exibida.");
                        TakeScreenshot($"ReportIssueLimitTest_Error_{i}");
                        break;
                    }
                    else if (IsElementPresent(By.CssSelector(".alert-success")))
                    {
                        IWebElement successMessage = driver.FindElement(By.CssSelector(".alert-success"));
                        Assert.IsTrue(successMessage.Displayed, "O problema não foi criado com sucesso.");
                        TakeScreenshot($"ReportIssueLimitTest_Success_{i}");
                    }

                    driver.Navigate().GoToUrl(ConfigReader.BaseUrl);
                }
                catch (Exception ex)
                {
                    TakeScreenshot($"ReportIssueLimitTest_Exception_{i}");
                    Assert.Fail($"O teste 'ReportIssueLimitTest' falhou na iteração {i}: " + ex.Message);
                }
            }
        }

        [Test]
        public void VerifyCreatedIssuesTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername(ConfigReader.Username);
            loginPage.EnterPassword(ConfigReader.Password);

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

                IWebElement viewIssuesLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/view_all_bug_page.php']")));
                viewIssuesLink.Click();

                Assert.IsTrue(driver.Url.Contains("view_all_bug_page.php"), "Falha ao acessar a página 'Ver Problemas'.");

                IWebElement issuesTable = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("buglist")));

                var issueRows = issuesTable.FindElements(By.CssSelector("tbody tr"));

                Assert.IsTrue(issueRows.Count > 0, "Nenhum problema encontrado na lista.");

                foreach (var row in issueRows)
                {
                    var issueIdElement = row.FindElement(By.CssSelector(".column-id a"));
                    var issueSummaryElement = row.FindElement(By.CssSelector(".column-summary"));

                    string issueId = issueIdElement.Text;
                    string issueSummary = issueSummaryElement.Text;

                    TestContext.Progress.WriteLine($"Problema encontrado - ID: {issueId}, Resumo: {issueSummary}");
                }

                TakeScreenshot("VerifyCreatedIssuesTest_Passed");
            }
            catch (Exception ex)
            {
                TakeScreenshot("VerifyCreatedIssuesTest_Failed");
                Assert.Fail("Teste 'VerifyCreatedIssuesTest' falhou: " + ex.Message);
            }
        }
    }
}
