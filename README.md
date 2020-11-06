# Introduction 
Framework for automating mobile applications using Appium.
Solution contains core framework and test projects and is built on
dotnet core.

# Getting Started
### Framework Dependencies
- Dotnet Core 3.1
- Appium 4.0
- DotNetSeleniumExtras.WaitHelpers 3.11
- NUnit 3.12
> These are the latest framework/tooling supported in dotnet core.
### Framework Test Dependencies
- Android device
- Appium Server
- Selenium Grid
- iTimekeep native app
- EA Pro app

# Build and Test
- dotnet build
- dotnet test 

# How-To
## 1. Connect Appium Server to a device via wifi.
*Useful when connecting multiple devices without usb cable*
- adb tcpip <portNumber>
- adb connect <ipaddressOfDevice>
> Sample Usage:
```
adb tcpip 5555
adb connect 192.168.1.46
```
![Image of tcp](Documentation\resources\tcp.png)
## 2. Get app id and activity
> Launch the app on the device and then fire up the following commands in powershell
- adb shell
- dumpsys window windows | grep -E 'mCurrentFocus'
> Sample Usage:
``` 
PS C:\Source\MobileAutomation> adb shell
$ dumpsys window windows | grep -E 'mCurrentFocus'
mCurrentFocus=Window{c8c5313 u0 com.bellefield.itimekeep/md5bd47c877efb590789a7c1cc51d6525da.MainActivity}
```
![Image of iTimekeep](Documentation\resources\nativeApp.png)
## 3. Inspect native apps via appium inspector
> Start appium desktop (server) and open inspect tool.
> Provide the following desired capabilities and start the session.
```
{
  "deviceName": "android",
  "platformName": "android",
  "udid": "<deviceId>",
  "deviceId": "<deviceId>"
}

```
*udid and deviceId can be the same. Run **adb devices** to get the device Id.*
![Image of nativeInspect](Documentation\resources\nativeInspect.png)
## 4. Inspect Web / Hybrid app (Web Views)
> Connect the device. For web app, Navigate to url in chrome (on the device). For hybrid app launch and navigate to appriate page of the app. Now on the desktop open chrome and type in the following:
```
chrome://inspect/#devices
```

> This will list all the web page /web views open on the connected devices. Select appropriate option from the ones provided for your device and click Inspect.
![Image of webInspect](Documentation\resources\webInspect.png)

## 5. Parallel execution on same machine
Be aware of **systemPort** race conditions, eventhough selectFreePort is selected.
This must be specified specifically to avaid them stepping over each other's foot. 