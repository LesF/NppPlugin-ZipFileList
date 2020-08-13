using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;

namespace Kbg.NppPluginNET
{
	class Main
	{
		internal const string PluginName = "ZipFileList";
		private const string ZipExe = "7zG.exe";

		static string PathTo7Zip;
		static string iniFilePath = null;
		static string config7ZipPath = "";
		static frmMyDlg frmMyDlg = null;
		// static int idMyDlg = -1;
		// static Bitmap tbBmp = Properties.Resources.star;
		// static Bitmap tbBmp_tbTab = Properties.Resources.star_bmp;
		// static Icon tbIcon = null;

		public static void OnNotification(ScNotification notification)
		{  
			// This method is invoked whenever something is happening in notepad++
			// use eg. as
			// if (notification.Header.Code == (uint)NppMsg.NPPN_xxx)
			// { ... }
			// or
			//
			// if (notification.Header.Code == (uint)SciMsg.SCNxxx)
			// { ... }
		}

		internal static void CommandMenuInit()
		{
			StringBuilder sbIniFilePath = new StringBuilder(Win32.MAX_PATH);
			Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbIniFilePath);
			iniFilePath = sbIniFilePath.ToString();
			if (!Directory.Exists(iniFilePath)) Directory.CreateDirectory(iniFilePath);
			iniFilePath = Path.Combine(iniFilePath, PluginName + ".ini");

			StringBuilder savedPathname = new StringBuilder("", 255);
			int i = Win32.GetPrivateProfileString("Dependencies", "7ZipExeDirectory", "", savedPathname, 255, iniFilePath);
			config7ZipPath = savedPathname.ToString();

			PluginBase.SetCommand(0, "Create archive", myMenuFunction, new ShortcutKey(false, false, false, Keys.None));
		}

		internal static void SetToolBarIcon()
		{
			/*
			toolbarIcons tbIcons = new toolbarIcons();
			tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
			IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
			Marshal.StructureToPtr(tbIcons, pTbIcons, false);
			Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_ADDTOOLBARICON, PluginBase._funcItems.Items[idMyDlg]._cmdID, pTbIcons);
			Marshal.FreeHGlobal(pTbIcons);
			*/
		}

		internal static void PluginCleanUp()
		{
			if (!string.IsNullOrEmpty(PathTo7Zip))
				Win32.WritePrivateProfileString("Dependencies", "7ZipExeDirectory", PathTo7Zip, iniFilePath);
		}

		internal static void myMenuFunction()
		{
			PathTo7Zip = Find7zPath();
			INotepadPPGateway notepad = new NotepadPPGateway();
			string currentFile = notepad.GetCurrentFilePath();
			if (string.IsNullOrEmpty(currentFile) || !File.Exists(currentFile))
			{
				MessageBox.Show("Please save the file list first");
				return;
			}
			if (frmMyDlg == null)
				frmMyDlg = new frmMyDlg();
			frmMyDlg.SourceFilePath = currentFile;
			if (frmMyDlg.ShowDialog() == DialogResult.OK)
				CreateArchive(currentFile, frmMyDlg.ArchiveFilename, frmMyDlg.StartDirectory);
		}

		/// <summary>
		/// Create the archive file using the user's parameters
		/// </summary>
		/// <returns>True if there were no indications of failure</returns>
		private static void CreateArchive(string FileListName, string ArchiveName, string StartDirectory)
		{
			FileInfo fileInfo = new FileInfo(FileListName);
			if (fileInfo.Exists)
			{
				string targetFile =  ArchiveName;
				Process zipProcess = new Process();
				try
				{
					if (Directory.Exists(StartDirectory))
						Directory.SetCurrentDirectory(StartDirectory);

					zipProcess.StartInfo.UseShellExecute = false;
					zipProcess.StartInfo.FileName = PathTo7Zip;
					zipProcess.StartInfo.Arguments = $"a {targetFile} @{FileListName}";
					zipProcess.EnableRaisingEvents = true;
					zipProcess.Exited += ArchiveProcess_Exited;
					zipProcess.Start();
					zipProcess.WaitForExit(300000);

					/* TODO first check if we are creating a new archive or adding to an existing one
					 * then modify outcome message to suit - an add to an existing file may fail, and
					 * the file will still exist
					 */
					FileInfo zipFile = new FileInfo(targetFile);
					if (!zipFile.Exists)
					{
						MessageBox.Show(null, "Archive creation failed.", "Failed");
						return;
					}
					double arcSize = 0;
					if (zipFile.Length > 0)
						arcSize = (double)(zipFile.Length / 1000);
					MessageBox.Show(null, $"Archive completed\r\n\r\n{targetFile}\r\n\r\nSize: {arcSize:N} KB", "Completed");
				}
				catch (Exception exc)
				{
					MessageBox.Show(null, $"Archive creation failed\r\n{exc.Message}", "Failed");
				}
			}
			else
				MessageBox.Show(null, $"File list not found:\r\n{FileListName}\r\nDid you save the file first?","File not found");

			return;
		}

