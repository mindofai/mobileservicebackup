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
            _addDebtCommand ?? (_addDebtCommand = new DelegateCommand(AddDebt));

        private DelegateCommand _updateDebtCommand;
        public DelegateCommand UpdateDebtCommand =>
            _updateDebtCommand ?? (_updateDebtCommand = new DelegateCommand(UpdateDebt));



        public MainPageViewModel()
        {
            NameValue = string.Empty;
            AmountValue = string.Empty;

            IsBusy = false;
            IsAddShow = false;

            SelectedDebt = new Debt();
        }

        public void UpdateDebt()
        {
            IsBusy = true;
        }

        public void AddDebt()
        {
            IsBusy = true;
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

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }

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
