# EverPlains
## Contributors
Jonathan Rivera and Tram Vuong

## Description
EverPlains is a 2D platformer/side scroller created in Unity, which uses C#. The main components of the game are described below.  

##### Player
The player can run left and right, double jump, and block. Currently, the player will have a sword slash basic attack and move across the screen to destroy enemies.

##### Enemies
The current game has 2 enemy types, each with their own unique movement and attacks. Enemies will chase the player and attack when they are close in range. There is a final boss, which is a larger slime with more health that the player must defeat to win the game. Defeating enemies will give the player either health or gold. Gold obtained by the player will acculumate as the player moves through the levels. 

##### Health and mana system
HP and mana are recorded on the top left screen. Mana can be gained by destroying enemies scattered around the level. Currently, mana is recorded but is not used for anything. Our goal is to implement a more thorough mana system that allows the player to use a special ranged attack. 

##### Future item(s)
Initially, we wanted to have a projectile special attack that would travel across the screen. When we were ready to add it, we realized that the special attack code would have to change the enemy behavior, especially when taking special damage. So we parked this item, but we would like to return to it in the near future.

## Roadmap
The roadmap can be viewed at: https://app.gitkraken.com/glo/board/YOOYqEEZ3QASG6vP

## How to build, install, and run the project

#### Release Downloads
[Windows](https://github.com/EverPlainsGroup/EverPlains/releases/download/v0.1-alpha/EverPlains-windows.zip)

[Mac (untested)](https://github.com/EverPlainsGroup/EverPlains/releases/download/v0.1-alpha/EverPlains-mac.app.zip)

#### Checkout instructions
1. Fork this repository
2. Clone the forked repository
3. Use the following git command to make your own branch with your changes and then switch to that branch: git checkout -b name-of-your-branch
   Example: git checkout -b feature/new-special-attack

#### Prerequisites for build

    Unity version 2020.3.14f1 or newer

#### Installation instructions
 - Download [Unity Hub](https://unity3d.com/get-unity/download)
 - In Unity Hub, install version 2020.3.14f1 or newer
 - In Unity Hub > Projects, hit the "Open" button and select the project folder

#### Documentation
This project uses Doxygen for documentation. Download Doxygen [here](https://www.doxygen.nl/index.html).

## Example screenshot
![Slashing some slimes](https://i.gyazo.com/3c952bebe560c1c2885d76e6af599ad4.png)

## How to play
- Move Left - A
- Move Right - D
- Jump - Space
- Attack - J
- Block - K

## Additional requirements
The following are additional requirements for CS461/561 Open Source course taught by Bart Massey.

### Project video
The project video can be found [here](https://youtu.be/8OcopNqZeU4).

### Work summary/member contributions

 - Tram Vuong - enemies, level design, level transitions, game over
 - Jonathan Rivera - player, collisions, hud, camera, victory

## Acknowledgements

### Assets
We want the main focus to be on developing game mechanics and systems. Therefore, we plan on using free asset packs as much as possible and will only our own assets if needed. The following assets were used:

[Slime Mob](https://assetstore.unity.com/packages/2d/characters/free-pixel-mob-113577) - sprites, animations, animation controller  
[Hero Knight](https://assetstore.unity.com/packages/2d/characters/hero-knight-pixel-art-165188) - sprites, animations, animation controller  
[Nature Pixel Art](https://assetstore.unity.com/packages/2d/environments/nature-pixel-art-base-assets-free-151370) - props, tilemap  
[Pixel Mobs](https://assetstore.unity.com/packages/2d/characters/pixel-mobs-54995) - bat sprite, bat animations, bat animation controller  
[2D Pixel Item Asset Pack](https://assetstore.unity.com/packages/2d/gui/icons/2d-pixel-item-asset-pack-99645) - potion sprite, gold coin sprite

### References
The following resources were used to learn various aspects of creating a 2D platformer in Unity. Some of the code may be adapted in some way. Huge thanks to the many content creators that dedicate their time to teaching others.

[*2D Platformer in Unity* video series by Muddy Wolf Games](https://www.youtube.com/playlist?list=PLfX6C2dxVyLw5kerGvTxB-8xqVINe85gw)  
[*How to Make a 2D Character Controller in Unity* by Blackthornprod](https://www.youtube.com/watch?v=CeXAiaQOzmY)  
[*Create a 2D Platformer* video series by Unity](https://www.youtube.com/watch?v=j29NgzV8Dw4)  
[*MELEE COMBAT in Unity* by Brackeys](https://youtu.be/sPiVz1k-fEs)  
[*2D Game Dev Tutorial - Melee Attacking in Unity* by Lost Relic Games](https://www.youtube.com/watch?v=KamdeKs6eKo)

## License
The license can be found in [LICENSE](https://github.com/EverPlainsGroup/EverPlains/blob/main/LICENSE)  in the root directory.

