using Terraria;
using TShockAPI;

namespace VeinMiner;

public partial class Helpers
{
    public static int GetFreeSlotCount(this TSPlayer player, Item item)
    {
        Item[] inv = player.TPlayer.inventory;
        int count = 0;
        for (int i = 0; i < 50; i++)
        {
            if (inv[i].netID == item.netID)
            {
                count += inv[i].maxStack - inv[i].stack;
            }
            else if (inv[i].stack <= 0)
            {
                count += item.maxStack;
            }
        }

        return count;
    }
}
