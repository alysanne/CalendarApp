using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Tests
{
    public class SeleniumTests :IDisposable
    {
        IWebDriver driver = new ChromeDriver();
        
        public void Dispose()
        {
            driver.Quit();
        }

        [Fact]
        public void LoadLoginPage()
        {
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
            Assert.Equal("Log in - Bucks Calendar", driver.Title);
        }
        
        [Fact]
        public void LoadRegisterPage()
        {
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Register");
            Assert.Equal("Sign up - Bucks Calendar", driver.Title);
        }
        
        [Fact]
        public void LoadMainPage_NotLogged()
        {
            driver.Navigate().GoToUrl("https://localhost:5001/");
            Assert.Equal("Log in - Bucks Calendar", driver.Title);
        }
        
        [Fact]
        public void LoadMainPage_Logged()
        {
            Login();

            Assert.Equal("Index - Bucks Calendar", driver.Title);
        }
        
        [Fact]
        public void NavigateCalendar()
        {
            Login();
            NavigateToDec2019();
            
            Assert.Equal("December", driver.FindElement(By.Id("chosen-month")).Text);
            Assert.Equal("2019", driver.FindElement(By.Id("chosen-year")).Text);
        }
        
        [Fact]
        public void NavigateEventDetails()
        {
            Login();
            NavigateToDec2019();
            
            // Click Event
            driver.FindElement(By.CssSelector("#day-24 button")).Click();
            
            // Click Details in modal
            driver.FindElement(By.Id("modal-details")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            Assert.Equal("Details - Bucks Calendar", driver.Title);
        }
        
        [Fact]
        public void NavigateEditEvent()
        {
            Login();
            NavigateToDec2019();
            
            // Click Event
            driver.FindElement(By.CssSelector("#day-24 button")).Click();
            
            // Click Edit in modal
            driver.FindElement(By.Id("modal-edit")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            Assert.Equal("Edit - Bucks Calendar", driver.Title);
        }
        
        [Fact]
        public void NavigateDeleteEvent()
        {
            Login();
            NavigateToDec2019();
            
            // Click Event
            driver.FindElement(By.CssSelector("#day-24 button")).Click();
            
            // Click Edit in modal
            driver.FindElement(By.Id("modal-delete")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            Assert.Equal("Delete - Bucks Calendar", driver.Title);
        }

        private void NavigateToDec2019()
        {
            // Select December
            driver.FindElements(By.CssSelector(".bootstrap-select .dropdown-toggle .filter-option-inner-inner"))[0].Click();
            driver.FindElements(By.CssSelector("a.dropdown-item span"))[11].Click();

            // Select 2019
            driver.FindElements(By.CssSelector(".bootstrap-select .dropdown-toggle .filter-option-inner-inner"))[1].Click();
            driver.FindElements(By.CssSelector("a.dropdown-item span"))[12].Click();

            // Click Go!
            driver.FindElement(By.ClassName("go-button")).Click();
        }

        private void Login()
        {
            driver.Navigate().GoToUrl("https://localhost:5001/Identity/Account/Login");
            var email = driver.FindElement(By.Id("Input_Email"));
            email.SendKeys("theadmin@admin.com");

            var password = driver.FindElement(By.Id("Input_Password"));
            password.SendKeys("Password123!");
            
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
    }
}