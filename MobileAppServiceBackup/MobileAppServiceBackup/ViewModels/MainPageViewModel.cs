using MobileAppServiceBackup.Models;
using MobileAppServiceBackup.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MobileAppServiceBackup.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private AzureMobileService _azureMobileService;

        private List<Debt> _debts;
        public List<Debt> Debts
        {
            get { return _debts; }
            set { SetProperty(ref _debts, value); }
        }

        public MainPageViewModel()
        {
            _azureMobileService = DependencyService.Get<AzureMobileService>();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {

            Debts = await _azureMobileService.GetAllDebts();
            var debt = Debts[0];

            debt.Amount = 200;

            await _azureMobileService.UpdateDebt(debt);
            Debts = await _azureMobileService.GetAllDebts();


        }
    }
}
