# Project Shoot 'Em Up

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Ben Sultzer
-   Section: 06

## Game Design

-   Camera Orientation: Topdown
-   Camera Movement: Stationary
-   Player Health: The player dies in three hits, however, if they collect enough pieces of enemy ships to upgrade to the next hull, their hitpoints will reset. They can get hit a certain number of times at each hull level until that hull breaks off of them and they get demoted, at which point their hitpoints are reset and they will lose if they are at a hull level of 1 and get hit three times.
-   End Condition: The player wins by collecting enough enemy ship pieces to upgrade to the final hull level and reaches a score of 25000 points.
-   Scoring: The player gets points by killing enemies. At each ascending hull level, the player earns more points per enemy kill.

### Game Description

Inspired by Asteroids, the game takes place in open space where enemy ships randomly spawn in waves, shooting at and chasing the player while wrapping around the screen edges, along with the player, until they are killed. The waves of enemies grow more difficult as the player attempts to destroy enemy ships, collect points and enemy ship parts, upgrade their own ship, and reach the final hull level and score!

### Controls

-   Movement
    -   Up: W/Up Arrow Key
    -   Down: S/Down Arrow Key
    -   Left: A/Left Arrow Key
    -   Right: D/Right Arrow Key
-   Fire: Left Mouse Button (No omni-drectional aiming with mouse, only serves to fire a projectile in the direction the ship is facing.)

## Your Additions

-   When a player destroys an enemy's ship, occasionally a ship part will fly off into space simulating accurate destruction physics in zero-gravity, where the player will need to collect enough of them to upgrade to the next hull level. If the player is hit enough, their own ship will be demoted to the previous hull level. The ship movement will also simulate accurate forces in zero-gravity, such as acceleration, thrust, etc.

## Sources

-   I created my own ship, projectile, ship part, and background art

## Known Issues

-   Enemies will occasionally despawn, which I believe may be because the engine slowly loses performance as enemies, ship parts, and projectiles are continuously destroyed. This may cause the projectiles that actually hit those enemies to not be rendered.
-   After a certain amount of time of being at hull level 2, I believe it to be the second time the player reaches a hull level of 2, they become invincible. I believe it may be because a variable controlling the lives in relation to the hull level is not reset properly.

### Requirements not completed

-   N/A

