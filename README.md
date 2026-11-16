# Minimal AR Museum - Project RV SID 2024-25

AR Museum is an Android augmented-reality museum about North-African heritage, developed as a Project for the Virtual Reality course (RV, Réalité Virtuelle) at ESI for the academic year 2024-25. It was created with Unity, C#, and Vuforia to make a museum visit possible anywhere:
when the phone camera recognises one of the project’s image targets, it anchors a
virtual museum experience to the real world.

The experience combines historical 3D artefacts with information panels, audio
narration, bilingual content (English and French), an optional day/night visual
mode, and an interactive quiz. The project includes artefacts such as an
astrolabe and a stele, as well as a mummy-themed target.

## Try the Android application

### Requirements

- An Android phone with a working camera.
- The APK, you can find it in this link (because I forgot to sign in Unity, and I am not re-installing it just for that):
  [`Minimal_AR_Museum-Project_RV_SID_2024-25.apk`](https://drive.google.com/file/d/10jMUPW9Lj88M0-jSNbrdx_QXE0yG61iq/view?usp=sharing)
- The target images in [`Images to scan using phone camera/`](./Images%20to%20scan%20using%20phone%20camera/).
- If you want to test out the project in Unity, download the [`Vuforia Enginge Package`](https://drive.google.com/file/d/12s5vnxn84GWRAn5AVJJDzxOCPL8Q1ASN/view?usp=sharing) to the [`Packages/`](./Packages/) folder.

### Installation

1. Download the APK to your Android phone.
2. Open the downloaded APK and allow installation from this source if Android
   asks for permission.
3. Install and launch **AR Museum**.
4. Allow the application to use the camera.

### Scan an image

The camera needs to see a target image while the application is running. The
easiest setup is to display the target images on another screen:

1. Open one of the JPG files in
   [`Images to scan using phone camera/`](./Images%20to%20scan%20using%20phone%20camera/)
   on a computer or another phone.
2. In AR Museum, point the phone camera at the image and keep it fully visible,
   flat, and well lit.
3. Move the phone slowly until the image is recognised. Keep the image in view
   while interacting with the content.
4. Tap the information buttons to show descriptions and use the audio controls
   to play or pause narration.

Available target images:

- [`astrolabe_scaled.jpg`](./Images%20to%20scan%20using%20phone%20camera/astrolabe_scaled.jpg)
- [`egyptian-mummy_scaled.jpg`](./Images%20to%20scan%20using%20phone%20camera/egyptian-mummy_scaled.jpg)
- [`quiz_scaled.jpg`](./Images%20to%20scan%20using%20phone%20camera/quiz_scaled.jpg)
- [`stele_scaled.jpg`](./Images%20to%20scan%20using%20phone%20camera/stele_scaled.jpg)

For best tracking results, use the original images at a reasonable size rather
than a blurry or heavily compressed copy. Avoid glare, shadows, and low light.

## Main features

- **Image-based AR:** Vuforia recognises the target images and places 3D museum
  content in the camera view.
- **Interactive artefacts:** Tap the in-scene controls to reveal descriptions.
- **Audio guide:** Play English or French narration for the available artefacts.
- **English/French interface:** Change the language from the settings menu.
- **Day/night mode:** Tap the astrolabe artefact to switch the scene’s
  lighting and atmosphere.
- **Quiz:** Scan `quiz_scaled.jpg` to open the question-and-answer experience,
  receive visual feedback, and reset the quiz when it is complete.

## Project structure

This repository contains the Unity project and a ready-to-install Android build.

- [`Assets/Scenes/`](./Assets/Scenes/) contains the Unity scenes.
- [`Assets/Scripts/`](./Assets/Scripts/) contains the C# interaction, audio,
  language, lighting, and quiz scripts.
- [`Assets/AudioClips/`](./Assets/AudioClips/) contains the English and French
  narration clips.
- [`Images to scan using phone camera/`](./Images%20to%20scan%20using%20phone%20camera/)
  contains the image targets used by the APK.
- [`Packages/manifest.json`](./Packages/manifest.json) records the Unity
  dependencies, including Vuforia.
- [`PRESENTATION_Minimal_AR_Museum-Project_RV_SID_2024-25.pdf`](./PRESENTATION_Minimal_AR_Museum-Project_RV_SID_2024-25.pdf)
  contains the project presentation, in French only.

## Development

The project was created with **Unity 2022.3.61f1**. Open the repository folder
as a Unity project, then use the included scenes and assets to inspect or
modify the experience. The APK is provided for users who only want to try the
application and do not need Unity.
