# Gogo Loco Automator

### How to use Gogo Loco Automator to your project
 1. Import the package to your VRChat avatar unity project.
 2. If not already, import Gogo Loco to the same project

### How to add Gogo Loco easily
 1. Under the Window options, click "Tools" -> "Gogo Loco Automator"
 2. Select your avatar and click "Select Avatar in scene" or drag your avatar into the avatar slot.
 3. Click "Add Gogo Loco" with or without Write Defaults (personal preference) to auto add Gogo loco
   
### How to use Gogo Loco Automator to add Simulated Eye Tracking to your avatar
 1. Under the Window options, click "Tools" -> "Gogo Loco Automator"
 2. Select your avatar and click "Select Avatar in scene" or drag your avatar into the avatar slot.
 3. Click "Assign Eyebones".

### How to use Gogo Loco Automator to add Automatic blinking to your avatar
 1. Under the Window options, click "Tools" -> "Gogo Loco Automator"
 2. Select your avatar and click "Select Avatar in scene" or drag your avatar into the avatar slot.
 3. Click "Assign Blink Blendshape". You may get a popup dialog telling you to click the button again, click the button again.


### Update log
- 0.05 
  - Added a button to auto assign the blink blend shape. There is unusual behavior where the first attempt cannot assign the index. The second attempt works fine.
- 0.04
  - Added a button to auto assign eye bones for eye look. It uses what is assigned in the animator, and if that fails, looks through the hierarchy by name.
- 0.03  
  - Found an issue where Avatar Descriptor script got angry when added then edited immediately.
  - Changed behavior of the descriptor checking method to only Get the descriptor.
  - Added button for the end user to add an avatar descriptor.
- 0.02 Fixed issue where wrong controller was placed in Sitting and Tpose (controllers swapped) slots
- 0.01 Initial update