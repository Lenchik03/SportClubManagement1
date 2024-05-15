using SportClubManagement.mvvm.view;
using SportClubManagement.view;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SportClubManagement.mvvm.viewmodel
{
    public class PageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;

            string p = parameter.ToString();
            if (p == "ManagerPage" && value is ManagerPage)
                return Visibility.Visible;
            if (p == "EditClientPage" && value is EditClientPage)
                return Visibility.Visible;
            if (p == "EditCoachPage" && value is EditCoachPage)
                return Visibility.Visible;

            return Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
