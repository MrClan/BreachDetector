﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Foundation;

namespace Plugin.BreachDetector
{
    /// <summary>
    /// Interface for BreachDetector
    /// </summary>
    public class BreachDetectorImplementation : IBreachDetector
    {
        public bool? InDebugMode()
        {
            return Securing.IOSSecuritySuite.AmIDebugged() || System.Diagnostics.Debugger.IsAttached;
        }

        // https://gist.github.com/steipete/7668246
        public bool? InstalledFromStore()
        {
            try
            {
                // If file doesn't exist, the installation is from AppStore
                var file = NSData.FromFile(NSBundle.MainBundle.PathForResource("embedded", "mobileprovision"));
                return file == null;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Unable to get iOS installation source {ex.Message}");
                return false;
            }
        }

        public bool? IsRooted()
        {
            return Securing.IOSSecuritySuite.AmIJailbroken();
        }

        public bool? IsRunningOnVirtualDevice()
        {
            return Securing.IOSSecuritySuite.AmIRunInEmulator();
        }
    }
}
