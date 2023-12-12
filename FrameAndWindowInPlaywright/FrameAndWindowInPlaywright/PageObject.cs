using Microsoft.Playwright;

namespace FrameAndWindowInPlaywright
{
    public class PageObject
    {
        public async Task<IPage> ClickAndWaitForNewWindowAsync(IBrowserContext context, IPage page, string locatorValue)
        {
            var newPage = await context.RunAndWaitForPageAsync(async () =>
            {
                await page.Locator(locatorValue).ClickAsync();
            });

            return newPage;
        }

        public static void SwitchToWindowAndPerformOperationAsync(IPage page, Action<IPage> operation)
        {
            if (page != null)
            {
                operation(page);
                page.CloseAsync();
            }
            else
            {
                Console.WriteLine("New window has not be found...");
            }
        }

        public static void SwitchToFrameAndPerformOperation(IPage page, string frameSelector, Action<IFrameLocator> operation)
        {
            IFrameLocator frame = page.FrameLocator(frameSelector);
            operation(frame);
        }
    }
}