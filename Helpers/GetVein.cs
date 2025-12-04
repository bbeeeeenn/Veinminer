using Microsoft.Xna.Framework;
using Terraria;

namespace VeinMiner;

public static partial class Helpers
{
    public static List<Point> GetVein(ushort tileType, Point start)
    {
        List<Point> tileList = new();
        Stack<Point> stack = new();
        stack.Push(start);

        while (stack.Count > 0)
        {
            Point p = stack.Pop();

            if (p.X < 0 || p.Y < 0 || p.X >= Main.maxTilesX || p.Y >= Main.maxTilesY)
                continue;

            if (tileList.Any(tp => tp.Equals(p)))
                continue;

            ITile tile = Main.tile[p.X, p.Y];
            if (!tile.active() || tile.type != tileType)
                continue;

            if (tileList.Count >= PluginSettings.Config.MaxTileDestroy)
                return tileList;

            tileList.Add(p);

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    if (xOffset == 0 && yOffset == 0)
                        continue;

                    stack.Push(new Point(p.X + xOffset, p.Y + yOffset));
                }
            }
        }

        return tileList;
    }
}
