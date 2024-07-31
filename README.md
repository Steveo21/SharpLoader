# SharpLoader
A simple C# shellcode loader that takes a .bin file as input. Originally designed for testing functionality of shellcode when performing patching.

![image](https://github.com/user-attachments/assets/cca0d115-fa87-46fd-be1d-cb606dcb5c78)

This project was inspired by Hasherezade's runshc project located at: https://github.com/hasherezade/pe_to_shellcode/tree/master/runshc

Installation:
cd /path/to/install_dir
git clone --recursive https://github.com/Steveo21/SharpLoader.git

Usage: SharpLoader.exe .\calc.bin

Compilation with Command Prompt for Visual Studio: 
csc /out:SharpLoader.exe /debug:full source.cs
