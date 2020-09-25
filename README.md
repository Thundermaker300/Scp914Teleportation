## SCP-914 Teleportation
Ever get stuck in SCP-914 with three SCPs outside, waiting for you to leave just so you can be instantly murdered? No longer! This plugin allows you to put yourself through SCP-914 on a specific setting and escape.

## Configurations
More settings are planned.
| Setting            | Type           | Default                                                                   | Description                                                                                                                  |
|--------------------|----------------|---------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------|
| IsEnabled          | boolean        | false                                                                     | Enables SCP-914 teleportation.                                                                                               |
| TeleportMode       | Scp914Knob     | Coarse                                                                    | Determines what SCP-914 setting will cause the teleport.                                                                     |
| EffectedTeams      | Team List      | [CDP, RSC, MTF, CHI, SCP, TUT]                                            | Determines what teams are allowed to use SCP-914 teleportation.                                                              |
| TeleportChangeMode | boolean        | true                                                                      | If set to true, the room will change every time someone teleports. If set to false, it'll change when the machine is started.|
| TeleportRooms      | RoomType List  | [LczCafe, LczCrossing, LczStraight, LczTCross, LczPlants, LczClassDSpawn] | Determines what rooms can be teleported to using SCP-914 teleportation.                                                                           |
| TeleportEffects    | String List    | ["Damage:50", "ApplyEffect:Blinded:2",  "ApplyEffect:Amnesia:2"]          | Determines what happens to players upon teleporting through SCP-914. See below for a list of valid strings for this config.  |

## TeleportEffects - Valid Strings
`Damage:N` - Deals N damage to the user upon teleporting.  
`ApplyEffect:EffectType:Duration` - Applies the EffectType effect for the Duration (in seconds).  
(More soon!)