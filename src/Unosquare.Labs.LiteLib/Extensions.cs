﻿namespace Unosquare.Labs.LiteLib
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class Extensions
    {
        private const string IntegerAffinity = "INTEGER";
        private const string NumericAffinity = "NUMERIC";
        private const string TextAffinity = "TEXT";
        private const string DateTimeAffinity = "DATETIME";

        private static readonly Dictionary<Type, string> TypeMappings = new Dictionary<Type, string>
        {
            {typeof(short), IntegerAffinity},
            {typeof(int), IntegerAffinity},
            {typeof(long), IntegerAffinity},
            {typeof(ushort), IntegerAffinity},
            {typeof(uint), IntegerAffinity},
            {typeof(ulong), IntegerAffinity},
            {typeof(byte), IntegerAffinity},
            {typeof(char), IntegerAffinity},
            {typeof(decimal), NumericAffinity},
            {typeof(bool), NumericAffinity},
            {typeof(DateTime), DateTimeAffinity},
        };

        /// <summary>
        /// Gets the type mapping.
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns>A property type of the mapping.</returns>
        public static string GetTypeMapping(this Type propertyType) => TypeMappings.ContainsKey(propertyType) ? TypeMappings[propertyType] : TextAffinity;

        /// <summary>
        /// Transform a DateTime to a SQLite UTC date.
        /// </summary>
        /// <param name="utcDate">The UTC date.</param>
        /// <returns>UTC DateTime.</returns>
        public static DateTime ToSQLiteUtcDate(this DateTime utcDate)
        {
            var startupDifference = (int)DateTime.UtcNow.Subtract(DateTime.Now).TotalHours;
            return utcDate.AddHours(startupDifference);
        }
    }
}