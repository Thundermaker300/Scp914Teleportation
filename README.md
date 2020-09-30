## SCP-914 Teleportation
Ever get stuck in SCP-914 with three SCPs outside, waiting for you to leave just so you can be instantly murdered? No longer! This plugin allows you to put yourself through SCP-914 on a specific setting and escape.

## Configurations
More settings are planned.
| Setting               | Type                                    | Default                                                                                   | Description                                                                                                                                                                               |
|-----------------------|-----------------------------------------|-------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| IsEnabled             | boolean                                 | true                                                                                      | Enables SCP-914 teleportation.                                                                                                                                                            |
| AffectedTeams         | Team List                               | [CDP, RSC, MTF, CHI, SCP, TUT]                                                            | Determines what teams are allowed to use SCP-914 teleportation.                                                                                                                           |
| TeleportEffects       | Dictionary<Scp914Knob, List<string>>    | {[Scp914Knob.Coarse] = {"Damage:50","ApplyEffect:Blinded:2","ApplyEffect:Amnesia:2"} }    | Determines what settings can be used for SCP-914 teleportation, as well as what happens to players upon teleporting through SCP-914. Add more modes to have them become teleporting modes.|
| TeleportChangeMode    | boolean                                 | true                                                                                      | If set to true, the room will change every time someone teleports. If set to false, it'll change when the machine is started.                                                             |
| TeleportChance        | <Dictionary<Scp914Knob, integer>        | 50                                                                                        | Determines the chance to teleport per 914 setting. Must be between 0 and 100.                                                                                                             |
| TeleportBackfire      | boolean                                 | true                                                                                      | If they go through on the specified TeleportMode but do not teleport, should the TeleportEffects listed below still be applied?                                                           |
| TeleportRooms         | RoomType List                           | [LczCafe, LczCrossing, LczStraight, LczTCross, LczPlants, LczClassDSpawn]                 | Determines what rooms can be teleported to using SCP-914 teleportation.                                                                                                                   |
| AlertSCPs             | boolean                                 | false                                                                                     | Determines if SCPs will be alerted when players teleport.                                                                                                                                 |
| AlertInformPlayer     | boolean                                 | true                                                                                      | Determines if the player will be notified that the SCPs know they have teleported (Message will only be shown if the AlertSCPs config is set to true).                                    |
| TeleportPlayerMessage | string                                  | The SCPs know you teleported out of SCP-914!                                              | The message to show the teleporting player (if AlertInformSubject and AlertSCPs are true).                                                                                                |
| TeleportSCPMessage    | string                                  | {Name} has just teleported out of SCP-914, and has spawned in {Room}! They are a {Class}. | The message to show SCPs if one or more players teleported using SCP-914 and the AlertSCPs config is set to true.                                                                         |

## TeleportEffects - Valid Strings
- `AddAHP:Amount` - Gives the player the specified amount of artificial health (AHP)  
  - Note that this is add, **NOT** set. If player's are teleported back to SCP-914, the effect can be stacked.  
  - This effect ignores SCP-096 (the rest of the SCPs can use it). I've discovered that this effect breaks SCP-096's hume shield.  
- `Broadcast:Type:Duration:Message` - Displays a broadcast for Duration (in seconds) with the specified message.  
  - Valid parameters for `Type`:  
    - `0` - Will show a broadcast to the person teleporting  
    - `1` - Will show a broadcast to everyone in the server  
    - `2` - Will show a broadcast to all SCPs.  
    - `3` - Will show a broadcast to all users with the AdminChat RA permission.
  - Valid text replacements in `Message`:
    - `{Name}` will turn into the teleported player's name.
    - `{Class}` will turn into the teleported player's class (and color it).
    - `{Room}` will turn into the name of the room the player teleported into.
