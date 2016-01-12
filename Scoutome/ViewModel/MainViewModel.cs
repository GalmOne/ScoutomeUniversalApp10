using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace Scoutome.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        private ICommand _editReunionCommand;
        public ICommand EditReunionCommand
        {
            get
            {
                if (this._editReunionCommand == null)
                {
                    this._editReunionCommand = new RelayCommand(() => EditReunion());
                }
                return this._editReunionCommand;
            }
        }

        private ICommand _newReunionCommand;
        public ICommand NewReunionCommand
        {
            get
            {
                if (this._newReunionCommand == null)
                {
                    this._newReunionCommand = new RelayCommand(() => NewReunion());
                }
                return this._newReunionCommand;
            }
        }

        private ICommand _childrenListCommand;
        public ICommand ChildrenListCommand
        {
            get
            {
                if (this._childrenListCommand == null)
                {
                    this._childrenListCommand = new RelayCommand(() => GoToChildrenlist());
                }
                return this._childrenListCommand;
            }
        }


        public MainViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService;
        }

        private void EditReunion()
        {
            _navigationService.NavigateTo("ReunionPage");
        }       

        private void GoToChildrenlist()
        {
            _navigationService.NavigateTo("ChildrenList");
        }     

        private void NewReunion()
        {
            _navigationService.NavigateTo("AddReunion");
        }
    }
}
