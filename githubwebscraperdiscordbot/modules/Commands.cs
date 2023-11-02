using Discord.Commands;
using linkedinwebscraper;
using linkedinwebscraper.modules;

namespace githubwebscraperdiscordbot.modules;

public class Commands : ModuleBase<SocketCommandContext>
{
    [Command("online")]
    public async Task Online()
    {
        await ReplyAsync("Online and Ready to go!");
    }

    [Command("getgithubscore")]
    public async Task GetScore([Remainder] string rawMessage)
    {
        var context = Context;

        if (Uri.TryCreate(rawMessage, UriKind.Absolute, out Uri url))
        {
            PuppeteerScraper scraper = new PuppeteerScraper();
            try
            {
                User user = await scraper.ScrapeWebsiteAsync(rawMessage);
                Score scoreuser = new Score();
                await context.Channel.SendMessageAsync($"Github Score - {user.title.Split(" ")[0]}: {scoreuser.CalculateScore(user)}%");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await context.Channel.SendMessageAsync($"You requested the URL: {url}");
        }
        else
        {
            await context.Channel.SendMessageAsync("Invalid URL provided. Please provide a valid URL after !getscore.");
        }
    }
}