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
using Hints;
using System.ComponentModel;
using Mirror;
using Exiled.Permissions.Extensions;
using RemoteAdmin;

namespace Scp914Teleportation
{
    public class EventHandlers
    {

        public Random rnd = new Random();

        public List<Player> teleported = new List<Player> { };
        public void OnUpgradingItems(UpgradingItemsEventArgs ev)
        {
            if (Scp914Teleportation.Instance.Config.TeleportEffects.ContainsKey(ev.KnobSetting))
            {
                Log.Info("SCP-914 Teleportation activated");
                int roomIndex = rnd.Next(0, Scp914Teleportation.Instance.Config.TeleportRooms.Count());
                if (!Scp914Teleportation.Instance.Config.TeleportChance.ContainsKey(ev.KnobSetting))
                {
                    Log.Warn($"TeleportChance config does not provide a setting for the {ev.KnobSetting.ToString()} 914 setting. Aborting teleportation");
                    return;
                }
                foreach (Player Ply in ev.Players)
                {
                    if (rnd.Next(0, 100) > Scp914Teleportation.Instance.Config.TeleportChance[ev.KnobSetting])
                    {
                        if (Scp914Teleportation.Instance.Config.TeleportBackfire == true)
                        {
                            ApplyTeleportEffects(Ply, Scp914Teleportation.Instance.Config.TeleportEffects[ev.KnobSetting]);
                        }
                        return;
                    }
                    if (!Scp914Teleportation.Instance.Config.AffectedTeams.Contains(Ply.Team)) return;
                    RoomType roomType = Scp914Teleportation.Instance.Config.TeleportRooms.ElementAt(roomIndex);
                    Room teleportRoom = Map.Rooms.Where(r => r.Type == roomType).First();

                    if (Scp914Teleportation.Instance.Config.TeleportChangeMode == true)
                    {
                        roomIndex = rnd.Next(0, Scp914Teleportation.Instance.Config.TeleportRooms.Count());
                    }

                    Timing.CallDelayed(0.1f, () =>
                    {
                        Ply.Position = teleportRoom.Position + new Vector3(0,2,0);
                        teleported.Add(Ply);
                        Timing.CallDelayed(Timing.WaitForOneFrame, () =>
                        {
                            ApplyTeleportEffects(Ply, Scp914Teleportation.Instance.Config.TeleportEffects[ev.KnobSetting]);
                        });
                    });
                }
                Timing.CallDelayed(0.5f, () =>
                {
                    if (Scp914Teleportation.Instance.Config.AlertSCPs == true)
                    {
                        // Notify players
                        if (Scp914Teleportation.Instance.Config.AlertInformPlayer == true)
                        {
                            foreach (Player TeleportedPly in teleported)
                            {
                                if (TeleportedPly.Team == Team.SCP) continue; // Prevent SCPs from being notified about other SCPs using SCP-914 to teleport.
                                TeleportedPly.ClearBroadcasts();
                                TeleportedPly.Broadcast(5, Scp914Teleportation.Instance.Config.TeleportPlayerMessage);
                            }
                        }

                        // Notify SCPs
                        foreach (Player SCP in Player.List)
                        {
                            if (SCP.Team == Team.SCP)
                            {
                                string stringToShow = "\n\n\n\n\n\n\n";
                                foreach (Player Escapee in teleported)
                                {
                                    if (Escapee.Team == Team.SCP) continue; // Prevent SCPs from being notified about other SCPs using SCP-914 to teleport.
                                    stringToShow += Scp914Teleportation.Instance.Config.TeleportSCPMessage.Replace("{Name}", Escapee.Nickname).Replace("{Room}", GetRoomName(Escapee.CurrentRoom)).Replace("{Class}", $"<color={Escapee.Role.GetColor().ToHex()}>{Constants.ClassStrings[Escapee.Role.ToString()]}</color>") + "\n";
                                }
                                SCP.ReferenceHub.hints.Show(new TextHint(stringToShow, new HintParameter[] {
                                    new StringHintParameter("")
                                 }, HintEffectPresets.FadeInAndOut(0.25f, 1f, 0.25f), 5f));
                            }
                        }

                    }
                });
            }

            teleported.Clear();

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
                    case "addahp":
                        if (Ply.Role == RoleType.Scp096) continue;
                        try
                        {
                            Ply.AdrenalineHealth += Convert.ToInt32(args.ElementAt(1));
                        }
                        catch
                        {
                            Log.Warn($"WARNING: SCP-914 Teleportation effects configured incorrectly. \"{effect}\" arguments are not valid.");
                        }
                        break;
                    case "applyeffect":
                        try
                        {
                            Ply.ReferenceHub.playerEffectsController.EnableByString(args.ElementAt(1), Convert.ToInt32(args.ElementAt(2)));
                        }
                        catch
                        {
                            Log.Warn($"WARNING: SCP-914 Teleportation effects configured incorrectly. \"{effect}\" arguments are not valid.");
                        }
                        break;
                    case "broadcast":
                        try
                        {
                            int type = Convert.ToInt32(args.ElementAt(1));
                            int dur = Convert.ToInt32(args.ElementAt(2));
                            string msg = string.Join(" ", args.Skip(3));
                            IEnumerable<Player> Plrs = new List<Player> { };
                            switch (type)
                            {
                                case 0:
                                    Plrs = new List<Player> { Ply };
                                    break;
                                case 1:
                                    Plrs = Player.List;
                                    break;
                                case 2:
                                    Plrs = Player.List.Where(p => p.Team == Team.SCP);
                                    break;
                                case 3:
                                    Plrs = Player.List.Where(p => CommandProcessor.CheckPermissions(p.Sender, "AdminChat", PlayerPermissions.AdminChat));
                                    break;
                                default:
                                    Log.Warn($"WARNING: SCP-914 Teleportation effects configured incorrectly. \"{effect}\" first argument (type) is invalid.");
                                    break;

                            }
                            foreach (Player ToSend in Plrs)
                            {
                                ToSend.ClearBroadcasts();
                                ToSend.Broadcast((ushort)dur, msg.Replace("{Name}", Ply.Nickname).Replace("{Room}", GetRoomName(Ply.CurrentRoom)).Replace("{Class}", $"<color={Ply.Role.GetColor().ToHex()}>{Constants.ClassStrings[Ply.Role.ToString()]}</color>") + "\n");
                            }
                        }
                        catch
                        {
                            Log.Warn($"WARNING: SCP-914 Teleportation effects configured incorrectly. \"{effect}\" arguments are not valid.");
                        }
                        break;
                    case "damage":
                        try
                        {
                            int amt = Convert.ToInt32(args.ElementAt(1));
                            if (Ply.IsGodModeEnabled == false)
                            {
                                Ply.Hurt(amt, DamageTypes.Wall, "SCP-914 Teleportation");
                            }
                        }
                        catch
                        {
                            Log.Warn($"WARNING: SCP-914 Teleportation effects configured incorrectly. \"{effect}\" arguments are not valid.");
                        }
                        break;
                    case "dropitems":
                        try
                        {
                            if (args.ElementAt(1) == "all" || args.ElementAt(1) == "*")
                            {
                                Ply.Inventory.ServerDropAll();
                            }
                            else
                            {
                                int amount = Convert.ToInt32(args.ElementAt(1));
                                for (int i = 0; i<amount; i++)
                                {
                                    int pos = rnd.Next(0, Ply.Inventory.items.Count()-1);
                                    Ply.DropItem(Ply.Inventory.items.ElementAt(pos));
                                };
                            }
                        }
                        catch
                        {
                            Log.Warn($"WARNING: SCP-914 Teleportation effects configured incorrectly. \"{effect}\" arguments are not valid.");
                        }
                        break;
                    case "god":
                        try
                        {
                            if (!Ply.IsGodModeEnabled) // do not toggle godmode if they already had it on - that defeats the point
                            {
                                Ply.IsGodModeEnabled = true;
                                Timing.CallDelayed((float)Convert.ToInt32(args.ElementAt(1)), () =>
                                {
                                   
                                    Ply.IsGodModeEnabled = false;
                                });
                            }
                        }
                        catch
                        {
                            Log.Warn($"WARNING: SCP-914 Teleportation effects configured incorrectly. \"{effect}\" arguments are not valid.");
                        }
                        break;
                    case "stamina":
                        if (Ply.Team == Team.SCP) break;
                        try
                        {
                            int amt = Convert.ToInt32(args.ElementAt(1)) / 100;
                            Ply.Stamina.RemainingStamina = amt;
                        }
                        catch
                        {
                            Log.Warn($"WARNING: SCP-914 Teleportation effects configured incorrectly. \"{effect}\" arguments are not valid.");
                        }
                        break;
                    default:
                        Log.Warn($"WARNING: SCP-914 Teleportation effects configured incorrectly. Invalid effect type: {effect}.");
                        break;
                }
            }
        }

        public string GetRoomName(Room R)
        {
            if (R.Zone == ZoneType.Surface)
            {
                return "The Surface";
            }
            switch (R.Type)
            {
                // Entrance Zone
                ///// Structure
                case RoomType.EzCafeteria:
                case RoomType.EzConference:
                case RoomType.EzDownstairsPcs:
                case RoomType.EzPcs:
                case RoomType.EzStraight:
                case RoomType.EzUpstairsPcs:
                    return "an EZ straight";
                case RoomType.EzCrossing:
                    return "an EZ four way";
                case RoomType.EzCurve:
                    return "an EZ turn";
                //// Other  (& Dead End Rooms)
                case RoomType.EzCollapsedTunnel:
                    return "an EZ collapsed tunnel";
                case RoomType.EzGateA:
                    return "gate A";
                case RoomType.EzGateB:
                    return "gate B";
                case RoomType.EzIntercom:
                    return "the EZ intercom hallway";
                case RoomType.EzShelter:
                    return "the EVAC shelter";
                case RoomType.EzVent:
                    return "an EZ red room";
                // Heavy Containment Zone
                //// Containment Chambers
                case RoomType.Hcz049:
                    return "SCP-049's hallway";
                case RoomType.Hcz079:
                    return "SCP-079's containment chamber";
                case RoomType.Hcz096:
                    return "SCP-096's containment chamber";
                case RoomType.Hcz106:
                    return "SCP-106's containment chamber";
                case RoomType.Hcz939:
                    return "SCP-939's containment chamber";
                //// Structure
                case RoomType.HczArmory:
                    return "the HCZ armory";
                case RoomType.HczCrossing:
                    return "a HCZ four way";
                case RoomType.HczCurve:
                    return "a HCZ turn";
                case RoomType.HczHid:
                    return "the Micro-HID hallway";
                case RoomType.HczNuke:
                    return "the Alpha Warhead Silo hallway";
                case RoomType.HczServers:
                    return "the Servers room";
                case RoomType.HczTCross:
                    return "a HCZ three way";
                case RoomType.HczTesla:
                    return "a tesla gate.";
                //// Other
                case RoomType.HczChkpA:
                    return "HCZ elevator system A";
                case RoomType.HczChkpB:
                    return "HCZ elevator system B";
                case RoomType.HczEzCheckpoint:
                    return "the HCZ/EZ checkpoint";
                // Light Containment Zone
                //// Containment Chambers
                case RoomType.Lcz012:
                    return "SCP-012's hallway";
                case RoomType.Lcz173:
                    return "SCP-173's containment chamber";
                case RoomType.Lcz914:
                    return "SCP-914's containment chamber";
                //// Structure
                case RoomType.LczAirlock:
                    return "a LCZ airlock";
                case RoomType.LczCrossing:
                    return "a LCZ four way";
                case RoomType.LczCurve:
                    return "A LCZ turn";
                case RoomType.LczPlants:
                    return "VT 00";
                case RoomType.LczStraight:
                    return "a LCZ straight";
                case RoomType.LczToilets:
                    return "the LCZ WC";
                //// Other (& Dead End Rooms)
                case RoomType.LczArmory:
                    return "the LCZ armory";
                case RoomType.LczCafe:
                    return "PC 15";
                case RoomType.LczChkpA:
                    return "LCZ exit-A";
                case RoomType.LczChkpB:
                    return "LCZ exit-B";
                case RoomType.LczClassDSpawn:
                    return "Class-D cells";
                case RoomType.LczGlassBox:
                    return "GR 18";
                default:
                    return "An unknown room";
            }
        }
    }
}
