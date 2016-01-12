using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Scoutome.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace Scoutome.ViewModel
{
    public class InformationsReunionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        private Reunion _selectedReunion;
        public Reunion SelectedReunion
        {
            get
            {
                return _selectedReunion;
            }

            set
            {
                _selectedReunion = value;
                RaisePropertyChanged("SelectedReunion");
            }
        }
        
        private ObservableCollection<Anime> childrenPresents;
        public ObservableCollection<Anime> ChildrenPresents
        {
            get { return childrenPresents; }
            set { childrenPresents = value; }
        }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (this._backCommand == null)
                {
                    this._backCommand = new RelayCommand(() => BackToReunion());
                }
                return this._backCommand;
            }
        }

        public string Libelle
        {
            get { return SelectedReunion.libelle; }
            set { SelectedReunion.libelle = value; }
        }
        public string Date
        {
            get { return SelectedReunion.dateReunion; }
            set { SelectedReunion.dateReunion = value; }
        }
        public string Lieu
        {
            get { return SelectedReunion.lieu; }
            set { SelectedReunion.lieu = value; }
        }
        public double Prix
        {
            get { return SelectedReunion.prix; }
            set { SelectedReunion.prix = value; }
        }


        [PreferredConstructor]
        public InformationsReunionViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService;
            SelectedReunion = new Reunion();
            childrenPresents = new ObservableCollection<Anime>();      
            PopulateListView();
        }
      
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            SelectedReunion = (Reunion)e.Parameter;
        }             

        private void BackToReunion()
        {
            _navigationService.NavigateTo("ReunionPage");
        }

        public void PopulateListView ()
        {
            Anime anime = new Anime();
            anime.nom = "Petit";
            anime.prenom = "Arnaud";
            childrenPresents.Add(anime);
            Anime anime1 = new Anime();
            anime1.nom = "Mathieu";
            anime1.prenom = "Bastien";
            childrenPresents.Add(anime1);
            Anime anime2 = new Anime();
            anime2.nom = "Herman";
            anime2.prenom = "Axel";
            childrenPresents.Add(anime2);
        }
    }
}
