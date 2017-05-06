using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MobileAppServiceBackup.Models;
using MobileAppServiceBackup.Services;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureMobileService))]
namespace MobileAppServiceBackup.Services
{
    public class AzureMobileService
    {
        public MobileServiceClient Client { get; private set; }
        private IMobileServiceSyncTable<Debt> debtTable;

        private async Task Initialize()
        {
            Client = new MobileServiceClient("mobile app base url here");

            var path = Path.Combine(MobileServiceClient.DefaultDatabasePath, "nameoflocaldb.db");

            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<Debt>();  

            await Client.SyncContext.InitializeAsync(store);

            debtTable = Client.GetSyncTable<Debt>();
        }

        private async Task SyncDebt()
        {
            await debtTable.PullAsync("queryName", debtTable.CreateQuery());
            await Client.SyncContext.PushAsync();
        }

        public async Task<List<Debt>> GetAllDebts()
        {
            await Initialize();
            await SyncDebt();
            return await debtTable.ToListAsync();
        }

        public async Task<bool> AddDebt(Debt debt)
        {
            try
            {
                await debtTable.InsertAsync(debt);
                await SyncDebt();
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
                debt.IsPaid = true;
                await debtTable.UpdateAsync(debt);
                await SyncDebt();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
