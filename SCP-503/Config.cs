using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;
using System.ComponentModel;

namespace SCP_503
{
    public class Config : IConfig
    {
        [Description("Is this plugin enabled")]
        public bool IsEnabled { get; set; } = true;

        [Description("The SCP will spawn at start of the round?")]
        public bool SpawnAtStartRound { get; set; } = true;

        [Description("How many players needs to be in the server for automatically spawn?")]
        public int PlayersForScpSpawning { get; set; } = 5;

        [Description("How much health SCP 503 has?")]
        public int ScpHealth { get; set; } = 200;

        [Description("What role will have SCP 503?")]
        public RoleType ScpRole { get; set; } = RoleType.ClassD;

        [Description("How much damage the SCP will take if hurted by tesla?")]
        public int TeslaDamage { get; set; } = 50;

        [Description("How much damage the SCP will take if hurted by SCP-173?")]
        public int Scp173Damage { get; set; } = 50;

        [Description("How much damage the SCP will take if hurted by SCP-096?")]
        public int Scp096Damage { get; set; } = 50;

        [Description("How much damage the SCP will take if hurted by SCP-049?")]
        public int Scp049Damage { get; set; } = 50;

        [Description("How much damage the SCP will take if hurted by SCP-049-2")]
        public int Scp0492Damage { get; set; } = 10;

        [Description("How much damage the SCP will take if hurted by SCP-106?")]
        public int Scp106Damage { get; set; } = 20;

        [Description("How much damage the SCP will take if hurted by SCP-939?")]
        public int Scp939Damage { get; set; } = 40;

        [Description("How high is the percentuage of failing handcuffing SCP-503, and with that destroying the disarmer? (101 = No Fail Percentuage)")]
        public int FailHandcuffPercentuage { get; set; } = 50;
    }
}
