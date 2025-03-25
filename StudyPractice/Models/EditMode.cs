using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace StudyPractice.Models;

public enum EditMode
{
    Add, 
    Edit
}

public class EnumToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
        {
            return false;
        }
        
        return value.ToString() == parameter.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return BindingValue<object>.Unset;
    }
    
    
}