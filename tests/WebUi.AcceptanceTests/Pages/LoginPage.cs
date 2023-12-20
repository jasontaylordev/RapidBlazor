using Microsoft.Playwright;

namespace RapidBlazor.WebUi.AcceptanceTests.Pages;

public sealed class LoginPage : BasePage
{
    public LoginPage(IBrowser browser, IPage page)
    {
        Browser = browser;
        Page = page;
    }

    public override string PagePath => $"{BaseUrl}/Account/Login";
    public override IBrowser Browser { get; }
    public override IPage Page { get; protected set; }

    public Task SetEmail(string email)
        => Page.FillAsync("//html/body/div[1]/main/article/div/div[1]/section/form/div[1]/input", email);

    public Task SetPassword(string password)
        => Page.FillAsync("//html/body/div[1]/main/article/div/div[1]/section/form/div[2]/input", password);

    public Task ClickLogin()
        => Page.ClickAsync("//html/body/div[1]/main/article/div/div[1]/section/form/div[4]/button");

    public Task<string?> ProfileLinkText()
        => Page.Locator("a[href='Account/Manage']").TextContentAsync();

    public Task<bool> InvalidLoginAttemptMessageVisible()
        => Page.Locator("text=Error: Invalid login attempt").IsVisibleAsync();
}
