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
        public bool IsEnabled { get; set; } = true;

        [Description("Determines what teams are allowed to use SCP-914 teleportation.")]
        public List<Team> AffectedTeams { get; set; } = new List<Team> { Team.CDP, Team.RSC, Team.MTF, Team.CHI, Team.SCP, Team.TUT };

        [Description("Determines what settings can be used for SCP-914 teleportation, as well as what happens to players upon teleporting through SCP-914. Add more modes to have them become teleporting modes. See GitHub page for an example on how to use this.")]
        public Dictionary<Scp914Knob, List<string>> TeleportEffects { get; set; } = new Dictionary<Scp914Knob, List<string>> { [Scp914Knob.Coarse] = new List<string> { "Damage:50", "ApplyEffect:Blinded:2", "ApplyEffect:Amnesia:2" } };

        [Description("If set to true, the room will change every time someone teleports. If set to false, it'll change when the machine is started.")]
        public bool TeleportChangeMode { get; set; } = true;

        [Description("Determines the chance to teleport per 914 setting. Must be between 0 and 100.")]
        public Dictionary<Scp914Knob, int> TeleportChance { get; set; } = new Dictionary<Scp914Knob, int> { [Scp914Knob.Coarse] = 50};

        [Description("If they go through on the specified TeleportMode but do not teleport, should the TeleportEffects listed above still be applied?")]
        public bool TeleportBackfire { get; set; } = true;

        [Description("Determines what rooms can be teleported to using SCP-914 teleportation.")]
        public List<RoomType> TeleportRooms { get; set; } = new List<RoomType> { RoomType.LczCafe, RoomType.LczCrossing, RoomType.LczStraight, RoomType.LczTCross, RoomType.LczPlants, RoomType.LczClassDSpawn };

        [Description("Determines if SCPs will be alerted when players teleport.")]
        public bool AlertSCPs { get; set; } = false;

        [Description("Determines if the player will be notified that the SCPs know they have teleported (Message will only be shown if the AlertSCPs config is set to true).")]
        public bool AlertInformPlayer { get; set; } = true;

        [Description("The message to show the teleporting player (if AlertInformSubject and AlertSCPs are true).")]
        public string TeleportPlayerMessage { get; set; } = "The SCPs know you teleported out of SCP-914!";

        [Description("The message to show SCPs if one or more players teleported using SCP-914 and the AlertSCPs config is set to true.")]
        public string TeleportSCPMessage { get; set; } = "{Name} has just teleported out of SCP-914, and has spawned in {Room}! They are a {Class}.";

        /*
        
         CDP:
          - TeleportRooms
            - LczCafe:50
            - LczStraight:50
        RSC:
          - TeleportRooms
            - LczPlants:100
        */

    }
}
