using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Events = Exiled.Events.Handlers;

namespace Scp914Teleportation
{
    public class Scp914Teleportation : Plugin<Config>
    {
        public static Scp914Teleportation Instance;

        public override PluginPriority Priority { get; } = PluginPriority.Low;

        private EventHandlers handler = new EventHandlers();

        public override void OnEnabled()
        {
            base.OnEnabled();

            Instance = this;

            // Handlers
            Events.Scp914.UpgradingItems += handler.OnUpgradingItems;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();

            // Handlers
            Events.Scp914.UpgradingItems -= handler.OnUpgradingItems;

            // Goodbye
            Instance = null;
            handler = null;
        }
    }
}
