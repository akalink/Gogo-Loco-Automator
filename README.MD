# Gogo Loco Automator

### How to use

 1. Import the package to your VRChat avatar unity project.
 2. If not already, import Gogo Loco to the same project
 3. Under the Window options, click "Tools" -> "Gogo Loco Automator"
 4. Select your avatar and click "Select Avatar in scene" or drag your avatar into the avatar slot.
 5. Click "Add Gogo Loco" with or without Write Defaults (personal preference) to auto add Gogo loco
   

### Update log
- 0.04
  - Added a button to auto assign eye bones for eye look. It uses what is assigned in the animator, and if that fails, looks through the hierarchy by name.
- 0.03  
  - Found an issue where Avatar Descriptor script got angry when added then edited immediately.
  - Changed behavior of the descriptor checking method to only Get the descriptor.
  - Added button for the end user to add an avatar descriptor.
- 0.02 Fixed issue where wrong controller was placed in Sitting and Tpose (controllers swapped) slots
- 0.01 Initial update