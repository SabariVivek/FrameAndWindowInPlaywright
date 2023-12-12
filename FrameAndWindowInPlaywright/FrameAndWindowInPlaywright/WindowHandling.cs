using Microsoft.Playwright;
using NUnit.Framework;

namespace FrameAndWindowInPlaywright
{
    public class WindowHandling : PageObject
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
            await page.GotoAsync("https://omayo.blogspot.com/");

            // Click and wait for a new window to open...
            string locator = "//*[text()='Open a popup window']";
            IPage childWindow = await ClickAndWaitForNewWindowAsync(context, page, locator);

            // Switch to a new window and perform operations in it...
            SwitchToWindowAndPerformOperationAsync(childWindow, targetWindow =>
            {
                Console.WriteLine(targetWindow.Url);
            });

            await Console.Out.WriteLineAsync(page.Url);
        }
    }
}