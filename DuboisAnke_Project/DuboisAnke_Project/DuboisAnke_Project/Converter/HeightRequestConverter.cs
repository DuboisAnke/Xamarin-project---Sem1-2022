using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DuboisAnke_Project.Data;
using Xamarin.Forms;

namespace DuboisAnke_Project.Converter
{
    public class HeightRequestConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int amountOfProjects = DbService.AllProjectCollection.Count;
            int totalHeight = amountOfProjects * 50;
            return totalHeight;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
