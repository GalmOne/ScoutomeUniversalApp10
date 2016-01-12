using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Scoutome.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using Scoutome.DAO;
using Windows.UI.Popups;

namespace Scoutome.ViewModel
{
    public class ReunionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private DataAccessObject data = new DataAccessObject();

        private ObservableCollection<Reunion> _reunions;
        public ObservableCollection<Reunion> Reunions
        {
            get
            {
                return _reunions;
            }

            set
            {
                _reunions = value;
                RaisePropertyChanged("Reunions");
            }
        }

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

        private ICommand _infoReunionCommand;
        public ICommand InfoReunionCommand
        {
            get
            {
                if (this._infoReunionCommand == null)
                {
                    this._infoReunionCommand = new RelayCommand(() => GoToInformationsReunion());
                }
                return this._infoReunionCommand;
            }
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
        public ReunionViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService;         
        }

        public async void OnNavigatedTo(NavigationEventArgs e)
        {
            Reunions = await data.CallWebApiReunionList("reunions");
            if (Reunions == null)
            {
                CreatePopUp("ErrorInternet");
                BackToMain();
            }
        }      

        private void GoToInformationsReunion()
        {
            if (SelectedReunion != null)
                _navigationService.NavigateTo("InformationsReunionPage", SelectedReunion);
            else
            {
                CreatePopUp("NoSelectReunion");
            }
        }
       
        private void BackToMain()
        {
            _navigationService.NavigateTo("MainPage");
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
