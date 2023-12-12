using Microsoft.Playwright;
using NUnit.Framework;

namespace FrameAndWindowInPlaywright
{
    public class FrameHandling : PageObject
    {
        private static IBrowser? browser;
        private static IBrowserContext? context;
        private static IPage? page;

        [Test]
        public async Task WindowHandlingMethod()
        {
            // Playwright Instance Creation...
            using var playwright = await Playwright.CreateAsync();

            // Browser Setup...
            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = false
            };

            browser = await playwright.Chromium.LaunchAsync(launchOptions);

            context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport
            });

            // Page Setup...
            page = await context.NewPageAsync();

            // URL Launching...
            await page.GotoAsync("https://demoqa.com/frames");

            // Switch to a frame and perform operations in it...
            string locator = "#frame1";
            SwitchToFrameAndPerformOperation(page, locator, async frame =>
            {
                Console.WriteLine(await frame.Locator("//*[@id='sampleHeading']").TextContentAsync());
            });

            await page.Locator("//img[@src='/images/Toolsqa.jpg']").ClickAsync();
        }
    }
}