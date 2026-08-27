using LoginAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace LoginAutomation.Tests
{   
    public class LoginTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;

        private const string ValidUsername = "tomsmith";
        private const string ValidPassword = "SuperSecretPassword!";
        private const string ExpectedSuccessMessage = "You logged into a secure area!";

        public LoginTests()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1280,800");

            _driver = new ChromeDriver(options);
            _loginPage = new LoginPage(_driver);
        }

        [Fact]
        public void Login_WithValidCredentials_ShowsSuccessMessage()
        {            
            _loginPage.NavigateTo();

            _loginPage.Login(ValidUsername, ValidPassword);

            Assert.True(_loginPage.IsFlashMessageDisplayed(), "Flash message was not displayed after login.");
            Assert.Contains(ExpectedSuccessMessage, _loginPage.GetFlashMessageText());
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
