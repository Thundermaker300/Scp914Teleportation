using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MEC;
using UnityEngine;
using Random = System.Random;
using CustomPlayerEffects;
using Exiled.API.Extensions;

namespace Scp914Teleportation
{
    public class EventHandlers
    {

        public Random rnd = new Random();

        public void OnUpgradingItems(UpgradingItemsEventArgs ev)
        {
            if (ev.KnobSetting == Scp914Teleportation.Instance.Config.TeleportMode)
            {
                Log.Info("SCP-914 Teleportation activated");
                int roomIndex = rnd.Next(0, Scp914Teleportation.Instance.Config.TeleportRooms.Count());
                foreach (Player Ply in ev.Players)
                { 
                    if (rnd.Next(0, 100) <= Scp914Teleportation.Instance.Config.TeleportChance)
                    {
                        if (Scp914Teleportation.Instance.Config.TeleportBackfire == true)
                        {
                            ApplyTeleportEffects(Ply, Scp914Teleportation.Instance.Config.TeleportEffects);
                        }
                        return;
                    }
                    if (!Scp914Teleportation.Instance.Config.EffectedTeams.Contains(Ply.Team)) return;
                    RoomType roomType = Scp914Teleportation.Instance.Config.TeleportRooms.ElementAt(roomIndex);
                    Room teleportRoom = Map.Rooms.Where(r => r.Type == roomType).First();

                    if (Scp914Teleportation.Instance.Config.TeleportChangeMode == true)
                    {
                        roomIndex = rnd.Next(0, Scp914Teleportation.Instance.Config.TeleportRooms.Count());
                    }

                    Timing.CallDelayed(0.1f, () =>
                    {
                        Ply.Position = teleportRoom.Position + new Vector3(0,2,0);
                        ApplyTeleportEffects(Ply, Scp914Teleportation.Instance.Config.TeleportEffects);
                    });
                }
            }

        }

        public void ApplyTeleportEffects(Player Ply, List<string> Effects)
        {
            if (Ply == null) return;
            if (Effects.Count == 0) return;

            foreach(string effect in Effects)
            {
                string[] args = effect.Split((":").ToCharArray());
                switch (args.First().ToLower())
                {
                    case "applyeffect":
                        try
                        {
                            Ply.ReferenceHub.playerEffectsController.EnableByString(args.ElementAt(1), Convert.ToInt32(args.ElementAt(2)));
                        }
                        catch
                        {
                            Log.Warn($"WARNING: SCP-914 Teleportation effects configured incorrectly. Invalid effect type: {effect}.");
                        }
                        break;
                    case "damage":
                        try
                        {
                            int amt = Convert.ToInt32(args.ElementAt(1));
                            Ply.Hurt(amt, DamageTypes.Wall, "SCP-914 Teleportation");
                        }
                        catch
                        {
                            Log.Warn($"WARNING: SCP-914 Teleportation effects configured incorrectly. Invalid effect type: {effect}.");
                        }
                        break;
                }
            }
        }
    }
}
