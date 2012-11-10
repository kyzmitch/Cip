using System;
using System.Text;
using System.Runtime.InteropServices;


namespace OSVersion
{
    [StructLayout(LayoutKind.Sequential)]
    public struct OSVersionInfo
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint dwOSVersionInfoSize;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwMajorVersion;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwMinorVersion;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwBuildNumber;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwPlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public String szCSDVersion;
    }
    public class LibWrap
    {
        [DllImport("kernel32", EntryPoint = "GetVersionEx")]
        public static extern bool GetVersionOS(ref OSVersionInfo osvi);
    }
}