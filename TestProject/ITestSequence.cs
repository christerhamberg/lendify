using OpenQA.Selenium;
using System;

namespace TestProject
{
    interface ITestSequence
    {
        public void runTestSequence(IWebDriver driver);

        public String getTestDescription();

    }
}
