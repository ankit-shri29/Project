using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LoginAutomation.Pages
{
        public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private const string Url = "https://the-internet.herokuapp.com/login";

        private readonly By _usernameField = By.Id("username");
        private readonly By _passwordField = By.Id("password");
        private readonly By _loginButton = By.CssSelector("button[type='submit']");
        private readonly By _flashMessage = By.Id("flash");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public LoginPage NavigateTo()
        {
            _driver.Navigate().GoToUrl(Url);
            _wait.Until(d => d.FindElement(_usernameField).Displayed);
            return this;
        }

        public LoginPage EnterUsername(string username)
        {
            var field = _wait.Until(d => d.FindElement(_usernameField));
            field.Clear();
            field.SendKeys(username);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            var field = _driver.FindElement(_passwordField);
            field.Clear();
            field.SendKeys(password);
            return this;
        }

        public LoginPage ClickLogin()
        {
            _driver.FindElement(_loginButton).Click();
            return this;
        }
               
        public LoginPage Login(string username, string password)
        {
            return EnterUsername(username)
                .EnterPassword(password)
                .ClickLogin();
        }

        public string GetFlashMessageText()
        {
            var element = _wait.Until(d => d.FindElement(_flashMessage));
            return element.Text.Trim();
        }

        public bool IsFlashMessageDisplayed()
        {
            try
            {
                return _wait.Until(d => d.FindElement(_flashMessage)).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
