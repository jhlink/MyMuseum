Name: James Kim
Google VR SDK version: 1.6.0
Unity version: 2017.1.0f3 
Platform: Android

Submission Notes:

- Video Players within the scene. Occasionally, when viewing Video Players consecutively, a frame from the previous video player may show up. This is a result of a bug with the RenderTexture associated with the video player/quad game object. After some research, a fix is currently under the works, but as of this version, there isn't a hard and fast fix. 

- The problem is resolved partially by explicitly releasing the render texture when any of the following conditions are satisfied: 

1. The video player has finished playing through an entire clip. 
2. The video player has been restarted by the user. 
3. The video player is interrupted by either playing another video player or audio player. 

However, this is not a perfect solution as Unity throws an error saying that the render texture is being released when the it is active. I have tried a combination of disabling the player, the parent quad game object, and calling the "DiscardContents" method; however, this never completely resolves the issue. Either a frame from the previous video player is briefly shown, an error is thrown, or no video is shown. 
