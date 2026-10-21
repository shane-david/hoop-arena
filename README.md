# Hoop ARena
## Overview
Hoop ARena is a markerless AR experience developed for GDD3210 AR/VR Programming at UCCS. It is an AR basketball game where the play scans their area to establish planes and then can instantiate a basketball hoop on any wall. The player then has a limited amount of time to score as many times as possible while the basketball hoop moves around in different ways after every score. 

## Demo Video
[Click here to watch video!](https://www.youtube.com/watch?v=YrvKOJhuTHY)

## Features
- AR Markerless plane instantiation
- AR Markerless object instantiation 
- Touch input for mobile devices 
- Physics for "shooting" a basketball
- Score system UI within the AR world itself 
- Different, randomly determined hoop movement types after each score. 
- Score multiplier 

## How to play
1. Slowly move your phone until planes appear for the walls and floor. 
2. Click a wall to instantiate the hoop.
3. Tap the screen to spawn a ball and swipe to shoot it. 

## Platform Requirements
**This .apk build for this project runs on physical android devices only** It can however be tested in a simulated environment in the Unity Editor. 

## Installation 
### Requirements
- An Android mobile device that supports ARCore
- Android 7.0+
- Google Play Services for Ar

### Steps
1. Download the APK on you Android device from the [Release page](https://github.com/shane-david/hoop-arena/releases/tag/v1.0.0)
2. Allow installs from you browser or file manager
3. Open the downloaded APK and tap Install
4. Launch the Game
5. Play

## Technical Overview
Hoop ARena is built on the AR Foundation Unity Template. It uses several systems from within this template to detect and place planes when scanned and detected, and to instantiate objects on those planes. 
Similarly, the game uses Unity's Enhanced Touch Input System to detect touch input from a mobile device. All systems are loosely coupled, instead communication through C# events so that very rarely do systems need to reference each other.

### Important Systems
**`MarkerlessPlacementHandler.cs`**

Uses AR Foundation's `ARPlaneManager` and `ARRaycastManager` for surface tracking. While planes are being scanned a `WallDetectionHandler.cs` script check if any of the planes are vertical and of a sufficient size for the basketball hoop and transitions into the placement phase. During the placement phase, when a touch is detected it uses the `raycastManager` to determine which plane the player touched. It then uses an `anchorManager` to attach an anchor to that plane and then instantiate the basketball hoop to that anchor so that is is parallel to the plane no matter what. 

**`TouchInputHandler.cs`**

Uses Unity's `InputSystem.EnhancedTouch` to see if if the player has touched the screen. When a touch is detected it determines the phase of the touch (Began, Moved, Ended) and broadcasts a C# Event for that according touch. Other systems, like the `MarkerlessPlacementHandler`, `BallFlickHandler`, and `GameCanvasHandler` subscribe to these events to handle their according tasks. 

**`EventHandler.cs`**

The `EventHandler` routes all game-wide events, such as scores, player shooting events, plane detection events, to their according systems. This allows for systems to be loosely coupled and simply react to events instead of referencing all relevant systems and constantly checking for whether or not these events are happening. 

### Technical Challenges
The biggest technical challenge was the markerless placement. Doing standard placement with known Unity3D rotation/position methods worked perfectly in the simulated environment in the editor, however, when testing on the actual mobile device, the basketball hoop was constantly oriented weirdly and randomly. This was because the simulator gave perfectly axis aligned walls while this was not possible in a real environment. The fix for this was to build everything off of wall-relative axes instead of global axes. Everything that edits the transform of the basketball hoop now calculates rotation or position based oof the wall's normal instead of world directions. 

## Known Issues
- Inconsistency in scanning walls for markerless placement. 
- Ball can disappear when static on hoop for too long instead of waiting for gravity to be resolved. 


