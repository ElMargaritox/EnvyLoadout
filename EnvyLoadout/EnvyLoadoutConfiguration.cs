using EnvyLoadout.Models;
using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvyLoadout
{
    public class EnvyLoadoutConfiguration : IRocketPluginConfiguration
    {
        public int Interval { get; set; }
        public List<Loadout> Loadouts { get; set; }
        public void LoadDefaults()
        {
            Loadouts = new List<Loadout>
            {
            new Loadout{Id = 121, Amount = 1}
            };
        }
    }
}
