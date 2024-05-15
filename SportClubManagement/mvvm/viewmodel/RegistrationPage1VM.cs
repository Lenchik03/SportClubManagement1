using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SportClubManagement.mvvm.model;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using SportClubManagement.view;

namespace SportClubManagement.mvvm.viewmodel
{
    public class RegistrationPage1VM: BaseVM
    {
        MainVM mainVM;
        public VmCommand Save { get; set; }
        private Manager selectedManager = new();
        public Manager SelectedManager
        {
            get => selectedManager;
            set
            {
                selectedManager = value;
                Signal();
            }
        }
        public RegistrationPage1VM()
        {
            Save = new VmCommand(() =>
            {
                SelectedManager.Password = Password;
                ManagerRepository.Instance.AddManager(SelectedManager);
                RegistrationPage managerPage = new RegistrationPage();
                MainVM.Instance.CurrentPage = managerPage;

            });
         }

        internal void SetMainVM(MainVM mainVM)
        {
            this.mainVM = mainVM;
        }
        internal void SetPasswordBox(PasswordBox passwrdBox)
        {
            this.passwrdBox = passwrdBox;
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
