using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Kbg.NppPluginNET
{
	/// <summary>
	/// <list type="table">
	/// <listheader>
	///		Assumptions
	///	</listheader>
	///		<item>
	///			The current file in the editor has been saved
	///			TODO check if the file has been changed and not saved - maybe before this dialog is shown
	///		</item>
	///		<item>
	///			The SourceFilePath property was set before this dialog was shown, so we can use it's value in SetDefaults
	///		</item>
	///		<item>
	///			The current file in NP++ is a list of files and/or directories to be zipped into an archive
	///		</item>
	///		<item>
	///			The file list may contain absolute paths or partial paths relative to a specific directory,
	///			assume they are relative to the location in which the file list exists, allow user to change
	///			the location that the zip process runs from
	///			TODO ** make sure that setting current directory before executing 7z.exe makes it use that base
	///			location when using relative paths
	///			TODO ** make sure the full path to the file list is provided in the command-line args if
	///			starting the process in a different directory
	///		</item>
	///		<item>
	///			The archive type will be determined by the given file extension, the default archive name will have .zip
	///			but the user may change it to anything.  Appropriate display of error output from the 7z.exe process
	///			will be the user's only feedback of invalid values, hopefully it will tell them what they did wrong.
	///		</item>
	///		<item>
	///			User is responsible for selecting a new file name, or an existing file if they want to append to it.
	///		</item>
	/// </list>
	/// <para>
	///		Could also include a textbox to accept additional command-line arguments, but this could add more
	///		complexity than the quick action this feature is intended to provide.
	///		Could include checkbox option to test the archive after creation.
	/// </para>
	/// </summary>
	public partial class frmMyDlg : Form
	{
		public string SourceFilePath { get; set; }
		public int CompressionLevel { get; set; }
		public string StartDirectory { get; set; }
		public string ArchiveFilename { get; set; }

		public frmMyDlg()
		{
			InitializeComponent();
		}

		private void frmMyDlg_Shown(object sender, EventArgs e)
		{
			SetDefaults();
		}

		private void buttonArchive_Click(object sender, EventArgs e)
		{
			StartDirectory = textBoxBaseFrom.Text;
			ArchiveFilename = textBoxArchiveName.Text;
			CompressionLevel = (int)numericCompLevel.Value;
		}

		private void buttonBaseFrom_Click(object sender, EventArgs e)
		{
			folderBrowserDialog1.SelectedPath = textBoxBaseFrom.Text;
			folderBrowserDialog1.Description = "Directory to run archive from";
			if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
			{
				string startDir = folderBrowserDialog1.SelectedPath;
				if (startDir.Length > 0 && Directory.Exists(startDir))
					textBoxBaseFrom.Text = startDir;
			}
		}

		private void buttonArchiveName_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveZipDialog = new SaveFileDialog() {
				Filter = "Zip files (*.zip)|*.zip|7-Zip files (*.7z)|*.7z|Any file type|*.*",
				FilterIndex = 1,
				RestoreDirectory = true,
				CheckPathExists = true,
				Title = "Create or update archive"
			};
			/* Assumes textBoxArchiveName contains the suggested name created in SetDefaults,
			 * or any value the user has updated the field with since then.
			 */
			FileInfo inf = new FileInfo(textBoxArchiveName.Text);
			if (inf.Exists)
			{
				saveZipDialog.InitialDirectory = inf.DirectoryName;
				saveZipDialog.FileName = inf.FullName;
			}
			if (saveZipDialog.ShowDialog() == DialogResult.OK)
				textBoxArchiveName.Text = saveZipDialog.FileName;
		}

		private void SetDefaults()
		{
			buttonArchive.Enabled = false;
			FileInfo fi = null;
			if (!string.IsNullOrEmpty(SourceFilePath))
				fi = new FileInfo(SourceFilePath);
			if (fi != null && fi.Exists)
			{
				string baseDir = fi.DirectoryName;
				int idx = SourceFilePath.LastIndexOf(".");
				string archiveFilename = SourceFilePath.Substring(0, idx) + ".zip";
				textBoxBaseFrom.Text = baseDir;
				textBoxArchiveName.Text = archiveFilename;
				buttonArchive.Enabled = true;
			}
			else
			{
				MessageBox.Show(this, 
					"Save the file first", 
					"Source file list not found", 
					MessageBoxButtons.OK);
				buttonArchive.Enabled = false;
			}
		}
	}
}
