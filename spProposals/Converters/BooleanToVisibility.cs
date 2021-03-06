﻿/*=====================================================================
  File:      BooleanToVisibility.cs

  Summary:   Converter class for converting Boolean to Visibility. 


=====================================================================*/

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace spProposals.Converters
{
    /// <summary>
    /// BooleanToVisibility Converter. Checks a boolean property and changes it to
    /// either Visible or Collapsed
    /// </summary>
    public class BooleanToVisibility : IValueConverter
    {
        /// <summary>
        /// Method that returns the Visibility status depending on 'value'.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                value = System.Convert.ToBoolean(value);
            }
            return ConvertValue((bool)value);
        }

        /// <summary>
        /// Method that calls ConverCollapsed and returns Visbility
        /// </summary>
        /// <param name="value"></param>
        protected virtual Visibility ConvertValue(bool value)
        {
            return ConvertCollapsed(value);
        }

        /// <summary>
        /// Method that converts Visbilility to Boolean. Not implemented.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Internal method to return Visibility depending on isVisible
        /// </summary>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        internal static Visibility ConvertCollapsed(bool isVisible)
        {
            return isVisible ?
                        Visibility.Visible :
                        Visibility.Collapsed;
        }
    }
}
