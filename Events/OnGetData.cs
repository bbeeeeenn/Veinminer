using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using VeinMiner.Models;

namespace VeinMiner.Events;

public class OnGetData : Event
{
    public override void Disable(TerrariaPlugin plugin)
    {
        ServerApi.Hooks.NetGetData.Deregister(plugin, EventMethod);
    }

    public override void Enable(TerrariaPlugin plugin)
    {
        ServerApi.Hooks.NetGetData.Register(plugin, EventMethod);
    }

    private void EventMethod(GetDataEventArgs args)
    {
        TSPlayer player = TShock.Players[args.Msg.whoAmI];
        if (
            args.MsgID != PacketTypes.Tile
            || !PluginSettings.Config.Enabled
            || !player.IsLoggedIn
            || !player.GetData<Veinmining>("veinmining").Enabled
        )
        {
            return;
        }

        using BinaryReader reader = new(
            new MemoryStream(args.Msg.readBuffer, args.Index, args.Length)
        );
        byte action = reader.ReadByte();
        short tileX = reader.ReadInt16();
        short tileY = reader.ReadInt16();
        short flag = reader.ReadInt16();

        ITile tile = Main.tile[tileX, tileY];
        if (action == 0 && flag == 0)
        {
            if (PluginSettings.Config.TileIds.Contains(tile.type))
            {
                args.Handled = true;
                Helpers.VeinMine(tile.type, tileX, tileY, player);
            }
        }
    }
}
