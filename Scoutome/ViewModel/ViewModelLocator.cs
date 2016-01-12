using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace Scoutome.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ReunionViewModel>();
            SimpleIoc.Default.Register<InformationsReunionViewModel>();
            SimpleIoc.Default.Register<AddReunionViewModel>();
            SimpleIoc.Default.Register<ChildrenListViewModel>();

            NavigationService navigationService = new NavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            navigationService.Configure("MainPage", typeof(MainPage));
            navigationService.Configure("ReunionPage", typeof(ReunionPage));
            navigationService.Configure("InformationsReunionPage", typeof(InformationsReunionPage));
            navigationService.Configure("AddReunion", typeof(AddReunion));
            navigationService.Configure("ChildrenList", typeof(ChildrenList));
        }

        public MainViewModel MainPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public ReunionViewModel ReunionPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ReunionViewModel>();
            }
        }
        public InformationsReunionViewModel InformationsReunionPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InformationsReunionViewModel>();
            }
        }
        public AddReunionViewModel AddReunion
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddReunionViewModel>();
            }
        }
        public ChildrenListViewModel ChildrenList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChildrenListViewModel>();
            }
        }
    }
}
