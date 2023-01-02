using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject1_NUnit
{
    public class TestForRegistrationPage
    {
        IWebDriver webDriver;
        RegistrationPage registrationPage;
        string URL = "https://practice.automationtesting.in/my-account/";

        [SetUp]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
            registrationPage = new RegistrationPage(webDriver);
            webDriver.Navigate().GoToUrl(URL);
            webDriver.Manage().Window.Maximize();
        }
        [Test]
        [Category("PageObject")]
        [TestCase("Remember me")]
        public void RememberMeRightText(string expectedText)
        {
            Assert.IsTrue(registrationPage.GetRemenberMeText() == expectedText, "The text is wrong!");
        }

        [Test]
        [Category("PageObject")]
        [TestCase("Lost your password?")]
        public void LostPasswordRightText(string expectedText)
        {
            Assert.That(expectedText,
                Is.EqualTo(registrationPage.GetLostPasswordText()),
                $"The {expectedText} is not the same as {registrationPage.GetLostPasswordText()}");
        }

        [Test]
        [Category("PageObject")]
        [TestCase("Register")]
        public void RegisterButtonRightText(string expectedText)
        {
            Assert.AreEqual(expectedText, registrationPage.GetRegisterText());
        }

        [Test]
        [Category("PageObject")]
        [TestCase("katekate123.petqa@qa.com", "kateKate123!")]
        public void LogInHappy(string email, string password)
        {
            registrationPage.LoginIntoAccount(email, password);
            string expectedAccountName = registrationPage.GetAccountName();
            string currentAccountName = email.Substring(0, email.IndexOf("@"));
            Assert.That(expectedAccountName, Is.EqualTo(currentAccountName), 
                $"The {expectedAccountName} doesn't equal to {currentAccountName}");    
        }

        [Test]
        [Category("PageObject")]
        [TestCase("katekate@qa.com", "kateKate")]
        [TestCase("kate123@qa.com", "kateKate123!")]
        public void LogInFailEmail(string email, string password)
        {
            string expectedErrorText = "Error: A user could not be found with this email address.";
            registrationPage.LoginIntoAccount(email, password);
            string wrongEmailErrorText = registrationPage.GetCredentialsError();
            Assert.That(wrongEmailErrorText, Is.EqualTo(expectedErrorText));

        }
        [Test]
        [Category("PageObject")]
        [TestCase("katekate123.petqa@qa.com", "Kate123!")]
        public void LogInFailPassword(string email, string password)
        {
            string expectedErrorText = "Error: the password you entered for the username " + email + " is incorrect. Lost your password?";
            registrationPage.LoginIntoAccount(email, password);
            string wrongPasswordErrorText = registrationPage.GetCredentialsError();
            Assert.That(wrongPasswordErrorText, Is.EqualTo(expectedErrorText));
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Close();
        }
    }
}
