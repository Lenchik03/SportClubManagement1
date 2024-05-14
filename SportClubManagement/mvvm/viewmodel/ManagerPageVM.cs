using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportClubManagement.mvvm.viewmodel.ManagerPageVM;
using System.Windows;
using SportClubManagement.mvvm.model;
using SportClubManagement.view;

namespace SportClubManagement.mvvm.viewmodel
{ 
        public class ManagerPageVM : BaseVM
        {

        private MainVM mainVM;
        private string searchText = "";
        private ObservableCollection<Client> clients;
        private ObservableCollection<Coach> coaches;
        private ObservableCollection<Membership> memberships;
        private ObservableCollection<TypeActivities> typeActivities;
        public ObservableCollection<Coach> AllCoaches { get; set; }
        public ObservableCollection<Membership> AllMemberships { get; set; }
        public ObservableCollection<TypeActivities> AllTypeActivities { get; set; }
        private Coach selectedCoach;
        private Membership selectedMembership;
        private TypeActivities selectedTypeActivities;


        public VmCommand Create { get; set; }
        public VmCommand Edit { get; set; }
        public VmCommand Delete { get; set; }
        public VmCommand CreateCoach { get; set; }
        public VmCommand EditCoach { get; set; }
        public VmCommand RemoveCoach { get; set; }
            

            public Coach SelectedCoach
            {
                get => selectedCoach;
                set
                {
                    selectedCoach = value;
                    Signal();
                    Search();
                }
            }
            public Membership SelectedMembership
            {
                get => selectedMembership;
                set
                {
                    selectedMembership = value;
                    Signal();
                    Search();
                }
            }

            public TypeActivities SelectedTypeActivities
            {
                get => selectedTypeActivities;
                set
                {
                    selectedTypeActivities = value;
                    Signal();
                    Search();
                }
            }

            public string SearchText
            {
                get => searchText;
                set
                {
                    searchText = value;
                    Search();
                }
            }

            public Client SelectedClient { get; set; }
            public ObservableCollection<Client> Clients
            {
                get => clients;
                set
                {
                    clients = value;
                    Signal();
                }
            }

        public ObservableCollection<Coach> Coaches
        {
            get => coaches;
            set
            {
                coaches = value;
                Signal();
            }
        }


        public ManagerPageVM()
            {

                AllCoaches = new ObservableCollection<Coach>(CoachRepository.Instance.GetAllCoaches());
                AllCoaches.Insert(0, new Coach { ID = 0, FIO = "Все тренеры" });
                SelectedCoach = AllCoaches[0];

                AllMemberships = new ObservableCollection<Membership>(MembershipRepository.Instance.GetAllMemberships());
                AllMemberships.Insert(0, new Membership { ID = 0, Title = "Все абонементы" });
                SelectedMembership = AllMemberships[0];

                AllTypeActivities = new ObservableCollection<TypeActivities>(TypeActivitiesRepository.Instance.GetAllTypeActivities());
                AllTypeActivities.Insert(0, new TypeActivities { ID = 0, Title = "Все виды абонементов" });
                SelectedTypeActivities = AllTypeActivities[0];

                string sql = "SELECT c.clientID, c.FIO, c.membershipID, c.typeActivitiesID, c.coachID, c.classes_days, c.phone_number, c.email, ch.FIO as coach, m.tittle as membership, t.title as typeActivities FROM clients c, coaches ch, memberships m, typeActivities t WHERE c.coachID = ch.coachID AND c.membershipID = m.membershipsID AND c.typeActivitiesID = t.typeActivitiesID;";
                Clients = new ObservableCollection<Client>(ClientRepository.Instance.GetAllClients(sql));
                Coaches = new ObservableCollection<Coach>(CoachRepository.Instance.GetAllCoaches());

                Create = new VmCommand(() =>
                {
                    mainVM.CurrentPage = new EditClientPage(mainVM);
                });

                Edit = new VmCommand(() => {
                    if (SelectedClient == null)
                        return;
                    mainVM.CurrentPage = new EditClientPage(mainVM, SelectedClient);
                });

                Delete = new VmCommand(() => {
                    if (SelectedClient == null)
                        return;

                    if (MessageBox.Show("Удаление клиента", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        ClientRepository.Instance.Remove(SelectedClient);
                        Clients.Remove(SelectedClient);
                    }
                });

                CreateCoach  = new VmCommand(() =>
                {
                    mainVM.CurrentPage = new EditCoachPage(mainVM);
                });

                EditCoach = new VmCommand(() =>
                {
                    mainVM.CurrentPage = new EditCoachPage(mainVM, selectedCoach);
                });

                RemoveCoach = new VmCommand(() => {
                    if (SelectedCoach == null)
                        return;

                    if (MessageBox.Show("Удаление тренера", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        CoachRepository.Instance.Remove(SelectedCoach);
                        Coaches.Remove(SelectedCoach);
                    }
                });


            //Edit = new VmCommand(() => {
            //    if (SelectedClient == null)
            //        return;
            //    mainVM.CurrentPage = new EditClientPage(mainVM, SelectedClient);
            //});
        }

            internal void SetMainVM(MainVM mainVM)
            {
                this.mainVM = mainVM;
            }

            private void Search()
            {
                Clients = new ObservableCollection<Client>(
                    ClientRepository.Instance.Search(SearchText, SelectedCoach, SelectedMembership, SelectedTypeActivities));
            }
        }
    }
