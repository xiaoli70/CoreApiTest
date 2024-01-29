
    using System.Diagnostics;
    using System.Runtime.InteropServices;

public class StartExe 
{
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern IntPtr GetModuleFileName(IntPtr hModule, out IntPtr lpFilename, int nCmdBuf);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern IntPtr GetModuleHandle(string lpModuleName);

    public void StartExe1(string exePath)
    {
        Process.Start(new ProcessStartInfo(exePath) { UseShellExecute = true });
    }
}