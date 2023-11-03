using System.Threading.Channels;
using Discord.Commands;
using githubwebscraper;
using githubwebscraper.modules;

namespace githubwebscraperdiscordbot.modules;

public class Commands : ModuleBase<SocketCommandContext>
{
    [Command("online")]
    public async Task Online()
    {
        var context = Context;
        
        await context.Channel.TriggerTypingAsync();
        await ReplyAsync("Online and Ready to go!");
        await context.Channel.TriggerTypingAsync();
        await context.Channel.SendMessageAsync($"You can use the following commands: !getscore <github url>");
    }

    [Command("getgithubscore")]
    public async Task GetScore([Remainder] string rawMessage)
    {
        var context = Context;

        if (Uri.TryCreate(rawMessage, UriKind.Absolute, out Uri url))
        {
            await context.Channel.TriggerTypingAsync();
            PuppeteerScraper scraper = new PuppeteerScraper();
            try
            {
                User user = await scraper.ScrapeWebsiteAsync(rawMessage);
                Score scoreuser = new Score();
                await context.Channel.SendMessageAsync(
                    $"Github Score - {user.title.Split(" ")[0]}: {scoreuser.CalculateScore(user)}%");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await context.Channel.TriggerTypingAsync();
            }

            await context.Channel.SendMessageAsync($"You requested the URL: {url}");
        }
        else
        {
            await context.Channel.SendMessageAsync("Invalid URL provided. Please provide a valid URL after !getscore.");
        }
    }
}