using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;

using Interact = OpenQA.Selenium.Appium.Interactions;

namespace MobileAutomation.Framework.Core.Utilities {
    public static class TouchActionExtensions {
        /// <summary>
        /// Move to and bring element to focus on screen using touch pointer.
        /// </summary>
        /// <param name="driver">driver</param>
        /// <param name="element">element to focus on</param>
        /// <param name="x">horizontal offset from origin</param>
        /// <param name="y">vertical offset from origin</param>
        public static void MoveTo(this AppiumDriver<AppiumWebElement> driver, AppiumWebElement element, int x, int y) {
          
            //ToDo: Make the scroll and tap work.
            Interact.PointerInputDevice inputDevice = new Interact.PointerInputDevice(PointerKind.Touch);
            ActionSequence actionSequence = new ActionSequence(inputDevice);
            actionSequence.AddAction(inputDevice.CreatePointerMove(element, x, y, TimeSpan.FromSeconds(1)));
            try {
                driver.PerformActions(new List<ActionSequence> { actionSequence });

            } catch {
                TestContext.WriteLine($"Move to element: {element.Text} failed.");

                //ignore
            } finally {
                Wait.Seconds(0.5);
            }

        }
        
        /// <summary>
        /// Bring element in Focus
        /// </summary>
        /// <param name="driver">Driver</param>
        /// <param name="element">Element to bring to focus</param>
        public static void MoveTo(this AppiumDriver<AppiumWebElement> driver, AppiumWebElement element) {
            MoveTo(driver, element, 0, 0);
        }
    }
}
