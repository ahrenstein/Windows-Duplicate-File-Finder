/* ----------------------------------------------------------------------
// <copyright file="frmMain.cs" company="Ahrenstein">
//     Copyright (c) 2015 Ahrenstein., All Rights Reserved.
//     Authors:
//          Matthew Ahrenstein 2015 @ahrenstein
// </copyright>
// ----------------------------------------------------------------------
//
// WindowsDuplicateFileFinder
// OPEN SOURCE PROJECT! Use is subject to license terms in LICENSE.txt
// DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDuplicateFileFinder.Common;

namespace WindowsDuplicateFileFinder
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            // Set the default scan path to somewhere useful
            txtPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }
        /// <summary>
        /// When clicked, this button quits the software by closing the main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        /// <summary>
        /// When clicked, this button opens a select folder dialog box to allow the user to select the folder to scan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPath_Click(object sender, EventArgs e)
        {
            // Create a Select Folder dialog box
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            // Set the starting point to be the user's home folder
            //fbd.RootFolder = Environment.SpecialFolder.UserProfile;

            // Ask for a path
            fbd.ShowDialog();
            // Set the selected path as the text box text
            txtPath.Text = fbd.SelectedPath.ToString();
        }

        /// <summary>
        /// When clicked, this button opens the About dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbout_Click(object sender, EventArgs e)
        {
            // Instantiate About Box
            frmAbout about = new frmAbout();
            // Call About Box as a dialog
            about.ShowDialog();
        }

        /// <summary>
        ///  When clicked, run the functions to scan for duplicates and save the list to the desktop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScan_Click(object sender, EventArgs e)
        {
            // Instantiate the common library class
            CoreFunctions core = new CoreFunctions();
            // Display a dialog box displaying where the output of the scan will be saved
            string strFileToSaveResultsTo = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\DupeScan.txt";
            MessageBox.Show("Files will be saved to " + strFileToSaveResultsTo + "\n\nThis may take a while. Another message will appear when the scan is over.", "Starting Scan...", MessageBoxButtons.OK);

            // Perform the search using the Windows Duplicate File Finder Common Library
            core.FindFileDuplicates(txtPath.Text, strFileToSaveResultsTo);

            // Show a dialog box to alert the user that the scan is over.
            MessageBox.Show("The scan has now stopped. If no errors were shown, then all duplicates should be found.", "Scan Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
