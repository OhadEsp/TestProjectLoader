using System;
using Microsoft.Win32;

public class VisualStudioPathFinder
{
    public enum VisualStudioVersion { Vs2015 = 140, Vs2013 = 120, Vs2012 = 110, Vs2010 = 100, Vs2008 = 90, Vs2005 = 80, VsNet2003 = 71, VsNet2002 = 70 };

    public static string GetVisualStudioInstallationDir(VisualStudioVersion version)
    {
        string registryKeyString = String.Format(@"SOFTWARE{0}Microsoft\VisualStudio\{1}", Environment.Is64BitOperatingSystem ? @"\Wow6432Node\" : "", GetVersionNumber(version));

        using (RegistryKey localMachineKey = Registry.LocalMachine.OpenSubKey(registryKeyString))
        {
            if (localMachineKey != null) return localMachineKey.GetValue("InstallDir") as string;
            else return "";
        }
    }

    private static string GetVersionNumber(VisualStudioVersion version)
    {
        return string.Format("{0}.0", Convert.ToInt32(version) / 10);
    }
}
