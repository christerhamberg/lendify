using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Edge;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWithChrome()
        {

            ITestSequence sequence = new TestSeq1();

            // Demo of accessing lendify and navigating around
            // Text is visible only in the Test Explorer under additional output not in the Console
            Console.WriteLine("Chrome");
            Console.WriteLine("Test Description: " +sequence.getTestDescription());

            // Just to do some stuff with chrome
            IWebDriver driver = new ChromeDriver("C:\\Work\\selenium\\chromedriver_win32");

            sequence.runTestSequence(driver);

            // close down
            driver.Close();

        }



        [TestMethod]
        public void TestWithFirefox()
        {

            ITestSequence sequence = new TestSeq1();

            // Demo of accessing lendify and navigating around
            // Text is visible only in the Test Explorer under additional output not in the Console
            Console.WriteLine("Firefox");
            Console.WriteLine("Test Description: " + sequence.getTestDescription());

            // Just to do some stuff with firefox
            IWebDriver driver = new FirefoxDriver("C:\\Work\\selenium\\geckodriver-v0.27.0-win64");

            sequence.runTestSequence(driver);

            // close down
            driver.Close();

        }


        [TestMethod]
        public void TestWithEdge()
        {

            ITestSequence sequence = new TestSeq1();

            // Demo of accessing lendify and navigating around
            // Text is visible only in the Test Explorer under additional output not in the Console
            Console.WriteLine("Edge");
            Console.WriteLine("Test Description: " + sequence.getTestDescription());

            // Just to do some stuff with edge
            var options = new EdgeOptions();
            options.UseChromium = true;
            options.BinaryLocation = @"C:\\Work\\selenium\\edgedriver_win64\\MicrosoftWebDriver.exe";

            var driver = new EdgeDriver(options);


            IWebDriver driver = new EdgeDriver("C:\\Work\\selenium\\edgedriver_win64\\MicrosoftWebDriver.exe");

            sequence.runTestSequence(driver);

            // close down
            driver.Close();

        }
        
    }
}
