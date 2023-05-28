# NGUI-Memory-Game

The AR Memory Game is a fun and immersive augmented reality application built with Unity and Vuforia. This game aims to enhance memory skills in a delightful manner, using QR codes to discover 3D models of animals and related educational content.

## Prerequisites

-   Unity 2019.4 or later
-   Vuforia Engine 9.8
-   Android SDK (for Android) or Xcode (for iOS)

## Getting Started

1.  **Clone** the repository.

2.  **Open the project in Unity**: Open Unity Hub, and click on the 'Add' button. Navigate to the cloned repository and select the project.

3.  **Import Vuforia Engine**: Go 'Assets' > 'Import Package' > 'Custom Package' and select the Vuforia package that you downloaded.

4.  **Set up Vuforia**: Follow the instructions [here](https://library.vuforia.com/articles/Training/getting-started-with-vuforia-in-unity.html) in order to setup Vuforia in your Unity project.

5.  **Setup your build platform**: Go 'File' > 'Build Settings' and select your desired platform (iOS or Android) but we highly recommended Android. Warning it didnt test in IOS it may produce unexpected error.

6.  **Run the game**: Hit the 'Play' button in Unity. You can also build and run the game on your mobile device.
   
7.  **Simply run project**: You will able too see *build.apk* in the project root level.Just setup the apk and scan [animals pdf](qrcode_animals.pdf).
   

## Game Instructions


1.  Open the AR memory game on your phone.
2.  Scan the QR codes to see 3D models of animals.
3.  If the discovered animal matches the previously scanned animal, you will hear a success sound, and a related educational video will be played.
4.  If the animals don't match, you'll hear a failure sound, and you can try again.
5.  Keep track of your score displayed on the screen.

## Built With


-   [Unity](https://unity.com/)
-   [Vuforia](https://developer.vuforia.com/)
-   C# language
-   QR Code technology
-   TextMeshPro for Unity
-   UnityEngine.Video

## Contributing


Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.