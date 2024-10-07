using OpenQA.Selenium;

namespace MantisTestAutomation.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        private By usernameField = By.Name("username");
        private By passwordField = By.Name("password");
        private By loginButton = By.XPath("//input[@type='submit']");

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void EnterUsername(string username)
        {
            driver.FindElement(usernameField).SendKeys(username);
            driver.FindElement(loginButton).Click();
        }

        public void EnterPassword(string password)
        {
            driver.FindElement(passwordField).SendKeys(password);
            driver.FindElement(loginButton).Click();
        }
    }
}
