using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

using Events = Exiled.Events.Handlers;

namespace Scp914Teleportation
{
    public class Scp914Teleportation : Plugin<Config>
    {
        public override string Name { get; } = "Scp914Teleportation";
        public override string Author { get; } = "Thunder";
        public override Version Version { get; } = new Version(1, 5, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 6);

        public override string Prefix { get; } = "Scp914Teleportation";

        public static Scp914Teleportation Instance;

        public override PluginPriority Priority { get; } = PluginPriority.Low;

        private EventHandlers handler = new EventHandlers();

        public override void OnEnabled()
        {
            base.OnEnabled();

            Instance = this;


            if (!this.Config.IsEnabled) return;

            // Handlers
            Events.Scp914.UpgradingItems += handler.OnUpgradingItems;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();

            if (!this.Config.IsEnabled) return;

            // Handlers
            Events.Scp914.UpgradingItems -= handler.OnUpgradingItems;

            // Goodbye
            Instance = null;
            handler = null;
        }
    }
}
