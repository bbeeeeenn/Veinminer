using VeinMiner.Commands;
using VeinMiner.Models;

namespace VeinMiner;

public class CommandManager
{
    public static readonly List<Command> Commands = new()
    {
        // Commands
        new Veinminer(),
    };

    public static void RegisterAll()
    {
        foreach (Command command in Commands)
        {
            TShockAPI.Commands.ChatCommands.Add(command);
        }
    }
}
