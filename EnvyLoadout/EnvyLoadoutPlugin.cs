using EnvyLoadout.Models;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace EnvyLoadout
{
    public class EnvyLoadoutPlugin : RocketPlugin<EnvyLoadoutConfiguration>
    {
        public static EnvyLoadoutPlugin Instance;
        public List<CSteamID> cSteamIDs = new List<CSteamID>();
        protected override void Load()
        {
            Instance = this;
            Logger.Log("Se cargo correctamente EnvyLoadout");
            Logger.Log("Plugin By: Margarita#8172");
            UnturnedPlayerEvents.OnPlayerRevive += OnRevive;
            U.Events.OnPlayerConnected += OnPlayerConnected;
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            if (!cSteamIDs.Contains(player.CSteamID))
            {
                foreach (Loadout loadout in Configuration.Instance.Loadouts) player.GiveItem(loadout.Id, loadout.Amount);
                cSteamIDs.Add(player.CSteamID);
                StartCoroutine(Wait(player.CSteamID));
            }
        }

        private void OnRevive(UnturnedPlayer player, Vector3 position, byte angle)
        {
            if (!cSteamIDs.Contains(player.CSteamID))
            {
                foreach (Loadout loadout in Configuration.Instance.Loadouts) player.GiveItem(loadout.Id, loadout.Amount);
                cSteamIDs.Add(player.CSteamID);
                StartCoroutine(Wait(player.CSteamID));
            }
        }

        public IEnumerator<WaitForSeconds> Wait(CSteamID csteamid)
        {
            yield return new WaitForSeconds(Configuration.Instance.Interval);
            cSteamIDs.Remove(csteamid);
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerRevive -= OnRevive;
            U.Events.OnPlayerConnected -= OnPlayerConnected;
        }
    }
}
