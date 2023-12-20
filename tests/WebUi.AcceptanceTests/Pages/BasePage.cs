using Microsoft.Playwright;

namespace RapidBlazor.WebUi.AcceptanceTests.Pages;

public abstract class BasePage
{
    public static string BaseUrl => ConfigurationHelper.GetBaseUrl();
    
    public abstract string PagePath { get; }
    
    public abstract IBrowser Browser { get; }
    
    public abstract IPage Page { get; protected set; }

    public async Task GotoAsync()
    {
        Page = await Browser.NewPageAsync();
        await Page.GotoAsync(PagePath);
    }
}
