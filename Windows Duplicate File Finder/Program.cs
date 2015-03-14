/* ----------------------------------------------------------------------
// <copyright file="Program.cs" company="Ahrenstein">
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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsDuplicateFileFinder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