- `ApplyEffect:EffectType:Duration` - Applies the EffectType effect for the Duration (in seconds).  
- `Damage:N` - Deals N damage to the user upon teleporting. Does no damage to godded users.  
- `DropItems:N` - Drops the amount of items specified from the player's inventory (chooses items randomly). Set N to `*` to drop all items in their inventory.
- `God:N` - Puts the player on god mode (can't take damage/die) for N seconds.  
- `Stamina:N` - Sets player's stamina amount to N. Must be between 0-100 (100 being full stamina and 0 being no stamina).
  - Stamina can only go down, not up, with this effect.  
  - This effect ignores SCPs
(More soon!)

## AffectedTeams - Valid Teams
- `CDP` - Class-D Personnel
- `RSC` - Scientists
- `MTF` - Guards + All NTF Ranks
- `CHI` - Chaos Insurgency
- `SCP` - All SCPs
- `TUT` - Tutorials

## TeleportRooms - Valid Rooms
**Light Containment Zone**  
- `Lcz012` - SCP-012's hallway
- `Lcz173` - Outside of SCP-173's chamber *WARNING: I've tested this and discovered that it sometimes does teleport out of the map. Would not recommend using this one.*
- `Lcz914` - Inside of SCP-914's chamber
- `LczAirlock` - One of the two LCZ airlocks (random)
- `LczArmory` - Outside of the LCZ armory.
- `LczCafe` - The PC15 room.
- `LczChkpA` - Exit A on the LCZ side.
- `LczChkpB` - Exit B on the LCZ side.
- `LczClassDSpawn` - The Class-D cell hallway.
- `LczCrossing` - One of the LCZ four ways (labled IX) (random)
- `LczCurve` - One of the LCZ curves (labeled HC) (random)
- `LczGlassBox` - SCP-372's containment chamber, also known as GR18
- `LczPlants` - VT00
- `LczStraight` - One of the LCZ straights (labeled HS) (random)
- `LczTCross` - One of the LCZ T-Crosses (labeled IT) (random)
- `LczToilets` - The WC hallway.  

**Heavy Containment Zone**  
- `Hcz049` - The hallway outside of SCP-049's elevator.
- `Hcz079` - The short hallway outside of SCP-079's containment chamber.
- `Hcz096` - The small workspace outside of SCP-096's containment chamber.
- `Hcz106` - The walkway outside of SCP-106's containment chamber.
- `Hcz939` - The walkway above SCP-939's containment chamber.
- `HczArmory` - Outside of the HCZ armory.
- `HczChkpA` - Elevator system A in heavy.
- `HczChkpB` - Elevator system B in heavy.
- `HczCrossing` - One of the HCZ four ways (random)
- `HczCurve` - One of the HCZ turns (random)
- `HczEzCheckpoint` - The HCZ/EZ checkpoint.
- `HczHid` - The Micro-HID hallway.
- `HczNuke` - The hallway leading to the alpha warhead.
- `HczServers` - The HCZ servers room
- `HczTCross` - One of the HCZ T-Crosses (random)

**Entrance Zone**  
- `EzCafeteria` - The EZ straight hallway consisting of two benches.
- `EzCollapsedTunnel` - One of the EZ dead-ends featuring scattered debris *WARNING: Can sometimes teleport ON the debris and cause stuck players. Use with caution.*
- `EzConference` - The EZ straight hallway consisting of two locked doors, one of Dr. L's room and one of Conference Room 9B.
- `EzCrossing` - One of the EZ four ways (random)
- `EzCurve` - One of the EZ turns (random)
- `EzDownstairsPcs` - The EZ straight hallway with a stairwell downward, consisting of offices.
- `EzGateA` - Outside of Gate A.
- `EzGateB` - Outside of Gate B.
- `EzIntercom` - Outside of the intercom room.
- `EzPcs` - The EZ straight hallway consisting of offices and TV screens.
- `EzStraight` - A standard EZ straight hallway.
- `EzUpstairsPcs` - The EZ straight hallway with a stairwell upward, consisting of offices.
- `EzVent` - One of the EZ red room dead-ends.  

**Surface**  
- `Surface` - Gate A at the surface.