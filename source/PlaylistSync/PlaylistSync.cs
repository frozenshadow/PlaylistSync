using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace PlaylistSync
{
    /// <summary>
    /// PlaylistSync main window form
    /// </summary>
    public partial class PlaylistSync : Form
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the PlaylistSync object.
        /// </summary>
        public PlaylistSync()
        {
            InitializeComponent();
        }

        #endregion

        #region PROPERTIES AND FIELDS

        /// <summary>
        /// Get or set the playlist file path
        /// </summary>
        private string _playlistFile;

        /// <summary>
        /// Get or set the destination folder path
        /// </summary>
        private string _destFolder;

        /// <summary>
        /// Get or set the list with files
        /// in the destination folder
        /// </summary>
        private List<string> _destFileList = new List<string>();

        /// <summary>
        /// Get or set the source folder to synchronize.
        /// This is currently relative to the playlist location
        /// </summary>
        private string SourcePath
        {
            get { return Path.GetDirectoryName(_playlistFile); }
            set {
                if (!File.Exists(value))
                    throw new FileNotFoundException();
            }
        }

        #endregion

        #region EVENTS

        /// <summary>
        /// When the browse button for the playlist location is clicked,
        /// show the browse dialogbox and add the playlist content to the listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaylistLoc_btn_Click(object sender, EventArgs e)
        {
            // IF OK in dialogbox is clicked
            if (SelectDialog(false, out _playlistFile))
            {
                // Put the playlist file location in a TextBox
                playlistLoc.Text = _playlistFile;

                // Add the playlist content to the listview
                FillListView(_playlistFile);
            }
        }

        /// <summary>
        /// Fired when the playlist location textbox is manually edited
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaylistLoc_TextChanged(object sender, EventArgs e)
        {
            _playlistFile = playlistLoc.Text;

            // Check if the playlist file exists (never ever trust a user)
            if (!File.Exists(_playlistFile))
                throw new FileNotFoundException();

            // Add the playlist content to the listview
            FillListView(_playlistFile);
        }

        /// <summary>
        /// When the browse button for the destination folder is clicked,
        /// show the select folder dialogbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DestDir_btn_Click(object sender, EventArgs e)
        {
            // IF OK in dialogbox is clicked
            if (SelectDialog(true, out _destFolder))
            {
                // Put the destination folder path in a TextBox
                destDir.Text = _destFolder;
            }
        }

        /// <summary>
        /// Start the synchronization proccess when the 
        /// start sync button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartSync_btn_Click(object sender, EventArgs e)
        {
            List<string> playlistFileList = new List<string>();
            List<string> inPlaylistbutnotDest;
            List<string> inDestbutnotPlaylist;

            // Test if the destination folder textbox is filled in
            if (string.IsNullOrWhiteSpace(_destFolder))
            {
                MessageBox.Show("Please set a destination folder!", "No destination folder defined",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a list with files from the 
            // destination folder for later comparison
            ProcessDirectory(_destFolder);

            List<string> musicfolders = new List<string>();
            foreach (ListViewItem itemRow in listView1.Items)
            {
                // Add playlist files to the file list
                playlistFileList.Add(itemRow.Text);

                // Do some voodoo stuff by stripping off the music file
                // from it's subfolders and re-create the full source path
                string musicSubfolder = Path.GetDirectoryName(itemRow.Text);
                string fullSourcePath = Path.Combine(SourcePath, musicSubfolder);

                // Add additional files from the 'Include files' textbox
                // to the playlist file list
                if (!musicfolders.Contains(fullSourcePath))
                {
                    musicfolders.Add(fullSourcePath);
                    playlistFileList.AddRange(FindFiles(fullSourcePath, textBox1.Text, false));
                }
            }

            // Compare the playlist files with the destination folder
            CompareLists(playlistFileList, 
                _destFileList, 
                out inPlaylistbutnotDest, 
                out inDestbutnotPlaylist);

            // Copy all files from the playlist file list
            // that are not in the destination folder
            Copyfiles(inPlaylistbutnotDest);

            // When the deleteFromDest option is checked
            if (deleteFromDest.Checked)
            {
                // Delete all files which are not in the playlist file list
                DeleteFiles(inDestbutnotPlaylist);
            }

            // When the deleteEmptyDirectries option is checked
            if (deleteEmptyDirs.Checked)
            {
                // Recursively delete all empty directories
                DeleteEmptyDirectories(_destFolder);
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Create and show the file and folder select dialog
        /// </summary>
        /// <param name="folderPicker"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool SelectDialog(bool folderPicker, out string fileName)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();

            if (!folderPicker)
            {
                dialog.Filters.Add(new CommonFileDialogFilter("Textual playlist files", "*.txt, *.m3u, *.m3u8"));
            }
            dialog.IsFolderPicker = folderPicker;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                fileName = dialog.FileName;
                return true;
            }

            fileName = "";
            return false;
        }

        /// <summary>
        /// Reads the playlist file and adds each song
        /// to the listview
        /// </summary>
        /// <param name="playlistFile"></param>
        public void FillListView(string playlistFile)
        {
            // Clear/reset listview
            listView1.Columns.Clear();
            listView1.Clear();

            // To display the items in a detailview, first add a column
            listView1.Columns.Add("File", -2, HorizontalAlignment.Left);

            // Read the playlist file and append each song to the listview
            string line;
            StreamReader file = new StreamReader(playlistFile);
            while ((line = file.ReadLine()) != null)
            {
                listView1.Items.Add(line);
            }
            file.Close();
        }

        /// <summary>
        /// Gets all (sub)directories and files
        /// from the targetDirectory
        /// </summary>
        /// <param name="targetDirectory"></param>
        public void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        private void ProcessFile(string path)
        {
            // Add a relative path to the destination folder file list
            _destFileList.Add(Helpers.GetRelativePath(_destFolder, path));
        }

        /// <summary>
        /// Find files that are matching a pattern
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="patterns"></param>
        /// <param name="searchSubdirectories"></param>
        /// <returns></returns>
        private List<string> FindFiles(string dirName, string patterns, bool searchSubdirectories)
        {
            // Make the result list
            List<string> files = new List<string>();

            // Get the patterns
            string[] patternArray = Regex.Split(patterns, @";\s*");

            // Search
            SearchOption searchOption = SearchOption.TopDirectoryOnly;
            if (searchSubdirectories)
                searchOption = SearchOption.AllDirectories;
            foreach (string pattern in patternArray)
            {
                foreach (string filename in Directory.GetFiles(
                    dirName, pattern, searchOption))
                {
                    if (!files.Contains(filename)) files.Add(Helpers.GetRelativePath(SourcePath, filename));
                }
            }

            // Sort
            // TODO: is this necessary?
            files.Sort();

            // Return the result
            return files;
        }

        /// <summary>
        /// Compares two lists and returns the differences
        /// </summary>
        /// <param name="listA"></param>
        /// <param name="listB"></param>
        /// <param name="inAbutnotB"></param>
        /// <param name="inBbutnotA"></param>
        /// <returns></returns>
        private bool CompareLists(List<string> listA, List<string> listB, out List<string> inAbutnotB, out List<string> inBbutnotA)
        {
            inAbutnotB = listA.Except(listB).ToList(); // List with items that are in listA but not in ListB
            inBbutnotA = listB.Except(listA).ToList(); // List with items that are in listB but not in ListA

            // Return true when there are no differences
            return !inAbutnotB.Any() && !inBbutnotA.Any();
        }

        /// <summary>
        /// Copy files from list
        /// </summary>
        /// <param name="files"></param>
        /// TODO: copied from other project so cleanup is needed
        private void Copyfiles(List<string> files)
        {
            try
            {
                // Loop through the list and copy all files
                foreach (String file in files.ToArray()) //alleen verschillen Wel in playlist niet in dest
                {
                    try
                    {
                        // Variables
                        String[] filepath;
                        String tempDirPath = "";

                        string sourceFilepath = Path.Combine(SourcePath, file);
                        string filename = file.Replace(SourcePath + "\\", "");
                        string destFilepath = Path.Combine(destDir.Text, filename);

                        Debug.WriteLine("sourceFilepath: " + sourceFilepath); // Debug
                        //Debug.WriteLine("filename: " + filename); // Debug
                        Debug.WriteLine("destFilepath: " + destFilepath); // Debug


                        // Save the directory path and check if the path exists on the destination
                        filepath = destFilepath.Split('\\');
                        for (int i = 0; i < filepath.Length - 1; i++)
                        {
                            tempDirPath += filepath[i] + "\\";
                        }
                        if (!Directory.Exists(tempDirPath))
                        {
                            Directory.CreateDirectory(tempDirPath);
                        }

                        // Check if the file already exists
                        if (File.Exists(destFilepath) && File.Exists(sourceFilepath))
                        {
                            // Create FileInfo objects for both files
                            FileInfo fileInfo1 = new FileInfo(destFilepath);
                            FileInfo fileInfo2 = new FileInfo(sourceFilepath);

                            // Compare file sizes
                            if (fileInfo1.Length != fileInfo2.Length)
                            {
                                DialogResult overwrite = MessageBox.Show(
                                    "De volgende bestanden zijn niet gelijk in grootte." + "\n \n" +
                                    "Wilt u \"" + fileInfo1 + "\" vervangen door \"" + fileInfo2 + "\"?",
                                    "Bestand bestaat al", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                                );
                                if (overwrite == DialogResult.Yes) // If the user clicks Yes, exit the application
                                {
                                    //log(filename + " bestanden zijn niet gelijk!"); // File sizes are not equal therefore files are not identical
                                    File.Copy(sourceFilepath, destFilepath, true); // If the file does NOT exist already, execute copy operation
                                    //log("Kopieer " + "\"" + sourceFilepath + "\"" + " naar " + "\"" + destinationDir + "\""); // Debug
                                    files.Remove(filename); // Remove the file from the files list
                                }
                                else
                                {
                                    // Throw error at log
                                    Debug.WriteLine(fileInfo1 + " bestaat al");
                                }
                            }
                        }
                        else
                        {
                            //log("stap 8: " + filename + ", " + destFilepath); // Debug
                            File.Copy(sourceFilepath, destFilepath, false); // If the file does NOT exist already, execute copy operation
                            //Debug.WriteLine("Kopieer " + "\"" + sourceFilepath + "\"" + " naar " + "\"" + destinationDir + "\""); // Log the copied file
                            files.Remove(filename); // Remove the file from the files list
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message); // Catch the error and log
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message); // Catch the error and log
            }
        }

        /// <summary>
        /// Delete files from list
        /// </summary>
        /// <param name="files"></param>
        private void DeleteFiles(List<string> files)
        {
            foreach (string f in files)
            {
                string file = Path.Combine(_destFolder, f);

                // Check if the file still exists
                if (File.Exists(file))
                {
                    // Use a try block to catch IOExceptions, to
                    // handle the case of the file already being
                    // opened by another process.
                    try
                    {
                        File.Delete(file);
                    }
                    catch (IOException e)
                    {
                        Debug.WriteLine(e.Message);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Recursively delete all directories
        /// which are empty
        /// </summary>
        /// <param name="rootDir"></param>
        private static void DeleteEmptyDirectories(string rootDir)
        {
            foreach (var dir in Directory.GetDirectories(rootDir))
            {
                DeleteEmptyDirectories(dir);
                if (Directory.GetFiles(dir).Length == 0 &&
                    Directory.GetDirectories(dir).Length == 0)
                {
                    Directory.Delete(dir, false);
                }
            }
        }

        #endregion

    }
}
