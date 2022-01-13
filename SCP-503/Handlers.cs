using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs;
using Exiled.API.Enums;
using UnityEngine;
using Exiled.API.Features;
using Exiled.API;
using MEC;

namespace SCP_503
{

    public class Handlers
    {
        public static Player SCP503;
        public List<Player> PlayerList = new List<Player>();
        public CoroutineHandle ChooseSCPHandle;
        public static bool SCPSet;


        public void RoundStarted()
        {
            SCP503 = null;
            SCPSet = false;
            ChooseSCPHandle = Timing.RunCoroutine(Choose503());
        }
        public void OnHurting(HurtingEventArgs ev)
        {
            var cfg = Plugin.Singleton.Config;
            if(ev.Target == SCP503)
            {
                switch (ev.Handler.Type)
                {
                    case DamageType.Tesla: ev.Amount = cfg.TeslaDamage;
                        break;
                    case DamageType.Scp173: ev.Amount = cfg.Scp173Damage;
                        break;
                    case DamageType.Scp096: ev.Amount = cfg.Scp096Damage;
                        break;
                    case DamageType.Scp049: ev.Amount = cfg.Scp049Damage;
                        break;
                    case DamageType.Scp106: ev.Amount = cfg.Scp106Damage;
                        break;
                    case DamageType.Scp939: ev.Amount = cfg.Scp939Damage;
                        break;
                    case DamageType.Firearm: ev.Amount /= 2;
                        break;
                    case DamageType.Falldown: ev.Amount /= 3;
                        break;
                }
            }

            if(ev.Attacker == SCP503)
            {
               if(ev.Handler.Type == DamageType.Firearm)
                {
                    ev.Amount *= 2;
                }
            }
        }

        public void FailingEscapePocketDimension(FailingEscapePocketDimensionEventArgs ev)
        {
            if(ev.Player == SCP503)
            {
                ev.IsAllowed = false;
                Timing.CallDelayed(0.15f, () => ev.Player.Position = Map.Rooms.First(room => room.Type == RoomType.Hcz106).Position + new Vector3(0, 2.5F, 0));
            }
        }
        public void InteractingElevator(InteractingElevatorEventArgs ev)
        {
            if(ev.Player == SCP503)
            {
                ev.Lift.movingSpeed /= 2;
            }
            else
            {
                ev.Lift.movingSpeed = 5;
            }
        }
        

        public IEnumerator<float> Choose503()
        {
            yield return Timing.WaitForSeconds(1f);
            if (Player.List.Count() >= Plugin.Singleton.Config.PlayersForScpSpawning)
            {
                foreach (Player player in Player.List)
                {
                    PlayerList.Add(player);
                }
                PlayerList.ShuffleList();
                SCP503 = PlayerList.First();
                Spawn503(SCP503);
            }
            Timing.KillCoroutines(ChooseSCPHandle);
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if(ev.Player == SCP503)
            {
                UnSpawn503(SCP503);
            }
        }

        public void UsingRadioBattery(UsingRadioBatteryEventArgs ev)
        {
            if (ev.Player == SCP503)
            {
                ev.Drain /= 2;
            }
        }

        public void Handcuffing(HandcuffingEventArgs ev)
        {
            var cfg = Plugin.Singleton.Config;
            if(ev.Target == SCP503)
            {
                int rrange = UnityEngine.Random.Range(1, 101);
                if (rrange <= cfg.FailHandcuffPercentuage)
                {
                    ev.IsAllowed = false;
                    ev.Cuffer.RemoveHeldItem();
                }
            }
        }

        public static void Spawn503(Player player)
        { 
            SCP503 = player;
            SCPSet = true;
            var cfg = Plugin.Singleton.Config;
            player.Role = cfg.ScpRole;
            player.MaxHealth = cfg.ScpHealth;
            player.Health = cfg.ScpHealth;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
            player.CustomInfo = "<color=red>SCP-503</color>";
            Timing.CallDelayed(0.15f, () => player.Position = Map.Rooms.First(room => room.Type == RoomType.LczGlassBox).Position + new Vector3(0, 2.5f, 0));
        } 

        public static void UnSpawn503(Player player)
        {
            player.CustomInfo = null;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;
            SCPSet = false;
            SCP503 = null;
        }

        
    }
}
