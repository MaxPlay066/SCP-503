using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API;
using Exiled.API.Features;
using PlayerEvents = Exiled.Events.Handlers.Player;
using ServerEvents = Exiled.Events.Handlers.Server;

namespace SCP_503
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Singleton { get; private set; }

        public override string Author { get; } = "MaxPlay066";
        public override string Name { get; } = "SCP503";
        public override string Prefix { get; } = "SCP503";
        public override Version Version { get; } = new Version(1, 0, 2);
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);
        public Handlers Handlers { get; private set; }
        public override void OnEnabled()
        {
            Singleton = this;
            Handlers.SCP503 = null;
            SubscribeEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnSubscribeEvents();
            base.OnDisabled();
        }

        public void SubscribeEvents()
        {
            Handlers = new Handlers();
            PlayerEvents.ChangingRole += Handlers.OnChangingRole;
            PlayerEvents.Hurting += Handlers.OnHurting;
            ServerEvents.RoundStarted += Handlers.RoundStarted;
            PlayerEvents.InteractingElevator += Handlers.InteractingElevator;
            PlayerEvents.FailingEscapePocketDimension += Handlers.FailingEscapePocketDimension;
            PlayerEvents.UsingRadioBattery += Handlers.UsingRadioBattery;
            PlayerEvents.Handcuffing += Handlers.Handcuffing;
            
        }

        public void UnSubscribeEvents()
        {
            PlayerEvents.ChangingRole -= Handlers.OnChangingRole;
            PlayerEvents.Hurting -= Handlers.OnHurting;
            ServerEvents.RoundStarted -= Handlers.RoundStarted;
            PlayerEvents.InteractingElevator -= Handlers.InteractingElevator;
            PlayerEvents.FailingEscapePocketDimension -= Handlers.FailingEscapePocketDimension;
            PlayerEvents.UsingRadioBattery -= Handlers.UsingRadioBattery;
            PlayerEvents.Handcuffing -= Handlers.Handcuffing;
            Handlers = null;
        }
    }
}
