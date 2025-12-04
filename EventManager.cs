using TerrariaApi.Server;
using VeinMiner.Events;
using VeinMiner.Models;

namespace VeinMiner;

public class EventManager
{
    public static readonly List<Event> events = new()
    {
        // Events
        new OnReload(),
        new OnGetData(),
        new OnPostLogin(),
    };

    public static void RegisterAll(TerrariaPlugin plugin)
    {
        foreach (Event _event in events)
        {
            _event.Enable(plugin);
        }
    }

    public static void DeregisterAll(TerrariaPlugin plugin)
    {
        foreach (Event _event in events)
        {
            _event.Disable(plugin);
        }
    }
}
