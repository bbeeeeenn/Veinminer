using Microsoft.Xna.Framework;
using Terraria;
using TShockAPI;
using VeinMiner.Models;

namespace VeinMiner;

public static partial class Helpers
{
    public static void VeinMine(ushort type, int tileX, int tileY, TSPlayer player)
    {
        List<Point> vein = GetVein(type, new(tileX, tileY));
        int veinminedCount = vein.Count;

        WorldGen.KillTile_GetItemDrops(
            tileX,
            tileY,
            Main.tile[tileX, tileY],
            out int dropitem,
            out int _,
            out int _,
            out int _
        );
        Item item = TShock.Utils.GetItemById(dropitem);

        static void KillTile(Point p, bool noItem)
        {
            WorldGen.KillTile(p.X, p.Y, noItem: noItem);
            NetMessage.SendData((int)PacketTypes.Tile, -1, -1, null, 0, p.X, p.Y);
        }

        if (!PluginSettings.Config.GiveItemsDirectly.Enabled)
        {
            foreach (Point p in vein)
            {
                KillTile(p, false);
            }
        }
        else
        {
            int freeSlot = player.GetFreeSlotCount(item);

            if (vein.Count <= freeSlot)
            {
                player.GiveItem(item.type, stack: vein.Count);
                foreach (Point p in vein)
                {
                    KillTile(p, true);
                }
            }
            else if (freeSlot == 0)
            {
                if (PluginSettings.Config.GiveItemsDirectly.DisableVeinmineWhenNoFreeSlot)
                {
                    veinminedCount = 0;
                    KillTile(vein[0], false);
                }
                else
                {
                    foreach (Point p in vein)
                    {
                        KillTile(p, false);
                    }
                }
            }
            else
            {
                veinminedCount = PluginSettings
                    .Config
                    .GiveItemsDirectly
                    .DisableVeinmineWhenNoFreeSlot
                    ? freeSlot
                    : vein.Count;

                player.GiveItem(item.type, stack: freeSlot);
                for (int i = 0; i < vein.Count; i++)
                {
                    if (i < freeSlot)
                    {
                        KillTile(vein[i], true);
                        continue;
                    }
                    else if (!PluginSettings.Config.GiveItemsDirectly.DisableVeinmineWhenNoFreeSlot)
                    {
                        KillTile(vein[i], false);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        if (veinminedCount > 1 && player.GetData<Veinmining>("veinmining").Broadcast)
        {
            player.SendMessage(
                string.Format(
                    PluginSettings.Config.Broadcast.Format,
                    $"[i/s{veinminedCount}:{item.netID}]"
                ),
                Color.AliceBlue
            );
        }
    }
}
