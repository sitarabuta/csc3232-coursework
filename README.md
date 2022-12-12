# CSC3231 Coursework


## Features

This is a **not** an exhaustive list of implemented features.

✅ 5 marks available for Newtonian Physics
  - 2 marks for appropriate use of Newtonian physics
     - Rigidbody2D on character respects Newtonian phyiscs (in 2D space)
  - 3 marks for advanced usage (multiple gravity areas / changing mass / etc)
     - First level has a gravity scale of 3 and second level has a lower gravity scale of 0.75 which sporadically changes to 3 during gameplay

✅ 5 marks available for Collision Detection
  - 2 marks for making use of appropriate collision volumes
     - Collision (both blocking colliders and triggers) is used throughout the game (e.g. characters, interactables and terrain)
  - 3 marks for advanced usage (specific hit areas, dynamic changes over time etc)
     - Specific hit areas are used when jumping on an enemy to kill them; collision changes dynamically when the player shrinks or expands

✅ 5 marks available for Collision Response and Feedback
  - 2 marks for making use of appropriate collision response and feedback
     - Collision is used throughout the game (blocking when needed or causing actions in case of triggers)
  - 3 marks for advanced usage (physics material properties etc)
     - When the player is on ice, their velocity is no longer forcefully set on every frame in order to respect phyisical ice material friction; healing fruits also have a physics material applied with higher bounciness

✅ 15 marks available for AI
  - 2 marks for basic state-based behaviour (menu system, simple opponents)
     - UI menus can be seen both in-game and in the main menu; simple opponents that move and attack when overlapping are present
  - 2 marks for advanced usage (hierarchical state machines etc)
     - Bees have 3 states (idling, chasing, retreating) through which they cycle appropiately (idle->chasing when detecting players, chasing->retreating after hitting them, retreating->idling after arriving "home") and behaviours such as patience (how long they willing to chase the player for)
  - 2 marks for making use of appropriate pathfinding
     - The boss in the second level uses A* pathfinding for reaching the player
  - 2 marks for advanced pathfinding features (dynamic map changes etc)3
     - The A* pathfinding for the boss in the second level is recalculated when objects spawn in the fighting arena
  - 7 marks available for advanced real-time AI techniques (GOAP / flocking / etc)
     - Bees are a flock that follow the player in a flocking pattern and according to flocking rules (respecting distance between individual units, acting together as one, moving towards the target at slightly different rates, etc.)

✅ 20 marks available for probability, adversarial AI, and game design
  - 4 marks for making use of stochastic behaviour
     - There is a chance to drop a healing fruit when killing an enemy or for an icicle to fall when the player is approaching
  - 4 marks for evidence of gameplay modelling
     - Game has level progression which makes the player feel closer to the end (harder enemies, more obstacles, second level has a boss - suggets final fight); multiple paths can also be taken (dillema challenge)
  - 4 marks for demonstrating positive feedback loops
     - Score is increased (i.e. points are awarded) when the player hits enemies, reaches checkpoints, collects coins, etc.; fruits that retore health have a chance to be dropped when killing enemies
  - 8 marks available for advanced gameplay progression techniques (Min/Max, managing state etc)
     - Coins (hidden or that seem next to impossible to collect) can be collected to get extra points; trade-offs such as risking to get a coin for extra damage which could kill the player later in the run; the currently reach level is persistently saved along with the score both between scenes and application restarts; high score can be used to compare to previous best run


## External Content References

- A* Pathfinding:
https://arongranberg.com/astar/

- Pixel Adventure 1:
https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360

- Clouds:
https://latenightcoffe.itch.io/2d-pixel-art-semi-realistic-clouds

- Background Music:
https://www.fesliyanstudios.com/royalty-free-music/downloads-c/8-bit-music/6

- Sound Effects:
https://pixabay.com/sound-effects/search/8-bit/

- CineMachine:
Built-in Unity Package
