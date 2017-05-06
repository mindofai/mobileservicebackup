using MobileAppServiceBackup.Models;
using MobileAppServiceBackup.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileAppServiceBackup.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        #region Properties
        private List<Debt> _debts;
        public List<Debt> Debts
        {
            get { return _debts; }
            set { SetProperty(ref _debts, value); }
        }

        private string _nameValue;

        public string NameValue
        {
            get { return _nameValue; }
            set { SetProperty(ref _nameValue, value); }
        }

        private string _amountValue;

        public string AmountValue
        {
            get { return _amountValue; }
            set { SetProperty(ref _amountValue, value); }
        }


        private bool _isAddShow;

        public bool IsAddShow
        {
            get { return _isAddShow; }
            set { SetProperty(ref _isAddShow, value); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private bool _isRefreshBusy;

        public bool IsRefreshBusy
        {
            get { return _isRefreshBusy; }
            set { SetProperty(ref _isRefreshBusy, value); }
        }


        private bool _isPaidShow;

        public bool IsPaidShow
        {
            get { return _isPaidShow; }
            set { SetProperty(ref _isPaidShow, value); }
        }

        private Debt _selectedDebt;

        public Debt SelectedDebt
        {
            get { return _selectedDebt; }
            set { SetProperty(ref _selectedDebt, value); }
        }

        private DelegateCommand _openAddCommand;
        public DelegateCommand OpenAddCommand =>
            _openAddCommand ?? (_openAddCommand = new DelegateCommand(OpenAdd));

        private DelegateCommand _closeAddCommand;
        public DelegateCommand CloseAddCommand =>
            _closeAddCommand ?? (_closeAddCommand = new DelegateCommand(CloseAdd));

        private DelegateCommand _addDebtCommand;
        public DelegateCommand AddDebtCommand =>
            _addDebtCommand ?? (_addDebtCommand = new DelegateCommand(async () => await AddDebt()));

        private DelegateCommand _updateDebtCommand;
        public DelegateCommand UpdateDebtCommand =>
            _updateDebtCommand ?? (_updateDebtCommand = new DelegateCommand(async () => await UpdateDebt()));

        private DelegateCommand _refreshDebtsCommand;

        public DelegateCommand RefreshDebtsCommand =>
            _refreshDebtsCommand ?? (_refreshDebtsCommand = new DelegateCommand(async () => await RefreshDebts()));

        private AzureMobileService _azureMobileService;

        #endregion

        #region Constructor
        public MainPageViewModel()
        {

            _azureMobileService = DependencyService.Get<AzureMobileService>();

            Debts = new List<Debt>();
            SelectedDebt = new Debt();
            NameValue = string.Empty;
            AmountValue = string.Empty;

            IsBusy = false;
            IsAddShow = false;
        }
        #endregion

        #region Methods
        private async Task RefreshDebts()
        {
            IsRefreshBusy = true;
            var debts = await _azureMobileService.GetAllDebts();
            Debts = debts.Where(d => d.IsPaid == false).ToList();
            IsRefreshBusy = false;
        }

        public async Task UpdateDebt()
        {
            IsBusy = true;

            await _azureMobileService.UpdateDebt(SelectedDebt);
            await RefreshDebts();

            IsBusy = false;
            IsPaidShow = false;
        }

        public async Task AddDebt()
        {
            IsBusy = true;
            var debt = new Debt()
            {
                Name = NameValue,
                Amount = Convert.ToDouble(AmountValue),
                IsPaid = false
            };
            await _azureMobileService.AddDebt(debt);

            await RefreshDebts();
            IsBusy = false;
            IsAddShow = false;
        }


        private void OpenAdd()
        {
            NameValue = string.Empty;
            AmountValue = string.Empty;

            IsAddShow = true;
        }

        public void OpenUpdate()
        {
            IsPaidShow = true;
        }

        private void CloseAdd()
        {
            IsAddShow = false;
            IsPaidShow = false;
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await RefreshDebts();

        }
        #endregion

        #region NavigationHelpers
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        #endregion
    }
}
