/* ----------------------------------------------------------------------
// <copyright file="clsCoreFunctions.cs" company="Ahrenstein">
//     Copyright (c) 2015 Ahrenstein., All Rights Reserved.
//     Authors:
//          Matthew Ahrenstein 2015 @ahrenstein
// </copyright>
// ----------------------------------------------------------------------
//
// WindowsDuplicateFileFinder.Common
// OPEN SOURCE PROJECT! Use is subject to license terms in LICENSE.txt
// DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
*/
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography; //For MD5 sum generation

namespace WindowsDuplicateFileFinder.Common
{
    public class CoreFunctions
    {
        // This bool is set to true if GetFileList succeeds in the Try/Catch
        bool FileListGathered = false;

        /// <summary>
        /// Scan a path recursively for file duplicates via MD5 sum and save the duplicates to a file
        /// </summary>
        /// <param name="pathToScan">Directory to start recursive scan from</param>
        /// <param name="pathToSave">File to save results to</param>
        public void FindFileDuplicates(string pathToScan, string pathToSave)
        {
            // Scan for a list of files and save to a string array
            string[] filesList = GetFileList(pathToScan);

            // If the GetFileList succeeds then continue with the rest of the application functions
            if (FileListGathered == true)
            {
                // Instantiate a string List object for storing MD5 sums
                List<string> md5sums = new List<string>();

                // Loop through the file list and add MD5 sums to the string List object md5sums
                foreach (string file in filesList)
                {
                    md5sums.Add(GetMD5Sum(file));
                }

                // Instantiate a string List object and store the duplicate search results in it
                List<string> duplicates = DuplicateSearch(md5sums);

                // Save the duplicate search results to a file
                SaveToFile(md5sums, duplicates, filesList, pathToSave);
            }


        }
        /// <summary>
        /// Get a list of files recursively from a starting path and store them in a string array
        /// </summary>
        /// <param name="startingPath">Starting directory</param>
        /// <returns>String array of files found</returns>
        string[] GetFileList(string startingPath)
        {
            // Declare a string to store the results in
            string[] results = null;
            // Try/Catch used to make sure file permissions errors don't crash the app
            try
            {
                // Store the file scan results in the results array
                results = Directory.GetFiles(startingPath, "*.*", SearchOption.AllDirectories);
                // Set the FileListGathered bool to true to allow the rest of the functions to continue
                FileListGathered = true;
                // return the results
                return results;
            }
            catch (Exception ex) // If an exception occurs, this will deal with it without crashing the app
            { 
                // Display a message containing the exception
                MessageBox.Show("Error: You probably tried scanning a folder with hidden/system files. \n\nError received from Windows:\n" + ex, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return results;
        }

        /// <summary>
        /// Generate the MD5 sum of the given file and return it as a string
        /// </summary>
        /// <param name="file">Complete path to file that the sum will be generated for</param>
        /// <returns>Lower case MD5 sum</returns>
        static string GetMD5Sum(string file)
        {
            // Start the MD5 generation process
            using (var sum = MD5.Create())
            // Read the bytes of the file we want the sum for
            using (var stream = File.OpenRead(file))
                // Convert the stream to MD5 sum and make it a lower case string. Then return it
                return BitConverter.ToString(sum.ComputeHash(stream)).Replace("-", "").ToLower();

        }

        /// <summary>
        /// Search a string List object of files for duplicate MD5 sums and return the results as a string List object
        /// </summary>
        /// <param name="md5sums">string List object of MD5 sums to search for duplicates</param>
        /// <returns>string List object of duplicate MD5 sums</returns>
        static List<string> DuplicateSearch(List<string> md5sums)
        {
            // Instantiate a string List for storing duplicates
            List<string> duplicates = new List<string>();

            // for every item in the duplicate list
            for (int i = 0; i <= (md5sums.Count() -2); i++)
            {
                // for every item in the duplicate list 1 ahead of the parent for loop
                for (int j = (i + 1); j <= (md5sums.Count() -2); j++)
                {
                    // Compare the two items and add them to the list if they match
                    if (md5sums[i] == md5sums[j])
                    {
                        // Only add them to the list if they aren't aleeady in it
                        if (!duplicates.Contains(md5sums[i]))
                        {
                            duplicates.Add(md5sums[i]);
                        }
                    }
                }
            }
            // return the list of duplicates
            return duplicates;
        }

        /// <summary>
        /// Save the list of duplicate MD5 sums to the specified file
        /// </summary>
        /// <param name="md5sums">MD5 sums string List object to compare</param>
        /// <param name="duplicates">string List object of known duplicates</param>
        /// <param name="files">string array of file paths</param>
        /// <param name="saveFile">File to save output to</param>
        void SaveToFile(List<string> md5sums, List<string> duplicates, string[] files, string saveFile)
        {
            // Using a Try/Catch here to make sure we don't try to write null to the file
            try
            {
                // Delete the old output file if it exists
                if (File.Exists(saveFile))
                {
                    File.Delete(saveFile);
                }
                // Recreate and open the file for writing
                StreamWriter file = File.CreateText(saveFile);
                foreach (string dupeFile in duplicates)
                {
                    // Write out the hash to a line in the file
                    file.WriteLine(dupeFile);
                    file.WriteLine("---------------------------------");


                    //Write out the corresponding file paths for the duplicate
                    for (int i = 0; i <= (files.Length - 1); i++)
                    {
                        if (md5sums[i] == dupeFile)
                        {
                            file.WriteLine(files[i]);
                        }
                    }
                    // Write an empty line before continuing the next iteration
                    file.WriteLine("");
                }

                // Close the file once writing is done
                file.Close();
                // The below line will create the scan results file if it doesn't exists (or overwrite it if it does) and insert the results
                // File.WriteAllLines(saveFile, fileContents, Encoding.UTF8);
            }
            catch (Exception ex) // If an exception occurs, this will deal with it without crashing the app
            {
                // Display a message containing the exception
                MessageBox.Show("Error!: Unable to save data to the results file. \n\nError received from Windows:\n" + ex, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}