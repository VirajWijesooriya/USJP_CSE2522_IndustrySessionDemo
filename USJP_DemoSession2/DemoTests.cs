using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Reflection;

namespace USJP_DemoSession2
{
    public class DemoTests
    {
        [TestCase(TestName = "TC004 - LockedOutUserAddsCredentialsandTriesToLogin_ErrorMessagePopingUp")]
        public void Test1()
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

        [Test]
        public void Test2WebDriverNavigateAndManage()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.Navigate().GoToUrl("https://only-testing-blog.blogspot.com/2014/01/textbox.html");

            for (int i = 0; i < 100; i++)
            {
                driver.Navigate().Back();
                Thread.Sleep(500);
                driver.Manage().Window.Maximize();

                driver.Navigate().Forward();
                Thread.Sleep(500);
                driver.Manage().Window.Minimize();
            }
        }

        [Test]
        public void Test3FindElementAndFindElements()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://artoftesting.com/samplesiteforselenium");

            var btnDouleClickButton = driver.FindElement(By.Id("dblClkBtn"));
            var douleClickButtonText = btnDouleClickButton.Text;

            var btnDouleClickButtons = driver.FindElements(By.Id("dblClkBtn"));
            var douleClickButtonsText = btnDouleClickButtons[0].Text;
        }

        [Test]
        public void Test4iWebElementActions()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://artoftesting.com/samplesiteforselenium");

            var txtTextBox = driver.FindElement(By.Id("fname"));

            for (int i = 0; i < 100; i++)
            {
                txtTextBox.SendKeys("USJP Demo " + i);
                Thread.Sleep(100);

                txtTextBox.Clear();
                Thread.Sleep(100);
            }
        }

        [Test]
        public void Test5Locators()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://artoftesting.com/samplesiteforselenium");

            var txtTextBox1 = driver.FindElement(By.Id("fname"));
            var txtTextBox11 = driver.FindElement(By.CssSelector("#fname"));

            var txtTextBox2 = driver.FindElement(By.Name("firstName"));
            var txtTextBox22 = driver.FindElement(By.CssSelector("input[name='firstName']"));

            var txtTextBox3 = driver.FindElement(By.ClassName("main-navigation"));
            var txtTextBox33 = driver.FindElement(By.CssSelector(".main-navigation"));

            var txtTextBox4 = driver.FindElement(By.XPath("var txtTextBox2 = driver.FindElement(By.ClassName(\"main-navigation\"));"));

            var rdoMale = driver.FindElement(By.CssSelector("input[type='radio'][name='gender'][value='male']"));

            // parent child
            var elePtagsParentChild = driver.FindElements(By.CssSelector("#commonWebElements p"));

            // sibling
            var elePtagsSiblings = driver.FindElements(By.CssSelector("#commonWebElements p + p"));
        }

        [Test]
        public void Test6ActionsAndWaits()
        {
            var driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl("https://artoftesting.com/samplesiteforselenium");

            var btnDoubleClick = driver.FindElement(By.Id("dblClkBtn"));
            Actions actions = new Actions(driver);
            actions.DoubleClick(btnDoubleClick).Perform();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

            var alert = driver.SwitchTo().Alert();
            var alertText = alert.Text;

            alert.Accept();

            driver.Quit();
        }

        [Test]
        public void Test7Assertions()
        {
            var sumValue = Addition(5.214, 4.221);

            Assert.AreEqual(9.435, sumValue, "Values didn't match");
            Assert.IsTrue(9.435 == sumValue, "Values didn't match");
            Assert.That(sumValue, Is.EqualTo(9.435).Within(0.1), "Values didn't match");

            StringAssert.AreEqualIgnoringCase("Expected", "expected", "Strings didn't match");
            Assert.AreEqual("Expected", "expected", "Strings didn't match");
        }

        public static double Addition(double a, double b)
        {
            var sum = a + b;
            return sum;
        }
    }
}