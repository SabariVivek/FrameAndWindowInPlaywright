using Microsoft.Playwright;
using NUnit.Framework;

namespace FrameAndWindowInPlaywright
{
    public class Maximize
    {
        private static IBrowser? browser;
        private static IBrowserContext? context;
        private static IPage? page;

        [Test]
        public async Task MaximizeMethodAsync()
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
            await page.GotoAsync("https://omayo.blogspot.com");

            var buttonSelector = "//a[text()='Open a popup window']";
            await page.ClickAsync(buttonSelector, new PageClickOptions { Button = MouseButton.Left, ClickCount = 1 });
            await page.WaitForTimeoutAsync(2000);
        }
    }
}