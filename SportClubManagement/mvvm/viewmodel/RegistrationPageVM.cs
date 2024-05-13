using SportClubManagement.mvvm.model;
using SportClubManagement.mvvm.view;
using SportClubManagement.view;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SportClubManagement.mvvm.viewmodel
{
    public class RegistrationPageVM : BaseVM
    {
        MainVM mainVM;
        public VmCommand Open { get; }
        public VmCommand Registr { get; }

        public RegistrationPageVM()
        {
            Open = new VmCommand(() =>
            {
                ActiveManager.Instance.Manager = ManagerRepository.Instance.ActiveManager(Email, Password);
                var manager = ActiveManager.Instance.Manager;
                if (manager.ID == 0)
                {
                    MessageBox.Show("Неверный логин или пароль");
                }

                else
                {
                    ManagerPage managerPage = new ManagerPage(mainVM);
                    mainVM.CurrentPage = managerPage;
                }
            });

            Registr = new VmCommand(() =>
            {
                RegistrationPage1 registrationPage1 = new RegistrationPage1();
                mainVM.CurrentPage = registrationPage1;
            });
        }

        internal void SetPasswordBox(PasswordBox passwrdBox)
        {
            this.passwrdBox = passwrdBox;
            passwrdBox.PasswordChanged += EventPassword;
        }
        internal void SetMainVM(MainVM mainVM)
        {
            this.mainVM = mainVM;
        }

        private void EventPassword(object sender, RoutedEventArgs e)
        {
            Signal(nameof(Password));
        }

        private string emailBox;
        public string Email
        {
            get { return emailBox; }
            set
            {
                emailBox = value;
            }
        }

        private PasswordBox passwrdBox;

        public string Password
        {
            get { return passwrdBox.Password; }
            set
            {
                passwrdBox.Password = value;
            }
        }
    }
    }
