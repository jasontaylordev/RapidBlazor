using BoDi;
using FluentAssertions;
using Microsoft.Playwright;
using RapidBlazor.WebUi.AcceptanceTests.Pages;
using TechTalk.SpecFlow;

namespace RapidBlazor.WebUi.AcceptanceTests.StepDefinitions;

[Binding]
public class LoginStepDefinitions
{
    private readonly LoginPage _loginPage;

    public LoginStepDefinitions(LoginPage loginPage)
    {
        _loginPage = loginPage;
    }

    [BeforeFeature("Login")]
    public static async Task BeforeLoginScenario(IObjectContainer container)
    {
        var playwright = await Playwright.CreateAsync();

        var options = new BrowserTypeLaunchOptions();
        
#if DEBUG
        options.Headless = false;
        options.SlowMo = 500;
#endif

        var browser = await playwright.Chromium.LaunchAsync(options);

        var page = await browser.NewPageAsync();

        var loginPage = new LoginPage(browser, page);

        container.RegisterInstanceAs(playwright);
        container.RegisterInstanceAs(browser);
        container.RegisterInstanceAs(loginPage);
    }
    
    [Given(@"a logged out user")]
    public async Task GivenALoggedOutUser()
    {
        await _loginPage.GotoAsync();
    }

    [When(@"the user logs in with valid credentials")]
    public async Task WhenTheUserLogsInWithValidCredentials()
    {
        await _loginPage.SetEmail("admin@localhost");
        await _loginPage.SetPassword("Password123!");
        await _loginPage.ClickLogin();
    }

    [Then(@"they log in successfully")]
    public async Task ThenTheyLogInSuccessfully()
    {
        var profileLinkText = (await _loginPage.ProfileLinkText())?.Trim();

        profileLinkText.Should().NotBeNull();
        profileLinkText.Should().Be("admin@localhost");
    }

    [When(@"the user logs in with invalid credentials")]
    public async Task WhenTheUserLogsInWithInvalidCredentials()
    {
        await _loginPage.SetEmail("hacker@localhost");
        await _loginPage.SetPassword("l337hax");
        await _loginPage.ClickLogin();
    }

    [Then(@"an error is displayed")]
    public async Task ThenAnErrorIsDisplayed()
    {
        var errorVisible = await _loginPage.InvalidLoginAttemptMessageVisible();

        errorVisible.Should().BeTrue();
    }
}
