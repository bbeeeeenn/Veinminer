using TerrariaApi.Server;
using TShockAPI.Hooks;
using VeinMiner.Models;

namespace VeinMiner.Events;

public class OnPostLogin : Event
{
    public override void Disable(TerrariaPlugin plugin)
    {
        PlayerHooks.PlayerPostLogin -= EventMethod;
    }

    public override void Enable(TerrariaPlugin plugin)
    {
        PlayerHooks.PlayerPostLogin += EventMethod;
    }

    private void EventMethod(PlayerPostLoginEventArgs e)
    {
        e.Player.SetData(
            "veinmining",
            new Veinmining(true, PluginSettings.Config.Broadcast.Enabled)
        );
    }
}
