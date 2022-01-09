using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.Permissions.Extensions;
using Exiled.API.Features;

namespace SCP_503
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpawnCommand : ICommand
    {
        public string Command { get; } = "spawn503";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Command for spawning 503";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player;
            player = Player.Get(arguments.At(0));
            if (!sender.CheckPermission("SCP503.spawn503"))
            {
                response = "You can't do that command";
                return false;
            }
            else if (arguments.Count != 1 || !(Player.Get(arguments.At(0)) is Player ply))
            {
                response = "You need to provide a valid player ID";
                return false;
            }
            else if (Handlers.SCPSet) 
            {
                response = "There is already a SCP-503 alive";
                return false;
            }
            else
            {
                Handlers.Spawn503(player);
                response = "SCP-503 Spawned Succesfully";
                return true;
            }
        }
    }
}
