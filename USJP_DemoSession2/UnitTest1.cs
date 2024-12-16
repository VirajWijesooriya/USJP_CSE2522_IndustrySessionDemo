using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace USJP_DemoSession2
{
    public class SwagLabsTests
    {
        [Test]
        public void LockedOutUserAddsCredentialsAndTriesToLogin_ErrorMessagePopingUp()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            var txtUserName = driver.FindElement(By.Id("user-name"));
            txtUserName.SendKeys("locked_out_user");

            var txtUserPassword = driver.FindElement(By.Id("password"));
            txtUserPassword.SendKeys("secret_sauce");

            var btnLogin = driver.FindElement(By.Id("login-button"));
            btnLogin.Click();

            var eleErrorMessage = driver.FindElement(By.CssSelector("h3[data-test='error']"));
            var errorMessageText = eleErrorMessage.Text;

            Assert.That(errorMessageText, Is.EqualTo("Epic sadface: Sorry, this user has been locked out."), 
                "Strings didn't match");

            driver.Quit();

        }
    }
}