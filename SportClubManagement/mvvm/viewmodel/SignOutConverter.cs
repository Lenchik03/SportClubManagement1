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
    public class SignOutConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Visible;

            string p = parameter.ToString();
            if (p == "RegistrationPage" && value is RegistrationPage)
                return Visibility.Collapsed;
            if (p == "RegistrationPage1" && value is RegistrationPage1)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
