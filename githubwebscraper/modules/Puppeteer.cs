using PuppeteerSharp;

namespace linkedinwebscraper.modules;

public class PuppeteerScraper
{
    public async Task<User> ScrapeWebsiteAsync(string? url)
    {
        LaunchOptions? launchOptions = new LaunchOptions
        {
            Headless = true,
            ExecutablePath = "/home/lucaslinuxlaptop/.appimages/gearlever_thoriumbrowser_02e245.appimage"
        };

        using IBrowser? browser = await Puppeteer.LaunchAsync(launchOptions);
        using IPage? page = await browser.NewPageAsync();

        await page.GoToAsync(url);

        User user = new User();
        user.title = await page.EvaluateExpressionAsync<string>("document.title");

        user.fullName =
            await page.EvaluateExpressionAsync<string>(
                "document.querySelector('.js-profile-editable-replace .vcard-fullname')?.innerText");

        user.bio =
            await page.EvaluateExpressionAsync<string>(
                "document.querySelector('.js-profile-editable-replace .user-profile-bio')?.innerText");
        
        user.location =
            await page.EvaluateExpressionAsync<string>(
                "document.querySelector('.vcard-detail[itemprop=homeLocation] .p-label')?.innerText");
        
        user.social = await page.EvaluateFunctionAsync<string[]>(
            "() => {return Array.from(document.querySelectorAll('.vcard-detail[itemprop=social] title')).map(element => element.getAttribute('title'));}");
        
       user.repositories = Convert.ToInt32(
            await page.EvaluateExpressionAsync<string>(
                "document.querySelector('[data-tab-item*=\"repositories\"] .Counter')?.title"));
        
        user.stars = Convert.ToInt32((
            await page.EvaluateExpressionAsync<string>(
                "document.querySelector('[data-tab-item*=\"stars\"] .Counter')?.title")).Replace(",", ""));

       user.readme =
           !string.IsNullOrWhiteSpace(await page.EvaluateExpressionAsync<string>(
                "document.querySelector('.profile-readme')?.innerText"));
        
        
        user.contributions = Convert.ToInt32((await page.EvaluateExpressionAsync<string>("document.querySelector('.js-yearly-contributions .f4')?.innerText")).Split(" ")[0].Replace(",", ""));
        
        user.pinned =
            !string.IsNullOrWhiteSpace(await page.EvaluateExpressionAsync<string>(
                "document.querySelector('.js-pinned-items-reorder-list')?.innerText"));

        user.achievements = await page.EvaluateFunctionAsync<bool>("() => {return document.querySelectorAll('.js-profile-editable-replace [data-hovercard-type=\"achievement\"]').length > 0}");        

        user.highlights = await page.EvaluateFunctionAsync<bool>("() => { const elements = document.querySelectorAll('.js-profile-editable-replace .color-border-muted'); for (const element of elements) { const h2Element = element.querySelector('h2'); if (h2Element && h2Element.innerHTML === 'Highlights') { return true; } } return false; }");

        user.sponsors = await page.EvaluateFunctionAsync<bool>("() => {      const elements = document.querySelectorAll('.js-profile-editable-replace .color-border-muted');      for (const element of elements) {          const h2Element = element.querySelector('h2');         if (h2Element && h2Element.innerHTML === 'Sponsors') {              return true;          }      }      return false;  }");        
        user.organizations = await page.EvaluateFunctionAsync<bool>("() => {      const elements = document.querySelectorAll('.js-profile-editable-replace .color-border-muted');      for (const element of elements) {          const h2Element = element.querySelector('h2');         if (h2Element && h2Element.innerHTML === 'Organizations') {              return true;          }      }      return false;  }");        
        
        await browser.CloseAsync();
        
        return user;
        
        
    }
}