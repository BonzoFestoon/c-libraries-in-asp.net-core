using System;
using System.Runtime.InteropServices;
namespace ImageConvertService
{
    public static class LTInterop
    {
        // TODO: Update the following with the path to the LEADTOOLS CDLLVC10 DLLs.
        //private const string WinX64BinPath = @"LEADTOOLS_BIN_CDLLVC10_x64_PATH_HERE";
        //private const string WinX86BinPath = @"LEADTOOLS_BIN_CDLLVC10_Win32_PATH_HERE";
        private const string WinX64BinPath = @"C:\LEADTOOLS 19\Bin\CDLLVC10\x64";
        private const string WinX86BinPath = @"C:\LEADTOOLS 19\Bin\CDLLVC10\Win32";
        static LTInterop()
        {
            var architecture = RuntimeInformation.OSArchitecture;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var oldPath = System.Environment.GetEnvironmentVariable("Path");
                if (architecture == Architecture.X86)
                {
                    Environment.SetEnvironmentVariable("Path", WinX86BinPath + "; " + oldPath);
                    Winx86.Apply();
                }
                else if (architecture == Architecture.X64)
                {
                    Environment.SetEnvironmentVariable("Path", WinX64BinPath + "; " + oldPath);
                    Winx64.Apply();
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Linux.Apply();
            }
        }
        // Need to call one of the SetLicense functions only once.
        public delegate int _L_SetLicenseFile(string licenseFile, string developerKey);
        public static _L_SetLicenseFile L_SetLicenseFile;
        public delegate int _L_SetLicenseBuffer(byte[] pLicenseBuffer, IntPtr nSize, string pszDeveloperKey);
        public static _L_SetLicenseBuffer L_SetLicenseBuffer;
        public delegate int _L_FileConvert(string sourceFile, string destFile, int format, int width, int height, int bitsPerPixel, int qfactor, IntPtr loadOptions, IntPtr saveOptions, IntPtr fileInfo);
        public static _L_FileConvert L_FileConvert;
        private static class Winx86
        {
            private const string _ltkrn = "ltkrnu.dll";
            private const string _ltfil = "ltfilu.dll";
            private const CharSet _charset = CharSet.Unicode;
            [DllImport(_ltkrn, CharSet = _charset)]
            public static extern int L_SetLicenseFile(string licenseFile, string developerKey);
            [DllImport(_ltkrn, CharSet = _charset)]
            public static extern int L_SetLicenseBuffer(byte[] pLicenseBuffer, IntPtr nSize, string pszDeveloperKey);
            [DllImport(_ltfil, CharSet = _charset)]
            public static extern int L_FileConvert(string sourceFile, string destFile, int format, int width, int height, int bitsPerPixel, int qfactor, IntPtr loadOptions, IntPtr saveOptions, IntPtr fileInfo);

            public static void Apply()
            {
                LTInterop.L_SetLicenseFile = L_SetLicenseFile;
                LTInterop.L_SetLicenseBuffer = L_SetLicenseBuffer;
                LTInterop.L_FileConvert = L_FileConvert;
            }
        }
        private static class Winx64
        {
            private const string _ltkrn = @"ltkrnx.dll";
            private const string _ltfil = @"ltfilx.dll";
            private const CharSet _charset = CharSet.Unicode;
            [DllImport(_ltkrn, CharSet = _charset)]
            public static extern int L_SetLicenseFile(string licenseFile, string developerKey);
            [DllImport(_ltkrn, CharSet = _charset)]
            public static extern int L_SetLicenseBuffer(byte[] pLicenseBuffer, IntPtr nSize, string pszDeveloperKey);
            [DllImport(_ltfil, CharSet = _charset)]
            public static extern int L_FileConvert(string sourceFile, string destFile, int format, int width, int height, int bitsPerPixel, int qfactor, IntPtr loadOptions, IntPtr saveOptions, IntPtr fileInfo);

            public static void Apply()
            {
                LTInterop.L_SetLicenseFile = L_SetLicenseFile;
                LTInterop.L_SetLicenseBuffer = L_SetLicenseBuffer;
                LTInterop.L_FileConvert = L_FileConvert;
            }
        }
        private static class Linux
        {
            private const string _ltkrn = "libltkrn.so";
            private const string _ltfil = "libltfil.so";
            private const CharSet _charset = CharSet.Ansi;
            [DllImport(_ltkrn, CharSet = _charset)]
            public static extern int L_SetLicenseFileA(string licenseFile, string developerKey);
            [DllImport(_ltkrn, CharSet = _charset)]
            public static extern int L_SetLicenseBufferA(byte[] pLicenseBuffer, IntPtr nSize, string pszDeveloperKey);
            [DllImport(_ltfil, CharSet = _charset)]
            public static extern int L_FileConvertA(string sourceFile, string destFile, int format, int width, int height, int bitsPerPixel, int qfactor, IntPtr loadOptions, IntPtr saveOptions, IntPtr fileInfo);

            public static void Apply()
            {
                LTInterop.L_SetLicenseFile = L_SetLicenseFileA;
                LTInterop.L_SetLicenseBuffer = L_SetLicenseBufferA;
                LTInterop.L_FileConvert = L_FileConvertA;
            }
        }
    }
}