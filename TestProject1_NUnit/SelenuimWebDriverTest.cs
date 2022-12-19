using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TestProject1_NUnit
{
    public class SelenuimWebDriverTest
    {
        IWebDriver webDriver;
        IJavaScriptExecutor js;

        [SetUp]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
           
        }

        [Test]
        [TestCase("html")]
        [TestCase("selenium")]
        [TestCase("web")]
        [TestCase("guide")]
        [Category("SeleniumHW")]
        public void SearchBySearchField(string searchWord)
        {
            string urlPractice = "https://practice.automationtesting.in/shop/";
            webDriver.Navigate().GoToUrl(urlPractice);
            IWebElement searchField = webDriver.FindElement(By.Id("s"));
            searchField.SendKeys(searchWord + Keys.Enter);

            IWebElement searchTitle = webDriver.FindElement(By.XPath("//h1[@class='page-title']/em"));
            StringAssert.AreEqualIgnoringCase(searchWord, searchTitle.Text);

        }

        [Test]
        [Category("SeleniumHW")]
        public void MatchReslt()
        {
            string urlPractice = "https://practice.automationtesting.in/?s=html";
            webDriver.Navigate().GoToUrl(urlPractice);
            IList<IWebElement> results = webDriver.FindElements(By.XPath("//h2[@class='post-title entry-title']/a"));

           Assert.Multiple(() =>
           {
               Assert.That(results.Select(x => x.Text), Is.All.Contains("html").IgnoreCase);
               Assert.That(results.Select(x => x.GetAttribute("href")), Is.All.Contains("https://"));
           });
        }

        [Test]
        [Category("SeleniumHW")]
        public void CheckTheSalePrice()
        {
            string urlPractice = "https://practice.automationtesting.in/?s=html";
            webDriver.Navigate().GoToUrl(urlPractice);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            HideGoogleAds();
 
            var element = webDriver.FindElement(By.XPath("//a[@title='Thinking in HTML']"));

            element.Click();
            HideGoogleAds();

            IWebElement saleIcon = webDriver.FindElement(By.XPath("//span[@class='onsale']"));
            IWebElement oldPrice = webDriver.FindElement(By.XPath("//del/span"));
            IWebElement newPrice = webDriver.FindElement(By.XPath("//ins/span"));

            Assert.Multiple(() =>
            {
                Assert.That(saleIcon.Displayed, Is.True, "Sale Label is not displayed");
                Assert.That(oldPrice.Displayed, Is.True, "Old Price is not displayed");
                Assert.That(newPrice.Displayed, Is.True, "NewPrice is not displayed");

            });
        }

        private void HideGoogleAds()
        {
            js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript(@"
                     var divsToHide = document.getElementsByClassName(""adsbygoogle"");
                     for (var i = 0; i < divsToHide.length; i++)
                     {
                       divsToHide[i].style.visibility = ""hidden"";
                       divsToHide[i].style.display = ""none"";
                     }"
            );
        }

        [Test]
        [TestCase(3)]
        [Category("SeleniumHW")]
        public void AddRelatedProductToBasket(int qty)
        {
            string urlPractice = "https://practice.automationtesting.in/product/thinking-in-html/";
            webDriver.Navigate().GoToUrl(urlPractice);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            HideGoogleAds();

            IList<IWebElement> relatedProductList = webDriver.FindElements(By.XPath("//div[@class='related products']//li//h3"));
            IWebElement html5Item = relatedProductList.Where(item => item.Text == "HTML5 WebApp Develpment").First();
            html5Item.Click();
            HideGoogleAds();
            html5Item.Click();

            var html5ItemName = (webDriver.FindElement(By.XPath("//h1[@itemprop='name']"))).Text;
            var price = webDriver.FindElement(By.XPath("//div[@itemprop='offers']//span")).Text;
            var html5ItemPrice = double.Parse(price.Substring(1));

            webDriver.FindElement(By.XPath(("//button[@type='submit']"))).Click();
            webDriver.FindElement(By.XPath("//a[@class='button wc-forward']")).Click();

            IWebElement qtyOfItem = webDriver.FindElement(By.XPath("//input[@type='number']"));
            qtyOfItem.Clear();
            qtyOfItem.SendKeys(qty.ToString());
            webDriver.FindElement(By.XPath("//input[@name='update_cart']")).Click();
            webDriver.FindElement(By.XPath("//a[@class='wpmenucart-contents']")).Click();

            var totalString = webDriver.FindElement(By.XPath("//td[@data-title='Total']/span")).Text;
            var total = double.Parse(totalString.Substring(1));
            var productName = (webDriver.FindElement(By.XPath("//td[@data-title='Product']/a"))).Text;


            Assert.Multiple(() =>
            {
                Assert.That(html5ItemName, Is.EqualTo(productName));
                Assert.That((html5ItemPrice * qty), Is.EqualTo(total));

            });

        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Close();
        }
    }
}
