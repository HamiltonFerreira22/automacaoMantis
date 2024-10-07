using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System;
using System.IO;
using MantisTestAutomation.Utils;

namespace MantisTestAutomation.Tests
{
    public class TestBase
    {
        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            string browser = ConfigReader.Browser;

            if (browser == "Chrome")
            {
                driver = new ChromeDriver();
            }
            else if (browser == "Firefox")
            {
                driver = new FirefoxDriver();
            }
            else if (browser == "Edge")
            {
                driver = new EdgeDriver();
            }
            else
            {
                throw new ArgumentException("Navegador não suportado: " + browser);
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(ConfigReader.BaseUrl);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }

        protected void TakeScreenshot(string fileName)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fullPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, $"{fileName}_{timestamp}.png");
                screenshot.SaveAsFile(fullPath, ScreenshotImageFormat.Png);
                TestContext.AddTestAttachment(fullPath);
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine("Erro ao capturar screenshot: " + ex.Message);
            }
        }

        protected void CapturePageSource(string fileName)
        {
            try
            {
                string pageSource = driver.PageSource;
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fullPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, $"{fileName}_{timestamp}.html");
                File.WriteAllText(fullPath, pageSource);
                TestContext.AddTestAttachment(fullPath);
                TestContext.Progress.WriteLine($"Página fonte capturada e salva em: {fullPath}");
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine("Erro ao capturar a página fonte: " + ex.Message);
            }
        }

        protected bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
