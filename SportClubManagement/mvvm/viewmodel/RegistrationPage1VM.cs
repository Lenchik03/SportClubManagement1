using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SportClubManagement.mvvm.model;

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

                ManagerRepository.Instance.AddManager(SelectedManager);

            });
         }
        internal void SetMainVM(MainVM mainVM)
        {
            this.mainVM = mainVM;
        }
    }
}
