using SportClubManagement.mvvm.model;
using SportClubManagement.mvvm.viewmodel;
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

namespace SportClubManagement.view
{
    /// <summary>
    /// Логика взаимодействия для EditClientPage.xaml
    /// </summary>
    public partial class EditClientPage : Page
    {
        public EditClientPage(MainVM mainVM)
        {
            InitializeComponent();
            var vm = ((EditClientPageVM)DataContext);
            vm.SetMainVM(mainVM);
        }

        public EditClientPage(MainVM mainVM, Client selectedClient) : this(mainVM)
        {
            ((EditClientPageVM)DataContext).SetEditClient(selectedClient);
        }
    }
}
