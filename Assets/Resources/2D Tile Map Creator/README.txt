2D Tile Map Creator v.1 For Unity 5.
Created By - Paul Britton 31/05/15/ paul.l.b@me.com

Introduction:

Firstly, thanks for downloading. Hopefully this wont be too much to take in.

To open the window, click Window/2D Tile Map Creator.
You can dock the window or leave it float.

There is a certain folder structer in order for the editor to run. If any folders or files are missing you will be prompted.

Requirements:
GameObjects are stored in 3 folders/layers. Lower, Middle, Above. You will need 3 layers available in the layer manager for the editor to automatically create them and add them to your gameobjects. In your folders you will generally have floors and walls in the lower folder, objects and NPC's in the middle folder and anything you want to place on top of objects in the above folder.

GameObjects require 4 Children Objects:

Editor Sprite, - Sprite MUST be even on all sides and MUST be imported and set to ADVANCED with Read/Write Endabled. Works well with 16 x 16 sprites. Anything larger will looked slightly stretched. Error message will say 'NPOT width * height'. This sprite is for the editor window only. This object remains inactive so not to show their sprites rather than the GameSprites.

GameEvents,  - Parent for any other objects  you wish to add.

GameSprites, - Game sprites will be used as the Tile preview and will be what you see in game. For tiles bigger than one sqaure (16 x 16) it is better to make a bunch 16 x 16 images and place them togther rather than use one whole sprite as you need to take into consideration what image layers your player can walk under.

EditorHitbox, - This should be wrapped to the size you want to be able to click the tile on the scene. Again a 16 x 16 sprite is easy. Refer to Tree sprites and Bookshelf sprites for examples. This object will turn inactive upon gameplay, so not to interupt your game.

***These Objects will not be created for you. All must be ready as a prefab before you open the editor window. You can edit your prefabs without having to close and reopen the window. Just tab to another list and back for it to refresh. If you add tiles to your folder while the editor window is open you will not see the results until you reopen.

**The Layers in the layer manager and tabs in the window, do not represent sprite layers. All editor sprite layers can be set to 0. Your game sprite layers will also need to be manually set to whatever standard your game is running at.

Controls:

Left Click single = paint one tile.
Left Click Hold = paint tile constantly. Can be fiddly if your EditorHitbox is not setup right.
Right M.Click = Delete single layer starting with the highest.

Hold Z = Delete all layers on tiles.
Left Control = Draw in straight lines.

Ctrl+1 = Select Parent of object can be handy if your map is not made as a prefab yet. Unity defaulty selects most child of object you click on scene.
ctrl+2 = Add a Group Object that can be tagged to make editing easier.

Ctrl+M = Open/Close Editor Window.

=========================================================
Any bugs you find please let me know. If you need help you feel free to contact me. And if you maybe make a game with this, credit me :)

Enjoy.

