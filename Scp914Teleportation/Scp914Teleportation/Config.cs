﻿using Exiled.API.Enums;
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

        [Description("Determines what rooms can be teleported to using SCP-914 teleportation.")]
        public List<RoomType> TeleportRooms { get; set; } = new List<RoomType> { };
    }
}