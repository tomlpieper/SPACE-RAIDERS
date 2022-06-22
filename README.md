# SPACE-RAIDERS

## Developers Note
This Repo contains my whole final version of the course project: Introduction to Unity.
As I already spoke to Jule Bufler regarding the missing Git-History of my project, I try to make my development process and structure of this project very clear in this README as well as in the comments inside the C#-Scripts.

With a solid base in OOP but no experience in visually based development this course was the perfect way to start my way into GameDev. This is also the reason why i decided to work on this project on my own - of course with the help of the courseware but also with a lot of other openly available online resources such as Youtube tutorials and StackOverflow threads. 
Since I developed the project on my own machine and I was told by other students that the Git-history was mainly neccessary to proof that everybody in the group did work on the project, I include a timeline of the development in the end of this README to make up for the missing history.
I coded everything in this project myself (of course there are certain base concepts like the FPS-Movement or Interaction System which I coded myself but very closely sticked to tutorials online.) If there are any further questions regarding this, please to not hesitate to contact me.

Project idea and implementation by Tom Pieper.

Note: As this is a private repo and video, please do not share the links to the youtube video.



## Game Idea 
My game idea was to make a First-Person SciFi-themed survival game. Therefore one can see a glimpse of Star Wars or other franchises in the level-design and concepts. While first wanting to create a simple Jump-and-Run-Game, my fascination for the possiblities in the unity engine, once I got ahang of it resulted in more and more concepts i wanted to include in the game. Last but no least the game as it is now also shall work as a framework for further developing levels and creating new content in this world easily which is why I created a lot of structure and mainly focused on interaction with the environment rather than making an endless game with a lot of levels as this project was very fun yet already very timeconsuming.
The first level (tutorial) focuses on the basic concepts and also interior design and baking of lightmaps.
The actual first level is designated to enemy AI, and playing the actual game.
I hope the idea gets clear when watching the video uploaded [here](https://youtu.be/mBIjuP0cHk8). 



## Main Concepts of the Game 
This abstract explains the main concepts and framework structures I mentioned above. 
I created to make creating and scaling the game more easy and fun. While being a lot of work in the first place the project gets way more easy to structure and further develop.

### FPS-Movement and Interaction System
Firstly I created a Character Controller for the FPS movement and looking with mouse and an interaction system that consists of a new class and corresponding layermask "Interactable". With this structure, every object of desire can be easily converted to be interactable with the player. When the player looks at a certain object with the mask and script (this is done with Raycasting), a customized message is displayed in the players UI. When pressing the key that is related to interaction-action in the unity input system the Interact() function of that object is triggered. 
This gives room to many possiblities of actively changing the environment durin runtime e.g. destroying objects, opening doors, playing a sound,  changing light and so on - basically anything one can put in a function. 

### SuperScripts 
Superscript (not all are in the corresponding folder, some are also in the Playerscripts folder but act as superscripts) act as a framewokr and are are handling the big picture stuff, like changing scenes, saving data, interaction between classes other things (really depending on the individual script).

### Collection System
The collection system acts similar to the interaction system but is not triggered by and interaction function but rather by colliders, that are triggered when walking into them. Each item(prefab of an item) has their own effects(more info in the how to play section).

### Weapon System
The weapon is attached to the players cam as a child object which moves with its parent. The shooting of the gun again is possible through raycasts which proofed more simple than using projectiles. If the raycast shot hits an object of type enemy i.e. that has a TakeDamage() function this function is triggered and therefore gets hit. The visual animation consist of a simple particle system of the below provided assets. The firerate is set to 4 Shots/Second - AutoFire.

### Enemy AI
One of the last concepts I integrated was enemy AI in form of KamizazeDrones.

The basic idea consists of NavMeshAgents, that are switching between 3 States: patroling, chasing, and attacking (in our case exploding)
The States are dependent on the distance to the player - if the player steps inside sightRange, the drone will chase after him, if it gets close enough it will explode and damage him.
In "Idle" / patroling mode, the drone will randomly select a walkpoint on the NavMesh and fly there.
Although each function is reletively basic, the change of states is smooth an gives the enemies a kind of smart appearence and makes the game more dynamic.



## How to play
The game is very easy to play and relatively easy to win if won is patient but not too patient. One moves with the WASD Keys and sprints with LShift. Interacing with an gameobject can be done by pressing the E-Key. Shooting is done by pressing the left mouse button. 


### How to win
In the tutorial your only task is to get familiar with the interaction and collection system - you cannot die but are supposed to get a little familiar with the game mechanics.

Once the tutorial is finished the player must colled the big tanks representing the parts needed to repair the ship while watching his o2-level on the left side of the screen, which decays during the game with every frame. One can counter this process by collecting the smaller o2 tanks. Ammunition so far is endless. To win the Level 1 one must collect all 3 tanks without dying from low oxygen or too much damage done by the enemies. For a clearer picture on this, please watch the video I uploaded on StudIP.



## Resources and Assets
- [Low Poly SciFi City Asset Pack](https://assetstore.unity.com/packages/3d/environments/sci-fi/polygon-sci-fi-city-low-poly-3d-art-by-synty-115950)
- [Polygon SciFi Space Asset Pack](https://assetstore.unity.com/packages/3d/environments/sci-fi/polygon-sci-fi-space-low-poly-3d-art-by-synty-138857)
- [Polygon SciFi Worlds](https://assetstore.unity.com/packages/3d/environments/sci-fi/polygon-sci-fi-worlds-low-poly-3d-art-by-synty-206299)

Note: these packages were on Sale as a package for very, very cheap so don't worry ;)



## Development Timeline
Note: I  - as already mentioned - include this to give an insight into my development process. They may not be 100% accurate but are as good as I recall and my calender tells me.


- 7th June 2022: Start of Project, Start of implementing the movement system
- 9th June 2022: FPS System working, first try of Interaction system
- 13th June 2022: Interaction system working, Including Assets in the project, start of designing first level
- 15th June 2022: First Level designed, only hangar, start of playerstatshandler and between class interaction
- 17th June 2022: Design Cockpit hangar, second (or first actual level after tutorial) level first part
- 18th June 2022: Design second part of actual level
- 20th June 2022: Including weapon system and NavMeshTryout (still included in the files)
- 21st June 2022: NavMeshAgents working an flying :)
- 22nd finishing up on SceneSwitching and LevelHandler, commenting last parts of the code, github upload, README


## Thanks
At last I want to thank you for reading this very long readme and before everything else, giving this course. I've been meaning to do something like this for a long time now and having the frame to do this was all it took. 


