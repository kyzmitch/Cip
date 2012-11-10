using System;
using System.Collections.Generic;
using System.Resources;
using System.Globalization;
using System.Reflection;
using System.Diagnostics;

namespace Cip.LabDemo.Resources
{
     public static class LabDemoResources
    {
         private static ResourceManager resourceManager;
         private static CultureInfo labCulture;
         private static Assembly ourAssembly;
         private const string ourNamespace = "Cip.LabDemo.Resources.";
         private const string baseName = "Cip.LabDemo.Strings";

         public static CultureInfo Culture
         {
             get
             {
                 return labCulture;
             }

             set
             {
                 System.Threading.Thread.CurrentThread.CurrentUICulture = value;
                 Initialize();
             }
         }
         public static ResourceManager Strings
         {
             get
             {
                 return resourceManager;
             }
         }
         private static void Initialize()
         {
             ourAssembly = Assembly.GetExecutingAssembly();
             resourceManager = CreateResourceManager();
             labCulture = CultureInfo.CurrentUICulture;
         }
         private static ResourceManager CreateResourceManager()
         {
             ResourceManager rm = new ResourceManager(ourNamespace + baseName, ourAssembly);
             
             return rm;
         }
         public static string GetString(string stringName)
         {
             string theString = resourceManager.GetString(stringName, labCulture);

             if (theString == null)
             {
                 Debug.WriteLine(stringName + " not found");
             }

             return theString;
         }
    }
}
