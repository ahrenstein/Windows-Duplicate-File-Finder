/**
 * Copyright (c) 2014 Nevec Networks LLC., All Rights Reserved.
 * INTERNAL/PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 * DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
 */
using System;
using System.Reflection;

namespace WindowsDuplicateFileFinder
{
    /// <summary>
    /// Static class to build the engine version string.
    /// </summary>
    public class AssemblyVersion
    {
        /**
         * Fields
         */
        public static string _NAME;
        public static string _VERSION;
        public static string _BUILD;
        public static string _BUILD_DATE;
        public static string _COPYRIGHT;

#if  DEBUG
        public static string _DEBUG = "DEBUG_BUILD";
#else
        public static string _DEBUG = "";
#endif
        public static string _VERSION_STRING;

        /**
         * Methods
         */
        /// <summary>
        /// Initializes static members of the AssemblyVersion class.
        /// </summary>
        static AssemblyVersion()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyProductAttribute asmProd = asm.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0] as AssemblyProductAttribute;
            AssemblyDescriptionAttribute asmDesc = asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0] as AssemblyDescriptionAttribute;
            AssemblyCopyrightAttribute asmCopyright = asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0] as AssemblyCopyrightAttribute;
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(asm.GetName().Version.Build).AddSeconds(asm.GetName().Version.Revision * 2);
            _NAME = asmProd.Product;
            _VERSION = asm.GetName().Version.Major + "." + asm.GetName().Version.Minor + "." + asm.GetName().Version.Revision;
            _BUILD = string.Empty + asm.GetName().Version.Build;
            _BUILD_DATE = buildDate.ToShortDateString() + " @ " + buildDate.ToShortTimeString();
            _COPYRIGHT = asmCopyright.Copyright;

            _VERSION_STRING = AssemblyVersion._NAME + " " + AssemblyVersion._DEBUG + " " + AssemblyVersion._VERSION + " build " + AssemblyVersion._BUILD;
        }
    }
}
