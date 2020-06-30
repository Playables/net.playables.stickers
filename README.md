# iMessage Stickers for Unity

This package adds a sticker app extension to an iOS app exported from Unity. The stickers have to be prepared in Xcode manually. They will be automatically added to the Xcode project each time you export it from Unity.

## Installation

- In the Unity Package Manager, click "+" -> "Add package from git URL"
- enter ```https://github.com/Playables/net.playables.stickers``` and confirm.

## Usage

- Create an iMessage sticker app extension in an existing or new Xcode project. 
- Add some stickers and icons (in Xcode).
- Copy Info.plist and Stickers.xcassets from your sticker app extension in Xcode to a new folder called "Stickers" next to your Assets folder in your Unity project.
- Build with Unity. The stickers are copied and added to your exported Xcode project during OnPostprocessBuild.

## Compatibility

- Tested with Unity 2019.4. 
- This package is not well tested, use at your own risk.

## Created by

[Mario von Rickenbach](https://mariov.ch/) / [Playables](https://playables.net/)
