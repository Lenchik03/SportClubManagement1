using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static SportClubManagement.mvvm.viewmodel.ManagerPageVM;
using System.Windows;
using SportClubManagement.mvvm.model;
using SportClubManagement.view;

namespace SportClubManagement.mvvm.viewmodel
{
    internal class EditClientPageVM: BaseVM
    {
        private Membership selectedMembership;
        private TypeActivities selectedTypeActivities;
        private Coach selectedCoach;
        public VmCommand Save { get; set; }
        MainVM mainVM;
        public VmCommand Delete { get; set; }

        private ObservableCollection<Client> clients;
        private Client client = new();
        public ObservableCollection<Client> Clients

        {
            get => clients;
            set
            {
                clients = value;
                Signal();
            }
        }

        public Client Client
        {
            get => client;
            set
            {
                client = value;
                Signal();
            }
        }

        public Membership SelectedMembership
        {
            get => selectedMembership; set
            {
                selectedMembership = value;
                Signal();
            }
        }
        public TypeActivities SelectedTypeActivities
        {
            get => selectedTypeActivities; set
            {
                selectedTypeActivities = value;
                Signal();
            }
        }

        public Coach SelectedCoach
        {
            get => selectedCoach; set
            {
                selectedCoach = value;
                Signal();
            }
        }
        public List<Membership> AllMemberships { get; set; }
        public List<TypeActivities> AllTypeActivities { get; set; }
        public List<Coach> AllCoaches { get; set; }

        public EditClientPageVM()
        {
            AllMemberships = (List<Membership>?)MembershipRepository.Instance.GetAllMemberships();
            AllTypeActivities = (List<TypeActivities>?)TypeActivitiesRepository.Instance.GetAllTypeActivities();
            AllCoaches = (List<Coach>?)CoachRepository.Instance.GetAllCoaches();

            string sql = "SELECT c.clientID, c.FIO, c.membershipID, c.typeActivitiesID, c.coachID, c.classes_days, c.phone_number, c.email, ch.FIO as coach, m.tittle as membership, t.title as typeActivities FROM clients c, coaches ch, memberships m, typeActivities t WHERE c.coachID = ch.coachID AND c.membershipID = m.membershipsID AND c.typeActivitiesID = t.typeActivitiesID;";
            Clients = new ObservableCollection<Client>(ClientRepository.Instance.GetAllClients(sql));

            //команда на удаление клиента с базы
            Delete = new VmCommand(() => {
                if (Client == null)
                    return;

                if (MessageBox.Show("Удаление клиента", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ClientRepository.Instance.Remove(Client);
                    Clients.Remove(Client);
                }
            });

            //команда на добавление в базу или обновление клиента в базе
            Save = new VmCommand(() => {

                Client.MembershipID = SelectedMembership?.ID ?? 1;
                Client.TypeActivitiesID = SelectedTypeActivities?.ID ?? 1;
                Client.CoachID = SelectedCoach?.ID ?? 1;
                if (Client.ID == 0)
                {
                    if (Client.MembershipID == 1)
                        Client.ClassesDays = 1;
                    else if (Client.MembershipID == 2)
                        Client.ClassesDays = 12;
                    else if (Client.MembershipID == 2)
                        Client.ClassesDays = 72;
                    else
                        Client.ClassesDays = 144;
                    ClientRepository.Instance.AddClient(Client);
                    
                }
                else
                {
                    ClientRepository.Instance.Update(Client);
                }


                mainVM.CurrentPage = new ManagerPage(mainVM);
            });

            
        }

        
        internal void SetMainVM(MainVM mainVM)
        {
            this.mainVM = mainVM;
        }

        internal void SetEditClient(Client selectedClient)
        {
            Client = selectedClient;
            SelectedMembership = AllMemberships.FirstOrDefault(s => s.ID == client.MembershipID);
            SelectedTypeActivities = AllTypeActivities.FirstOrDefault(s => s.ID == client.TypeActivitiesID);
            SelectedCoach = AllCoaches.FirstOrDefault(s => s.ID == client.CoachID);
        }
    }
}