		private static void ArchiveProcess_Exited(object sender, System.EventArgs e)
		{
			if (sender is Process process)
			{
				int exitCode = process.ExitCode;
				/* TODO take action based on exit code?
				 * 0 No error 
				 * 1 Warning (Non fatal error(s)). For example, one or more files were locked by some other application, so they were not compressed. 
				 * 2 Fatal error 
				 * 7 Command line error 
				 * 8 Not enough memory for operation 
				 * 255 User stopped the process 
				 */
			}
		}

		/// <summary>
		/// Look for 7z.exe in this order:
		/// 1. TODO first check pre-saved config, if a pathname is present, verify that it can be used
		/// 2. Normal install locations, directory '7-Zip' in either ProgramFiles or ProgramFilesX86
		/// 3. Iterate thru environment PATH to see if 7z.exe is in some other location
		/// 4. Finally ask the user to look up the directory
		/// 5. TODO save the full pathname to config so we don't need to search for it again
		/// </summary>
		/// <returns>The full path to 7z.exe</returns>
		private static string Find7zPath()
		{
			if (!string.IsNullOrEmpty(config7ZipPath) && File.Exists(config7ZipPath))
					return config7ZipPath;

			string ZipExePath = "";

			// Common install location
			try
			{
				string[] programFiles = {
					Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles, Environment.SpecialFolderOption.None),
					Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86, Environment.SpecialFolderOption.None)
				};
				for (int pf = 0; pf < programFiles.Length; pf++)
				{
					ZipExePath = Path.Combine(programFiles[pf], "7-Zip");
					if (Directory.Exists(ZipExePath))
					{
						ZipExePath = Path.Combine(ZipExePath, ZipExe);
						if (File.Exists(ZipExePath))
							return ZipExePath;
					}
				}
			}
			catch (Exception){}

			// Elsewhere in PATH
			try
			{
				ZipExePath = FindExePath(ZipExe);
				if (ZipExePath.Length > 0)
					return ZipExePath;
			}
			catch (Exception){}

			/* TODO any other options for finding it?
			 * HKEY_LOCAL_MACHINE\SOFTWARE\7-Zip values 'Path' and 'Path64' both = "C:\Program Files\7-Zip\" on my setup
			 * HKEY_CLASSES_ROOT\7z_auto_file\shell\open\command 'Default' = full 7zFM.exe command line
			 */

			// Can't find it, ask for help
			if (ZipExePath.Length == 0)
			{
				FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog() { 
					SelectedPath = @"C:\",
					Description = "Directory that 7-Zip was installed into"
				};
				if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
				{
					ZipExePath = folderBrowserDialog1.SelectedPath;
					if (ZipExePath.Length > 0 && Directory.Exists(ZipExePath))
					{
						ZipExePath = Path.Combine(ZipExePath, ZipExe);
						if (!File.Exists(ZipExePath))
							ZipExePath = "";
						// TODO Save path to config so user doesn't get asked again next time
					}
				}
			}
			return ZipExePath;
		}

		/// <summary>
		/// Expands environment variables and, if unqualified, locates the exe in the working directory
		/// or the evironment's path.
		/// </summary>
		/// <param name="someExe">The name of the executable file</param>
		/// <returns>The fully-qualified path to the file</returns>
		/// <exception cref="System.IO.FileNotFoundException">Raised when the exe was not found</exception>
		private static string FindExePath(string someExe)
		{
			someExe = Environment.ExpandEnvironmentVariables(someExe);
			if (!File.Exists(someExe))
			{
				if (Path.GetDirectoryName(someExe) == String.Empty)
				{
					foreach (string test in (Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User) ?? "").Split(';'))
					{
						string path = test.Trim();
						if (!String.IsNullOrEmpty(path) && File.Exists(path = Path.Combine(path, someExe)))
							return Path.GetFullPath(path);
					}
				}
				throw new FileNotFoundException(new FileNotFoundException().Message, someExe);
			}
			return Path.GetFullPath(someExe);
		}
	}
}