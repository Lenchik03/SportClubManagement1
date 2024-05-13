using SportClubManagement.view;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SportClubManagement.mvvm.viewmodel
{
    public class MainVM : BaseVM
    {
        private Page currentPage;

        public static MainVM Instance { get; set; }


        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                Signal();
            }
        }

        public VmCommand Search { get; set; }
        public VmCommand SignOut { get; set; }
        public VmCommand MainPage { get; set; }

        public MainVM()
        {
            Instance = this;
            Search = new VmCommand(() =>
            {
                OpenSearch();
            });
            CurrentPage = new RegistrationPage();

            SignOut = new VmCommand(() =>
            {
                CurrentPage = new RegistrationPage();
            });

            MainPage = new VmCommand(() =>
            {
                CurrentPage = new ManagerPage(this);
            });


        }

        private void OpenSearch()
        {

            CurrentPage = new ManagerPage(this);
        }
    }
}
