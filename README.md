# Introduction 
Sample framework for automating mobile applications using Appium.
Solution contains core framework and test projects and is built on
dotnet core.

# Getting Started
### Framework Dependencies
- Dotnet Core 3.1
- Appium 4.0
- DotNetSeleniumExtras.WaitHelpers 3.11
- NUnit 3.12
> These are the latest framework/tooling supported in dotnet core.
### Framework Integration Test Dependencies
- Android device
- Appium Server
- Selenium Grid

# Build and Test Commands
- dotnet build
- dotnet test 
# Consuming the framework
- Checkout the Integration tests in the tests project. WebTests folder contains tests for mobile browsers. All setting provided in the "fixture setup" can be replaced with an equivalent run settings parameter value. 
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
## 2. Get app id and activity
> Launch the app on the device and then fire up the following commands in powershell
- adb shell
- dumpsys window windows | grep -E 'mCurrentFocus'
> Sample Usage:
``` 
PS C:\Source\MobileAutomation> adb shell
$ dumpsys window windows | grep -E 'mCurrentFocus'
mCurrentFocus=Window{<Application>/<blah-blah>.MainActivity}
```
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
## 4. Inspect Web / Hybrid app (Web Views)
> Connect the device. For web app, Navigate to url in chrome (on the device). For hybrid app launch and navigate to appriate page of the app. Now on the desktop open chrome and type in the following:
```
chrome://inspect/#devices
```

> This will list all the web page /web views open on the connected devices. Select appropriate option from the ones provided for your device and click Inspect.

## 5. Parallel execution on same machine
Be aware of **systemPort** race conditions, eventhough selectFreePort is selected.
This must be specified explicitly to avaid them stepping over each other's foot. 

# Important - Selenium Grid (Files provided) and Framework Dependency
Ensure that you add the DeviceGroup capability in your node config and test files. The capability at a minimum requires platformName(ios/android) and device type (phone/tablet) entries there. These are mandatory and help in running tests against specific devices on the grid and are closely tagged within the framework. Check out integration tests in the tests project for its correct usage.
