
<!-- Team Name：194-F24-Planters

Description：
Our project educates users about the crucial process of restoring a forest by putting them in a game that simulates the process.
We decided to model our forest’s issues after the issues faced by the Amazon Rainforest
The game contains two levels, both of which contain a progress bar that determines when the level had been completed (when its full).
Both levels contain interactive components where users will be able to complete them through working together.

The first key script is the fieldAct script : 
it changes the ph value of the field and the state of the biochar through the trigger event of the land.

The key script for the second level is SnailController.cs. 
It is bound to the snail. Controls the behaviour of the snail in level 2



leftcontoler:Range Detection: Physics.OverlapSphere scans all Collide) within the player's detectionRange.
Object activation/deactivation: If there is a BiocharSpace, DetectorSpace, NetBagSpace, or CakeSpace in the player's detection range, call the pickoff() method to turn off objects other than the target objects
1.PHdetector:The player with the Detector enters the filed probe, which displays a textPHboard with a specific ph value.
2.Biochar：
The player holding the Biochar enters the filed probe range, the value on the PHboard is incremented by one, and the value on the biochar is decremented by one.
3.Cake: if the snail detects a Cake, it moves towards the Cake and tries to eat it.
4.Net: if the snail detects NetBag, it escapes in the opposite direction and triggers the capture logic.



--> -->
