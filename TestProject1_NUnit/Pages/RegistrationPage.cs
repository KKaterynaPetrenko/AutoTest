using OpenQA.Selenium;


namespace TestProject1_NUnit
{
    public class RegistrationPage
    {
        IWebDriver webDriver;
        private IWebElement _username;
        private IWebElement _password;
        private IWebElement _loginButton;
        private IWebElement _rememberMeCheckbox;
        private IWebElement _rememberMe;
        private IWebElement _lostPasswordLink;
        private IWebElement _loginRegistrationField;
        private IWebElement _passwordRegistrationField;
        private IWebElement _registrationButton;
        private IWebElement _accountNameAfterHappyLogin; //to check that registration is happy
        private IWebElement _wrongCredentialsError;

        public RegistrationPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        private IWebElement nameLoginField => _username ??= webDriver.FindElement(By.Id("username"));
        private IWebElement passwordLoginField => _password ??= webDriver.FindElement(By.Id("password"));
        private IWebElement loginButtonField => _loginButton ??= webDriver.FindElement(By.XPath("//input[@name='login']"));
        private IWebElement rememberMeCheckbox => _rememberMeCheckbox ??= webDriver.FindElement(By.Id("rememberme"));
        private IWebElement rememeberMe => _rememberMe ??= webDriver.FindElement(By.XPath("//label[@for='rememberme']"));
        private IWebElement lostPasswordLink => _lostPasswordLink ??= webDriver.FindElement(By.XPath("//a[contains(@href,'lost-password')]"));
        private IWebElement loginRegistrationField => _loginRegistrationField ??= webDriver.FindElement(By.Id("reg_email"));
        private IWebElement passwordRegistrationField => _passwordRegistrationField ??= webDriver.FindElement(By.Id("reg_password"));
        private IWebElement registrationButton => _registrationButton ??= webDriver.FindElement(By.XPath("//input[@value='Register']"));
        private IWebElement accountName => _accountNameAfterHappyLogin ??= webDriver.FindElement(By.XPath("//p/strong"));
        private IWebElement wrongCredentialsError => _wrongCredentialsError ??= webDriver.FindElement(By.XPath("//ul[@class='woocommerce-error']/li"));

        public RegistrationPage LoginIntoAccount(string email, string password)
        {
            nameLoginField.SendKeys(email);
            passwordLoginField.SendKeys(password);
            loginButtonField.Click();
            return this;
        }
        public string GetRemenberMeText()
        {
            return rememeberMe.Text;
        }
        public string GetRegisterText()
        {
            return registrationButton.GetAttribute("value").ToString();
        }
        public string GetLostPasswordText()
        {
            return lostPasswordLink.Text;
        }
        public string GetAccountName()
        {
            return accountName.Text;
        }
        public string GetCredentialsError()
        {
            return wrongCredentialsError.Text;
        }
    }
}
