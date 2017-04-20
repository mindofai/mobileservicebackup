using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MobileAppServiceBackup.Models;
using MobileAppServiceBackup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureMobileService))]
namespace MobileAppServiceBackup.Services
{
    public class AzureMobileService
    {
        private string appUrl = "https://pautangmobileservice.azurewebsites.net/";

        IMobileServiceTable<Debt> debts;

        private MobileServiceClient Client;
        public AzureMobileService()
        {
        }

        private async Task Initialize()
        {

            Client = new MobileServiceClient(appUrl);
            debts = Client.GetTable<Debt>();
        }

        public async Task<List<Debt>> GetAllDebts()
        {
            await Initialize();
            return await debts.ToListAsync();
        }

        public async Task<bool> AddDebt(Debt debt)
        {
            try
            {
                await Initialize();
                await debts.InsertAsync(debt);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateDebt(Debt debt)
        {
            try
            {
                await Initialize();
                await debts.UpdateAsync(debt);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SyncDebts()
        {
            try
            {
                await Initialize();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
