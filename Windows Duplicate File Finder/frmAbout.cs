/* ----------------------------------------------------------------------
// <copyright file="frmAbout.cs" company="Ahrenstein">
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
using System.Windows.Forms;
using System.Reflection; // used for grabbing Assembly info to make a nice About Box

namespace WindowsDuplicateFileFinder
{
    /// <summary>
    /// Implements the logic code for the about box.
    /// </summary>
    public partial class frmAbout : Form
    {
        /**
         * Methods
         */
        /// <summary>
        /// Initializes a new instance of the <see cref="frmAbout"/> class.
        /// </summary>
        public frmAbout()
        {
            string[] projectDescription = new string[]
            {
                "Windows Duplicate File Finder.",
                "This program will recursively scan the selected directory for duplicate files",
                "by checking and comparing the MD5 sums of all the found files.",
                "Any duplicates will be saved to a text file list on the user's desktop",
                "",
                "\t\t*** Windows Duplicate File Finder Credits ***",
                "Matthew Ahrenstein:",
                "\t- Common Library (Contains Duplicate File Finder Code)",
                "\t- User Interface",
                "",
                "Nevec Networks LLC:",
                "\t- Assembly Attribute Accessors (Used to grab build/version info automatically)",
            };

            InitializeComponent();
            this.lblProjectName.Text = AssemblyVersion._NAME;
            this.lblVersion.Text = "Version: " + AssemblyVersion._VERSION_STRING;
            this.lblCopyright.Text = AssemblyVersion._COPYRIGHT;
            this.txtDescription.Lines = projectDescription;
        }

        /// <summary>
        /// Event that occurs when the "Close" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
