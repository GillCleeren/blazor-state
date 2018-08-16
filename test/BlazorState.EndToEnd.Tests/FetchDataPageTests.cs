﻿namespace BlazorState.EndToEnd.Tests
{
  using System;
  using System.Collections.ObjectModel;
  using System.Threading;
  using BlazorState.EndToEnd.Tests.Infrastructure;
  using OpenQA.Selenium;
  using OpenQA.Selenium.Support.UI;
  using Shouldly;
  using static Infrastructure.WaitAndAssert;


  public class FetchDataPageTests : BaseTest
  {
    /// <summary>
    /// Test that the FetchData link is available from the root page 
    /// The page loads
    /// And the page Loads data.
    /// </summary>
    /// <param name="aWebDriver"></param>
    /// <param name="aServerFixture">
    /// Is a dependency as the server needs to be running
    /// but is not referenced otherwise thus the injected item is NOT stored
    /// </param>
    public FetchDataPageTests(IWebDriver aWebDriver, ServerFixture aServerFixture)
      : base(aWebDriver, aServerFixture)
    {
      WebDriver = aWebDriver;
      aServerFixture.Environment = AspNetEnvironment.Development;
      aServerFixture.BuildWebHostMethod = BlazorState.Server.Program.BuildWebHost;

      Navigate("/", aReload: true);
      WaitUntilLoaded();
    }

    private IWebDriver WebDriver { get; }

    public void HasFetchDataPage()
    {
      // Navigate to "Fetch data"
      WebDriver.FindElement(By.LinkText("Fetch data")).Click();
      WaitAndAssertEqual("Weather forecast", () => WebDriver.FindElement(By.TagName("h1")).Text);

      // There should be rows in the table
      WaitAndAssertNotEmpty(() => WebDriver.FindElements(By.CssSelector("table td")));
    }
  }
}