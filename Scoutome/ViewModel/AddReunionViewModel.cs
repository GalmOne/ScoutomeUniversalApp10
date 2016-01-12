using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Scoutome.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Scoutome.DAO;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace Scoutome.ViewModel
{
    public class AddReunionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private DataAccessObject data = new DataAccessObject();
        private Reunion reunion;

        private String mylieu;
        public String Mylieu
        {
            get { return mylieu; }
            set { mylieu = value; }
        }

        private String mylibelle = "Réunion du " + DateTime.Now.ToString("dd/MM/yyyy");
        public String Mylibelle
        {
            get { return mylibelle; }
            set { mylibelle = value; }
        }

        private double myprix = 1;
        public double Myprix
        {
            get { return myprix; }
            set { myprix = value; }
        }

        private String myDate = DateTime.Now.ToString("dd/MM/yyyy");
        public String MyDate
        {
            get { return myDate; }
            set { myDate = value; }
        }

        private ObservableCollection<Anime> myAnimeListView;
        public ObservableCollection<Anime> MyAnimeListView
        {
            get { return myAnimeListView; }
            set { myAnimeListView = value;
                RaisePropertyChanged("MyAnimeListView");
            }
        }

        private ObservableCollection<Anime> selectedAnime;
        public ObservableCollection<Anime> SelectedAnime
        {
            get { return selectedAnime; }

            set
            {
                selectedAnime = value;
                RaisePropertyChanged("SelectedAnime");
            }
        }

        private ICommand _addReunionCommand;
        public ICommand AddReunionCommand
        {
            get
            {
                if (_addReunionCommand == null)
                {
                    _addReunionCommand = new RelayCommand(() => AddReunionCmd());
                }
                return _addReunionCommand;
            }
        }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new RelayCommand(() => BackMainPage());
                }
                return _backCommand;
            }
        }       
        

        [PreferredConstructor]
        public AddReunionViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService;
            reunion = new Reunion();
            myAnimeListView = new ObservableCollection<Anime>();
            SelectedAnime = new ObservableCollection<Anime>();                      
        }

        public async void OnNavigatedTo(NavigationEventArgs e)
        {
            MyAnimeListView = await data.GetChildrenList("animes");
            if (MyAnimeListView == null)
            {
                CreatePopUp("ErrorInternet");
                BackMainPage();
            }
        }

        public async void AddReunionCmd()
        {
            if (myAnimeListView == null || myAnimeListView.Count() < 1)
            {
                CreatePopUp("ErrorInternet");               
            }
            else
            { 
                CreateReunionObject();
                TrtSelectedItems();
                await AddReunion();
            }               
        }

        private async Task AddReunion()
        {
            bool response = await data.AddReunion(reunion, selectedAnime);

            if (response)
            {
                CreatePopUp("SuccesAjoutReunion");
                _navigationService.NavigateTo("MainPage");
            }
            else
            {
                CreatePopUp("ErreurAjoutReunion");
            }
        }       

        public void BackMainPage()
        {
            _navigationService.NavigateTo("MainPage");
        }

        public void CreateReunionObject()
        {
            DateTime date = DateTime.Now;
            reunion.codeReunion = date.Year * 10000 + date.Month * 100 + date.Day + date.Hour * 60 + date.Minute;
            reunion.dateReunion = date.ToString("dd/MM/yyyy");
            reunion.libelle = mylibelle;
            reunion.lieu = mylieu;
            reunion.prix = myprix;
        }

        public void TrtSelectedItems()
        {
            for (int i = 0; i < 3; i++)
            {
                selectedAnime.Add(myAnimeListView[i]);
            }

        }

        public void CreatePopUp(String value)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var str = loader.GetString(value);

            var messageDialog = new MessageDialog(str);
            messageDialog.ShowAsync();
        }
    }
}
