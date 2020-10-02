using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scp914Teleportation
{
    public class Constants
    {
        public static Dictionary<string, string> ClassStrings = new Dictionary<string, string>
        {
            // SCPs
            ["Scp049"] = "SCP-049",
            ["Scp0492"] = "SCP-049-2",
            ["Scp079"] = "SCP-079",
            ["Scp096"] = "SCP-096",
            ["Scp106"] = "SCP-106",
            ["Scp173"] = "SCP-173",
            ["Scp93953"] = "SCP-939-53",
            ["Scp93989"] = "SCP-939-89",

            // Classes
            /// On Spawn
            ["ClassD"] = "Class-D Personnel",
            ["Scientist"] = "Scientist",
            ["FacilityGuard"] = "Facility Guard",

            /// Reinforcements
            ["NtfCadet"] = "NTF Cadet",
            ["NtfLieutenant"] = "NTF Lieutenant",
            ["NtfCommander"] = "NTF Commander",
            ["NtfScientist"] = "NTF Scientist",

            ["ChaosInsurgency"] = "Chaos Insurgency",

            // Other
            ["Tutorial"] = "Tutorial",
        };

        public List<Player> GetPlayersFromString(string str)
        {
            if (str == "*")
            {
                return Player.List.ToList();
            }
            try
            {
                int number = Convert.ToInt32(str);
                return new List<Player> { Player.Get(number) };
            }
            catch
            {
                Log.Warn("Error in 'GetPlayersFromString' method");
            }
            return new List<Player> { };
        }
    }
}
