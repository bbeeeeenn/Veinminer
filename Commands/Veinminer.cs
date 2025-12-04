using Microsoft.Xna.Framework;
using Terraria;
using TShockAPI;
using VeinMiner.Models;

namespace VeinMiner.Commands;

public class Veinminer : Models.Command
{
    public override bool AllowServer => false;
    public override string[] Aliases { get; set; } = PluginSettings.Config.CommandAliases;
    public override string PermissionNode { get; set; } = PluginSettings.Config.PermissionNode;

    public override void Execute(CommandArgs args)
    {
        if (!args.Player.IsLoggedIn)
        {
            args.Player.SendErrorMessage("You must log-in first.");
            return;
        }
        if (!PluginSettings.Config.Enabled)
        {
            args.Player.SendErrorMessage("Veinmining is disabled.");
            return;
        }
        TSPlayer player = args.Player;
        Veinmining veinmining = player.GetData<Veinmining>("veinmining");

        if (args.Parameters.Count >= 1)
        {
            if (args.Parameters[0].ToLower() == "bc" || args.Parameters[0].ToLower() == "broadcast")
            {
                veinmining.Broadcast = !veinmining.Broadcast;
                player.SendSuccessMessage(
                    $"Veinmine broadcasting is turned {(veinmining.Broadcast ? $"[c/{Color.LightBlue.Hex3()}:ON]" : $"[c/{Color.OrangeRed.Hex3()}:OFF]")}."
                );
            }
            else
            {
                player.SendErrorMessage(
                    $"Command usage: Toggle veinminer: [c/ffffff:{TShock.Config.Settings.CommandSpecifier}{PluginSettings.Config.CommandAliases[0]}], Toggle broadcast: [c/ffffff:{TShock.Config.Settings.CommandSpecifier}{PluginSettings.Config.CommandAliases[0]} bc]."
                );
            }
            return;
        }

        veinmining.Enabled = !veinmining.Enabled;
        player.SendSuccessMessage(
            $"Veinmining is turned {(veinmining.Enabled ? $"[c/{Color.LightBlue.Hex3()}:ON]" : $"[c/{Color.OrangeRed.Hex3()}:OFF]")}."
        );
    }
}
