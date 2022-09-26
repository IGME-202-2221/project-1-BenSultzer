# Project Shoot 'Em Up

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Ben Sultzer
-   Section: 06

## Game Design

-   Camera Orientation: Topdown
-   Camera Movement: Stationary
-   Player Health: The player dies in one hit, however, if they collect enough pieces of enemy ships to upgrade to the next hull, their hitpoints will increase. They can get hit a certain number of times at each hull level until that hull breaks off of them and they get demoted, at which point their hitpoints are reduced again to the previous level.
-   End Condition: The player wins by collecting enough enemy ship pieces to upgrade to the final hull level.
-   Scoring: The player gets points by killing enemies and collecting enemy ship pieces. At each ascending hull level, the player earns more points per enemy kill.

### Game Description

Inspired by Asteroids, the game takes place in open space where enemy ships randomly spawn off-screen, shooting at and chasing the player while wrapping around the screen edges along with the player until they are killed. The waves of enemies grow more difficult as the player attempts to destroy enemy ships, collect points and enemy ship parts for each kill, upgrade their own ship, and reach the final hull level!

### Controls

-   Movement
    -   Up: W/Up Arrow Key
    -   Down: S/Down Arrow Key
    -   Left: A/Left Arrow Key
    -   Right: D/Right Arrow Key
-   Fire: Left Mouse Button (No omni-drectional aiming with mouse, only serves to fire a projectile in the direction the ship is facing.)

## Your Additions

-   When a player destroys an enemy's ship, occasionally three ship parts will fly off into space simulating accurate destruction physics in zero-gravity, where the player will have a limited time to collect enough of them to upgrade to the next hull level. If the player is hit enough, their own ship will break apart down to the previous hull level and they will not be able to re-collect those broken pieces. The ship movement will also simulate accurate forces in zero-gravity, such as acceleration, thrust, etc.

## Sources

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

