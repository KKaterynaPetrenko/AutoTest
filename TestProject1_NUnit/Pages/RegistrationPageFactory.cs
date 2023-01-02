using OpenQA.Selenium;
using SeleniumExtras.PageObjects;


namespace TestProject1_NUnit
{
    public class RegistrationPageFactory
    {
        IWebDriver webdriver;

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement _userName;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement _password;

        [FindsBy(How = How.XPath, Using = "//input[@name='login']")]
        private IWebElement _loginButton;

        [FindsBy(How = How.Id, Using = "rememberme")]
        private IWebElement _rememberMeCheckbox;

        [FindsBy(How = How.XPath, Using = "//label[@for='rememberme']")]
        private IWebElement _rememberMe;

        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'lost-password')]")]
        private IWebElement _lostPasswordLink;

        [FindsBy(How = How.Id, Using = "reg_email")]
        private IWebElement _loginRegistrationField;

        [FindsBy(How = How.Id, Using = "reg_password")]
        private IWebElement _passwordRegistrationField;

        [FindsBy(How = How.XPath, Using = "//input[@value='Register']")]
        private IWebElement _registrationButton;

        [FindsBy(How = How.XPath, Using = "//p/strong")]
        private IWebElement _accountName;

        [FindsBy(How = How.XPath, Using = "//ul[@class='woocommerce-error']/li")]
        private IWebElement _wrongCredentialsError;

        public RegistrationPageFactory(IWebDriver webDriver)
        {
            this.webdriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }
        public RegistrationPageFactory LoginIntoAccount(string email, string password)
        {
            _userName.SendKeys(email);
            _password.SendKeys(password);
            _loginButton.Click();
            return this;
        }
        public string GetRemenberMeText()
        {
            return _rememberMe.Text;
        }
        public string GetRegisterText()
        {
            return _registrationButton.GetAttribute("value").ToString();
        }
        public string GetLostPasswordText()
        {
            return _lostPasswordLink.Text;
        }
        public string GetAccountName()
        {
            return _accountName.Text;
        }
        public string GetCredentialsError()
        {
            return _wrongCredentialsError.Text;
        }


    }
}
