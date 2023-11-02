using githubwebscraper.modules;

namespace githubwebscraper;

class Program
{
    static async Task Main(string[] args)
    {
        PuppeteerScraper scraper = new PuppeteerScraper();
        Console.Write("Github URL: ");
        string? url = Console.ReadLine();

        using (var cts = new CancellationTokenSource())
        {
            Task loadingTask = DisplayLoadingAnimation(cts.Token);
            try
            {
                User user = await scraper.ScrapeWebsiteAsync(url);
                cts.Cancel();
                await loadingTask;
                Console.WriteLine();
                Score scoreuser = new Score();
                Console.WriteLine($"Github Score - {user.title.Split(" ")[0]}: {scoreuser.CalculateScore(user)}%");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    static async Task DisplayLoadingAnimation(CancellationToken cancellationToken)
    {
        Console.Write("Calculating");
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(500); // Add a dot every half second
            Console.Write(".");
        }
    }
}