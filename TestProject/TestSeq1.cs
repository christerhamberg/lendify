using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace TestProject
{
    class TestSeq1 : ITestSequence
    {

        private const String testDescription = "This is just a short demo of Selenium";

        public String getTestDescription()
        {
            return testDescription;
        }


        public void runTestSequence(IWebDriver driver)
        {

            // Check how long it takes to load the main page
            Stopwatch timer = new Stopwatch();
            timer.Start();

            driver.Navigate().GoToUrl("https://lendify.se/");

            // Wait for page to load maximum 10000 seconds
            waitPageToLoad(driver, 10000);

            // Stop timer
            timer.Stop();

            // Log how long it took
            Console.WriteLine("Chrome loading time: " + timer.ElapsedMilliseconds);

            // Validate page name
            Assert.AreEqual("Låna pengar till låga räntor och spara med god avkastning hos Lendify", driver.Title);

            // maximize window
            driver.Manage().Window.Maximize();

            // Wait 2 seconds
            makeDelay(2000);

            // Close the cookie accept form
            IWebElement cookieButton = driver.FindElement(By.Id("cookieConsent"));
            Console.WriteLine("Cookie button text: " + cookieButton.Text); // text of the button
            cookieButton.Click();

            // Wait 2 seconds
            makeDelay(2000);

            // Play around with the menu
            IWebElement mainMenu = driver.FindElement(By.Id("navbarMenu"));
            // Locate the menuObjects

            // Will not work for all menu objects as all menus does not include hover a better locator would be needed
            ReadOnlyCollection<IWebElement> menuItems = mainMenu.FindElements(By.ClassName("hover-block"));

            foreach (IWebElement item in menuItems)
            {

                if (item.Text.Length > 0)
                {

                    IWebElement mainMenuItem = item.FindElement(By.ClassName("hover-header"));

                    // Hover over the menu
                    hoverMenuItem(driver, mainMenuItem, 2000);
                    Console.WriteLine("Hover: " + mainMenuItem.Text);

                    ReadOnlyCollection<IWebElement> subMenuItems = item.FindElements(By.ClassName("template-link"));
                    Console.WriteLine("Items: " + item.Text);

                    foreach (IWebElement subItem in subMenuItems)
                    {

                        hoverMenuItem(driver, subItem, 1000);
                        Console.WriteLine("Hover: " + item.Text);

                    }

                }

            }

            // Hover and Click login Button

            // The implementation of the login button needs to be fixed as there is no good way to locate it with
            // "mt-20" can not be used it is not unique enough
            // All <button> tags needs to be fetched and then loop them and compare the name

            ReadOnlyCollection<IWebElement> btns = driver.FindElements(By.TagName("button"));

            IWebElement loginButton = null;
            foreach (IWebElement item in btns)
            {
                Console.WriteLine(item.Text);

                if (item.Text == "Logga in") loginButton = item;

            }

            if (loginButton != null)
            {

                Actions action = new Actions(driver);
                action.MoveToElement(loginButton).Build().Perform();
                makeDelay(2000);
                loginButton.Click();

                waitPageToLoad(driver, 10000);

                // Play around with the ID number ID is missing needs to be added

                IWebElement idNumber = driver.FindElement(By.Id("LoginModel"));
                if (idNumber != null) idNumber = idNumber.FindElement(By.TagName("input"));

                idNumber.SendKeys("191212121221");

                makeDelay(2000);

                // Scroll down to the footer
                IWebElement footer = driver.FindElement(By.ClassName("footer"));
                if (footer != null)
                {
                    Actions afooter = new Actions(driver);
                    afooter.MoveToElement(footer).Perform();

                    makeDelay(2000);

                }

            }

            makeDelay(4000);



        }


        // Shall not be included in automated tests
        private void makeDelay(int milliSec)
        {
            System.Threading.Thread.Sleep(milliSec);
        }

        private void waitPageToLoad(IWebDriver driver, int maxTimeSeconds)
        {

            // check document.readyState = complete

            new WebDriverWait(driver, new System.TimeSpan(0, 0, maxTimeSeconds)).Until(
                x => ((IJavaScriptExecutor)x).ExecuteScript("return document.readyState").Equals("complete")
            );

        }


        private void hoverMenuItem(IWebDriver driver, IWebElement item, int delayMilliSec)
        {

            Actions action = new Actions(driver);
            action.MoveToElement(item).Build().Perform();

            makeDelay(delayMilliSec);

        }

    }

}
