Authentic Early Medieval Ages Audio Pack: SETUP GUIDE

Dear Unity Developer,
Thank you for downloading this package and supporting my work!

Here are some indications as to how to use this pack.

********************************************************************************************************************************************************************************************************
GENERAL REMARKS:

- DO NOT, under any circumstance, change the folder structure of the "Resources" folder.  The resources folder HAS to be located in your asset folder and the folder structure HAS to remain untouched.
  The reason is that many scrips rely on this folder structure to load the samples into arrays. Many scripts could stop working if you change the folder structure.

- Some samples can be looped using the "loop" function of Unity but others rely on scripts to ensure there is no "break" in the sound.
  In order to avoid abrupt cuts in the natural release of instruments (the time it takes for the reverb and sound to completely disappear) you have to alternate between two samples instead of looping them.
  The samples that work with the Unity "loop" function are located only in the "Ambient" and the "FX loopable" folders.  All other samples require a script to loop seamlessly.

- Be sure to add an audio listener to your camera.  Apparently, this helps with the overall performance of the script.

*********************************************************************************************************************************************************************************************************


1) Medieval Master Script Prefab

The Medieval Master Script Prefab can be found in the "Prefabs" folder inside the "Authentic Early Medieval Ages Audio Pack".
Simply drag and drop it into your scene to set it up and manipulate the four colored squares, resize them and rotate them to set up the trigger zones.
You can duplicate them as necessary in your scene, test the triggers, and once you're satisfied with their location, disable their "Mesh Renderer" to make them invisible.

The color codes work as follows:
- The green square activates the "Random Folk Dance" music (and deactives all other music)
- The white square activates the "Medieval Church" music (choosing randomly from the two medieval organs and the gregorian chant)
- The light blue square activates the "Medieval Ambiant Lute" music
- The purple/blue square activates the "Medieval Harp" music
- The pink square activates the "Medieval Flute" music
- The grey square activates the "Gregorian Ensemble" music.
- The red square stops all the music and resets the script.


2) Adaptive music according to the action

The Exploration and Battle Prefab can be found in the "Prefabs" folder inside the "Authentic Early Medieval Ages Audio Pack".
Simply drag and drop it into your scene to set it up and manipulate the four colored squares, resize them and rotate them to set up the trigger zones.
You can duplicate them as necessary in your scene, test the triggers, and once you're satisfied with their location, disable their "Mesh Renderer" to make them invisible.

The color codes work as follows:
- The light yellow activates the initial exploration mode (when first launching the script)
- The light orange transitions from exploration mode to battle mode (allow for a few seconds for the transition to happen)
- The light green transitions from the battle mode back to the exploration mode
- The red square stops the music and resets the script.

IMPORTANT TO REMEMBER
---------------------

Please note that you need to wait before some transitions occur. This is precisely to AVOID doing the good old "fade in - fade out" effects that you hear in many soundtracks.
The transitions will occur always at the end of the natural musical "cycle" of the samples so it sounds like a regular composition.

For instance, if you are in Battle mode, the "natural" sequence of the samples is an alternation between an A melodic phrase and a B melodic phrase.
If the player walks through the trigger to transition to exploration mode while in battle mode, the transition to break out of battle mode will only be played once the B melodic phrase was played.


Remember that to have a good user experience with this technique, the key word is ANTICIPATION.  
You need to predict for instance that the player is likely to reach an enemy in 2, 3, or more seconds and trigger the transition in advance so that the Battle sequence 
initiates as close as possible with the actual encounter with the enemy.  Similarily, while in Battle, you need to calculate when all enemies are likely to be slain 
by measuring for instance their remaining health and that of the player to trigger the "end battle" sequence as close as possible to either the death of all enemies or that of the player.

4) Nine full pieces of music (in both loopable and non loopable versions)

The 9 pieces of music can be found in the "Music" folder".

5) 18 chimes to underline events

There are 18 chimes available for your game in the "Chimes" folder.  These are very short jingles that you can use when an event occurs like a victory, the acquisition of a new item etc.

6) Multiple ambiance sounds, all loopable

In the "Ambient" folder, you will find a variety of useful ambiance sounds that you can attach to any object, using the "loop" function in Unity.

7) As a bonus, over 206 medieval sound FX

I am mostly a music composer and by no means a specialist in special effects.  This is why I am offering these samples as a "bonus" and not as an "official" part of this package. 
They are a combination of my own recordings and various samplings from royalty free sources.  Therefore, there can be a big difference in the sound quality and "footprint".
What I mean by this is, it is very important to use the same microphone as often as possible when creating sound effects otherwise they will "sound" very different between samples, which does not always
sound very good. In future releases, if you believe that some of the sound FX I have produced are valuable, I will try to use my own mics to record as many sounds as possible instead of sampling royalty free
audio, so feel free to leave a comment with some suggestions! 
(PS: I know there is no female voice in the FX, but I don't really know any native enlish speaker in my country! :-P )

Please refer yourself to the videos I have put online to demo this pack as you can see how everything works!

I hope you'll be able to make use of this!
Don't forget to leave a review and suggestions/ideas for how to make this work even better!
I have purposefully created a variety of music/soundtrack solutions so that you can test them and provide me with feedback on the ones you prefer and would like me to enhance in future updates!

Thanks again for your support! 

sincerely,

Marma

CONTACT: marma.developer@gmail.com
WEBSITE: http://marmadeveloper.wix.com/marmamusic

