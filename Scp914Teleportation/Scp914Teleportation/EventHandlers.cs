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

namespace Scp914Teleportation
{
    public class EventHandlers
    {

        public Random rnd = new Random();

        public void OnUpgradingItems(UpgradingItemsEventArgs ev)
        {
            if (ev.KnobSetting == Scp914Teleportation.Instance.Config.TeleportMode)
            {
                foreach (Player Ply in ev.Players)
                {
                    int roomIndex = rnd.Next(0, Scp914Teleportation.Instance.Config.TeleportRooms.Count());
                    RoomType roomType = Scp914Teleportation.Instance.Config.TeleportRooms.ElementAt(roomIndex);
                    Room teleportRoom = Map.Rooms.Where(r => r.Type == roomType).First();

                    Timing.CallDelayed(0.1f, () =>
                    {
                        Ply.Position = teleportRoom.Position + new Vector3(0,2,0);
                    });
                }
            }
        }
    }
}
