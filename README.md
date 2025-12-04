# VeinMiner

**Installation**

1. [Download the plugin `.dll` file from the Releases page](https://github.com/bbeeeeenn/Veinminer/releases).
2. Place the file in your TShock server’s `ServerPlugins` folder.
3. Restart your TShock server.
4. The plugin is now active and ready to use.

**Usage / Commands**
By default the plugin registers the command aliases defined in the config (`veinminer` and `vm`).

Examples:

```
/veinminer       -> Toggle vein-mining on/off for the player
/vm              -> Alias for the same toggle
/veinminer bc    -> Toggle per-player broadcast of vein-mined results
```

**Permissions**

-  Default permission node: `veinminer`
   -  Grant with: `/group <group> addperm veinminer` or `/user <user> addperm veinminer` depending on your server setup.

**Configuration**
The plugin stores its configuration in `tshock/VeinMiner/config.json`. Below is the structure and default values:

```json
{
	"Enabled": true,
	"GiveItemsDirectly": {
		"Enabled": true,
		"DisableVeinmineWhenNoFreeSlot": false
	},
	"Broadcast": {
		"Enabled": true,
		"Format": "Mined {0}"
	},
	"MaxTileDestroy": 100,
	"TileIds": [
		7, 166, 6, 167, 9, 168, 8, 169, 37, 22, 204, 56, 58, 107, 221, 108, 222,
		111, 223, 211, 408, 68, 64, 65, 63, 66, 67, 178, 123, 224, 404
	],
	"CommandAliases": ["veinminer", "vm"],
	"PermissionNode": "veinminer"
}
```

Notes on options:

-  `Enabled` (bool): Master toggle for the plugin.

-  `GiveItemsDirectly` (object): Controls whether mined items are directly added to the player's inventory instead of dropping on the ground.

   -  `Enabled` (bool): If `true`, the plugin attempts to give items directly to the player.
   -  `DisableVeinmineWhenNoFreeSlot` (bool): If `true` and the player has no free inventory slots, the vein-mine action will be suppressed (no tiles will be removed for that vein). If `false`, the plugin will break the tiles normally and drop items on the ground when inventory space is insufficient.

-  `Broadcast` (object): Controls broadcast behavior and format.

   -  `Enabled` (bool): Global toggle for broadcasts created by the plugin (affects whether the plugin attempts to send messages).
   -  `Format` (string): Format string used with `string.Format`. It receives a single parameter (`{0}`) which the plugin replaces with an inline item tag showing how many and which item was mined. Example default: `"Mined {0}"` — when a player mines multiple tiles, players see messages such as `Mined [i/s5:... ]` (an inline item tag representing 5 of that item).

-  `MaxTileDestroy` (int): Maximum number of tiles the vein-miner algorithm will consider/destroy in one operation. Default is `100`.

-  `TileIds` (array): List of tile types the vein-miner will consider as part of veins. The defaults include common ores, gems and some special tiles (see example above). These entries correspond to tile identifiers (original code uses `Terraria.ID.TileID` constants).

-  `CommandAliases` (array): Command names/aliases to register the plugin command under. Defaults to `["veinminer", "vm"]`.

-  `PermissionNode` (string): Permission required for player to use Veinminer. Defaults to `veinminer`.

---

This is an AI generated `README.md` file
