using finallexamp.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var menuService = new MenuServices();
        menuService.ShowMenu();
        while (true)
        {
            await menuService.HandleMenuSelectionAsync();
        }
    }
}