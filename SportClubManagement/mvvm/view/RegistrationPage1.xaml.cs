﻿using SportClubManagement.mvvm.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SportClubManagement.mvvm.view
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage1.xaml
    /// </summary>
    public partial class RegistrationPage1 : Page
    {
        public RegistrationPage1(MainVM mainVM)
        {
            InitializeComponent();
            var vm = ((RegistrationPage1VM)DataContext);
            vm.SetMainVM(mainVM);
            vm.SetPasswordBox(passwrdBox);
        }
    }
}
