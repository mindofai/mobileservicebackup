using MobileAppServiceBackup.Models;
using MobileAppServiceBackup.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MobileAppServiceBackup.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {   
            InitializeComponent();
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            MainPageViewModel vm = (MainPageViewModel)BindingContext;
            vm.SelectedDebt = (Debt)e.Item;
            vm.OpenUpdate();
        }
    }
}
