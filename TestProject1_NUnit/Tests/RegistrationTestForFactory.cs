using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace TestProject1_NUnit
{
    public class RegistrationTestForFactory
    {
        IWebDriver webDriver;
        RegistrationPageFactory registrationPageFactory;
        string URL = "https://practice.automationtesting.in/my-account/";

        [SetUp]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
            registrationPageFactory = new RegistrationPageFactory(webDriver);
            webDriver.Navigate().GoToUrl(URL);
            webDriver.Manage().Window.Maximize();
        }

        [Test]
        [Category("Factory")]
        [TestCase("Remember me")]
        public void RememberMeRightText(string expectedText)
        {
            Assert.IsTrue(registrationPageFactory.GetRemenberMeText() == expectedText, "The text is wrong!");
        }

        [Test]
        [Category("Factory")]
        [TestCase("Lost your password?")]
        public void LostPasswordRightText(string expectedText)
        {
            Assert.That(expectedText,
                Is.EqualTo(registrationPageFactory.GetLostPasswordText()),
                $"The {expectedText} is not the same as {registrationPageFactory.GetLostPasswordText()}");
        }

        [Test]
        [Category("Factory")]
        [TestCase("Register")]
        public void RegisterButtonRightText(string expectedText)
        {
            Assert.AreEqual(expectedText, registrationPageFactory.GetRegisterText());
        }

        [Test]
        [Category("Factory")]
        [TestCase("katekate123.petqa@qa.com", "kateKate123!")]
        public void LogInHappy(string email, string password)
        {
            registrationPageFactory.LoginIntoAccount(email, password);
            string expectedAccountName = registrationPageFactory.GetAccountName();
            string currentAccountName = email.Substring(0, email.IndexOf("@"));
            Assert.That(expectedAccountName, Is.EqualTo(currentAccountName),
                $"The {expectedAccountName} doesn't equal to {currentAccountName}");
        }

        [Test]
        [Category("Factory")]
        [TestCase("katekate@qa.com", "kateKate")]
        [TestCase("kate123@qa.com", "kateKate123!")]
        public void LogInFailEmail(string email, string password)
        {
            string expectedErrorText = "Error: A user could not be found with this email address.";
            registrationPageFactory.LoginIntoAccount(email, password);
            string wrongEmailErrorText = registrationPageFactory.GetCredentialsError();
            Assert.That(wrongEmailErrorText, Is.EqualTo(expectedErrorText));

        }
        [Test]
        [Category("Factory")]
        [TestCase("katekate123.petqa@qa.com", "Kate123!")]
        public void LogInFailPassword(string email, string password)
        {
            string expectedErrorText = "Error: the password you entered for the username " + email + " is incorrect. Lost your password?";
            registrationPageFactory.LoginIntoAccount(email, password);
            string wrongPasswordErrorText = registrationPageFactory.GetCredentialsError();
            Assert.That(wrongPasswordErrorText, Is.EqualTo(expectedErrorText));
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }

    }
}
