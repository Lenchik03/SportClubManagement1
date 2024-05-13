using SportClubManagement.mvvm.model;
using SportClubManagement.view;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SportClubManagement.mvvm.viewmodel
{
    public class EditCoachPageVM: BaseVM
    {
        public VmCommand Save { get; set; }
        MainVM mainVM;
        private TypeActivities selectedTypeActivities;
        public List<TypeActivities> AllTypeActivities { get; set; }
        public VmCommand Delete { get; set; }
        private ObservableCollection<Coach> coaches;
        private Coach coach = new();
        public ObservableCollection<Coach> Coaches

        {
            get => coaches;
            set
            {
                coaches = value;
                Signal();
            }
        }

        public Coach Coach
        {
            get => coach;
            set
            {
                coach = value;
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


        public EditCoachPageVM()
        {
            AllTypeActivities = (List<TypeActivities>?)TypeActivitiesRepository.Instance.GetAllTypeActivities();

            Coaches = new ObservableCollection<Coach>(CoachRepository.Instance.GetAllCoaches());

            //команда на добавление в базу или обновление тренера в базе
            Save = new VmCommand(() => {
                if (Coach.ID == 0)
                {

                    CoachRepository.Instance.AddCoach(Coach);

                }
                else
                {
                    CoachRepository.Instance.Update(Coach);
                }


                mainVM.CurrentPage = new ManagerPage(mainVM);
            });
        }


        internal void SetMainVM(MainVM mainVM)
        {
            this.mainVM = mainVM;
        }

        internal void SetEditCoach(Coach selectedCoach)
        {
            Coach = selectedCoach;
            SelectedTypeActivities = AllTypeActivities.FirstOrDefault(s => s.ID == coach.TypeActivitiesID);
        }
    }
}

