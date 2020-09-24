## SCP-914 Teleportation
Ever get stuck in SCP-914 with three SCPs outside, waiting for you to leave just so you can be instantly murdered? No longer! This plugin allows you to put yourself through SCP-914 on a specific setting and escape.

## Configurations
| Setting         | Type           | Default                                                                   | Description                                                                                                                 |
|-----------------|----------------|---------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------|
| IsEnabled       | boolean        | false                                                                     | Enables/Disables the plugin                                                                                                 |
| TeleportMode    | Scp914Knob     | Coarse                                                                    | Determines the mode to teleport on.                                                                                         |
| TeleportRooms   | List<RoomType> | [LczCafe, LczCrossing, LczStraight, LczTCross, LczPlants, LczClassDSpawn] | Determines the rooms that players can teleport to.                                                                          |
| TeleportEffects | List<string>   | ["Damage:50", "ApplyEffect:Blinded:2",  "ApplyEffect:Amnesia:2"]          | Determines what happens to players upon teleporting through SCP-914. See below for a list of valid strings for this config. |

## TeleportEffects - Valid Strings
`Damage:N` - Deals N damage to the user upon teleporting.
`ApplyEffect:EffectType:Duration` - Applies the EffectType effect for the Duration (in seconds).
(More soon!)