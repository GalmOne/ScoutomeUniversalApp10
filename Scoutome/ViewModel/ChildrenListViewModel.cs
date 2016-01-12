using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Scoutome.DAO;
using Scoutome.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace Scoutome.ViewModel
{
    public class ChildrenListViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private DataAccessObject data = new DataAccessObject();

        private ObservableCollection<Anime> myAnimeListView;
        public ObservableCollection<Anime> MyAnimeListView
        {
            get { return myAnimeListView; }
            set { myAnimeListView = value;
                  RaisePropertyChanged("MyAnimeListView"); }
        }   

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (this._backCommand == null)
                {
                    this._backCommand = new RelayCommand(() => BackToMain());
                }
                return this._backCommand;
            }
        }


        [PreferredConstructor]
        public ChildrenListViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService;
            myAnimeListView = new ObservableCollection<Anime>();         
        }

        public async void OnNavigatedTo(NavigationEventArgs e)
        {
            MyAnimeListView = await data.GetChildrenList("animes");
            if (MyAnimeListView == null)
            {
                CreatePopUp("ErrorInternet");
                BackToMain();
            }             
        }

        private void BackToMain()
        {
            _navigationService.NavigateTo("MainPage");
        }

        public void CreatePopUp(string value)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var str = loader.GetString(value);
            var messageDialog = new MessageDialog(str);
            messageDialog.ShowAsync();
        }
    }
}