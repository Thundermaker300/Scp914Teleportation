using Exiled.API.Enums;
using Exiled.API.Interfaces;
using Scp914;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scp914Teleportation
{
    public sealed class Config : IConfig
    {
        [Description("Enables SCP-914 teleportation.")]
        public bool IsEnabled { get; set; } = false;

        [Description("Determines what SCP-914 setting will cause the teleport.")]
        public Scp914Knob TeleportMode { get; set; } = Scp914Knob.Coarse;

        [Description("Determines what teams are allowed to use SCP-914 teleportation.")]
        public List<Team> AffectedTeams { get; set; } = new List<Team> { Team.CDP, Team.RSC, Team.MTF, Team.CHI, Team.SCP, Team.TUT };

        [Description("If set to true, the room will change every time someone teleports. If set to false, it'll change when the machine is started.")]
        public bool TeleportChangeMode { get; set; } = true;

        [Description("Determines the chance to teleport. Must be between 0 and 100.")]
        public int TeleportChance { get; set; } = 50;

        [Description("If they go through on the specified TeleportMode but do not teleport, should the TeleportEffects listed below still be applied?")]
        public bool TeleportBackfire { get; set; } = true;

        [Description("Determines what rooms can be teleported to using SCP-914 teleportation.")]
        public List<RoomType> TeleportRooms { get; set; } = new List<RoomType> { RoomType.LczCafe, RoomType.LczCrossing, RoomType.LczStraight, RoomType.LczTCross, RoomType.LczPlants, RoomType.LczClassDSpawn };

        [Description("Determines what happens to players upon teleporting through SCP-914. See GitHub page for an example on how to use this.")]
        public List<string> TeleportEffects { get; set; } = new List<string> { "Damage:50", "ApplyEffect:Blinded:2", "ApplyEffect:Amnesia:2", };

    }
}
