# ZombieShoot
**2.5D TopDown Zombie Quest Shooter** [Unfinished]


**Technologies**:
C# + Unity


**Common**:
Structered Project (models, controllers).
Currently almost all classes extends Monobehaviour - it's easier for debugging via editor,
but it's also easy to remove all dependencies on Monobehaviour and leave only MainInitializator.
Easy to step realizing MVVM pattern.


Already implemented mechanics:
- physics person movement
- opening boxes
- inventory logic
- zombies moving at point
- zombies movement-attacking logic
- riddle "find key to exit"
- highlighting key objects


**TODO** **Need finish**
 - Change mouse pointer on smth interesting,
 - Finish animation slide movement
 - handling weapon with animation
 - gui
 - sound effects
 - zombies/character attack on animation
 - attack animation, weapon changing



**Fix**
Zombies splitting into one line object when attack non movable character.
Repeatable jumping - set timeout for pressing jump key
Timeout for displaying items in boxes
Timeout for zombies attack
Timeout for 


**Hardware**:
PC, Keyboard, Mouse


**Controls**:
[wasde]keys, [01]mouse


**Before launch as a game**:
 - Set Baracks_Beta Scene
 - Main Initilizator scene object contains all controllers scripts that used due game experience
 - Disable directional lights
 - Enable fog in light settings: "/Lighting/Environment/OtherSettings"
