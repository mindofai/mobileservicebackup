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
            var debts = new List<Debt>()
            {
                new Debt
                {
                    Name = "John",
                    Amount = 100,
                    IsPaid = false
                },new Debt
                {
                    Name = "Mark",
                    Amount = 250,
                    IsPaid = false
                },new Debt
                {
                    Name = "Arnold",
                    Amount = 500,
                    IsPaid = false
                },new Debt
                {
                    Name = "William",
                    Amount = 320,
                    IsPaid = false
                }
            };

            listView.ItemsSource = debts.FindAll(d => d.IsPaid == false);
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            MainPageViewModel vm = (MainPageViewModel)BindingContext;
            vm.SelectedDebt = (Debt)e.Item;
            vm.OpenUpdate();
        }
    }
}
