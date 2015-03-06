﻿/*=====================================================================
  File:      BooleanToVisibility.cs

  Summary:   Converter class for converting Boolean to Visibility. 
=====================================================================*/

using System.Windows;

namespace spProposals.Converters
{
    /// <summary>
    /// NotBooleanToVisibility Converter. Implements the BooleanToVisibility Converter and 
    /// overrides the ConvertValue method to return the opposite of the implementation in the
    /// base class.
    /// </summary>
    public class NotBooleanToVisibility :
        BooleanToVisibility
    {
        /// <summary>
        /// Method that overrides the ConvertValue method of the base class. 
        /// </summary>
        /// <param name="value"></param>
        protected override Visibility ConvertValue(bool value)
        {
            return ConvertCollapsed(!value);
        }
    } 
}
