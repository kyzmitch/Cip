using System;
using System.Collections.Generic;
using System.Text;

using Cip;
using Cip.Collections;

namespace Cip.Cmd
{
    /// <summary>
    /// Colour image processing 
    /// [command line demo application].
    /// </summary>
    class CipCommandLine
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Colour Image Processing [CommandLine Edition]\n\n");
            // The Length property is used to obtain the length of the array. 
            // Notice that Length is a read-only property:
            Console.WriteLine("Number of command line parameters = {0}",
               args.Length);
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("Arg[{0}] = [{1}]", i, args[i]);
            }

            int n_proc_count = System.Environment.ProcessorCount;
            System.OperatingSystem os_version = System.Environment.OSVersion;
            System.Version clr_version = System.Environment.Version;

            Console.ReadLine();
        }
    }
}
