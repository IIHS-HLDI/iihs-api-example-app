![](http://www.iihs.org/frontend/images/logohq.svg) ![](https://developer.android.com/images/brand/Android_Robot_100.png)
---

# Overview

<p align="center">
	<a href="https://github.com/MrPickles2009/iihs-api-example-app" rel="nofollow">
		<img src="https://img.shields.io/badge/build-passing-brightgreen.svg" alt="Build Status">
	</a>  
  <a href="https://github.com/MrPickles2009/iihs-api-example-app/blob/github/License.md">
		<img src="https://img.shields.io/badge/license-Apache%202.0-yellowgreen.svg" alt="license">
	</a>
	<a href="https://github.com/MrPickles2009/iihs-api-example-app/releases">
		<img src="https://img.shields.io/badge/release-v1.1.0-blue.svg" alt="1.1.0">
	</a>
</p>

This repository contains code for a mobile android app that will display data from the IIHS Ratings API website at [https://api.iihs.org](https://api.iihs.org) in an intuitive and consumer friendly layout.  

# Installation

After cloning this repository, you'll need to do a few extra things to prepare your dev workstation:

- **Download the Android Device Emulator**
  - To run the app you will need either an android Device or an emulator. The official android device emulator for Visual Studio is located [here](https://www.visualstudio.com/vs/msft-android-emulator/).
- **Installing Xamarin**
  - You will also need to modify Visual Studio to include the '_Mobile development with .NET_' workload.
  - You can do this by running the '_Visual Studio Installer_' and selecting '_Modify_'. Scroll down to the '_Mobile & Gaming_' category and select '_Mobile development with .NET_'. Press '_Modify_' to install the workload.

# Running the app

- **This app needs an IIHS API key to use!**
  - This can be obtained by registering for an account [here](http://api.iihs.org/account/register)
  - Create an api-key.txt with your key being the only content in the file
  - In Visual Studio add this file to the root of the IIHSApiApp project.  In the properties of the api-key.txt file set it's content type to ```Embedded Resource```

To run the app on a/an:

- **Emulator**
  - Open '_Visual Studio Emulator for Android_' and launch the device you wish to deploy to.
  - In Visual Studio select the emulated device in the debug selection box and press '_Start Debugging_' (F5)
  - If this does not work you can _Right Click_ on the solution and select '_Deploy_'
- **Physical Device**
  - On the device go into '_Settings_' > '_About device_'
  - Unlock the _Developer options_ by tapping '_Build number_' 7 times.
  - Go to '_Settings_' > '_Developer options_' and select '_USB debugging_' to be '**On**'
  - In Visual Studio select the physical device in the debug selection box and press '_Start Debugging_' (F5)
  - If this does not work you can _Right Click_ on the solution and select '_Deploy_'
